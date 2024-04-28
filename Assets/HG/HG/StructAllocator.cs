using System;

namespace HG
{
	// Token: 0x02000018 RID: 24
	public class StructAllocator<T> where T : struct
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004710 File Offset: 0x00002910
		public StructAllocator(uint initialSize)
		{
			this.heap = new StructAllocator<T>.Element[initialSize];
			this.allocator = new IndexPool();
			this.allocator.RequestIndex();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000473B File Offset: 0x0000293B
		public bool PtrIsValid(in StructAllocator<T>.Ptr ptr)
		{
			return ptr.targetAddress != 0 && this.heap[ptr.targetAddress].cookie == ptr.targetCookie;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004768 File Offset: 0x00002968
		public StructAllocator<T>.Ptr Alloc()
		{
			int num = this.allocator.RequestIndex();
			if (this.heap.Length <= num)
			{
				Array.Resize<StructAllocator<T>.Element>(ref this.heap, this.heap.Length * 2);
			}
			return new StructAllocator<T>.Ptr(num, this.heap[num].cookie);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000047B8 File Offset: 0x000029B8
		public void Free(in StructAllocator<T>.Ptr ptr)
		{
			if (!this.PtrIsValid(ptr))
			{
				return;
			}
			StructAllocator<T>.Element[] array = this.heap;
			int targetAddress = ptr.targetAddress;
			array[targetAddress].value = default(T);
			array[targetAddress].cookie = array[targetAddress].cookie + 1U;
			this.allocator.FreeIndex(ptr.targetAddress);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004807 File Offset: 0x00002A07
		public void SetValue(in StructAllocator<T>.Ptr ptr, in T value)
		{
			this.heap[ptr.targetAddress].value = value;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004825 File Offset: 0x00002A25
		public T GetValue(in StructAllocator<T>.Ptr ptr)
		{
			return this.heap[ptr.targetAddress].value;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000483D File Offset: 0x00002A3D
		public ref T GetRef(StructAllocator<T>.Ptr ptr)
		{
			return ref this.heap[ptr.targetAddress].value;
		}

		// Token: 0x04000028 RID: 40
		private StructAllocator<T>.Element[] heap;

		// Token: 0x04000029 RID: 41
		private readonly IndexPool allocator;

		// Token: 0x0400002A RID: 42
		public static readonly StructAllocator<T>.Ptr nullPtr = new StructAllocator<T>.Ptr(0, 0U);

		// Token: 0x02000029 RID: 41
		private struct Element
		{
			// Token: 0x0400005A RID: 90
			public uint cookie;

			// Token: 0x0400005B RID: 91
			public T value;
		}

		// Token: 0x0200002A RID: 42
		public struct Ptr
		{
			// Token: 0x0600014F RID: 335 RVA: 0x00005950 File Offset: 0x00003B50
			public Ptr(int targetAddress, uint targetCookie)
			{
				this.targetAddress = targetAddress;
				this.targetCookie = targetCookie;
			}

			// Token: 0x0400005C RID: 92
			public readonly int targetAddress;

			// Token: 0x0400005D RID: 93
			public readonly uint targetCookie;
		}
	}
}
