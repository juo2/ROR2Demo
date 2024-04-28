using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C41 RID: 3137
	public class NetworkContextActivationGuard : MonoBehaviour
	{
		// Token: 0x060046FF RID: 18175 RVA: 0x00125770 File Offset: 0x00123970
		private void Awake()
		{
			bool flag = true;
			flag &= this.CheckRule(this.server, NetworkServer.active);
			flag &= this.CheckRule(this.client, NetworkClient.active);
			base.gameObject.SetActive(flag);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x001257B3 File Offset: 0x001239B3
		private bool CheckRule(NetworkContextActivationGuard.Rule rule, bool value)
		{
			switch (rule)
			{
			case NetworkContextActivationGuard.Rule.Neutral:
				return true;
			case NetworkContextActivationGuard.Rule.MustBeTrue:
				return value;
			case NetworkContextActivationGuard.Rule.MustBeFalse:
				return !value;
			default:
				return false;
			}
		}

		// Token: 0x040044B0 RID: 17584
		public NetworkContextActivationGuard.Rule server;

		// Token: 0x040044B1 RID: 17585
		public NetworkContextActivationGuard.Rule client;

		// Token: 0x02000C42 RID: 3138
		public enum Rule
		{
			// Token: 0x040044B3 RID: 17587
			Neutral,
			// Token: 0x040044B4 RID: 17588
			MustBeTrue,
			// Token: 0x040044B5 RID: 17589
			MustBeFalse
		}
	}
}
