using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000309 RID: 777
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00014A64 File Offset: 0x00012C64
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00014A6C File Offset: 0x00012C6C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00014A88 File Offset: 0x00012C88
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x00014A90 File Offset: 0x00012C90
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000934 RID: 2356
		private Result m_ResultCode;

		// Token: 0x04000935 RID: 2357
		private IntPtr m_ClientData;

		// Token: 0x04000936 RID: 2358
		private IntPtr m_LobbyId;
	}
}
