using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A2 RID: 930
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0001801C File Offset: 0x0001621C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00018024 File Offset: 0x00016224
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00018040 File Offset: 0x00016240
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00018048 File Offset: 0x00016248
		public string InviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_InviteId, out result);
				return result;
			}
		}

		// Token: 0x04000AA1 RID: 2721
		private Result m_ResultCode;

		// Token: 0x04000AA2 RID: 2722
		private IntPtr m_ClientData;

		// Token: 0x04000AA3 RID: 2723
		private IntPtr m_InviteId;
	}
}
