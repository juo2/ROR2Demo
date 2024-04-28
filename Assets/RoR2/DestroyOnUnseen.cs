using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200069E RID: 1694
	public class DestroyOnUnseen : MonoBehaviour
	{
		// Token: 0x06002116 RID: 8470 RVA: 0x0008DE6C File Offset: 0x0008C06C
		private void Start()
		{
			this.rend = base.GetComponentInChildren<Renderer>();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0008DE7A File Offset: 0x0008C07A
		private void Update()
		{
			if (this.cull && this.rend && !this.rend.isVisible)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0400266C RID: 9836
		public bool cull;

		// Token: 0x0400266D RID: 9837
		private Renderer rend;
	}
}
