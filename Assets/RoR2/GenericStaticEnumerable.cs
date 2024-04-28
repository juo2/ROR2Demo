using System;
using System.Collections;
using System.Collections.Generic;

namespace RoR2
{
	// Token: 0x0200091B RID: 2331
	public struct GenericStaticEnumerable<T, TEnumerator> : IEnumerable<T>, IEnumerable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x060034BA RID: 13498 RVA: 0x000DEB9F File Offset: 0x000DCD9F
		static GenericStaticEnumerable()
		{
			GenericStaticEnumerable<T, TEnumerator>.defaultValue.Reset();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000DEBBC File Offset: 0x000DCDBC
		public TEnumerator GetEnumerator()
		{
			return GenericStaticEnumerable<T, TEnumerator>.defaultValue;
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000DEBC3 File Offset: 0x000DCDC3
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return GenericStaticEnumerable<T, TEnumerator>.defaultValue;
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000DEBC3 File Offset: 0x000DCDC3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GenericStaticEnumerable<T, TEnumerator>.defaultValue;
		}

		// Token: 0x040035B1 RID: 13745
		private static readonly TEnumerator defaultValue = default(TEnumerator);
	}
}
