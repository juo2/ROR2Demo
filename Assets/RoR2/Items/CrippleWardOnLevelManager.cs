using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BD6 RID: 3030
	public static class CrippleWardOnLevelManager
	{
		// Token: 0x060044D4 RID: 17620 RVA: 0x0011EA92 File Offset: 0x0011CC92
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			Run.onRunAmbientLevelUp += CrippleWardOnLevelManager.onRunAmbientLevelUp;
			CrippleWardOnLevelManager.wardPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/CrippleWard");
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x0011EAB4 File Offset: 0x0011CCB4
		private static void onRunAmbientLevelUp(Run run)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
			{
				int itemCount = characterMaster.inventory.GetItemCount(RoR2Content.Items.CrippleWardOnLevel);
				if (itemCount > 0)
				{
					CharacterBody body = characterMaster.GetBody();
					if (body)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(CrippleWardOnLevelManager.wardPrefab, body.transform.position, Quaternion.identity);
						gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * (float)itemCount;
						NetworkServer.Spawn(gameObject);
					}
				}
			}
		}

		// Token: 0x0400434E RID: 17230
		private static GameObject wardPrefab;
	}
}
