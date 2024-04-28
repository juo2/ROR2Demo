using System;
using System.Globalization;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.Networking;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A6A RID: 2666
	internal static class SteamworksInviteManager
	{
		// Token: 0x06003D24 RID: 15652 RVA: 0x000FC89E File Offset: 0x000FAA9E
		private static void SetKeyValue([NotNull] string key, [CanBeNull] string value)
		{
			Client.Instance.User.SetRichPresence(key, value);
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000FC8B4 File Offset: 0x000FAAB4
		private static void OnNetworkStart()
		{
			string text = null;
			CSteamID a = CSteamID.nil;
			CSteamID a2 = CSteamID.nil;
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length - 1; i++)
			{
				string a3 = commandLineArgs[i].ToLower(CultureInfo.InvariantCulture);
				CSteamID csteamID2;
				if (a3 == "+connect")
				{
					AddressPortPair addressPortPair;
					if (AddressPortPair.TryParse(commandLineArgs[i + 1], out addressPortPair))
					{
						text = addressPortPair.address + ":" + addressPortPair.port;
					}
				}
				else if (a3 == "+connect_steamworks_p2p")
				{
					CSteamID csteamID;
					if (CSteamID.TryParse(commandLineArgs[i + 1], out csteamID))
					{
						a = csteamID;
					}
				}
				else if (a3 == "+steam_lobby_join" && CSteamID.TryParse(commandLineArgs[i + 1], out csteamID2))
				{
					a2 = csteamID2;
				}
			}
			if (a2 != CSteamID.nil)
			{
				Console.instance.SubmitCmd(null, "steam_lobby_join " + a2.value, false);
				return;
			}
			if (a != CSteamID.nil)
			{
				Console.instance.SubmitCmd(null, "connect_steamworks_p2p " + a.value, false);
				return;
			}
			if (text != null)
			{
				Console.instance.SubmitCmd(null, "connect " + text, false);
			}
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000FC9EE File Offset: 0x000FABEE
		private static void OnLobbyChanged()
		{
			if (PlatformSystems.lobbyManager.isInLobby)
			{
				SteamworksInviteManager.SetKeyValue("connect", "+steam_lobby_join " + PlatformSystems.lobbyManager.GetLobbyID());
				return;
			}
			SteamworksInviteManager.SetKeyValue("connect", null);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000FCA28 File Offset: 0x000FAC28
		private static void OnInvitedToGame(SteamFriend steamFriend, string connectString)
		{
			Debug.LogFormat("OnGameRichPresenceJoinRequested connectString=\"{0}\" steamFriend=\"{1}\"", new object[]
			{
				connectString,
				steamFriend.Name
			});
			string[] array = connectString.Split(new char[]
			{
				' '
			});
			if (array.Length >= 2)
			{
				CSteamID csteamID;
				if (array[0] == "+connect_steamworks_p2p" && CSteamID.TryParse(array[1], out csteamID))
				{
					if (!PlatformSystems.lobbyManager.ownsLobby)
					{
						PlatformSystems.lobbyManager.LeaveLobby();
					}
					QuitConfirmationHelper.IssueQuitCommand(null, "connect_steamworks_p2p " + csteamID.value);
				}
				if (array[0] == "+steam_lobby_join")
				{
					if (!(PlatformSystems.lobbyManager as PCLobbyManager).CheckLobbyIdValidity(array[1]))
					{
						if (PlatformSystems.ShouldUseEpicOnlineSystems)
						{
							PCLobbyManager.ShowEnableCrossPlayPopup(false);
							return;
						}
						PCLobbyManager.ShowEnableCrossPlayPopup(true);
						return;
					}
					else
					{
						bool shouldUseEpicOnlineSystems = PlatformSystems.ShouldUseEpicOnlineSystems;
						CSteamID csteamID2;
						if (CSteamID.TryParse(array[1], out csteamID2) || shouldUseEpicOnlineSystems)
						{
							if (!PlatformSystems.lobbyManager.ownsLobby)
							{
								PlatformSystems.lobbyManager.LeaveLobby();
							}
							string str = shouldUseEpicOnlineSystems ? array[1] : csteamID2.value.ToString();
							QuitConfirmationHelper.IssueQuitCommand(null, "steam_lobby_join " + str);
						}
					}
				}
			}
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000FCB40 File Offset: 0x000FAD40
		private static void OnGameServerChangeRequested(string address, string password)
		{
			Debug.LogFormat("OnGameServerChangeRequested address=\"{0}\"", new object[]
			{
				address
			});
			if (!PlatformSystems.lobbyManager.ownsLobby)
			{
				PlatformSystems.lobbyManager.LeaveLobby();
			}
			string consoleCmd = string.Format("cl_password \"{0}\"; connect \"{1}\"", Util.EscapeQuotes(password), Util.EscapeQuotes(address));
			QuitConfirmationHelper.IssueQuitCommand(null, consoleCmd);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000FCB98 File Offset: 0x000FAD98
		private static void SetupCallbacks()
		{
			NetworkManagerSystem.onStartGlobal += SteamworksInviteManager.OnNetworkStart;
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyChanged = (Action)Delegate.Combine(lobbyManager.onLobbyChanged, new Action(SteamworksInviteManager.OnLobbyChanged));
			if (Client.Instance != null)
			{
				Client.Instance.Friends.OnInvitedToGame += SteamworksInviteManager.OnInvitedToGame;
				Client.Instance.Friends.OnGameServerChangeRequested = new Action<string, string>(SteamworksInviteManager.OnGameServerChangeRequested);
			}
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000FCC19 File Offset: 0x000FAE19
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			SteamworksClientManager.onLoaded += SteamworksInviteManager.SetupCallbacks;
		}

		// Token: 0x04003C46 RID: 15430
		private const string rpConnect = "connect";

		// Token: 0x04003C47 RID: 15431
		private const string rpStatus = "status";
	}
}
