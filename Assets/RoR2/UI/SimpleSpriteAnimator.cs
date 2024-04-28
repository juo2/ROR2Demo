using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D89 RID: 3465
	[RequireComponent(typeof(Image))]
	public class SimpleSpriteAnimator : MonoBehaviour
	{
		// Token: 0x06004F63 RID: 20323 RVA: 0x0014848A File Offset: 0x0014668A
		private void Awake()
		{
			this.target = base.GetComponent<Image>();
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00148498 File Offset: 0x00146698
		private void Update()
		{
			if (!this.animation)
			{
				return;
			}
			if (this.animation.frames.Length == 0)
			{
				return;
			}
			this.tickStopwatch += Time.fixedDeltaTime;
			float num = 1f / this.animation.frameRate;
			if (this.tickStopwatch > num)
			{
				this.tickStopwatch -= num;
				this.Tick();
			}
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00148504 File Offset: 0x00146704
		private void Tick()
		{
			this.tick++;
			if (this.frame >= this.animation.frames.Length)
			{
				this.frame = 0;
				this.tick = 0;
			}
			ref SimpleSpriteAnimation.Frame ptr = ref this.animation.frames[this.frame];
			if (ptr.duration <= this.tick)
			{
				this.frame++;
				if (this.frame >= this.animation.frames.Length)
				{
					this.frame = 0;
					this.tick = 0;
				}
				ptr = ref this.animation.frames[this.frame];
				this.target.sprite = ptr.sprite;
			}
		}

		// Token: 0x04004C07 RID: 19463
		public SimpleSpriteAnimation animation;

		// Token: 0x04004C08 RID: 19464
		private Image target;

		// Token: 0x04004C09 RID: 19465
		private int frame;

		// Token: 0x04004C0A RID: 19466
		private int tick;

		// Token: 0x04004C0B RID: 19467
		private float tickStopwatch;
	}
}
