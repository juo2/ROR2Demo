using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200040C RID: 1036
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0001A511 File Offset: 0x00018711
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0001A51C File Offset: 0x0001871C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0001A538 File Offset: 0x00018738
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0001A540 File Offset: 0x00018740
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0001A55C File Offset: 0x0001875C
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x04000BA3 RID: 2979
		private Result m_ResultCode;

		// Token: 0x04000BA4 RID: 2980
		private IntPtr m_ClientData;

		// Token: 0x04000BA5 RID: 2981
		private IntPtr m_LocalUserId;

		// Token: 0x04000BA6 RID: 2982
		private IntPtr m_TargetUserId;
	}
}
