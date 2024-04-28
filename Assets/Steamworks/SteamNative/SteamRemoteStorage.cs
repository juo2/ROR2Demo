using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000069 RID: 105
	internal class SteamRemoteStorage : IDisposable
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00005C98 File Offset: 0x00003E98
		internal SteamRemoteStorage(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005D15 File Offset: 0x00003F15
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005D2C File Offset: 0x00003F2C
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005D48 File Offset: 0x00003F48
		public CallbackHandle CommitPublishedFileUpdate(PublishedFileUpdateHandle_t updateHandle, Action<RemoteStorageUpdatePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_CommitPublishedFileUpdate(updateHandle.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageUpdatePublishedFileResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00005D8B File Offset: 0x00003F8B
		public PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest(PublishedFileId_t unPublishedFileId)
		{
			return this.platform.ISteamRemoteStorage_CreatePublishedFileUpdateRequest(unPublishedFileId.Value);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public CallbackHandle DeletePublishedFile(PublishedFileId_t unPublishedFileId, Action<RemoteStorageDeletePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_DeletePublishedFile(unPublishedFileId.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageDeletePublishedFileResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public CallbackHandle EnumeratePublishedFilesByUserAction(WorkshopFileAction eAction, uint unStartIndex, Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(eAction, unStartIndex);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageEnumeratePublishedFilesByUserActionResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00005E24 File Offset: 0x00004024
		public CallbackHandle EnumeratePublishedWorkshopFiles(WorkshopEnumerationType eEnumerationType, uint unStartIndex, uint unCount, uint unDays, string[] pTags, ref SteamParamStringArray_t pUserTags, Action<RemoteStorageEnumerateWorkshopFilesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			IntPtr[] array = new IntPtr[pTags.Length];
			for (int i = 0; i < pTags.Length; i++)
			{
				array[i] = Marshal.StringToHGlobalAnsi(pTags[i]);
			}
			try
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				SteamParamStringArray_t steamParamStringArray_t = default(SteamParamStringArray_t);
				steamParamStringArray_t.Strings = intPtr;
				steamParamStringArray_t.NumStrings = pTags.Length;
				steamAPICall_t = this.platform.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(eEnumerationType, unStartIndex, unCount, unDays, ref steamParamStringArray_t, ref pUserTags);
			}
			finally
			{
				IntPtr[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Marshal.FreeHGlobal(array2[j]);
				}
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageEnumerateWorkshopFilesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00005F04 File Offset: 0x00004104
		public CallbackHandle EnumerateUserPublishedFiles(uint unStartIndex, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_EnumerateUserPublishedFiles(unStartIndex);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageEnumerateUserPublishedFilesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005F44 File Offset: 0x00004144
		public CallbackHandle EnumerateUserSharedWorkshopFiles(CSteamID steamId, uint unStartIndex, string[] pRequiredTags, ref SteamParamStringArray_t pExcludedTags, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			IntPtr[] array = new IntPtr[pRequiredTags.Length];
			for (int i = 0; i < pRequiredTags.Length; i++)
			{
				array[i] = Marshal.StringToHGlobalAnsi(pRequiredTags[i]);
			}
			try
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				SteamParamStringArray_t steamParamStringArray_t = default(SteamParamStringArray_t);
				steamParamStringArray_t.Strings = intPtr;
				steamParamStringArray_t.NumStrings = pRequiredTags.Length;
				steamAPICall_t = this.platform.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(steamId.Value, unStartIndex, ref steamParamStringArray_t, ref pExcludedTags);
			}
			finally
			{
				IntPtr[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Marshal.FreeHGlobal(array2[j]);
				}
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageEnumerateUserPublishedFilesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006024 File Offset: 0x00004224
		public CallbackHandle EnumerateUserSubscribedFiles(uint unStartIndex, Action<RemoteStorageEnumerateUserSubscribedFilesResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_EnumerateUserSubscribedFiles(unStartIndex);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageEnumerateUserSubscribedFilesResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006062 File Offset: 0x00004262
		public bool FileDelete(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_FileDelete(pchFile);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006070 File Offset: 0x00004270
		public bool FileExists(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_FileExists(pchFile);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000607E File Offset: 0x0000427E
		public bool FileForget(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_FileForget(pchFile);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000608C File Offset: 0x0000428C
		public bool FilePersisted(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_FilePersisted(pchFile);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000609A File Offset: 0x0000429A
		public int FileRead(string pchFile, IntPtr pvData, int cubDataToRead)
		{
			return this.platform.ISteamRemoteStorage_FileRead(pchFile, pvData, cubDataToRead);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000060AC File Offset: 0x000042AC
		public CallbackHandle FileReadAsync(string pchFile, uint nOffset, uint cubToRead, Action<RemoteStorageFileReadAsyncComplete_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_FileReadAsync(pchFile, nOffset, cubToRead);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageFileReadAsyncComplete_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000060EE File Offset: 0x000042EE
		public bool FileReadAsyncComplete(SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead)
		{
			return this.platform.ISteamRemoteStorage_FileReadAsyncComplete(hReadCall.Value, pvBuffer, cubToRead);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006104 File Offset: 0x00004304
		public CallbackHandle FileShare(string pchFile, Action<RemoteStorageFileShareResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_FileShare(pchFile);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageFileShareResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006142 File Offset: 0x00004342
		public bool FileWrite(string pchFile, IntPtr pvData, int cubData)
		{
			return this.platform.ISteamRemoteStorage_FileWrite(pchFile, pvData, cubData);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006154 File Offset: 0x00004354
		public CallbackHandle FileWriteAsync(string pchFile, IntPtr pvData, uint cubData, Action<RemoteStorageFileWriteAsyncComplete_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_FileWriteAsync(pchFile, pvData, cubData);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageFileWriteAsyncComplete_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006196 File Offset: 0x00004396
		public bool FileWriteStreamCancel(UGCFileWriteStreamHandle_t writeHandle)
		{
			return this.platform.ISteamRemoteStorage_FileWriteStreamCancel(writeHandle.Value);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000061A9 File Offset: 0x000043A9
		public bool FileWriteStreamClose(UGCFileWriteStreamHandle_t writeHandle)
		{
			return this.platform.ISteamRemoteStorage_FileWriteStreamClose(writeHandle.Value);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000061BC File Offset: 0x000043BC
		public UGCFileWriteStreamHandle_t FileWriteStreamOpen(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_FileWriteStreamOpen(pchFile);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000061CA File Offset: 0x000043CA
		public bool FileWriteStreamWriteChunk(UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData)
		{
			return this.platform.ISteamRemoteStorage_FileWriteStreamWriteChunk(writeHandle.Value, pvData, cubData);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000061DF File Offset: 0x000043DF
		public int GetCachedUGCCount()
		{
			return this.platform.ISteamRemoteStorage_GetCachedUGCCount();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000061EC File Offset: 0x000043EC
		public UGCHandle_t GetCachedUGCHandle(int iCachedContent)
		{
			return this.platform.ISteamRemoteStorage_GetCachedUGCHandle(iCachedContent);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000061FA File Offset: 0x000043FA
		public int GetFileCount()
		{
			return this.platform.ISteamRemoteStorage_GetFileCount();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006207 File Offset: 0x00004407
		public string GetFileNameAndSize(int iFile, out int pnFileSizeInBytes)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamRemoteStorage_GetFileNameAndSize(iFile, out pnFileSizeInBytes));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000621B File Offset: 0x0000441B
		public int GetFileSize(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_GetFileSize(pchFile);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006229 File Offset: 0x00004429
		public long GetFileTimestamp(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_GetFileTimestamp(pchFile);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006238 File Offset: 0x00004438
		public CallbackHandle GetPublishedFileDetails(PublishedFileId_t unPublishedFileId, uint unMaxSecondsOld, Action<RemoteStorageGetPublishedFileDetailsResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_GetPublishedFileDetails(unPublishedFileId.Value, unMaxSecondsOld);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageGetPublishedFileDetailsResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000627C File Offset: 0x0000447C
		public CallbackHandle GetPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_GetPublishedItemVoteDetails(unPublishedFileId.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000062BF File Offset: 0x000044BF
		public bool GetQuota(out ulong pnTotalBytes, out ulong puAvailableBytes)
		{
			return this.platform.ISteamRemoteStorage_GetQuota(out pnTotalBytes, out puAvailableBytes);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000062CE File Offset: 0x000044CE
		public RemoteStoragePlatform GetSyncPlatforms(string pchFile)
		{
			return this.platform.ISteamRemoteStorage_GetSyncPlatforms(pchFile);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000062DC File Offset: 0x000044DC
		public bool GetUGCDetails(UGCHandle_t hContent, ref AppId_t pnAppID, out string ppchName, out CSteamID pSteamIDOwner)
		{
			ppchName = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int num = 4096;
			bool flag = this.platform.ISteamRemoteStorage_GetUGCDetails(hContent.Value, ref pnAppID.Value, stringBuilder, out num, out pSteamIDOwner.Value);
			if (!flag)
			{
				return flag;
			}
			ppchName = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000632E File Offset: 0x0000452E
		public bool GetUGCDownloadProgress(UGCHandle_t hContent, out int pnBytesDownloaded, out int pnBytesExpected)
		{
			return this.platform.ISteamRemoteStorage_GetUGCDownloadProgress(hContent.Value, out pnBytesDownloaded, out pnBytesExpected);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006344 File Offset: 0x00004544
		public CallbackHandle GetUserPublishedItemVoteDetails(PublishedFileId_t unPublishedFileId, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_GetUserPublishedItemVoteDetails(unPublishedFileId.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006387 File Offset: 0x00004587
		public bool IsCloudEnabledForAccount()
		{
			return this.platform.ISteamRemoteStorage_IsCloudEnabledForAccount();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006394 File Offset: 0x00004594
		public bool IsCloudEnabledForApp()
		{
			return this.platform.ISteamRemoteStorage_IsCloudEnabledForApp();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000063A4 File Offset: 0x000045A4
		public CallbackHandle PublishVideo(WorkshopVideoProvider eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, RemoteStoragePublishedFileVisibility eVisibility, string[] pTags, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			IntPtr[] array = new IntPtr[pTags.Length];
			for (int i = 0; i < pTags.Length; i++)
			{
				array[i] = Marshal.StringToHGlobalAnsi(pTags[i]);
			}
			try
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				SteamParamStringArray_t steamParamStringArray_t = default(SteamParamStringArray_t);
				steamParamStringArray_t.Strings = intPtr;
				steamParamStringArray_t.NumStrings = pTags.Length;
				steamAPICall_t = this.platform.ISteamRemoteStorage_PublishVideo(eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, ref steamParamStringArray_t);
			}
			finally
			{
				IntPtr[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Marshal.FreeHGlobal(array2[j]);
				}
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStoragePublishFileProgress_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006490 File Offset: 0x00004690
		public CallbackHandle PublishWorkshopFile(string pchFile, string pchPreviewFile, AppId_t nConsumerAppId, string pchTitle, string pchDescription, RemoteStoragePublishedFileVisibility eVisibility, string[] pTags, WorkshopFileType eWorkshopFileType, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			IntPtr[] array = new IntPtr[pTags.Length];
			for (int i = 0; i < pTags.Length; i++)
			{
				array[i] = Marshal.StringToHGlobalAnsi(pTags[i]);
			}
			try
			{
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				SteamParamStringArray_t steamParamStringArray_t = default(SteamParamStringArray_t);
				steamParamStringArray_t.Strings = intPtr;
				steamParamStringArray_t.NumStrings = pTags.Length;
				steamAPICall_t = this.platform.ISteamRemoteStorage_PublishWorkshopFile(pchFile, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, ref steamParamStringArray_t, eWorkshopFileType);
			}
			finally
			{
				IntPtr[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					Marshal.FreeHGlobal(array2[j]);
				}
			}
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStoragePublishFileProgress_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000657C File Offset: 0x0000477C
		public void SetCloudEnabledForApp(bool bEnabled)
		{
			this.platform.ISteamRemoteStorage_SetCloudEnabledForApp(bEnabled);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000658A File Offset: 0x0000478A
		public bool SetSyncPlatforms(string pchFile, RemoteStoragePlatform eRemoteStoragePlatform)
		{
			return this.platform.ISteamRemoteStorage_SetSyncPlatforms(pchFile, eRemoteStoragePlatform);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000659C File Offset: 0x0000479C
		public CallbackHandle SetUserPublishedFileAction(PublishedFileId_t unPublishedFileId, WorkshopFileAction eAction, Action<RemoteStorageSetUserPublishedFileActionResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_SetUserPublishedFileAction(unPublishedFileId.Value, eAction);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageSetUserPublishedFileActionResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000065E0 File Offset: 0x000047E0
		public CallbackHandle SubscribePublishedFile(PublishedFileId_t unPublishedFileId, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_SubscribePublishedFile(unPublishedFileId.Value);
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

		// Token: 0x06000247 RID: 583 RVA: 0x00006624 File Offset: 0x00004824
		public CallbackHandle UGCDownload(UGCHandle_t hContent, uint unPriority, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_UGCDownload(hContent.Value, unPriority);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageDownloadUGCResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006668 File Offset: 0x00004868
		public CallbackHandle UGCDownloadToLocation(UGCHandle_t hContent, string pchLocation, uint unPriority, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_UGCDownloadToLocation(hContent.Value, pchLocation, unPriority);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageDownloadUGCResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000066AF File Offset: 0x000048AF
		public int UGCRead(UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction)
		{
			return this.platform.ISteamRemoteStorage_UGCRead(hContent.Value, pvData, cubDataToRead, cOffset, eAction);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000066C8 File Offset: 0x000048C8
		public CallbackHandle UnsubscribePublishedFile(PublishedFileId_t unPublishedFileId, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_UnsubscribePublishedFile(unPublishedFileId.Value);
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

		// Token: 0x0600024B RID: 587 RVA: 0x0000670B File Offset: 0x0000490B
		public bool UpdatePublishedFileDescription(PublishedFileUpdateHandle_t updateHandle, string pchDescription)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFileDescription(updateHandle.Value, pchDescription);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000671F File Offset: 0x0000491F
		public bool UpdatePublishedFileFile(PublishedFileUpdateHandle_t updateHandle, string pchFile)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFileFile(updateHandle.Value, pchFile);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006733 File Offset: 0x00004933
		public bool UpdatePublishedFilePreviewFile(PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFilePreviewFile(updateHandle.Value, pchPreviewFile);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006747 File Offset: 0x00004947
		public bool UpdatePublishedFileSetChangeDescription(PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(updateHandle.Value, pchChangeDescription);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000675C File Offset: 0x0000495C
		public bool UpdatePublishedFileTags(PublishedFileUpdateHandle_t updateHandle, string[] pTags)
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
				result = this.platform.ISteamRemoteStorage_UpdatePublishedFileTags(updateHandle.Value, ref steamParamStringArray_t);
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

		// Token: 0x06000250 RID: 592 RVA: 0x00006814 File Offset: 0x00004A14
		public bool UpdatePublishedFileTitle(PublishedFileUpdateHandle_t updateHandle, string pchTitle)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFileTitle(updateHandle.Value, pchTitle);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00006828 File Offset: 0x00004A28
		public bool UpdatePublishedFileVisibility(PublishedFileUpdateHandle_t updateHandle, RemoteStoragePublishedFileVisibility eVisibility)
		{
			return this.platform.ISteamRemoteStorage_UpdatePublishedFileVisibility(updateHandle.Value, eVisibility);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000683C File Offset: 0x00004A3C
		public CallbackHandle UpdateUserPublishedItemVote(PublishedFileId_t unPublishedFileId, bool bVoteUp, Action<RemoteStorageUpdateUserPublishedItemVoteResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamRemoteStorage_UpdateUserPublishedItemVote(unPublishedFileId.Value, bVoteUp);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return RemoteStorageUpdateUserPublishedItemVoteResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x04000482 RID: 1154
		internal Platform.Interface platform;

		// Token: 0x04000483 RID: 1155
		internal BaseSteamworks steamworks;
	}
}
