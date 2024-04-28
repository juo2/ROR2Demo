using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E8 RID: 1000
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPermissionsCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000729 RID: 1833
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x00019861 File Offset: 0x00017A61
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00019870 File Offset: 0x00017A70
		public void Set(GetPermissionsCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00019888 File Offset: 0x00017A88
		public void Set(object other)
		{
			this.Set(other as GetPermissionsCountOptions);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00019896 File Offset: 0x00017A96
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B55 RID: 2901
		private int m_ApiVersion;

		// Token: 0x04000B56 RID: 2902
		private IntPtr m_LocalUserId;
	}
}
