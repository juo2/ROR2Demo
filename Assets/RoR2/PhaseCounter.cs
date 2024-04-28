using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007FC RID: 2044
	public class PhaseCounter : MonoBehaviour
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000BC75F File Offset: 0x000BA95F
		public static PhaseCounter instance
		{
			get
			{
				return PhaseCounter._instance;
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000BC766 File Offset: 0x000BA966
		private void OnEnable()
		{
			if (!PhaseCounter._instance)
			{
				PhaseCounter._instance = this;
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000BC77A File Offset: 0x000BA97A
		private void OnDisable()
		{
			if (PhaseCounter._instance == this)
			{
				PhaseCounter._instance = null;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000BC78F File Offset: 0x000BA98F
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000BC797 File Offset: 0x000BA997
		public int phase { get; private set; }

		// Token: 0x06002C12 RID: 11282 RVA: 0x000BC7A0 File Offset: 0x000BA9A0
		public void GoToNextPhase()
		{
			int phase = this.phase;
			this.phase = phase + 1;
			Debug.LogFormat("Entering phase {0}", new object[]
			{
				this.phase
			});
		}

		// Token: 0x04002E84 RID: 11908
		private static PhaseCounter _instance;
	}
}
