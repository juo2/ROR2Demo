using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x0200001A RID: 26
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageQueryInternal : ISettable, IDisposable
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00003863 File Offset: 0x00001A63
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000386B File Offset: 0x00001A6B
		public int StartIndex
		{
			get
			{
				return this.m_StartIndex;
			}
			set
			{
				this.m_StartIndex = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003874 File Offset: 0x00001A74
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000387C File Offset: 0x00001A7C
		public int MaxCount
		{
			get
			{
				return this.m_MaxCount;
			}
			set
			{
				this.m_MaxCount = value;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00003885 File Offset: 0x00001A85
		public void Set(PageQuery other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.StartIndex = other.StartIndex;
				this.MaxCount = other.MaxCount;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000038A9 File Offset: 0x00001AA9
		public void Set(object other)
		{
			this.Set(other as PageQuery);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000049 RID: 73
		private int m_ApiVersion;

		// Token: 0x0400004A RID: 74
		private int m_StartIndex;

		// Token: 0x0400004B RID: 75
		private int m_MaxCount;
	}
}
