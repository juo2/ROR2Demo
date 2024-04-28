using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FF RID: 255
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00008217 File Offset: 0x00006417
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00008220 File Offset: 0x00006420
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000823C File Offset: 0x0000643C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400038F RID: 911
		private Result m_ResultCode;

		// Token: 0x04000390 RID: 912
		private IntPtr m_ClientData;
	}
}
