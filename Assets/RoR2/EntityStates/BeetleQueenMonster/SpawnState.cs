using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000467 RID: 1127
	public class SpawnState : BaseState
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x00059BA0 File Offset: 0x00057DA0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.GetModelTransform().GetComponent<ChildLocator>();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			string muzzleName = "BurrowCenter";
			EffectManager.SimpleMuzzleFlash(SpawnState.burrowPrefab, base.gameObject, muzzleName, false);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00059C02 File Offset: 0x00057E02
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040019D2 RID: 6610
		public static float duration = 4f;

		// Token: 0x040019D3 RID: 6611
		public static GameObject burrowPrefab;

		// Token: 0x040019D4 RID: 6612
		public static string spawnSoundString;
	}
}
