using System;
using System.Collections.Generic;

namespace RoR2
{
	// Token: 0x020004F7 RID: 1271
	public class CatalogModHelper<TEntry>
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001722 RID: 5922 RVA: 0x00066248 File Offset: 0x00064448
		// (remove) Token: 0x06001723 RID: 5923 RVA: 0x00066280 File Offset: 0x00064480
		public event Action<List<TEntry>> getAdditionalEntries;

		// Token: 0x06001724 RID: 5924 RVA: 0x000662B5 File Offset: 0x000644B5
		public CatalogModHelper(Action<int, TEntry> registrationDelegate, Func<TEntry, string> nameGetter)
		{
			this.registrationDelegate = registrationDelegate;
			this.nameGetter = nameGetter;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000662CC File Offset: 0x000644CC
		public void CollectAndRegisterAdditionalEntries(ref TEntry[] entries)
		{
			int num = entries.Length;
			List<TEntry> list = new List<TEntry>();
			Action<List<TEntry>> action = this.getAdditionalEntries;
			if (action != null)
			{
				action(list);
			}
			list.Sort((TEntry a, TEntry b) => StringComparer.Ordinal.Compare(this.nameGetter(a), this.nameGetter(b)));
			Array.Resize<TEntry>(ref entries, entries.Length + list.Count);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				entries[num + i] = list[i];
				Action<int, TEntry> action2 = this.registrationDelegate;
				if (action2 != null)
				{
					action2(num + i, list[i]);
				}
				i++;
			}
		}

		// Token: 0x04001CD6 RID: 7382
		private readonly Action<int, TEntry> registrationDelegate;

		// Token: 0x04001CD7 RID: 7383
		private readonly Func<TEntry, string> nameGetter;
	}
}
