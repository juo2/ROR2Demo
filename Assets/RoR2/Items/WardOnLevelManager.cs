using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BEF RID: 3055
	public static class WardOnLevelManager
	{
		// Token: 0x0600454B RID: 17739 RVA: 0x00120743 File Offset: 0x0011E943
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			GlobalEventManager.onCharacterLevelUp += WardOnLevelManager.OnCharacterLevelUp;
			WardOnLevelManager.wardPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard");
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00120768 File Offset: 0x0011E968
		private static void OnCharacterLevelUp(CharacterBody characterBody)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			Inventory inventory = characterBody.inventory;
			if (inventory)
			{
				int itemCount = inventory.GetItemCount(RoR2Content.Items.WardOnLevel);
				if (itemCount > 0)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(WardOnLevelManager.wardPrefab, characterBody.transform.position, Quaternion.identity);
					gameObject.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;
					gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * (float)itemCount;
					NetworkServer.Spawn(gameObject);
				}
			}
		}

		// Token: 0x04004395 RID: 17301
		private static GameObject wardPrefab;
	}
}
