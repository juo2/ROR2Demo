using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003CC RID: 972
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x00019007 File Offset: 0x00017207
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00019010 File Offset: 0x00017210
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0001902C File Offset: 0x0001722C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000B10 RID: 2832
		private Result m_ResultCode;

		// Token: 0x04000B11 RID: 2833
		private IntPtr m_ClientData;
	}
}
