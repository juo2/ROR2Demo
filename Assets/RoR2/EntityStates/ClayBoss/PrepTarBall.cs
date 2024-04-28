using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBoss
{
	// Token: 0x02000407 RID: 1031
	public class PrepTarBall : BaseState
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x00052D5C File Offset: 0x00050F5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = PrepTarBall.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayCrossfade("Body", "PrepTarBall", "PrepTarBall.playbackRate", this.duration, 0.5f);
			}
			if (!string.IsNullOrEmpty(PrepTarBall.prepTarBallSoundString))
			{
				Util.PlayAttackSpeedSound(PrepTarBall.prepTarBallSoundString, base.gameObject, this.attackSpeedStat);
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00052DE8 File Offset: 0x00050FE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new FireTarball());
				return;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040017E8 RID: 6120
		public static float baseDuration = 3f;

		// Token: 0x040017E9 RID: 6121
		public static string prepTarBallSoundString;

		// Token: 0x040017EA RID: 6122
		private float duration;

		// Token: 0x040017EB RID: 6123
		private float stopwatch;

		// Token: 0x040017EC RID: 6124
		private Animator modelAnimator;
	}
}
