using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarExploderMonster
{
	// Token: 0x020002BB RID: 699
	public class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x06000C67 RID: 3175 RVA: 0x000344AF File Offset: 0x000326AF
		public override void OnEnter()
		{
			base.OnEnter();
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, SpawnState.spawnEffectChildString, false);
			}
		}

		// Token: 0x04000F26 RID: 3878
		public static GameObject spawnEffectPrefab;

		// Token: 0x04000F27 RID: 3879
		public static string spawnEffectChildString;
	}
}
