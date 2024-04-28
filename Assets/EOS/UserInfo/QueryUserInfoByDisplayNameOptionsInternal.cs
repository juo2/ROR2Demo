using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000034 RID: 52
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700003C RID: 60
		// (set) Token: 0x0600032E RID: 814 RVA: 0x000040C6 File Offset: 0x000022C6
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000040D5 File Offset: 0x000022D5
		public string DisplayName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000040E4 File Offset: 0x000022E4
		public void Set(QueryUserInfoByDisplayNameOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.DisplayName = other.DisplayName;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00004108 File Offset: 0x00002308
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoByDisplayNameOptions);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00004116 File Offset: 0x00002316
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
		}

		// Token: 0x04000151 RID: 337
		private int m_ApiVersion;

		// Token: 0x04000152 RID: 338
		private IntPtr m_LocalUserId;

		// Token: 0x04000153 RID: 339
		private IntPtr m_DisplayName;
	}
}
