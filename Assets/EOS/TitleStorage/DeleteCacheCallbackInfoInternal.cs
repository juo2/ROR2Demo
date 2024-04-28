using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000068 RID: 104
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00005458 File Offset: 0x00003658
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00005460 File Offset: 0x00003660
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000547C File Offset: 0x0000367C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00005484 File Offset: 0x00003684
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000246 RID: 582
		private Result m_ResultCode;

		// Token: 0x04000247 RID: 583
		private IntPtr m_ClientData;

		// Token: 0x04000248 RID: 584
		private IntPtr m_LocalUserId;
	}
}
