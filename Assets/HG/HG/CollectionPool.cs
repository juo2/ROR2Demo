using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x02000008 RID: 8
	public class CollectionPool<TElement, TCollection> where TCollection : ICollection<TElement>, new()
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002A88 File Offset: 0x00000C88
		[NotNull]
		public static TCollection RentCollection()
		{
			if (CollectionPool<TElement, TCollection>.pooledCollections.Count <= 0)
			{
				return Activator.CreateInstance<TCollection>();
			}
			return CollectionPool<TElement, TCollection>.pooledCollections.Pop();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA8 File Offset: 0x00000CA8
		[CanBeNull]
		public static TCollection ReturnCollection([NotNull] TCollection rentedCollection)
		{
			rentedCollection.Clear();
			CollectionPool<TElement, TCollection>.pooledCollections.Push(rentedCollection);
			return default(TCollection);
		}

		// Token: 0x0400000D RID: 13
		private static readonly Stack<TCollection> pooledCollections = new Stack<TCollection>();
	}
}
