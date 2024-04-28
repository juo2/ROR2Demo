using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x02000016 RID: 22
	public class RefCountedCollection<T, TDict> : IDisposable where TDict : IDictionary<T, int>, new()
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000044C6 File Offset: 0x000026C6
		public RefCountedCollection()
		{
			this.dict = Activator.CreateInstance<TDict>();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000044E0 File Offset: 0x000026E0
		public void Dispose()
		{
			if (this.dict == null)
			{
				return;
			}
			foreach (KeyValuePair<T, int> keyValuePair in this.dict)
			{
				Action<T> action = this.onItemLost;
				if (action != null)
				{
					action(keyValuePair.Key);
				}
			}
			this.dict.Clear();
			this.dict = null;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000455C File Offset: 0x0000275C
		public void Add([NotNull] T item)
		{
			int num;
			this.dict.TryGetValue(item, out num);
			num++;
			this.dict[item] = num;
			if (num == 1)
			{
				Action<T> action = this.onItemDiscovered;
				if (action == null)
				{
					return;
				}
				action(item);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000045A0 File Offset: 0x000027A0
		public void Remove([NotNull] T item)
		{
			int num;
			if (!this.dict.TryGetValue(item, out num))
			{
				return;
			}
			num--;
			if (num > 0)
			{
				this.dict[item] = num;
				return;
			}
			this.dict.Remove(item);
			Action<T> action = this.onItemLost;
			if (action == null)
			{
				return;
			}
			action(item);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000E2 RID: 226 RVA: 0x000045F4 File Offset: 0x000027F4
		// (remove) Token: 0x060000E3 RID: 227 RVA: 0x0000462C File Offset: 0x0000282C
		public event Action<T> onItemDiscovered;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000E4 RID: 228 RVA: 0x00004664 File Offset: 0x00002864
		// (remove) Token: 0x060000E5 RID: 229 RVA: 0x0000469C File Offset: 0x0000289C
		public event Action<T> onItemLost;

		// Token: 0x04000024 RID: 36
		private IDictionary<T, int> dict;
	}
}
