using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B1 RID: 433
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserTokenByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002F0 RID: 752
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public uint UserTokenIndex
		{
			set
			{
				this.m_UserTokenIndex = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0000C9D9 File Offset: 0x0000ABD9
		public uint QueryId
		{
			set
			{
				this.m_QueryId = value;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0000C9E2 File Offset: 0x0000ABE2
		public void Set(CopyUserTokenByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserTokenIndex = other.UserTokenIndex;
				this.QueryId = other.QueryId;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0000CA06 File Offset: 0x0000AC06
		public void Set(object other)
		{
			this.Set(other as CopyUserTokenByIndexOptions);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000580 RID: 1408
		private int m_ApiVersion;

		// Token: 0x04000581 RID: 1409
		private uint m_UserTokenIndex;

		// Token: 0x04000582 RID: 1410
		private uint m_QueryId;
	}
}
