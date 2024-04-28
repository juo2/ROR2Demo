using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryStatsCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00006899 File Offset: 0x00004A99
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000068A4 File Offset: 0x00004AA4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000068C0 File Offset: 0x00004AC0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000068C8 File Offset: 0x00004AC8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000068E4 File Offset: 0x00004AE4
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x040002D0 RID: 720
		private Result m_ResultCode;

		// Token: 0x040002D1 RID: 721
		private IntPtr m_ClientData;

		// Token: 0x040002D2 RID: 722
		private IntPtr m_LocalUserId;

		// Token: 0x040002D3 RID: 723
		private IntPtr m_TargetUserId;
	}
}
