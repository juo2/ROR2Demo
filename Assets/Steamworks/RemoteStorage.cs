using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016C RID: 364
	public class RemoteStorage : IDisposable
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x00036A1D File Offset: 0x00034C1D
		private static string NormalizePath(string path)
		{
			if (!Platform.IsWindows)
			{
				return new FileInfo("/x/" + path).FullName.Substring(3);
			}
			return new FileInfo("x:/" + path).FullName.Substring(3);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00036A5D File Offset: 0x00034C5D
		internal RemoteStorage(Client c)
		{
			this.client = c;
			this.native = this.client.native.remoteStorage;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00036A94 File Offset: 0x00034C94
		public bool IsCloudEnabledForAccount
		{
			get
			{
				return this.native.IsCloudEnabledForAccount();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00036AA1 File Offset: 0x00034CA1
		public bool IsCloudEnabledForApp
		{
			get
			{
				return this.native.IsCloudEnabledForApp();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00036AAE File Offset: 0x00034CAE
		public int FileCount
		{
			get
			{
				return this.native.GetFileCount();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00036ABB File Offset: 0x00034CBB
		public IEnumerable<RemoteFile> Files
		{
			get
			{
				this.UpdateFiles();
				return this._files;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00036ACC File Offset: 0x00034CCC
		public RemoteFile CreateFile(string path)
		{
			path = RemoteStorage.NormalizePath(path);
			this.InvalidateFiles();
			return this.Files.FirstOrDefault((RemoteFile x) => x.FileName == path) ?? new RemoteFile(this, path, this.client.SteamId, 0, 0L);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00036B34 File Offset: 0x00034D34
		public RemoteFile OpenFile(string path)
		{
			path = RemoteStorage.NormalizePath(path);
			this.InvalidateFiles();
			return this.Files.FirstOrDefault((RemoteFile x) => x.FileName == path);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00036B7C File Offset: 0x00034D7C
		public RemoteFile OpenSharedFile(ulong sharingId)
		{
			return new RemoteFile(this, sharingId);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00036B8A File Offset: 0x00034D8A
		public bool WriteString(string path, string text, Encoding encoding = null)
		{
			RemoteFile remoteFile = this.CreateFile(path);
			remoteFile.WriteAllText(text, encoding);
			return remoteFile.Exists;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00036BA0 File Offset: 0x00034DA0
		public bool WriteBytes(string path, byte[] data)
		{
			RemoteFile remoteFile = this.CreateFile(path);
			remoteFile.WriteAllBytes(data);
			return remoteFile.Exists;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00036BB8 File Offset: 0x00034DB8
		public string ReadString(string path, Encoding encoding = null)
		{
			RemoteFile remoteFile = this.OpenFile(path);
			if (remoteFile == null)
			{
				return null;
			}
			return remoteFile.ReadAllText(encoding);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00036BDC File Offset: 0x00034DDC
		public byte[] ReadBytes(string path)
		{
			RemoteFile remoteFile = this.OpenFile(path);
			if (remoteFile == null)
			{
				return null;
			}
			return remoteFile.ReadAllBytes();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00036BFC File Offset: 0x00034DFC
		internal void OnWrittenNewFile(RemoteFile file)
		{
			if (this._files.Any((RemoteFile x) => x.FileName == file.FileName))
			{
				return;
			}
			this._files.Add(file);
			file.Exists = true;
			this.InvalidateFiles();
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00036C53 File Offset: 0x00034E53
		internal void InvalidateFiles()
		{
			this._filesInvalid = true;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00036C5C File Offset: 0x00034E5C
		private void UpdateFiles()
		{
			if (!this._filesInvalid)
			{
				return;
			}
			this._filesInvalid = false;
			foreach (RemoteFile remoteFile in this._files)
			{
				remoteFile.Exists = false;
			}
			int fileCount = this.FileCount;
			for (int i = 0; i < fileCount; i++)
			{
				int sizeInBytes;
				string name = RemoteStorage.NormalizePath(this.native.GetFileNameAndSize(i, out sizeInBytes));
				long fileTimestamp = this.native.GetFileTimestamp(name);
				RemoteFile remoteFile2 = this._files.FirstOrDefault((RemoteFile x) => x.FileName == name);
				if (remoteFile2 == null)
				{
					remoteFile2 = new RemoteFile(this, name, this.client.SteamId, sizeInBytes, fileTimestamp);
					this._files.Add(remoteFile2);
				}
				else
				{
					remoteFile2.SizeInBytes = sizeInBytes;
					remoteFile2.FileTimestamp = fileTimestamp;
				}
				remoteFile2.Exists = true;
			}
			for (int j = this._files.Count - 1; j >= 0; j--)
			{
				if (!this._files[j].Exists)
				{
					this._files.RemoveAt(j);
				}
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00036DA8 File Offset: 0x00034FA8
		public bool FileExists(string path)
		{
			return this.native.FileExists(path);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00036DB6 File Offset: 0x00034FB6
		public void Dispose()
		{
			this.client = null;
			this.native = null;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00036DC8 File Offset: 0x00034FC8
		public ulong QuotaUsed
		{
			get
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				if (!this.native.GetQuota(out num, out num2))
				{
					return 0UL;
				}
				return num - num2;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00036DF4 File Offset: 0x00034FF4
		public ulong QuotaTotal
		{
			get
			{
				ulong result = 0UL;
				ulong num = 0UL;
				if (!this.native.GetQuota(out result, out num))
				{
					return 0UL;
				}
				return result;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00036E1C File Offset: 0x0003501C
		public ulong QuotaRemaining
		{
			get
			{
				ulong num = 0UL;
				ulong result = 0UL;
				if (!this.native.GetQuota(out num, out result))
				{
					return 0UL;
				}
				return result;
			}
		}

		// Token: 0x0400081B RID: 2075
		internal Client client;

		// Token: 0x0400081C RID: 2076
		internal SteamRemoteStorage native;

		// Token: 0x0400081D RID: 2077
		private bool _filesInvalid = true;

		// Token: 0x0400081E RID: 2078
		private readonly List<RemoteFile> _files = new List<RemoteFile>();
	}
}
