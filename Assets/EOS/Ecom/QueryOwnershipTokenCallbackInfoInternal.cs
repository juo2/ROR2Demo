using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000487 RID: 1159
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0001DD3D File Offset: 0x0001BF3D
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0001DD48 File Offset: 0x0001BF48
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0001DD88 File Offset: 0x0001BF88
		public string OwnershipToken
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_OwnershipToken, out result);
				return result;
			}
		}

		// Token: 0x04000D2E RID: 3374
		private Result m_ResultCode;

		// Token: 0x04000D2F RID: 3375
		private IntPtr m_ClientData;

		// Token: 0x04000D30 RID: 3376
		private IntPtr m_LocalUserId;

		// Token: 0x04000D31 RID: 3377
		private IntPtr m_OwnershipToken;
	}
}
