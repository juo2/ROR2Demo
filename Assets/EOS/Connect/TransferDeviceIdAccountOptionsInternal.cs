using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000500 RID: 1280
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000964 RID: 2404
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x0002070B File Offset: 0x0001E90B
		public ProductUserId PrimaryLocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PrimaryLocalUserId, value);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0002071A File Offset: 0x0001E91A
		public ProductUserId LocalDeviceUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalDeviceUserId, value);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x00020729 File Offset: 0x0001E929
		public ProductUserId ProductUserIdToPreserve
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductUserIdToPreserve, value);
			}
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00020738 File Offset: 0x0001E938
		public void Set(TransferDeviceIdAccountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PrimaryLocalUserId = other.PrimaryLocalUserId;
				this.LocalDeviceUserId = other.LocalDeviceUserId;
				this.ProductUserIdToPreserve = other.ProductUserIdToPreserve;
			}
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00020768 File Offset: 0x0001E968
		public void Set(object other)
		{
			this.Set(other as TransferDeviceIdAccountOptions);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00020776 File Offset: 0x0001E976
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PrimaryLocalUserId);
			Helper.TryMarshalDispose(ref this.m_LocalDeviceUserId);
			Helper.TryMarshalDispose(ref this.m_ProductUserIdToPreserve);
		}

		// Token: 0x04000E3F RID: 3647
		private int m_ApiVersion;

		// Token: 0x04000E40 RID: 3648
		private IntPtr m_PrimaryLocalUserId;

		// Token: 0x04000E41 RID: 3649
		private IntPtr m_LocalDeviceUserId;

		// Token: 0x04000E42 RID: 3650
		private IntPtr m_ProductUserIdToPreserve;
	}
}
