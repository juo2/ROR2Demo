using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005B RID: 91
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetDisplayPreferenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000083 RID: 131
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00004E4D File Offset: 0x0000304D
		public NotificationLocation NotificationLocation
		{
			set
			{
				this.m_NotificationLocation = value;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00004E56 File Offset: 0x00003056
		public void Set(SetDisplayPreferenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.NotificationLocation = other.NotificationLocation;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00004E6E File Offset: 0x0000306E
		public void Set(object other)
		{
			this.Set(other as SetDisplayPreferenceOptions);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400021F RID: 543
		private int m_ApiVersion;

		// Token: 0x04000220 RID: 544
		private NotificationLocation m_NotificationLocation;
	}
}
