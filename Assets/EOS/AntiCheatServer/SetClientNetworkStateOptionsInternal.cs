using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056E RID: 1390
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetClientNetworkStateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A59 RID: 2649
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x000238F5 File Offset: 0x00021AF5
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x000238FE File Offset: 0x00021AFE
		public bool IsNetworkActive
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsNetworkActive, value);
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0002390D File Offset: 0x00021B0D
		public void Set(SetClientNetworkStateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.IsNetworkActive = other.IsNetworkActive;
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00023931 File Offset: 0x00021B31
		public void Set(object other)
		{
			this.Set(other as SetClientNetworkStateOptions);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0002393F File Offset: 0x00021B3F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
		}

		// Token: 0x04000F7F RID: 3967
		private int m_ApiVersion;

		// Token: 0x04000F80 RID: 3968
		private IntPtr m_ClientHandle;

		// Token: 0x04000F81 RID: 3969
		private int m_IsNetworkActive;
	}
}
