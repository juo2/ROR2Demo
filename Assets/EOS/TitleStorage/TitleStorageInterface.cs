using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200008D RID: 141
	public sealed class TitleStorageInterface : Handle
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x000036D3 File Offset: 0x000018D3
		public TitleStorageInterface()
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000036DB File Offset: 0x000018DB
		public TitleStorageInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00006064 File Offset: 0x00004264
		public Result CopyFileMetadataAtIndex(CopyFileMetadataAtIndexOptions options, out FileMetadata outMetadata)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyFileMetadataAtIndexOptionsInternal, CopyFileMetadataAtIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_TitleStorage_CopyFileMetadataAtIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero2, out outMetadata))
			{
				Bindings.EOS_TitleStorage_FileMetadata_Release(zero2);
			}
			return result;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000060AC File Offset: 0x000042AC
		public Result CopyFileMetadataByFilename(CopyFileMetadataByFilenameOptions options, out FileMetadata outMetadata)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<CopyFileMetadataByFilenameOptionsInternal, CopyFileMetadataByFilenameOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_TitleStorage_CopyFileMetadataByFilename(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<FileMetadataInternal, FileMetadata>(zero2, out outMetadata))
			{
				Bindings.EOS_TitleStorage_FileMetadata_Release(zero2);
			}
			return result;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000060F4 File Offset: 0x000042F4
		public Result DeleteCache(DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<DeleteCacheOptionsInternal, DeleteCacheOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = new OnDeleteCacheCompleteCallbackInternal(TitleStorageInterface.OnDeleteCacheCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal, Array.Empty<Delegate>());
			Result result = Bindings.EOS_TitleStorage_DeleteCache(base.InnerHandle, zero, zero2, onDeleteCacheCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00006148 File Offset: 0x00004348
		public uint GetFileMetadataCount(GetFileMetadataCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<GetFileMetadataCountOptionsInternal, GetFileMetadataCountOptions>(ref zero, options);
			uint result = Bindings.EOS_TitleStorage_GetFileMetadataCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00006178 File Offset: 0x00004378
		public void QueryFile(QueryFileOptions options, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryFileOptionsInternal, QueryFileOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(TitleStorageInterface.OnQueryFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_TitleStorage_QueryFile(base.InnerHandle, zero, zero2, onQueryFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000061CC File Offset: 0x000043CC
		public void QueryFileList(QueryFileListOptions options, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<QueryFileListOptionsInternal, QueryFileListOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(TitleStorageInterface.OnQueryFileListCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_TitleStorage_QueryFileList(base.InnerHandle, zero, zero2, onQueryFileListCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00006220 File Offset: 0x00004420
		public TitleStorageFileTransferRequest ReadFile(ReadFileOptions options, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<ReadFileOptionsInternal, ReadFileOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(TitleStorageInterface.OnReadFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref zero2, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[]
			{
				options.ReadFileDataCallback,
				ReadFileOptionsInternal.ReadFileDataCallback,
				options.FileTransferProgressCallback,
				ReadFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr source = Bindings.EOS_TitleStorage_ReadFile(base.InnerHandle, zero, zero2, onReadFileCompleteCallbackInternal);
			Helper.TryMarshalDispose(ref zero);
			TitleStorageFileTransferRequest result;
			Helper.TryMarshalGet<TitleStorageFileTransferRequest>(source, out result);
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000062A0 File Offset: 0x000044A0
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

		// Token: 0x06000536 RID: 1334 RVA: 0x000062C0 File Offset: 0x000044C0
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

		// Token: 0x06000537 RID: 1335 RVA: 0x000062EC File Offset: 0x000044EC
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

		// Token: 0x06000538 RID: 1336 RVA: 0x0000630C File Offset: 0x0000450C
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

		// Token: 0x06000539 RID: 1337 RVA: 0x0000632C File Offset: 0x0000452C
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

		// Token: 0x0600053A RID: 1338 RVA: 0x0000634C File Offset: 0x0000454C
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

		// Token: 0x040002A1 RID: 673
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x040002A2 RID: 674
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x040002A3 RID: 675
		public const int DeletecacheoptionsApiLatest = 1;

		// Token: 0x040002A4 RID: 676
		public const int FilemetadataApiLatest = 2;

		// Token: 0x040002A5 RID: 677
		public const int FilenameMaxLengthBytes = 64;

		// Token: 0x040002A6 RID: 678
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x040002A7 RID: 679
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x040002A8 RID: 680
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x040002A9 RID: 681
		public const int ReadfileoptionsApiLatest = 1;
	}
}
