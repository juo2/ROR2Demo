using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003E1 RID: 993
	public class WakeUp : BaseState
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0004E5D4 File Offset: 0x0004C7D4
		public override void OnEnter()
		{
			base.OnEnter();
			base.modelLocator.normalizeToFloor = true;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(AnimationParameters.aimWeight, 0f);
			}
			base.PlayAnimation("Body", "SleepToIdle", "SleepToIdle.playbackRate", WakeUp.duration);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0004E63B File Offset: 0x0004C83B
		public override void Update()
		{
			base.Update();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(AnimationParameters.aimWeight, Mathf.Clamp01((base.age - WakeUp.delayBeforeAimAnimatorWeight) / WakeUp.duration));
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004E677 File Offset: 0x0004C877
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= WakeUp.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004E697 File Offset: 0x0004C897
		public override void OnExit()
		{
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(AnimationParameters.aimWeight, 1f);
			}
			base.OnExit();
		}

		// Token: 0x04001690 RID: 5776
		public static float duration;

		// Token: 0x04001691 RID: 5777
		public static float delayBeforeAimAnimatorWeight;

		// Token: 0x04001692 RID: 5778
		private Animator modelAnimator;
	}
}
