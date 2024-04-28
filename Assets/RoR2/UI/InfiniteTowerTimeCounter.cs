using System;
using RoR2.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D1F RID: 3359
	public class InfiniteTowerTimeCounter : MonoBehaviour
	{
		// Token: 0x06004C84 RID: 19588 RVA: 0x0013BF34 File Offset: 0x0013A134
		private void OnEnable()
		{
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			if (infiniteTowerRun)
			{
				this.waveController = infiniteTowerRun.waveController;
			}
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0013BF60 File Offset: 0x0013A160
		private void OnDisable()
		{
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0013BF70 File Offset: 0x0013A170
		private void Update()
		{
			if (this.waveController)
			{
				bool flag = this.waveController.secondsRemaining <= 0f;
				bool flag2 = this.waveController.isTimerActive;
				InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
				if (infiniteTowerRun && infiniteTowerRun.IsStageTransitionWave())
				{
					flag2 = false;
				}
				if (flag && this.loopPtr.isValid)
				{
					LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
				}
				this.rootObject.SetActive(flag2 && !flag);
				if (this.animator && (!this.wasTimerActive && flag2))
				{
					int layerIndex = this.animator.GetLayerIndex("Base");
					this.animator.Play("Idle", layerIndex);
					this.animator.Update(0f);
					this.animator.Play("Finish", layerIndex);
				}
				this.wasTimerActive = flag2;
				if (flag2)
				{
					if (this.timerText)
					{
						if (this.timerLoop && !this.loopPtr.isValid && this.waveController.secondsRemaining > 0f)
						{
							this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.timerLoop);
						}
						int num = Mathf.FloorToInt((float)this.timerText.seconds);
						int num2 = Mathf.FloorToInt(this.waveController.secondsRemaining);
						if (num != num2)
						{
							if (this.waveController.secondsRemaining < this.criticalSecondsThreshold)
							{
								Util.PlaySound(this.onSecondRegularSound, RoR2Application.instance.gameObject);
							}
							else
							{
								Util.PlaySound(this.onSecondCriticalSound, RoR2Application.instance.gameObject);
							}
						}
						this.timerText.seconds = (double)this.waveController.secondsRemaining;
					}
					if (this.barImage)
					{
						this.barImage.fillAmount = this.waveController.secondsRemaining / (float)this.waveController.secondsAfterWave;
						return;
					}
				}
				else if (this.loopPtr.isValid)
				{
					LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
					return;
				}
			}
			else
			{
				this.rootObject.SetActive(false);
			}
		}

		// Token: 0x0400498A RID: 18826
		[Tooltip("The root we're toggling")]
		[SerializeField]
		private GameObject rootObject;

		// Token: 0x0400498B RID: 18827
		[SerializeField]
		[Tooltip("The timer we're setting")]
		private TimerText timerText;

		// Token: 0x0400498C RID: 18828
		[SerializeField]
		private Animator animator;

		// Token: 0x0400498D RID: 18829
		[SerializeField]
		private Image barImage;

		// Token: 0x0400498E RID: 18830
		[SerializeField]
		[Tooltip("The sound we loop while the timer is active")]
		private LoopSoundDef timerLoop;

		// Token: 0x0400498F RID: 18831
		[Tooltip("The sound we play on each second above criticalSecondsThreshold")]
		[SerializeField]
		private string onSecondRegularSound;

		// Token: 0x04004990 RID: 18832
		[SerializeField]
		[Tooltip("The sound we play on each second below criticalSecondsThreshold")]
		private string onSecondCriticalSound;

		// Token: 0x04004991 RID: 18833
		[SerializeField]
		[Tooltip("Below this number of seconds remaining, we are 'critical'")]
		private float criticalSecondsThreshold;

		// Token: 0x04004992 RID: 18834
		private InfiniteTowerWaveController waveController;

		// Token: 0x04004993 RID: 18835
		private bool wasTimerActive;

		// Token: 0x04004994 RID: 18836
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
