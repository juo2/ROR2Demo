using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000103 RID: 259
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00008353 File Offset: 0x00006553
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0000835C File Offset: 0x0000655C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00008378 File Offset: 0x00006578
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000398 RID: 920
		private Result m_ResultCode;

		// Token: 0x04000399 RID: 921
		private IntPtr m_ClientData;
	}
}
