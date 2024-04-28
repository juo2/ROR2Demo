using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006D6 RID: 1750
	public class FadeToBlackOffset : MonoBehaviour
	{
		// Token: 0x06002282 RID: 8834 RVA: 0x00094DA2 File Offset: 0x00092FA2
		private void OnEnable()
		{
			InstanceTracker.Add<FadeToBlackOffset>(this);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00094DAA File Offset: 0x00092FAA
		private void OnDisable()
		{
			InstanceTracker.Remove<FadeToBlackOffset>(this);
		}

		// Token: 0x04002781 RID: 10113
		public float value;
	}
}
