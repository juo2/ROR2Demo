using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000349 RID: 841
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00016EC4 File Offset: 0x000150C4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00016EE0 File Offset: 0x000150E0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00016EE8 File Offset: 0x000150E8
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00016F04 File Offset: 0x00015104
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x04000A1E RID: 2590
		private IntPtr m_ClientData;

		// Token: 0x04000A1F RID: 2591
		private IntPtr m_LobbyId;

		// Token: 0x04000A20 RID: 2592
		private IntPtr m_TargetUserId;
	}
}
