using System;
using RoR2.Audio;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200079D RID: 1949
	public class LoopSoundPlayer : MonoBehaviour
	{
		// Token: 0x06002921 RID: 10529 RVA: 0x000B22EC File Offset: 0x000B04EC
		private void OnEnable()
		{
			if (this.loopDef)
			{
				this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopDef);
			}
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000B2312 File Offset: 0x000B0512
		private void OnDisable()
		{
			if (this.loopPtr.isValid)
			{
				LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			}
		}

		// Token: 0x04002C97 RID: 11415
		[SerializeField]
		private LoopSoundDef loopDef;

		// Token: 0x04002C98 RID: 11416
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
