using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D79 RID: 3449
	public class RunTimerUIController : MonoBehaviour
	{
		// Token: 0x06004F13 RID: 20243 RVA: 0x001473CC File Offset: 0x001455CC
		private void Update()
		{
			if (this.runStopwatchTimerTextController)
			{
				this.runStopwatchTimerTextController.seconds = (double)(Run.instance ? Run.instance.GetRunStopwatch() : 0f);
			}
		}

		// Token: 0x04004BCA RID: 19402
		public TimerText runStopwatchTimerTextController;
	}
}
