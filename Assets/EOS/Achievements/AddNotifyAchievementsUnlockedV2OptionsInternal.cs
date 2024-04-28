using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005F5 RID: 1525
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedV2OptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06002543 RID: 9539 RVA: 0x00027B2E File Offset: 0x00025D2E
		public void Set(AddNotifyAchievementsUnlockedV2Options other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00027B3A File Offset: 0x00025D3A
		public void Set(object other)
		{
			this.Set(other as AddNotifyAchievementsUnlockedV2Options);
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040011AF RID: 4527
		private int m_ApiVersion;
	}
}
