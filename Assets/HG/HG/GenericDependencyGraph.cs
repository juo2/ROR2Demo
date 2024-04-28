using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x0200000E RID: 14
	public class GenericDependencyGraph<TElement> where TElement : class
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002E0C File Offset: 0x0000100C
		public GenericDependencyGraph([NotNull] GenericDependencyGraph<TElement>.EntryData[] newEntries)
		{
			if (newEntries == null)
			{
				throw new ArgumentNullException("newEntries");
			}
			this.elementToId = new Dictionary<TElement, int>();
			int num = -1;
			for (int i = 0; i < newEntries.Length; i++)
			{
				TElement element = newEntries[i].element;
				if (newEntries[i].element == null)
				{
					throw new ArgumentException(string.Format("{0}[{1}]=null", "newEntries", i));
				}
				int num2;
				if (this.elementToId.TryGetValue(element, out num2))
				{
					throw new ArgumentException("Element is included more than once. element=" + GenericDependencyGraph<TElement>.ElementToStringSafe(element));
				}
				this.elementToId.Add(element, num);
				num++;
			}
			int num3 = 0;
			int num4 = 0;
			int[] array = new int[newEntries.Length];
			for (int j = 0; j < newEntries.Length; j++)
			{
				TElement[] dependencies = newEntries[j].dependencies;
				if (dependencies != null && dependencies.Length != 0)
				{
					for (int k = 0; k < dependencies.Length; k++)
					{
						TElement telement = dependencies[k];
						int num5;
						bool flag = this.elementToId.TryGetValue(telement, out num5);
						if (telement == null)
						{
							throw new ArgumentException(string.Format("{0}[{1}].{2}[{3}]=null", new object[]
							{
								"newEntries",
								j,
								"dependencies",
								k
							}));
						}
						if (!flag)
						{
							throw new ArgumentException(string.Format("{0}[{1}].{2}[{3}] specifies a dependency which is not registered. dependency={4}", new object[]
							{
								"newEntries",
								j,
								"dependencies",
								k,
								GenericDependencyGraph<TElement>.ElementToStringSafe(telement)
							}));
						}
						for (int l = k + 1; l < dependencies.Length; l++)
						{
							if (telement == dependencies[l])
							{
								throw new ArgumentException(string.Format("{0}[{1}].{2} includes a single dependency more than once. firstOccurrenceIndex=[{3}] secondOccurrenceIndex=[{4}] dependency={5}", new object[]
								{
									"newEntries",
									j,
									"dependencies",
									k,
									l,
									GenericDependencyGraph<TElement>.ElementToStringSafe(telement)
								}));
							}
						}
						array[num5]++;
					}
					num3 += dependencies.Length;
				}
				else
				{
					num4++;
				}
			}
			this.internalEntries = new GenericDependencyGraph<TElement>.InternalEntry[newEntries.Length];
			this.dependencyIds = new int[num3];
			this.dependentsIds = new int[num3];
			this.rootIds = new int[num4];
			int num6 = 0;
			int[] array2 = new int[newEntries.Length];
			int num7 = 0;
			int num8 = 0;
			for (int m = 0; m < newEntries.Length; m++)
			{
				ref GenericDependencyGraph<TElement>.InternalEntry ptr = ref this.internalEntries[m];
				ptr.dependentsStartPtr = num8;
				array2[m] = ptr.dependentsStartPtr;
				ptr.dependentsLength = array[m];
				num8 += ptr.dependentsLength;
			}
			for (int n = 0; n < newEntries.Length; n++)
			{
				GenericDependencyGraph<TElement>.EntryData entryData = newEntries[n];
				ref GenericDependencyGraph<TElement>.InternalEntry ptr2 = ref this.internalEntries[n];
				ptr2.element = entryData.element;
				TElement[] dependencies2 = entryData.dependencies;
				if (dependencies2 != null && dependencies2.Length != 0)
				{
					ptr2.dependenciesStartPtr = num6;
					ptr2.dependenciesLength = dependencies2.Length;
					for (int num9 = 0; num9 < dependencies2.Length; num9++)
					{
						TElement key = dependencies2[n];
						int num10 = this.elementToId[key];
						this.dependencyIds[num6] = num10;
						num6++;
						ref int ptr3 = ref array2[num10];
						this.dependentsIds[ptr3] = n;
						ptr3++;
					}
				}
				else
				{
					this.rootIds[num7] = n;
					num7++;
				}
			}
			this.CheckForCircularReferences();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031D8 File Offset: 0x000013D8
		private void CheckForCircularReferences()
		{
			GenericDependencyGraph<TElement>.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.checkStack = new Stack<GenericDependencyGraph<TElement>.NodeIterationFrame>();
			CS$<>8__locals1.entriesInChainSet = new bool[this.internalEntries.Length];
			CS$<>8__locals1.checkedSet = new bool[this.internalEntries.Length];
			List<int> list = new List<int>();
			for (int i = 0; i < this.internalEntries.Length; i++)
			{
				this.<CheckForCircularReferences>g__CheckEntryForCircularReferences|9_0(i, list, ref CS$<>8__locals1);
				if (list.Count > 0)
				{
					string text = "Circular reference detected. Chain: ";
					for (int j = 0; j < list.Count; j++)
					{
						int num = list[j];
						if (j > 0)
						{
							text += "<-";
						}
						text += string.Format("[{0}]=({1})", num, GenericDependencyGraph<TElement>.ElementToStringSafe(this.internalEntries[j].element));
					}
					throw new Exception(text);
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032C0 File Offset: 0x000014C0
		private static string ElementToStringSafe(TElement element)
		{
			string name = element.GetType().Name;
			string text;
			try
			{
				text = "\"" + element.ToString() + "\"";
			}
			catch (Exception ex)
			{
				text = ex.GetType().Name;
			}
			return string.Concat(new string[]
			{
				"{GetType()=",
				name,
				", ToString()=",
				text,
				"}"
			});
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003344 File Offset: 0x00001544
		public void GetElementDependents([NotNull] TElement element, [NotNull] List<TElement> dest)
		{
			TElement telement = element;
			if (telement == null)
			{
				throw new ArgumentNullException("element");
			}
			element = telement;
			List<TElement> list = dest;
			if (list == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest = list;
			int num;
			if (!this.elementToId.TryGetValue(element, out num))
			{
				return;
			}
			ref GenericDependencyGraph<TElement>.InternalEntry ptr = ref this.internalEntries[num];
			for (int i = 0; i < ptr.dependentsLength; i++)
			{
				int num2 = this.dependentsIds[ptr.dependentsStartPtr + i];
				ref GenericDependencyGraph<TElement>.InternalEntry ptr2 = ref this.internalEntries[num2];
				dest.Add(ptr2.element);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033D8 File Offset: 0x000015D8
		public void GetElementDependencies([NotNull] TElement element, [NotNull] List<TElement> dest)
		{
			TElement telement = element;
			if (telement == null)
			{
				throw new ArgumentNullException("element");
			}
			element = telement;
			List<TElement> list = dest;
			if (list == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest = list;
			int num;
			if (!this.elementToId.TryGetValue(element, out num))
			{
				return;
			}
			ref GenericDependencyGraph<TElement>.InternalEntry ptr = ref this.internalEntries[num];
			for (int i = 0; i < ptr.dependenciesLength; i++)
			{
				int num2 = this.dependencyIds[ptr.dependenciesStartPtr + i];
				ref GenericDependencyGraph<TElement>.InternalEntry ptr2 = ref this.internalEntries[num2];
				dest.Add(ptr2.element);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000346C File Offset: 0x0000166C
		public void GetRootElements([NotNull] List<TElement> dest)
		{
			List<TElement> list = dest;
			if (list == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest = list;
			for (int i = 0; i < this.rootIds.Length; i++)
			{
				int num = this.rootIds[i];
				ref GenericDependencyGraph<TElement>.InternalEntry ptr = ref this.internalEntries[num];
				dest.Add(ptr.element);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034C0 File Offset: 0x000016C0
		[CompilerGenerated]
		private void <CheckForCircularReferences>g__CheckEntryForCircularReferences|9_0(int entryIdToSearchFor, List<int> dest, ref GenericDependencyGraph<TElement>.<>c__DisplayClass9_0 A_3)
		{
			GenericDependencyGraph<TElement>.<>c__DisplayClass9_1 CS$<>8__locals1;
			CS$<>8__locals1.dest = dest;
			this.<CheckForCircularReferences>g__Push|9_1(new GenericDependencyGraph<TElement>.NodeIterationFrame
			{
				entryId = entryIdToSearchFor,
				entryDependencyIndex = 0
			}, ref A_3, ref CS$<>8__locals1);
			while (A_3.checkStack.Count > 0)
			{
				GenericDependencyGraph<TElement>.NodeIterationFrame nodeIterationFrame = A_3.checkStack.Peek();
				ref GenericDependencyGraph<TElement>.InternalEntry ptr = ref this.internalEntries[nodeIterationFrame.entryId];
				if (ptr.dependentsLength <= nodeIterationFrame.entryDependencyIndex)
				{
					A_3.checkStack.Pop();
					A_3.entriesInChainSet[nodeIterationFrame.entryId] = false;
					A_3.checkedSet[nodeIterationFrame.entryId] = true;
				}
				else
				{
					int entryId = this.dependencyIds[ptr.dependentsStartPtr + nodeIterationFrame.entryDependencyIndex];
					A_3.checkStack.Pop();
					nodeIterationFrame.entryDependencyIndex++;
					A_3.checkStack.Push(nodeIterationFrame);
					this.<CheckForCircularReferences>g__Push|9_1(new GenericDependencyGraph<TElement>.NodeIterationFrame
					{
						entryId = entryId,
						entryDependencyIndex = 0
					}, ref A_3, ref CS$<>8__locals1);
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035C0 File Offset: 0x000017C0
		[CompilerGenerated]
		private void <CheckForCircularReferences>g__Push|9_1(GenericDependencyGraph<TElement>.NodeIterationFrame nodeIterationFrame, ref GenericDependencyGraph<TElement>.<>c__DisplayClass9_0 A_2, ref GenericDependencyGraph<TElement>.<>c__DisplayClass9_1 A_3)
		{
			int entryId = nodeIterationFrame.entryId;
			if (A_2.checkedSet[entryId])
			{
				return;
			}
			A_2.checkStack.Push(nodeIterationFrame);
			ref bool ptr = ref A_2.entriesInChainSet[entryId];
			if (ptr)
			{
				while (A_2.checkStack.Count > 0)
				{
					int entryId2 = A_2.checkStack.Pop().entryId;
					A_3.dest.Add(entryId2);
					if (entryId2 == entryId)
					{
						break;
					}
				}
				A_2.checkStack.Clear();
				return;
			}
			ptr = true;
		}

		// Token: 0x04000014 RID: 20
		private GenericDependencyGraph<TElement>.InternalEntry[] internalEntries;

		// Token: 0x04000015 RID: 21
		private int[] dependencyIds;

		// Token: 0x04000016 RID: 22
		private int[] dependentsIds;

		// Token: 0x04000017 RID: 23
		private int[] rootIds;

		// Token: 0x04000018 RID: 24
		private Dictionary<TElement, int> elementToId;

		// Token: 0x02000020 RID: 32
		private struct InternalEntry
		{
			// Token: 0x04000046 RID: 70
			public TElement element;

			// Token: 0x04000047 RID: 71
			public int dependenciesStartPtr;

			// Token: 0x04000048 RID: 72
			public int dependenciesLength;

			// Token: 0x04000049 RID: 73
			public int dependentsStartPtr;

			// Token: 0x0400004A RID: 74
			public int dependentsLength;
		}

		// Token: 0x02000021 RID: 33
		public struct EntryData
		{
			// Token: 0x0400004B RID: 75
			public TElement element;

			// Token: 0x0400004C RID: 76
			public TElement[] dependencies;
		}

		// Token: 0x02000022 RID: 34
		private struct NodeIterationFrame
		{
			// Token: 0x0400004D RID: 77
			public int entryId;

			// Token: 0x0400004E RID: 78
			public int entryDependencyIndex;
		}
	}
}
