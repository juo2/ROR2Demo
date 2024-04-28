using System;
using RoR2;
using UnityEngine;

namespace EntityStates.HermitCrab
{
	// Token: 0x02000326 RID: 806
	public class BurrowIn : BaseState
	{
		// Token: 0x06000E75 RID: 3701 RVA: 0x0003E874 File Offset: 0x0003CA74
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = BurrowIn.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "BurrowIn", "BurrowIn.playbackRate", this.duration, 0.1f);
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			Util.PlaySound(BurrowIn.burrowInSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(BurrowIn.burrowPrefab, base.gameObject, "BurrowCenter", false);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003E900 File Offset: 0x0003CB00
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				Burrowed nextState = new Burrowed();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001227 RID: 4647
		public static GameObject burrowPrefab;

		// Token: 0x04001228 RID: 4648
		public static float baseDuration;

		// Token: 0x04001229 RID: 4649
		public static string burrowInSoundString;

		// Token: 0x0400122A RID: 4650
		private float stopwatch;

		// Token: 0x0400122B RID: 4651
		private float duration;

		// Token: 0x0400122C RID: 4652
		private Transform modelTransform;

		// Token: 0x0400122D RID: 4653
		private ChildLocator childLocator;
	}
}
