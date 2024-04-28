using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E9 RID: 489
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantStatusChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0000DF04 File Offset: 0x0000C104
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0000DF20 File Offset: 0x0000C120
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public RTCParticipantStatus ParticipantStatus
		{
			get
			{
				return this.m_ParticipantStatus;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0000DF44 File Offset: 0x0000C144
		public ParticipantMetadata[] ParticipantMetadata
		{
			get
			{
				ParticipantMetadata[] result;
				Helper.TryMarshalGet<ParticipantMetadataInternal, ParticipantMetadata>(this.m_ParticipantMetadata, out result, this.m_ParticipantMetadataCount);
				return result;
			}
		}

		// Token: 0x04000618 RID: 1560
		private IntPtr m_ClientData;

		// Token: 0x04000619 RID: 1561
		private IntPtr m_LocalUserId;

		// Token: 0x0400061A RID: 1562
		private IntPtr m_RoomName;

		// Token: 0x0400061B RID: 1563
		private IntPtr m_ParticipantId;

		// Token: 0x0400061C RID: 1564
		private RTCParticipantStatus m_ParticipantStatus;

		// Token: 0x0400061D RID: 1565
		private uint m_ParticipantMetadataCount;

		// Token: 0x0400061E RID: 1566
		private IntPtr m_ParticipantMetadata;
	}
}
