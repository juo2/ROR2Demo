using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005F9 RID: 1529
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BAB RID: 2987
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x00027BAD File Offset: 0x00025DAD
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00027BB6 File Offset: 0x00025DB6
		public void Set(CopyAchievementDefinitionByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AchievementIndex = other.AchievementIndex;
			}
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00027BCE File Offset: 0x00025DCE
		public void Set(object other)
		{
			this.Set(other as CopyAchievementDefinitionByIndexOptions);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040011B4 RID: 4532
		private int m_ApiVersion;

		// Token: 0x040011B5 RID: 4533
		private uint m_AchievementIndex;
	}
}
