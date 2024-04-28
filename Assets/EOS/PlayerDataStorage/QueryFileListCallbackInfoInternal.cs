using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000265 RID: 613
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00010CB1 File Offset: 0x0000EEB1
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00010CFC File Offset: 0x0000EEFC
		public uint FileCount
		{
			get
			{
				return this.m_FileCount;
			}
		}

		// Token: 0x0400073F RID: 1855
		private Result m_ResultCode;

		// Token: 0x04000740 RID: 1856
		private IntPtr m_ClientData;

		// Token: 0x04000741 RID: 1857
		private IntPtr m_LocalUserId;

		// Token: 0x04000742 RID: 1858
		private uint m_FileCount;
	}
}
