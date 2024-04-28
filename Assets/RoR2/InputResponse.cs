using System;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x02000771 RID: 1905
	public class InputResponse : MonoBehaviour
	{
		// Token: 0x06002796 RID: 10134 RVA: 0x000ABF44 File Offset: 0x000AA144
		private void Update()
		{
			Player player = MPEventSystemManager.combinedEventSystem.player;
			int i = 0;
			while (i < this.inputActionNames.Length)
			{
				if (player.GetButtonDown(this.inputActionNames[i]))
				{
					UnityEvent unityEvent = this.onPress;
					if (unityEvent == null)
					{
						return;
					}
					unityEvent.Invoke();
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x04002B88 RID: 11144
		public string[] inputActionNames;

		// Token: 0x04002B89 RID: 11145
		public UnityEvent onPress;
	}
}
