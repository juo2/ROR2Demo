using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007A7 RID: 1959
	[RequireComponent(typeof(CharacterMaster))]
	public class MasterDropDroplet : MonoBehaviour
	{
		// Token: 0x06002963 RID: 10595 RVA: 0x000B305D File Offset: 0x000B125D
		private void Start()
		{
			this.characterMaster = base.GetComponent<CharacterMaster>();
			this.rng = new Xoroshiro128Plus(Run.instance.seed ^ this.salt);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000B3088 File Offset: 0x000B1288
		public void DropItems()
		{
			CharacterBody body = this.characterMaster.GetBody();
			if (body)
			{
				foreach (PickupDropTable pickupDropTable in this.dropTables)
				{
					if (pickupDropTable)
					{
						PickupDropletController.CreatePickupDroplet(pickupDropTable.GenerateDrop(this.rng), body.coreTransform.position, new Vector3(UnityEngine.Random.Range(-4f, 4f), 20f, UnityEngine.Random.Range(-4f, 4f)));
					}
				}
				SerializablePickupIndex[] array2 = this.pickupsToDrop;
				for (int i = 0; i < array2.Length; i++)
				{
					PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(array2[i].pickupName), body.coreTransform.position, new Vector3(UnityEngine.Random.Range(-4f, 4f), 20f, UnityEngine.Random.Range(-4f, 4f)));
				}
			}
		}

		// Token: 0x04002CC5 RID: 11461
		private CharacterMaster characterMaster;

		// Token: 0x04002CC6 RID: 11462
		public PickupDropTable[] dropTables;

		// Token: 0x04002CC7 RID: 11463
		public ulong salt;

		// Token: 0x04002CC8 RID: 11464
		[Header("Deprecated")]
		public SerializablePickupIndex[] pickupsToDrop;

		// Token: 0x04002CC9 RID: 11465
		private Xoroshiro128Plus rng;
	}
}
