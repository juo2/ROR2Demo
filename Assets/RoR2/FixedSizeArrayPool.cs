using System;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x020005B8 RID: 1464
	public class FixedSizeArrayPool<T>
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00071CE1 File Offset: 0x0006FEE1
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00071CE9 File Offset: 0x0006FEE9
		public int lengthOfArrays
		{
			get
			{
				return this._lengthOfArrays;
			}
			set
			{
				if (this._lengthOfArrays == value)
				{
					return;
				}
				ArrayUtils.Clear<T[]>(this.pooledArrays, ref this.count);
				this._lengthOfArrays = value;
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00071D0D File Offset: 0x0006FF0D
		public FixedSizeArrayPool(int lengthOfArrays)
		{
			this._lengthOfArrays = lengthOfArrays;
			this.pooledArrays = Array.Empty<T[]>();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00071D28 File Offset: 0x0006FF28
		[NotNull]
		public T[] Request()
		{
			if (this.count <= 0)
			{
				return new T[this._lengthOfArrays];
			}
			T[][] array = this.pooledArrays;
			int num = this.count - 1;
			this.count = num;
			T[] result = array[num];
			this.pooledArrays[this.count] = null;
			return result;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00071D70 File Offset: 0x0006FF70
		[NotNull]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static ArgumentException CreateArraySizeMismatchException(int incomingArrayLength, int arrayPoolSizeRequirement)
		{
			return new ArgumentException(string.Format("Array of length {0} may not be returned to pool for arrays of length {1}", incomingArrayLength, arrayPoolSizeRequirement), "array");
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00071D94 File Offset: 0x0006FF94
		public void Return([NotNull] T[] array)
		{
			if (array.Length != this._lengthOfArrays)
			{
				throw FixedSizeArrayPool<T>.CreateArraySizeMismatchException(array.Length, this._lengthOfArrays);
			}
			T[] array2 = array;
			T t = default(T);
			ArrayUtils.SetAll<T>(array2, t);
			ArrayUtils.ArrayAppend<T[]>(ref this.pooledArrays, ref this.count, array);
		}

		// Token: 0x04002095 RID: 8341
		private int _lengthOfArrays;

		// Token: 0x04002096 RID: 8342
		[NotNull]
		private T[][] pooledArrays;

		// Token: 0x04002097 RID: 8343
		private int count;
	}
}
