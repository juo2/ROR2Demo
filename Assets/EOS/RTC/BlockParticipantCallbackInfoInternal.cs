using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CE RID: 462
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BlockParticipantCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0000D503 File Offset: 0x0000B703
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0000D50C File Offset: 0x0000B70C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0000D528 File Offset: 0x0000B728
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0000D530 File Offset: 0x0000B730
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0000D54C File Offset: 0x0000B74C
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0000D568 File Offset: 0x0000B768
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0000D584 File Offset: 0x0000B784
		public bool Blocked
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_Blocked, out result);
				return result;
			}
		}

		// Token: 0x040005CD RID: 1485
		private Result m_ResultCode;

		// Token: 0x040005CE RID: 1486
		private IntPtr m_ClientData;

		// Token: 0x040005CF RID: 1487
		private IntPtr m_LocalUserId;

		// Token: 0x040005D0 RID: 1488
		private IntPtr m_RoomName;

		// Token: 0x040005D1 RID: 1489
		private IntPtr m_ParticipantId;

		// Token: 0x040005D2 RID: 1490
		private int m_Blocked;
	}
}
