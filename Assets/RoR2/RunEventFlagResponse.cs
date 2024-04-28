using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000866 RID: 2150
	public class RunEventFlagResponse : MonoBehaviour
	{
		// Token: 0x06002F17 RID: 12055 RVA: 0x000C8DF8 File Offset: 0x000C6FF8
		private void Awake()
		{
			if (NetworkServer.active)
			{
				if (Run.instance)
				{
					UnityEvent unityEvent = Run.instance.GetEventFlag(this.flagName) ? this.onAwakeWithFlagSetServer : this.onAwakeWithFlagUnsetServer;
					if (unityEvent == null)
					{
						return;
					}
					unityEvent.Invoke();
					return;
				}
				else
				{
					Debug.LogErrorFormat("Cannot handle run event flag response {0}: No run exists.", new object[]
					{
						base.gameObject.name
					});
				}
			}
		}

		// Token: 0x040030FF RID: 12543
		public string flagName;

		// Token: 0x04003100 RID: 12544
		public UnityEvent onAwakeWithFlagSetServer;

		// Token: 0x04003101 RID: 12545
		public UnityEvent onAwakeWithFlagUnsetServer;
	}
}
