using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047D RID: 1149
	public abstract class BasePrepSidearmRevolverState : BaseSidearmState
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x0005B7F8 File Offset: 0x000599F8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Gesture, Additive", "MainToSide", "MainToSide.playbackRate", this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005B82D File Offset: 0x00059A2D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(this.GetNextState());
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0005B854 File Offset: 0x00059A54
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001490 RID: 5264
		protected abstract EntityState GetNextState();

		// Token: 0x04001A54 RID: 6740
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04001A55 RID: 6741
		private Animator animator;
	}
}
