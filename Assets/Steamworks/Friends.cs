using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000164 RID: 356
	public class Friends
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0003488C File Offset: 0x00032A8C
		internal Friends(Client c)
		{
			this.client = c;
			this.client.RegisterCallback<AvatarImageLoaded_t>(new Action<AvatarImageLoaded_t>(this.OnAvatarImageLoaded));
			this.client.RegisterCallback<PersonaStateChange_t>(new Action<PersonaStateChange_t>(this.OnPersonaStateChange));
			this.client.RegisterCallback<GameRichPresenceJoinRequested_t>(new Action<GameRichPresenceJoinRequested_t>(this.OnGameJoinRequested));
			this.client.RegisterCallback<GameConnectedFriendChatMsg_t>(new Action<GameConnectedFriendChatMsg_t>(this.OnFriendChatMessage));
			this.client.RegisterCallback<GameServerChangeRequested_t>(new Action<GameServerChangeRequested_t>(this.OnGameServerChangeRequestedAPI));
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A76 RID: 2678 RVA: 0x00034934 File Offset: 0x00032B34
		// (remove) Token: 0x06000A77 RID: 2679 RVA: 0x0003496C File Offset: 0x00032B6C
		public event Friends.ChatMessageDelegate OnChatMessage;

		// Token: 0x06000A78 RID: 2680 RVA: 0x000349A4 File Offset: 0x00032BA4
		private unsafe void OnFriendChatMessage(GameConnectedFriendChatMsg_t data)
		{
			if (this.OnChatMessage == null)
			{
				return;
			}
			SteamFriend friend = this.Get(data.SteamIDUser);
			ChatEntryType chatEntryType = ChatEntryType.ChatMsg;
			byte[] array;
			byte* value;
			if ((array = this.buffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			int friendMessage = this.client.native.friends.GetFriendMessage(data.SteamIDUser, data.MessageID, (IntPtr)((void*)value), this.buffer.Length, out chatEntryType);
			if (friendMessage == 0 && chatEntryType == ChatEntryType.Invalid)
			{
				return;
			}
			string type = chatEntryType.ToString();
			string @string = Encoding.UTF8.GetString(this.buffer, 0, friendMessage);
			this.OnChatMessage(friend, type, @string);
			array = null;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00034A5C File Offset: 0x00032C5C
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00034A64 File Offset: 0x00032C64
		public bool ListenForFriendsMessages
		{
			get
			{
				return this._listenForFriendsMessages;
			}
			set
			{
				this._listenForFriendsMessages = value;
				this.client.native.friends.SetListenForFriendsMessages(value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000A7B RID: 2683 RVA: 0x00034A84 File Offset: 0x00032C84
		// (remove) Token: 0x06000A7C RID: 2684 RVA: 0x00034ABC File Offset: 0x00032CBC
		public event Friends.JoinRequestedDelegate OnInvitedToGame;

		// Token: 0x06000A7D RID: 2685 RVA: 0x00034AF1 File Offset: 0x00032CF1
		private void OnGameJoinRequested(GameRichPresenceJoinRequested_t data)
		{
			if (this.OnInvitedToGame != null)
			{
				this.OnInvitedToGame(this.Get(data.SteamIDFriend), data.Connect);
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00034B18 File Offset: 0x00032D18
		public bool UpdateInformation(ulong steamid)
		{
			return !this.client.native.friends.RequestUserInformation(steamid, false);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00034B39 File Offset: 0x00032D39
		public string GetName(ulong steamid)
		{
			this.client.native.friends.RequestUserInformation(steamid, true);
			return this.client.native.friends.GetFriendPersonaName(steamid);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00034B73 File Offset: 0x00032D73
		public IEnumerable<SteamFriend> All
		{
			get
			{
				if (this._allFriends == null)
				{
					this._allFriends = new List<SteamFriend>();
					this.Refresh();
				}
				return this._allFriends;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00034B94 File Offset: 0x00032D94
		public IEnumerable<SteamFriend> AllFriends
		{
			get
			{
				foreach (SteamFriend steamFriend in this.All)
				{
					if (steamFriend.IsFriend)
					{
						yield return steamFriend;
					}
				}
				IEnumerator<SteamFriend> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00034BA4 File Offset: 0x00032DA4
		public IEnumerable<SteamFriend> AllBlocked
		{
			get
			{
				foreach (SteamFriend steamFriend in this.All)
				{
					if (steamFriend.IsBlocked)
					{
						yield return steamFriend;
					}
				}
				IEnumerator<SteamFriend> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00034BB4 File Offset: 0x00032DB4
		public void Refresh()
		{
			if (this._allFriends == null)
			{
				this._allFriends = new List<SteamFriend>();
			}
			this._allFriends.Clear();
			int iFriendFlags = 65535;
			int friendCount = this.client.native.friends.GetFriendCount(iFriendFlags);
			for (int i = 0; i < friendCount; i++)
			{
				ulong friendByIndex = this.client.native.friends.GetFriendByIndex(i, iFriendFlags);
				this._allFriends.Add(this.Get(friendByIndex));
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00034C34 File Offset: 0x00032E34
		public Image GetCachedAvatar(Friends.AvatarSize size, ulong steamid)
		{
			int num = 0;
			switch (size)
			{
			case Friends.AvatarSize.Small:
				num = this.client.native.friends.GetSmallFriendAvatar(steamid);
				break;
			case Friends.AvatarSize.Medium:
				num = this.client.native.friends.GetMediumFriendAvatar(steamid);
				break;
			case Friends.AvatarSize.Large:
				num = this.client.native.friends.GetLargeFriendAvatar(steamid);
				break;
			}
			if (num == 1)
			{
				return null;
			}
			if (num == 2)
			{
				return null;
			}
			if (num == 3)
			{
				return null;
			}
			Image image = new Image
			{
				Id = num
			};
			if (!image.TryLoad(this.client.native.utils))
			{
				return null;
			}
			return image;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00034CEC File Offset: 0x00032EEC
		public void GetAvatar(Friends.AvatarSize size, ulong steamid, Action<Image> callback)
		{
			Image cachedAvatar = this.GetCachedAvatar(size, steamid);
			if (cachedAvatar != null)
			{
				callback(cachedAvatar);
				return;
			}
			if (!this.client.native.friends.RequestUserInformation(steamid, false))
			{
				cachedAvatar = this.GetCachedAvatar(size, steamid);
				if (cachedAvatar != null)
				{
					callback(cachedAvatar);
					return;
				}
			}
			this.PersonaCallbacks.Add(new Friends.PersonaCallback
			{
				SteamId = steamid,
				Size = size,
				Callback = callback,
				Time = DateTime.Now
			});
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00034D70 File Offset: 0x00032F70
		public SteamFriend Get(ulong steamid)
		{
			SteamFriend steamFriend = (from x in this.All
			where x.Id == steamid
			select x).FirstOrDefault<SteamFriend>();
			if (steamFriend != null)
			{
				return steamFriend;
			}
			SteamFriend steamFriend2 = new SteamFriend();
			steamFriend2.Id = steamid;
			steamFriend2.Client = this.client;
			steamFriend2.Refresh();
			return steamFriend2;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00034DCF File Offset: 0x00032FCF
		private void OnGameServerChangeRequestedAPI(GameServerChangeRequested_t o)
		{
			if (this.OnGameServerChangeRequested != null)
			{
				this.OnGameServerChangeRequested(o.Server, o.Password);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00034DF0 File Offset: 0x00032FF0
		internal void Cycle()
		{
			if (this.PersonaCallbacks.Count == 0)
			{
				return;
			}
			DateTime t = DateTime.Now.AddSeconds(-10.0);
			for (int i = this.PersonaCallbacks.Count - 1; i >= 0; i--)
			{
				Friends.PersonaCallback personaCallback = this.PersonaCallbacks[i];
				if (personaCallback.Time < t)
				{
					if (personaCallback.Callback != null)
					{
						Image cachedAvatar = this.GetCachedAvatar(personaCallback.Size, personaCallback.SteamId);
						personaCallback.Callback(cachedAvatar);
					}
					this.PersonaCallbacks.Remove(personaCallback);
				}
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00034E8C File Offset: 0x0003308C
		private void OnPersonaStateChange(PersonaStateChange_t data)
		{
			if ((data.ChangeFlags & 64) == 64)
			{
				this.LoadAvatarForSteamId(data.SteamID);
			}
			foreach (SteamFriend steamFriend in this.All)
			{
				if (steamFriend.Id == data.SteamID)
				{
					steamFriend.Refresh();
				}
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00034F00 File Offset: 0x00033100
		private void LoadAvatarForSteamId(ulong Steamid)
		{
			for (int i = this.PersonaCallbacks.Count - 1; i >= 0; i--)
			{
				Friends.PersonaCallback personaCallback = this.PersonaCallbacks[i];
				if (personaCallback.SteamId == Steamid)
				{
					Image cachedAvatar = this.GetCachedAvatar(personaCallback.Size, personaCallback.SteamId);
					if (cachedAvatar != null)
					{
						this.PersonaCallbacks.Remove(personaCallback);
						if (personaCallback.Callback != null)
						{
							personaCallback.Callback(cachedAvatar);
						}
					}
				}
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00034F72 File Offset: 0x00033172
		private void OnAvatarImageLoaded(AvatarImageLoaded_t data)
		{
			this.LoadAvatarForSteamId(data.SteamID);
		}

		// Token: 0x040007E4 RID: 2020
		internal Client client;

		// Token: 0x040007E5 RID: 2021
		private byte[] buffer = new byte[131072];

		// Token: 0x040007E7 RID: 2023
		private bool _listenForFriendsMessages;

		// Token: 0x040007E9 RID: 2025
		private List<SteamFriend> _allFriends;

		// Token: 0x040007EA RID: 2026
		private List<Friends.PersonaCallback> PersonaCallbacks = new List<Friends.PersonaCallback>();

		// Token: 0x040007EB RID: 2027
		public Action<string, string> OnGameServerChangeRequested;

		// Token: 0x0200025A RID: 602
		// (Invoke) Token: 0x06001D9E RID: 7582
		public delegate void ChatMessageDelegate(SteamFriend friend, string type, string message);

		// Token: 0x0200025B RID: 603
		// (Invoke) Token: 0x06001DA2 RID: 7586
		public delegate void JoinRequestedDelegate(SteamFriend friend, string connect);

		// Token: 0x0200025C RID: 604
		public enum AvatarSize
		{
			// Token: 0x04000B94 RID: 2964
			Small,
			// Token: 0x04000B95 RID: 2965
			Medium,
			// Token: 0x04000B96 RID: 2966
			Large
		}

		// Token: 0x0200025D RID: 605
		private class PersonaCallback
		{
			// Token: 0x04000B97 RID: 2967
			public ulong SteamId;

			// Token: 0x04000B98 RID: 2968
			public Friends.AvatarSize Size;

			// Token: 0x04000B99 RID: 2969
			public Action<Image> Callback;

			// Token: 0x04000B9A RID: 2970
			public DateTime Time;
		}
	}
}
