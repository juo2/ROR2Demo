using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C84 RID: 3204
	public class Repeat : MonoBehaviour
	{
		// Token: 0x0600494D RID: 18765 RVA: 0x0012DE66 File Offset: 0x0012C066
		public void CallRepeat(int repeatNumber)
		{
			while (repeatNumber > 0)
			{
				repeatNumber--;
				UnityEvent unityEvent = this.repeatedEvent;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
			}
		}

		// Token: 0x04004620 RID: 17952
		public UnityEvent repeatedEvent;
	}
}
