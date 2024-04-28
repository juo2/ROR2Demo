using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000261 RID: 609
	public sealed class PlayerDataStorageInterface : Handle
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x000036D3 File Offset: 0x000018D3
		public PlayerDataStorageInterface()
		{
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000036DB File Offset: 0x000018DB
		public PlayerDataStorageInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00010604 File Offset: 0x0000E804
		public Result CopyFileMetadataAtIndex(CopyFileMetadataAtIndexOptions copyFileMetadataOptions, out FileMetadata outMetadata)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyFileMetadataAtIndexOptionsInternal, CopyFileMetadataAtIndexOptions>(ref zero, copyFileMetadataOptions);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_PlayerDataStorage_CopyFileMetadataAtIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero2, out outMetadata))
			{
				Bindings.EOS_PlayerDataStorage_FileMetadata_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0001064C File Offset: 0x0000E84C
		public Result CopyFileMetadataByFilename(CopyFileMetadataByFilenameOptions copyFileMetadataOptions, out FileMetadata outMetadata)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyFileMetadataByFilenameOptionsInternal, CopyFileMetadataByFilenameOptions>(ref zero, copyFileMetadataOptions);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_PlayerDataStorage_CopyFileMetadataByFilename(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero2, out outMetadata))
			{
				Bindings.EOS_PlayerDataStorage_FileMetadata_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00010694 File Offset: 0x0000E894
		public Result DeleteCache(DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeleteCacheOptionsInternal, DeleteCacheOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = new OnDeleteCacheCompleteCallbackInternal(PlayerDataStorageInterface.OnDeleteCacheCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal, Array.Empty<Delegate>());
			Result result = Bindings.EOS_PlayerDataStorage_DeleteCache(base.InnerHandle, zero, zero2, onDeleteCacheCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public void DeleteFile(DeleteFileOptions deleteOptions, object clientData, OnDeleteFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeleteFileOptionsInternal, DeleteFileOptions>(ref zero, deleteOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnDeleteFileCompleteCallbackInternal onDeleteFileCompleteCallbackInternal = new OnDeleteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDeleteFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onDeleteFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_DeleteFile(base.InnerHandle, zero, zero2, onDeleteFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0001073C File Offset: 0x0000E93C
		public void DuplicateFile(DuplicateFileOptions duplicateOptions, object clientData, OnDuplicateFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DuplicateFileOptionsInternal, DuplicateFileOptions>(ref zero, duplicateOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnDuplicateFileCompleteCallbackInternal onDuplicateFileCompleteCallbackInternal = new OnDuplicateFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDuplicateFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onDuplicateFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_DuplicateFile(base.InnerHandle, zero, zero2, onDuplicateFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00010790 File Offset: 0x0000E990
		public Result GetFileMetadataCount(GetFileMetadataCountOptions getFileMetadataCountOptions, out int outFileMetadataCount)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetFileMetadataCountOptionsInternal, GetFileMetadataCountOptions>(ref zero, getFileMetadataCountOptions);
			outFileMetadataCount = Helper.GetDefault<int>();
			Result result = Bindings.EOS_PlayerDataStorage_GetFileMetadataCount(base.InnerHandle, zero, ref outFileMetadataCount);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public void QueryFile(QueryFileOptions queryFileOptions, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryFileOptionsInternal, QueryFileOptions>(ref zero, queryFileOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_QueryFile(base.InnerHandle, zero, zero2, onQueryFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0001081C File Offset: 0x0000EA1C
		public void QueryFileList(QueryFileListOptions queryFileListOptions, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryFileListOptionsInternal, QueryFileListOptions>(ref zero, queryFileListOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileListCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_QueryFileList(base.InnerHandle, zero, zero2, onQueryFileListCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00010870 File Offset: 0x0000EA70
		public PlayerDataStorageFileTransferRequest ReadFile(ReadFileOptions readOptions, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReadFileOptionsInternal, ReadFileOptions>(ref zero, readOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(PlayerDataStorageInterface.OnReadFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[]
			{
				readOptions.ReadFileDataCallback,
				ReadFileOptionsInternal.ReadFileDataCallback,
				readOptions.FileTransferProgressCallback,
				ReadFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr source = Bindings.EOS_PlayerDataStorage_ReadFile(base.InnerHandle, zero, zero2, onReadFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			PlayerDataStorageFileTransferRequest result;
			Helper.TryMarshalGet<PlayerDataStorageFileTransferRequest>(source, out result);
			return result;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public PlayerDataStorageFileTransferRequest WriteFile(WriteFileOptions writeOptions, object clientData, OnWriteFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<WriteFileOptionsInternal, WriteFileOptions>(ref zero, writeOptions);
			IntPtr zero2 = IntPtr.Zero;
			OnWriteFileCompleteCallbackInternal onWriteFileCompleteCallbackInternal = new OnWriteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnWriteFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onWriteFileCompleteCallbackInternal, new Delegate[]
			{
				writeOptions.WriteFileDataCallback,
				WriteFileOptionsInternal.WriteFileDataCallback,
				writeOptions.FileTransferProgressCallback,
				WriteFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr source = Bindings.EOS_PlayerDataStorage_WriteFile(base.InnerHandle, zero, zero2, onWriteFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			PlayerDataStorageFileTransferRequest result;
			Helper.TryMarshalGet<PlayerDataStorageFileTransferRequest>(source, out result);
			return result;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00010970 File Offset: 0x0000EB70
		[MonoPInvokeCallback(typeof(OnDeleteCacheCompleteCallbackInternal))]
		internal static void OnDeleteCacheCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnDeleteCacheCompleteCallback onDeleteCacheCompleteCallback;
			DeleteCacheCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDeleteCacheCompleteCallback, DeleteCacheCallbackInfoInternal, DeleteCacheCallbackInfo>(data, out onDeleteCacheCompleteCallback, out data2))
			{
				onDeleteCacheCompleteCallback(data2);
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00010990 File Offset: 0x0000EB90
		[MonoPInvokeCallback(typeof(OnDeleteFileCompleteCallbackInternal))]
		internal static void OnDeleteFileCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnDeleteFileCompleteCallback onDeleteFileCompleteCallback;
			DeleteFileCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDeleteFileCompleteCallback, DeleteFileCallbackInfoInternal, DeleteFileCallbackInfo>(data, out onDeleteFileCompleteCallback, out data2))
			{
				onDeleteFileCompleteCallback(data2);
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000109B0 File Offset: 0x0000EBB0
		[MonoPInvokeCallback(typeof(OnDuplicateFileCompleteCallbackInternal))]
		internal static void OnDuplicateFileCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnDuplicateFileCompleteCallback onDuplicateFileCompleteCallback;
			DuplicateFileCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnDuplicateFileCompleteCallback, DuplicateFileCallbackInfoInternal, DuplicateFileCallbackInfo>(data, out onDuplicateFileCompleteCallback, out data2))
			{
				onDuplicateFileCompleteCallback(data2);
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000109D0 File Offset: 0x0000EBD0
		[MonoPInvokeCallback(typeof(OnFileTransferProgressCallbackInternal))]
		internal static void OnFileTransferProgressCallbackInternalImplementation(IntPtr data)
		{
			OnFileTransferProgressCallback onFileTransferProgressCallback;
			FileTransferProgressCallbackInfo fileTransferProgressCallbackInfo;
			if (Helper.TryGetStructCallback<OnFileTransferProgressCallback, FileTransferProgressCallbackInfoInternal, FileTransferProgressCallbackInfo>(data, out onFileTransferProgressCallback, out fileTransferProgressCallbackInfo))
			{
				FileTransferProgressCallbackInfo data2;
				Helper.TryMarshalGet<FileTransferProgressCallbackInfoInternal, FileTransferProgressCallbackInfo>(data, out data2);
				onFileTransferProgressCallback(data2);
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000109FC File Offset: 0x0000EBFC
		[MonoPInvokeCallback(typeof(OnQueryFileCompleteCallbackInternal))]
		internal static void OnQueryFileCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryFileCompleteCallback onQueryFileCompleteCallback;
			QueryFileCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryFileCompleteCallback, QueryFileCallbackInfoInternal, QueryFileCallbackInfo>(data, out onQueryFileCompleteCallback, out data2))
			{
				onQueryFileCompleteCallback(data2);
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00010A1C File Offset: 0x0000EC1C
		[MonoPInvokeCallback(typeof(OnQueryFileListCompleteCallbackInternal))]
		internal static void OnQueryFileListCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnQueryFileListCompleteCallback onQueryFileListCompleteCallback;
			QueryFileListCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnQueryFileListCompleteCallback, QueryFileListCallbackInfoInternal, QueryFileListCallbackInfo>(data, out onQueryFileListCompleteCallback, out data2))
			{
				onQueryFileListCompleteCallback(data2);
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00010A3C File Offset: 0x0000EC3C
		[MonoPInvokeCallback(typeof(OnReadFileCompleteCallbackInternal))]
		internal static void OnReadFileCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnReadFileCompleteCallback onReadFileCompleteCallback;
			ReadFileCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnReadFileCompleteCallback, ReadFileCallbackInfoInternal, ReadFileCallbackInfo>(data, out onReadFileCompleteCallback, out data2))
			{
				onReadFileCompleteCallback(data2);
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00010A5C File Offset: 0x0000EC5C
		[MonoPInvokeCallback(typeof(OnReadFileDataCallbackInternal))]
		internal static ReadResult OnReadFileDataCallbackInternalImplementation(IntPtr data)
		{
			OnReadFileDataCallback onReadFileDataCallback;
			ReadFileDataCallbackInfo readFileDataCallbackInfo;
			if (Helper.TryGetStructCallback<OnReadFileDataCallback, ReadFileDataCallbackInfoInternal, ReadFileDataCallbackInfo>(data, out onReadFileDataCallback, out readFileDataCallbackInfo))
			{
				ReadFileDataCallbackInfo data2;
				Helper.TryMarshalGet<ReadFileDataCallbackInfoInternal, ReadFileDataCallbackInfo>(data, out data2);
				return onReadFileDataCallback(data2);
			}
			return Helper.GetDefault<ReadResult>();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00010A8C File Offset: 0x0000EC8C
		[MonoPInvokeCallback(typeof(OnWriteFileCompleteCallbackInternal))]
		internal static void OnWriteFileCompleteCallbackInternalImplementation(IntPtr data)
		{
			OnWriteFileCompleteCallback onWriteFileCompleteCallback;
			WriteFileCallbackInfo data2;
			if (Helper.TryGetAndRemoveCallback<OnWriteFileCompleteCallback, WriteFileCallbackInfoInternal, WriteFileCallbackInfo>(data, out onWriteFileCompleteCallback, out data2))
			{
				onWriteFileCompleteCallback(data2);
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00010AAC File Offset: 0x0000ECAC
		[MonoPInvokeCallback(typeof(OnWriteFileDataCallbackInternal))]
		internal static WriteResult OnWriteFileDataCallbackInternalImplementation(IntPtr data, IntPtr outDataBuffer, ref uint outDataWritten)
		{
			OnWriteFileDataCallback onWriteFileDataCallback;
			WriteFileDataCallbackInfo writeFileDataCallbackInfo;
			if (Helper.TryGetStructCallback<OnWriteFileDataCallback, WriteFileDataCallbackInfoInternal, WriteFileDataCallbackInfo>(data, out onWriteFileDataCallback, out writeFileDataCallbackInfo))
			{
				WriteFileDataCallbackInfo data2;
				Helper.TryMarshalGet<WriteFileDataCallbackInfoInternal, WriteFileDataCallbackInfo>(data, out data2);
				byte[] source;
				WriteResult result = onWriteFileDataCallback(data2, out source);
				Helper.TryMarshalGet<byte>(source, out outDataWritten);
				Helper.TryMarshalCopy(outDataBuffer, source);
				return result;
			}
			return Helper.GetDefault<WriteResult>();
		}

		// Token: 0x04000728 RID: 1832
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x04000729 RID: 1833
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x0400072A RID: 1834
		public const int DeletecacheoptionsApiLatest = 1;

		// Token: 0x0400072B RID: 1835
		public const int DeletefileoptionsApiLatest = 1;

		// Token: 0x0400072C RID: 1836
		public const int DuplicatefileoptionsApiLatest = 1;

		// Token: 0x0400072D RID: 1837
		public const int FileMaxSizeBytes = 67108864;

		// Token: 0x0400072E RID: 1838
		public const int FilemetadataApiLatest = 3;

		// Token: 0x0400072F RID: 1839
		public const int FilenameMaxLengthBytes = 64;

		// Token: 0x04000730 RID: 1840
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x04000731 RID: 1841
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x04000732 RID: 1842
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x04000733 RID: 1843
		public const int ReadfileoptionsApiLatest = 1;

		// Token: 0x04000734 RID: 1844
		public const int WritefileoptionsApiLatest = 1;
	}
}
