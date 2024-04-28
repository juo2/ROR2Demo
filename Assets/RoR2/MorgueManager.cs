using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HG;
using RoR2.ConVar;
using UnityEngine;
using Zio;

namespace RoR2
{
	// Token: 0x0200096D RID: 2413
	public static class MorgueManager
	{
		// Token: 0x060036C4 RID: 14020 RVA: 0x000E7404 File Offset: 0x000E5604
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			Run.onClientGameOverGlobal += MorgueManager.OnClientGameOverGlobal;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000E7417 File Offset: 0x000E5617
		private static void OnClientGameOverGlobal(Run run, RunReport runReport)
		{
			MorgueManager.AddRunReportToHistory(runReport);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000E7420 File Offset: 0x000E5620
		private static void AddRunReportToHistory(RunReport runReport)
		{
			MorgueManager.EnforceHistoryLimit();
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			string fileName = stringBuilder.Append(runReport.runGuid).Append(".xml").ToString();
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			using (Stream historyFile = MorgueManager.GetHistoryFile(fileName))
			{
				if (historyFile != null)
				{
					XDocument xdocument = new XDocument();
					xdocument.Add(HGXml.ToXml<RunReport>("RunReport", runReport));
					xdocument.Save(historyFile);
					historyFile.Flush();
					historyFile.Dispose();
				}
			}
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000E74AC File Offset: 0x000E56AC
		private static void EnforceHistoryLimit()
		{
			List<MorgueManager.HistoryFileInfo> list = CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.RentCollection();
			MorgueManager.GetHistoryFiles(list);
			int i = list.Count - 1;
			int num = Math.Max(MorgueManager.morgueHistoryLimit.value, 0);
			while (i >= num)
			{
				list[i].Delete();
				i--;
			}
			CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.ReturnCollection(list);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000E7500 File Offset: 0x000E5700
		private static bool RemoveOldestHistoryFile()
		{
			List<MorgueManager.HistoryFileInfo> list = CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.RentCollection();
			MorgueManager.GetHistoryFiles(list);
			DateTime t = DateTime.MaxValue;
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				MorgueManager.HistoryFileInfo historyFileInfo = list[i];
				if (historyFileInfo.lastModified < t)
				{
					num = i;
					t = historyFileInfo.lastModified;
				}
			}
			if (num != -1)
			{
				list[num].Delete();
				return true;
			}
			CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.ReturnCollection(list);
			return false;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000E7574 File Offset: 0x000E5774
		public static void LoadHistoryRunReports(List<RunReport> dest)
		{
			List<MorgueManager.HistoryFileInfo> list = CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.RentCollection();
			MorgueManager.GetHistoryFiles(list);
			list.Sort(new Comparison<MorgueManager.HistoryFileInfo>(MorgueManager.<>c.<>9.<LoadHistoryRunReports>g__CompareHistoryFileLastModified|5_0));
			for (int i = 0; i < list.Count; i++)
			{
				MorgueManager.HistoryFileInfo historyFileInfo = list[i];
				try
				{
					RunReport runReport = new RunReport();
					historyFileInfo.LoadRunReport(runReport);
					dest.Add(runReport);
				}
				catch (Exception ex)
				{
					Debug.LogFormat("Could not load RunReport \"{0}\": {1}", new object[]
					{
						historyFileInfo,
						ex
					});
				}
			}
			CollectionPool<MorgueManager.HistoryFileInfo, List<MorgueManager.HistoryFileInfo>>.ReturnCollection(list);
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000E760C File Offset: 0x000E580C
		private static IFileSystem storage
		{
			get
			{
				return RoR2Application.fileSystem;
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000E7614 File Offset: 0x000E5814
		private static Stream GetHistoryFile(string fileName)
		{
			UPath path = MorgueManager.historyDirectory / fileName;
			MorgueManager.storage.CreateDirectory(MorgueManager.historyDirectory);
			return MorgueManager.storage.OpenFile(path, FileMode.Create, FileAccess.Write, FileShare.None);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000E7650 File Offset: 0x000E5850
		private static void GetHistoryFiles(List<MorgueManager.HistoryFileInfo> dest)
		{
			foreach (UPath upath in (MorgueManager.storage.DirectoryExists(MorgueManager.historyDirectory) ? MorgueManager.storage.EnumeratePaths(MorgueManager.historyDirectory) : Enumerable.Empty<UPath>()))
			{
				if (MorgueManager.storage.FileExists(upath) && upath.GetExtensionWithDot().Equals(".xml", StringComparison.OrdinalIgnoreCase))
				{
					dest.Add(new MorgueManager.HistoryFileInfo
					{
						path = upath,
						lastModified = MorgueManager.storage.GetLastWriteTime(upath),
						fileEntry = MorgueManager.storage.GetFileEntry(upath)
					});
				}
			}
		}

		// Token: 0x04003736 RID: 14134
		private static readonly IntConVar morgueHistoryLimit = new IntConVar("morgue_history_limit", ConVarFlags.Archive, "30", "How many non-favorited entries we can store in the morgue before old ones are deleted.");

		// Token: 0x04003737 RID: 14135
		private static readonly UPath historyDirectory = new UPath("/RunReports/History/");

		// Token: 0x0200096E RID: 2414
		private struct HistoryFileInfo
		{
			// Token: 0x060036CE RID: 14030 RVA: 0x000E773F File Offset: 0x000E593F
			public void LoadRunReport(RunReport dest)
			{
				HGXml.FromXml<RunReport>(XDocument.Parse(this.fileEntry.ReadAllText()).Root, ref dest);
			}

			// Token: 0x060036CF RID: 14031 RVA: 0x000E775E File Offset: 0x000E595E
			public void Delete()
			{
				this.fileEntry.Delete();
			}

			// Token: 0x04003738 RID: 14136
			public UPath path;

			// Token: 0x04003739 RID: 14137
			public DateTime lastModified;

			// Token: 0x0400373A RID: 14138
			public FileEntry fileEntry;
		}
	}
}
