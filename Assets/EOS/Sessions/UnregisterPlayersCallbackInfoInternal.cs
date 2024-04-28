using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000146 RID: 326
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0000A214 File Offset: 0x00008414
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0000A21C File Offset: 0x0000841C
		public ProductUserId[] UnregisteredPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.TryMarshalGetHandle<ProductUserId>(this.m_UnregisteredPlayers, out result, this.m_UnregisteredPlayersCount);
				return result;
			}
		}

		// Token: 0x04000458 RID: 1112
		private Result m_ResultCode;

		// Token: 0x04000459 RID: 1113
		private IntPtr m_ClientData;

		// Token: 0x0400045A RID: 1114
		private IntPtr m_UnregisteredPlayers;

		// Token: 0x0400045B RID: 1115
		private uint m_UnregisteredPlayersCount;
	}
}
