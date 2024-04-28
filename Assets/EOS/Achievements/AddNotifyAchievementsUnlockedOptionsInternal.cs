using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005F3 RID: 1523
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600253F RID: 9535 RVA: 0x00027B14 File Offset: 0x00025D14
		public void Set(AddNotifyAchievementsUnlockedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00027B20 File Offset: 0x00025D20
		public void Set(object other)
		{
			this.Set(other as AddNotifyAchievementsUnlockedOptions);
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040011AE RID: 4526
		private int m_ApiVersion;
	}
}
