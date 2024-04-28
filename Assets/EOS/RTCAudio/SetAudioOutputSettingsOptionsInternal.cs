using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A5 RID: 421
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetAudioOutputSettingsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002C3 RID: 707
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0000C41D File Offset: 0x0000A61D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0000C42C File Offset: 0x0000A62C
		public string DeviceId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DeviceId, value);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0000C43B File Offset: 0x0000A63B
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000C444 File Offset: 0x0000A644
		public void Set(SetAudioOutputSettingsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.DeviceId = other.DeviceId;
				this.Volume = other.Volume;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0000C474 File Offset: 0x0000A674
		public void Set(object other)
		{
			this.Set(other as SetAudioOutputSettingsOptions);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0000C482 File Offset: 0x0000A682
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_DeviceId);
		}

		// Token: 0x04000551 RID: 1361
		private int m_ApiVersion;

		// Token: 0x04000552 RID: 1362
		private IntPtr m_LocalUserId;

		// Token: 0x04000553 RID: 1363
		private IntPtr m_DeviceId;

		// Token: 0x04000554 RID: 1364
		private float m_Volume;
	}
}
