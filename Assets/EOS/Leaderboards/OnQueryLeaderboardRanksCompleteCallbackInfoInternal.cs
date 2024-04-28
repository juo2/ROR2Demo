using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D0 RID: 976
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardRanksCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000190B7 File Offset: 0x000172B7
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000190C0 File Offset: 0x000172C0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000190DC File Offset: 0x000172DC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private Result m_ResultCode;

		// Token: 0x04000B15 RID: 2837
		private IntPtr m_ClientData;
	}
}
