using System;

namespace HG
{
	// Token: 0x02000011 RID: 17
	public class IndexPool
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000038A1 File Offset: 0x00001AA1
		public IndexPool()
		{
			this.ranges = new IndexPool.Range[16];
			this.ranges[0] = new IndexPool.Range(0, int.MaxValue);
			this.rangeCount = 1;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000038D4 File Offset: 0x00001AD4
		public int RequestIndex()
		{
			int result = this.ranges[0].TakeIndex();
			if (this.ranges[0].empty)
			{
				this.RemoveAt(0);
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003901 File Offset: 0x00001B01
		private void RemoveAt(int i)
		{
			ArrayUtils.ArrayRemoveAt<IndexPool.Range>(this.ranges, ref this.rangeCount, i, 1);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003916 File Offset: 0x00001B16
		private void InsertAt(int i, IndexPool.Range range)
		{
			ArrayUtils.ArrayInsert<IndexPool.Range>(ref this.ranges, ref this.rangeCount, i, range);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000392C File Offset: 0x00001B2C
		public void FreeIndex(int index)
		{
			if (index < this.ranges[0].startIndex)
			{
				if (this.ranges[0].TryExtending(index))
				{
					return;
				}
				this.InsertAt(0, new IndexPool.Range(index, index + 1));
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
									IndexPool.Range[] array = this.ranges;
									int num = i - 1;
									array[num].endIndex = array[num].endIndex + 1;
									return;
								}
								IndexPool.Range[] array2 = this.ranges;
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
								this.InsertAt(i, new IndexPool.Range(index, index + 1));
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
				this.InsertAt(this.rangeCount, new IndexPool.Range(index, index + 1));
				return;
			}
		}

		// Token: 0x0400001E RID: 30
		private IndexPool.Range[] ranges;

		// Token: 0x0400001F RID: 31
		private int rangeCount;

		// Token: 0x02000027 RID: 39
		private struct Range
		{
			// Token: 0x06000144 RID: 324 RVA: 0x00005860 File Offset: 0x00003A60
			public Range(int startIndex, int endIndex)
			{
				this.startIndex = startIndex;
				this.endIndex = endIndex;
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00005870 File Offset: 0x00003A70
			public int TakeIndex()
			{
				int num = this.startIndex;
				this.startIndex = num + 1;
				return num;
			}

			// Token: 0x06000146 RID: 326 RVA: 0x0000588E File Offset: 0x00003A8E
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

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000147 RID: 327 RVA: 0x000058C5 File Offset: 0x00003AC5
			public bool empty
			{
				get
				{
					return this.startIndex == this.endIndex;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000148 RID: 328 RVA: 0x000058D5 File Offset: 0x00003AD5
			public int size
			{
				get
				{
					return this.endIndex - this.startIndex;
				}
			}

			// Token: 0x04000056 RID: 86
			public int startIndex;

			// Token: 0x04000057 RID: 87
			public int endIndex;
		}
	}
}
