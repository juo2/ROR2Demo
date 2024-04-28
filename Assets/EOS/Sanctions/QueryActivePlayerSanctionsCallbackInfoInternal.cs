using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000158 RID: 344
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryActivePlayerSanctionsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0000A7ED File Offset: 0x000089ED
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0000A814 File Offset: 0x00008A14
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0000A81C File Offset: 0x00008A1C
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0000A838 File Offset: 0x00008A38
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000485 RID: 1157
		private Result m_ResultCode;

		// Token: 0x04000486 RID: 1158
		private IntPtr m_ClientData;

		// Token: 0x04000487 RID: 1159
		private IntPtr m_TargetUserId;

		// Token: 0x04000488 RID: 1160
		private IntPtr m_LocalUserId;
	}
}
