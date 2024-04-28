using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000398 RID: 920
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00017BA0 File Offset: 0x00015DA0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00017BA8 File Offset: 0x00015DA8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00017BC4 File Offset: 0x00015DC4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00017BCC File Offset: 0x00015DCC
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x04000A81 RID: 2689
		private Result m_ResultCode;

		// Token: 0x04000A82 RID: 2690
		private IntPtr m_ClientData;

		// Token: 0x04000A83 RID: 2691
		private IntPtr m_LobbyId;
	}
}
