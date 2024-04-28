using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000555 RID: 1365
	[CreateAssetMenu(menuName = "RoR2/SpawnCards/MultiCharacterSpawnCard")]
	public class MultiCharacterSpawnCard : CharacterSpawnCard
	{
		// Token: 0x060018C6 RID: 6342 RVA: 0x0006BCB1 File Offset: 0x00069EB1
		protected override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
		{
			this.prefab = this.masterPrefabs[(int)(directorSpawnRequest.rng.nextNormalizedFloat * (float)this.masterPrefabs.Length)];
			base.Spawn(position, rotation, directorSpawnRequest, ref result);
		}

		// Token: 0x04001E68 RID: 7784
		public GameObject[] masterPrefabs;
	}
}
