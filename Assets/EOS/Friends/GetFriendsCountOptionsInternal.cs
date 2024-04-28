using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000416 RID: 1046
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000773 RID: 1907
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x0001A9AF File Offset: 0x00018BAF
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0001A9BE File Offset: 0x00018BBE
		public void Set(GetFriendsCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0001A9D6 File Offset: 0x00018BD6
		public void Set(object other)
		{
			this.Set(other as GetFriendsCountOptions);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000BC0 RID: 3008
		private int m_ApiVersion;

		// Token: 0x04000BC1 RID: 3009
		private IntPtr m_LocalUserId;
	}
}
