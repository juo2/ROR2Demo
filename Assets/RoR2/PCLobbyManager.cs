using System;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using RoR2.ConVar;
using RoR2.UI;
using RoR2.UI.MainMenu;

namespace RoR2
{
	// Token: 0x020009C8 RID: 2504
	public abstract class PCLobbyManager : LobbyManager
	{
		// Token: 0x06003954 RID: 14676
		public abstract void SetLobbyTypeConVarString(string newValue);

		// Token: 0x06003955 RID: 14677
		public abstract string GetLobbyTypeConVarString();

		// Token: 0x06003956 RID: 14678
		public abstract void JoinLobby(ConCommandArgs lobbyID);

		// Token: 0x06003957 RID: 14679
		public abstract void LobbyCreate(ConCommandArgs args);

		// Token: 0x06003958 RID: 14680
		public abstract void LobbyCreateIfNone(ConCommandArgs args);

		// Token: 0x06003959 RID: 14681
		public abstract void LobbyLeave(ConCommandArgs args);

		// Token: 0x0600395A RID: 14682
		public abstract void LobbyAssignOwner(ConCommandArgs args);

		// Token: 0x0600395B RID: 14683
		public abstract void LobbyInvite(ConCommandArgs args);

		// Token: 0x0600395C RID: 14684
		public abstract void LobbyOpenInviteOverlay(ConCommandArgs args);

		// Token: 0x0600395D RID: 14685
		public abstract void LobbyCopyToClipboard(ConCommandArgs args);

		// Token: 0x0600395E RID: 14686
		public abstract void LobbyPrintData(ConCommandArgs args);

		// Token: 0x0600395F RID: 14687
		public abstract void DisplayId(ConCommandArgs args);

		// Token: 0x06003960 RID: 14688
		public abstract void DisplayLobbyId(ConCommandArgs args);

		// Token: 0x06003961 RID: 14689
		public abstract void LobbyPrintMembers(ConCommandArgs args);

		// Token: 0x06003962 RID: 14690
		public abstract void ClearLobbies(ConCommandArgs args);

		// Token: 0x06003963 RID: 14691
		public abstract void LobbyUpdatePlayerCount(ConCommandArgs args);

		// Token: 0x06003964 RID: 14692
		public abstract void LobbyForceUpdateData(ConCommandArgs args);

		// Token: 0x06003965 RID: 14693
		public abstract void LobbyPrintList(ConCommandArgs args);

		// Token: 0x06003966 RID: 14694
		public abstract bool CheckLobbyIdValidity(string lobbyID);

		// Token: 0x06003967 RID: 14695 RVA: 0x000EF9D0 File Offset: 0x000EDBD0
		public static void ShowEnableCrossPlayPopup(bool isLobbyCrossplay)
		{
			SimpleDialogBox dialogBox = SimpleDialogBox.Create(null);
			Action activateCrossplayAndRestartFunction = delegate()
			{
				if (dialogBox)
				{
					PCLobbyManager.<ShowEnableCrossPlayPopup>g__ActivateCrossPlayAndRestart|20_2();
				}
			};
			dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "EOS_CANNOT_JOIN_STEAM_LOBBY_HEADER",
				formatParams = Array.Empty<object>()
			};
			if (isLobbyCrossplay)
			{
				dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
				{
					token = "EOS_INVALID_LOBBY_REQUIRES_CROSSPLAY_DESCRIPTION",
					formatParams = Array.Empty<object>()
				};
			}
			else
			{
				dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
				{
					token = "EOS_INVALID_LOBBY_NO_CROSSPLAY_DESCRIPTION",
					formatParams = Array.Empty<object>()
				};
			}
			dialogBox.AddActionButton(delegate
			{
				activateCrossplayAndRestartFunction();
			}, "EOS_INVALID_LOBBY_CROSSPLAY_GO_TO_SETTINGS", true, Array.Empty<object>());
			dialogBox.AddCancelButton(CommonLanguageTokens.cancel, Array.Empty<object>());
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000EFAC3 File Offset: 0x000EDCC3
		[ConCommand(commandName = "steam_lobby_join")]
		private static void CCSteamLobbyJoin(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).JoinLobby(args);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000EFAD5 File Offset: 0x000EDCD5
		[ConCommand(commandName = "steam_lobby_create")]
		private static void CCSteamLobbyCreate(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyCreate(args);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000EFAE7 File Offset: 0x000EDCE7
		[ConCommand(commandName = "steam_lobby_create_if_none")]
		private static void CCSteamLobbyCreateIfNone(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyCreateIfNone(args);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000EFAF9 File Offset: 0x000EDCF9
		[ConCommand(commandName = "steam_lobby_leave")]
		private static void CCSteamLobbyLeave(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyLeave(args);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000EFB0B File Offset: 0x000EDD0B
		[ConCommand(commandName = "steam_lobby_assign_owner")]
		private static void CCSteamLobbyAssignOwner(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyAssignOwner(args);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000EFB1D File Offset: 0x000EDD1D
		[ConCommand(commandName = "steam_lobby_invite", flags = ConVarFlags.None, helpText = "Invites the player with the specified steam id to the current lobby.")]
		private static void CCSteamLobbyInvite(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyInvite(args);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000EFB2F File Offset: 0x000EDD2F
		[ConCommand(commandName = "steam_lobby_open_invite_overlay", flags = ConVarFlags.None, helpText = "Opens the steam overlay to the friend invite dialog.")]
		private static void CCSteamLobbyOpenInviteOverlay(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyOpenInviteOverlay(args);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000EFB2F File Offset: 0x000EDD2F
		[ConCommand(commandName = "lobby_open_invite_overlay", flags = ConVarFlags.None, helpText = "Opens the platform overlay to the friend invite dialog.")]
		private static void CCLobbyOpenInviteOverlay(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyOpenInviteOverlay(args);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000EFB41 File Offset: 0x000EDD41
		[ConCommand(commandName = "steam_lobby_copy_to_clipboard", flags = ConVarFlags.None, helpText = "Copies the currently active lobby to the clipboard if applicable.")]
		private static void CCSteamLobbyCopyToClipboard(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyCopyToClipboard(args);
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000EFB53 File Offset: 0x000EDD53
		private static void CCSteamLobbyPrintData(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyPrintData(args);
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000EFB65 File Offset: 0x000EDD65
		[ConCommand(commandName = "steam_id", flags = ConVarFlags.None, helpText = "Displays your steam id.")]
		private static void CCSteamId(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).DisplayId(args);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000EFB77 File Offset: 0x000EDD77
		[ConCommand(commandName = "steam_lobby_id", flags = ConVarFlags.None, helpText = "Displays the steam id of the current lobby.")]
		private static void CCSteamLobbyId(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).DisplayLobbyId(args);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000EFB89 File Offset: 0x000EDD89
		[ConCommand(commandName = "steam_lobby_print_members", flags = ConVarFlags.None, helpText = "Displays the members current lobby.")]
		private static void CCSteamLobbyPrintMembers(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyPrintMembers(args);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000EFB9B File Offset: 0x000EDD9B
		[ConCommand(commandName = "steam_lobby_print_list", flags = ConVarFlags.None, helpText = "Displays a list of lobbies from the last search.")]
		private static void CCSteamLobbyPrintList(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyPrintList(args);
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000EFBAD File Offset: 0x000EDDAD
		[ConCommand(commandName = "steam_lobby_update_player_count", flags = ConVarFlags.None, helpText = "Forces a refresh of the steam lobby player count.")]
		private static void CCSteamLobbyUpdatePlayerCount(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).LobbyUpdatePlayerCount(args);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000EFBBF File Offset: 0x000EDDBF
		[ConCommand(commandName = "dump_lobbies", flags = ConVarFlags.None, helpText = "")]
		private static void DumpLobbies(ConCommandArgs args)
		{
			(PlatformSystems.lobbyManager as PCLobbyManager).ClearLobbies(args);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000EFBD9 File Offset: 0x000EDDD9
		[CompilerGenerated]
		internal static void <ShowEnableCrossPlayPopup>g__ActivateCrossPlayAndRestart|20_2()
		{
			MainMenuController.instance.SetDesiredMenuScreen(MainMenuController.instance.settingsMenuScreen);
		}

		// Token: 0x020009C9 RID: 2505
		protected class SteamLobbyTypeConVar : BaseConVar
		{
			// Token: 0x0600397A RID: 14714 RVA: 0x00009F73 File Offset: 0x00008173
			public SteamLobbyTypeConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x0600397B RID: 14715 RVA: 0x000EFBEF File Offset: 0x000EDDEF
			public override void SetString(string newValue)
			{
				(PlatformSystems.lobbyManager as PCLobbyManager).SetLobbyTypeConVarString(newValue);
			}

			// Token: 0x0600397C RID: 14716 RVA: 0x000EFC01 File Offset: 0x000EDE01
			public override string GetString()
			{
				return (PlatformSystems.lobbyManager as PCLobbyManager).GetLobbyTypeConVarString();
			}

			// Token: 0x0600397D RID: 14717 RVA: 0x000EFC14 File Offset: 0x000EDE14
			public void GetEnumValueAbstract<T>(string str, ref T dest) where T : struct, Enum
			{
				T t;
				if (Enum.TryParse<T>(str, out t))
				{
					dest = t;
					return;
				}
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				stringBuilder.Append("Provided value \"").Append(str).Append("\"").Append(" is not a recognized option. Recognized options: { ");
				bool flag = false;
				foreach (string value in Enum.GetNames(typeof(T)))
				{
					if (flag)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						flag = true;
					}
					stringBuilder.Append("\"").Append(value).Append("\"");
				}
				stringBuilder.Append(" }");
				string message = stringBuilder.ToString();
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				throw new ConCommandException(message);
			}

			// Token: 0x040038F0 RID: 14576
			public static readonly PCLobbyManager.SteamLobbyTypeConVar instance = new PCLobbyManager.SteamLobbyTypeConVar("steam_lobby_type", ConVarFlags.Engine, null, "The type of the current Steamworks lobby. Cannot be set if not the owner of a lobby.");
		}
	}
}
