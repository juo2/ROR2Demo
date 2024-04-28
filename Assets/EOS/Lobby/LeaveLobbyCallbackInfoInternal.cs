using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000325 RID: 805
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0001558C File Offset: 0x0001378C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00015594 File Offset: 0x00013794
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x000155B0 File Offset: 0x000137B0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x000155B8 File Offset: 0x000137B8
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000991 RID: 2449
		private Result m_ResultCode;

		// Token: 0x04000992 RID: 2450
		private IntPtr m_ClientData;

		// Token: 0x04000993 RID: 2451
		private IntPtr m_LobbyId;
	}
}
