using System;
using System.Collections.Generic;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Friends;

namespace RoR2
{
	// Token: 0x0200099C RID: 2460
	public class EOSFriendsManager
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000EAB93 File Offset: 0x000E8D93
		public static List<ProductUserId> FriendsProductUserIds { get; } = new List<ProductUserId>();

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000EAB9A File Offset: 0x000E8D9A
		private static EpicAccountId LocalUserEpicAccountId
		{
			get
			{
				return EOSLoginManager.loggedInAuthId;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x000EABA1 File Offset: 0x000E8DA1
		private static ProductUserId LocalUserProductUserId
		{
			get
			{
				return EOSLoginManager.loggedInProductId;
			}
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000EABA8 File Offset: 0x000E8DA8
		public EOSFriendsManager()
		{
			EOSFriendsManager.Interface = EOSPlatformManager.GetPlatformInterface().GetFriendsInterface();
			EOSFriendsManager._connectInterface = EOSPlatformManager.GetPlatformInterface().GetConnectInterface();
			EOSLoginManager.OnAuthLoggedIn += EOSFriendsManager.Initialize;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000EABE0 File Offset: 0x000E8DE0
		private static void Initialize(EpicAccountId obj)
		{
			EOSFriendsManager.QueryFriendsRefresh(null, delegate(QueryFriendsCallbackInfo data)
			{
				EOSFriendsManager.PopulateFriendsEpicAccountIds();
			});
			EOSFriendsManager.Interface.AddNotifyFriendsUpdate(new AddNotifyFriendsUpdateOptions(), null, new OnFriendsUpdateCallback(EOSFriendsManager.FriendsUpdateHandler));
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000EAC30 File Offset: 0x000E8E30
		private static void FriendsUpdateHandler(OnFriendsUpdateInfo data)
		{
			FriendsStatus currentStatus = data.CurrentStatus;
			if (currentStatus == FriendsStatus.NotFriends)
			{
				EOSFriendsManager.FriendsProductUserIds.Remove(data.TargetUserId.ToProductUserId());
				return;
			}
			if (currentStatus != FriendsStatus.Friends)
			{
				return;
			}
			EOSFriendsManager.QueryFriendsRefresh(null, new OnQueryFriendsCallback(EOSFriendsManager.TryAddFriend));
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000EAC78 File Offset: 0x000E8E78
		private static void TryAddFriend(QueryFriendsCallbackInfo data)
		{
			ProductUserId productUserId = data.LocalUserId.ToProductUserId();
			if (productUserId != null && !EOSFriendsManager.FriendsProductUserIds.Contains(productUserId))
			{
				EOSFriendsManager.FriendsProductUserIds.Add(productUserId);
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000EACB2 File Offset: 0x000E8EB2
		private static void QueryFriendsRefresh(object clientData = null, OnQueryFriendsCallback callback = null)
		{
			EOSFriendsManager.Interface.QueryFriends(new QueryFriendsOptions
			{
				LocalUserId = EOSFriendsManager.LocalUserEpicAccountId
			}, clientData, callback);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000EACD0 File Offset: 0x000E8ED0
		private static void PopulateFriendsEpicAccountIds()
		{
			int friendsCount = EOSFriendsManager.Interface.GetFriendsCount(new GetFriendsCountOptions
			{
				LocalUserId = EOSFriendsManager.LocalUserEpicAccountId
			});
			string[] array = new string[friendsCount];
			EpicAccountId[] array2 = new EpicAccountId[friendsCount];
			for (int i = 0; i < friendsCount; i++)
			{
				EpicAccountId friendAtIndex = EOSFriendsManager.Interface.GetFriendAtIndex(new GetFriendAtIndexOptions
				{
					Index = i,
					LocalUserId = EOSFriendsManager.LocalUserEpicAccountId
				});
				array[i] = friendAtIndex.ToString();
				array2[i] = friendAtIndex;
			}
			EOSFriendsManager._connectInterface.QueryExternalAccountMappings(new QueryExternalAccountMappingsOptions
			{
				AccountIdType = ExternalAccountType.Epic,
				ExternalAccountIds = array,
				LocalUserId = EOSFriendsManager.LocalUserProductUserId
			}, array2, new OnQueryExternalAccountMappingsCallback(EOSFriendsManager.PopulateFriendsProductUserId));
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000EAD7C File Offset: 0x000E8F7C
		private static void PopulateFriendsProductUserId(QueryExternalAccountMappingsCallbackInfo data)
		{
			EpicAccountId[] array;
			if (data.ResultCode == Result.Success && (array = (data.ClientData as EpicAccountId[])) != null)
			{
				EpicAccountId[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					ProductUserId productUserId = array2[i].ToProductUserId();
					EOSFriendsManager.FriendsProductUserIds.Add(productUserId);
					UserManagerEOS userManagerEOS = PlatformSystems.userManager as UserManagerEOS;
					if (userManagerEOS != null)
					{
						userManagerEOS.QueryForDisplayNames(productUserId, null);
					}
				}
				EOSFriendsManager.FriendsProductUserIds.Add(EOSLoginManager.loggedInProductId);
				UserManagerEOS userManagerEOS2 = PlatformSystems.userManager as UserManagerEOS;
				if (userManagerEOS2 == null)
				{
					return;
				}
				userManagerEOS2.QueryForDisplayNames(EOSLoginManager.loggedInProductId, null);
			}
		}

		// Token: 0x04003802 RID: 14338
		public static FriendsInterface Interface;

		// Token: 0x04003804 RID: 14340
		private static ConnectInterface _connectInterface;
	}
}
