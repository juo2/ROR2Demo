using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000080 RID: 128
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00005A19 File Offset: 0x00003C19
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00005A24 File Offset: 0x00003C24
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00005A40 File Offset: 0x00003C40
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00005A48 File Offset: 0x00003C48
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00005A64 File Offset: 0x00003C64
		public uint FileCount
		{
			get
			{
				return this.m_FileCount;
			}
		}

		// Token: 0x0400026C RID: 620
		private Result m_ResultCode;

		// Token: 0x0400026D RID: 621
		private IntPtr m_ClientData;

		// Token: 0x0400026E RID: 622
		private IntPtr m_LocalUserId;

		// Token: 0x0400026F RID: 623
		private uint m_FileCount;
	}
}
