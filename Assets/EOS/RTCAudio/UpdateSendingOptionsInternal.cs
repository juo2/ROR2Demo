using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AF RID: 431
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002EB RID: 747
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0000C92F File Offset: 0x0000AB2F
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170002EC RID: 748
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0000C93E File Offset: 0x0000AB3E
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x170002ED RID: 749
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0000C94D File Offset: 0x0000AB4D
		public RTCAudioStatus AudioStatus
		{
			set
			{
				this.m_AudioStatus = value;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0000C956 File Offset: 0x0000AB56
		public void Set(UpdateSendingOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.AudioStatus = other.AudioStatus;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0000C986 File Offset: 0x0000AB86
		public void Set(object other)
		{
			this.Set(other as UpdateSendingOptions);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0000C994 File Offset: 0x0000AB94
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x0400057A RID: 1402
		private int m_ApiVersion;

		// Token: 0x0400057B RID: 1403
		private IntPtr m_LocalUserId;

		// Token: 0x0400057C RID: 1404
		private IntPtr m_RoomName;

		// Token: 0x0400057D RID: 1405
		private RTCAudioStatus m_AudioStatus;
	}
}
