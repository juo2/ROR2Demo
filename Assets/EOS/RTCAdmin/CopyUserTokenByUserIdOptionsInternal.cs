using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B3 RID: 435
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserTokenByUserIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002F4 RID: 756
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0000CA36 File Offset: 0x0000AC36
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0000CA45 File Offset: 0x0000AC45
		public uint QueryId
		{
			set
			{
				this.m_QueryId = value;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0000CA4E File Offset: 0x0000AC4E
		public void Set(CopyUserTokenByUserIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.TargetUserId;
				this.QueryId = other.QueryId;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0000CA72 File Offset: 0x0000AC72
		public void Set(object other)
		{
			this.Set(other as CopyUserTokenByUserIdOptions);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000585 RID: 1413
		private int m_ApiVersion;

		// Token: 0x04000586 RID: 1414
		private IntPtr m_TargetUserId;

		// Token: 0x04000587 RID: 1415
		private uint m_QueryId;
	}
}
