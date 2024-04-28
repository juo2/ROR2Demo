using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000117 RID: 279
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00008C38 File Offset: 0x00006E38
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00008C54 File Offset: 0x00006E54
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00008C5C File Offset: 0x00006E5C
		public string SessionId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SessionId, out result);
				return result;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00008C78 File Offset: 0x00006E78
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00008C94 File Offset: 0x00006E94
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public string InviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_InviteId, out result);
				return result;
			}
		}

		// Token: 0x040003D3 RID: 979
		private IntPtr m_ClientData;

		// Token: 0x040003D4 RID: 980
		private IntPtr m_SessionId;

		// Token: 0x040003D5 RID: 981
		private IntPtr m_LocalUserId;

		// Token: 0x040003D6 RID: 982
		private IntPtr m_TargetUserId;

		// Token: 0x040003D7 RID: 983
		private IntPtr m_InviteId;
	}
}
