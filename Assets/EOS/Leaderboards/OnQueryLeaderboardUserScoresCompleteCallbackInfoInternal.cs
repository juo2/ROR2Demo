using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D4 RID: 980
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00019167 File Offset: 0x00017367
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00019170 File Offset: 0x00017370
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0001918C File Offset: 0x0001738C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000B18 RID: 2840
		private Result m_ResultCode;

		// Token: 0x04000B19 RID: 2841
		private IntPtr m_ClientData;
	}
}
