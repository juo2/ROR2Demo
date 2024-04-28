using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000406 RID: 1030
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPermissionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000754 RID: 1876
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0001A252 File Offset: 0x00018452
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0001A261 File Offset: 0x00018461
		public string[] PermissionKeys
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_PermissionKeys, value, out this.m_PermissionKeyCount, true);
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0001A277 File Offset: 0x00018477
		public void Set(RequestPermissionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.PermissionKeys = other.PermissionKeys;
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0001A29B File Offset: 0x0001849B
		public void Set(object other)
		{
			this.Set(other as RequestPermissionsOptions);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0001A2A9 File Offset: 0x000184A9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_PermissionKeys);
		}

		// Token: 0x04000B90 RID: 2960
		private int m_ApiVersion;

		// Token: 0x04000B91 RID: 2961
		private IntPtr m_LocalUserId;

		// Token: 0x04000B92 RID: 2962
		private uint m_PermissionKeyCount;

		// Token: 0x04000B93 RID: 2963
		private IntPtr m_PermissionKeys;
	}
}
