using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C81 RID: 3201
	public class Counter : MonoBehaviour
	{
		// Token: 0x06004946 RID: 18758 RVA: 0x0012DDAB File Offset: 0x0012BFAB
		public void Add(int valueToAdd)
		{
			this.value += valueToAdd;
			if (this.value >= this.threshold)
			{
				this.onTrigger.Invoke();
			}
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x0012DDD4 File Offset: 0x0012BFD4
		public void SetValue(int newValue)
		{
			this.value = newValue;
		}

		// Token: 0x04004617 RID: 17943
		public int value;

		// Token: 0x04004618 RID: 17944
		public int threshold;

		// Token: 0x04004619 RID: 17945
		public UnityEvent onTrigger;
	}
}
