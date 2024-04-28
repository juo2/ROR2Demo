using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059E RID: 1438
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientAuthStatusChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x00024EEC File Offset: 0x000230EC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00024F08 File Offset: 0x00023108
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x00024F10 File Offset: 0x00023110
		public IntPtr ClientHandle
		{
			get
			{
				return this.m_ClientHandle;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00024F18 File Offset: 0x00023118
		public AntiCheatCommonClientAuthStatus ClientAuthStatus
		{
			get
			{
				return this.m_ClientAuthStatus;
			}
		}

		// Token: 0x0400107A RID: 4218
		private IntPtr m_ClientData;

		// Token: 0x0400107B RID: 4219
		private IntPtr m_ClientHandle;

		// Token: 0x0400107C RID: 4220
		private AntiCheatCommonClientAuthStatus m_ClientAuthStatus;
	}
}
