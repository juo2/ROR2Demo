using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009E1 RID: 2529
	public class StreamingAssetsTextDataManager : TextDataManager
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool InitializedConfigFiles
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool InitializedLocFiles
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000F28D0 File Offset: 0x000F0AD0
		public StreamingAssetsTextDataManager()
		{
			this.configFolder = Path.Combine(Application.dataPath, "Config");
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000F28F0 File Offset: 0x000F0AF0
		public override string GetConfFile(string fileName, string path)
		{
			if (RoR2Application.fileSystem != null)
			{
				using (Stream stream = RoR2Application.fileSystem.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					if (stream != null)
					{
						using (TextReader textReader = new StreamReader(stream))
						{
							return textReader.ReadToEnd();
						}
					}
				}
			}
			return "";
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000F2964 File Offset: 0x000F0B64
		public override void GetLocFiles(string folderPath, Action<string[]> callback)
		{
			List<string> list = new List<string>();
			foreach (string text in Directory.EnumerateFiles(folderPath))
			{
				if (string.Compare(Path.GetFileName(text), "language.json", StringComparison.OrdinalIgnoreCase) != 0)
				{
					string extension = Path.GetExtension(text);
					if (StreamingAssetsTextDataManager.<GetLocFiles>g__MatchesExtension|7_0(extension, ".txt") || StreamingAssetsTextDataManager.<GetLocFiles>g__MatchesExtension|7_0(extension, ".json"))
					{
						list.Add(text);
					}
				}
			}
			if (callback != null)
			{
				callback(list.ConvertAll<string>((string x) => File.ReadAllText(x)).ToArray());
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F2A20 File Offset: 0x000F0C20
		[CompilerGenerated]
		internal static bool <GetLocFiles>g__MatchesExtension|7_0(string fileExtension, string testExtension)
		{
			return string.Compare(fileExtension, testExtension, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x04003944 RID: 14660
		private readonly string configFolder;
	}
}
