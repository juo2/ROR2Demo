using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A6 RID: 934
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00018198 File Offset: 0x00016398
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000181A0 File Offset: 0x000163A0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000181BC File Offset: 0x000163BC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000181C4 File Offset: 0x000163C4
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000AAC RID: 2732
		private Result m_ResultCode;

		// Token: 0x04000AAD RID: 2733
		private IntPtr m_ClientData;

		// Token: 0x04000AAE RID: 2734
		private IntPtr m_LobbyId;
	}
}
