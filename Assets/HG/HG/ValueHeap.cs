using System;

namespace HG
{
	// Token: 0x02000019 RID: 25
	public class ValueHeap<T> where T : struct
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004863 File Offset: 0x00002A63
		public ValueHeap(uint initialSize)
		{
			this.heap = new ValueHeap<T>.Element[initialSize];
			this.allocator = new IndexAllocator();
			this.allocator.RequestIndex();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000488E File Offset: 0x00002A8E
		public bool PtrIsValid(in ValueHeap<T>.Ptr ptr)
		{
			return ptr.targetAddress != 0 && this.heap[ptr.targetAddress].cookie == ptr.targetCookie;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000048B8 File Offset: 0x00002AB8
		public ValueHeap<T>.Ptr Alloc()
		{
			int num = this.allocator.RequestIndex();
			if (this.heap.Length <= num)
			{
				Array.Resize<ValueHeap<T>.Element>(ref this.heap, this.heap.Length * 2);
			}
			return new ValueHeap<T>.Ptr(num, this.heap[num].cookie);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004908 File Offset: 0x00002B08
		public void Free(in ValueHeap<T>.Ptr ptr)
		{
			if (!this.PtrIsValid(ptr))
			{
				return;
			}
			ValueHeap<T>.Element[] array = this.heap;
			int targetAddress = ptr.targetAddress;
			array[targetAddress].value = default(T);
			array[targetAddress].cookie = array[targetAddress].cookie + 1U;
			this.allocator.FreeIndex(ptr.targetAddress);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004957 File Offset: 0x00002B57
		public void SetValue(in ValueHeap<T>.Ptr ptr, in T value)
		{
			this.heap[ptr.targetAddress].value = value;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004975 File Offset: 0x00002B75
		public T GetValue(in ValueHeap<T>.Ptr ptr)
		{
			return this.heap[ptr.targetAddress].value;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000498D File Offset: 0x00002B8D
		public ref T GetRef(ValueHeap<T>.Ptr ptr)
		{
			return ref this.heap[ptr.targetAddress].value;
		}

		// Token: 0x0400002B RID: 43
		private ValueHeap<T>.Element[] heap;

		// Token: 0x0400002C RID: 44
		private readonly IndexAllocator allocator;

		// Token: 0x0400002D RID: 45
		public static readonly ValueHeap<T>.Ptr nullPtr = new ValueHeap<T>.Ptr(0, 0U);

		// Token: 0x0200002B RID: 43
		private struct Element
		{
			// Token: 0x0400005E RID: 94
			public uint cookie;

			// Token: 0x0400005F RID: 95
			public T value;
		}

		// Token: 0x0200002C RID: 44
		public struct Ptr
		{
			// Token: 0x06000150 RID: 336 RVA: 0x00005960 File Offset: 0x00003B60
			public Ptr(int targetAddress, uint targetCookie)
			{
				this.targetAddress = targetAddress;
				this.targetCookie = targetCookie;
			}

			// Token: 0x04000060 RID: 96
			public readonly int targetAddress;

			// Token: 0x04000061 RID: 97
			public readonly uint targetCookie;
		}
	}
}
