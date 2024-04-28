using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidJailer
{
	// Token: 0x02000151 RID: 337
	public class SpawnState : GenericCharacterSpawnState
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001914D File Offset: 0x0001734D
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlaySpawnFX();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001915B File Offset: 0x0001735B
		private void PlaySpawnFX()
		{
			if (SpawnState.spawnFXPrefab != null && !string.IsNullOrEmpty(SpawnState.spawnFXTransformName))
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnFXPrefab, base.gameObject, SpawnState.spawnFXTransformName, false);
			}
		}

		// Token: 0x04000701 RID: 1793
		public static GameObject spawnFXPrefab;

		// Token: 0x04000702 RID: 1794
		public static string spawnFXTransformName;
	}
}
