using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020008ED RID: 2285
	public class VfxKillBehavior : MonoBehaviour
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x000D8688 File Offset: 0x000D6888
		public static void KillVfxObject(GameObject gameObject)
		{
			if (!gameObject)
			{
				return;
			}
			List<VfxKillBehavior> list = CollectionPool<VfxKillBehavior, List<VfxKillBehavior>>.RentCollection();
			gameObject.GetComponents<VfxKillBehavior>(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						list[i].killBehavior.Invoke();
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
				return;
			}
			UnityEngine.Object.Destroy(gameObject);
		}

		// Token: 0x04003464 RID: 13412
		public UnityEvent killBehavior;
	}
}
