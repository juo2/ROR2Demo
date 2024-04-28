using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003FA RID: 1018
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PermissionsUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00019DD8 File Offset: 0x00017FD8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000B6E RID: 2926
		private IntPtr m_ClientData;

		// Token: 0x04000B6F RID: 2927
		private IntPtr m_LocalUserId;
	}
}
