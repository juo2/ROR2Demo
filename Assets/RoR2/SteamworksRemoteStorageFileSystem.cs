using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using UnityEngine;
using Zio;
using Zio.FileSystems;

namespace RoR2
{
	// Token: 0x02000A6E RID: 2670
	public class SteamworksRemoteStorageFileSystem : FileSystem
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000F054C File Offset: 0x000EE74C
		private static Client steamworksClient
		{
			get
			{
				return Client.Instance;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000FD73E File Offset: 0x000FB93E
		private static RemoteStorage remoteStorage
		{
			get
			{
				return SteamworksRemoteStorageFileSystem.steamworksClient.RemoteStorage;
			}
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000FD74C File Offset: 0x000FB94C
		public SteamworksRemoteStorageFileSystem()
		{
			this.pathToNodeMap[UPath.Root] = this.rootNode;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void CreateDirectoryImpl(UPath path)
		{
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool DirectoryExistsImpl(UPath path)
		{
			return true;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000FD7A2 File Offset: 0x000FB9A2
		protected override void MoveDirectoryImpl(UPath srcPath, UPath destPath)
		{
			this.treeIsDirty = true;
			throw new NotImplementedException();
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x00062756 File Offset: 0x00060956
		protected override void DeleteDirectoryImpl(UPath path, bool isRecursive)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000FD7A2 File Offset: 0x000FB9A2
		protected override void CopyFileImpl(UPath srcPath, UPath destPath, bool overwrite)
		{
			this.treeIsDirty = true;
			throw new NotImplementedException();
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x00062756 File Offset: 0x00060956
		protected override void ReplaceFileImpl(UPath srcPath, UPath destPath, UPath destBackupPath, bool ignoreMetadataErrors)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x000FD7B0 File Offset: 0x000FB9B0
		protected override long GetFileLengthImpl(UPath path)
		{
			int num = 0;
			SteamworksRemoteStorageFileSystem.EnterFileSystemShared();
			try
			{
				this.UpdateDirectories();
				SteamworksRemoteStorageFileSystem.FileNode fileNode = this.GetFileNode(path);
				num = ((fileNode != null) ? fileNode.GetLength() : 0);
			}
			finally
			{
				SteamworksRemoteStorageFileSystem.ExitFileSystemShared();
			}
			return (long)num;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000FD7F8 File Offset: 0x000FB9F8
		protected override bool FileExistsImpl(UPath path)
		{
			this.UpdateDirectories();
			return this.GetFileNode(path) != null;
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000FD7A2 File Offset: 0x000FB9A2
		protected override void MoveFileImpl(UPath srcPath, UPath destPath)
		{
			this.treeIsDirty = true;
			throw new NotImplementedException();
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000FD80C File Offset: 0x000FBA0C
		protected override void DeleteFileImpl(UPath path)
		{
			SteamworksRemoteStorageFileSystem.EnterFileSystemShared();
			try
			{
				this.treeIsDirty = true;
				SteamworksRemoteStorageFileSystem.FileNode fileNode = this.GetFileNode(path);
				if (fileNode != null)
				{
					fileNode.Delete();
				}
			}
			finally
			{
				SteamworksRemoteStorageFileSystem.ExitFileSystemShared();
			}
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000FD850 File Offset: 0x000FBA50
		protected override Stream OpenFileImpl(UPath path, FileMode mode, FileAccess access, FileShare share)
		{
			SteamworksRemoteStorageFileSystem.EnterFileSystemShared();
			if (!path.IsAbsolute)
			{
				throw new ArgumentException(string.Format("'{0}' must be absolute. {0} = {1}", "path", path));
			}
			Stream result;
			try
			{
				bool flag = false;
				switch (mode)
				{
				case FileMode.CreateNew:
					flag = true;
					break;
				case FileMode.Create:
					flag = true;
					break;
				case FileMode.Append:
					throw new NotImplementedException();
				}
				flag &= (access == FileAccess.Write);
				if (flag)
				{
					this.treeIsDirty = true;
					result = SteamworksRemoteStorageFileSystem.remoteStorage.CreateFile(path.ToRelative().FullName).OpenWrite();
				}
				else if (access != FileAccess.Read)
				{
					if (access != FileAccess.Write)
					{
						throw new NotImplementedException();
					}
					SteamworksRemoteStorageFileSystem.FileNode fileNode = this.GetFileNode(path);
					result = ((fileNode != null) ? fileNode.OpenWrite() : null);
				}
				else
				{
					SteamworksRemoteStorageFileSystem.FileNode fileNode2 = this.GetFileNode(path);
					result = ((fileNode2 != null) ? fileNode2.OpenRead() : null);
				}
			}
			finally
			{
				SteamworksRemoteStorageFileSystem.ExitFileSystemShared();
			}
			return result;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x00062756 File Offset: 0x00060956
		protected override FileAttributes GetAttributesImpl(UPath path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void SetAttributesImpl(UPath path, FileAttributes attributes)
		{
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x00062756 File Offset: 0x00060956
		protected override DateTime GetCreationTimeImpl(UPath path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void SetCreationTimeImpl(UPath path, DateTime time)
		{
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x00062756 File Offset: 0x00060956
		protected override DateTime GetLastAccessTimeImpl(UPath path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void SetLastAccessTimeImpl(UPath path, DateTime time)
		{
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000FD93C File Offset: 0x000FBB3C
		protected override DateTime GetLastWriteTimeImpl(UPath path)
		{
			return DateTime.FromFileTimeUtc(SteamworksRemoteStorageFileSystem.remoteStorage.OpenFile(path.ToRelative().FullName).FileTimestamp);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void SetLastWriteTimeImpl(UPath path, DateTime time)
		{
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000FD96C File Offset: 0x000FBB6C
		private SteamworksRemoteStorageFileSystem.FileNode AddFileToTree(string path)
		{
			SteamworksRemoteStorageFileSystem.FileNode fileNode = new SteamworksRemoteStorageFileSystem.FileNode(path);
			this.AddNodeToTree(fileNode);
			return fileNode;
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000FD990 File Offset: 0x000FBB90
		private SteamworksRemoteStorageFileSystem.DirectoryNode AddDirectoryToTree(UPath path)
		{
			SteamworksRemoteStorageFileSystem.DirectoryNode directoryNode = new SteamworksRemoteStorageFileSystem.DirectoryNode(path);
			this.AddNodeToTree(directoryNode);
			return directoryNode;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000FD9AC File Offset: 0x000FBBAC
		private void AddNodeToTree(SteamworksRemoteStorageFileSystem.Node newNode)
		{
			UPath directory = newNode.path.GetDirectory();
			this.GetDirectoryNode(directory).AddChild(newNode);
			this.pathToNodeMap[newNode.path] = newNode;
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000FD9E4 File Offset: 0x000FBBE4
		[CanBeNull]
		private SteamworksRemoteStorageFileSystem.DirectoryNode GetDirectoryNode(UPath directoryPath)
		{
			SteamworksRemoteStorageFileSystem.Node node;
			if (this.pathToNodeMap.TryGetValue(directoryPath, out node))
			{
				return node as SteamworksRemoteStorageFileSystem.DirectoryNode;
			}
			return this.AddDirectoryToTree(directoryPath);
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000FDA10 File Offset: 0x000FBC10
		[CanBeNull]
		private SteamworksRemoteStorageFileSystem.FileNode GetFileNode(UPath filePath)
		{
			SteamworksRemoteStorageFileSystem.Node node;
			if (this.pathToNodeMap.TryGetValue(filePath, out node))
			{
				return node as SteamworksRemoteStorageFileSystem.FileNode;
			}
			return null;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000FDA38 File Offset: 0x000FBC38
		private void UpdateDirectories()
		{
			SteamworksRemoteStorageFileSystem.EnterFileSystemShared();
			try
			{
				if (this.treeIsDirty)
				{
					this.treeIsDirty = false;
					IEnumerable<string> enumerable = from file in SteamworksRemoteStorageFileSystem.remoteStorage.Files
					select file.FileName;
					if (!enumerable.SequenceEqual(this.allFilePaths))
					{
						this.allFilePaths = enumerable.ToArray<string>();
						this.pathToNodeMap.Clear();
						this.pathToNodeMap[UPath.Root] = this.rootNode;
						this.rootNode.RemoveAllChildren();
						foreach (string path in this.allFilePaths)
						{
							this.AddFileToTree(path);
						}
					}
				}
			}
			finally
			{
				SteamworksRemoteStorageFileSystem.ExitFileSystemShared();
			}
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000FDB10 File Offset: 0x000FBD10
		private void AssertDirectory(SteamworksRemoteStorageFileSystem.Node node, UPath srcPath)
		{
			if (node is SteamworksRemoteStorageFileSystem.FileNode)
			{
				throw new IOException(string.Format("The source directory `{0}` is a file", srcPath));
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000FDB32 File Offset: 0x000FBD32
		protected override IEnumerable<UPath> EnumeratePathsImpl(UPath path, string searchPattern, SearchOption searchOption, SearchTarget searchTarget)
		{
			this.UpdateDirectories();
			SearchPattern search = SearchPattern.Parse(ref path, ref searchPattern);
			List<UPath> foldersToProcess = new List<UPath>
			{
				path
			};
			SortedSet<UPath> entries = new SortedSet<UPath>(UPath.DefaultComparerIgnoreCase);
			while (foldersToProcess.Count > 0)
			{
				UPath upath = foldersToProcess[0];
				foldersToProcess.RemoveAt(0);
				int num = 0;
				entries.Clear();
				SteamworksRemoteStorageFileSystem.EnterFileSystemShared();
				try
				{
					SteamworksRemoteStorageFileSystem.Node directoryNode = this.GetDirectoryNode(upath);
					if (upath == path)
					{
						this.AssertDirectory(directoryNode, upath);
					}
					else if (!(directoryNode is SteamworksRemoteStorageFileSystem.DirectoryNode))
					{
						continue;
					}
					SteamworksRemoteStorageFileSystem.DirectoryNode directoryNode2 = (SteamworksRemoteStorageFileSystem.DirectoryNode)directoryNode;
					for (int i = 0; i < directoryNode2.childCount; i++)
					{
						SteamworksRemoteStorageFileSystem.Node child = directoryNode2.GetChild(i);
						if (!(child is SteamworksRemoteStorageFileSystem.FileNode) || searchTarget != SearchTarget.Directory)
						{
							bool flag = search.Match(child.path);
							bool flag2 = searchOption == SearchOption.AllDirectories && child is SteamworksRemoteStorageFileSystem.DirectoryNode;
							bool flag3 = (child is SteamworksRemoteStorageFileSystem.FileNode && searchTarget != SearchTarget.Directory && flag) || (child is SteamworksRemoteStorageFileSystem.DirectoryNode && searchTarget != SearchTarget.File && flag);
							UPath item = upath / child.path;
							if (flag2)
							{
								foldersToProcess.Insert(num++, item);
							}
							if (flag3)
							{
								entries.Add(item);
							}
						}
					}
				}
				finally
				{
					SteamworksRemoteStorageFileSystem.ExitFileSystemShared();
				}
				foreach (UPath upath2 in entries)
				{
					yield return upath2;
				}
				SortedSet<UPath>.Enumerator enumerator = default(SortedSet<UPath>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000FDB5F File Offset: 0x000FBD5F
		private static void EnterFileSystemShared()
		{
			Monitor.Enter(SteamworksRemoteStorageFileSystem.globalLock);
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000FDB6B File Offset: 0x000FBD6B
		private static void ExitFileSystemShared()
		{
			Monitor.Exit(SteamworksRemoteStorageFileSystem.globalLock);
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x00062756 File Offset: 0x00060956
		protected override IFileSystemWatcher WatchImpl(UPath path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000FDB77 File Offset: 0x000FBD77
		protected override string ConvertPathToInternalImpl(UPath path)
		{
			return path.FullName;
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000FDB80 File Offset: 0x000FBD80
		protected override UPath ConvertPathFromInternalImpl(string innerPath)
		{
			return new UPath(innerPath);
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000FDB88 File Offset: 0x000FBD88
		[ConCommand(commandName = "steam_remote_storage_list_files", flags = ConVarFlags.None, helpText = "Lists the files currently being managed by Steamworks remote storage.")]
		private static void CCSteamRemoteStorageListFiles(ConCommandArgs args)
		{
			Debug.Log(string.Join("\n", (from file in SteamworksRemoteStorageFileSystem.remoteStorage.Files
			select string.Format("{0} .. {1}b", file.FileName, file.SizeInBytes)).ToArray<string>()));
		}

		// Token: 0x04003C59 RID: 15449
		private static readonly object globalLock = new object();

		// Token: 0x04003C5A RID: 15450
		private string[] allFilePaths = Array.Empty<string>();

		// Token: 0x04003C5B RID: 15451
		private readonly SteamworksRemoteStorageFileSystem.DirectoryNode rootNode = new SteamworksRemoteStorageFileSystem.DirectoryNode(UPath.Root);

		// Token: 0x04003C5C RID: 15452
		private readonly Dictionary<UPath, SteamworksRemoteStorageFileSystem.Node> pathToNodeMap = new Dictionary<UPath, SteamworksRemoteStorageFileSystem.Node>();

		// Token: 0x04003C5D RID: 15453
		private bool treeIsDirty = true;

		// Token: 0x02000A6F RID: 2671
		private struct SteamworksRemoteStoragePath : IEquatable<SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath>
		{
			// Token: 0x06003D66 RID: 15718 RVA: 0x000FDBE3 File Offset: 0x000FBDE3
			public SteamworksRemoteStoragePath(string path)
			{
				this.str = path;
			}

			// Token: 0x06003D67 RID: 15719 RVA: 0x000FDBEC File Offset: 0x000FBDEC
			public static implicit operator SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath(string str)
			{
				return new SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath(str);
			}

			// Token: 0x06003D68 RID: 15720 RVA: 0x000FDBF4 File Offset: 0x000FBDF4
			public bool Equals(SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath other)
			{
				return string.Equals(this.str, other.str);
			}

			// Token: 0x06003D69 RID: 15721 RVA: 0x000FDC08 File Offset: 0x000FBE08
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath)
				{
					SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath other = (SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06003D6A RID: 15722 RVA: 0x000FDC34 File Offset: 0x000FBE34
			public override int GetHashCode()
			{
				if (this.str == null)
				{
					return 0;
				}
				return this.str.GetHashCode();
			}

			// Token: 0x04003C5E RID: 15454
			public readonly string str;
		}

		// Token: 0x02000A70 RID: 2672
		private class Node
		{
			// Token: 0x06003D6B RID: 15723 RVA: 0x000FDC4B File Offset: 0x000FBE4B
			public Node(UPath path)
			{
				this.path = path.ToAbsolute();
			}

			// Token: 0x04003C5F RID: 15455
			public readonly UPath path;

			// Token: 0x04003C60 RID: 15456
			public SteamworksRemoteStorageFileSystem.Node parent;
		}

		// Token: 0x02000A71 RID: 2673
		private class FileNode : SteamworksRemoteStorageFileSystem.Node
		{
			// Token: 0x06003D6C RID: 15724 RVA: 0x000FDC5F File Offset: 0x000FBE5F
			public FileNode(SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath steamworksRemoteStoragePath) : base(steamworksRemoteStoragePath.str)
			{
				this.steamworksRemoteStoragePath = steamworksRemoteStoragePath;
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000FDC79 File Offset: 0x000FBE79
			private RemoteFile file
			{
				get
				{
					return SteamworksRemoteStorageFileSystem.remoteStorage.OpenFile(this.steamworksRemoteStoragePath.str);
				}
			}

			// Token: 0x06003D6E RID: 15726 RVA: 0x000FDC90 File Offset: 0x000FBE90
			public int GetLength()
			{
				return this.file.SizeInBytes;
			}

			// Token: 0x06003D6F RID: 15727 RVA: 0x000FDC9D File Offset: 0x000FBE9D
			public Stream OpenWrite()
			{
				return this.file.OpenWrite();
			}

			// Token: 0x06003D70 RID: 15728 RVA: 0x000FDCAA File Offset: 0x000FBEAA
			public Stream OpenRead()
			{
				return this.file.OpenRead();
			}

			// Token: 0x06003D71 RID: 15729 RVA: 0x000FDCB7 File Offset: 0x000FBEB7
			public void Delete()
			{
				this.file.Delete();
			}

			// Token: 0x04003C61 RID: 15457
			public readonly SteamworksRemoteStorageFileSystem.SteamworksRemoteStoragePath steamworksRemoteStoragePath;
		}

		// Token: 0x02000A72 RID: 2674
		private class DirectoryNode : SteamworksRemoteStorageFileSystem.Node
		{
			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000FDCC5 File Offset: 0x000FBEC5
			// (set) Token: 0x06003D73 RID: 15731 RVA: 0x000FDCCD File Offset: 0x000FBECD
			public int childCount { get; private set; }

			// Token: 0x06003D74 RID: 15732 RVA: 0x000FDCD6 File Offset: 0x000FBED6
			public SteamworksRemoteStorageFileSystem.Node GetChild(int i)
			{
				return this.childNodes[i];
			}

			// Token: 0x06003D75 RID: 15733 RVA: 0x000FDCE0 File Offset: 0x000FBEE0
			public void AddChild(SteamworksRemoteStorageFileSystem.Node node)
			{
				int childCount = this.childCount + 1;
				this.childCount = childCount;
				if (this.childCount > this.childNodes.Length)
				{
					Array.Resize<SteamworksRemoteStorageFileSystem.Node>(ref this.childNodes, this.childCount);
				}
				this.childNodes[this.childCount - 1] = node;
				node.parent = this;
			}

			// Token: 0x06003D76 RID: 15734 RVA: 0x000FDD38 File Offset: 0x000FBF38
			public void RemoveChildAt(int i)
			{
				if (this.childCount > 0)
				{
					this.childNodes[i].parent = null;
				}
				int num = this.childCount - 1;
				while (i < num)
				{
					this.childNodes[i] = this.childNodes[i + 1];
					i++;
				}
				if (this.childCount > 0)
				{
					this.childNodes[this.childCount - 1] = null;
				}
				int childCount = this.childCount - 1;
				this.childCount = childCount;
			}

			// Token: 0x06003D77 RID: 15735 RVA: 0x000FDDAC File Offset: 0x000FBFAC
			public void RemoveChild(SteamworksRemoteStorageFileSystem.Node node)
			{
				int num = Array.IndexOf<SteamworksRemoteStorageFileSystem.Node>(this.childNodes, node);
				if (num >= 0)
				{
					this.RemoveChildAt(num);
				}
			}

			// Token: 0x06003D78 RID: 15736 RVA: 0x000FDDD4 File Offset: 0x000FBFD4
			public void RemoveAllChildren()
			{
				for (int i = 0; i < this.childCount; i++)
				{
					this.childNodes[i].parent = null;
					this.childNodes[i] = null;
				}
				this.childCount = 0;
			}

			// Token: 0x06003D79 RID: 15737 RVA: 0x000FDE10 File Offset: 0x000FC010
			public DirectoryNode(UPath path) : base(path)
			{
			}

			// Token: 0x04003C62 RID: 15458
			private SteamworksRemoteStorageFileSystem.Node[] childNodes = Array.Empty<SteamworksRemoteStorageFileSystem.Node>();
		}
	}
}
