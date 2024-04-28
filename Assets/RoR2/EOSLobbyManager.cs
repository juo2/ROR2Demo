using System;
using System.Collections.Generic;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.UI;
using Facepunch.Steamworks;
using HG;
using RoR2.Networking;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020009A0 RID: 2464
	public class EOSLobbyManager : PCLobbyManager
	{
		// Token: 0x060037ED RID: 14317 RVA: 0x00014F2E File Offset: 0x0001312E
		public override MPFeatures GetPlatformMPFeatureFlags()
		{
			return MPFeatures.HostGame | MPFeatures.FindGame;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000EAE88 File Offset: 0x000E9088
		public override MPLobbyFeatures GetPlatformMPLobbyFeatureFlags()
		{
			return MPLobbyFeatures.CreateLobby | MPLobbyFeatures.SocialIcon | MPLobbyFeatures.HostPromotion | MPLobbyFeatures.Clipboard | MPLobbyFeatures.Invite | MPLobbyFeatures.UserIcon | MPLobbyFeatures.LeaveLobby | MPLobbyFeatures.LobbyDropdownOptions;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000EAE8F File Offset: 0x000E908F
		public static EOSLobbyManager GetFromPlatformSystems()
		{
			return PlatformSystems.lobbyManager as EOSLobbyManager;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000EAE9B File Offset: 0x000E909B
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000026ED File Offset: 0x000008ED
		public override bool isInLobby
		{
			get
			{
				return this.currentLobbyId != string.Empty;
			}
			protected set
			{
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000EAEAD File Offset: 0x000E90AD
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x000EAEB5 File Offset: 0x000E90B5
		public override bool ownsLobby
		{
			get
			{
				return this._ownsLobby;
			}
			protected set
			{
				if (value != this._ownsLobby)
				{
					this._ownsLobby = value;
					if (this._ownsLobby)
					{
						this.OnLobbyOwnershipGained();
						this.UpdatePlayerCount();
						return;
					}
					this.OnLobbyOwnershipLost();
				}
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000EAEE4 File Offset: 0x000E90E4
		private void UpdateOwnsLobby()
		{
			LobbyDetailsGetLobbyOwnerOptions options = new LobbyDetailsGetLobbyOwnerOptions();
			this.ownsLobby = (this.currentLobbyDetails != null && this.currentLobbyDetails.GetLobbyOwner(options) == EOSLoginManager.loggedInProductId);
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x000EAF24 File Offset: 0x000E9124
		public override bool hasMinimumPlayerCount
		{
			get
			{
				return this.newestLobbyData.totalPlayerCount >= this.minimumPlayerCount;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x000EAF3C File Offset: 0x000E913C
		// (set) Token: 0x060037F7 RID: 14327 RVA: 0x000EAF44 File Offset: 0x000E9144
		public int remoteMachineCount { get; private set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000EAF4D File Offset: 0x000E914D
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x000026ED File Offset: 0x000008ED
		public string CurrentLobbyId
		{
			get
			{
				return this.currentLobbyId;
			}
			private set
			{
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000EAF55 File Offset: 0x000E9155
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x000026ED File Offset: 0x000008ED
		public LobbyDetails CurrentLobbyDetails
		{
			get
			{
				return this.currentLobbyDetails;
			}
			private set
			{
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000EAF5D File Offset: 0x000E915D
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000026ED File Offset: 0x000008ED
		public LobbyModification CurrentLobbyModification
		{
			get
			{
				return this.currentLobbyModificationHandle;
			}
			private set
			{
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000EAF65 File Offset: 0x000E9165
		public UserID serverId
		{
			get
			{
				return this.newestLobbyData.serverId;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000EAF72 File Offset: 0x000E9172
		// (set) Token: 0x06003800 RID: 14336 RVA: 0x000EAF7A File Offset: 0x000E917A
		public override LobbyManager.LobbyData newestLobbyData { get; protected set; }

		// Token: 0x06003801 RID: 14337 RVA: 0x000EAF84 File Offset: 0x000E9184
		public void Init()
		{
			this.lobbyInterface = EOSPlatformManager.GetPlatformInterface().GetLobbyInterface();
			if (this.lobbyInterface == null)
			{
				Debug.LogError("Unable to Obtain EOS Lobby Interface!");
			}
			this.SetupLobbyCallbacks();
			RoR2Application.onUpdate += this.StaticUpdate;
			LobbyManager.LobbyDataSetupState setupState = new LobbyManager.LobbyDataSetupState
			{
				totalMaxPlayers = RoR2Application.maxPlayers
			};
			this.newestLobbyData = new LobbyManager.LobbyData(setupState);
			LocalUserManager.onLocalUsersUpdated += this.UpdatePlayerCount;
			NetworkManagerSystem.onStartServerGlobal += this.OnStartHostingServer;
			NetworkManagerSystem.onStopServerGlobal += this.OnStopHostingServer;
			NetworkManagerSystem.onStopClientGlobal += this.OnStopClient;
			NetworkManagerSystem.onStopClientGlobal += delegate()
			{
				this.SetStartingIfOwner(false);
			};
			this.onLobbyOwnershipGained = (Action)Delegate.Combine(this.onLobbyOwnershipGained, new Action(delegate()
			{
				this.SetStartingIfOwner(false);
			}));
			UserManager.OnDisplayNameMappingComplete += this.UpdateLobbyNames;
			this.SetStartingIfOwner(false);
			SteamworksClientManager.onLoaded += this.SetSteamLobbyCallbacks;
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000EB08D File Offset: 0x000E928D
		public override void Shutdown()
		{
			base.Shutdown();
			this.lobbyInterface = null;
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000EB09C File Offset: 0x000E929C
		private void SetSteamLobbyCallbacks()
		{
			Client steamworksClient = SteamworksClientManager.instance.steamworksClient;
			steamworksClient.Lobby.OnUserInvitedToLobby = new Action<ulong, ulong>(this.OnUserInvitedToSteamLobby);
			steamworksClient.Lobby.OnLobbyJoinRequested = new Action<ulong>(this.OnSteamLobbyJoinRequested);
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000EB0D5 File Offset: 0x000E92D5
		private void OnSteamLobbyJoinRequested(ulong lobbyId)
		{
			Debug.LogFormat("Request to join lobby {0} received but we're in cross-play, rejecting", new object[]
			{
				lobbyId
			});
			PCLobbyManager.ShowEnableCrossPlayPopup(false);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000EB0F6 File Offset: 0x000E92F6
		private void OnUserInvitedToSteamLobby(ulong lobbyId, ulong senderId)
		{
			Debug.LogFormat("Received invitation to lobby {0} from sender {1} but we're in cross play, rejecting", new object[]
			{
				lobbyId,
				senderId
			});
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000EB11A File Offset: 0x000E931A
		private void UpdateLobbyNames()
		{
			Action onLobbyDataUpdated = this.onLobbyDataUpdated;
			if (onLobbyDataUpdated == null)
			{
				return;
			}
			onLobbyDataUpdated();
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000EB12C File Offset: 0x000E932C
		private void SetupLobbyCallbacks()
		{
			this.lobbyInterface.AddNotifyLobbyUpdateReceived(new AddNotifyLobbyUpdateReceivedOptions(), null, new OnLobbyUpdateReceivedCallback(this.OnLobbyDataUpdateReceived));
			this.lobbyInterface.AddNotifyJoinLobbyAccepted(new AddNotifyJoinLobbyAcceptedOptions(), null, new OnJoinLobbyAcceptedCallback(this.OnJoinLobbyAccepted));
			this.lobbyInterface.AddNotifyLobbyInviteReceived(new AddNotifyLobbyInviteReceivedOptions(), null, new OnLobbyInviteReceivedCallback(this.OnUserInvitedToLobby));
			this.lobbyInterface.AddNotifyLobbyInviteAccepted(new AddNotifyLobbyInviteAcceptedOptions(), null, new OnLobbyInviteAcceptedCallback(this.OnLobbyInviteAccepted));
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000EB1B1 File Offset: 0x000E93B1
		public override int GetLobbyMemberPlayerCountByIndex(int memberIndex)
		{
			if (memberIndex >= this.playerCountsList.Count)
			{
				return 0;
			}
			return this.playerCountsList[memberIndex];
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000EB1CF File Offset: 0x000E93CF
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x000EB1D7 File Offset: 0x000E93D7
		public override int calculatedTotalPlayerCount { get; protected set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x000EB1E0 File Offset: 0x000E93E0
		// (set) Token: 0x0600380C RID: 14348 RVA: 0x000EB1E8 File Offset: 0x000E93E8
		public override int calculatedExtraPlayersCount { get; protected set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000EB1F4 File Offset: 0x000E93F4
		// (set) Token: 0x0600380E RID: 14350 RVA: 0x000026ED File Offset: 0x000008ED
		public override LobbyType currentLobbyType
		{
			get
			{
				if (this.currentLobbyDetails != null)
				{
					LobbyDetailsInfo lobbyDetailsInfo = new LobbyDetailsInfo();
					this.currentLobbyDetails.CopyInfo(new LobbyDetailsCopyInfoOptions(), out lobbyDetailsInfo);
					return EOSLobbyManager.PermissionLevelToType(lobbyDetailsInfo.PermissionLevel);
				}
				return LobbyType.Error;
			}
			set
			{
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x000EB235 File Offset: 0x000E9435
		// (set) Token: 0x06003810 RID: 14352 RVA: 0x000EB23D File Offset: 0x000E943D
		public override bool IsBusy { get; set; }

		// Token: 0x06003811 RID: 14353 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CheckIfInitializedAndValid()
		{
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000EB248 File Offset: 0x000E9448
		private void UpdateMemberAttribute(string inKey, string inValue)
		{
			if (this.CurrentLobbyModification != null)
			{
				Debug.LogFormat("Setting lobby member attribute {0} to value {1} for user {2}", new object[]
				{
					inKey,
					inValue,
					EOSLoginManager.loggedInProductId
				});
				this.CurrentLobbyModification.RemoveMemberAttribute(new LobbyModificationRemoveMemberAttributeOptions
				{
					Key = inKey
				});
				this.CurrentLobbyModification.AddMemberAttribute(new LobbyModificationAddMemberAttributeOptions
				{
					Attribute = new AttributeData
					{
						Key = inKey,
						Value = inValue
					},
					Visibility = LobbyAttributeVisibility.Public
				});
				EOSLobbyManager.UpdateLobby(this.CurrentLobbyModification);
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000EB2DC File Offset: 0x000E94DC
		private void UpdatePlayerCount()
		{
			if (this.currentLobbyDetails != null)
			{
				int count = LocalUserManager.readOnlyLocalUsersList.Count;
				int remoteMachineCount = Math.Max(1, count);
				string @string = this.localPlayerCountToString.GetString(remoteMachineCount);
				Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
				if (this.CurrentLobbyDetails.CopyMemberAttributeByKey(new LobbyDetailsCopyMemberAttributeByKeyOptions
				{
					AttrKey = "player_count",
					TargetUserId = EOSLoginManager.loggedInProductId
				}, out attribute) == Result.Success)
				{
					if (attribute.Data.Value.AsUtf8 != @string && this.CurrentLobbyModification != null)
					{
						this.UpdateMemberAttribute("player_count", @string);
					}
				}
				else
				{
					this.UpdateMemberAttribute("player_count", @string);
				}
				this.playerCountsList.Clear();
				this.calculatedTotalPlayerCount = 0;
				this.remoteMachineCount = 0;
				this.calculatedExtraPlayersCount = 0;
				ProductUserId loggedInProductId = EOSLoginManager.loggedInProductId;
				uint memberCount = this.currentLobbyDetails.GetMemberCount(new LobbyDetailsGetMemberCountOptions());
				for (uint num = 0U; num < memberCount; num += 1U)
				{
					Handle memberByIndex = this.currentLobbyDetails.GetMemberByIndex(new LobbyDetailsGetMemberByIndexOptions
					{
						MemberIndex = num
					});
					int num2 = 1;
					Epic.OnlineServices.Lobby.Attribute attribute2 = new Epic.OnlineServices.Lobby.Attribute();
					if (this.CurrentLobbyDetails.CopyMemberAttributeByKey(new LobbyDetailsCopyMemberAttributeByKeyOptions
					{
						AttrKey = "player_count",
						TargetUserId = EOSLoginManager.loggedInProductId
					}, out attribute2) == Result.Success)
					{
						num2 = (TextSerialization.TryParseInvariant(attribute2.Data.Value.AsUtf8, out num2) ? Math.Max(1, num2) : 1);
					}
					if (memberByIndex == loggedInProductId)
					{
						num2 = Math.Max(1, count);
					}
					else
					{
						remoteMachineCount = this.remoteMachineCount + 1;
						this.remoteMachineCount = remoteMachineCount;
					}
					this.playerCountsList.Add(num2);
					this.calculatedTotalPlayerCount += num2;
					if (num2 > 1)
					{
						this.calculatedExtraPlayersCount += num2 - 1;
					}
				}
			}
			Action onPlayerCountUpdated = this.onPlayerCountUpdated;
			if (onPlayerCountUpdated == null)
			{
				return;
			}
			onPlayerCountUpdated();
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000EB4B5 File Offset: 0x000E96B5
		private void OnLobbyChanged()
		{
			this.OnLobbyDataUpdated();
			Action onLobbyChanged = this.onLobbyChanged;
			if (onLobbyChanged != null)
			{
				onLobbyChanged();
			}
			(PlatformSystems.userManager as UserManagerEOS).QueryForDisplayNames(this.GetLobbyMembers(), null);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000EB4E4 File Offset: 0x000E96E4
		public override void CreateLobby()
		{
			CreateLobbyOptions options = new CreateLobbyOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				MaxLobbyMembers = (uint)RoR2Application.maxPlayers,
				PermissionLevel = ((this.preferredLobbyType == LobbyType.Public) ? LobbyPermissionLevel.Publicadvertised : LobbyPermissionLevel.Joinviapresence),
				BucketId = "gbx_internal",
				PresenceEnabled = true,
				AllowInvites = true
			};
			base.awaitingCreate = true;
			LobbyInterface lobbyInterface = this.lobbyInterface;
			if (lobbyInterface == null)
			{
				return;
			}
			lobbyInterface.CreateLobby(options, null, new OnCreateLobbyCallback(this.OnLobbyCreated));
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000EB55E File Offset: 0x000E975E
		public override void JoinLobby(UserID uid)
		{
			this.LeaveLobby(delegate()
			{
				JoinLobbyOptions options = new JoinLobbyOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId
				};
				LobbyInterface lobbyInterface = this.lobbyInterface;
				if (lobbyInterface == null)
				{
					return;
				}
				lobbyInterface.JoinLobby(options, null, new OnJoinLobbyCallback(this.OnLobbyJoined));
			});
			base.awaitingJoin = true;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000EB57C File Offset: 0x000E977C
		public void JoinLobby(LobbyDetails lobbyID)
		{
			this.LeaveLobby(delegate()
			{
				JoinLobbyOptions options = new JoinLobbyOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId,
					LobbyDetailsHandle = lobbyID
				};
				LobbyInterface lobbyInterface = this.lobbyInterface;
				if (lobbyInterface == null)
				{
					return;
				}
				lobbyInterface.JoinLobby(options, null, new OnJoinLobbyCallback(this.OnLobbyJoined));
			});
			base.awaitingJoin = true;
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000EB5B8 File Offset: 0x000E97B8
		public void FindClipboardLobby(string lobbyId)
		{
			if (this.joinClipboardLobbySearchHandle != null)
			{
				this.joinClipboardLobbySearchHandle.Release();
				this.joinClipboardLobbySearchHandle = null;
			}
			this.joinClipboardLobbySearchHandle = new LobbySearch();
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.CreateLobbySearch(new CreateLobbySearchOptions
			{
				MaxResults = 1U
			}, out this.joinClipboardLobbySearchHandle)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (!(result2.GetValueOrDefault() == result3 & result2 != null))
			{
				Debug.Log("Unable to create lobby search for joining lobby by ID, result = " + result.ToString());
				return;
			}
			this.joinClipboardLobbySearchHandle.SetLobbyId(new LobbySearchSetLobbyIdOptions
			{
				LobbyId = lobbyId
			});
			this.joinClipboardLobbySearchHandle.Find(new LobbySearchFindOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId
			}, "ClipboardJoin", new LobbySearchOnFindCallback(this.OnLobbySearchComplete));
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000EB69C File Offset: 0x000E989C
		public override void LeaveLobby()
		{
			LeaveLobbyOptions options = new LeaveLobbyOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				LobbyId = this.currentLobbyId
			};
			LobbyInterface lobbyInterface = this.lobbyInterface;
			if (lobbyInterface != null)
			{
				lobbyInterface.LeaveLobby(options, null, new OnLeaveLobbyCallback(this.OnLobbyLeave));
			}
			this.currentLobbyId = string.Empty;
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000EB6F0 File Offset: 0x000E98F0
		private void LeaveLobby(Action callback)
		{
			LeaveLobbyOptions options = new LeaveLobbyOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				LobbyId = this.currentLobbyId
			};
			LobbyInterface lobbyInterface = this.lobbyInterface;
			if (lobbyInterface != null)
			{
				lobbyInterface.LeaveLobby(options, null, delegate(LeaveLobbyCallbackInfo leaveLobbyCallbackInfo)
				{
					this.OnLobbyLeave(leaveLobbyCallbackInfo);
					Action callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2();
				});
			}
			this.currentLobbyId = string.Empty;
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000EB758 File Offset: 0x000E9958
		public override UserID[] GetLobbyMembers()
		{
			LobbyDetails lobbyDetails = this.CurrentLobbyDetails;
			uint? num = (lobbyDetails != null) ? new uint?(lobbyDetails.GetMemberCount(new LobbyDetailsGetMemberCountOptions())) : null;
			UserID[] array = null;
			if (num != null)
			{
				array = new UserID[num.Value];
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)array.Length))
				{
					array[(int)num2] = new UserID(this.CurrentLobbyDetails.GetMemberByIndex(new LobbyDetailsGetMemberByIndexOptions
					{
						MemberIndex = num2
					}));
					num2 += 1U;
				}
			}
			return array;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool ShouldShowPromoteButton()
		{
			return false;
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000EB7D8 File Offset: 0x000E99D8
		private void Update()
		{
			if (this.startingFadeSet != (this.newestLobbyData.starting && !ClientScene.ready))
			{
				if (this.startingFadeSet)
				{
					FadeToBlackManager.fadeCount--;
				}
				else
				{
					FadeToBlackManager.fadeCount++;
				}
				this.startingFadeSet = !this.startingFadeSet;
			}
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000EB836 File Offset: 0x000E9A36
		private void StaticUpdate()
		{
			this.UpdateOwnsLobby();
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000EB840 File Offset: 0x000E9A40
		private CSteamID GetLaunchParamsLobbyId()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length - 1; i++)
			{
				CSteamID result;
				if (string.Equals(commandLineArgs[i], "+connect_lobby", StringComparison.OrdinalIgnoreCase) && CSteamID.TryParse(ArrayUtils.GetSafe<string>(commandLineArgs, i + 1, string.Empty), out result))
				{
					return result;
				}
			}
			return CSteamID.nil;
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000026ED File Offset: 0x000008ED
		public void ForceLobbyDataUpdate()
		{
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000EB894 File Offset: 0x000E9A94
		public void SendLobbyMessage(LobbyManager.LobbyMessageType messageType, NetworkWriter writer)
		{
			byte[] array = new byte[(int)(1 + writer.Position)];
			array[0] = (byte)messageType;
			Array.Copy(writer.AsArray(), 0, array, 1, (int)writer.Position);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000026ED File Offset: 0x000008ED
		public override void SetQuickplayCutoffTime(double cutoffTime)
		{
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000EB8C7 File Offset: 0x000E9AC7
		public override double GetQuickplayCutoffTime()
		{
			return 0.0;
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnCutoffTimerComplete()
		{
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000EB8D4 File Offset: 0x000E9AD4
		private void OnChatMessageReceived(ulong senderId, byte[] buffer, int byteCount)
		{
			try
			{
				NetworkReader networkReader = new NetworkReader(buffer);
				if (byteCount >= 1)
				{
					LobbyManager.LobbyMessageType lobbyMessageType = (LobbyManager.LobbyMessageType)networkReader.ReadByte();
					Debug.LogFormat("Received Steamworks Lobby Message from {0} ({1}B). messageType={2}", new object[]
					{
						senderId,
						byteCount,
						lobbyMessageType
					});
					if (lobbyMessageType != LobbyManager.LobbyMessageType.Chat && lobbyMessageType != LobbyManager.LobbyMessageType.Password)
					{
					}
				}
				else
				{
					Debug.LogWarningFormat("Received SteamworksLobbyMessage from {0}, but the message was empty.", Array.Empty<object>());
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000EB950 File Offset: 0x000E9B50
		private void OnLobbyCreated(CreateLobbyCallbackInfo data)
		{
			base.awaitingCreate = false;
			if (data.ResultCode == Result.Success)
			{
				this.currentLobbyId = data.LobbyId;
				this.TryGetLobbyDetails();
				this.OnLobbyChanged();
				return;
			}
			Debug.Log("EOS lobby creation failed, result = " + data.ResultCode.ToString());
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000EB9A8 File Offset: 0x000E9BA8
		protected void OnLobbyDataUpdated()
		{
			this.UpdateNewestLobbyData();
			this.UpdateOwnsLobby();
			this.UpdatePlayerCount();
			if (this.currentLobbyDetails != null && !this.ownsLobby)
			{
				if (this.newestLobbyData.serverId.isValid)
				{
					int num = (this.newestLobbyData.serverId == new UserID(NetworkManagerSystem.singleton.serverP2PId) || NetworkManagerSystem.singleton.IsConnectedToServer(this.newestLobbyData.serverId)) ? 1 : 0;
					bool flag = string.CompareOrdinal(RoR2Application.GetBuildId(), this.newestLobbyData.buildId) == 0;
					if (num == 0 && flag)
					{
						NetworkManagerSystem.singleton.desiredHost = new HostDescription(this.newestLobbyData.serverId, HostDescription.HostType.EOS);
						this.lastHostingLobbyId = this.currentLobbyId;
					}
				}
				else if (this.lastHostingLobbyId == this.currentLobbyId)
				{
					Debug.LogFormat("Intercepting bad or out-of-order lobby update to server id.", Array.Empty<object>());
				}
				else
				{
					NetworkManagerSystem.singleton.desiredHost = HostDescription.none;
				}
			}
			Action onLobbyDataUpdated = this.onLobbyDataUpdated;
			if (onLobbyDataUpdated == null)
			{
				return;
			}
			onLobbyDataUpdated();
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000EBAC0 File Offset: 0x000E9CC0
		private void UpdateNewestLobbyData()
		{
			DateTime value = Util.UnixTimeStampToDateTimeUtc((uint)Util.GetCurrentUnixEpochTimeInSeconds()) + TimeSpan.FromSeconds(30.0);
			if (this.currentLobbyDetails != null)
			{
				LobbyManager.LobbyDataSetupState lobbyDataSetupState = new LobbyManager.LobbyDataSetupState
				{
					totalMaxPlayers = RoR2Application.maxPlayers,
					totalPlayerCount = this.calculatedTotalPlayerCount,
					quickplayQueued = false,
					starting = EOSLobbyManager.GetLobbyBoolValue(this.currentLobbyDetails, "starting"),
					buildId = EOSLobbyManager.GetLobbyStringValue(this.currentLobbyDetails, "build_id"),
					quickplayCutoffTime = new DateTime?(value),
					shouldConnect = false,
					joinable = (this.calculatedTotalPlayerCount < RoR2Application.maxPlayers)
				};
				string lobbyStringValue = EOSLobbyManager.GetLobbyStringValue(this.currentLobbyDetails, "server_id");
				if (lobbyStringValue != string.Empty)
				{
					lobbyDataSetupState.serverId = new UserID(new CSteamID(lobbyStringValue));
				}
				this.newestLobbyData = new LobbyManager.LobbyData(lobbyDataSetupState);
				return;
			}
			this.newestLobbyData = new LobbyManager.LobbyData();
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000EBBBC File Offset: 0x000E9DBC
		private void OnLobbyJoined(JoinLobbyCallbackInfo data)
		{
			base.awaitingJoin = false;
			bool flag = data.ResultCode == Result.Success;
			if (flag)
			{
				this.currentLobbyId = data.LobbyId;
				this.TryGetLobbyDetails();
				if (this.currentLobbyDetails != null)
				{
					string buildId = RoR2Application.GetBuildId();
					string lobbyStringValue = EOSLobbyManager.GetLobbyStringValue(this.currentLobbyDetails, "build_id");
					if (buildId != lobbyStringValue)
					{
						Debug.LogFormat("Lobby build_id mismatch, leaving lobby. Ours=\"{0}\" Theirs=\"{1}\"", new object[]
						{
							buildId,
							lobbyStringValue
						});
						SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
						simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
						simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
						{
							token = "STEAM_LOBBY_VERSION_MISMATCH_DIALOG_TITLE",
							formatParams = Array.Empty<object>()
						};
						SimpleDialogBox.TokenParamsPair descriptionToken = default(SimpleDialogBox.TokenParamsPair);
						descriptionToken.token = "STEAM_LOBBY_VERSION_MISMATCH_DIALOG_DESCRIPTION";
						object[] formatParams = new string[]
						{
							buildId,
							lobbyStringValue
						};
						descriptionToken.formatParams = formatParams;
						simpleDialogBox.descriptionToken = descriptionToken;
						return;
					}
				}
				Debug.LogFormat("lobby join succeeded. Lobby id = {0}", new object[]
				{
					this.currentLobbyId
				});
				this.OnLobbyChanged();
			}
			else
			{
				Debug.Log("Steamworks lobby join failed.");
				Console.instance.SubmitCmd(null, "steam_lobby_create_if_none", true);
			}
			Action<bool> onLobbyJoined = this.onLobbyJoined;
			if (onLobbyJoined == null)
			{
				return;
			}
			onLobbyJoined(flag);
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000EBCFD File Offset: 0x000E9EFD
		private void OnLobbyMemberDataUpdated(UserID memberId)
		{
			this.UpdateOwnsLobby();
			Action<UserID> onLobbyMemberDataUpdated = this.onLobbyMemberDataUpdated;
			if (onLobbyMemberDataUpdated == null)
			{
				return;
			}
			onLobbyMemberDataUpdated(memberId);
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000EBD16 File Offset: 0x000E9F16
		private void OnLobbyKicked(bool kickedDueToDisconnect, ulong lobbyId, ulong adminId)
		{
			Debug.LogFormat("Kicked from lobby. kickedDueToDisconnect={0} lobbyId={1} adminId={2}", new object[]
			{
				kickedDueToDisconnect,
				lobbyId,
				adminId
			});
			this.OnLobbyChanged();
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000EBD4C File Offset: 0x000E9F4C
		private void OnLobbyLeave(LeaveLobbyCallbackInfo data)
		{
			Debug.LogFormat("Left lobby {0}.", new object[]
			{
				data.LobbyId
			});
			this.currentLobbyId = string.Empty;
			if (this.currentLobbyDetails != null)
			{
				this.currentLobbyDetails.Release();
				this.currentLobbyDetails = null;
			}
			if (this.currentLobbyModificationHandle != null)
			{
				this.currentLobbyModificationHandle.Release();
				this.currentLobbyModificationHandle = null;
			}
			this.OnLobbyChanged();
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000EBDC3 File Offset: 0x000E9FC3
		private void OnLobbyJoinRequested(ulong lobbyId)
		{
			Debug.LogFormat("Request to join lobby {0} received. Attempting to join lobby.", new object[]
			{
				lobbyId
			});
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000EBDDE File Offset: 0x000E9FDE
		private void OnUserInvitedToLobby(LobbyInviteReceivedCallbackInfo data)
		{
			Debug.LogFormat("Received invitation to lobby {0} from sender {1}.", new object[]
			{
				data.InviteId,
				data.TargetUserId
			});
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000EBE04 File Offset: 0x000EA004
		private static void OnSendInviteComplete(SendInviteCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result result = Result.Success;
			if (!(resultCode.GetValueOrDefault() == result & resultCode != null))
			{
				Debug.Log("Unable to send invite!  LobbyID = " + data.LobbyId);
			}
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000EBE44 File Offset: 0x000EA044
		private void OnLobbySearchComplete(LobbySearchFindCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result result = Result.Success;
			if (resultCode.GetValueOrDefault() == result & resultCode != null)
			{
				bool flag = data.ClientData as string == "ClipboardJoin";
				List<LobbyDetails> list = new List<LobbyDetails>();
				LobbySearch lobbySearch = flag ? this.joinClipboardLobbySearchHandle : this.currentSearchHandle;
				if (lobbySearch != null)
				{
					uint searchResultCount = lobbySearch.GetSearchResultCount(new LobbySearchGetSearchResultCountOptions());
					for (uint num = 0U; num < searchResultCount; num += 1U)
					{
						LobbyDetails lobbyDetails = new LobbyDetails();
						if (lobbySearch.CopySearchResultByIndex(new LobbySearchCopySearchResultByIndexOptions
						{
							LobbyIndex = num
						}, out lobbyDetails) == Result.Success)
						{
							if (flag)
							{
								this.JoinLobby(lobbyDetails);
								this.joinClipboardLobbySearchHandle.Release();
								this.joinClipboardLobbySearchHandle = null;
								return;
							}
							list.Add(lobbyDetails);
						}
					}
				}
				(data.ClientData as Action<List<LobbyDetails>>)(list);
			}
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000EBF1F File Offset: 0x000EA11F
		private void OnLobbyDataUpdateReceived(LobbyUpdateReceivedCallbackInfo data)
		{
			if (data.LobbyId == this.currentLobbyId)
			{
				this.TryGetLobbyDetails();
				this.OnLobbyChanged();
			}
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000EBF40 File Offset: 0x000EA140
		private void OnJoinLobbyAccepted(JoinLobbyAcceptedCallbackInfo data)
		{
			Debug.LogFormat("Attempting to join from ui event {0} from local user {1}.", new object[]
			{
				data.UiEventId,
				data.LocalUserId
			});
			LobbyDetails lobbyID = new LobbyDetails();
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.CopyLobbyDetailsHandleByUiEventId(new CopyLobbyDetailsHandleByUiEventIdOptions
			{
				UiEventId = data.UiEventId
			}, out lobbyID)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (result2.GetValueOrDefault() == result3 & result2 != null)
			{
				this.JoinLobby(lobbyID);
				return;
			}
			Debug.LogFormat("failed getting lobby details from join callback, result = " + result.ToString(), Array.Empty<object>());
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000EBFF0 File Offset: 0x000EA1F0
		private void OnLobbyInviteAccepted(LobbyInviteAcceptedCallbackInfo data)
		{
			Debug.LogFormat("Accepted invitation to lobby {0} from sender {1}.", new object[]
			{
				data.InviteId,
				data.TargetUserId
			});
			LobbyDetails lobbyID = new LobbyDetails();
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.CopyLobbyDetailsHandleByInviteId(new CopyLobbyDetailsHandleByInviteIdOptions
			{
				InviteId = data.InviteId
			}, out lobbyID)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (result2.GetValueOrDefault() == result3 & result2 != null)
			{
				this.JoinLobby(lobbyID);
				return;
			}
			Debug.LogFormat("failed getting lobby details from invite, result = " + result.ToString(), Array.Empty<object>());
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000EC09C File Offset: 0x000EA29C
		private void OnShowFriendsComplete(ShowFriendsCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result? result = resultCode;
			Result result2 = Result.Success;
			if (!(result.GetValueOrDefault() == result2 & result != null))
			{
				Debug.Log("Failed to show friends list, result = " + resultCode.ToString());
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000EC0E4 File Offset: 0x000EA2E4
		private void OnLobbyUpdated(UpdateLobbyCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result? result = resultCode;
			Result result2 = Result.Success;
			if (!(result.GetValueOrDefault() == result2 & result != null))
			{
				Debug.Log("Failed to successfully update lobby.  Result = " + resultCode.ToString());
				return;
			}
			this.OnLobbyChanged();
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000EC134 File Offset: 0x000EA334
		private void TryGetLobbyDetails()
		{
			if (this.currentLobbyDetails != null)
			{
				this.currentLobbyDetails.Release();
				this.currentLobbyDetails = null;
			}
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.CopyLobbyDetailsHandle(new CopyLobbyDetailsHandleOptions
			{
				LobbyId = this.currentLobbyId,
				LocalUserId = EOSLoginManager.loggedInProductId
			}, out this.currentLobbyDetails)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (!(result2.GetValueOrDefault() == result3 & result2 != null))
			{
				Debug.Log("Failed to get details for lobby: " + this.CurrentLobbyId + " result = " + result.ToString());
			}
			this.TryGetLobbyModificationHandle();
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000EC1E8 File Offset: 0x000EA3E8
		private void TryGetLobbyModificationHandle()
		{
			if (this.currentLobbyModificationHandle != null)
			{
				this.currentLobbyModificationHandle.Release();
				this.currentLobbyModificationHandle = null;
			}
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.UpdateLobbyModification(new UpdateLobbyModificationOptions
			{
				LobbyId = this.currentLobbyId,
				LocalUserId = EOSLoginManager.loggedInProductId
			}, out this.currentLobbyModificationHandle)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (!(result2.GetValueOrDefault() == result3 & result2 != null))
			{
				Debug.Log("Failed to get modification handle for lobby: " + this.CurrentLobbyId + " result = " + result.ToString());
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000EC298 File Offset: 0x000EA498
		public bool RequestLobbyList(object requester, EOSLobbyManager.Filter filter, Action<List<LobbyDetails>> callback)
		{
			if (requester != null)
			{
				foreach (EOSLobbyManager.LobbyRefreshRequest lobbyRefreshRequest in this.lobbyRefreshRequests)
				{
					if (requester == lobbyRefreshRequest.requester)
					{
						return false;
					}
				}
			}
			EOSLobbyManager.LobbyRefreshRequest item = new EOSLobbyManager.LobbyRefreshRequest
			{
				requester = requester,
				filter = filter,
				callback = callback
			};
			this.lobbyRefreshRequests.Enqueue(item);
			this.UpdateRefreshRequestQueue();
			return true;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000EC32C File Offset: 0x000EA52C
		private void UpdateRefreshRequestQueue()
		{
			if (this.currentRefreshRequest != null)
			{
				return;
			}
			if (this.lobbyRefreshRequests.Count == 0)
			{
				return;
			}
			this.SearchForLobbiesWithRequest(this.lobbyRefreshRequests.Dequeue());
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000EC35C File Offset: 0x000EA55C
		private void SearchForLobbiesWithRequest(EOSLobbyManager.LobbyRefreshRequest request)
		{
			if (this.currentSearchHandle != null)
			{
				this.currentSearchHandle.Release();
				this.currentSearchHandle = null;
			}
			CreateLobbySearchOptions options = new CreateLobbySearchOptions
			{
				MaxResults = 50U
			};
			this.currentSearchHandle = new LobbySearch();
			LobbyInterface lobbyInterface = this.lobbyInterface;
			Result? result = (lobbyInterface != null) ? new Result?(lobbyInterface.CreateLobbySearch(options, out this.currentSearchHandle)) : null;
			Result? result2 = result;
			Result result3 = Result.Success;
			if (result2.GetValueOrDefault() == result3 & result2 != null)
			{
				this.currentSearchHandle.SetParameter(new LobbySearchSetParameterOptions
				{
					ComparisonOp = ComparisonOp.Equal,
					Parameter = new AttributeData
					{
						Key = "build_id",
						Value = RoR2Application.GetBuildId()
					}
				});
				foreach (AttributeData parameter in request.filter.SearchData)
				{
					this.currentSearchHandle.SetParameter(new LobbySearchSetParameterOptions
					{
						ComparisonOp = ComparisonOp.Equal,
						Parameter = parameter
					});
				}
				this.currentSearchHandle.Find(new LobbySearchFindOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId
				}, request.callback, new LobbySearchOnFindCallback(this.OnLobbySearchComplete));
				return;
			}
			Debug.LogError("Error Creating Lobby Search Handle! " + result.ToString());
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000EC4D4 File Offset: 0x000EA6D4
		private void OnStopClient()
		{
			NetworkConnection connection = NetworkManagerSystem.singleton.client.connection;
			bool flag = Util.ConnectionIsLocal(connection);
			bool flag2;
			if (connection is EOSNetworkConnection)
			{
				Handle remoteUserID = ((EOSNetworkConnection)connection).RemoteUserID;
				CSteamID cid = this.newestLobbyData.serverId.CID;
				flag2 = (remoteUserID == cid.egsValue);
			}
			else
			{
				flag2 = (connection.address == this.newestLobbyData.serverAddressPortPair.address);
			}
			if (flag && this.ownsLobby && this.currentLobbyModificationHandle != null)
			{
				this.currentLobbyModificationHandle.RemoveAttribute(new LobbyModificationRemoveAttributeOptions
				{
					Key = "server_id"
				});
			}
			if (!flag && flag2)
			{
				this.LeaveLobby();
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000EC58C File Offset: 0x000EA78C
		private void OnStartHostingServer()
		{
			this.hostingServer = true;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000EC595 File Offset: 0x000EA795
		private void OnStopHostingServer()
		{
			this.hostingServer = false;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000EC59E File Offset: 0x000EA79E
		public void JoinOrStartMigrate(UserID newLobbyId)
		{
			if (this.ownsLobby)
			{
				this.StartMigrateLobby(newLobbyId.CID.egsValue);
				return;
			}
			this.JoinLobby(newLobbyId);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000026ED File Offset: 0x000008ED
		public void StartMigrateLobby(ProductUserId newLobbyId)
		{
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000EC5C2 File Offset: 0x000EA7C2
		private void AttemptToJoinPendingSteamworksLobby()
		{
			bool isAnyUserSignedIn = LocalUserManager.isAnyUserSignedIn;
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000026ED File Offset: 0x000008ED
		public void SetLobbyQuickPlayQueuedIfOwner(bool quickplayQueuedState)
		{
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000026ED File Offset: 0x000008ED
		public void SetLobbyQuickPlayCutoffTimeIfOwner(uint? timestamp)
		{
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000026ED File Offset: 0x000008ED
		public void SetStartingIfOwner(bool startingState)
		{
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000EC5CA File Offset: 0x000EA7CA
		protected void OnLobbyOwnershipGained()
		{
			Action onLobbyOwnershipGained = this.onLobbyOwnershipGained;
			if (onLobbyOwnershipGained == null)
			{
				return;
			}
			onLobbyOwnershipGained();
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000EC5DC File Offset: 0x000EA7DC
		private void OnLobbyOwnershipLost()
		{
			Action onLobbyOwnershipLost = this.onLobbyOwnershipLost;
			if (onLobbyOwnershipLost == null)
			{
				return;
			}
			onLobbyOwnershipLost();
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x000EC5EE File Offset: 0x000EA7EE
		public override bool IsLobbyOwner(UserID user)
		{
			return this.currentLobbyDetails != null && this.currentLobbyDetails.GetLobbyOwner(new LobbyDetailsGetLobbyOwnerOptions()) == user.CID.egsValue;
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000026ED File Offset: 0x000008ED
		public override void AutoMatchmake()
		{
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000EC621 File Offset: 0x000EA821
		public override bool IsLobbyOwner()
		{
			return this.isInLobby && this._ownsLobby;
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000EC633 File Offset: 0x000EA833
		public override bool CanInvite()
		{
			return !this.IsBusy;
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000EC63E File Offset: 0x000EA83E
		public override void OpenInviteOverlay()
		{
			UIInterface uiinterface = EOSPlatformManager.GetPlatformInterface().GetUIInterface();
			if (uiinterface == null)
			{
				return;
			}
			uiinterface.ShowFriends(new ShowFriendsOptions
			{
				LocalUserId = EOSLoginManager.loggedInAuthId
			}, null, new OnShowFriendsCallback(this.OnShowFriendsComplete));
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnStartPrivateGame()
		{
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000026ED File Offset: 0x000008ED
		public override void ToggleQuickplay()
		{
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CheckIfInvited()
		{
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CheckBusyTimer()
		{
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool ShouldEnableQuickplayButton()
		{
			return false;
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool ShouldEnableStartPrivateGameButton()
		{
			return true;
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000EC674 File Offset: 0x000EA874
		public override string GetUserDisplayName(UserID user)
		{
			if (user.CID.egsValue != null)
			{
				string userDisplayName = (PlatformSystems.userManager as UserManagerEOS).GetUserDisplayName(user);
				if (userDisplayName != string.Empty)
				{
					return userDisplayName;
				}
			}
			return "";
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000EC6BC File Offset: 0x000EA8BC
		public string GetUserDisplayNameFromProductIdString(string productIdString)
		{
			if (productIdString != string.Empty)
			{
				string userDisplayName = (PlatformSystems.userManager as UserManagerEOS).GetUserDisplayName(new UserID(new CSteamID(productIdString)));
				if (userDisplayName != string.Empty)
				{
					return userDisplayName;
				}
			}
			return "";
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000EC705 File Offset: 0x000EA905
		private static LobbyType PermissionLevelToType(LobbyPermissionLevel permissionLevel)
		{
			switch (permissionLevel)
			{
			case LobbyPermissionLevel.Publicadvertised:
				return LobbyType.Public;
			case LobbyPermissionLevel.Joinviapresence:
				return LobbyType.FriendsOnly;
			case LobbyPermissionLevel.Inviteonly:
				return LobbyType.Private;
			default:
				return LobbyType.Error;
			}
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000EC722 File Offset: 0x000EA922
		private static LobbyPermissionLevel LobbyTypeToPermissionLevel(LobbyType type)
		{
			switch (type)
			{
			case LobbyType.Private:
				return LobbyPermissionLevel.Joinviapresence;
			case LobbyType.FriendsOnly:
				return LobbyPermissionLevel.Joinviapresence;
			case LobbyType.Public:
				return LobbyPermissionLevel.Publicadvertised;
			default:
				return LobbyPermissionLevel.Joinviapresence;
			}
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000EAF4D File Offset: 0x000E914D
		public override string GetLobbyID()
		{
			return this.currentLobbyId;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000EC73F File Offset: 0x000EA93F
		public override bool CheckLobbyIdValidity(string lobbyID)
		{
			return lobbyID.IndexOfAny(EOSLobbyManager.charactersToCheck) != -1;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000EC754 File Offset: 0x000EA954
		public override void JoinLobby(ConCommandArgs args)
		{
			this.CheckIfInitializedAndValid();
			string text = args[0];
			Debug.LogFormat("Enqueuing join for lobby {0}...", new object[]
			{
				text
			});
			this.FindClipboardLobby(text);
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000EC78B File Offset: 0x000EA98B
		public override void LobbyCreate(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				throw new ConCommandException("Cannot create a Steamworks lobby without any local users signed in.");
			}
			eoslobbyManager.CreateLobby();
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000EC78B File Offset: 0x000EA98B
		public override void LobbyCreateIfNone(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			if (!LocalUserManager.isAnyUserSignedIn)
			{
				throw new ConCommandException("Cannot create a Steamworks lobby without any local users signed in.");
			}
			eoslobbyManager.CreateLobby();
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000EC7B4 File Offset: 0x000EA9B4
		public override void LobbyLeave(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			eoslobbyManager.LeaveLobby();
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000EC7CB File Offset: 0x000EA9CB
		public override void LobbyAssignOwner(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
			Debug.LogFormat("Promoting {0} to lobby leader...", new object[]
			{
				args[0]
			});
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000EC7F8 File Offset: 0x000EA9F8
		public override void LobbyInvite(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			SendInviteOptions options = new SendInviteOptions
			{
				LobbyId = eoslobbyManager.currentLobbyId,
				LocalUserId = EOSLoginManager.loggedInProductId,
				TargetUserId = ProductUserId.FromString(args.GetArgString(0))
			};
			LobbyInterface lobbyInterface = eoslobbyManager.lobbyInterface;
			if (lobbyInterface == null)
			{
				return;
			}
			lobbyInterface.SendInvite(options, null, new OnSendInviteCallback(EOSLobbyManager.OnSendInviteComplete));
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000EC864 File Offset: 0x000EAA64
		public override void LobbyOpenInviteOverlay(ConCommandArgs args)
		{
			if (EOSLoginManager.loggedInAuthId == null)
			{
				SteamworksLobbyManager.DoSteamLobbyOpenOverlay();
				return;
			}
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			eoslobbyManager.OpenInviteOverlay();
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000EC88E File Offset: 0x000EAA8E
		public override void LobbyCopyToClipboard(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			GUIUtility.systemCopyBuffer = eoslobbyManager.currentLobbyId;
			Chat.AddMessage(Language.GetString("STEAM_COPY_LOBBY_TO_CLIPBOARD_MESSAGE"));
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000EC8BC File Offset: 0x000EAABC
		public override void LobbyPrintData(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
			List<string> list = new List<string>();
			Debug.Log(string.Join("\n", list.ToArray()));
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000EC8F3 File Offset: 0x000EAAF3
		public override void DisplayId(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000EC8F3 File Offset: 0x000EAAF3
		public override void DisplayLobbyId(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000EC8F3 File Offset: 0x000EAAF3
		public override void LobbyPrintMembers(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000EC904 File Offset: 0x000EAB04
		public override void ClearLobbies(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			EOSLobbyManager.Filter filter = new EOSLobbyManager.Filter();
			eoslobbyManager.RequestLobbyList(null, filter, delegate(List<LobbyDetails> lobbies)
			{
				foreach (LobbyDetails message in lobbies)
				{
					Debug.Log(message);
				}
			});
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000EC948 File Offset: 0x000EAB48
		public override void LobbyUpdatePlayerCount(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).UpdatePlayerCount();
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000EC959 File Offset: 0x000EAB59
		public override void LobbyForceUpdateData(ConCommandArgs args)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			eoslobbyManager.ForceLobbyDataUpdate();
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000EC8F3 File Offset: 0x000EAAF3
		public override void LobbyPrintList(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as EOSLobbyManager).CheckIfInitializedAndValid();
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000EC970 File Offset: 0x000EAB70
		public static bool IsLobbyOwner(LobbyDetails lobby)
		{
			return lobby.GetLobbyOwner(new LobbyDetailsGetLobbyOwnerOptions()) == EOSLoginManager.loggedInProductId;
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000EC988 File Offset: 0x000EAB88
		public static string GetLobbyStringValue(LobbyDetails lobby, string key)
		{
			Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
			if (lobby.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions
			{
				AttrKey = key
			}, out attribute) == Result.Success)
			{
				return attribute.Data.Value.AsUtf8;
			}
			return string.Empty;
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000EC9C8 File Offset: 0x000EABC8
		public static bool GetLobbyBoolValue(LobbyDetails lobbyDetails, string key)
		{
			Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
			return lobbyDetails.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions
			{
				AttrKey = key
			}, out attribute) == Result.Success && attribute.Data.Value.AsBool != null && attribute.Data.Value.AsBool.Value;
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000ECA28 File Offset: 0x000EAC28
		public static long GetLobbyIntValue(LobbyDetails lobbyDetails, string key)
		{
			Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
			if (lobbyDetails.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions
			{
				AttrKey = key
			}, out attribute) == Result.Success && attribute.Data.Value.AsInt64 != null)
			{
				return attribute.Data.Value.AsInt64.Value;
			}
			return 0L;
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000ECA88 File Offset: 0x000EAC88
		public static double GetLobbyDoubleValue(LobbyDetails lobbyDetails, string key)
		{
			Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
			if (lobbyDetails.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions
			{
				AttrKey = key
			}, out attribute) == Result.Success && attribute.Data.Value.AsDouble != null)
			{
				return attribute.Data.Value.AsDouble.Value;
			}
			return 0.0;
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000ECAF0 File Offset: 0x000EACF0
		public static bool SetLobbyStringValue(LobbyModification lobby, string key, string value)
		{
			if (lobby == null || key == null || value == null || key == string.Empty || value == string.Empty)
			{
				return false;
			}
			Result result = lobby.AddAttribute(new LobbyModificationAddAttributeOptions
			{
				Attribute = new AttributeData
				{
					Key = key,
					Value = value
				},
				Visibility = LobbyAttributeVisibility.Public
			});
			bool flag = result == Result.Success;
			Debug.Log(string.Concat(new string[]
			{
				"Setting KVP for Lobby: ",
				lobby.ToString(),
				" key = ",
				key,
				" value = ",
				value
			}));
			if (!flag)
			{
				Debug.Log(string.Concat(new string[]
				{
					"Failed to Set KVP for Lobby: ",
					lobby.ToString(),
					" key = ",
					key,
					" value = ",
					value,
					" result = ",
					result.ToString()
				}));
			}
			return flag;
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000ECBEC File Offset: 0x000EADEC
		public static void UpdateLobby(LobbyModification lobby)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			LobbyInterface lobbyInterface = eoslobbyManager.lobbyInterface;
			if (lobbyInterface == null)
			{
				return;
			}
			lobbyInterface.UpdateLobby(new UpdateLobbyOptions
			{
				LobbyModificationHandle = lobby
			}, null, new OnUpdateLobbyCallback(eoslobbyManager.OnLobbyUpdated));
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000ECC34 File Offset: 0x000EAE34
		public static bool RemoveLobbyStringValue(LobbyModification lobby, string key)
		{
			if (lobby != null)
			{
				Result result = lobby.RemoveAttribute(new LobbyModificationRemoveAttributeOptions
				{
					Key = key
				});
				bool flag = result == Result.Success;
				if (!flag)
				{
					Debug.Log(string.Concat(new string[]
					{
						"Failed to Remove KVP from Lobby: ",
						lobby.ToString(),
						" key = ",
						key,
						" result = ",
						result.ToString()
					}));
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000ECCB0 File Offset: 0x000EAEB0
		public static uint GetAllCurrentLobbyKVPs(LobbyDetails lobby, ref Dictionary<string, string> KVPs)
		{
			uint attributeCount = lobby.GetAttributeCount(new LobbyDetailsGetAttributeCountOptions());
			uint num = 0U;
			uint num2 = attributeCount;
			while (num < num2)
			{
				Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
				if (lobby.CopyAttributeByIndex(new LobbyDetailsCopyAttributeByIndexOptions
				{
					AttrIndex = num
				}, out attribute) == Result.Success)
				{
					KVPs.Add(attribute.Data.Key, attribute.Data.Value.AsUtf8);
				}
				num += 1U;
			}
			return attributeCount;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000ECD18 File Offset: 0x000EAF18
		public override void SetLobbyTypeConVarString(string newValue)
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			LobbyDetailsInfo lobbyDetailsInfo = new LobbyDetailsInfo();
			if (eoslobbyManager.CurrentLobbyDetails.CopyInfo(new LobbyDetailsCopyInfoOptions(), out lobbyDetailsInfo) != Result.Success || !eoslobbyManager.IsLobbyOwner())
			{
				throw new ConCommandException("Lobby type cannot be set while not the owner of a valid lobby.");
			}
			LobbyType lobbyType = LobbyType.Error;
			PCLobbyManager.SteamLobbyTypeConVar.instance.GetEnumValueAbstract<LobbyType>(newValue, ref lobbyType);
			if (lobbyType == LobbyType.Error)
			{
				throw new ConCommandException("Lobby type \"Error\" is not allowed.");
			}
			if (eoslobbyManager.CurrentLobbyModification.SetPermissionLevel(new LobbyModificationSetPermissionLevelOptions
			{
				PermissionLevel = EOSLobbyManager.LobbyTypeToPermissionLevel(lobbyType)
			}) != Result.Success)
			{
				Debug.Log("Unable to set permission level for lobby!");
				return;
			}
			EOSLobbyManager.UpdateLobby(eoslobbyManager.CurrentLobbyModification);
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000ECDB8 File Offset: 0x000EAFB8
		public override string GetLobbyTypeConVarString()
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			eoslobbyManager.CheckIfInitializedAndValid();
			LobbyDetailsInfo lobbyDetailsInfo = new LobbyDetailsInfo();
			if (!(eoslobbyManager.currentLobbyDetails != null))
			{
				return string.Empty;
			}
			if (eoslobbyManager.CurrentLobbyDetails.CopyInfo(new LobbyDetailsCopyInfoOptions(), out lobbyDetailsInfo) != Result.Success)
			{
				return string.Empty;
			}
			return EOSLobbyManager.PermissionLevelToType(lobbyDetailsInfo.PermissionLevel).ToString();
		}

		// Token: 0x0400380A RID: 14346
		private const MPFeatures PlatformFeatureFlags = MPFeatures.HostGame | MPFeatures.FindGame;

		// Token: 0x0400380B RID: 14347
		private const MPLobbyFeatures PlatformLobbyUIFlags = MPLobbyFeatures.CreateLobby | MPLobbyFeatures.SocialIcon | MPLobbyFeatures.HostPromotion | MPLobbyFeatures.Clipboard | MPLobbyFeatures.Invite | MPLobbyFeatures.UserIcon | MPLobbyFeatures.LeaveLobby | MPLobbyFeatures.LobbyDropdownOptions;

		// Token: 0x0400380C RID: 14348
		private bool _ownsLobby;

		// Token: 0x0400380D RID: 14349
		private int minimumPlayerCount = 2;

		// Token: 0x0400380F RID: 14351
		private string currentLobbyId = string.Empty;

		// Token: 0x04003810 RID: 14352
		private LobbyDetails currentLobbyDetails;

		// Token: 0x04003811 RID: 14353
		private LobbyModification currentLobbyModificationHandle;

		// Token: 0x04003812 RID: 14354
		private LobbySearch currentSearchHandle;

		// Token: 0x04003813 RID: 14355
		private LobbySearch joinClipboardLobbySearchHandle;

		// Token: 0x04003814 RID: 14356
		private const int MaxSearchResults = 50;

		// Token: 0x04003815 RID: 14357
		public const string mdEdition = "v";

		// Token: 0x04003816 RID: 14358
		public const string mdAppId = "appid";

		// Token: 0x04003817 RID: 14359
		public const string mdTotalMaxPlayers = "total_max_players";

		// Token: 0x04003818 RID: 14360
		public const string mdPlayerCount = "player_count";

		// Token: 0x04003819 RID: 14361
		public const string mdQuickplayQueued = "qp";

		// Token: 0x0400381A RID: 14362
		public const string mdQuickplayCutoffTime = "qp_cutoff_time";

		// Token: 0x0400381B RID: 14363
		public const string mdStarting = "starting";

		// Token: 0x0400381C RID: 14364
		public const string mdBuildId = "build_id";

		// Token: 0x0400381D RID: 14365
		public const string mdServerId = "server_id";

		// Token: 0x0400381E RID: 14366
		public const string mdServerAddress = "server_address";

		// Token: 0x0400381F RID: 14367
		public const string mdMap = "_map";

		// Token: 0x04003820 RID: 14368
		public const string mdRuleBook = "rulebook";

		// Token: 0x04003821 RID: 14369
		public const string mdMigrationId = "migration_id";

		// Token: 0x04003822 RID: 14370
		public const string mdHasPassword = "_pw";

		// Token: 0x04003823 RID: 14371
		public const string mdIsDedicatedServer = "_ds";

		// Token: 0x04003824 RID: 14372
		public const string mdServerName = "_svnm";

		// Token: 0x04003825 RID: 14373
		public const string mdServerTags = "_svtags";

		// Token: 0x04003826 RID: 14374
		public const string mdServerMaxPlayers = "_svmpl";

		// Token: 0x04003827 RID: 14375
		public const string mdServerPlayerCount = "_svplc";

		// Token: 0x04003828 RID: 14376
		public const string mdGameModeName = "_svgm";

		// Token: 0x04003829 RID: 14377
		public const string mdModHash = "_mh";

		// Token: 0x0400382A RID: 14378
		public const string BucketName = "gbx_internal";

		// Token: 0x0400382C RID: 14380
		private LobbyInterface lobbyInterface;

		// Token: 0x0400382D RID: 14381
		private readonly List<int> playerCountsList = new List<int>();

		// Token: 0x04003831 RID: 14385
		private MemoizedToString<int, ToStringImplementationInvariant> localPlayerCountToString = MemoizedToString<int, ToStringImplementationInvariant>.GetNew();

		// Token: 0x04003832 RID: 14386
		private const string LobbyJoinIndicator = "ClipboardJoin";

		// Token: 0x04003833 RID: 14387
		private bool startingFadeSet;

		// Token: 0x04003834 RID: 14388
		private string lastHostingLobbyId;

		// Token: 0x04003835 RID: 14389
		private const float quickplayCutoffTime = 30f;

		// Token: 0x04003836 RID: 14390
		private Queue<EOSLobbyManager.LobbyRefreshRequest> lobbyRefreshRequests = new Queue<EOSLobbyManager.LobbyRefreshRequest>();

		// Token: 0x04003837 RID: 14391
		private EOSLobbyManager.LobbyRefreshRequest? currentRefreshRequest;

		// Token: 0x04003838 RID: 14392
		private bool hostingServer;

		// Token: 0x04003839 RID: 14393
		private UserID currentServerId;

		// Token: 0x0400383A RID: 14394
		private static readonly char[] charactersToCheck = "abcdef".ToCharArray();

		// Token: 0x020009A1 RID: 2465
		public class Filter
		{
			// Token: 0x0400383B RID: 14395
			public List<AttributeData> SearchData = new List<AttributeData>();
		}

		// Token: 0x020009A2 RID: 2466
		private struct LobbyRefreshRequest
		{
			// Token: 0x0400383C RID: 14396
			public object requester;

			// Token: 0x0400383D RID: 14397
			public EOSLobbyManager.Filter filter;

			// Token: 0x0400383E RID: 14398
			public Action<List<LobbyDetails>> callback;
		}
	}
}
