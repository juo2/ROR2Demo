using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC6 RID: 3014
	public class RandomBlinkController : MonoBehaviour
	{
		// Token: 0x06004499 RID: 17561 RVA: 0x0011D9E0 File Offset: 0x0011BBE0
		private void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= 0.25f)
			{
				this.stopwatch = 0f;
				for (int i = 0; i < this.blinkTriggers.Length; i++)
				{
					if (Util.CheckRoll(this.blinkChancePerUpdate, 0f, null))
					{
						this.animator.SetTrigger(this.blinkTriggers[i]);
					}
				}
			}
		}

		// Token: 0x0400431C RID: 17180
		public Animator animator;

		// Token: 0x0400431D RID: 17181
		public string[] blinkTriggers;

		// Token: 0x0400431E RID: 17182
		public float blinkChancePerUpdate;

		// Token: 0x0400431F RID: 17183
		private float stopwatch;

		// Token: 0x04004320 RID: 17184
		private const float updateFrequency = 4f;
	}
}
