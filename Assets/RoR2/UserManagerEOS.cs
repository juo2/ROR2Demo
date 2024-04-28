using System;
using System.Collections.Generic;
using System.Linq;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.UserInfo;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009B1 RID: 2481
	public class UserManagerEOS : UserManager
	{
		// Token: 0x060038B1 RID: 14513 RVA: 0x000EDBAA File Offset: 0x000EBDAA
		public void InitializeUserManager()
		{
			EOSLoginManager.OnAuthLoggedIn += this.TryGetLogin;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000EDBC0 File Offset: 0x000EBDC0
		public override void GetAvatar(UserID userID, GameObject requestSender, Texture2D tex, UserManager.AvatarSize size, Action<Texture2D> onRecieved)
		{
			ulong id = 0UL;
			ProductUserId egsValue = userID.CID.egsValue;
			if (UserManagerEOS.UserIDsToAccountInfo.ContainsKey(egsValue) && ulong.TryParse(UserManagerEOS.UserIDsToAccountInfo[egsValue].AccountId, out id))
			{
				SteamUserManager.GetSteamAvatar(new UserID(id), requestSender, tex, size, onRecieved);
			}
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000EDC14 File Offset: 0x000EBE14
		public override string GetUserName()
		{
			return this.LocalUserName;
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000EDC1C File Offset: 0x000EBE1C
		public override int GetUserID()
		{
			return -1;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000EDC20 File Offset: 0x000EBE20
		public void QueryForDisplayNames(UserID[] ids, Action callback = null)
		{
			if (ids == null)
			{
				Debug.Log("Cannot query. ids is null.");
				return;
			}
			this.QueryForDisplayNames((from x in ids
			select x.CID.egsValue).ToArray<ProductUserId>(), callback);
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000EDC6C File Offset: 0x000EBE6C
		public void QueryForDisplayNames(ProductUserId productId, Action callback = null)
		{
			this.QueryForDisplayNames(new ProductUserId[]
			{
				productId
			}, callback);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000EDC80 File Offset: 0x000EBE80
		public void QueryForDisplayNames(ProductUserId[] productIds, Action callback = null)
		{
			QueryProductUserIdMappingsOptions options = new QueryProductUserIdMappingsOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				ProductUserIds = productIds
			};
			UserManagerEOS.IDQueryList.AddRange(productIds);
			UserManagerEOS.IDQueryList = UserManagerEOS.IDQueryList.Distinct<ProductUserId>().ToList<ProductUserId>();
			EOSPlatformManager.GetPlatformInterface().GetConnectInterface().QueryProductUserIdMappings(options, productIds, delegate(QueryProductUserIdMappingsCallbackInfo data)
			{
				this.OnQueryProductUserIdMappingsComplete(data);
				Action callback2 = callback;
				if (callback2 == null)
				{
					return;
				}
				callback2();
			});
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000EDCF5 File Offset: 0x000EBEF5
		private bool GetUserNameFromExternalAccountType(ProductUserId id, uint index, out ExternalAccountInfo accountInfo)
		{
			accountInfo = null;
			return EOSPlatformManager.GetPlatformInterface().GetConnectInterface().CopyProductUserExternalAccountByIndex(new CopyProductUserExternalAccountByIndexOptions
			{
				TargetUserId = id,
				ExternalAccountInfoIndex = index
			}, out accountInfo) == Result.Success;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000EDD24 File Offset: 0x000EBF24
		private void OnQueryProductUserIdMappingsComplete(QueryProductUserIdMappingsCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result result = Result.Success;
			if (resultCode.GetValueOrDefault() == result & resultCode != null)
			{
				List<ProductUserId> list = new List<ProductUserId>(UserManagerEOS.IDQueryList);
				foreach (ProductUserId productUserId in UserManagerEOS.IDQueryList)
				{
					ExternalAccountInfo value;
					if (this.GetUserNameFromExternalAccountType(productUserId, 1U, out value))
					{
						UserManagerEOS.UserIDsToAccountInfo[productUserId] = value;
						list.Remove(productUserId);
					}
					else if (this.GetUserNameFromExternalAccountType(productUserId, 0U, out value))
					{
						UserManagerEOS.UserIDsToAccountInfo[productUserId] = value;
						list.Remove(productUserId);
					}
				}
				UserManagerEOS.IDQueryList = list;
				if (UserManagerEOS.IDQueryList.Count == 0)
				{
					base.InvokeDisplayMappingCompleteAction();
					return;
				}
			}
			else
			{
				Debug.Log("Failed to get ProductUserIdMappings, result = " + data.GetResultCode().ToString());
			}
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000EDE24 File Offset: 0x000EC024
		public override string GetUserDisplayName(UserID other)
		{
			if (UserManagerEOS.UserIDsToAccountInfo.ContainsKey(other.CID.egsValue))
			{
				return UserManagerEOS.UserIDsToAccountInfo[other.CID.egsValue].DisplayName;
			}
			if (!string.IsNullOrEmpty(other.CID.egsValue.ToString()))
			{
				this.QueryForDisplayNames(new UserID[]
				{
					other
				}, null);
			}
			return string.Empty;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000EDE98 File Offset: 0x000EC098
		private void TryGetLogin(EpicAccountId obj)
		{
			UserInfoInterface userInfoInterface = EOSPlatformManager.GetPlatformInterface().GetUserInfoInterface();
			if (userInfoInterface != null && EOSLoginManager.loggedInAuthId != null)
			{
				QueryUserInfoOptions options = new QueryUserInfoOptions
				{
					LocalUserId = obj,
					TargetUserId = obj
				};
				userInfoInterface.QueryUserInfo(options, null, new OnQueryUserInfoCallback(this.OnQueryUserInfo));
			}
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000EDEF0 File Offset: 0x000EC0F0
		private void OnQueryUserInfo(QueryUserInfoCallbackInfo data)
		{
			if (data.ResultCode == Result.Success)
			{
				UserInfoInterface userInfoInterface = EOSPlatformManager.GetPlatformInterface().GetUserInfoInterface();
				if (userInfoInterface != null)
				{
					CopyUserInfoOptions options = new CopyUserInfoOptions
					{
						LocalUserId = EOSLoginManager.loggedInAuthId,
						TargetUserId = EOSLoginManager.loggedInAuthId
					};
					UserInfoData userInfoData = new UserInfoData();
					userInfoInterface.CopyUserInfo(options, out userInfoData);
					this.LocalUserName = userInfoData.DisplayName;
					Debug.Log("Got userinfo, user name = " + this.LocalUserName);
				}
			}
		}

		// Token: 0x0400386E RID: 14446
		private string LocalUserName = string.Empty;

		// Token: 0x0400386F RID: 14447
		private static Dictionary<ProductUserId, ExternalAccountInfo> UserIDsToAccountInfo = new Dictionary<ProductUserId, ExternalAccountInfo>();

		// Token: 0x04003870 RID: 14448
		private static List<ProductUserId> IDQueryList = new List<ProductUserId>();
	}
}
