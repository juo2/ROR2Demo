using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006B RID: 107
	internal class SteamUGC : IDisposable
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000069D4 File Offset: 0x00004BD4
		internal SteamUGC(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00006A51 File Offset: 0x00004C51
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006A68 File Offset: 0x00004C68
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006A84 File Offset: 0x00004C84
		public CallbackHandle AddAppDependency(PublishedFileId_t nPublishedFileID, AppId_t nAppID, Action<AddAppDependencyResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_AddAppDependency(nPublishedFileID.Value, nAppID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return AddAppDependencyResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public CallbackHandle AddDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID, Action<AddUGCDependencyResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_AddDependency(nParentPublishedFileID.Value, nChildPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return AddUGCDependencyResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006B19 File Offset: 0x00004D19
		public bool AddExcludedTag(UGCQueryHandle_t handle, string pTagName)
		{
			return this.platform.ISteamUGC_AddExcludedTag(handle.Value, pTagName);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00006B2D File Offset: 0x00004D2D
		public bool AddItemKeyValueTag(UGCUpdateHandle_t handle, string pchKey, string pchValue)
		{
			return this.platform.ISteamUGC_AddItemKeyValueTag(handle.Value, pchKey, pchValue);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006B42 File Offset: 0x00004D42
		public bool AddItemPreviewFile(UGCUpdateHandle_t handle, string pszPreviewFile, ItemPreviewType type)
		{
			return this.platform.ISteamUGC_AddItemPreviewFile(handle.Value, pszPreviewFile, type);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006B57 File Offset: 0x00004D57
		public bool AddItemPreviewVideo(UGCUpdateHandle_t handle, string pszVideoID)
		{
			return this.platform.ISteamUGC_AddItemPreviewVideo(handle.Value, pszVideoID);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00006B6C File Offset: 0x00004D6C
		public CallbackHandle AddItemToFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_AddItemToFavorites(nAppId.Value, nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return UserFavoriteItemsListChanged_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public bool AddRequiredKeyValueTag(UGCQueryHandle_t handle, string pKey, string pValue)
		{
			return this.platform.ISteamUGC_AddRequiredKeyValueTag(handle.Value, pKey, pValue);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006BCA File Offset: 0x00004DCA
		public bool AddRequiredTag(UGCQueryHandle_t handle, string pTagName)
		{
			return this.platform.ISteamUGC_AddRequiredTag(handle.Value, pTagName);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00006BDE File Offset: 0x00004DDE
		public bool BInitWorkshopForGameServer(DepotId_t unWorkshopDepotID, string pszFolder)
		{
			return this.platform.ISteamUGC_BInitWorkshopForGameServer(unWorkshopDepotID.Value, pszFolder);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public CallbackHandle CreateItem(AppId_t nConsumerAppId, WorkshopFileType eFileType, Action<CreateItemResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_CreateItem(nConsumerAppId.Value, eFileType);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return CreateItemResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00006C38 File Offset: 0x00004E38
		public UGCQueryHandle_t CreateQueryAllUGCRequest(UGCQuery eQueryType, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			return this.platform.ISteamUGC_CreateQueryAllUGCRequest(eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID.Value, nConsumerAppID.Value, unPage);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006C58 File Offset: 0x00004E58
		public unsafe UGCQueryHandle_t CreateQueryUGCDetailsRequest(PublishedFileId_t[] pvecPublishedFileID)
		{
			uint unNumPublishedFileIDs = (uint)pvecPublishedFileID.Length;
			PublishedFileId_t* value;
			if (pvecPublishedFileID == null || pvecPublishedFileID.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &pvecPublishedFileID[0];
			}
			return this.platform.ISteamUGC_CreateQueryUGCDetailsRequest((IntPtr)((void*)value), unNumPublishedFileIDs);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006C93 File Offset: 0x00004E93
		public UGCQueryHandle_t CreateQueryUserUGCRequest(AccountID_t unAccountID, UserUGCList eListType, UGCMatchingUGCType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage)
		{
			return this.platform.ISteamUGC_CreateQueryUserUGCRequest(unAccountID.Value, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID.Value, nConsumerAppID.Value, unPage);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006CBC File Offset: 0x00004EBC
		public CallbackHandle DeleteItem(PublishedFileId_t nPublishedFileID, Action<DeleteItemResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_DeleteItem(nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return DeleteItemResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00006CFF File Offset: 0x00004EFF
		public bool DownloadItem(PublishedFileId_t nPublishedFileID, bool bHighPriority)
		{
			return this.platform.ISteamUGC_DownloadItem(nPublishedFileID.Value, bHighPriority);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00006D14 File Offset: 0x00004F14
		public CallbackHandle GetAppDependencies(PublishedFileId_t nPublishedFileID, Action<GetAppDependenciesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_GetAppDependencies(nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GetAppDependenciesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006D57 File Offset: 0x00004F57
		public bool GetItemDownloadInfo(PublishedFileId_t nPublishedFileID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			return this.platform.ISteamUGC_GetItemDownloadInfo(nPublishedFileID.Value, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006D6C File Offset: 0x00004F6C
		public bool GetItemInstallInfo(PublishedFileId_t nPublishedFileID, out ulong punSizeOnDisk, out string pchFolder, out uint punTimeStamp)
		{
			pchFolder = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchFolderSize = 4096U;
			bool flag = this.platform.ISteamUGC_GetItemInstallInfo(nPublishedFileID.Value, out punSizeOnDisk, stringBuilder, cchFolderSize, out punTimeStamp);
			if (!flag)
			{
				return flag;
			}
			pchFolder = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00006DB3 File Offset: 0x00004FB3
		public uint GetItemState(PublishedFileId_t nPublishedFileID)
		{
			return this.platform.ISteamUGC_GetItemState(nPublishedFileID.Value);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006DC6 File Offset: 0x00004FC6
		public ItemUpdateStatus GetItemUpdateProgress(UGCUpdateHandle_t handle, out ulong punBytesProcessed, out ulong punBytesTotal)
		{
			return this.platform.ISteamUGC_GetItemUpdateProgress(handle.Value, out punBytesProcessed, out punBytesTotal);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006DDB File Offset: 0x00004FDB
		public uint GetNumSubscribedItems()
		{
			return this.platform.ISteamUGC_GetNumSubscribedItems();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public bool GetQueryUGCAdditionalPreview(UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, out ItemPreviewType pPreviewType)
		{
			pchURLOrVideoID = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchURLSize = 4096U;
			pchOriginalFileName = string.Empty;
			StringBuilder stringBuilder2 = Helpers.TakeStringBuilder();
			uint cchOriginalFileNameSize = 4096U;
			bool flag = this.platform.ISteamUGC_GetQueryUGCAdditionalPreview(handle.Value, index, previewIndex, stringBuilder, cchURLSize, stringBuilder2, cchOriginalFileNameSize, out pPreviewType);
			if (!flag)
			{
				return flag;
			}
			pchOriginalFileName = stringBuilder2.ToString();
			if (!flag)
			{
				return flag;
			}
			pchURLOrVideoID = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006E58 File Offset: 0x00005058
		public unsafe bool GetQueryUGCChildren(UGCQueryHandle_t handle, uint index, PublishedFileId_t* pvecPublishedFileID, uint cMaxEntries)
		{
			return this.platform.ISteamUGC_GetQueryUGCChildren(handle.Value, index, (IntPtr)((void*)pvecPublishedFileID), cMaxEntries);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00006E74 File Offset: 0x00005074
		public bool GetQueryUGCKeyValueTag(UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue)
		{
			pchKey = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchKeySize = 4096U;
			pchValue = string.Empty;
			StringBuilder stringBuilder2 = Helpers.TakeStringBuilder();
			uint cchValueSize = 4096U;
			bool flag = this.platform.ISteamUGC_GetQueryUGCKeyValueTag(handle.Value, index, keyValueTagIndex, stringBuilder, cchKeySize, stringBuilder2, cchValueSize);
			if (!flag)
			{
				return flag;
			}
			pchValue = stringBuilder2.ToString();
			if (!flag)
			{
				return flag;
			}
			pchKey = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006EE4 File Offset: 0x000050E4
		public bool GetQueryUGCMetadata(UGCQueryHandle_t handle, uint index, out string pchMetadata)
		{
			pchMetadata = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchMetadatasize = 4096U;
			bool flag = this.platform.ISteamUGC_GetQueryUGCMetadata(handle.Value, index, stringBuilder, cchMetadatasize);
			if (!flag)
			{
				return flag;
			}
			pchMetadata = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006F29 File Offset: 0x00005129
		public uint GetQueryUGCNumAdditionalPreviews(UGCQueryHandle_t handle, uint index)
		{
			return this.platform.ISteamUGC_GetQueryUGCNumAdditionalPreviews(handle.Value, index);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006F3D File Offset: 0x0000513D
		public uint GetQueryUGCNumKeyValueTags(UGCQueryHandle_t handle, uint index)
		{
			return this.platform.ISteamUGC_GetQueryUGCNumKeyValueTags(handle.Value, index);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006F54 File Offset: 0x00005154
		public bool GetQueryUGCPreviewURL(UGCQueryHandle_t handle, uint index, out string pchURL)
		{
			pchURL = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchURLSize = 4096U;
			bool flag = this.platform.ISteamUGC_GetQueryUGCPreviewURL(handle.Value, index, stringBuilder, cchURLSize);
			if (!flag)
			{
				return flag;
			}
			pchURL = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006F99 File Offset: 0x00005199
		public bool GetQueryUGCResult(UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails)
		{
			return this.platform.ISteamUGC_GetQueryUGCResult(handle.Value, index, ref pDetails);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006FAE File Offset: 0x000051AE
		public bool GetQueryUGCStatistic(UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, out ulong pStatValue)
		{
			return this.platform.ISteamUGC_GetQueryUGCStatistic(handle.Value, index, eStatType, out pStatValue);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006FC5 File Offset: 0x000051C5
		public unsafe uint GetSubscribedItems(PublishedFileId_t* pvecPublishedFileID, uint cMaxEntries)
		{
			return this.platform.ISteamUGC_GetSubscribedItems((IntPtr)((void*)pvecPublishedFileID), cMaxEntries);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006FDC File Offset: 0x000051DC
		public CallbackHandle GetUserItemVote(PublishedFileId_t nPublishedFileID, Action<GetUserItemVoteResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_GetUserItemVote(nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GetUserItemVoteResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000701F File Offset: 0x0000521F
		public bool ReleaseQueryUGCRequest(UGCQueryHandle_t handle)
		{
			return this.platform.ISteamUGC_ReleaseQueryUGCRequest(handle.Value);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007034 File Offset: 0x00005234
		public CallbackHandle RemoveAppDependency(PublishedFileId_t nPublishedFileID, AppId_t nAppID, Action<RemoveAppDependencyResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_RemoveAppDependency(nPublishedFileID.Value, nAppID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoveAppDependencyResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007080 File Offset: 0x00005280
		public CallbackHandle RemoveDependency(PublishedFileId_t nParentPublishedFileID, PublishedFileId_t nChildPublishedFileID, Action<RemoveUGCDependencyResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_RemoveDependency(nParentPublishedFileID.Value, nChildPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoveUGCDependencyResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000070CC File Offset: 0x000052CC
		public CallbackHandle RemoveItemFromFavorites(AppId_t nAppId, PublishedFileId_t nPublishedFileID, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_RemoveItemFromFavorites(nAppId.Value, nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return UserFavoriteItemsListChanged_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007115 File Offset: 0x00005315
		public bool RemoveItemKeyValueTags(UGCUpdateHandle_t handle, string pchKey)
		{
			return this.platform.ISteamUGC_RemoveItemKeyValueTags(handle.Value, pchKey);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007129 File Offset: 0x00005329
		public bool RemoveItemPreview(UGCUpdateHandle_t handle, uint index)
		{
			return this.platform.ISteamUGC_RemoveItemPreview(handle.Value, index);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000713D File Offset: 0x0000533D
		public SteamAPICall_t RequestUGCDetails(PublishedFileId_t nPublishedFileID, uint unMaxAgeSeconds)
		{
			return this.platform.ISteamUGC_RequestUGCDetails(nPublishedFileID.Value, unMaxAgeSeconds);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007154 File Offset: 0x00005354
		public CallbackHandle SendQueryUGCRequest(UGCQueryHandle_t handle, Action<SteamUGCQueryCompleted_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_SendQueryUGCRequest(handle.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SteamUGCQueryCompleted_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007197 File Offset: 0x00005397
		public bool SetAllowCachedResponse(UGCQueryHandle_t handle, uint unMaxAgeSeconds)
		{
			return this.platform.ISteamUGC_SetAllowCachedResponse(handle.Value, unMaxAgeSeconds);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000071AB File Offset: 0x000053AB
		public bool SetCloudFileNameFilter(UGCQueryHandle_t handle, string pMatchCloudFileName)
		{
			return this.platform.ISteamUGC_SetCloudFileNameFilter(handle.Value, pMatchCloudFileName);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000071BF File Offset: 0x000053BF
		public bool SetItemContent(UGCUpdateHandle_t handle, string pszContentFolder)
		{
			return this.platform.ISteamUGC_SetItemContent(handle.Value, pszContentFolder);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000071D3 File Offset: 0x000053D3
		public bool SetItemDescription(UGCUpdateHandle_t handle, string pchDescription)
		{
			return this.platform.ISteamUGC_SetItemDescription(handle.Value, pchDescription);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000071E7 File Offset: 0x000053E7
		public bool SetItemMetadata(UGCUpdateHandle_t handle, string pchMetaData)
		{
			return this.platform.ISteamUGC_SetItemMetadata(handle.Value, pchMetaData);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000071FB File Offset: 0x000053FB
		public bool SetItemPreview(UGCUpdateHandle_t handle, string pszPreviewFile)
		{
			return this.platform.ISteamUGC_SetItemPreview(handle.Value, pszPreviewFile);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007210 File Offset: 0x00005410
		public bool SetItemTags(UGCUpdateHandle_t updateHandle, string[] pTags)
		{
			IntPtr[] array = new IntPtr[pTags.Length];
			for (int i = 0; i < pTags.Length; i++)
			{
				array[i] = Marshal.StringToHGlobalAnsi(pTags[i]);
			}
			bool result;
			try
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				SteamParamStringArray_t steamParamStringArray_t = default(SteamParamStringArray_t);
				steamParamStringArray_t.Strings = intPtr;
				steamParamStringArray_t.NumStrings = pTags.Length;
				result = this.platform.ISteamUGC_SetItemTags(updateHandle.Value, ref steamParamStringArray_t);
			}
			finally
			{
				IntPtr[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Marshal.FreeHGlobal(array2[j]);
				}
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000072C8 File Offset: 0x000054C8
		public bool SetItemTitle(UGCUpdateHandle_t handle, string pchTitle)
		{
			return this.platform.ISteamUGC_SetItemTitle(handle.Value, pchTitle);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000072DC File Offset: 0x000054DC
		public bool SetItemUpdateLanguage(UGCUpdateHandle_t handle, string pchLanguage)
		{
			return this.platform.ISteamUGC_SetItemUpdateLanguage(handle.Value, pchLanguage);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000072F0 File Offset: 0x000054F0
		public bool SetItemVisibility(UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility)
		{
			return this.platform.ISteamUGC_SetItemVisibility(handle.Value, eVisibility);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007304 File Offset: 0x00005504
		public bool SetLanguage(UGCQueryHandle_t handle, string pchLanguage)
		{
			return this.platform.ISteamUGC_SetLanguage(handle.Value, pchLanguage);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007318 File Offset: 0x00005518
		public bool SetMatchAnyTag(UGCQueryHandle_t handle, bool bMatchAnyTag)
		{
			return this.platform.ISteamUGC_SetMatchAnyTag(handle.Value, bMatchAnyTag);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000732C File Offset: 0x0000552C
		public bool SetRankedByTrendDays(UGCQueryHandle_t handle, uint unDays)
		{
			return this.platform.ISteamUGC_SetRankedByTrendDays(handle.Value, unDays);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00007340 File Offset: 0x00005540
		public bool SetReturnAdditionalPreviews(UGCQueryHandle_t handle, bool bReturnAdditionalPreviews)
		{
			return this.platform.ISteamUGC_SetReturnAdditionalPreviews(handle.Value, bReturnAdditionalPreviews);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007354 File Offset: 0x00005554
		public bool SetReturnChildren(UGCQueryHandle_t handle, bool bReturnChildren)
		{
			return this.platform.ISteamUGC_SetReturnChildren(handle.Value, bReturnChildren);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007368 File Offset: 0x00005568
		public bool SetReturnKeyValueTags(UGCQueryHandle_t handle, bool bReturnKeyValueTags)
		{
			return this.platform.ISteamUGC_SetReturnKeyValueTags(handle.Value, bReturnKeyValueTags);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000737C File Offset: 0x0000557C
		public bool SetReturnLongDescription(UGCQueryHandle_t handle, bool bReturnLongDescription)
		{
			return this.platform.ISteamUGC_SetReturnLongDescription(handle.Value, bReturnLongDescription);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007390 File Offset: 0x00005590
		public bool SetReturnMetadata(UGCQueryHandle_t handle, bool bReturnMetadata)
		{
			return this.platform.ISteamUGC_SetReturnMetadata(handle.Value, bReturnMetadata);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000073A4 File Offset: 0x000055A4
		public bool SetReturnOnlyIDs(UGCQueryHandle_t handle, bool bReturnOnlyIDs)
		{
			return this.platform.ISteamUGC_SetReturnOnlyIDs(handle.Value, bReturnOnlyIDs);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000073B8 File Offset: 0x000055B8
		public bool SetReturnPlaytimeStats(UGCQueryHandle_t handle, uint unDays)
		{
			return this.platform.ISteamUGC_SetReturnPlaytimeStats(handle.Value, unDays);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000073CC File Offset: 0x000055CC
		public bool SetReturnTotalOnly(UGCQueryHandle_t handle, bool bReturnTotalOnly)
		{
			return this.platform.ISteamUGC_SetReturnTotalOnly(handle.Value, bReturnTotalOnly);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000073E0 File Offset: 0x000055E0
		public bool SetSearchText(UGCQueryHandle_t handle, string pSearchText)
		{
			return this.platform.ISteamUGC_SetSearchText(handle.Value, pSearchText);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000073F4 File Offset: 0x000055F4
		public CallbackHandle SetUserItemVote(PublishedFileId_t nPublishedFileID, bool bVoteUp, Action<SetUserItemVoteResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_SetUserItemVote(nPublishedFileID.Value, bVoteUp);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SetUserItemVoteResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00007438 File Offset: 0x00005638
		public UGCUpdateHandle_t StartItemUpdate(AppId_t nConsumerAppId, PublishedFileId_t nPublishedFileID)
		{
			return this.platform.ISteamUGC_StartItemUpdate(nConsumerAppId.Value, nPublishedFileID.Value);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007454 File Offset: 0x00005654
		public unsafe CallbackHandle StartPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, Action<StartPlaytimeTrackingResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			uint unNumPublishedFileIDs = (uint)pvecPublishedFileID.Length;
			fixed (PublishedFileId_t[] array = pvecPublishedFileID)
			{
				PublishedFileId_t* value;
				if (pvecPublishedFileID == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				steamAPICall_t = this.platform.ISteamUGC_StartPlaytimeTracking((IntPtr)((void*)value), unNumPublishedFileIDs);
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return StartPlaytimeTrackingResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000074B8 File Offset: 0x000056B8
		public unsafe CallbackHandle StopPlaytimeTracking(PublishedFileId_t[] pvecPublishedFileID, Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			uint unNumPublishedFileIDs = (uint)pvecPublishedFileID.Length;
			fixed (PublishedFileId_t[] array = pvecPublishedFileID)
			{
				PublishedFileId_t* value;
				if (pvecPublishedFileID == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				steamAPICall_t = this.platform.ISteamUGC_StopPlaytimeTracking((IntPtr)((void*)value), unNumPublishedFileIDs);
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return StopPlaytimeTrackingResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000751C File Offset: 0x0000571C
		public CallbackHandle StopPlaytimeTrackingForAllItems(Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_StopPlaytimeTrackingForAllItems();
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return StopPlaytimeTrackingResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000755C File Offset: 0x0000575C
		public CallbackHandle SubmitItemUpdate(UGCUpdateHandle_t handle, string pchChangeNote, Action<SubmitItemUpdateResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_SubmitItemUpdate(handle.Value, pchChangeNote);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return SubmitItemUpdateResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000075A0 File Offset: 0x000057A0
		public CallbackHandle SubscribeItem(PublishedFileId_t nPublishedFileID, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_SubscribeItem(nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageSubscribePublishedFileResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000075E3 File Offset: 0x000057E3
		public void SuspendDownloads(bool bSuspend)
		{
			this.platform.ISteamUGC_SuspendDownloads(bSuspend);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000075F4 File Offset: 0x000057F4
		public CallbackHandle UnsubscribeItem(PublishedFileId_t nPublishedFileID, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamUGC_UnsubscribeItem(nPublishedFileID.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageUnsubscribePublishedFileResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007637 File Offset: 0x00005837
		public bool UpdateItemPreviewFile(UGCUpdateHandle_t handle, uint index, string pszPreviewFile)
		{
			return this.platform.ISteamUGC_UpdateItemPreviewFile(handle.Value, index, pszPreviewFile);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000764C File Offset: 0x0000584C
		public bool UpdateItemPreviewVideo(UGCUpdateHandle_t handle, uint index, string pszVideoID)
		{
			return this.platform.ISteamUGC_UpdateItemPreviewVideo(handle.Value, index, pszVideoID);
		}

		// Token: 0x04000486 RID: 1158
		internal Platform.Interface platform;

		// Token: 0x04000487 RID: 1159
		internal BaseSteamworks steamworks;
	}
}
