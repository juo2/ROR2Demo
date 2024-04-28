using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200079C RID: 1948
	public class LoopSound : MonoBehaviour
	{
		// Token: 0x0600291F RID: 10527 RVA: 0x000B2288 File Offset: 0x000B0488
		private void Update()
		{
			this.stopwatch += Time.deltaTime;
			if (this.stopwatch > this.repeatInterval)
			{
				this.stopwatch -= this.repeatInterval;
				if (this.soundOwner)
				{
					Util.PlaySound(this.akSoundString, this.soundOwner.gameObject);
				}
			}
		}

		// Token: 0x04002C93 RID: 11411
		public string akSoundString;

		// Token: 0x04002C94 RID: 11412
		public float repeatInterval;

		// Token: 0x04002C95 RID: 11413
		public Transform soundOwner;

		// Token: 0x04002C96 RID: 11414
		private float stopwatch;
	}
}
