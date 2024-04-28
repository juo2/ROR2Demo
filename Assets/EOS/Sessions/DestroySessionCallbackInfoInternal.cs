using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C5 RID: 197
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x000078A7 File Offset: 0x00005AA7
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000078B0 File Offset: 0x00005AB0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x000078CC File Offset: 0x00005ACC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000338 RID: 824
		private Result m_ResultCode;

		// Token: 0x04000339 RID: 825
		private IntPtr m_ClientData;
	}
}
