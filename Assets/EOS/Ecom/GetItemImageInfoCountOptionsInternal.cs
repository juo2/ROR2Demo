using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200045E RID: 1118
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemImageInfoCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000845 RID: 2117
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0001D1E9 File Offset: 0x0001B3E9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public string ItemId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0001D207 File Offset: 0x0001B407
		public void Set(GetItemImageInfoCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ItemId = other.ItemId;
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0001D22B File Offset: 0x0001B42B
		public void Set(object other)
		{
			this.Set(other as GetItemImageInfoCountOptions);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0001D239 File Offset: 0x0001B439
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
		}

		// Token: 0x04000CD7 RID: 3287
		private int m_ApiVersion;

		// Token: 0x04000CD8 RID: 3288
		private IntPtr m_LocalUserId;

		// Token: 0x04000CD9 RID: 3289
		private IntPtr m_ItemId;
	}
}
