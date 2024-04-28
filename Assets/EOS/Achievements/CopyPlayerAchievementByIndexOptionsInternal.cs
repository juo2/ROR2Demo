using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000601 RID: 1537
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BB9 RID: 3001
		// (set) Token: 0x06002576 RID: 9590 RVA: 0x00027D67 File Offset: 0x00025F67
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x00027D76 File Offset: 0x00025F76
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x00027D7F File Offset: 0x00025F7F
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00027D8E File Offset: 0x00025F8E
		public void Set(CopyPlayerAchievementByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.TargetUserId;
				this.AchievementIndex = other.AchievementIndex;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00027DBE File Offset: 0x00025FBE
		public void Set(object other)
		{
			this.Set(other as CopyPlayerAchievementByIndexOptions);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00027DCC File Offset: 0x00025FCC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x040011C6 RID: 4550
		private int m_ApiVersion;

		// Token: 0x040011C7 RID: 4551
		private IntPtr m_TargetUserId;

		// Token: 0x040011C8 RID: 4552
		private uint m_AchievementIndex;

		// Token: 0x040011C9 RID: 4553
		private IntPtr m_LocalUserId;
	}
}
