using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Gup
{
	// Token: 0x0200033A RID: 826
	internal class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x06000ED9 RID: 3801 RVA: 0x000402D4 File Offset: 0x0003E4D4
		public override void OnEnter()
		{
			base.OnEnter();
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SpawnEffect(SpawnState.spawnEffectPrefab, new EffectData
				{
					origin = base.FindModelChild(SpawnState.spawnEffectMuzzle).position,
					scale = base.characterBody.radius
				}, true);
			}
		}

		// Token: 0x0400129E RID: 4766
		public static GameObject spawnEffectPrefab;

		// Token: 0x0400129F RID: 4767
		public static string spawnEffectMuzzle;
	}
}
