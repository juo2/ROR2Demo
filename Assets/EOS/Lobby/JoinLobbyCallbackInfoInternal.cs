using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031D RID: 797
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000151F8 File Offset: 0x000133F8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00015200 File Offset: 0x00013400
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0001521C File Offset: 0x0001341C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00015224 File Offset: 0x00013424
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000975 RID: 2421
		private Result m_ResultCode;

		// Token: 0x04000976 RID: 2422
		private IntPtr m_ClientData;

		// Token: 0x04000977 RID: 2423
		private IntPtr m_LobbyId;
	}
}
