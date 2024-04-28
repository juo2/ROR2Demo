using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D7 RID: 471
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinRoomOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0000D988 File Offset: 0x0000BB88
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0000D997 File Offset: 0x0000BB97
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		public string ClientBaseUrl
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ClientBaseUrl, value);
			}
		}

		// Token: 0x1700035D RID: 861
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0000D9B5 File Offset: 0x0000BBB5
		public string ParticipantToken
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParticipantToken, value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParticipantId, value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
		public JoinRoomFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		public bool ManualAudioInputEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ManualAudioInputEnabled, value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0000D9EB File Offset: 0x0000BBEB
		public bool ManualAudioOutputEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ManualAudioOutputEnabled, value);
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public void Set(JoinRoomOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.ClientBaseUrl = other.ClientBaseUrl;
				this.ParticipantToken = other.ParticipantToken;
				this.ParticipantId = other.ParticipantId;
				this.Flags = other.Flags;
				this.ManualAudioInputEnabled = other.ManualAudioInputEnabled;
				this.ManualAudioOutputEnabled = other.ManualAudioOutputEnabled;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0000DA73 File Offset: 0x0000BC73
		public void Set(object other)
		{
			this.Set(other as JoinRoomOptions);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0000DA81 File Offset: 0x0000BC81
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_ClientBaseUrl);
			Helper.TryMarshalDispose(ref this.m_ParticipantToken);
			Helper.TryMarshalDispose(ref this.m_ParticipantId);
		}

		// Token: 0x040005F7 RID: 1527
		private int m_ApiVersion;

		// Token: 0x040005F8 RID: 1528
		private IntPtr m_LocalUserId;

		// Token: 0x040005F9 RID: 1529
		private IntPtr m_RoomName;

		// Token: 0x040005FA RID: 1530
		private IntPtr m_ClientBaseUrl;

		// Token: 0x040005FB RID: 1531
		private IntPtr m_ParticipantToken;

		// Token: 0x040005FC RID: 1532
		private IntPtr m_ParticipantId;

		// Token: 0x040005FD RID: 1533
		private JoinRoomFlags m_Flags;

		// Token: 0x040005FE RID: 1534
		private int m_ManualAudioInputEnabled;

		// Token: 0x040005FF RID: 1535
		private int m_ManualAudioOutputEnabled;
	}
}
