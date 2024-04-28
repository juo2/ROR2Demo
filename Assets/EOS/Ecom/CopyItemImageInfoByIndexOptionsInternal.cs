using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000446 RID: 1094
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemImageInfoByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700080C RID: 2060
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0001C248 File Offset: 0x0001A448
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x0001C257 File Offset: 0x0001A457
		public string ItemId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0001C266 File Offset: 0x0001A466
		public uint ImageInfoIndex
		{
			set
			{
				this.m_ImageInfoIndex = value;
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0001C26F File Offset: 0x0001A46F
		public void Set(CopyItemImageInfoByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ItemId = other.ItemId;
				this.ImageInfoIndex = other.ImageInfoIndex;
			}
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0001C29F File Offset: 0x0001A49F
		public void Set(object other)
		{
			this.Set(other as CopyItemImageInfoByIndexOptions);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0001C2AD File Offset: 0x0001A4AD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
		}

		// Token: 0x04000C65 RID: 3173
		private int m_ApiVersion;

		// Token: 0x04000C66 RID: 3174
		private IntPtr m_LocalUserId;

		// Token: 0x04000C67 RID: 3175
		private IntPtr m_ItemId;

		// Token: 0x04000C68 RID: 3176
		private uint m_ImageInfoIndex;
	}
}
