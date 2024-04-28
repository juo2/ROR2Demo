using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000525 RID: 1317
	[CreateAssetMenu(menuName = "RoR2/SpawnCards/BodySpawnCard")]
	public class BodySpawnCard : SpawnCard
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x000692E0 File Offset: 0x000674E0
		protected override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
		{
			Vector3 position2 = position;
			position2.y += Util.GetBodyPrefabFootOffset(this.prefab);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position2, rotation);
			NetworkServer.Spawn(gameObject);
			result.spawnedInstance = gameObject;
			result.success = true;
		}
	}
}
