using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CB RID: 203
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x000079FF File Offset: 0x00005BFF
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00007A08 File Offset: 0x00005C08
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00007A24 File Offset: 0x00005C24
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000342 RID: 834
		private Result m_ResultCode;

		// Token: 0x04000343 RID: 835
		private IntPtr m_ClientData;
	}
}
