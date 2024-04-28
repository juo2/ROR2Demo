using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005FB RID: 1531
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByAchievementIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BAD RID: 2989
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x00027BED File Offset: 0x00025DED
		public string AchievementId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00027BFC File Offset: 0x00025DFC
		public void Set(CopyAchievementDefinitionV2ByAchievementIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.AchievementId;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00027C14 File Offset: 0x00025E14
		public void Set(object other)
		{
			this.Set(other as CopyAchievementDefinitionV2ByAchievementIdOptions);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00027C22 File Offset: 0x00025E22
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementId);
		}

		// Token: 0x040011B7 RID: 4535
		private int m_ApiVersion;

		// Token: 0x040011B8 RID: 4536
		private IntPtr m_AchievementId;
	}
}
