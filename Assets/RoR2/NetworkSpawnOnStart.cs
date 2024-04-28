using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007D3 RID: 2003
	public class NetworkSpawnOnStart : MonoBehaviour
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x000B7ADA File Offset: 0x000B5CDA
		private void Start()
		{
			if (NetworkServer.active)
			{
				NetworkServer.Spawn(base.gameObject);
			}
		}
	}
}
