using System;
using System.IO;
using System.Text;
using Facepunch.Steamworks.Callbacks;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016D RID: 365
	public class RemoteFile
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00036E44 File Offset: 0x00035044
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00036E4C File Offset: 0x0003504C
		public bool Exists { get; internal set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00036E55 File Offset: 0x00035055
		public bool IsDownloading
		{
			get
			{
				return this._isUgc && this._isDownloading && this._downloadedData == null;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00036E72 File Offset: 0x00035072
		public bool IsDownloaded
		{
			get
			{
				return !this._isUgc || this._downloadedData != null;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00036E87 File Offset: 0x00035087
		public bool IsShared
		{
			get
			{
				return this._handle.Value > 0UL;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00036E98 File Offset: 0x00035098
		internal UGCHandle_t UGCHandle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00036EA0 File Offset: 0x000350A0
		public ulong SharingId
		{
			get
			{
				return this.UGCHandle.Value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00036EAD File Offset: 0x000350AD
		public string FileName
		{
			get
			{
				if (this._fileName != null)
				{
					return this._fileName;
				}
				this.GetUGCDetails();
				return this._fileName;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00036ECA File Offset: 0x000350CA
		public ulong OwnerId
		{
			get
			{
				if (this._ownerId != 0UL)
				{
					return this._ownerId;
				}
				this.GetUGCDetails();
				return this._ownerId;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00036EE8 File Offset: 0x000350E8
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00036F35 File Offset: 0x00035135
		public int SizeInBytes
		{
			get
			{
				if (this._sizeInBytes != -1)
				{
					return this._sizeInBytes;
				}
				if (this._isUgc)
				{
					throw new NotImplementedException();
				}
				this._sizeInBytes = this.remoteStorage.native.GetFileSize(this.FileName);
				return this._sizeInBytes;
			}
			internal set
			{
				this._sizeInBytes = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00036F40 File Offset: 0x00035140
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00036F8C File Offset: 0x0003518C
		public long FileTimestamp
		{
			get
			{
				if (this._timestamp != 0L)
				{
					return this._timestamp;
				}
				if (this._isUgc)
				{
					throw new NotImplementedException();
				}
				this._timestamp = this.remoteStorage.native.GetFileTimestamp(this.FileName);
				return this._timestamp;
			}
			internal set
			{
				this._timestamp = value;
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00036F95 File Offset: 0x00035195
		internal RemoteFile(RemoteStorage r, UGCHandle_t handle)
		{
			this.Exists = true;
			this.remoteStorage = r;
			this._isUgc = true;
			this._handle = handle;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00036FC0 File Offset: 0x000351C0
		internal RemoteFile(RemoteStorage r, string name, ulong ownerId, int sizeInBytes = -1, long timestamp = 0L)
		{
			this.remoteStorage = r;
			this._isUgc = false;
			this._fileName = name;
			this._ownerId = ownerId;
			this._sizeInBytes = sizeInBytes;
			this._timestamp = timestamp;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00036FFB File Offset: 0x000351FB
		public RemoteFileWriteStream OpenWrite()
		{
			if (this._isUgc)
			{
				throw new InvalidOperationException("Cannot write to a shared file.");
			}
			return new RemoteFileWriteStream(this.remoteStorage, this);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0003701C File Offset: 0x0003521C
		public void WriteAllBytes(byte[] buffer)
		{
			using (RemoteFileWriteStream remoteFileWriteStream = this.OpenWrite())
			{
				remoteFileWriteStream.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00037058 File Offset: 0x00035258
		public void WriteAllText(string text, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			this.WriteAllBytes(encoding.GetBytes(text));
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00037071 File Offset: 0x00035271
		public bool GetDownloadProgress(out int bytesDownloaded, out int bytesExpected)
		{
			return this.remoteStorage.native.GetUGCDownloadProgress(this._handle, out bytesDownloaded, out bytesExpected);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0003708C File Offset: 0x0003528C
		public unsafe bool Download(RemoteFile.DownloadCallback onSuccess = null, FailureCallback onFailure = null)
		{
			if (!this._isUgc)
			{
				return false;
			}
			if (this._isDownloading)
			{
				return false;
			}
			if (this.IsDownloaded)
			{
				return false;
			}
			this._isDownloading = true;
			this.remoteStorage.native.UGCDownload(this._handle, 1000U, delegate(RemoteStorageDownloadUGCResult_t result, bool error)
			{
				this._isDownloading = false;
				if (error || result.Result != SteamNative.Result.OK)
				{
					FailureCallback onFailure2 = onFailure;
					if (onFailure2 == null)
					{
						return;
					}
					onFailure2((Facepunch.Steamworks.Callbacks.Result)((result.Result == (SteamNative.Result)0) ? SteamNative.Result.IOFailure : result.Result));
					return;
				}
				else
				{
					this._ownerId = result.SteamIDOwner;
					this._sizeInBytes = result.SizeInBytes;
					this._fileName = result.PchFileName;
					this._downloadedData = new byte[this._sizeInBytes];
					byte[] array;
					byte* value;
					if ((array = this._downloadedData) == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array[0];
					}
					this.remoteStorage.native.UGCRead(this._handle, (IntPtr)((void*)value), this._sizeInBytes, 0U, UGCReadAction.ontinueReading);
					array = null;
					RemoteFile.DownloadCallback onSuccess2 = onSuccess;
					if (onSuccess2 == null)
					{
						return;
					}
					onSuccess2();
					return;
				}
			});
			return true;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00037102 File Offset: 0x00035302
		public Stream OpenRead()
		{
			return new MemoryStream(this.ReadAllBytes(), false);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00037110 File Offset: 0x00035310
		public unsafe byte[] ReadAllBytes()
		{
			if (!this._isUgc)
			{
				int sizeInBytes = this.SizeInBytes;
				byte[] array2;
				byte[] array = array2 = new byte[sizeInBytes];
				byte* value;
				if (array == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				this.remoteStorage.native.FileRead(this.FileName, (IntPtr)((void*)value), sizeInBytes);
				array2 = null;
				return array;
			}
			if (!this.IsDownloaded)
			{
				throw new Exception("Cannot read a file that hasn't been downloaded.");
			}
			return this._downloadedData;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00037184 File Offset: 0x00035384
		public string ReadAllText(Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			return encoding.GetString(this.ReadAllBytes());
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0003719C File Offset: 0x0003539C
		public bool Share(RemoteFile.ShareCallback onSuccess = null, FailureCallback onFailure = null)
		{
			if (this._isUgc)
			{
				return false;
			}
			if (this._handle.Value != 0UL)
			{
				return false;
			}
			this.remoteStorage.native.FileShare(this.FileName, delegate(RemoteStorageFileShareResult_t result, bool error)
			{
				if (!error && result.Result == SteamNative.Result.OK)
				{
					this._handle.Value = result.File;
					RemoteFile.ShareCallback onSuccess2 = onSuccess;
					if (onSuccess2 == null)
					{
						return;
					}
					onSuccess2();
					return;
				}
				else
				{
					FailureCallback onFailure2 = onFailure;
					if (onFailure2 == null)
					{
						return;
					}
					onFailure2((Facepunch.Steamworks.Callbacks.Result)((result.Result == (SteamNative.Result)0) ? SteamNative.Result.IOFailure : result.Result));
					return;
				}
			});
			return true;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00037204 File Offset: 0x00035404
		public bool Delete()
		{
			if (!this.Exists)
			{
				return false;
			}
			if (this._isUgc)
			{
				return false;
			}
			if (!this.remoteStorage.native.FileDelete(this.FileName))
			{
				return false;
			}
			this.Exists = false;
			this.remoteStorage.InvalidateFiles();
			return true;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00037252 File Offset: 0x00035452
		public bool Forget()
		{
			return this.Exists && !this._isUgc && this.remoteStorage.native.FileForget(this.FileName);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00037280 File Offset: 0x00035480
		private void GetUGCDetails()
		{
			if (!this._isUgc)
			{
				throw new InvalidOperationException();
			}
			AppId_t appId_t = new AppId_t
			{
				Value = this.remoteStorage.native.steamworks.AppId
			};
			CSteamID csteamID;
			this.remoteStorage.native.GetUGCDetails(this._handle, ref appId_t, out this._fileName, out csteamID);
			this._ownerId = csteamID.Value;
		}

		// Token: 0x0400081F RID: 2079
		internal readonly RemoteStorage remoteStorage;

		// Token: 0x04000820 RID: 2080
		private readonly bool _isUgc;

		// Token: 0x04000821 RID: 2081
		private string _fileName;

		// Token: 0x04000822 RID: 2082
		private int _sizeInBytes = -1;

		// Token: 0x04000823 RID: 2083
		private long _timestamp;

		// Token: 0x04000824 RID: 2084
		private UGCHandle_t _handle;

		// Token: 0x04000825 RID: 2085
		private ulong _ownerId;

		// Token: 0x04000826 RID: 2086
		private bool _isDownloading;

		// Token: 0x04000827 RID: 2087
		private byte[] _downloadedData;

		// Token: 0x02000278 RID: 632
		// (Invoke) Token: 0x06001E03 RID: 7683
		public delegate void DownloadCallback();

		// Token: 0x02000279 RID: 633
		// (Invoke) Token: 0x06001E07 RID: 7687
		public delegate void ShareCallback();
	}
}
