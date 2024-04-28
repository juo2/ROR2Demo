using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000120 RID: 288
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetBucketIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001E3 RID: 483
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0000908F File Offset: 0x0000728F
		public string BucketId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_BucketId, value);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000909E File Offset: 0x0000729E
		public void Set(SessionModificationSetBucketIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.BucketId = other.BucketId;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000090B6 File Offset: 0x000072B6
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetBucketIdOptions);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000090C4 File Offset: 0x000072C4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_BucketId);
		}

		// Token: 0x040003F5 RID: 1013
		private int m_ApiVersion;

		// Token: 0x040003F6 RID: 1014
		private IntPtr m_BucketId;
	}
}
