using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000150 RID: 336
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerSanctionByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000226 RID: 550
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0000A4D2 File Offset: 0x000086D2
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000227 RID: 551
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0000A4E1 File Offset: 0x000086E1
		public uint SanctionIndex
		{
			set
			{
				this.m_SanctionIndex = value;
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0000A4EA File Offset: 0x000086EA
		public void Set(CopyPlayerSanctionByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.SanctionIndex = other.SanctionIndex;
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0000A50E File Offset: 0x0000870E
		public void Set(object other)
		{
			this.Set(other as CopyPlayerSanctionByIndexOptions);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000A51C File Offset: 0x0000871C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000472 RID: 1138
		private int m_ApiVersion;

		// Token: 0x04000473 RID: 1139
		private IntPtr m_TargetUserId;

		// Token: 0x04000474 RID: 1140
		private uint m_SanctionIndex;
	}
}
