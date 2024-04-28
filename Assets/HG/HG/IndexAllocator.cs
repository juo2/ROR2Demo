using System;

namespace HG
{
	// Token: 0x02000010 RID: 16
	public class IndexAllocator
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000036B1 File Offset: 0x000018B1
		public IndexAllocator()
		{
			this.ranges = new IndexAllocator.Range[16];
			this.ranges[0] = new IndexAllocator.Range(0, int.MaxValue);
			this.rangeCount = 1;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000036E4 File Offset: 0x000018E4
		public int RequestIndex()
		{
			int result = this.ranges[0].TakeIndex();
			if (this.ranges[0].empty)
			{
				this.RemoveAt(0);
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003711 File Offset: 0x00001911
		private void RemoveAt(int i)
		{
			ArrayUtils.ArrayRemoveAt<IndexAllocator.Range>(this.ranges, ref this.rangeCount, i, 1);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003726 File Offset: 0x00001926
		private void InsertAt(int i, IndexAllocator.Range range)
		{
			ArrayUtils.ArrayInsert<IndexAllocator.Range>(ref this.ranges, ref this.rangeCount, i, range);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000373C File Offset: 0x0000193C
		public void FreeIndex(int index)
		{
			if (index < this.ranges[0].startIndex)
			{
				if (this.ranges[0].TryExtending(index))
				{
					return;
				}
				this.InsertAt(0, new IndexAllocator.Range(index, index + 1));
				return;
			}
			else
			{
				if (this.ranges[this.rangeCount - 1].endIndex > index)
				{
					int i = 1;
					while (i < this.rangeCount)
					{
						int endIndex = this.ranges[i - 1].endIndex;
						int startIndex = this.ranges[i].startIndex;
						if (endIndex <= index && index < startIndex)
						{
							bool flag = index == endIndex;
							bool flag2 = index == startIndex - 1;
							if (flag ^ flag2)
							{
								if (flag)
								{
									IndexAllocator.Range[] array = this.ranges;
									int num = i - 1;
									array[num].endIndex = array[num].endIndex + 1;
									return;
								}
								IndexAllocator.Range[] array2 = this.ranges;
								int num2 = i;
								array2[num2].startIndex = array2[num2].startIndex - 1;
								return;
							}
							else
							{
								if (flag)
								{
									this.ranges[i - 1].endIndex = this.ranges[i].endIndex;
									this.RemoveAt(i);
									return;
								}
								this.InsertAt(i, new IndexAllocator.Range(index, index + 1));
								return;
							}
						}
						else
						{
							i++;
						}
					}
					return;
				}
				if (this.ranges[this.rangeCount - 1].TryExtending(index))
				{
					return;
				}
				this.InsertAt(this.rangeCount, new IndexAllocator.Range(index, index + 1));
				return;
			}
		}

		// Token: 0x0400001C RID: 28
		private IndexAllocator.Range[] ranges;

		// Token: 0x0400001D RID: 29
		private int rangeCount;

		// Token: 0x02000026 RID: 38
		private struct Range
		{
			// Token: 0x0600013F RID: 319 RVA: 0x000057DA File Offset: 0x000039DA
			public Range(int startIndex, int endIndex)
			{
				this.startIndex = startIndex;
				this.endIndex = endIndex;
			}

			// Token: 0x06000140 RID: 320 RVA: 0x000057EC File Offset: 0x000039EC
			public int TakeIndex()
			{
				int num = this.startIndex;
				this.startIndex = num + 1;
				return num;
			}

			// Token: 0x06000141 RID: 321 RVA: 0x0000580A File Offset: 0x00003A0A
			public bool TryExtending(int index)
			{
				if (index == this.startIndex - 1)
				{
					this.startIndex--;
					return true;
				}
				if (index == this.endIndex)
				{
					this.endIndex++;
					return true;
				}
				return false;
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000142 RID: 322 RVA: 0x00005841 File Offset: 0x00003A41
			public bool empty
			{
				get
				{
					return this.startIndex == this.endIndex;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00005851 File Offset: 0x00003A51
			public int size
			{
				get
				{
					return this.endIndex - this.startIndex;
				}
			}

			// Token: 0x04000054 RID: 84
			public int startIndex;

			// Token: 0x04000055 RID: 85
			public int endIndex;
		}
	}
}
