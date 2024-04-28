using System;
using RoR2;
using UnityEngine;

namespace EntityStates.RoboBallBoss
{
	// Token: 0x020001E2 RID: 482
	public class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x000245D2 File Offset: 0x000227D2
		public override void OnEnter()
		{
			base.OnEnter();
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SpawnEffect(SpawnState.spawnEffectPrefab, new EffectData
				{
					origin = base.characterBody.corePosition,
					scale = SpawnState.spawnEffectRadius
				}, false);
			}
		}

		// Token: 0x04000A1E RID: 2590
		public static GameObject spawnEffectPrefab;

		// Token: 0x04000A1F RID: 2591
		public static float spawnEffectRadius;
	}
}
