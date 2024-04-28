using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008AA RID: 2218
	public class StartEvent : MonoBehaviour
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x000D0FA0 File Offset: 0x000CF1A0
		private void Start()
		{
			if (!this.runOnServerOnly || NetworkServer.active)
			{
				this.action.Invoke();
			}
		}

		// Token: 0x040032C0 RID: 12992
		public bool runOnServerOnly;

		// Token: 0x040032C1 RID: 12993
		public UnityEvent action;
	}
}
