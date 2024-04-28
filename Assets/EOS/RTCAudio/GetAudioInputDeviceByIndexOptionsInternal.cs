using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000181 RID: 385
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioInputDeviceByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002A0 RID: 672
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0000B95D File Offset: 0x00009B5D
		public uint DeviceInfoIndex
		{
			set
			{
				this.m_DeviceInfoIndex = value;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000B966 File Offset: 0x00009B66
		public void Set(GetAudioInputDeviceByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DeviceInfoIndex = other.DeviceInfoIndex;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000B97E File Offset: 0x00009B7E
		public void Set(object other)
		{
			this.Set(other as GetAudioInputDeviceByIndexOptions);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000504 RID: 1284
		private int m_ApiVersion;

		// Token: 0x04000505 RID: 1285
		private uint m_DeviceInfoIndex;
	}
}
