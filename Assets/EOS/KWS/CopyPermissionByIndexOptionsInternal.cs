using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E0 RID: 992
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPermissionByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000711 RID: 1809
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x00019530 File Offset: 0x00017730
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x0001953F File Offset: 0x0001773F
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00019548 File Offset: 0x00017748
		public void Set(CopyPermissionByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0001956C File Offset: 0x0001776C
		public void Set(object other)
		{
			this.Set(other as CopyPermissionByIndexOptions);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0001957A File Offset: 0x0001777A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B3B RID: 2875
		private int m_ApiVersion;

		// Token: 0x04000B3C RID: 2876
		private IntPtr m_LocalUserId;

		// Token: 0x04000B3D RID: 2877
		private uint m_Index;
	}
}
