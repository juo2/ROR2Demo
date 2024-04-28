using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A9 RID: 425
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0000C60B File Offset: 0x0000A80B
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0000C614 File Offset: 0x0000A814
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0000C630 File Offset: 0x0000A830
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0000C638 File Offset: 0x0000A838
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0000C654 File Offset: 0x0000A854
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0000C670 File Offset: 0x0000A870
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000C68C File Offset: 0x0000A88C
		public bool AudioEnabled
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_AudioEnabled, out result);
				return result;
			}
		}

		// Token: 0x0400055E RID: 1374
		private Result m_ResultCode;

		// Token: 0x0400055F RID: 1375
		private IntPtr m_ClientData;

		// Token: 0x04000560 RID: 1376
		private IntPtr m_LocalUserId;

		// Token: 0x04000561 RID: 1377
		private IntPtr m_RoomName;

		// Token: 0x04000562 RID: 1378
		private IntPtr m_ParticipantId;

		// Token: 0x04000563 RID: 1379
		private int m_AudioEnabled;
	}
}
