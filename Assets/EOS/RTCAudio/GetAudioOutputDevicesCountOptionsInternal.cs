using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000187 RID: 391
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioOutputDevicesCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0000B9E6 File Offset: 0x00009BE6
		public void Set(GetAudioOutputDevicesCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000B9F2 File Offset: 0x00009BF2
		public void Set(object other)
		{
			this.Set(other as GetAudioOutputDevicesCountOptions);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400050A RID: 1290
		private int m_ApiVersion;
	}
}
