using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D91 RID: 3473
	[RequireComponent(typeof(Image))]
	public class StageFadeTransitionController : MonoBehaviour
	{
		// Token: 0x06004F83 RID: 20355 RVA: 0x00148F68 File Offset: 0x00147168
		private void Awake()
		{
			this.fadeImage = base.GetComponent<Image>();
			Color color = this.fadeImage.color;
			color.a = 1f;
			this.fadeImage.color = color;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x00148FA8 File Offset: 0x001471A8
		private void Start()
		{
			Color color = this.fadeImage.color;
			color.a = 1f;
			this.fadeImage.color = color;
			this.fadeImage.CrossFadeColor(Color.black, 0.5f, false, true);
			this.startEngineTime = Time.time;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00148FFC File Offset: 0x001471FC
		private void Update()
		{
			if (Stage.instance)
			{
				Run.FixedTimeStamp stageAdvanceTime = Stage.instance.stageAdvanceTime;
				float num = Time.time - this.startEngineTime;
				float a = 0f;
				float b = 0f;
				if (num < 0.5f)
				{
					a = 1f - Mathf.Clamp01((Time.time - this.startEngineTime) / 0.5f);
				}
				if (!stageAdvanceTime.isInfinity)
				{
					float num2 = Stage.instance.stageAdvanceTime - 0.25f - Run.FixedTimeStamp.now;
					b = 1f - Mathf.Clamp01(num2 / 0.5f);
				}
				Color color = this.fadeImage.color;
				color.a = Mathf.Max(a, b);
				this.fadeImage.color = color;
			}
		}

		// Token: 0x04004C2F RID: 19503
		private Image fadeImage;

		// Token: 0x04004C30 RID: 19504
		private float startEngineTime;

		// Token: 0x04004C31 RID: 19505
		private const float transitionDuration = 0.5f;
	}
}
