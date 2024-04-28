using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007EA RID: 2026
	public class OnPlayerEnterEvent : MonoBehaviour
	{
		// Token: 0x06002BBA RID: 11194 RVA: 0x000BB354 File Offset: 0x000B9554
		private void OnTriggerEnter(Collider other)
		{
			if ((this.serverOnly && !NetworkServer.active) || this.calledAction)
			{
				return;
			}
			CharacterBody component = other.GetComponent<CharacterBody>();
			if (component && component.isPlayerControlled)
			{
				Debug.LogFormat("OnPlayerEnterEvent called on {0}", new object[]
				{
					base.gameObject.name
				});
				this.calledAction = true;
				UnityEvent unityEvent = this.action;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x04002E25 RID: 11813
		public bool serverOnly;

		// Token: 0x04002E26 RID: 11814
		public UnityEvent action;

		// Token: 0x04002E27 RID: 11815
		private bool calledAction;
	}
}
