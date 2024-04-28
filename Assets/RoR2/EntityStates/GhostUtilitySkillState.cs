using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000C7 RID: 199
	public class GhostUtilitySkillState : GenericCharacterMain
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = GhostUtilitySkillState.baseDuration;
			this.characterGravityParameterProvider = base.gameObject.GetComponent<ICharacterGravityParameterProvider>();
			this.characterFlightParameterProvider = base.gameObject.GetComponent<ICharacterFlightParameterProvider>();
			if (base.characterBody)
			{
				if (base.characterBody.inventory)
				{
					this.duration *= (float)base.characterBody.inventory.GetItemCount(RoR2Content.Items.LunarUtilityReplacement);
				}
				this.hurtBoxGroup = base.characterBody.hurtBoxGroup;
				if (this.hurtBoxGroup)
				{
					HurtBoxGroup hurtBoxGroup = this.hurtBoxGroup;
					int i = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
					hurtBoxGroup.hurtBoxesDeactivatorCounter = i;
				}
				if (GhostUtilitySkillState.coreVfxPrefab)
				{
					this.coreVfxInstance = UnityEngine.Object.Instantiate<GameObject>(GhostUtilitySkillState.coreVfxPrefab);
				}
				if (GhostUtilitySkillState.footVfxPrefab)
				{
					this.footVfxInstance = UnityEngine.Object.Instantiate<GameObject>(GhostUtilitySkillState.footVfxPrefab);
				}
				this.UpdateVfxPositions();
				if (GhostUtilitySkillState.entryEffectPrefab)
				{
					Ray aimRay = base.GetAimRay();
					EffectManager.SimpleEffect(GhostUtilitySkillState.entryEffectPrefab, aimRay.origin, Quaternion.LookRotation(aimRay.direction), false);
				}
			}
			Transform modelTransform = base.GetModelTransform();
			this.characterModel = ((modelTransform != null) ? modelTransform.GetComponent<CharacterModel>() : null);
			if (base.modelAnimator)
			{
				base.modelAnimator.enabled = false;
			}
			if (base.characterMotor)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = GhostUtilitySkillState.moveSpeedCoefficient;
			}
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount++;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount++;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount++;
			}
			foreach (EntityStateMachine entityStateMachine in base.gameObject.GetComponents<EntityStateMachine>())
			{
				if (entityStateMachine.customName == "Weapon")
				{
					entityStateMachine.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		private void UpdateVfxPositions()
		{
			if (base.characterBody)
			{
				if (this.coreVfxInstance)
				{
					this.coreVfxInstance.transform.position = base.characterBody.corePosition;
				}
				if (this.footVfxInstance)
				{
					this.footVfxInstance.transform.position = base.characterBody.footPosition;
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool CanExecuteSkill(GenericSkill skillSlot)
		{
			return false;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000F146 File Offset: 0x0000D346
		public override void Update()
		{
			base.Update();
			this.UpdateVfxPositions();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000F154 File Offset: 0x0000D354
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.healTimer -= Time.fixedDeltaTime;
			if (this.healTimer <= 0f)
			{
				if (NetworkServer.active)
				{
					base.healthComponent.HealFraction(GhostUtilitySkillState.healFractionPerTick, default(ProcChainMask));
				}
				this.healTimer = 1f / GhostUtilitySkillState.healFrequency;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public override void OnExit()
		{
			if (GhostUtilitySkillState.exitEffectPrefab && !this.outer.destroying)
			{
				Ray aimRay = base.GetAimRay();
				EffectManager.SimpleEffect(GhostUtilitySkillState.exitEffectPrefab, aimRay.origin, Quaternion.LookRotation(aimRay.direction), false);
			}
			if (this.coreVfxInstance)
			{
				EntityState.Destroy(this.coreVfxInstance);
			}
			if (this.footVfxInstance)
			{
				EntityState.Destroy(this.footVfxInstance);
			}
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount--;
			}
			if (this.hurtBoxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtBoxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			if (base.modelAnimator)
			{
				base.modelAnimator.enabled = true;
			}
			if (this.characterFlightParameterProvider != null)
			{
				CharacterFlightParameters flightParameters = this.characterFlightParameterProvider.flightParameters;
				flightParameters.channeledFlightGranterCount--;
				this.characterFlightParameterProvider.flightParameters = flightParameters;
			}
			if (this.characterGravityParameterProvider != null)
			{
				CharacterGravityParameters gravityParameters = this.characterGravityParameterProvider.gravityParameters;
				gravityParameters.channeledAntiGravityGranterCount--;
				this.characterGravityParameterProvider.gravityParameters = gravityParameters;
			}
			if (base.characterMotor)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			}
			base.OnExit();
		}

		// Token: 0x040003AB RID: 939
		public static float baseDuration;

		// Token: 0x040003AC RID: 940
		public static GameObject coreVfxPrefab;

		// Token: 0x040003AD RID: 941
		public static GameObject footVfxPrefab;

		// Token: 0x040003AE RID: 942
		public static GameObject entryEffectPrefab;

		// Token: 0x040003AF RID: 943
		public static GameObject exitEffectPrefab;

		// Token: 0x040003B0 RID: 944
		public static float moveSpeedCoefficient;

		// Token: 0x040003B1 RID: 945
		public static float healFractionPerTick;

		// Token: 0x040003B2 RID: 946
		public static float healFrequency;

		// Token: 0x040003B3 RID: 947
		private HurtBoxGroup hurtBoxGroup;

		// Token: 0x040003B4 RID: 948
		private CharacterModel characterModel;

		// Token: 0x040003B5 RID: 949
		private GameObject coreVfxInstance;

		// Token: 0x040003B6 RID: 950
		private GameObject footVfxInstance;

		// Token: 0x040003B7 RID: 951
		private float healTimer;

		// Token: 0x040003B8 RID: 952
		private float duration;

		// Token: 0x040003B9 RID: 953
		private ICharacterGravityParameterProvider characterGravityParameterProvider;

		// Token: 0x040003BA RID: 954
		private ICharacterFlightParameterProvider characterFlightParameterProvider;

		// Token: 0x040003BB RID: 955
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040003BC RID: 956
		[SerializeField]
		public string animationStateName;

		// Token: 0x040003BD RID: 957
		[SerializeField]
		public string playbackRateParam;
	}
}
