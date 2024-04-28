using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004DC RID: 1244
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x00020244 File Offset: 0x0001E444
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00020260 File Offset: 0x0001E460
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00020268 File Offset: 0x0001E468
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00020284 File Offset: 0x0001E484
		public LoginStatus PreviousStatus
		{
			get
			{
				return this.m_PreviousStatus;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0002028C File Offset: 0x0001E48C
		public LoginStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
		}

		// Token: 0x04000E16 RID: 3606
		private IntPtr m_ClientData;

		// Token: 0x04000E17 RID: 3607
		private IntPtr m_LocalUserId;

		// Token: 0x04000E18 RID: 3608
		private LoginStatus m_PreviousStatus;

		// Token: 0x04000E19 RID: 3609
		private LoginStatus m_CurrentStatus;
	}
}
