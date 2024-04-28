using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C6 RID: 454
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetParticipantHardMuteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700031C RID: 796
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0000D12F File Offset: 0x0000B32F
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x1700031D RID: 797
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0000D13E File Offset: 0x0000B33E
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x1700031E RID: 798
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0000D14D File Offset: 0x0000B34D
		public bool Mute
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Mute, value);
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0000D15C File Offset: 0x0000B35C
		public void Set(SetParticipantHardMuteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.RoomName = other.RoomName;
				this.TargetUserId = other.TargetUserId;
				this.Mute = other.Mute;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0000D18C File Offset: 0x0000B38C
		public void Set(object other)
		{
			this.Set(other as SetParticipantHardMuteOptions);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0000D19A File Offset: 0x0000B39A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040005B4 RID: 1460
		private int m_ApiVersion;

		// Token: 0x040005B5 RID: 1461
		private IntPtr m_RoomName;

		// Token: 0x040005B6 RID: 1462
		private IntPtr m_TargetUserId;

		// Token: 0x040005B7 RID: 1463
		private int m_Mute;
	}
}
