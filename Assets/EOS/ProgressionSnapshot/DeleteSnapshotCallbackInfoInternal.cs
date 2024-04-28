using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F5 RID: 501
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteSnapshotCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000647 RID: 1607
		private Result m_ResultCode;

		// Token: 0x04000648 RID: 1608
		private IntPtr m_LocalUserId;

		// Token: 0x04000649 RID: 1609
		private IntPtr m_ClientData;
	}
}
