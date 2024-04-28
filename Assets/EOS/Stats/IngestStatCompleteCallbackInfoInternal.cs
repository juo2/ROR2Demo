using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000097 RID: 151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00006699 File Offset: 0x00004899
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000066A4 File Offset: 0x000048A4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x000066C0 File Offset: 0x000048C0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000066C8 File Offset: 0x000048C8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000066E4 File Offset: 0x000048E4
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x040002C0 RID: 704
		private Result m_ResultCode;

		// Token: 0x040002C1 RID: 705
		private IntPtr m_ClientData;

		// Token: 0x040002C2 RID: 706
		private IntPtr m_LocalUserId;

		// Token: 0x040002C3 RID: 707
		private IntPtr m_TargetUserId;
	}
}
