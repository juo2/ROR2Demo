using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000003 RID: 3
	internal class CallbackHandle : IDisposable
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal CallbackHandle(BaseSteamworks steamworks)
		{
			this.Steamworks = steamworks;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public void Dispose()
		{
			this.UnregisterCallback();
			if (this.FuncA.IsAllocated)
			{
				this.FuncA.Free();
			}
			if (this.FuncB.IsAllocated)
			{
				this.FuncB.Free();
			}
			if (this.FuncC.IsAllocated)
			{
				this.FuncC.Free();
			}
			if (this.PinnedCallback.IsAllocated)
			{
				this.PinnedCallback.Free();
			}
			if (this.vTablePtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.vTablePtr);
				this.vTablePtr = IntPtr.Zero;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002103 File Offset: 0x00000303
		private void UnregisterCallback()
		{
			if (!this.PinnedCallback.IsAllocated)
			{
				return;
			}
			this.Steamworks.native.api.SteamAPI_UnregisterCallback(this.PinnedCallback.AddrOfPinnedObject());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002133 File Offset: 0x00000333
		public virtual bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000004 RID: 4
		internal BaseSteamworks Steamworks;

		// Token: 0x04000005 RID: 5
		internal GCHandle FuncA;

		// Token: 0x04000006 RID: 6
		internal GCHandle FuncB;

		// Token: 0x04000007 RID: 7
		internal GCHandle FuncC;

		// Token: 0x04000008 RID: 8
		internal IntPtr vTablePtr;

		// Token: 0x04000009 RID: 9
		internal GCHandle PinnedCallback;
	}
}
