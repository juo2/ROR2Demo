using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x0200001C RID: 28
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageResultInternal : ISettable, IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00003954 File Offset: 0x00001B54
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000395C File Offset: 0x00001B5C
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00003965 File Offset: 0x00001B65
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000396D File Offset: 0x00001B6D
		public int Count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00003976 File Offset: 0x00001B76
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000397E File Offset: 0x00001B7E
		public int TotalCount
		{
			get
			{
				return this.m_TotalCount;
			}
			set
			{
				this.m_TotalCount = value;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00003987 File Offset: 0x00001B87
		public void Set(PageResult other)
		{
			if (other != null)
			{
				this.StartIndex = other.StartIndex;
				this.Count = other.Count;
				this.TotalCount = other.TotalCount;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000039B0 File Offset: 0x00001BB0
		public void Set(object other)
		{
			this.Set(other as PageResult);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400004F RID: 79
		private int m_StartIndex;

		// Token: 0x04000050 RID: 80
		private int m_Count;

		// Token: 0x04000051 RID: 81
		private int m_TotalCount;
	}
}
