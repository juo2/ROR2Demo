using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020007E8 RID: 2024
	public class OnDisableEvent : MonoBehaviour
	{
		// Token: 0x06002BB6 RID: 11190 RVA: 0x000BB32E File Offset: 0x000B952E
		private void OnDisable()
		{
			UnityEvent unityEvent = this.action;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x04002E23 RID: 11811
		public UnityEvent action;
	}
}
