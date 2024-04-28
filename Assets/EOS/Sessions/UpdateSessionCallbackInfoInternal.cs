using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014A RID: 330
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000A3A1 File Offset: 0x000085A1
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0000A3AC File Offset: 0x000085AC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0000A3C8 File Offset: 0x000085C8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public string SessionName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SessionName, out result);
				return result;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0000A3EC File Offset: 0x000085EC
		public string SessionId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SessionId, out result);
				return result;
			}
		}

		// Token: 0x04000466 RID: 1126
		private Result m_ResultCode;

		// Token: 0x04000467 RID: 1127
		private IntPtr m_ClientData;

		// Token: 0x04000468 RID: 1128
		private IntPtr m_SessionName;

		// Token: 0x04000469 RID: 1129
		private IntPtr m_SessionId;
	}
}
