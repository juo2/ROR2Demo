using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Rewired;
using RoR2.ConVar;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RoR2
{
	// Token: 0x0200095E RID: 2398
	public static class LocalUserManager
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000E63AF File Offset: 0x000E45AF
		public static bool isAnyUserSignedIn
		{
			get
			{
				return LocalUserManager.localUsersList.Count > 0;
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000E63C0 File Offset: 0x000E45C0
		public static bool UserExists(Player inputPlayer)
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i].inputPlayer == inputPlayer)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000E63F8 File Offset: 0x000E45F8
		private static int FindUserIndex(int userId)
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i].id == userId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000E6430 File Offset: 0x000E4630
		public static LocalUser FindLocalUser(int userId)
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i].id == userId)
				{
					return LocalUserManager.localUsersList[i];
				}
			}
			return null;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000E6474 File Offset: 0x000E4674
		public static LocalUser FindLocalUser(Player inputPlayer)
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i].inputPlayer == inputPlayer)
				{
					return LocalUserManager.localUsersList[i];
				}
			}
			return null;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000E64B6 File Offset: 0x000E46B6
		public static LocalUser GetFirstLocalUser()
		{
			if (LocalUserManager.localUsersList.Count <= 0)
			{
				return null;
			}
			return LocalUserManager.localUsersList[0];
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000E64D4 File Offset: 0x000E46D4
		private static int FindUserIndex(Player inputPlayer)
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i].inputPlayer == inputPlayer)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000E650C File Offset: 0x000E470C
		private static int GetFirstAvailableId()
		{
			int i;
			for (i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.FindUserIndex(i) == -1)
				{
					return i;
				}
			}
			return i;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000E653C File Offset: 0x000E473C
		private static void AddUser(Player inputPlayer, UserProfile userProfile)
		{
			if (LocalUserManager.UserExists(inputPlayer))
			{
				return;
			}
			int firstAvailableId = LocalUserManager.GetFirstAvailableId();
			LocalUser localUser = new LocalUser
			{
				inputPlayer = inputPlayer,
				id = firstAvailableId,
				userProfile = userProfile
			};
			LocalUserManager.localUsersList.Add(localUser);
			userProfile.OnLogin();
			MPEventSystem.FindByPlayer(inputPlayer).localUser = localUser;
			if (LocalUserManager.onUserSignIn != null)
			{
				LocalUserManager.onUserSignIn(localUser);
			}
			if (LocalUserManager.onLocalUsersUpdated != null)
			{
				LocalUserManager.onLocalUsersUpdated();
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000E65B4 File Offset: 0x000E47B4
		public static bool IsUserChangeSafe()
		{
			return SceneManager.GetActiveScene().name == "title";
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000E65E0 File Offset: 0x000E47E0
		public static void SetLocalUsers(LocalUserManager.LocalUserInitializationInfo[] initializationInfo)
		{
			if (LocalUserManager.localUsersList.Count > 0)
			{
				Debug.LogError("Cannot call LocalUserManager.SetLocalUsers while users are already signed in!");
				return;
			}
			if (!LocalUserManager.IsUserChangeSafe())
			{
				Debug.LogError("Cannot call LocalUserManager.SetLocalUsers at this time, user login changes are not safe at this time.");
				return;
			}
			if (initializationInfo.Length == 1)
			{
				initializationInfo[0].player = LocalUserManager.GetRewiredMainPlayer();
			}
			for (int i = 0; i < initializationInfo.Length; i++)
			{
				LocalUserManager.AddUser(initializationInfo[i].player, initializationInfo[i].profile);
			}
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000E6659 File Offset: 0x000E4859
		private static Player GetRewiredMainPlayer()
		{
			return ReInput.players.GetPlayer("PlayerMain");
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000E666A File Offset: 0x000E486A
		private static void AddMainUser(UserProfile userProfile)
		{
			LocalUserManager.AddUser(LocalUserManager.GetRewiredMainPlayer(), userProfile);
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000E6678 File Offset: 0x000E4878
		private static bool AddMainUser(string userProfileName)
		{
			UserProfile profile = PlatformSystems.saveSystem.GetProfile(userProfileName);
			if (profile != null && !profile.isCorrupted)
			{
				LocalUserManager.AddMainUser(profile);
				return true;
			}
			return false;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000E66A8 File Offset: 0x000E48A8
		private static void RemoveUser(Player inputPlayer)
		{
			int num = LocalUserManager.FindUserIndex(inputPlayer);
			if (num != -1)
			{
				LocalUserManager.RemoveUser(num);
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000E66C8 File Offset: 0x000E48C8
		private static void RemoveUser(int userIndex)
		{
			LocalUser localUser = LocalUserManager.localUsersList[userIndex];
			if (LocalUserManager.onUserSignOut != null)
			{
				LocalUserManager.onUserSignOut(localUser);
			}
			localUser.userProfile.OnLogout();
			MPEventSystem.FindByPlayer(localUser.inputPlayer).localUser = null;
			LocalUserManager.localUsersList.RemoveAt(userIndex);
			if (LocalUserManager.onLocalUsersUpdated != null)
			{
				LocalUserManager.onLocalUsersUpdated();
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000E672C File Offset: 0x000E492C
		public static void ClearUsers()
		{
			if (!LocalUserManager.IsUserChangeSafe())
			{
				Debug.LogError("Cannot call LocalUserManager.SetLocalUsers at this time, user login changes are not safe at this time.");
				return;
			}
			for (int i = LocalUserManager.localUsersList.Count - 1; i >= 0; i--)
			{
				LocalUserManager.RemoveUser(i);
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000E6768 File Offset: 0x000E4968
		private static Player ListenForStartSignIn()
		{
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				Player player = players[i];
				if (!(player.name == "PlayerMain") && !LocalUserManager.UserExists(player) && player.GetButtonDown(11))
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000E67C0 File Offset: 0x000E49C0
		public static void Init()
		{
			LocalUserManager.ready = true;
			RoR2Application.onUpdate += LocalUserManager.Update;
			SaveSystem.onAvailableUserProfilesChanged += LocalUserManager.<>c.<>9.<Init>g__LogInWithPreviousSessionUser|24_0;
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000E67F0 File Offset: 0x000E49F0
		private static void Update()
		{
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				LocalUserManager.localUsersList[i].RebuildControlChain();
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06003688 RID: 13960 RVA: 0x000E6824 File Offset: 0x000E4A24
		// (remove) Token: 0x06003689 RID: 13961 RVA: 0x000E6858 File Offset: 0x000E4A58
		public static event Action<LocalUser> onUserSignIn;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x0600368A RID: 13962 RVA: 0x000E688C File Offset: 0x000E4A8C
		// (remove) Token: 0x0600368B RID: 13963 RVA: 0x000E68C0 File Offset: 0x000E4AC0
		public static event Action<LocalUser> onUserSignOut;

		// Token: 0x0600368C RID: 13964 RVA: 0x000E68F3 File Offset: 0x000E4AF3
		[ConCommand(commandName = "remove_all_local_users", flags = ConVarFlags.None, helpText = "Removes all local users.")]
		private static void CCRemoveAllLocalUsers(ConCommandArgs args)
		{
			LocalUserManager.ClearUsers();
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000E68FC File Offset: 0x000E4AFC
		[ConCommand(commandName = "print_local_users", flags = ConVarFlags.None, helpText = "Prints a list of all local users.")]
		private static void CCPrintLocalUsers(ConCommandArgs args)
		{
			string[] array = new string[LocalUserManager.localUsersList.Count];
			for (int i = 0; i < LocalUserManager.localUsersList.Count; i++)
			{
				if (LocalUserManager.localUsersList[i] != null)
				{
					array[i] = string.Format("localUsersList[{0}] id={1} userProfile={2}", i, LocalUserManager.localUsersList[i].id, (LocalUserManager.localUsersList[i].userProfile != null) ? LocalUserManager.localUsersList[i].userProfile.fileName : "null");
				}
				else
				{
					array[i] = string.Format("localUsersList[{0}] null", i);
				}
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000E69BC File Offset: 0x000E4BBC
		[ConCommand(commandName = "test_splitscreen", flags = ConVarFlags.None, helpText = "Logs in the specified number of guest users, or two by default.")]
		private static void CCTestSplitscreen(ConCommandArgs args)
		{
			int num = 2;
			int value;
			if (args.Count >= 1 && TextSerialization.TryParseInvariant(args[0], out value))
			{
				num = Mathf.Clamp(value, 1, 4);
			}
			if (!NetworkClient.active)
			{
				LocalUserManager.ClearUsers();
				LocalUserManager.LocalUserInitializationInfo[] array = new LocalUserManager.LocalUserInitializationInfo[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new LocalUserManager.LocalUserInitializationInfo
					{
						player = ReInput.players.GetPlayer(2 + i),
						profile = PlatformSystems.saveSystem.CreateGuestProfile()
					};
				}
				LocalUserManager.SetLocalUsers(array);
			}
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000E6A4C File Offset: 0x000E4C4C
		[ConCommand(commandName = "export_controller_maps", flags = ConVarFlags.None, helpText = "Prints all Rewired ControllerMaps of the first player as xml.")]
		public static void CCExportControllerMaps(ConCommandArgs args)
		{
			if (LocalUserManager.localUsersList.Count <= 0)
			{
				return;
			}
			foreach (string message in from v in LocalUserManager.localUsersList[0].inputPlayer.controllers.maps.GetAllMaps()
			select v.ToXmlString())
			{
				Debug.Log(message);
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06003690 RID: 13968 RVA: 0x000E6AE4 File Offset: 0x000E4CE4
		// (remove) Token: 0x06003691 RID: 13969 RVA: 0x000E6B18 File Offset: 0x000E4D18
		public static event Action onLocalUsersUpdated;

		// Token: 0x04003704 RID: 14084
		private static readonly List<LocalUser> localUsersList = new List<LocalUser>();

		// Token: 0x04003705 RID: 14085
		public static readonly ReadOnlyCollection<LocalUser> readOnlyLocalUsersList = LocalUserManager.localUsersList.AsReadOnly();

		// Token: 0x04003706 RID: 14086
		public static Player startPlayer;

		// Token: 0x04003707 RID: 14087
		private static bool ready = false;

		// Token: 0x0200095F RID: 2399
		public struct LocalUserInitializationInfo
		{
			// Token: 0x0400370B RID: 14091
			public Player player;

			// Token: 0x0400370C RID: 14092
			public UserProfile profile;
		}

		// Token: 0x02000960 RID: 2400
		private class UserProfileMainConVar : BaseConVar
		{
			// Token: 0x06003693 RID: 13971 RVA: 0x00009F73 File Offset: 0x00008173
			public UserProfileMainConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003694 RID: 13972 RVA: 0x000E6B6C File Offset: 0x000E4D6C
			public override void SetString(string newValue)
			{
				this.lastReceivedValue = newValue;
				if (LocalUserManager.isAnyUserSignedIn)
				{
					Debug.Log("Can't change user_profile_main while there are users signed in.");
					return;
				}
				if (!LocalUserManager.ready)
				{
					return;
				}
				LocalUserManager.AddMainUser(newValue);
			}

			// Token: 0x06003695 RID: 13973 RVA: 0x000E6B98 File Offset: 0x000E4D98
			public override string GetString()
			{
				int num = LocalUserManager.FindUserIndex(LocalUserManager.GetRewiredMainPlayer());
				if (num == -1)
				{
					return "";
				}
				return LocalUserManager.localUsersList[num].userProfile.fileName;
			}

			// Token: 0x0400370D RID: 14093
			public static LocalUserManager.UserProfileMainConVar instance = new LocalUserManager.UserProfileMainConVar("user_profile_main", ConVarFlags.Archive, null, "The current user profile.");

			// Token: 0x0400370E RID: 14094
			public string lastReceivedValue;
		}
	}
}
