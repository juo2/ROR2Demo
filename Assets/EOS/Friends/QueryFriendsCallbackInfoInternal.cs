using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000426 RID: 1062
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0001AC94 File Offset: 0x00018E94
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000BD4 RID: 3028
		private Result m_ResultCode;

		// Token: 0x04000BD5 RID: 3029
		private IntPtr m_ClientData;

		// Token: 0x04000BD6 RID: 3030
		private IntPtr m_LocalUserId;
	}
}
