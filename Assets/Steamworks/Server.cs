using System;
using System.Collections.Generic;
using System.Net;
using Facepunch.Steamworks.Interop;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200017A RID: 378
	public class Server : BaseSteamworks
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000398B0 File Offset: 0x00037AB0
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000398B7 File Offset: 0x00037AB7
		public static Server Instance { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000398BF File Offset: 0x00037ABF
		internal override bool IsGameServer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000398C2 File Offset: 0x00037AC2
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x000398CA File Offset: 0x00037ACA
		public ulong SteamId { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000398D3 File Offset: 0x00037AD3
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x000398DB File Offset: 0x00037ADB
		public ServerQuery Query { get; internal set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000398E4 File Offset: 0x00037AE4
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000398EC File Offset: 0x00037AEC
		public ServerStats Stats { get; internal set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000398F5 File Offset: 0x00037AF5
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x000398FD File Offset: 0x00037AFD
		public ServerAuth Auth { get; internal set; }

		// Token: 0x06000BEA RID: 3050 RVA: 0x00039908 File Offset: 0x00037B08
		public Server(uint appId, ServerInit init) : base(appId)
		{
			if (Server.Instance != null)
			{
				throw new Exception("Only one Facepunch.Steamworks.Server can exist - dispose the old one before trying to create a new one.");
			}
			Server.Instance = this;
			this.native = new NativeInterface();
			uint ipAddress = 0U;
			if (init.SteamPort == 0)
			{
				init.RandomSteamPort();
			}
			if (init.IpAddress != null)
			{
				ipAddress = init.IpAddress.IpToInt32();
			}
			if (!this.native.InitServer(this, ipAddress, init.SteamPort, init.GamePort, init.QueryPort, init.Secure ? 3 : 2, init.VersionString))
			{
				this.native.Dispose();
				this.native = null;
				Server.Instance = null;
				return;
			}
			Callbacks.RegisterCallbacks(this);
			base.SetupCommonInterfaces();
			this.native.gameServer.EnableHeartbeats(true);
			this.MaxPlayers = 32;
			this.BotCount = 0;
			this.Product = string.Format("{0}", base.AppId);
			this.ModDir = init.ModDir;
			this.GameDescription = init.GameDescription;
			this.GameData = init.GameData;
			this.Passworded = false;
			this.DedicatedServer = true;
			this.Query = new ServerQuery(this);
			this.Stats = new ServerStats(this);
			this.Auth = new ServerAuth(this);
			this.Update();
			this.SteamId = this.native.gameServer.GetSteamID();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00039AB8 File Offset: 0x00037CB8
		~Server()
		{
			this.Dispose();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00039AE4 File Offset: 0x00037CE4
		public override void Update()
		{
			if (!base.IsValid)
			{
				return;
			}
			this.native.api.SteamGameServer_RunCallbacks();
			base.Update();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00039B05 File Offset: 0x00037D05
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00039B0D File Offset: 0x00037D0D
		public bool DedicatedServer
		{
			get
			{
				return this._dedicatedServer;
			}
			set
			{
				if (this._dedicatedServer == value)
				{
					return;
				}
				this.native.gameServer.SetDedicatedServer(value);
				this._dedicatedServer = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00039B31 File Offset: 0x00037D31
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00039B39 File Offset: 0x00037D39
		public int MaxPlayers
		{
			get
			{
				return this._maxplayers;
			}
			set
			{
				if (this._maxplayers == value)
				{
					return;
				}
				this.native.gameServer.SetMaxPlayerCount(value);
				this._maxplayers = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00039B5D File Offset: 0x00037D5D
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00039B65 File Offset: 0x00037D65
		public int BotCount
		{
			get
			{
				return this._botcount;
			}
			set
			{
				if (this._botcount == value)
				{
					return;
				}
				this.native.gameServer.SetBotPlayerCount(value);
				this._botcount = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00039B89 File Offset: 0x00037D89
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00039B91 File Offset: 0x00037D91
		public string MapName
		{
			get
			{
				return this._mapname;
			}
			set
			{
				if (this._mapname == value)
				{
					return;
				}
				this.native.gameServer.SetMapName(value);
				this._mapname = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00039BBA File Offset: 0x00037DBA
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00039BC2 File Offset: 0x00037DC2
		public string ModDir
		{
			get
			{
				return this._modDir;
			}
			internal set
			{
				if (this._modDir == value)
				{
					return;
				}
				this.native.gameServer.SetModDir(value);
				this._modDir = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00039BEB File Offset: 0x00037DEB
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00039BF3 File Offset: 0x00037DF3
		public string Product
		{
			get
			{
				return this._product;
			}
			internal set
			{
				if (this._product == value)
				{
					return;
				}
				this.native.gameServer.SetProduct(value);
				this._product = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00039C1C File Offset: 0x00037E1C
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00039C24 File Offset: 0x00037E24
		public string GameDescription
		{
			get
			{
				return this._gameDescription;
			}
			internal set
			{
				if (this._gameDescription == value)
				{
					return;
				}
				this.native.gameServer.SetGameDescription(value);
				this._gameDescription = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00039C4D File Offset: 0x00037E4D
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00039C55 File Offset: 0x00037E55
		public string GameData
		{
			get
			{
				return this._gameData;
			}
			internal set
			{
				if (this._gameData == value)
				{
					return;
				}
				this.native.gameServer.SetGameData(value);
				this._gameData = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00039C7E File Offset: 0x00037E7E
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00039C86 File Offset: 0x00037E86
		public string ServerName
		{
			get
			{
				return this._serverName;
			}
			set
			{
				if (this._serverName == value)
				{
					return;
				}
				this.native.gameServer.SetServerName(value);
				this._serverName = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00039CAF File Offset: 0x00037EAF
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00039CB7 File Offset: 0x00037EB7
		public bool Passworded
		{
			get
			{
				return this._passworded;
			}
			set
			{
				if (this._passworded == value)
				{
					return;
				}
				this.native.gameServer.SetPasswordProtected(value);
				this._passworded = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00039CDB File Offset: 0x00037EDB
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00039CE3 File Offset: 0x00037EE3
		public string GameTags
		{
			get
			{
				return this._gametags;
			}
			set
			{
				if (this._gametags == value)
				{
					return;
				}
				this.native.gameServer.SetGameTags(value);
				this._gametags = value;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00039D0C File Offset: 0x00037F0C
		public void LogOnAnonymous()
		{
			this.native.gameServer.LogOnAnonymous();
			this.ForceHeartbeat();
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00039D24 File Offset: 0x00037F24
		public bool LoggedOn
		{
			get
			{
				return this.native.gameServer.BLoggedOn();
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00039D38 File Offset: 0x00037F38
		public void SetKey(string Key, string Value)
		{
			if (this.KeyValue.ContainsKey(Key))
			{
				if (this.KeyValue[Key] == Value)
				{
					return;
				}
				this.KeyValue[Key] = Value;
			}
			else
			{
				this.KeyValue.Add(Key, Value);
			}
			this.native.gameServer.SetKeyValue(Key, Value);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00039D96 File Offset: 0x00037F96
		public void UpdatePlayer(ulong steamid, string name, int score)
		{
			this.native.gameServer.BUpdateUserData(steamid, name, (uint)score);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00039DB4 File Offset: 0x00037FB4
		public override void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.Query != null)
			{
				this.Query = null;
			}
			if (this.Stats != null)
			{
				this.Stats = null;
			}
			if (this.Auth != null)
			{
				this.Auth = null;
			}
			if (Server.Instance == this)
			{
				Server.Instance = null;
			}
			base.Dispose();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00039E0C File Offset: 0x0003800C
		public IPAddress PublicIp
		{
			get
			{
				uint publicIP = this.native.gameServer.GetPublicIP();
				if (publicIP == 0U)
				{
					return null;
				}
				return Utility.Int32ToIp(publicIP);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00039E35 File Offset: 0x00038035
		public bool AutomaticHeartbeats
		{
			set
			{
				this.native.gameServer.EnableHeartbeats(value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00039E48 File Offset: 0x00038048
		public int AutomaticHeartbeatRate
		{
			set
			{
				this.native.gameServer.SetHeartbeatInterval(value);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00039E5B File Offset: 0x0003805B
		public void ForceHeartbeat()
		{
			this.native.gameServer.ForceHeartbeat();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00039E6D File Offset: 0x0003806D
		public User.UserHasLicenseForAppResult UserHasLicenseForApp(ulong steamId, uint appId)
		{
			return (User.UserHasLicenseForAppResult)this.native.gameServer.UserHasLicenseForApp(steamId, appId);
		}

		// Token: 0x04000871 RID: 2161
		private bool _dedicatedServer;

		// Token: 0x04000872 RID: 2162
		private int _maxplayers;

		// Token: 0x04000873 RID: 2163
		private int _botcount;

		// Token: 0x04000874 RID: 2164
		private string _mapname;

		// Token: 0x04000875 RID: 2165
		private string _modDir = "";

		// Token: 0x04000876 RID: 2166
		private string _product = "";

		// Token: 0x04000877 RID: 2167
		private string _gameDescription = "";

		// Token: 0x04000878 RID: 2168
		private string _gameData = "";

		// Token: 0x04000879 RID: 2169
		private string _serverName = "";

		// Token: 0x0400087A RID: 2170
		private bool _passworded;

		// Token: 0x0400087B RID: 2171
		private string _gametags = "";

		// Token: 0x0400087C RID: 2172
		private Dictionary<string, string> KeyValue = new Dictionary<string, string>();
	}
}
