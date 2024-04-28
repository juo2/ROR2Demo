using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000CA RID: 202
	public class HurtStateFlyer : BaseState
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000F654 File Offset: 0x0000D854
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.sfxLocator && base.sfxLocator.deathSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.CrossFadeInFixedTime((UnityEngine.Random.Range(0, 2) == 0) ? "Hurt1" : "Hurt2", 0.1f);
				modelAnimator.Update(0f);
				this.duration = modelAnimator.GetNextAnimatorStateInfo(layerIndex).length;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000F6FD File Offset: 0x0000D8FD
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x040003C5 RID: 965
		private float stopwatch;

		// Token: 0x040003C6 RID: 966
		private float duration = 0.35f;
	}
}
