using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005A9 RID: 1449
	public class VoidRaidEncounterController : NetworkBehaviour
	{
		// Token: 0x06001A27 RID: 6695 RVA: 0x00070D9A File Offset: 0x0006EF9A
		private void Start()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VoidRaidBossPrefab, this.VoidRaidBossSpawnPosition.position, Quaternion.identity);
			gameObject.GetComponent<CharacterMaster>().teamIndex = TeamIndex.Monster;
			NetworkServer.Spawn(gameObject);
			gameObject.GetComponent<CharacterMaster>().SpawnBodyHere();
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00070DDC File Offset: 0x0006EFDC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002060 RID: 8288
		public GameObject VoidRaidBossPrefab;

		// Token: 0x04002061 RID: 8289
		public Transform VoidRaidBossSpawnPosition;
	}
}
