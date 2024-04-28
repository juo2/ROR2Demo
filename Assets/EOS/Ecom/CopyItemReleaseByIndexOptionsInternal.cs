using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000448 RID: 1096
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemReleaseByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000812 RID: 2066
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x0001C2FA File Offset: 0x0001A4FA
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0001C309 File Offset: 0x0001A509
		public string ItemId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (set) Token: 0x06001ABF RID: 6847 RVA: 0x0001C318 File Offset: 0x0001A518
		public uint ReleaseIndex
		{
			set
			{
				this.m_ReleaseIndex = value;
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0001C321 File Offset: 0x0001A521
		public void Set(CopyItemReleaseByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ItemId = other.ItemId;
				this.ReleaseIndex = other.ReleaseIndex;
			}
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0001C351 File Offset: 0x0001A551
		public void Set(object other)
		{
			this.Set(other as CopyItemReleaseByIndexOptions);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0001C35F File Offset: 0x0001A55F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
		}

		// Token: 0x04000C6C RID: 3180
		private int m_ApiVersion;

		// Token: 0x04000C6D RID: 3181
		private IntPtr m_LocalUserId;

		// Token: 0x04000C6E RID: 3182
		private IntPtr m_ItemId;

		// Token: 0x04000C6F RID: 3183
		private uint m_ReleaseIndex;
	}
}
