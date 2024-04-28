using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000024 RID: 36
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00003C34 File Offset: 0x00001E34
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00003C43 File Offset: 0x00001E43
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00003C52 File Offset: 0x00001E52
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00003C5B File Offset: 0x00001E5B
		public void Set(CopyExternalUserInfoByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00003C8B File Offset: 0x00001E8B
		public void Set(object other)
		{
			this.Set(other as CopyExternalUserInfoByIndexOptions);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00003C99 File Offset: 0x00001E99
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000130 RID: 304
		private int m_ApiVersion;

		// Token: 0x04000131 RID: 305
		private IntPtr m_LocalUserId;

		// Token: 0x04000132 RID: 306
		private IntPtr m_TargetUserId;

		// Token: 0x04000133 RID: 307
		private uint m_Index;
	}
}
