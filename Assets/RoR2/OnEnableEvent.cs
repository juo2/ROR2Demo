using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020007E9 RID: 2025
	public class OnEnableEvent : MonoBehaviour
	{
		// Token: 0x06002BB8 RID: 11192 RVA: 0x000BB340 File Offset: 0x000B9540
		private void OnEnable()
		{
			UnityEvent unityEvent = this.action;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x04002E24 RID: 11812
		public UnityEvent action;
	}
}
