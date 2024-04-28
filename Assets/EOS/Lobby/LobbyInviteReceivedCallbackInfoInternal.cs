using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000344 RID: 836
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00016C58 File Offset: 0x00014E58
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00016C74 File Offset: 0x00014E74
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00016C7C File Offset: 0x00014E7C
		public string InviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_InviteId, out result);
				return result;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00016C98 File Offset: 0x00014E98
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00016CB4 File Offset: 0x00014EB4
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x04000A08 RID: 2568
		private IntPtr m_ClientData;

		// Token: 0x04000A09 RID: 2569
		private IntPtr m_InviteId;

		// Token: 0x04000A0A RID: 2570
		private IntPtr m_LocalUserId;

		// Token: 0x04000A0B RID: 2571
		private IntPtr m_TargetUserId;
	}
}
