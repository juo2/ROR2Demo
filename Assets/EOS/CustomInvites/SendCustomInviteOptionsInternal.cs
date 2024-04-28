using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A7 RID: 1191
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendCustomInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008CF RID: 2255
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0001E8BC File Offset: 0x0001CABC
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0001E8CB File Offset: 0x0001CACB
		public ProductUserId[] TargetUserIds
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_TargetUserIds, value, out this.m_TargetUserIdsCount);
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		public void Set(SendCustomInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserIds = other.TargetUserIds;
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0001E904 File Offset: 0x0001CB04
		public void Set(object other)
		{
			this.Set(other as SendCustomInviteOptions);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0001E912 File Offset: 0x0001CB12
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserIds);
		}

		// Token: 0x04000D7C RID: 3452
		private int m_ApiVersion;

		// Token: 0x04000D7D RID: 3453
		private IntPtr m_LocalUserId;

		// Token: 0x04000D7E RID: 3454
		private IntPtr m_TargetUserIds;

		// Token: 0x04000D7F RID: 3455
		private uint m_TargetUserIdsCount;
	}
}
