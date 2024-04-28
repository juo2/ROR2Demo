using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.HudOverlay
{
	// Token: 0x02000BF4 RID: 3060
	public class TargetTracker : IDisposable
	{
		// Token: 0x06004578 RID: 17784 RVA: 0x00121054 File Offset: 0x0011F254
		public TargetTracker()
		{
			this.overlayControllers = CollectionPool<OverlayController, List<OverlayController>>.RentCollection();
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00121068 File Offset: 0x0011F268
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			while (this.overlayControllers.Count > 0)
			{
				this.RemoveOverlayAt(this.overlayControllers.Count - 1);
			}
			this.overlayControllers = CollectionPool<OverlayController, List<OverlayController>>.ReturnCollection(this.overlayControllers);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x001210B9 File Offset: 0x0011F2B9
		public void AddOverlay(OverlayController overlayController)
		{
			this.overlayControllers.Add(overlayController);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x001210C8 File Offset: 0x0011F2C8
		public void RemoveOverlay(OverlayController overlayController)
		{
			int i = this.overlayControllers.IndexOf(overlayController);
			this.RemoveOverlayAt(i);
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x001210E9 File Offset: 0x0011F2E9
		private void RemoveOverlayAt(int i)
		{
			this.overlayControllers.RemoveAt(i);
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x001210F7 File Offset: 0x0011F2F7
		public void GetOverlayControllers(List<OverlayController> dest)
		{
			ListUtils.AddRange<OverlayController, List<OverlayController>>(dest, this.overlayControllers);
		}

		// Token: 0x040043A8 RID: 17320
		public GameObject target;

		// Token: 0x040043A9 RID: 17321
		public int refCount;

		// Token: 0x040043AA RID: 17322
		private List<OverlayController> overlayControllers;

		// Token: 0x040043AB RID: 17323
		private bool disposed;
	}
}
