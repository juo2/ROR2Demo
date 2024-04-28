using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008E0 RID: 2272
	public class UnparentOnStart : MonoBehaviour
	{
		// Token: 0x060032F9 RID: 13049 RVA: 0x000D6C75 File Offset: 0x000D4E75
		private void Start()
		{
			base.transform.parent = null;
		}
	}
}
