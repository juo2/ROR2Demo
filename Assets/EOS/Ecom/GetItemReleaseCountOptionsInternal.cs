using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000460 RID: 1120
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemReleaseCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000849 RID: 2121
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0001D275 File Offset: 0x0001B475
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x0001D284 File Offset: 0x0001B484
		public string ItemId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ItemId, value);
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0001D293 File Offset: 0x0001B493
		public void Set(GetItemReleaseCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ItemId = other.ItemId;
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0001D2B7 File Offset: 0x0001B4B7
		public void Set(object other)
		{
			this.Set(other as GetItemReleaseCountOptions);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0001D2C5 File Offset: 0x0001B4C5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ItemId);
		}

		// Token: 0x04000CDC RID: 3292
		private int m_ApiVersion;

		// Token: 0x04000CDD RID: 3293
		private IntPtr m_LocalUserId;

		// Token: 0x04000CDE RID: 3294
		private IntPtr m_ItemId;
	}
}
