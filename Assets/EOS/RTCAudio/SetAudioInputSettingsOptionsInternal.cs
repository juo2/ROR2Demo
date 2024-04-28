using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetAudioInputSettingsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002BC RID: 700
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0000C350 File Offset: 0x0000A550
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170002BD RID: 701
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0000C35F File Offset: 0x0000A55F
		public string DeviceId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DeviceId, value);
			}
		}

		// Token: 0x170002BE RID: 702
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0000C36E File Offset: 0x0000A56E
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0000C377 File Offset: 0x0000A577
		public bool PlatformAEC
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PlatformAEC, value);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0000C386 File Offset: 0x0000A586
		public void Set(SetAudioInputSettingsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.DeviceId = other.DeviceId;
				this.Volume = other.Volume;
				this.PlatformAEC = other.PlatformAEC;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0000C3C2 File Offset: 0x0000A5C2
		public void Set(object other)
		{
			this.Set(other as SetAudioInputSettingsOptions);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_DeviceId);
		}

		// Token: 0x04000549 RID: 1353
		private int m_ApiVersion;

		// Token: 0x0400054A RID: 1354
		private IntPtr m_LocalUserId;

		// Token: 0x0400054B RID: 1355
		private IntPtr m_DeviceId;

		// Token: 0x0400054C RID: 1356
		private float m_Volume;

		// Token: 0x0400054D RID: 1357
		private int m_PlatformAEC;
	}
}
