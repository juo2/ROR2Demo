using System;
using EntityStates.Treebot.Weapon;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x02000178 RID: 376
	public class Burrowed : GenericCharacterMain
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "Burrowed", 0.1f);
			base.skillLocator.primary = base.skillLocator.FindSkill(Burrowed.altPrimarySkillName);
			base.skillLocator.utility = base.skillLocator.FindSkill(Burrowed.altUtilitySkillName);
			base.skillLocator.primary.stateMachine.mainStateType = new SerializableEntityStateType(typeof(AimMortar));
			base.skillLocator.primary.stateMachine.SetNextStateToMain();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
			}
			if (this.childLocator)
			{
				base.characterBody.aimOriginTransform = this.childLocator.FindChild("AimOriginMortar");
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		public override void OnExit()
		{
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			base.skillLocator.primary = base.skillLocator.FindSkill(Burrowed.primarySkillName);
			base.skillLocator.utility = base.skillLocator.FindSkill(Burrowed.utilitySkillName);
			base.skillLocator.primary.stateMachine.mainStateType = new SerializableEntityStateType(typeof(Idle));
			base.skillLocator.primary.stateMachine.SetNextStateToMain();
			if (this.childLocator)
			{
				base.characterBody.aimOriginTransform = this.childLocator.FindChild("AimOriginSyringe");
			}
			base.OnExit();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400080D RID: 2061
		public static float mortarCooldown;

		// Token: 0x0400080E RID: 2062
		public static string primarySkillName;

		// Token: 0x0400080F RID: 2063
		public static string altPrimarySkillName;

		// Token: 0x04000810 RID: 2064
		public static string utilitySkillName;

		// Token: 0x04000811 RID: 2065
		public static string altUtilitySkillName;

		// Token: 0x04000812 RID: 2066
		public float duration;

		// Token: 0x04000813 RID: 2067
		private ChildLocator childLocator;

		// Token: 0x04000814 RID: 2068
		private CameraTargetParams.AimRequest aimRequest;
	}
}
