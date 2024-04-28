using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E6 RID: 998
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPermissionByKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000726 RID: 1830
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x000197E6 File Offset: 0x000179E6
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (set) Token: 0x0600182A RID: 6186 RVA: 0x000197F5 File Offset: 0x000179F5
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00019804 File Offset: 0x00017A04
		public void Set(GetPermissionByKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Key = other.Key;
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00019828 File Offset: 0x00017A28
		public void Set(object other)
		{
			this.Set(other as GetPermissionByKeyOptions);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00019836 File Offset: 0x00017A36
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x04000B51 RID: 2897
		private int m_ApiVersion;

		// Token: 0x04000B52 RID: 2898
		private IntPtr m_LocalUserId;

		// Token: 0x04000B53 RID: 2899
		private IntPtr m_Key;
	}
}
