using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000043 RID: 67
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyDisplaySettingsUpdatedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public void Set(AddNotifyDisplaySettingsUpdatedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00004ACE File Offset: 0x00002CCE
		public void Set(object other)
		{
			this.Set(other as AddNotifyDisplaySettingsUpdatedOptions);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400018F RID: 399
		private int m_ApiVersion;
	}
}
