using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000442 RID: 1090
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByNameAndIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000802 RID: 2050
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x0001C10A File Offset: 0x0001A30A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0001C119 File Offset: 0x0001A319
		public string EntitlementName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0001C128 File Offset: 0x0001A328
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0001C131 File Offset: 0x0001A331
		public void Set(CopyEntitlementByNameAndIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementName = other.EntitlementName;
				this.Index = other.Index;
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0001C161 File Offset: 0x0001A361
		public void Set(object other)
		{
			this.Set(other as CopyEntitlementByNameAndIndexOptions);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0001C16F File Offset: 0x0001A36F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EntitlementName);
		}

		// Token: 0x04000C59 RID: 3161
		private int m_ApiVersion;

		// Token: 0x04000C5A RID: 3162
		private IntPtr m_LocalUserId;

		// Token: 0x04000C5B RID: 3163
		private IntPtr m_EntitlementName;

		// Token: 0x04000C5C RID: 3164
		private uint m_Index;
	}
}
