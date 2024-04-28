using System;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A5 RID: 421
	public abstract class ToolbotDualWieldBase : GenericCharacterMain, ISkillState
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00020570 File Offset: 0x0001E770
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00020578 File Offset: 0x0001E778
		public GenericSkill activatorSkillSlot { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00020581 File Offset: 0x0001E781
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00020589 File Offset: 0x0001E789
		private protected GenericSkill primary1Slot { protected get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00020592 File Offset: 0x0001E792
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0002059A File Offset: 0x0001E79A
		private protected GenericSkill primary2Slot { protected get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldAllowPrimarySkills
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000205A4 File Offset: 0x0001E7A4
		public override void OnEnter()
		{
			base.OnEnter();
			this.allowPrimarySkills = this.shouldAllowPrimarySkills;
			if (NetworkServer.active && base.characterBody)
			{
				if (ToolbotDualWieldBase.penaltyBuff && this.applyPenaltyBuff)
				{
					base.characterBody.AddBuff(ToolbotDualWieldBase.penaltyBuff);
				}
				if (ToolbotDualWieldBase.bonusBuff && this.applyBonusBuff)
				{
					base.characterBody.AddBuff(ToolbotDualWieldBase.bonusBuff);
				}
			}
			if (base.skillLocator)
			{
				this.primary1Slot = base.skillLocator.FindSkillByFamilyName("ToolbotBodyPrimary1");
				this.primary2Slot = base.skillLocator.FindSkillByFamilyName("ToolbotBodyPrimary2");
				this.specialSlot = base.skillLocator.FindSkillByFamilyName("ToolbotBodySpecialFamily");
				if (!this.allowPrimarySkills)
				{
					if (ToolbotDualWieldBase.inertSkillDef != null)
					{
						if (base.skillLocator.primary)
						{
							base.skillLocator.primary.SetSkillOverride(this, ToolbotDualWieldBase.inertSkillDef, GenericSkill.SkillOverridePriority.Contextual);
						}
						if (base.skillLocator.secondary)
						{
							base.skillLocator.secondary.SetSkillOverride(this, ToolbotDualWieldBase.inertSkillDef, GenericSkill.SkillOverridePriority.Contextual);
						}
					}
				}
				else if (base.skillLocator.secondary && this.primary2Slot)
				{
					base.skillLocator.secondary.SetSkillOverride(this, this.primary2Slot.skillDef, GenericSkill.SkillOverridePriority.Contextual);
					base.skillLocator.secondary.customStateMachineResolver += ToolbotDualWieldBase.offhandStateMachineResolverDelegate;
				}
				if (this.specialSlot && ToolbotDualWieldBase.cancelSkillDef != null)
				{
					this.specialSlot.SetSkillOverride(this, ToolbotDualWieldBase.cancelSkillDef, GenericSkill.SkillOverridePriority.Contextual);
				}
			}
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			if (base.modelAnimator)
			{
				base.modelAnimator.SetBool("isDualWielding", true);
			}
			if (base.cameraTargetParams && this.applyCameraAimMode)
			{
				this.cameraParamsOverrideHandle = base.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
				{
					cameraParamsData = ToolbotDualWieldBase.cameraParams.data,
					priority = 1f
				}, 0.2f);
			}
			this.StopSkills();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(1f);
				if (base.isAuthority && base.characterBody.isSprinting)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00020838 File Offset: 0x0001EA38
		public override void OnExit()
		{
			if (this.specialSlot && ToolbotDualWieldBase.cancelSkillDef != null)
			{
				this.specialSlot.UnsetSkillOverride(this, ToolbotDualWieldBase.cancelSkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			if (!this.allowPrimarySkills)
			{
				if (ToolbotDualWieldBase.inertSkillDef != null)
				{
					if (base.skillLocator.secondary)
					{
						base.skillLocator.secondary.UnsetSkillOverride(this, ToolbotDualWieldBase.inertSkillDef, GenericSkill.SkillOverridePriority.Contextual);
					}
					if (base.skillLocator.primary)
					{
						base.skillLocator.primary.UnsetSkillOverride(this, ToolbotDualWieldBase.inertSkillDef, GenericSkill.SkillOverridePriority.Contextual);
					}
				}
			}
			else if (base.skillLocator.secondary && this.primary2Slot)
			{
				base.skillLocator.secondary.UnsetSkillOverride(this, this.primary2Slot.skillDef, GenericSkill.SkillOverridePriority.Contextual);
				base.skillLocator.secondary.customStateMachineResolver -= ToolbotDualWieldBase.offhandStateMachineResolverDelegate;
			}
			if (NetworkServer.active && base.characterBody)
			{
				if (ToolbotDualWieldBase.bonusBuff && this.applyBonusBuff)
				{
					base.characterBody.RemoveBuff(ToolbotDualWieldBase.bonusBuff);
				}
				if (ToolbotDualWieldBase.penaltyBuff && this.applyPenaltyBuff)
				{
					base.characterBody.RemoveBuff(ToolbotDualWieldBase.penaltyBuff);
				}
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			if (base.modelAnimator)
			{
				base.modelAnimator.SetBool("isDualWielding", false);
			}
			this.PlayAnimation("DualWield, Additive", "Empty");
			if (base.cameraTargetParams && this.cameraParamsOverrideHandle.isValid)
			{
				this.cameraParamsOverrideHandle = base.cameraTargetParams.RemoveParamsOverride(this.cameraParamsOverrideHandle, 0.2f);
			}
			base.OnExit();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000C08B File Offset: 0x0000A28B
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			this.Serialize(base.skillLocator, writer);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.Deserialize(base.skillLocator, reader);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000209F9 File Offset: 0x0001EBF9
		private static void OffhandStateMachineResolver(GenericSkill genericSkill, SkillDef skillDef, ref EntityStateMachine targetStateMachine)
		{
			if (string.Equals(skillDef.activationStateMachineName, "Weapon", StringComparison.Ordinal))
			{
				targetStateMachine = EntityStateMachine.FindByCustomName(genericSkill.gameObject, "Weapon2");
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020A20 File Offset: 0x0001EC20
		protected void StopSkills()
		{
			if (base.isAuthority)
			{
				EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Weapon");
				if (entityStateMachine != null)
				{
					entityStateMachine.SetNextStateToMain();
				}
				EntityStateMachine entityStateMachine2 = EntityStateMachine.FindByCustomName(base.gameObject, "Weapon2");
				if (entityStateMachine2 == null)
				{
					return;
				}
				entityStateMachine2.SetNextStateToMain();
			}
		}

		// Token: 0x04000918 RID: 2328
		public static BuffDef penaltyBuff;

		// Token: 0x04000919 RID: 2329
		public static BuffDef bonusBuff;

		// Token: 0x0400091A RID: 2330
		public static SkillDef inertSkillDef;

		// Token: 0x0400091B RID: 2331
		public static SkillDef cancelSkillDef;

		// Token: 0x0400091C RID: 2332
		public static CharacterCameraParams cameraParams;

		// Token: 0x0400091D RID: 2333
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x0400091E RID: 2334
		[SerializeField]
		public bool applyPenaltyBuff = true;

		// Token: 0x0400091F RID: 2335
		[SerializeField]
		public bool applyBonusBuff = true;

		// Token: 0x04000920 RID: 2336
		[SerializeField]
		public bool applyCameraAimMode = true;

		// Token: 0x04000924 RID: 2340
		private GenericSkill specialSlot;

		// Token: 0x04000925 RID: 2341
		private bool allowPrimarySkills;

		// Token: 0x04000926 RID: 2342
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000927 RID: 2343
		private CameraTargetParams.CameraParamsOverrideHandle cameraParamsOverrideHandle;

		// Token: 0x04000928 RID: 2344
		private static GenericSkill.StateMachineResolver offhandStateMachineResolverDelegate = new GenericSkill.StateMachineResolver(ToolbotDualWieldBase.OffhandStateMachineResolver);
	}
}
