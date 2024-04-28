using System;
using RoR2;
using UnityEngine;

namespace EntityStates.HermitCrab
{
	// Token: 0x02000327 RID: 807
	public class BurrowOut : BaseState
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x0003E950 File Offset: 0x0003CB50
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = BurrowOut.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "BurrowOut", "BurrowOut.playbackRate", this.duration, 0.1f);
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			Util.PlaySound(BurrowOut.burrowOutSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(BurrowOut.burrowPrefab, base.gameObject, "BurrowCenter", false);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003E9D9 File Offset: 0x0003CBD9
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

		// Token: 0x06000E7D RID: 3709 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400122E RID: 4654
		public static GameObject burrowPrefab;

		// Token: 0x0400122F RID: 4655
		public static float baseDuration;

		// Token: 0x04001230 RID: 4656
		public static string burrowOutSoundString;

		// Token: 0x04001231 RID: 4657
		private float stopwatch;

		// Token: 0x04001232 RID: 4658
		private Transform modelTransform;

		// Token: 0x04001233 RID: 4659
		private ChildLocator childLocator;

		// Token: 0x04001234 RID: 4660
		private float duration;
	}
}
