using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00008DCC File Offset: 0x00006FCC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00008DE8 File Offset: 0x00006FE8
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00008E04 File Offset: 0x00007004
		public string InviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_InviteId, out result);
				return result;
			}
		}

		// Token: 0x040003DC RID: 988
		private IntPtr m_ClientData;

		// Token: 0x040003DD RID: 989
		private IntPtr m_LocalUserId;

		// Token: 0x040003DE RID: 990
		private IntPtr m_TargetUserId;

		// Token: 0x040003DF RID: 991
		private IntPtr m_InviteId;
	}
}
