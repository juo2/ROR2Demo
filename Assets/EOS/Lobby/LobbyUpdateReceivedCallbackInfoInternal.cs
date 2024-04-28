using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000372 RID: 882
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyUpdateReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00017914 File Offset: 0x00015B14
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00017930 File Offset: 0x00015B30
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00017938 File Offset: 0x00015B38
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000A73 RID: 2675
		private IntPtr m_ClientData;

		// Token: 0x04000A74 RID: 2676
		private IntPtr m_LobbyId;
	}
}
