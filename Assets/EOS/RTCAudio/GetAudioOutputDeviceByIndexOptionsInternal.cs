using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000185 RID: 389
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioOutputDeviceByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002A2 RID: 674
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public uint DeviceInfoIndex
		{
			set
			{
				this.m_DeviceInfoIndex = value;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public void Set(GetAudioOutputDeviceByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DeviceInfoIndex = other.DeviceInfoIndex;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public void Set(object other)
		{
			this.Set(other as GetAudioOutputDeviceByIndexOptions);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000508 RID: 1288
		private int m_ApiVersion;

		// Token: 0x04000509 RID: 1289
		private uint m_DeviceInfoIndex;
	}
}
