using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Facepunch.Steamworks.Callbacks;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000179 RID: 377
	public class Workshop : IDisposable
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000BD0 RID: 3024 RVA: 0x00039590 File Offset: 0x00037790
		// (remove) Token: 0x06000BD1 RID: 3025 RVA: 0x000395C8 File Offset: 0x000377C8
		public event Action<ulong, Facepunch.Steamworks.Callbacks.Result> OnFileDownloaded;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000BD2 RID: 3026 RVA: 0x00039600 File Offset: 0x00037800
		// (remove) Token: 0x06000BD3 RID: 3027 RVA: 0x00039638 File Offset: 0x00037838
		public event Action<ulong> OnItemInstalled;

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00039670 File Offset: 0x00037870
		internal Workshop(BaseSteamworks steamworks, SteamUGC ugc, SteamRemoteStorage remoteStorage)
		{
			this.ugc = ugc;
			this.steamworks = steamworks;
			this.remoteStorage = remoteStorage;
			steamworks.RegisterCallback<DownloadItemResult_t>(new Action<DownloadItemResult_t>(this.onDownloadResult));
			steamworks.RegisterCallback<ItemInstalled_t>(new Action<ItemInstalled_t>(this.onItemInstalled));
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000396BC File Offset: 0x000378BC
		public void Dispose()
		{
			this.ugc = null;
			this.steamworks = null;
			this.remoteStorage = null;
			this.friends = null;
			this.OnFileDownloaded = null;
			this.OnItemInstalled = null;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000396E8 File Offset: 0x000378E8
		private void onItemInstalled(ItemInstalled_t obj)
		{
			if (this.OnItemInstalled != null && obj.AppID == Client.Instance.AppId)
			{
				this.OnItemInstalled(obj.PublishedFileId);
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00039715 File Offset: 0x00037915
		private void onDownloadResult(DownloadItemResult_t obj)
		{
			if (this.OnFileDownloaded != null && obj.AppID == Client.Instance.AppId)
			{
				this.OnFileDownloaded(obj.PublishedFileId, (Facepunch.Steamworks.Callbacks.Result)obj.Result);
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00039748 File Offset: 0x00037948
		public unsafe ulong[] GetSubscribedItemIds()
		{
			uint numSubscribedItems = this.ugc.GetNumSubscribedItems();
			ulong[] array2;
			ulong[] array = array2 = new ulong[numSubscribedItems];
			ulong* pvecPublishedFileID;
			if (array == null || array2.Length == 0)
			{
				pvecPublishedFileID = null;
			}
			else
			{
				pvecPublishedFileID = &array2[0];
			}
			this.ugc.GetSubscribedItems((PublishedFileId_t*)pvecPublishedFileID, numSubscribedItems);
			array2 = null;
			return array;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00039790 File Offset: 0x00037990
		public unsafe int GetSubscribedItemIds(List<ulong> destList)
		{
			uint num = this.ugc.GetNumSubscribedItems();
			if (num >= 1024U)
			{
				destList.AddRange(this.GetSubscribedItemIds());
				return (int)num;
			}
			if (Workshop._sSubscribedItemBuffer == null)
			{
				Workshop._sSubscribedItemBuffer = new ulong[1024];
			}
			ulong[] array;
			ulong* pvecPublishedFileID;
			if ((array = Workshop._sSubscribedItemBuffer) == null || array.Length == 0)
			{
				pvecPublishedFileID = null;
			}
			else
			{
				pvecPublishedFileID = &array[0];
			}
			num = this.ugc.GetSubscribedItems((PublishedFileId_t*)pvecPublishedFileID, 1024U);
			array = null;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				destList.Add(Workshop._sSubscribedItemBuffer[num2]);
				num2++;
			}
			return (int)num;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00039821 File Offset: 0x00037A21
		public Workshop.Query CreateQuery()
		{
			return new Workshop.Query
			{
				AppId = this.steamworks.AppId,
				workshop = this,
				friends = this.friends
			};
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0003984C File Offset: 0x00037A4C
		public Workshop.Editor CreateItem(Workshop.ItemType type = Workshop.ItemType.Community)
		{
			return this.CreateItem(this.steamworks.AppId, type);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00039860 File Offset: 0x00037A60
		public Workshop.Editor CreateItem(uint workshopUploadAppId, Workshop.ItemType type = Workshop.ItemType.Community)
		{
			return new Workshop.Editor
			{
				workshop = this,
				WorkshopUploadAppId = workshopUploadAppId,
				Type = new Workshop.ItemType?(type)
			};
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00039881 File Offset: 0x00037A81
		public Workshop.Editor EditItem(ulong itemId)
		{
			return new Workshop.Editor
			{
				workshop = this,
				Id = itemId,
				WorkshopUploadAppId = this.steamworks.AppId
			};
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000398A7 File Offset: 0x00037AA7
		public Workshop.Item GetItem(ulong itemid)
		{
			return new Workshop.Item(itemid, this);
		}

		// Token: 0x04000864 RID: 2148
		internal const ulong InvalidHandle = 18446744073709551615UL;

		// Token: 0x04000865 RID: 2149
		internal SteamUGC ugc;

		// Token: 0x04000866 RID: 2150
		internal Friends friends;

		// Token: 0x04000867 RID: 2151
		internal BaseSteamworks steamworks;

		// Token: 0x04000868 RID: 2152
		internal SteamRemoteStorage remoteStorage;

		// Token: 0x0400086B RID: 2155
		[ThreadStatic]
		private static ulong[] _sSubscribedItemBuffer;

		// Token: 0x02000296 RID: 662
		public enum Order
		{
			// Token: 0x04000C87 RID: 3207
			RankedByVote,
			// Token: 0x04000C88 RID: 3208
			RankedByPublicationDate,
			// Token: 0x04000C89 RID: 3209
			AcceptedForGameRankedByAcceptanceDate,
			// Token: 0x04000C8A RID: 3210
			RankedByTrend,
			// Token: 0x04000C8B RID: 3211
			FavoritedByFriendsRankedByPublicationDate,
			// Token: 0x04000C8C RID: 3212
			CreatedByFriendsRankedByPublicationDate,
			// Token: 0x04000C8D RID: 3213
			RankedByNumTimesReported,
			// Token: 0x04000C8E RID: 3214
			CreatedByFollowedUsersRankedByPublicationDate,
			// Token: 0x04000C8F RID: 3215
			NotYetRated,
			// Token: 0x04000C90 RID: 3216
			RankedByTotalVotesAsc,
			// Token: 0x04000C91 RID: 3217
			RankedByVotesUp,
			// Token: 0x04000C92 RID: 3218
			RankedByTextSearch,
			// Token: 0x04000C93 RID: 3219
			RankedByTotalUniqueSubscriptions
		}

		// Token: 0x02000297 RID: 663
		public enum QueryType
		{
			// Token: 0x04000C95 RID: 3221
			Items,
			// Token: 0x04000C96 RID: 3222
			MicrotransactionItems,
			// Token: 0x04000C97 RID: 3223
			SubscriptionItems,
			// Token: 0x04000C98 RID: 3224
			Collections,
			// Token: 0x04000C99 RID: 3225
			Artwork,
			// Token: 0x04000C9A RID: 3226
			Videos,
			// Token: 0x04000C9B RID: 3227
			Screenshots,
			// Token: 0x04000C9C RID: 3228
			AllGuides,
			// Token: 0x04000C9D RID: 3229
			WebGuides,
			// Token: 0x04000C9E RID: 3230
			IntegratedGuides,
			// Token: 0x04000C9F RID: 3231
			UsableInGame,
			// Token: 0x04000CA0 RID: 3232
			ControllerBindings,
			// Token: 0x04000CA1 RID: 3233
			GameManagedItems
		}

		// Token: 0x02000298 RID: 664
		public enum ItemType
		{
			// Token: 0x04000CA3 RID: 3235
			Community,
			// Token: 0x04000CA4 RID: 3236
			Microtransaction,
			// Token: 0x04000CA5 RID: 3237
			Collection,
			// Token: 0x04000CA6 RID: 3238
			Art,
			// Token: 0x04000CA7 RID: 3239
			Video,
			// Token: 0x04000CA8 RID: 3240
			Screenshot,
			// Token: 0x04000CA9 RID: 3241
			Game,
			// Token: 0x04000CAA RID: 3242
			Software,
			// Token: 0x04000CAB RID: 3243
			Concept,
			// Token: 0x04000CAC RID: 3244
			WebGuide,
			// Token: 0x04000CAD RID: 3245
			IntegratedGuide,
			// Token: 0x04000CAE RID: 3246
			Merch,
			// Token: 0x04000CAF RID: 3247
			ControllerBinding,
			// Token: 0x04000CB0 RID: 3248
			SteamworksAccessInvite,
			// Token: 0x04000CB1 RID: 3249
			SteamVideo,
			// Token: 0x04000CB2 RID: 3250
			GameManagedItem
		}

		// Token: 0x02000299 RID: 665
		public enum UserQueryType : uint
		{
			// Token: 0x04000CB4 RID: 3252
			Published,
			// Token: 0x04000CB5 RID: 3253
			VotedOn,
			// Token: 0x04000CB6 RID: 3254
			VotedUp,
			// Token: 0x04000CB7 RID: 3255
			VotedDown,
			// Token: 0x04000CB8 RID: 3256
			WillVoteLater,
			// Token: 0x04000CB9 RID: 3257
			Favorited,
			// Token: 0x04000CBA RID: 3258
			Subscribed,
			// Token: 0x04000CBB RID: 3259
			UsedOrPlayed,
			// Token: 0x04000CBC RID: 3260
			Followed
		}

		// Token: 0x0200029A RID: 666
		public class Editor
		{
			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00066968 File Offset: 0x00064B68
			// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x00066970 File Offset: 0x00064B70
			public ulong Id { get; internal set; }

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00066979 File Offset: 0x00064B79
			// (set) Token: 0x06001EDB RID: 7899 RVA: 0x00066981 File Offset: 0x00064B81
			public string Title { get; set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0006698A File Offset: 0x00064B8A
			// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00066992 File Offset: 0x00064B92
			public string Description { get; set; }

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0006699B File Offset: 0x00064B9B
			// (set) Token: 0x06001EDF RID: 7903 RVA: 0x000669A3 File Offset: 0x00064BA3
			public string Folder { get; set; }

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000669AC File Offset: 0x00064BAC
			// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x000669B4 File Offset: 0x00064BB4
			public string PreviewImage { get; set; }

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000669BD File Offset: 0x00064BBD
			// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x000669C5 File Offset: 0x00064BC5
			public List<string> Tags { get; set; } = new List<string>();

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000669CE File Offset: 0x00064BCE
			// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x000669D6 File Offset: 0x00064BD6
			public bool Publishing { get; internal set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000669DF File Offset: 0x00064BDF
			// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x000669E7 File Offset: 0x00064BE7
			public Workshop.ItemType? Type { get; set; }

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000669F0 File Offset: 0x00064BF0
			// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x000669F8 File Offset: 0x00064BF8
			public string Error { get; internal set; }

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00066A01 File Offset: 0x00064C01
			// (set) Token: 0x06001EEB RID: 7915 RVA: 0x00066A09 File Offset: 0x00064C09
			public string ChangeNote { get; set; } = "";

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00066A12 File Offset: 0x00064C12
			// (set) Token: 0x06001EED RID: 7917 RVA: 0x00066A1A File Offset: 0x00064C1A
			public uint WorkshopUploadAppId { get; set; }

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06001EEE RID: 7918 RVA: 0x00066A23 File Offset: 0x00064C23
			// (set) Token: 0x06001EEF RID: 7919 RVA: 0x00066A2B File Offset: 0x00064C2B
			public string MetaData { get; set; }

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00066A34 File Offset: 0x00064C34
			// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x00066A3C File Offset: 0x00064C3C
			public Dictionary<string, string[]> KeyValues { get; set; } = new Dictionary<string, string[]>();

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00066A45 File Offset: 0x00064C45
			// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x00066A4D File Offset: 0x00064C4D
			public Workshop.Editor.VisibilityType? Visibility { get; set; }

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00066A56 File Offset: 0x00064C56
			// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x00066A5E File Offset: 0x00064C5E
			public bool NeedToAgreeToWorkshopLegal { get; internal set; }

			// Token: 0x1400000D RID: 13
			// (add) Token: 0x06001EF6 RID: 7926 RVA: 0x00066A68 File Offset: 0x00064C68
			// (remove) Token: 0x06001EF7 RID: 7927 RVA: 0x00066AA0 File Offset: 0x00064CA0
			public event Action<Facepunch.Steamworks.Callbacks.Result> OnChangesSubmitted;

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00066AD8 File Offset: 0x00064CD8
			public double Progress
			{
				get
				{
					int num = this.BytesTotal;
					if (num == 0)
					{
						return 0.0;
					}
					return (double)this.BytesUploaded / (double)num;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00066B04 File Offset: 0x00064D04
			public int BytesUploaded
			{
				get
				{
					if (!this.Publishing)
					{
						return this.bytesUploaded;
					}
					if (this.UpdateHandle == 0UL)
					{
						return this.bytesUploaded;
					}
					ulong num = 0UL;
					ulong num2 = 0UL;
					this.workshop.steamworks.native.ugc.GetItemUpdateProgress(this.UpdateHandle, out num, out num2);
					this.bytesUploaded = Math.Max(this.bytesUploaded, (int)num);
					return this.bytesUploaded;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00066B78 File Offset: 0x00064D78
			public int BytesTotal
			{
				get
				{
					if (!this.Publishing)
					{
						return this.bytesTotal;
					}
					if (this.UpdateHandle == 0UL)
					{
						return this.bytesTotal;
					}
					ulong num = 0UL;
					ulong num2 = 0UL;
					this.workshop.steamworks.native.ugc.GetItemUpdateProgress(this.UpdateHandle, out num, out num2);
					this.bytesTotal = Math.Max(this.bytesTotal, (int)num2);
					return this.bytesTotal;
				}
			}

			// Token: 0x06001EFB RID: 7931 RVA: 0x00066BEC File Offset: 0x00064DEC
			public void Publish()
			{
				this.bytesUploaded = 0;
				this.bytesTotal = 0;
				this.Publishing = true;
				this.Error = null;
				if (this.Id == 0UL)
				{
					this.StartCreatingItem();
					return;
				}
				this.PublishChanges();
			}

			// Token: 0x06001EFC RID: 7932 RVA: 0x00066C20 File Offset: 0x00064E20
			private void StartCreatingItem()
			{
				if (this.Type == null)
				{
					throw new Exception("Editor.Type must be set when creating a new item!");
				}
				if (this.WorkshopUploadAppId == 0U)
				{
					throw new Exception("WorkshopUploadAppId should not be 0");
				}
				this.CreateItem = this.workshop.ugc.CreateItem(this.WorkshopUploadAppId, (WorkshopFileType)this.Type.Value, new Action<CreateItemResult_t, bool>(this.OnItemCreated));
			}

			// Token: 0x06001EFD RID: 7933 RVA: 0x00066C98 File Offset: 0x00064E98
			private void OnItemCreated(CreateItemResult_t obj, bool Failed)
			{
				this.NeedToAgreeToWorkshopLegal = obj.UserNeedsToAcceptWorkshopLegalAgreement;
				this.CreateItem.Dispose();
				this.CreateItem = null;
				if (obj.Result == SteamNative.Result.OK && !Failed)
				{
					this.Error = null;
					this.Id = obj.PublishedFileId;
					this.PublishChanges();
					return;
				}
				this.Error = string.Format("Error creating new file: {0} ({1})", obj.Result, obj.PublishedFileId);
				this.Publishing = false;
				Action<Facepunch.Steamworks.Callbacks.Result> onChangesSubmitted = this.OnChangesSubmitted;
				if (onChangesSubmitted == null)
				{
					return;
				}
				onChangesSubmitted((Facepunch.Steamworks.Callbacks.Result)obj.Result);
			}

			// Token: 0x06001EFE RID: 7934 RVA: 0x00066D2C File Offset: 0x00064F2C
			private void PublishChanges()
			{
				if (this.WorkshopUploadAppId == 0U)
				{
					throw new Exception("WorkshopUploadAppId should not be 0");
				}
				this.UpdateHandle = this.workshop.ugc.StartItemUpdate(this.WorkshopUploadAppId, this.Id);
				if (this.Title != null)
				{
					this.workshop.ugc.SetItemTitle(this.UpdateHandle, this.Title);
				}
				if (this.Description != null)
				{
					this.workshop.ugc.SetItemDescription(this.UpdateHandle, this.Description);
				}
				if (this.Folder != null)
				{
					if (!new DirectoryInfo(this.Folder).Exists)
					{
						throw new Exception("Folder doesn't exist (" + this.Folder + ")");
					}
					this.workshop.ugc.SetItemContent(this.UpdateHandle, this.Folder);
				}
				if (this.Tags != null && this.Tags.Count > 0)
				{
					this.workshop.ugc.SetItemTags(this.UpdateHandle, this.Tags.ToArray());
				}
				if (this.Visibility != null)
				{
					this.workshop.ugc.SetItemVisibility(this.UpdateHandle, (RemoteStoragePublishedFileVisibility)this.Visibility.Value);
				}
				if (this.PreviewImage != null)
				{
					FileInfo fileInfo = new FileInfo(this.PreviewImage);
					if (!fileInfo.Exists)
					{
						throw new Exception("PreviewImage doesn't exist (" + this.PreviewImage + ")");
					}
					if (fileInfo.Length >= 1048576L)
					{
						throw new Exception(string.Format("PreviewImage should be under 1MB ({0})", fileInfo.Length));
					}
					this.workshop.ugc.SetItemPreview(this.UpdateHandle, this.PreviewImage);
				}
				if (this.MetaData != null)
				{
					this.workshop.ugc.SetItemMetadata(this.UpdateHandle, this.MetaData);
				}
				if (this.KeyValues != null)
				{
					foreach (KeyValuePair<string, string[]> keyValuePair in this.KeyValues)
					{
						foreach (string pchValue in keyValuePair.Value)
						{
							this.workshop.ugc.AddItemKeyValueTag(this.UpdateHandle, keyValuePair.Key, pchValue);
						}
					}
				}
				this.SubmitItemUpdate = this.workshop.ugc.SubmitItemUpdate(this.UpdateHandle, this.ChangeNote, new Action<SubmitItemUpdateResult_t, bool>(this.OnChangesSubmittedInternal));
			}

			// Token: 0x06001EFF RID: 7935 RVA: 0x00066FDC File Offset: 0x000651DC
			private void OnChangesSubmittedInternal(SubmitItemUpdateResult_t obj, bool Failed)
			{
				if (Failed)
				{
					throw new Exception("CreateItemResult_t Failed");
				}
				this.UpdateHandle = 0UL;
				this.SubmitItemUpdate = null;
				this.NeedToAgreeToWorkshopLegal = obj.UserNeedsToAcceptWorkshopLegalAgreement;
				this.Publishing = false;
				this.Error = ((obj.Result != SteamNative.Result.OK) ? string.Format("Error publishing changes: {0} ({1})", obj.Result, this.NeedToAgreeToWorkshopLegal) : null);
				Action<Facepunch.Steamworks.Callbacks.Result> onChangesSubmitted = this.OnChangesSubmitted;
				if (onChangesSubmitted == null)
				{
					return;
				}
				onChangesSubmitted((Facepunch.Steamworks.Callbacks.Result)obj.Result);
			}

			// Token: 0x06001F00 RID: 7936 RVA: 0x00067066 File Offset: 0x00065266
			public void Delete()
			{
				this.workshop.ugc.DeleteItem(this.Id, null);
				this.Id = 0UL;
			}

			// Token: 0x04000CBD RID: 3261
			internal Workshop workshop;

			// Token: 0x04000CBE RID: 3262
			internal CallbackHandle CreateItem;

			// Token: 0x04000CBF RID: 3263
			internal CallbackHandle SubmitItemUpdate;

			// Token: 0x04000CC0 RID: 3264
			internal UGCUpdateHandle_t UpdateHandle;

			// Token: 0x04000CD1 RID: 3281
			private int bytesUploaded;

			// Token: 0x04000CD2 RID: 3282
			private int bytesTotal;

			// Token: 0x020002C9 RID: 713
			public enum VisibilityType
			{
				// Token: 0x04000D55 RID: 3413
				Public,
				// Token: 0x04000D56 RID: 3414
				FriendsOnly,
				// Token: 0x04000D57 RID: 3415
				Private
			}
		}

		// Token: 0x0200029B RID: 667
		public class Item
		{
			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06001F02 RID: 7938 RVA: 0x000670B6 File Offset: 0x000652B6
			// (set) Token: 0x06001F03 RID: 7939 RVA: 0x000670BE File Offset: 0x000652BE
			public string Description { get; private set; }

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06001F04 RID: 7940 RVA: 0x000670C7 File Offset: 0x000652C7
			// (set) Token: 0x06001F05 RID: 7941 RVA: 0x000670CF File Offset: 0x000652CF
			public ulong Id { get; private set; }

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06001F06 RID: 7942 RVA: 0x000670D8 File Offset: 0x000652D8
			// (set) Token: 0x06001F07 RID: 7943 RVA: 0x000670E0 File Offset: 0x000652E0
			public ulong OwnerId { get; private set; }

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000670E9 File Offset: 0x000652E9
			// (set) Token: 0x06001F09 RID: 7945 RVA: 0x000670F1 File Offset: 0x000652F1
			public float Score { get; private set; }

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000670FA File Offset: 0x000652FA
			// (set) Token: 0x06001F0B RID: 7947 RVA: 0x00067102 File Offset: 0x00065302
			public string[] Tags { get; private set; }

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0006710B File Offset: 0x0006530B
			// (set) Token: 0x06001F0D RID: 7949 RVA: 0x00067113 File Offset: 0x00065313
			public string Title { get; private set; }

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0006711C File Offset: 0x0006531C
			// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00067124 File Offset: 0x00065324
			public uint VotesDown { get; private set; }

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0006712D File Offset: 0x0006532D
			// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00067135 File Offset: 0x00065335
			public uint VotesUp { get; private set; }

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0006713E File Offset: 0x0006533E
			// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00067146 File Offset: 0x00065346
			public DateTime Modified { get; private set; }

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0006714F File Offset: 0x0006534F
			// (set) Token: 0x06001F15 RID: 7957 RVA: 0x00067157 File Offset: 0x00065357
			public DateTime Created { get; private set; }

			// Token: 0x06001F16 RID: 7958 RVA: 0x00067160 File Offset: 0x00065360
			public Item(ulong Id, Workshop workshop)
			{
				this.Id = Id;
				this.workshop = workshop;
			}

			// Token: 0x06001F17 RID: 7959 RVA: 0x00067178 File Offset: 0x00065378
			internal static Workshop.Item From(SteamUGCDetails_t details, Workshop workshop)
			{
				Workshop.Item item = new Workshop.Item(details.PublishedFileId, workshop);
				item.Title = details.Title;
				item.Description = details.Description;
				item.OwnerId = details.SteamIDOwner;
				item.Tags = (from x in details.Tags.Split(new char[]
				{
					','
				})
				select x.ToLower()).ToArray<string>();
				item.Score = details.Score;
				item.VotesUp = details.VotesUp;
				item.VotesDown = details.VotesDown;
				item.Modified = Utility.Epoch.ToDateTime(details.TimeUpdated);
				item.Created = Utility.Epoch.ToDateTime(details.TimeCreated);
				return item;
			}

			// Token: 0x06001F18 RID: 7960 RVA: 0x0006724C File Offset: 0x0006544C
			public bool Download(bool highPriority = true)
			{
				if (this.Installed)
				{
					return true;
				}
				if (this.Downloading)
				{
					return true;
				}
				if (!this.workshop.ugc.DownloadItem(this.Id, highPriority))
				{
					Console.WriteLine("Download Failed");
					return false;
				}
				this.workshop.OnFileDownloaded += this.OnFileDownloaded;
				this.workshop.OnItemInstalled += this.OnItemInstalled;
				return true;
			}

			// Token: 0x06001F19 RID: 7961 RVA: 0x000672C8 File Offset: 0x000654C8
			public void Subscribe()
			{
				this.workshop.ugc.SubscribeItem(this.Id, null);
				int subscriptionCount = this.SubscriptionCount;
				this.SubscriptionCount = subscriptionCount + 1;
			}

			// Token: 0x06001F1A RID: 7962 RVA: 0x00067304 File Offset: 0x00065504
			public void UnSubscribe()
			{
				this.workshop.ugc.UnsubscribeItem(this.Id, null);
				int subscriptionCount = this.SubscriptionCount;
				this.SubscriptionCount = subscriptionCount - 1;
			}

			// Token: 0x06001F1B RID: 7963 RVA: 0x0006733E File Offset: 0x0006553E
			private void OnFileDownloaded(ulong fileid, Facepunch.Steamworks.Callbacks.Result result)
			{
				if (fileid != this.Id)
				{
					return;
				}
				this.workshop.OnFileDownloaded -= this.OnFileDownloaded;
			}

			// Token: 0x06001F1C RID: 7964 RVA: 0x00067361 File Offset: 0x00065561
			private void OnItemInstalled(ulong fileid)
			{
				if (fileid != this.Id)
				{
					return;
				}
				this.workshop.OnItemInstalled -= this.OnItemInstalled;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06001F1D RID: 7965 RVA: 0x00067384 File Offset: 0x00065584
			public ulong BytesDownloaded
			{
				get
				{
					this.UpdateDownloadProgress();
					return this._BytesDownloaded;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00067392 File Offset: 0x00065592
			public ulong BytesTotalDownload
			{
				get
				{
					this.UpdateDownloadProgress();
					return this._BytesTotal;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000673A0 File Offset: 0x000655A0
			public double DownloadProgress
			{
				get
				{
					this.UpdateDownloadProgress();
					if (this._BytesTotal == 0UL)
					{
						return 0.0;
					}
					return this._BytesDownloaded / this._BytesTotal;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06001F20 RID: 7968 RVA: 0x000673CB File Offset: 0x000655CB
			public bool Installed
			{
				get
				{
					return (this.State & ItemState.Installed) > ItemState.None;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000673D8 File Offset: 0x000655D8
			public bool Downloading
			{
				get
				{
					return (this.State & ItemState.Downloading) > ItemState.None;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000673E6 File Offset: 0x000655E6
			public bool DownloadPending
			{
				get
				{
					return (this.State & ItemState.DownloadPending) > ItemState.None;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000673F4 File Offset: 0x000655F4
			public bool Subscribed
			{
				get
				{
					return (this.State & ItemState.Subscribed) > ItemState.None;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06001F24 RID: 7972 RVA: 0x00067401 File Offset: 0x00065601
			public bool NeedsUpdate
			{
				get
				{
					return (this.State & ItemState.NeedsUpdate) > ItemState.None;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0006740E File Offset: 0x0006560E
			private ItemState State
			{
				get
				{
					return (ItemState)this.workshop.ugc.GetItemState(this.Id);
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0006742C File Offset: 0x0006562C
			public DirectoryInfo Directory
			{
				get
				{
					if (this._directory != null)
					{
						return this._directory;
					}
					if (!this.Installed)
					{
						return null;
					}
					ulong size;
					string path;
					uint num;
					if (this.workshop.ugc.GetItemInstallInfo(this.Id, out size, out path, out num))
					{
						this._directory = new DirectoryInfo(path);
						this.Size = size;
						bool exists = this._directory.Exists;
					}
					return this._directory;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06001F27 RID: 7975 RVA: 0x0006749A File Offset: 0x0006569A
			// (set) Token: 0x06001F28 RID: 7976 RVA: 0x000674A2 File Offset: 0x000656A2
			public ulong Size { get; private set; }

			// Token: 0x06001F29 RID: 7977 RVA: 0x000674AB File Offset: 0x000656AB
			internal void UpdateDownloadProgress()
			{
				this.workshop.ugc.GetItemDownloadInfo(this.Id, out this._BytesDownloaded, out this._BytesTotal);
			}

			// Token: 0x06001F2A RID: 7978 RVA: 0x000674D8 File Offset: 0x000656D8
			public void VoteUp()
			{
				if (this.YourVote == 1)
				{
					return;
				}
				uint num;
				if (this.YourVote == -1)
				{
					num = this.VotesDown;
					this.VotesDown = num - 1U;
				}
				num = this.VotesUp;
				this.VotesUp = num + 1U;
				this.workshop.ugc.SetUserItemVote(this.Id, true, null);
				this.YourVote = 1;
			}

			// Token: 0x06001F2B RID: 7979 RVA: 0x00067540 File Offset: 0x00065740
			public void VoteDown()
			{
				if (this.YourVote == -1)
				{
					return;
				}
				uint num;
				if (this.YourVote == 1)
				{
					num = this.VotesUp;
					this.VotesUp = num - 1U;
				}
				num = this.VotesDown;
				this.VotesDown = num + 1U;
				this.workshop.ugc.SetUserItemVote(this.Id, false, null);
				this.YourVote = -1;
			}

			// Token: 0x06001F2C RID: 7980 RVA: 0x000675A5 File Offset: 0x000657A5
			public Workshop.Editor Edit()
			{
				return this.workshop.EditItem(this.Id);
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06001F2D RID: 7981 RVA: 0x000675B8 File Offset: 0x000657B8
			public string Url
			{
				get
				{
					return string.Format("http://steamcommunity.com/sharedfiles/filedetails/?source=Facepunch.Steamworks&id={0}", this.Id);
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06001F2E RID: 7982 RVA: 0x000675CF File Offset: 0x000657CF
			public string ChangelogUrl
			{
				get
				{
					return string.Format("http://steamcommunity.com/sharedfiles/filedetails/changelog/{0}", this.Id);
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000675E6 File Offset: 0x000657E6
			public string CommentsUrl
			{
				get
				{
					return string.Format("http://steamcommunity.com/sharedfiles/filedetails/comments/{0}", this.Id);
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06001F30 RID: 7984 RVA: 0x000675FD File Offset: 0x000657FD
			public string DiscussUrl
			{
				get
				{
					return string.Format("http://steamcommunity.com/sharedfiles/filedetails/discussions/{0}", this.Id);
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00067614 File Offset: 0x00065814
			public string StartsUrl
			{
				get
				{
					return string.Format("http://steamcommunity.com/sharedfiles/filedetails/stats/{0}", this.Id);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0006762B File Offset: 0x0006582B
			// (set) Token: 0x06001F33 RID: 7987 RVA: 0x00067633 File Offset: 0x00065833
			public int SubscriptionCount { get; internal set; }

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006763C File Offset: 0x0006583C
			// (set) Token: 0x06001F35 RID: 7989 RVA: 0x00067644 File Offset: 0x00065844
			public int FavouriteCount { get; internal set; }

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006764D File Offset: 0x0006584D
			// (set) Token: 0x06001F37 RID: 7991 RVA: 0x00067655 File Offset: 0x00065855
			public int FollowerCount { get; internal set; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06001F38 RID: 7992 RVA: 0x0006765E File Offset: 0x0006585E
			// (set) Token: 0x06001F39 RID: 7993 RVA: 0x00067666 File Offset: 0x00065866
			public int WebsiteViews { get; internal set; }

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0006766F File Offset: 0x0006586F
			// (set) Token: 0x06001F3B RID: 7995 RVA: 0x00067677 File Offset: 0x00065877
			public int ReportScore { get; internal set; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00067680 File Offset: 0x00065880
			// (set) Token: 0x06001F3D RID: 7997 RVA: 0x00067688 File Offset: 0x00065888
			public string PreviewImageUrl { get; internal set; }

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00067694 File Offset: 0x00065894
			public string OwnerName
			{
				get
				{
					if (this._ownerName == null && this.workshop.friends != null)
					{
						this._ownerName = this.workshop.friends.GetName(this.OwnerId);
						if (this._ownerName == "[unknown]")
						{
							this._ownerName = null;
							return string.Empty;
						}
					}
					if (this._ownerName == null)
					{
						return string.Empty;
					}
					return this._ownerName;
				}
			}

			// Token: 0x04000CD3 RID: 3283
			internal Workshop workshop;

			// Token: 0x04000CDE RID: 3294
			private DirectoryInfo _directory;

			// Token: 0x04000CE0 RID: 3296
			private ulong _BytesDownloaded;

			// Token: 0x04000CE1 RID: 3297
			private ulong _BytesTotal;

			// Token: 0x04000CE2 RID: 3298
			private int YourVote;

			// Token: 0x04000CE9 RID: 3305
			private string _ownerName;
		}

		// Token: 0x0200029C RID: 668
		public class Query : IDisposable
		{
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06001F3F RID: 7999 RVA: 0x00067705 File Offset: 0x00065905
			// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0006770D File Offset: 0x0006590D
			public uint AppId { get; set; }

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00067716 File Offset: 0x00065916
			// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0006771E File Offset: 0x0006591E
			public uint UploaderAppId { get; set; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00067727 File Offset: 0x00065927
			// (set) Token: 0x06001F44 RID: 8004 RVA: 0x0006772F File Offset: 0x0006592F
			public Workshop.QueryType QueryType { get; set; }

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00067738 File Offset: 0x00065938
			// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00067740 File Offset: 0x00065940
			public Workshop.Order Order { get; set; }

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00067749 File Offset: 0x00065949
			// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00067751 File Offset: 0x00065951
			public string SearchText { get; set; }

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0006775A File Offset: 0x0006595A
			// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00067762 File Offset: 0x00065962
			public Workshop.Item[] Items { get; set; }

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0006776B File Offset: 0x0006596B
			// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00067773 File Offset: 0x00065973
			public int TotalResults { get; set; }

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0006777C File Offset: 0x0006597C
			// (set) Token: 0x06001F4E RID: 8014 RVA: 0x00067784 File Offset: 0x00065984
			public ulong? UserId { get; set; }

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006778D File Offset: 0x0006598D
			// (set) Token: 0x06001F50 RID: 8016 RVA: 0x00067795 File Offset: 0x00065995
			public int RankedByTrendDays { get; set; }

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0006779E File Offset: 0x0006599E
			// (set) Token: 0x06001F52 RID: 8018 RVA: 0x000677A6 File Offset: 0x000659A6
			public Workshop.UserQueryType UserQueryType { get; set; }

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06001F53 RID: 8019 RVA: 0x000677AF File Offset: 0x000659AF
			// (set) Token: 0x06001F54 RID: 8020 RVA: 0x000677B7 File Offset: 0x000659B7
			public int Page { get; set; } = 1;

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06001F55 RID: 8021 RVA: 0x000677C0 File Offset: 0x000659C0
			// (set) Token: 0x06001F56 RID: 8022 RVA: 0x000677C8 File Offset: 0x000659C8
			public int PerPage { get; set; } = 50;

			// Token: 0x06001F57 RID: 8023 RVA: 0x000677D4 File Offset: 0x000659D4
			public void Run()
			{
				if (this.Callback != null)
				{
					return;
				}
				if (this.Page <= 0)
				{
					throw new Exception("Page should be 1 or above");
				}
				int num = (this.Page - 1) * this.PerPage;
				this.TotalResults = 0;
				this._resultSkip = num % 50;
				this._resultsRemain = this.PerPage;
				this._resultPage = (int)Math.Floor((double)((float)num / 50f));
				this._results = new List<Workshop.Item>();
				this.RunInternal();
			}

			// Token: 0x06001F58 RID: 8024 RVA: 0x00067854 File Offset: 0x00065A54
			private void RunInternal()
			{
				if (this.FileId.Count != 0)
				{
					PublishedFileId_t[] array = (from x in this.FileId
					select x).ToArray<PublishedFileId_t>();
					this._resultsRemain = array.Length;
					this.Handle = this.workshop.ugc.CreateQueryUGCDetailsRequest(array);
				}
				else if (this.UserId != null)
				{
					uint value = (uint)(this.UserId.Value & (ulong)-1);
					this.Handle = this.workshop.ugc.CreateQueryUserUGCRequest(value, (UserUGCList)this.UserQueryType, (UGCMatchingUGCType)this.QueryType, UserUGCListSortOrder.LastUpdatedDesc, this.UploaderAppId, this.AppId, (uint)(this._resultPage + 1));
				}
				else
				{
					this.Handle = this.workshop.ugc.CreateQueryAllUGCRequest((UGCQuery)this.Order, (UGCMatchingUGCType)this.QueryType, this.UploaderAppId, this.AppId, (uint)(this._resultPage + 1));
				}
				if (!string.IsNullOrEmpty(this.SearchText))
				{
					this.workshop.ugc.SetSearchText(this.Handle, this.SearchText);
				}
				foreach (string pTagName in this.RequireTags)
				{
					this.workshop.ugc.AddRequiredTag(this.Handle, pTagName);
				}
				if (this.RequireTags.Count > 0)
				{
					this.workshop.ugc.SetMatchAnyTag(this.Handle, !this.RequireAllTags);
				}
				if (this.RankedByTrendDays > 0)
				{
					this.workshop.ugc.SetRankedByTrendDays(this.Handle, (uint)this.RankedByTrendDays);
				}
				foreach (string pTagName2 in this.ExcludeTags)
				{
					this.workshop.ugc.AddExcludedTag(this.Handle, pTagName2);
				}
				this.Callback = this.workshop.ugc.SendQueryUGCRequest(this.Handle, new Action<SteamUGCQueryCompleted_t, bool>(this.ResultCallback));
			}

			// Token: 0x06001F59 RID: 8025 RVA: 0x00067AC4 File Offset: 0x00065CC4
			private void ResultCallback(SteamUGCQueryCompleted_t data, bool bFailed)
			{
				if (bFailed)
				{
					throw new Exception("bFailed!");
				}
				int num = 0;
				int num2 = 0;
				while ((long)num2 < (long)((ulong)data.NumResultsReturned))
				{
					if (this._resultSkip > 0)
					{
						this._resultSkip--;
					}
					else
					{
						SteamUGCDetails_t details = default(SteamUGCDetails_t);
						if (this.workshop.ugc.GetQueryUGCResult(data.Handle, (uint)num2, ref details) && !this._results.Any((Workshop.Item x) => x.Id == details.PublishedFileId))
						{
							Workshop.Item item = Workshop.Item.From(details, this.workshop);
							item.SubscriptionCount = this.GetStat(data.Handle, num2, Workshop.ItemStatistic.NumSubscriptions);
							item.FavouriteCount = this.GetStat(data.Handle, num2, Workshop.ItemStatistic.NumFavorites);
							item.FollowerCount = this.GetStat(data.Handle, num2, Workshop.ItemStatistic.NumFollowers);
							item.WebsiteViews = this.GetStat(data.Handle, num2, Workshop.ItemStatistic.NumUniqueWebsiteViews);
							item.ReportScore = this.GetStat(data.Handle, num2, Workshop.ItemStatistic.ReportScore);
							string previewImageUrl = null;
							if (this.workshop.ugc.GetQueryUGCPreviewURL(data.Handle, (uint)num2, out previewImageUrl))
							{
								item.PreviewImageUrl = previewImageUrl;
							}
							this._results.Add(item);
							this._resultsRemain--;
							num++;
							if (this._resultsRemain <= 0)
							{
								break;
							}
						}
					}
					num2++;
				}
				this.TotalResults = (((long)this.TotalResults > (long)((ulong)data.TotalMatchingResults)) ? this.TotalResults : ((int)data.TotalMatchingResults));
				this.Callback.Dispose();
				this.Callback = null;
				this._resultPage++;
				if (this._resultsRemain > 0 && num > 0)
				{
					this.RunInternal();
					return;
				}
				this.Items = this._results.ToArray();
				if (this.OnResult != null)
				{
					this.OnResult(this);
				}
			}

			// Token: 0x06001F5A RID: 8026 RVA: 0x00067CB0 File Offset: 0x00065EB0
			private int GetStat(ulong handle, int index, Workshop.ItemStatistic stat)
			{
				ulong num = 0UL;
				if (!this.workshop.ugc.GetQueryUGCStatistic(handle, (uint)index, (SteamNative.ItemStatistic)stat, out num))
				{
					return 0;
				}
				return (int)num;
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06001F5B RID: 8027 RVA: 0x00067CE0 File Offset: 0x00065EE0
			public bool IsRunning
			{
				get
				{
					return this.Callback != null;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06001F5C RID: 8028 RVA: 0x00067CEB File Offset: 0x00065EEB
			// (set) Token: 0x06001F5D RID: 8029 RVA: 0x00067CF3 File Offset: 0x00065EF3
			public List<string> RequireTags { get; set; } = new List<string>();

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00067CFC File Offset: 0x00065EFC
			// (set) Token: 0x06001F5F RID: 8031 RVA: 0x00067D04 File Offset: 0x00065F04
			public bool RequireAllTags { get; set; }

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00067D0D File Offset: 0x00065F0D
			// (set) Token: 0x06001F61 RID: 8033 RVA: 0x00067D15 File Offset: 0x00065F15
			public List<string> ExcludeTags { get; set; } = new List<string>();

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00067D1E File Offset: 0x00065F1E
			// (set) Token: 0x06001F63 RID: 8035 RVA: 0x00067D26 File Offset: 0x00065F26
			public List<ulong> FileId { get; set; } = new List<ulong>();

			// Token: 0x06001F64 RID: 8036 RVA: 0x00067D2F File Offset: 0x00065F2F
			public void Block()
			{
				this.workshop.steamworks.Update();
				while (this.IsRunning)
				{
					Task.Delay(10).Wait();
					this.workshop.steamworks.Update();
				}
			}

			// Token: 0x06001F65 RID: 8037 RVA: 0x00067D67 File Offset: 0x00065F67
			public void Dispose()
			{
			}

			// Token: 0x04000CEA RID: 3306
			internal const int SteamResponseSize = 50;

			// Token: 0x04000CEB RID: 3307
			internal UGCQueryHandle_t Handle;

			// Token: 0x04000CEC RID: 3308
			internal CallbackHandle Callback;

			// Token: 0x04000CF7 RID: 3319
			public Action<Workshop.Query> OnResult;

			// Token: 0x04000CFA RID: 3322
			internal Workshop workshop;

			// Token: 0x04000CFB RID: 3323
			internal Friends friends;

			// Token: 0x04000CFC RID: 3324
			private int _resultPage;

			// Token: 0x04000CFD RID: 3325
			private int _resultsRemain;

			// Token: 0x04000CFE RID: 3326
			private int _resultSkip;

			// Token: 0x04000CFF RID: 3327
			private List<Workshop.Item> _results;
		}

		// Token: 0x0200029D RID: 669
		private enum ItemStatistic : uint
		{
			// Token: 0x04000D05 RID: 3333
			NumSubscriptions,
			// Token: 0x04000D06 RID: 3334
			NumFavorites,
			// Token: 0x04000D07 RID: 3335
			NumFollowers,
			// Token: 0x04000D08 RID: 3336
			NumUniqueSubscriptions,
			// Token: 0x04000D09 RID: 3337
			NumUniqueFavorites,
			// Token: 0x04000D0A RID: 3338
			NumUniqueFollowers,
			// Token: 0x04000D0B RID: 3339
			NumUniqueWebsiteViews,
			// Token: 0x04000D0C RID: 3340
			ReportScore
		}
	}
}
