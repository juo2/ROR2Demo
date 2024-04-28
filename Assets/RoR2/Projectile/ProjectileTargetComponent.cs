using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BB7 RID: 2999
	public class ProjectileTargetComponent : MonoBehaviour
	{
		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x0011CC00 File Offset: 0x0011AE00
		// (set) Token: 0x06004461 RID: 17505 RVA: 0x0011CC08 File Offset: 0x0011AE08
		public Transform target { get; set; }

		// Token: 0x06004462 RID: 17506 RVA: 0x0011CC11 File Offset: 0x0011AE11
		private void FixedUpdate()
		{
			if (this.target && !this.target.gameObject.activeSelf)
			{
				this.target = null;
			}
		}
	}
}
