using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002A RID: 42
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalUserInfoCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700002D RID: 45
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00003EC1 File Offset: 0x000020C1
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00003ED0 File Offset: 0x000020D0
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00003EDF File Offset: 0x000020DF
		public void Set(GetExternalUserInfoCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00003F03 File Offset: 0x00002103
		public void Set(object other)
		{
			this.Set(other as GetExternalUserInfoCountOptions);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00003F11 File Offset: 0x00002111
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000142 RID: 322
		private int m_ApiVersion;

		// Token: 0x04000143 RID: 323
		private IntPtr m_LocalUserId;

		// Token: 0x04000144 RID: 324
		private IntPtr m_TargetUserId;
	}
}
