using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FB RID: 251
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0000808D File Offset: 0x0000628D
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00008098 File Offset: 0x00006298
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000080B4 File Offset: 0x000062B4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000080BC File Offset: 0x000062BC
		public ProductUserId[] RegisteredPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.TryMarshalGetHandle<ProductUserId>(this.m_RegisteredPlayers, out result, this.m_RegisteredPlayersCount);
				return result;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000080E0 File Offset: 0x000062E0
		public ProductUserId[] SanctionedPlayers
		{
			get
			{
				ProductUserId[] result;
				Helper.TryMarshalGetHandle<ProductUserId>(this.m_SanctionedPlayers, out result, this.m_SanctionedPlayersCount);
				return result;
			}
		}

		// Token: 0x04000381 RID: 897
		private Result m_ResultCode;

		// Token: 0x04000382 RID: 898
		private IntPtr m_ClientData;

		// Token: 0x04000383 RID: 899
		private IntPtr m_RegisteredPlayers;

		// Token: 0x04000384 RID: 900
		private uint m_RegisteredPlayersCount;

		// Token: 0x04000385 RID: 901
		private IntPtr m_SanctionedPlayers;

		// Token: 0x04000386 RID: 902
		private uint m_SanctionedPlayersCount;
	}
}
