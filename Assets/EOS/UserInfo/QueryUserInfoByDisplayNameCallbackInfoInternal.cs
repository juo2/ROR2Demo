using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000032 RID: 50
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00004022 File Offset: 0x00002222
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000402C File Offset: 0x0000222C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00004048 File Offset: 0x00002248
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00004050 File Offset: 0x00002250
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000406C File Offset: 0x0000226C
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00004088 File Offset: 0x00002288
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
		}

		// Token: 0x0400014A RID: 330
		private Result m_ResultCode;

		// Token: 0x0400014B RID: 331
		private IntPtr m_ClientData;

		// Token: 0x0400014C RID: 332
		private IntPtr m_LocalUserId;

		// Token: 0x0400014D RID: 333
		private IntPtr m_TargetUserId;

		// Token: 0x0400014E RID: 334
		private IntPtr m_DisplayName;
	}
}
