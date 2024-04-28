using System;
using System.Collections.Generic;

namespace HG
{
	// Token: 0x0200000D RID: 13
	public class FilePathTree
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002C1B File Offset: 0x00000E1B
		private FilePathTree()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C2E File Offset: 0x00000E2E
		public static FilePathTree Create<T>(T paths) where T : IReadOnlyList<string>
		{
			FilePathTree filePathTree = new FilePathTree();
			filePathTree.SetPaths<T>(paths);
			return filePathTree;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C3C File Offset: 0x00000E3C
		private void SetPaths<T>(T paths) where T : IReadOnlyList<string>
		{
			this.paths = new string[paths.Count];
			for (int i = 0; i < paths.Count; i++)
			{
				this.paths[i] = paths[i];
			}
			Array.Sort<string>(this.paths, this.StringComparisonToStringComparer(FilePathTree.sortMode));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002CA8 File Offset: 0x00000EA8
		private int FindFirstIndexOfFolderFiles(string folderPath)
		{
			int num = Array.BinarySearch<string>(this.paths, folderPath, this.StringComparisonToStringComparer(FilePathTree.sortMode));
			if (num < 0)
			{
				num = -num - 1;
			}
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public void GetEntriesInFolder(string folderPath, List<string> dest)
		{
			if (!folderPath.EndsWith("/"))
			{
				throw new ArgumentException("folderPath must end with a '/'.", "folderPath");
			}
			for (int i = this.FindFirstIndexOfFolderFiles(folderPath); i < this.paths.Length; i++)
			{
				ref string ptr = ref this.paths[i];
				if (!ptr.StartsWith(folderPath, FilePathTree.sortMode))
				{
					break;
				}
				dest.Add(ptr);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D40 File Offset: 0x00000F40
		public void GetEntriesInFolder(string folderPath, Action<string> receiver)
		{
			if (!folderPath.EndsWith("/"))
			{
				throw new ArgumentException("folderPath must end with a '/'.", "folderPath");
			}
			for (int i = this.FindFirstIndexOfFolderFiles(folderPath); i < this.paths.Length; i++)
			{
				ref string ptr = ref this.paths[i];
				if (!ptr.StartsWith(folderPath, FilePathTree.sortMode))
				{
					break;
				}
				receiver(ptr);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DA8 File Offset: 0x00000FA8
		private StringComparer StringComparisonToStringComparer(StringComparison stringComparison)
		{
			switch (stringComparison)
			{
			case StringComparison.CurrentCulture:
				return StringComparer.CurrentCulture;
			case StringComparison.CurrentCultureIgnoreCase:
				return StringComparer.CurrentCultureIgnoreCase;
			case StringComparison.InvariantCulture:
				return StringComparer.InvariantCulture;
			case StringComparison.InvariantCultureIgnoreCase:
				return StringComparer.InvariantCultureIgnoreCase;
			case StringComparison.Ordinal:
				return StringComparer.Ordinal;
			case StringComparison.OrdinalIgnoreCase:
				return StringComparer.OrdinalIgnoreCase;
			default:
				throw new ArgumentOutOfRangeException("stringComparison");
			}
		}

		// Token: 0x04000012 RID: 18
		private string[] paths = Array.Empty<string>();

		// Token: 0x04000013 RID: 19
		private static StringComparison sortMode = StringComparison.OrdinalIgnoreCase;
	}
}
