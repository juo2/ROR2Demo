using System;
using UnityEngine;

namespace EntityStates.HAND.Weapon
{
	// Token: 0x02000333 RID: 819
	public class ChargeSlam : BaseState
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003F958 File Offset: 0x0003DB58
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ChargeSlam.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "ChargeSlam", "ChargeSlam.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(4f);
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003F9CE File Offset: 0x0003DBCE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.characterMotor.isGrounded && base.isAuthority)
			{
				this.outer.SetNextState(new Slam());
				return;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001270 RID: 4720
		public static float baseDuration = 3.5f;

		// Token: 0x04001271 RID: 4721
		private float duration;

		// Token: 0x04001272 RID: 4722
		private Animator modelAnimator;
	}
}
