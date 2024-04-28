using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005FD RID: 1533
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BAF RID: 2991
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x00027C41 File Offset: 0x00025E41
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00027C4A File Offset: 0x00025E4A
		public void Set(CopyAchievementDefinitionV2ByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.AchievementIndex = other.AchievementIndex;
			}
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00027C62 File Offset: 0x00025E62
		public void Set(object other)
		{
			this.Set(other as CopyAchievementDefinitionV2ByIndexOptions);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040011BA RID: 4538
		private int m_ApiVersion;

		// Token: 0x040011BB RID: 4539
		private uint m_AchievementIndex;
	}
}
