using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000093 RID: 147
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000F6 RID: 246
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00006492 File Offset: 0x00004692
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000064A1 File Offset: 0x000046A1
		public void Set(GetStatCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000064B9 File Offset: 0x000046B9
		public void Set(object other)
		{
			this.Set(other as GetStatCountOptions);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000064C7 File Offset: 0x000046C7
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040002B5 RID: 693
		private int m_ApiVersion;

		// Token: 0x040002B6 RID: 694
		private IntPtr m_TargetUserId;
	}
}
