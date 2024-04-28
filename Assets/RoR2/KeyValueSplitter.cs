using System;
using System.Collections.Generic;
using System.Text;
using HG;
using JetBrains.Annotations;

// Token: 0x02000071 RID: 113
public readonly struct KeyValueSplitter
{
	// Token: 0x060001FF RID: 511 RVA: 0x0000942C File Offset: 0x0000762C
	public KeyValueSplitter([NotNull] string baseKey, int maxKeyLength, int maxValueLength, [NotNull] Action<string, string> keyValueSetter)
	{
		this.baseKey = baseKey;
		this.maxKeyLength = maxKeyLength;
		this.maxValueLength = maxValueLength;
		this.keyValueSetter = keyValueSetter;
		this.currentSubKeys = new List<string>();
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00009456 File Offset: 0x00007656
	public void SetValue([NotNull] StringBuilder stringBuilder)
	{
		this.SetValueInternal<KeyValueSplitter.StringBuilderWrapper>(new KeyValueSplitter.StringBuilderWrapper(stringBuilder));
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00009464 File Offset: 0x00007664
	public void SetValue([NotNull] string value)
	{
		this.SetValueInternal<KeyValueSplitter.StringWrapper>(new KeyValueSplitter.StringWrapper(value));
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00009474 File Offset: 0x00007674
	private void SetValueInternal<T>(T value) where T : KeyValueSplitter.IStringWrapper
	{
		int length = value.Length;
		List<KeyValuePair<string, string>> list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.RentCollection();
		if (length <= this.maxValueLength)
		{
			list.Add(new KeyValuePair<string, string>(this.baseKey, value.ToString()));
		}
		else
		{
			int num = length;
			int num2 = 0;
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			do
			{
				int length2 = Math.Min(num, this.maxValueLength);
				string key = stringBuilder.Clear().Append(this.baseKey).Append("[").AppendInt(num2++, 1U, uint.MaxValue).Append("]").ToString();
				string value2 = value.SubString(value.Length - num, length2);
				list.Add(new KeyValuePair<string, string>(key, value2));
				num -= this.maxValueLength;
			}
			while (num > 0);
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}
		for (int i = 0; i < this.currentSubKeys.Count; i++)
		{
			string text = this.currentSubKeys[i];
			bool flag = false;
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j].Key.Equals(text, StringComparison.Ordinal))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.keyValueSetter(text, null);
			}
		}
		this.currentSubKeys.Clear();
		for (int k = 0; k < list.Count; k++)
		{
			this.currentSubKeys.Add(list[k].Key);
		}
		for (int l = 0; l < list.Count; l++)
		{
			KeyValuePair<string, string> keyValuePair = list[l];
			this.keyValueSetter(keyValuePair.Key, keyValuePair.Value);
		}
		CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.ReturnCollection(list);
	}

	// Token: 0x040001DC RID: 476
	public readonly string baseKey;

	// Token: 0x040001DD RID: 477
	private readonly int maxKeyLength;

	// Token: 0x040001DE RID: 478
	private readonly int maxValueLength;

	// Token: 0x040001DF RID: 479
	private readonly List<string> currentSubKeys;

	// Token: 0x040001E0 RID: 480
	private readonly Action<string, string> keyValueSetter;

	// Token: 0x02000072 RID: 114
	private interface IStringWrapper
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000203 RID: 515
		int Length { get; }

		// Token: 0x06000204 RID: 516
		string SubString(int startIndex, int length);
	}

	// Token: 0x02000073 RID: 115
	private struct StringWrapper : KeyValueSplitter.IStringWrapper
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00009648 File Offset: 0x00007848
		public StringWrapper([NotNull] string str)
		{
			this.src = str;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009651 File Offset: 0x00007851
		public override string ToString()
		{
			return this.src;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00009659 File Offset: 0x00007859
		public int Length
		{
			get
			{
				return this.src.Length;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009666 File Offset: 0x00007866
		public string SubString(int startIndex, int length)
		{
			return this.src.Substring(startIndex, length);
		}

		// Token: 0x040001E1 RID: 481
		private readonly string src;
	}

	// Token: 0x02000074 RID: 116
	private struct StringBuilderWrapper : KeyValueSplitter.IStringWrapper
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00009675 File Offset: 0x00007875
		public StringBuilderWrapper([NotNull] StringBuilder src)
		{
			this.src = src;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000967E File Offset: 0x0000787E
		public override string ToString()
		{
			return this.src.ToString();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000968B File Offset: 0x0000788B
		public int Length
		{
			get
			{
				return this.src.Length;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009698 File Offset: 0x00007898
		public string SubString(int startIndex, int length)
		{
			return this.src.ToString(startIndex, length);
		}

		// Token: 0x040001E2 RID: 482
		private readonly StringBuilder src;
	}
}
