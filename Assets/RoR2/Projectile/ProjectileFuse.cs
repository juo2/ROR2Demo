using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B92 RID: 2962
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileFuse : MonoBehaviour
	{
		// Token: 0x06004374 RID: 17268 RVA: 0x00002A4D File Offset: 0x00000C4D
		private void Awake()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x001182CD File Offset: 0x001164CD
		private void FixedUpdate()
		{
			this.fuse -= Time.fixedDeltaTime;
			if (this.fuse <= 0f)
			{
				base.enabled = false;
				this.onFuse.Invoke();
			}
		}

		// Token: 0x040041C5 RID: 16837
		public float fuse;

		// Token: 0x040041C6 RID: 16838
		public UnityEvent onFuse;
	}
}
