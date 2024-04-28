using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000084 RID: 132
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000CB RID: 203
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00005B20 File Offset: 0x00003D20
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170000CC RID: 204
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00005B2F File Offset: 0x00003D2F
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00005B3E File Offset: 0x00003D3E
		public void Set(QueryFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00005B62 File Offset: 0x00003D62
		public void Set(object other)
		{
			this.Set(other as QueryFileOptions);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00005B70 File Offset: 0x00003D70
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000278 RID: 632
		private int m_ApiVersion;

		// Token: 0x04000279 RID: 633
		private IntPtr m_LocalUserId;

		// Token: 0x0400027A RID: 634
		private IntPtr m_Filename;
	}
}
