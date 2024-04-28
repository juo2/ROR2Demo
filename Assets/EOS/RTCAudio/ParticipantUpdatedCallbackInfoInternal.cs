using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000199 RID: 409
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantUpdatedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0000BB28 File Offset: 0x00009D28
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0000BB44 File Offset: 0x00009D44
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0000BB68 File Offset: 0x00009D68
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0000BB84 File Offset: 0x00009D84
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public bool Speaking
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_Speaking, out result);
				return result;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public RTCAudioStatus AudioStatus
		{
			get
			{
				return this.m_AudioStatus;
			}
		}

		// Token: 0x04000511 RID: 1297
		private IntPtr m_ClientData;

		// Token: 0x04000512 RID: 1298
		private IntPtr m_LocalUserId;

		// Token: 0x04000513 RID: 1299
		private IntPtr m_RoomName;

		// Token: 0x04000514 RID: 1300
		private IntPtr m_ParticipantId;

		// Token: 0x04000515 RID: 1301
		private int m_Speaking;

		// Token: 0x04000516 RID: 1302
		private RTCAudioStatus m_AudioStatus;
	}
}
