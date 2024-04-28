using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000354 RID: 852
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetBucketIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700064F RID: 1615
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x0001725D File Offset: 0x0001545D
		public string BucketId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_BucketId, value);
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0001726C File Offset: 0x0001546C
		public void Set(LobbyModificationSetBucketIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.BucketId = other.BucketId;
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00017284 File Offset: 0x00015484
		public void Set(object other)
		{
			this.Set(other as LobbyModificationSetBucketIdOptions);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00017292 File Offset: 0x00015492
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_BucketId);
		}

		// Token: 0x04000A3C RID: 2620
		private int m_ApiVersion;

		// Token: 0x04000A3D RID: 2621
		private IntPtr m_BucketId;
	}
}
