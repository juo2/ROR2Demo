using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000414 RID: 1044
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendAtIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000770 RID: 1904
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x0001A946 File Offset: 0x00018B46
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0001A955 File Offset: 0x00018B55
		public int Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0001A95E File Offset: 0x00018B5E
		public void Set(GetFriendAtIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0001A982 File Offset: 0x00018B82
		public void Set(object other)
		{
			this.Set(other as GetFriendAtIndexOptions);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0001A990 File Offset: 0x00018B90
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000BBC RID: 3004
		private int m_ApiVersion;

		// Token: 0x04000BBD RID: 3005
		private IntPtr m_LocalUserId;

		// Token: 0x04000BBE RID: 3006
		private int m_Index;
	}
}
