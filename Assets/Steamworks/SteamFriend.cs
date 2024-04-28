using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000172 RID: 370
	public class SteamFriend
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00037DC7 File Offset: 0x00035FC7
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00037DCF File Offset: 0x00035FCF
		public ulong Id { get; internal set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00037DD8 File Offset: 0x00035FD8
		public bool IsBlocked
		{
			get
			{
				return this.relationship == FriendRelationship.Blocked;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00037DE3 File Offset: 0x00035FE3
		public bool IsFriend
		{
			get
			{
				return this.relationship == FriendRelationship.Friend;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00037DEE File Offset: 0x00035FEE
		public bool IsOnline
		{
			get
			{
				return this.personaState > PersonaState.Offline;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00037DF9 File Offset: 0x00035FF9
		public bool IsAway
		{
			get
			{
				return this.personaState == PersonaState.Away;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00037E04 File Offset: 0x00036004
		public bool IsBusy
		{
			get
			{
				return this.personaState == PersonaState.Busy;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00037E0F File Offset: 0x0003600F
		public bool IsSnoozing
		{
			get
			{
				return this.personaState == PersonaState.Snooze;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00037E1A File Offset: 0x0003601A
		public bool IsPlayingThisGame
		{
			get
			{
				return this.CurrentAppId == (ulong)this.Client.AppId;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00037E30 File Offset: 0x00036030
		public bool IsPlaying
		{
			get
			{
				return this.CurrentAppId > 0UL;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00037E3C File Offset: 0x0003603C
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00037E44 File Offset: 0x00036044
		public ulong CurrentAppId { get; internal set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00037E4D File Offset: 0x0003604D
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00037E55 File Offset: 0x00036055
		public uint ServerIp { get; internal set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00037E5E File Offset: 0x0003605E
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x00037E66 File Offset: 0x00036066
		public int ServerGamePort { get; internal set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00037E6F File Offset: 0x0003606F
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00037E77 File Offset: 0x00036077
		public int ServerQueryPort { get; internal set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00037E80 File Offset: 0x00036080
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00037E88 File Offset: 0x00036088
		public ulong ServerLobbyId { get; internal set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00037E91 File Offset: 0x00036091
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00037E99 File Offset: 0x00036099
		internal Client Client { get; set; }

		// Token: 0x06000B78 RID: 2936 RVA: 0x00037EA4 File Offset: 0x000360A4
		public string GetRichPresence(string key)
		{
			string friendRichPresence = this.Client.native.friends.GetFriendRichPresence(this.Id, key);
			if (string.IsNullOrEmpty(friendRichPresence))
			{
				return null;
			}
			return friendRichPresence;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00037EE0 File Offset: 0x000360E0
		public void Refresh()
		{
			this.Name = this.Client.native.friends.GetFriendPersonaName(this.Id);
			this.relationship = this.Client.native.friends.GetFriendRelationship(this.Id);
			this.personaState = this.Client.native.friends.GetFriendPersonaState(this.Id);
			this.CurrentAppId = 0UL;
			this.ServerIp = 0U;
			this.ServerGamePort = 0;
			this.ServerQueryPort = 0;
			this.ServerLobbyId = 0UL;
			FriendGameInfo_t friendGameInfo_t = default(FriendGameInfo_t);
			if (this.Client.native.friends.GetFriendGamePlayed(this.Id, ref friendGameInfo_t) && friendGameInfo_t.GameID > 0UL)
			{
				this.CurrentAppId = friendGameInfo_t.GameID;
				this.ServerIp = friendGameInfo_t.GameIP;
				this.ServerGamePort = (int)friendGameInfo_t.GamePort;
				this.ServerQueryPort = (int)friendGameInfo_t.QueryPort;
				this.ServerLobbyId = friendGameInfo_t.SteamIDLobby;
			}
			this.Client.native.friends.RequestFriendRichPresence(this.Id);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00038016 File Offset: 0x00036216
		public Image GetAvatar(Friends.AvatarSize size)
		{
			return this.Client.Friends.GetCachedAvatar(size, this.Id);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003802F File Offset: 0x0003622F
		public bool InviteToGame(string Text)
		{
			return this.Client.native.friends.InviteUserToGame(this.Id, Text);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00038052 File Offset: 0x00036252
		public bool SendMessage(string message)
		{
			return this.Client.native.friends.ReplyToFriendMessage(this.Id, message);
		}

		// Token: 0x04000835 RID: 2101
		public string Name;

		// Token: 0x0400083C RID: 2108
		private PersonaState personaState;

		// Token: 0x0400083D RID: 2109
		private FriendRelationship relationship;
	}
}
