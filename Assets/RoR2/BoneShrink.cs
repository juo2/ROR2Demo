using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005FB RID: 1531
	public class BoneShrink : MonoBehaviour
	{
		// Token: 0x06001BF4 RID: 7156 RVA: 0x000770EC File Offset: 0x000752EC
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000770FA File Offset: 0x000752FA
		private void LateUpdate()
		{
			this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		}

		// Token: 0x040021C6 RID: 8646
		private new Transform transform;
	}
}
