using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Facepunch.Steamworks;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009A6 RID: 2470
	public class EOSLoginManager
	{
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x0600387F RID: 14463 RVA: 0x000ECF90 File Offset: 0x000EB190
		// (remove) Token: 0x06003880 RID: 14464 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
		public static event Action<EpicAccountId> OnAuthLoggedIn;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06003881 RID: 14465 RVA: 0x000ECFF8 File Offset: 0x000EB1F8
		// (remove) Token: 0x06003882 RID: 14466 RVA: 0x000ED02C File Offset: 0x000EB22C
		public static event Action<ProductUserId> OnConnectLoggedIn;

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000ED067 File Offset: 0x000EB267
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x000ED05F File Offset: 0x000EB25F
		public static EOSLoginManager.EOSLoginState loginState { get; private set; } = EOSLoginManager.EOSLoginState.None;

		// Token: 0x06003885 RID: 14469 RVA: 0x000ED06E File Offset: 0x000EB26E
		public static bool IsWaitingOnLogin()
		{
			return EOSLoginManager.loginState != EOSLoginManager.EOSLoginState.Success;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000ED07B File Offset: 0x000EB27B
		public void TryLogin()
		{
			EOSLoginManager.instance = this;
			RoR2Application.onUpdate += this.OnUpdate;
			this.ExternalAuthLogin_Steam();
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000ED09A File Offset: 0x000EB29A
		private void OnUpdate()
		{
			if (EOSLoginManager.cvLinkEOSAccount.value && !EOSLoginManager.IsWaitingOnLogin())
			{
				this.StartSteamLoginWithDefaultOptions(true);
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000ED0B8 File Offset: 0x000EB2B8
		private void OnSteamworksInitialized()
		{
			byte[] data = Client.Instance.Auth.GetAuthSessionTicket().Data;
			if (data != null)
			{
				Debug.Log("Successfully got authSessionTicketData from Steam!");
				EOSLoginManager.ticket = data.ToHexString();
				this.StartSteamLoginWithDefaultOptions(false);
				return;
			}
			Debug.Log("Failure getting authSessionTicketData from Steam!  Can't log into EGS!");
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000ED107 File Offset: 0x000EB307
		public void ExternalAuthLogin_Steam()
		{
			if (Client.Instance != null)
			{
				this.OnSteamworksInitialized();
				return;
			}
			SteamworksClientManager.onLoaded += this.OnSteamworksInitialized;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000ED128 File Offset: 0x000EB328
		public void StartSteamLoginWithDefaultOptions(bool attemptAccountLink = false)
		{
			Epic.OnlineServices.Auth.LoginOptions loginOptions = new Epic.OnlineServices.Auth.LoginOptions
			{
				Credentials = new Epic.OnlineServices.Auth.Credentials
				{
					Type = LoginCredentialType.ExternalAuth,
					Token = EOSLoginManager.ticket,
					ExternalType = ExternalCredentialType.SteamSessionTicket
				},
				ScopeFlags = (AuthScopeFlags.BasicProfile | AuthScopeFlags.FriendsList | AuthScopeFlags.Presence)
			};
			Debug.Log(string.Format("Attempting Auth login with {0}.{1}", "ExternalCredentialType", loginOptions.Credentials.ExternalType));
			this.StartEGSLogin(loginOptions, attemptAccountLink);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000ED194 File Offset: 0x000EB394
		private void StartEGSLoginWithDefaultOptions()
		{
			Epic.OnlineServices.Auth.LoginOptions loginOptions = new Epic.OnlineServices.Auth.LoginOptions
			{
				Credentials = new Epic.OnlineServices.Auth.Credentials
				{
					Type = this._loginCredentialType,
					Id = this._loginCredentialId,
					Token = this._loginCredentialToken
				},
				ScopeFlags = (AuthScopeFlags.BasicProfile | AuthScopeFlags.FriendsList | AuthScopeFlags.Presence)
			};
			this.StartEGSLogin(loginOptions, false);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000ED1E8 File Offset: 0x000EB3E8
		private void StartEGSLogin(Epic.OnlineServices.Auth.LoginOptions loginOptions, bool attemptLink = false)
		{
			EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.AttemptingLogin;
			AuthInterface authInterface = EOSPlatformManager.GetPlatformInterface().GetAuthInterface();
			if (authInterface != null)
			{
				Epic.OnlineServices.Auth.OnLinkAccountCallback <>9__1;
				authInterface.Login(loginOptions, null, delegate(Epic.OnlineServices.Auth.LoginCallbackInfo loginCallbackInfo)
				{
					if (loginCallbackInfo.ResultCode == Result.Success)
					{
						EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.Success;
						EOSLoginManager.cvLinkEOSAccount.SetBool(false);
						this.ProcessSuccessfulAuthLogin(loginCallbackInfo.LocalUserId);
						return;
					}
					if (!Common.IsOperationComplete(loginCallbackInfo.ResultCode))
					{
						EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.FailedLogin;
						Debug.Log("EOS Auth Login failed: " + loginCallbackInfo.ResultCode);
						return;
					}
					if (!(loginCallbackInfo.ContinuanceToken != null))
					{
						EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.FailedLogin;
						Debug.Log("EOS Auth Login failed: " + loginCallbackInfo.ResultCode);
						return;
					}
					if (attemptLink)
					{
						EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.AttemptingLink;
						Debug.Log("EOS Auth Login failed but we have a Continuance Token, so we're gonna use that");
						Epic.OnlineServices.Auth.LinkAccountOptions linkAccountOptions = new Epic.OnlineServices.Auth.LinkAccountOptions
						{
							ContinuanceToken = loginCallbackInfo.ContinuanceToken,
							LinkAccountFlags = LinkAccountFlags.NoFlags
						};
						AuthInterface authInterface = authInterface;
						Epic.OnlineServices.Auth.LinkAccountOptions options = linkAccountOptions;
						object clientData = null;
						Epic.OnlineServices.Auth.OnLinkAccountCallback completionDelegate;
						if ((completionDelegate = <>9__1) == null)
						{
							completionDelegate = (<>9__1 = delegate(Epic.OnlineServices.Auth.LinkAccountCallbackInfo linkAccountCallbackInfo)
							{
								if (linkAccountCallbackInfo.ResultCode == Result.Success)
								{
									EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.Success;
									this.ProcessSuccessfulAuthLogin(linkAccountCallbackInfo.LocalUserId);
									return;
								}
								EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.FailedLink;
								Debug.Log("EOS Account Linking failed: " + linkAccountCallbackInfo.ResultCode);
							});
						}
						authInterface.LinkAccount(options, clientData, completionDelegate);
						return;
					}
					Debug.Log("EOS Auth Login failed so we're going to try with connect login with the app ticket");
					this.ProcessSteamAppTicketConnectLogin(loginOptions.Credentials.Token);
				});
				return;
			}
			throw new Exception("Failed to get auth interface");
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000ED260 File Offset: 0x000EB460
		private void ProcessSuccessfulAuthLogin(EpicAccountId loggedInId)
		{
			if (loggedInId == null || !loggedInId.IsValid())
			{
				Debug.LogError("loggedInId is not valid.");
				EOSLoginManager.loggedInAuthId = null;
				return;
			}
			EOSLoginManager.loggedInAuthId = loggedInId;
			Debug.Log("Auth Login succeeded, Id = " + EOSLoginManager.loggedInAuthId);
			Action<EpicAccountId> onAuthLoggedIn = EOSLoginManager.OnAuthLoggedIn;
			if (onAuthLoggedIn != null)
			{
				onAuthLoggedIn(EOSLoginManager.loggedInAuthId);
			}
			Debug.Log("Attempting Connect login with Auth Credentials");
			Token token = null;
			if (EOSPlatformManager.GetPlatformInterface().GetAuthInterface().CopyUserAuthToken(new CopyUserAuthTokenOptions(), EOSLoginManager.loggedInAuthId, out token) == Result.Success)
			{
				Epic.OnlineServices.Connect.LoginOptions options = new Epic.OnlineServices.Connect.LoginOptions
				{
					Credentials = new Epic.OnlineServices.Connect.Credentials
					{
						Token = token.AccessToken,
						Type = ExternalCredentialType.Epic
					},
					UserLoginInfo = null
				};
				this.AttemptConnectLogin(options);
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000ED31C File Offset: 0x000EB51C
		private void ProcessSteamAppTicketConnectLogin(string appTicket)
		{
			Epic.OnlineServices.Connect.LoginOptions loginOptions = new Epic.OnlineServices.Connect.LoginOptions
			{
				Credentials = new Epic.OnlineServices.Connect.Credentials
				{
					Token = appTicket,
					Type = ExternalCredentialType.SteamSessionTicket
				},
				UserLoginInfo = null
			};
			Debug.Log(string.Format("Attempting Connect login with {0}.{1}", "ExternalCredentialType", loginOptions.Credentials.Type));
			this.AttemptConnectLogin(loginOptions);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000ED37B File Offset: 0x000EB57B
		private bool IsConnectInterfaceValid()
		{
			if (this.connectInterface == null)
			{
				this.connectInterface = EOSPlatformManager.GetPlatformInterface().GetConnectInterface();
			}
			return this.connectInterface != null;
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000ED3A7 File Offset: 0x000EB5A7
		private void AttemptConnectLogin(Epic.OnlineServices.Connect.LoginOptions options)
		{
			if (this.IsConnectInterfaceValid())
			{
				this.connectInterface.Login(options, null, delegate(Epic.OnlineServices.Connect.LoginCallbackInfo loginCallbackInfo)
				{
					if (loginCallbackInfo.ResultCode == Result.Success)
					{
						this.CompleteConnectLogin(loginCallbackInfo.LocalUserId);
						return;
					}
					if (loginCallbackInfo.ResultCode == Result.InvalidUser)
					{
						Debug.Log("EOS Connect Login returned InvalidUser, attempting to create new user");
						CreateUserOptions options2 = new CreateUserOptions
						{
							ContinuanceToken = loginCallbackInfo.ContinuanceToken
						};
						this.connectInterface.CreateUser(options2, null, delegate(CreateUserCallbackInfo createUserCallbackInfo)
						{
							if (createUserCallbackInfo.ResultCode == Result.Success)
							{
								this.CompleteConnectLogin(createUserCallbackInfo.LocalUserId);
								return;
							}
							Debug.Log("EOS Connect Create User failed: " + loginCallbackInfo.ResultCode);
						});
						return;
					}
					if (Common.IsOperationComplete(loginCallbackInfo.ResultCode))
					{
						Debug.Log("EOS Connect Login failed: " + loginCallbackInfo.ResultCode);
					}
				});
				return;
			}
			throw new Exception("Failed to get connect interface");
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000ED3D8 File Offset: 0x000EB5D8
		private void CompleteConnectLogin(ProductUserId localUserId)
		{
			if (localUserId == null || !localUserId.IsValid())
			{
				Debug.LogError("localUserId is not valid.");
				EOSLoginManager.loggedInProductId = null;
				return;
			}
			EOSLoginManager.loggedInProductId = localUserId;
			EOSLoginManager.loggedInUserID = new UserID(new CSteamID(EOSLoginManager.loggedInProductId));
			Action<ProductUserId> onConnectLoggedIn = EOSLoginManager.OnConnectLoggedIn;
			if (onConnectLoggedIn != null)
			{
				onConnectLoggedIn(EOSLoginManager.loggedInProductId);
			}
			this.connectInterface.AddNotifyAuthExpiration(new AddNotifyAuthExpirationOptions(), null, new OnAuthExpirationCallback(this.OnAuthExpirationCallback));
			EOSLoginManager.loginState = EOSLoginManager.EOSLoginState.Success;
			Debug.Log("Connect Login Successful!  User Id = " + localUserId.ToString());
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000ED470 File Offset: 0x000EB670
		private void OnAuthExpirationCallback(AuthExpirationCallbackInfo data)
		{
			Token token = data.ClientData as Token;
			Debug.Log(string.Format("Auth expired! Attempting to refresh now.\n{0} : {1}\n{2} : {3}", new object[]
			{
				"LocalUserId",
				data.LocalUserId,
				"ExpiresIn",
				(token != null) ? new double?(token.ExpiresIn) : null
			}));
			if (EOSLoginManager.loggedInAuthId != null)
			{
				this.ProcessSuccessfulAuthLogin(EOSLoginManager.loggedInAuthId);
				return;
			}
			this.ProcessAppTicketRefresh();
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000ED4F8 File Offset: 0x000EB6F8
		private void ProcessAppTicketRefresh()
		{
			byte[] data = Client.Instance.Auth.GetAuthSessionTicket().Data;
			if (data != null)
			{
				Debug.Log("Successfully got authSessionTicketData from Steam!");
				EOSLoginManager.ticket = data.ToHexString();
				this.ProcessSteamAppTicketConnectLogin(EOSLoginManager.ticket);
				return;
			}
			Debug.Log("Failure getting authSessionTicketData from Steam!  Can't log into EGS!");
		}

		// Token: 0x04003845 RID: 14405
		public LoginCredentialType _loginCredentialType = LoginCredentialType.ExchangeCode;

		// Token: 0x04003846 RID: 14406
		public string _loginCredentialId;

		// Token: 0x04003847 RID: 14407
		public string _loginCredentialToken;

		// Token: 0x04003848 RID: 14408
		public static EOSLoginManager instance = null;

		// Token: 0x04003849 RID: 14409
		public static readonly BoolConVar cvLinkEOSAccount = new BoolConVar("eos_link_account", ConVarFlags.None, "0", "trigger the account linking process");

		// Token: 0x0400384A RID: 14410
		public static EpicAccountId loggedInAuthId = null;

		// Token: 0x0400384C RID: 14412
		public static ProductUserId loggedInProductId = null;

		// Token: 0x0400384D RID: 14413
		public static UserID loggedInUserID;

		// Token: 0x04003850 RID: 14416
		private static string ticket;

		// Token: 0x04003851 RID: 14417
		private ConnectInterface connectInterface;

		// Token: 0x020009A7 RID: 2471
		public enum EOSLoginState
		{
			// Token: 0x04003853 RID: 14419
			None,
			// Token: 0x04003854 RID: 14420
			AttemptingLogin,
			// Token: 0x04003855 RID: 14421
			AttemptingLink,
			// Token: 0x04003856 RID: 14422
			FailedLogin,
			// Token: 0x04003857 RID: 14423
			FailedLink,
			// Token: 0x04003858 RID: 14424
			Success
		}
	}
}
