using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000347 RID: 839
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberStatusReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00016DAC File Offset: 0x00014FAC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00016DC8 File Offset: 0x00014FC8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00016DD0 File Offset: 0x00014FD0
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00016DEC File Offset: 0x00014FEC
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00016E08 File Offset: 0x00015008
		public LobbyMemberStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
		}

		// Token: 0x04000A17 RID: 2583
		private IntPtr m_ClientData;

		// Token: 0x04000A18 RID: 2584
		private IntPtr m_LobbyId;

		// Token: 0x04000A19 RID: 2585
		private IntPtr m_TargetUserId;

		// Token: 0x04000A1A RID: 2586
		private LobbyMemberStatus m_CurrentStatus;
	}
}
