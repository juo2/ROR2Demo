using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A0 RID: 928
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RTCRoomConnectionChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00017EF4 File Offset: 0x000160F4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00017F10 File Offset: 0x00016110
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00017F18 File Offset: 0x00016118
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00017F34 File Offset: 0x00016134
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00017F50 File Offset: 0x00016150
		public bool IsConnected
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsConnected, out result);
				return result;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00017F6C File Offset: 0x0001616C
		public Result DisconnectReason
		{
			get
			{
				return this.m_DisconnectReason;
			}
		}

		// Token: 0x04000A99 RID: 2713
		private IntPtr m_ClientData;

		// Token: 0x04000A9A RID: 2714
		private IntPtr m_LobbyId;

		// Token: 0x04000A9B RID: 2715
		private IntPtr m_LocalUserId;

		// Token: 0x04000A9C RID: 2716
		private int m_IsConnected;

		// Token: 0x04000A9D RID: 2717
		private Result m_DisconnectReason;
	}
}
