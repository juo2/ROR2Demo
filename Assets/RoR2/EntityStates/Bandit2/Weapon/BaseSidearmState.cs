using System;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047C RID: 1148
	public class BaseSidearmState : BaseState
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005B6AE File Offset: 0x000598AE
		public virtual string exitAnimationStateName
		{
			get
			{
				return "BufferEmpty";
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0005B6B8 File Offset: 0x000598B8
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.duration = this.baseDuration / this.attackSpeedStat;
			if (this.animator)
			{
				this.bodySideWeaponLayerIndex = this.animator.GetLayerIndex("Body, SideWeapon");
				this.animator.SetLayerWeight(this.bodySideWeaponLayerIndex, 1f);
			}
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			base.characterBody.SetAimTimer(3f);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0005B758 File Offset: 0x00059958
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.characterBody.isSprinting)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0005B780 File Offset: 0x00059980
		public override void OnExit()
		{
			if (this.animator)
			{
				this.animator.SetLayerWeight(this.bodySideWeaponLayerIndex, 0f);
			}
			this.PlayAnimation("Gesture, Additive", this.exitAnimationStateName);
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			Transform transform = base.FindModelChild("SpinningPistolFX");
			if (transform)
			{
				transform.gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A4E RID: 6734
		[SerializeField]
		public float baseDuration;

		// Token: 0x04001A4F RID: 6735
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04001A50 RID: 6736
		protected float duration;

		// Token: 0x04001A51 RID: 6737
		private Animator animator;

		// Token: 0x04001A52 RID: 6738
		private int bodySideWeaponLayerIndex;

		// Token: 0x04001A53 RID: 6739
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
