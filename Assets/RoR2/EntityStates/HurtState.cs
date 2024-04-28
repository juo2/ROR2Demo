using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C9 RID: 201
	public class HurtState : BaseState
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000F544 File Offset: 0x0000D744
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
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
			if (base.characterBody)
			{
				base.characterBody.isSprinting = false;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000F606 File Offset: 0x0000D806
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040003C3 RID: 963
		private float stopwatch;

		// Token: 0x040003C4 RID: 964
		private float duration = 0.35f;
	}
}
