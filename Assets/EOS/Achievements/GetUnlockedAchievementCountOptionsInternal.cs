using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200060F RID: 1551
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetUnlockedAchievementCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000BF1 RID: 3057
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x000288E4 File Offset: 0x00026AE4
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000288F3 File Offset: 0x00026AF3
		public void Set(GetUnlockedAchievementCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0002890B File Offset: 0x00026B0B
		public void Set(object other)
		{
			this.Set(other as GetUnlockedAchievementCountOptions);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00028919 File Offset: 0x00026B19
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x04001207 RID: 4615
		private int m_ApiVersion;

		// Token: 0x04001208 RID: 4616
		private IntPtr m_UserId;
	}
}
