using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x0200008F RID: 143
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000EF RID: 239
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000639D File Offset: 0x0000459D
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000063AC File Offset: 0x000045AC
		public uint StatIndex
		{
			set
			{
				this.m_StatIndex = value;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000063B5 File Offset: 0x000045B5
		public void Set(CopyStatByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.StatIndex = other.StatIndex;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000063D9 File Offset: 0x000045D9
		public void Set(object other)
		{
			this.Set(other as CopyStatByIndexOptions);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000063E7 File Offset: 0x000045E7
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040002AC RID: 684
		private int m_ApiVersion;

		// Token: 0x040002AD RID: 685
		private IntPtr m_TargetUserId;

		// Token: 0x040002AE RID: 686
		private uint m_StatIndex;
	}
}
