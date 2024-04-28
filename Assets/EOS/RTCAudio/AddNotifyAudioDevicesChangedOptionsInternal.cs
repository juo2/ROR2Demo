using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioDevicesChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0000AD63 File Offset: 0x00008F63
		public void Set(AddNotifyAudioDevicesChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000AD6F File Offset: 0x00008F6F
		public void Set(object other)
		{
			this.Set(other as AddNotifyAudioDevicesChangedOptions);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040004B9 RID: 1209
		private int m_ApiVersion;
	}
}
