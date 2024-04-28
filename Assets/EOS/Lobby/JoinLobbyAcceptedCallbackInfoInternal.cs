using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031B RID: 795
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00015108 File Offset: 0x00013308
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00015124 File Offset: 0x00013324
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0001512C File Offset: 0x0001332C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00015148 File Offset: 0x00013348
		public ulong UiEventId
		{
			get
			{
				return this.m_UiEventId;
			}
		}

		// Token: 0x0400096F RID: 2415
		private IntPtr m_ClientData;

		// Token: 0x04000970 RID: 2416
		private IntPtr m_LocalUserId;

		// Token: 0x04000971 RID: 2417
		private ulong m_UiEventId;
	}
}
