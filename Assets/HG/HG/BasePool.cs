using System;
using System.Collections.Generic;

namespace HG
{
	// Token: 0x02000007 RID: 7
	public abstract class BasePool<TItem> where TItem : class, new()
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002A22 File Offset: 0x00000C22
		protected virtual TItem CreateItem()
		{
			return Activator.CreateInstance<TItem>();
		}

		// Token: 0x0600003F RID: 63
		protected abstract void ResetItem(TItem item);

		// Token: 0x06000040 RID: 64 RVA: 0x00002A29 File Offset: 0x00000C29
		public TItem Request()
		{
			if (this.items.Count > 0)
			{
				return this.items.Pop();
			}
			return this.CreateItem();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A4C File Offset: 0x00000C4C
		public TItem Return(TItem item)
		{
			this.ResetItem(item);
			this.items.Push(item);
			return default(TItem);
		}

		// Token: 0x0400000C RID: 12
		private readonly Stack<TItem> items = new Stack<TItem>();
	}
}
