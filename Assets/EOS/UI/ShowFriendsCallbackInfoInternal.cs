using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005F RID: 95
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00004F64 File Offset: 0x00003164
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00004F6C File Offset: 0x0000316C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00004F88 File Offset: 0x00003188
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00004F90 File Offset: 0x00003190
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000227 RID: 551
		private Result m_ResultCode;

		// Token: 0x04000228 RID: 552
		private IntPtr m_ClientData;

		// Token: 0x04000229 RID: 553
		private IntPtr m_LocalUserId;
	}
}
