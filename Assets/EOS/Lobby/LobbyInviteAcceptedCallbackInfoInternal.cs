using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000342 RID: 834
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00016B04 File Offset: 0x00014D04
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00016B0C File Offset: 0x00014D0C
		public string InviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_InviteId, out result);
				return result;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00016B28 File Offset: 0x00014D28
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00016B44 File Offset: 0x00014D44
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00016B60 File Offset: 0x00014D60
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x040009FF RID: 2559
		private IntPtr m_ClientData;

		// Token: 0x04000A00 RID: 2560
		private IntPtr m_InviteId;

		// Token: 0x04000A01 RID: 2561
		private IntPtr m_LocalUserId;

		// Token: 0x04000A02 RID: 2562
		private IntPtr m_TargetUserId;

		// Token: 0x04000A03 RID: 2563
		private IntPtr m_LobbyId;
	}
}
