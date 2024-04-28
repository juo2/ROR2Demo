using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005FF RID: 1535
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByAchievementIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BB3 RID: 2995
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x00027CA3 File Offset: 0x00025EA3
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x00027CB2 File Offset: 0x00025EB2
		public string AchievementId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (set) Token: 0x0600256B RID: 9579 RVA: 0x00027CC1 File Offset: 0x00025EC1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00027CD0 File Offset: 0x00025ED0
		public void Set(CopyPlayerAchievementByAchievementIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.TargetUserId;
				this.AchievementId = other.AchievementId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00027D00 File Offset: 0x00025F00
		public void Set(object other)
		{
			this.Set(other as CopyPlayerAchievementByAchievementIdOptions);
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00027D0E File Offset: 0x00025F0E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_AchievementId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x040011BF RID: 4543
		private int m_ApiVersion;

		// Token: 0x040011C0 RID: 4544
		private IntPtr m_TargetUserId;

		// Token: 0x040011C1 RID: 4545
		private IntPtr m_AchievementId;

		// Token: 0x040011C2 RID: 4546
		private IntPtr m_LocalUserId;
	}
}
