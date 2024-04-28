using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000230 RID: 560
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x040006D7 RID: 1751
		private Result m_ResultCode;

		// Token: 0x040006D8 RID: 1752
		private IntPtr m_ClientData;

		// Token: 0x040006D9 RID: 1753
		private IntPtr m_LocalUserId;
	}
}
