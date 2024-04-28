using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200046A RID: 1130
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ItemOwnershipInternal : ISettable, IDisposable
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0001D518 File Offset: 0x0001B718
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0001D534 File Offset: 0x0001B734
		public string Id
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Id, value);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x0001D543 File Offset: 0x0001B743
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0001D54B File Offset: 0x0001B74B
		public OwnershipStatus OwnershipStatus
		{
			get
			{
				return this.m_OwnershipStatus;
			}
			set
			{
				this.m_OwnershipStatus = value;
			}
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0001D554 File Offset: 0x0001B754
		public void Set(ItemOwnership other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Id = other.Id;
				this.OwnershipStatus = other.OwnershipStatus;
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0001D578 File Offset: 0x0001B778
		public void Set(object other)
		{
			this.Set(other as ItemOwnership);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0001D586 File Offset: 0x0001B786
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Id);
		}

		// Token: 0x04000CF1 RID: 3313
		private int m_ApiVersion;

		// Token: 0x04000CF2 RID: 3314
		private IntPtr m_Id;

		// Token: 0x04000CF3 RID: 3315
		private OwnershipStatus m_OwnershipStatus;
	}
}
