using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AD RID: 429
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0000C88E File Offset: 0x0000AA8E
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0000C898 File Offset: 0x0000AA98
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public RTCAudioStatus AudioStatus
		{
			get
			{
				return this.m_AudioStatus;
			}
		}

		// Token: 0x04000572 RID: 1394
		private Result m_ResultCode;

		// Token: 0x04000573 RID: 1395
		private IntPtr m_ClientData;

		// Token: 0x04000574 RID: 1396
		private IntPtr m_LocalUserId;

		// Token: 0x04000575 RID: 1397
		private IntPtr m_RoomName;

		// Token: 0x04000576 RID: 1398
		private RTCAudioStatus m_AudioStatus;
	}
}
