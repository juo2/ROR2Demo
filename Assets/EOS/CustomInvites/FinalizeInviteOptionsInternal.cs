using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000499 RID: 1177
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FinalizeInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008AA RID: 2218
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0001E388 File Offset: 0x0001C588
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x0001E397 File Offset: 0x0001C597
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0001E3A6 File Offset: 0x0001C5A6
		public string CustomInviteId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_CustomInviteId, value);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x0001E3B5 File Offset: 0x0001C5B5
		public Result ProcessingResult
		{
			set
			{
				this.m_ProcessingResult = value;
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0001E3BE File Offset: 0x0001C5BE
		public void Set(FinalizeInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.LocalUserId = other.LocalUserId;
				this.CustomInviteId = other.CustomInviteId;
				this.ProcessingResult = other.ProcessingResult;
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0001E3FA File Offset: 0x0001C5FA
		public void Set(object other)
		{
			this.Set(other as FinalizeInviteOptions);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0001E408 File Offset: 0x0001C608
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_CustomInviteId);
		}

		// Token: 0x04000D58 RID: 3416
		private int m_ApiVersion;

		// Token: 0x04000D59 RID: 3417
		private IntPtr m_TargetUserId;

		// Token: 0x04000D5A RID: 3418
		private IntPtr m_LocalUserId;

		// Token: 0x04000D5B RID: 3419
		private IntPtr m_CustomInviteId;

		// Token: 0x04000D5C RID: 3420
		private Result m_ProcessingResult;
	}
}
