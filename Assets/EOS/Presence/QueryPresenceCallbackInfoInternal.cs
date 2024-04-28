using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022C RID: 556
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0000F9E5 File Offset: 0x0000DBE5
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0000FA30 File Offset: 0x0000DC30
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x040006CB RID: 1739
		private Result m_ResultCode;

		// Token: 0x040006CC RID: 1740
		private IntPtr m_ClientData;

		// Token: 0x040006CD RID: 1741
		private IntPtr m_LocalUserId;

		// Token: 0x040006CE RID: 1742
		private IntPtr m_TargetUserId;
	}
}
