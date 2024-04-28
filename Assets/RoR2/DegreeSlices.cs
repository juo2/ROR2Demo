using System;
using System.Collections;
using System.Collections.Generic;

namespace RoR2
{
	// Token: 0x02000582 RID: 1410
	public struct DegreeSlices : IEnumerable<float>, IEnumerable
	{
		// Token: 0x06001947 RID: 6471 RVA: 0x0006D591 File Offset: 0x0006B791
		public DegreeSlices(int sliceCount, float sliceOffset)
		{
			this.sliceCount = sliceCount;
			this.sliceOffset = sliceOffset;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0006D5A1 File Offset: 0x0006B7A1
		public DegreeSlices.Enumerator GetEnumerator()
		{
			return new DegreeSlices.Enumerator(this.sliceCount, this.sliceOffset);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
		IEnumerator<float> IEnumerable<float>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001FB2 RID: 8114
		public readonly int sliceCount;

		// Token: 0x04001FB3 RID: 8115
		public readonly float sliceOffset;

		// Token: 0x02000583 RID: 1411
		public struct Enumerator : IEnumerator<float>, IEnumerator, IDisposable
		{
			// Token: 0x0600194B RID: 6475 RVA: 0x0006D5C1 File Offset: 0x0006B7C1
			public Enumerator(int sliceCount, float sliceOffset)
			{
				this.sliceSize = 360f / (float)sliceCount;
				this.offset = sliceOffset * this.sliceSize;
				this.i = -1;
				this.iEnd = sliceCount;
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600194C RID: 6476 RVA: 0x0006D5ED File Offset: 0x0006B7ED
			public float Current
			{
				get
				{
					return (float)this.i * this.sliceSize + this.offset;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600194D RID: 6477 RVA: 0x0006D604 File Offset: 0x0006B804
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600194E RID: 6478 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x0600194F RID: 6479 RVA: 0x0006D611 File Offset: 0x0006B811
			public bool MoveNext()
			{
				this.i++;
				return this.i < this.iEnd;
			}

			// Token: 0x06001950 RID: 6480 RVA: 0x0006D62F File Offset: 0x0006B82F
			public void Reset()
			{
				this.i = -1;
			}

			// Token: 0x04001FB4 RID: 8116
			public readonly float sliceSize;

			// Token: 0x04001FB5 RID: 8117
			public readonly float offset;

			// Token: 0x04001FB6 RID: 8118
			public int i;

			// Token: 0x04001FB7 RID: 8119
			public int iEnd;
		}
	}
}
