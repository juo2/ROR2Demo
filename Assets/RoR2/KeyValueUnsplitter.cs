using System;
using System.Collections.Generic;
using HG;

// Token: 0x02000075 RID: 117
public struct KeyValueUnsplitter
{
	// Token: 0x0600020D RID: 525 RVA: 0x000096A7 File Offset: 0x000078A7
	public KeyValueUnsplitter(string baseKey)
	{
		this.baseKey = baseKey;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x000096B0 File Offset: 0x000078B0
	public string GetValue(IEnumerable<KeyValuePair<string, string>> keyValues)
	{
		List<KeyValuePair<int, string>> list = CollectionPool<KeyValuePair<int, string>, List<KeyValuePair<int, string>>>.RentCollection();
		foreach (KeyValuePair<string, string> keyValuePair in keyValues)
		{
			if (keyValuePair.Key.EndsWith("]", StringComparison.Ordinal))
			{
				int num = keyValuePair.Key.LastIndexOf("[", StringComparison.Ordinal);
				string value = keyValuePair.Key.Substring(0, num);
				int key;
				if (this.baseKey.Equals(value, StringComparison.Ordinal) && int.TryParse(keyValuePair.Key.Substring(num + 1), out key))
				{
					list.Add(new KeyValuePair<int, string>(key, keyValuePair.Value));
				}
			}
			else if (this.baseKey.Equals(keyValuePair.Key, StringComparison.Ordinal))
			{
				return keyValuePair.Value;
			}
		}
		list.Sort((KeyValuePair<int, string> a, KeyValuePair<int, string> b) => a.Key.CompareTo(b.Key));
		string result = string.Concat<KeyValuePair<int, string>>(list);
		CollectionPool<KeyValuePair<int, string>, List<KeyValuePair<int, string>>>.ReturnCollection(list);
		return result;
	}

	// Token: 0x040001E3 RID: 483
	public readonly string baseKey;
}
