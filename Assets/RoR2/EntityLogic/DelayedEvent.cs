using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C82 RID: 3202
	public class DelayedEvent : MonoBehaviour
	{
		// Token: 0x06004949 RID: 18761 RVA: 0x0012DDE0 File Offset: 0x0012BFE0
		public void CallDelayed(float timer)
		{
			TimerQueue timerQueue = null;
			switch (this.timeStepType)
			{
			case DelayedEvent.TimeStepType.Time:
				timerQueue = RoR2Application.timeTimers;
				break;
			case DelayedEvent.TimeStepType.UnscaledTime:
				timerQueue = RoR2Application.unscaledTimeTimers;
				break;
			case DelayedEvent.TimeStepType.FixedTime:
				timerQueue = RoR2Application.fixedTimeTimers;
				break;
			}
			if (timerQueue != null)
			{
				timerQueue.CreateTimer(timer, new Action(this.Call));
			}
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0012DE37 File Offset: 0x0012C037
		public void CallDelayedIfActiveAndEnabled(float timer)
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.CallDelayed(timer);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0012DE49 File Offset: 0x0012C049
		private void Call()
		{
			if (this && base.enabled)
			{
				this.action.Invoke();
			}
		}

		// Token: 0x0400461A RID: 17946
		public UnityEvent action;

		// Token: 0x0400461B RID: 17947
		public DelayedEvent.TimeStepType timeStepType;

		// Token: 0x02000C83 RID: 3203
		public enum TimeStepType
		{
			// Token: 0x0400461D RID: 17949
			Time,
			// Token: 0x0400461E RID: 17950
			UnscaledTime,
			// Token: 0x0400461F RID: 17951
			FixedTime
		}
	}
}
