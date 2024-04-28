using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000321 RID: 801
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000153D8 File Offset: 0x000135D8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000153E0 File Offset: 0x000135E0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000153FC File Offset: 0x000135FC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00015404 File Offset: 0x00013604
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000984 RID: 2436
		private Result m_ResultCode;

		// Token: 0x04000985 RID: 2437
		private IntPtr m_ClientData;

		// Token: 0x04000986 RID: 2438
		private IntPtr m_LobbyId;
	}
}
