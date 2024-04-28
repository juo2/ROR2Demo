using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200030F RID: 783
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00014D98 File Offset: 0x00012F98
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00014DA0 File Offset: 0x00012FA0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00014DBC File Offset: 0x00012FBC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00014DC4 File Offset: 0x00012FC4
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000952 RID: 2386
		private Result m_ResultCode;

		// Token: 0x04000953 RID: 2387
		private IntPtr m_ClientData;

		// Token: 0x04000954 RID: 2388
		private IntPtr m_LobbyId;
	}
}
