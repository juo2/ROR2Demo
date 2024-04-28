using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Pounder
{
	// Token: 0x02000172 RID: 370
	public class Spawn : BaseState
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0001BAAC File Offset: 0x00019CAC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Spawn.baseDuration / this.attackSpeedStat;
			Util.PlaySound(Spawn.spawnSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(Spawn.spawnPrefab, base.gameObject, "Feet", false);
			base.PlayAnimation("Base", "Spawn", "Spawn.playbackRate", this.duration);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001BB13 File Offset: 0x00019D13
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040007D8 RID: 2008
		public static GameObject spawnPrefab;

		// Token: 0x040007D9 RID: 2009
		public static float baseDuration;

		// Token: 0x040007DA RID: 2010
		public static string spawnSoundString;

		// Token: 0x040007DB RID: 2011
		private float duration;
	}
}
