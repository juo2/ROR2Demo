using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200098F RID: 2447
	[Serializable]
	public struct SerializablePickupIndex
	{
		// Token: 0x060037AC RID: 14252 RVA: 0x000EA3CE File Offset: 0x000E85CE
		public static explicit operator PickupIndex(SerializablePickupIndex serializablePickupIndex)
		{
			return PickupCatalog.FindPickupIndex(serializablePickupIndex.pickupName);
		}

		// Token: 0x040037DE RID: 14302
		[SerializeField]
		public string pickupName;
	}
}
