using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020005E9 RID: 1513
	public class AwakeEvent : MonoBehaviour
	{
		// Token: 0x06001B78 RID: 7032 RVA: 0x000756C0 File Offset: 0x000738C0
		private void Awake()
		{
			this.action.Invoke();
		}

		// Token: 0x0400216F RID: 8559
		public UnityEvent action;
	}
}
