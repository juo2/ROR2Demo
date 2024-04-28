using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000524 RID: 1316
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00021B87 File Offset: 0x0001FD87
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00021B90 File Offset: 0x0001FD90
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x00021BAC File Offset: 0x0001FDAC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x00021BD0 File Offset: 0x0001FDD0
		public PinGrantInfo PinGrantInfo
		{
			get
			{
				PinGrantInfo result;
				Helper.TryMarshalGet<PinGrantInfoInternal, PinGrantInfo>(this.m_PinGrantInfo, out result);
				return result;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x00021BEC File Offset: 0x0001FDEC
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken result;
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out result);
				return result;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x00021C08 File Offset: 0x0001FE08
		public AccountFeatureRestrictedInfo AccountFeatureRestrictedInfo
		{
			get
			{
				AccountFeatureRestrictedInfo result;
				Helper.TryMarshalGet<AccountFeatureRestrictedInfoInternal, AccountFeatureRestrictedInfo>(this.m_AccountFeatureRestrictedInfo, out result);
				return result;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00021C24 File Offset: 0x0001FE24
		public EpicAccountId SelectedAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_SelectedAccountId, out result);
				return result;
			}
		}

		// Token: 0x04000EBE RID: 3774
		private Result m_ResultCode;

		// Token: 0x04000EBF RID: 3775
		private IntPtr m_ClientData;

		// Token: 0x04000EC0 RID: 3776
		private IntPtr m_LocalUserId;

		// Token: 0x04000EC1 RID: 3777
		private IntPtr m_PinGrantInfo;

		// Token: 0x04000EC2 RID: 3778
		private IntPtr m_ContinuanceToken;

		// Token: 0x04000EC3 RID: 3779
		private IntPtr m_AccountFeatureRestrictedInfo;

		// Token: 0x04000EC4 RID: 3780
		private IntPtr m_SelectedAccountId;
	}
}
