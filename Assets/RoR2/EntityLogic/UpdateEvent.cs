using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C88 RID: 3208
	public class UpdateEvent : MonoBehaviour
	{
		// Token: 0x06004965 RID: 18789 RVA: 0x0012E128 File Offset: 0x0012C328
		private void Update()
		{
			if (this.updateSkipCount < this.updateCount && (this.maxInvokeCount < 0 || this.invokeCount < this.maxInvokeCount))
			{
				UnityEvent unityEvent = this.action;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				this.invokeCount++;
			}
			this.updateCount++;
		}

		// Token: 0x04004631 RID: 17969
		[SerializeField]
		[Tooltip("Don't call the action until at least this many updates have fired")]
		private int updateSkipCount;

		// Token: 0x04004632 RID: 17970
		[Tooltip("Don't call the action after this many updates have fired.  If negative, ignore.")]
		[SerializeField]
		private int maxInvokeCount = -1;

		// Token: 0x04004633 RID: 17971
		[SerializeField]
		private UnityEvent action;

		// Token: 0x04004634 RID: 17972
		private int invokeCount;

		// Token: 0x04004635 RID: 17973
		private int updateCount;
	}
}
