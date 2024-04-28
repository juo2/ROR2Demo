using System;
using UnityEngine;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000C9E RID: 3230
	public struct PickupSearchSelector : IGenericWorldSearchSelector<GenericPickupController>
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x0012F39A File Offset: 0x0012D59A
		public Transform GetTransform(GenericPickupController source)
		{
			return source.pickupDisplay.transform;
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x000DB4C7 File Offset: 0x000D96C7
		public GameObject GetRootObject(GenericPickupController source)
		{
			return source.gameObject;
		}
	}
}
