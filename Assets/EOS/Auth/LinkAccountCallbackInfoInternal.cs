using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200051F RID: 1311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0002190E File Offset: 0x0001FB0E
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00021918 File Offset: 0x0001FB18
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00021934 File Offset: 0x0001FB34
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0002193C File Offset: 0x0001FB3C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00021958 File Offset: 0x0001FB58
		public PinGrantInfo PinGrantInfo
		{
			get
			{
				PinGrantInfo result;
				Helper.TryMarshalGet<PinGrantInfoInternal, PinGrantInfo>(this.m_PinGrantInfo, out result);
				return result;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x00021974 File Offset: 0x0001FB74
		public EpicAccountId SelectedAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_SelectedAccountId, out result);
				return result;
			}
		}

		// Token: 0x04000EA8 RID: 3752
		private Result m_ResultCode;

		// Token: 0x04000EA9 RID: 3753
		private IntPtr m_ClientData;

		// Token: 0x04000EAA RID: 3754
		private IntPtr m_LocalUserId;

		// Token: 0x04000EAB RID: 3755
		private IntPtr m_PinGrantInfo;

		// Token: 0x04000EAC RID: 3756
		private IntPtr m_SelectedAccountId;
	}
}
