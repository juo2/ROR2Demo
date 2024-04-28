using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C86 RID: 3206
	public class Timer : MonoBehaviour
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x0012DFB6 File Offset: 0x0012C1B6
		// (set) Token: 0x06004956 RID: 18774 RVA: 0x0012DFBE File Offset: 0x0012C1BE
		public float duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x0012DFC7 File Offset: 0x0012C1C7
		// (set) Token: 0x06004958 RID: 18776 RVA: 0x0012DFCF File Offset: 0x0012C1CF
		public Timer.TimeStepType timeStepType
		{
			get
			{
				return this._timeStepType;
			}
			set
			{
				this._timeStepType = value;
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0012DFD8 File Offset: 0x0012C1D8
		private void OnEnable()
		{
			if (this.resetTimerOnEnable)
			{
				this.RewindTimerToBeginning();
			}
			if (this.playTimerOnEnable)
			{
				this.PlayTimer();
			}
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x0012DFF6 File Offset: 0x0012C1F6
		private void Update()
		{
			if (this.timerRunning)
			{
				if (this.timeStepType == Timer.TimeStepType.Time)
				{
					this.RunTimer(Time.deltaTime);
					return;
				}
				if (this.timeStepType == Timer.TimeStepType.UnscaledTime)
				{
					this.RunTimer(Time.unscaledDeltaTime);
				}
			}
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0012E028 File Offset: 0x0012C228
		private void FixedUpdate()
		{
			if (this.timerRunning && this.timeStepType == Timer.TimeStepType.FixedTime)
			{
				this.RunTimer(Time.fixedDeltaTime);
			}
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0012E046 File Offset: 0x0012C246
		public void RewindTimerToBeginning()
		{
			this.stopwatch = 0f;
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0012E053 File Offset: 0x0012C253
		public void SkipTimerToEnd()
		{
			this.stopwatch = this.duration;
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x0012E061 File Offset: 0x0012C261
		public void SetTimerPlaying(bool newTimerRunning)
		{
			this.timerRunning = newTimerRunning;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0012E06A File Offset: 0x0012C26A
		public void PlayTimer()
		{
			this.SetTimerPlaying(true);
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0012E073 File Offset: 0x0012C273
		public void PauseTimer()
		{
			this.SetTimerPlaying(false);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0012E07C File Offset: 0x0012C27C
		public void CancelTimer()
		{
			this.PauseTimer();
			this.RewindTimerToBeginning();
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0012E08A File Offset: 0x0012C28A
		public void PlayTimerFromBeginning()
		{
			this.RewindTimerToBeginning();
			this.PlayTimer();
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x0012E098 File Offset: 0x0012C298
		private void RunTimer(float deltaTime)
		{
			this.stopwatch += deltaTime;
			if (this.stopwatch >= this.duration)
			{
				this.stopwatch = 0f;
				try
				{
					UnityEvent unityEvent = this.action;
					if (unityEvent != null)
					{
						unityEvent.Invoke();
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
				if (!this.loop)
				{
					this.timerRunning = false;
				}
			}
		}

		// Token: 0x04004625 RID: 17957
		[SerializeField]
		private float _duration;

		// Token: 0x04004626 RID: 17958
		[SerializeField]
		private Timer.TimeStepType _timeStepType = Timer.TimeStepType.FixedTime;

		// Token: 0x04004627 RID: 17959
		public bool resetTimerOnEnable = true;

		// Token: 0x04004628 RID: 17960
		public bool playTimerOnEnable = true;

		// Token: 0x04004629 RID: 17961
		public bool loop;

		// Token: 0x0400462A RID: 17962
		public UnityEvent action;

		// Token: 0x0400462B RID: 17963
		private float stopwatch;

		// Token: 0x0400462C RID: 17964
		private bool timerRunning;

		// Token: 0x02000C87 RID: 3207
		public enum TimeStepType
		{
			// Token: 0x0400462E RID: 17966
			Time,
			// Token: 0x0400462F RID: 17967
			UnscaledTime,
			// Token: 0x04004630 RID: 17968
			FixedTime
		}
	}
}
