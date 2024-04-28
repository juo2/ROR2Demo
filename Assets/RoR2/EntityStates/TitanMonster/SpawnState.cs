using System;
using RoR2;
using UnityEngine;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000368 RID: 872
	public class SpawnState : BaseState
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x00045440 File Offset: 0x00043640
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.GetModelTransform().GetComponent<ChildLocator>();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			EffectManager.SimpleMuzzleFlash(SpawnState.burrowPrefab, base.gameObject, "BurrowCenter", false);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000454A0 File Offset: 0x000436A0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001404 RID: 5124
		public static float duration = 4f;

		// Token: 0x04001405 RID: 5125
		public static GameObject burrowPrefab;

		// Token: 0x04001406 RID: 5126
		public static string spawnSoundString;
	}
}
