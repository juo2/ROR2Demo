using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005F7 RID: 1527
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByAchievementIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BA9 RID: 2985
		// (set) Token: 0x06002549 RID: 9545 RVA: 0x00027B59 File Offset: 0x00025D59
		public string AchievementId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00027B68 File Offset: 0x00025D68
		public void Set(CopyAchievementDefinitionByAchievementIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.AchievementId;
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00027B80 File Offset: 0x00025D80
		public void Set(object other)
		{
			this.Set(other as CopyAchievementDefinitionByAchievementIdOptions);
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00027B8E File Offset: 0x00025D8E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
		}

		// Token: 0x040011B1 RID: 4529
		private int m_ApiVersion;

		// Token: 0x040011B2 RID: 4530
		private IntPtr m_AchievementId;
	}
}
