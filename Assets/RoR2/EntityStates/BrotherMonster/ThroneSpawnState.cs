using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000441 RID: 1089
	public class ThroneSpawnState : BaseState
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x00056D4A File Offset: 0x00054F4A
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Throne");
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			EffectManager.SimpleMuzzleFlash(ThroneSpawnState.spawnEffectPrefab, base.gameObject, ThroneSpawnState.muzzleString, false);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00056D88 File Offset: 0x00054F88
		private void PlayAnimation()
		{
			this.hasPlayedAnimation = true;
			base.PlayAnimation("Body", "ThroneToIdle", "ThroneToIdle.playbackRate", ThroneSpawnState.duration);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00056DAC File Offset: 0x00054FAC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > ThroneSpawnState.initialDelay && !this.hasPlayedAnimation)
			{
				this.PlayAnimation();
			}
			if (base.fixedAge > ThroneSpawnState.initialDelay + ThroneSpawnState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040018F3 RID: 6387
		public static GameObject spawnEffectPrefab;

		// Token: 0x040018F4 RID: 6388
		public static string muzzleString;

		// Token: 0x040018F5 RID: 6389
		public static float initialDelay;

		// Token: 0x040018F6 RID: 6390
		public static float duration;

		// Token: 0x040018F7 RID: 6391
		private bool hasPlayedAnimation;
	}
}
