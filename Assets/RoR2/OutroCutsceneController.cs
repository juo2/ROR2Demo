using System;
using UnityEngine;
using UnityEngine.Playables;

namespace RoR2
{
	// Token: 0x020007F2 RID: 2034
	public class OutroCutsceneController : MonoBehaviour
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000BBD0D File Offset: 0x000B9F0D
		// (set) Token: 0x06002BDF RID: 11231 RVA: 0x000BBD14 File Offset: 0x000B9F14
		public static OutroCutsceneController instance { get; private set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000BBD1C File Offset: 0x000B9F1C
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x000BBD24 File Offset: 0x000B9F24
		public bool cutsceneIsFinished { get; private set; }

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000BBD2D File Offset: 0x000B9F2D
		public void Finish()
		{
			this.finishCount++;
			if (this.finishCount >= 1)
			{
				this.cutsceneIsFinished = true;
			}
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000BBD4D File Offset: 0x000B9F4D
		private void OnEnable()
		{
			OutroCutsceneController.instance = SingletonHelper.Assign<OutroCutsceneController>(OutroCutsceneController.instance, this);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000BBD5F File Offset: 0x000B9F5F
		private void OnDisable()
		{
			OutroCutsceneController.instance = SingletonHelper.Unassign<OutroCutsceneController>(OutroCutsceneController.instance, this);
		}

		// Token: 0x04002E55 RID: 11861
		public PlayableDirector playableDirector;

		// Token: 0x04002E57 RID: 11863
		private int finishCount;
	}
}
