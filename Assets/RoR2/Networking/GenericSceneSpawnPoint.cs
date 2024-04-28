using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C35 RID: 3125
	public class GenericSceneSpawnPoint : MonoBehaviour
	{
		// Token: 0x060046AC RID: 18092 RVA: 0x001247D0 File Offset: 0x001229D0
		private void Start()
		{
			if (NetworkServer.active)
			{
				NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(this.networkedObjectPrefab, base.transform.position, base.transform.rotation));
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x04004487 RID: 17543
		public GameObject networkedObjectPrefab;
	}
}
