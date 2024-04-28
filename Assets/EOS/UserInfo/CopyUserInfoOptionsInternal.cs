using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000026 RID: 38
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000023 RID: 35
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00003CF3 File Offset: 0x00001EF3
		public void Set(CopyUserInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00003D17 File Offset: 0x00001F17
		public void Set(object other)
		{
			this.Set(other as CopyUserInfoOptions);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00003D25 File Offset: 0x00001F25
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000136 RID: 310
		private int m_ApiVersion;

		// Token: 0x04000137 RID: 311
		private IntPtr m_LocalUserId;

		// Token: 0x04000138 RID: 312
		private IntPtr m_TargetUserId;
	}
}
