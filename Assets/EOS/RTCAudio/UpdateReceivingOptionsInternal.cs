using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AB RID: 427
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002D9 RID: 729
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170002DA RID: 730
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0000C6FB File Offset: 0x0000A8FB
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0000C70A File Offset: 0x0000A90A
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParticipantId, value);
			}
		}

		// Token: 0x170002DC RID: 732
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0000C719 File Offset: 0x0000A919
		public bool AudioEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AudioEnabled, value);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0000C728 File Offset: 0x0000A928
		public void Set(UpdateReceivingOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.ParticipantId = other.ParticipantId;
				this.AudioEnabled = other.AudioEnabled;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0000C764 File Offset: 0x0000A964
		public void Set(object other)
		{
			this.Set(other as UpdateReceivingOptions);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0000C772 File Offset: 0x0000A972
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_ParticipantId);
		}

		// Token: 0x04000568 RID: 1384
		private int m_ApiVersion;

		// Token: 0x04000569 RID: 1385
		private IntPtr m_LocalUserId;

		// Token: 0x0400056A RID: 1386
		private IntPtr m_RoomName;

		// Token: 0x0400056B RID: 1387
		private IntPtr m_ParticipantId;

		// Token: 0x0400056C RID: 1388
		private int m_AudioEnabled;
	}
}
