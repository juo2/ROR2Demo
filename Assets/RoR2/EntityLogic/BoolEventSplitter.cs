using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C80 RID: 3200
	public class BoolEventSplitter : MonoBehaviour
	{
		// Token: 0x06004944 RID: 18756 RVA: 0x0012DD8E File Offset: 0x0012BF8E
		public void CallEvent(bool value)
		{
			UnityEvent unityEvent = value ? this.trueEvent : this.falseEvent;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x04004615 RID: 17941
		public UnityEvent trueEvent;

		// Token: 0x04004616 RID: 17942
		public UnityEvent falseEvent;
	}
}
