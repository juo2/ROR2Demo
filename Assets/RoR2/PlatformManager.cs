using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009BD RID: 2493
	public abstract class PlatformManager
	{
		// Token: 0x06003906 RID: 14598 RVA: 0x00004479 File Offset: 0x00002679
		public PlatformManager()
		{
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000EE41A File Offset: 0x000EC61A
		public virtual void InitializePlatformManager()
		{
			Debug.Log("Initialize Platform Manager in base class.");
			RoR2Application.onUpdate += this.UpdatePlatformManager;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void StartSinglePlayer()
		{
		}

		// Token: 0x06003909 RID: 14601
		protected abstract void UpdatePlatformManager();
	}
}
