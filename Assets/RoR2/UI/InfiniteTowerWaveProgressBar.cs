using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D21 RID: 3361
	public class InfiniteTowerWaveProgressBar : MonoBehaviour
	{
		// Token: 0x06004C8B RID: 19595 RVA: 0x0013C1F8 File Offset: 0x0013A3F8
		private void OnEnable()
		{
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			if (infiniteTowerRun)
			{
				this.waveController = infiniteTowerRun.waveController;
			}
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0013C224 File Offset: 0x0013A424
		private void Update()
		{
			if (this.waveController)
			{
				float normalizedProgress = this.waveController.GetNormalizedProgress();
				if (normalizedProgress > this.previousFillAmount)
				{
					this.previousFillAmount = normalizedProgress;
					if (this.animator)
					{
						int layerIndex = this.animator.GetLayerIndex("Base");
						this.animator.Play("Idle", layerIndex);
						this.animator.Update(0f);
						this.animator.Play((normalizedProgress >= 1f) ? "Ready" : "Pulse", layerIndex);
					}
				}
				if (this.barImage)
				{
					this.barImage.fillAmount = normalizedProgress;
				}
			}
		}

		// Token: 0x04004998 RID: 18840
		[Tooltip("The bar we're filling up")]
		[SerializeField]
		private Image barImage;

		// Token: 0x04004999 RID: 18841
		[SerializeField]
		private Animator animator;

		// Token: 0x0400499A RID: 18842
		private InfiniteTowerWaveController waveController;

		// Token: 0x0400499B RID: 18843
		private float previousFillAmount;
	}
}
