using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AA RID: 938
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0001834C File Offset: 0x0001654C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00018354 File Offset: 0x00016554
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00018370 File Offset: 0x00016570
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00018378 File Offset: 0x00016578
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000AB9 RID: 2745
		private Result m_ResultCode;

		// Token: 0x04000ABA RID: 2746
		private IntPtr m_ClientData;

		// Token: 0x04000ABB RID: 2747
		private IntPtr m_LobbyId;
	}
}
