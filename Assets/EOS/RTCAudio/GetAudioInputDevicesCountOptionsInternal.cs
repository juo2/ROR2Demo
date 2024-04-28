using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000183 RID: 387
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioInputDevicesCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0000B98C File Offset: 0x00009B8C
		public void Set(GetAudioInputDevicesCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000B998 File Offset: 0x00009B98
		public void Set(object other)
		{
			this.Set(other as GetAudioInputDevicesCountOptions);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000506 RID: 1286
		private int m_ApiVersion;
	}
}
