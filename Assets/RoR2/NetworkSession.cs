using System;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;
using RoR2.ConVar;
using RoR2.ExpansionManagement;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007D0 RID: 2000
	public class NetworkSession : NetworkBehaviour
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000B71A6 File Offset: 0x000B53A6
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000B71AD File Offset: 0x000B53AD
		public static NetworkSession instance { get; private set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000B71B5 File Offset: 0x000B53B5
		// (set) Token: 0x06002A7D RID: 10877 RVA: 0x000B71BD File Offset: 0x000B53BD
		public NetworkSession.Flags flags
		{
			get
			{
				return (NetworkSession.Flags)this._flags;
			}
			set
			{
				this.Network_flags = (uint)value;
			}
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000B71C6 File Offset: 0x000B53C6
		private void SetFlag(NetworkSession.Flags flag, bool flagEnabled)
		{
			if (flagEnabled)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000B71E9 File Offset: 0x000B53E9
		public bool HasFlag(NetworkSession.Flags flag)
		{
			return (this.flags & flag) == flag;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000B71F8 File Offset: 0x000B53F8
		public override void OnStartServer()
		{
			base.OnStartServer();
			this.SetFlag(NetworkSession.Flags.IsDedicatedServer, false);
			NetworkManagerSystem.SvPasswordConVar.instance.onValueChanged += this.UpdatePasswordFlag;
			this.UpdatePasswordFlag(NetworkManagerSystem.SvPasswordConVar.instance.value);
			this.RegisterTags();
			this.NetworkmaxPlayers = (uint)NetworkManagerSystem.SvMaxPlayersConVar.instance.intValue;
			NetworkManagerSystem.SvHostNameConVar.instance.onValueChanged += this.UpdateServerName;
			this.UpdateServerName(NetworkManagerSystem.SvHostNameConVar.instance.GetString());
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000B7278 File Offset: 0x000B5478
		private void RegisterTags()
		{
			if (PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				this.serverManager = ServerManagerBase<EOSServerManager>.instance;
			}
			else
			{
				this.serverManager = ServerManagerBase<SteamworksServerManager>.instance;
				this.NetworkserverSteamId = NetworkManagerSystem.singleton.serverP2PId.steamValue;
			}
			if (this.serverManager != null)
			{
				TagManager tagManager = this.serverManager;
				tagManager.onTagsStringUpdated = (Action<string>)Delegate.Combine(tagManager.onTagsStringUpdated, new Action<string>(this.UpdateTagsString));
				this.UpdateTagsString(this.serverManager.tagsString ?? string.Empty);
			}
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000B7302 File Offset: 0x000B5502
		private void OnDestroy()
		{
			NetworkManagerSystem.SvHostNameConVar.instance.onValueChanged -= this.UpdateServerName;
			NetworkManagerSystem.SvPasswordConVar.instance.onValueChanged -= this.UpdatePasswordFlag;
			this.UnregisterTags();
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000B7336 File Offset: 0x000B5536
		private void UnregisterTags()
		{
			if (this.serverManager != null)
			{
				TagManager tagManager = this.serverManager;
				tagManager.onTagsStringUpdated = (Action<string>)Delegate.Remove(tagManager.onTagsStringUpdated, new Action<string>(this.UpdateTagsString));
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000B7367 File Offset: 0x000B5567
		private void UpdateTagsString(string tagsString)
		{
			this.NetworktagsString = tagsString;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000B7370 File Offset: 0x000B5570
		public override void OnStartClient()
		{
			base.OnStartClient();
			this.SteamworksAdvertiseGame();
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000B737E File Offset: 0x000B557E
		private void UpdatePasswordFlag(string password)
		{
			this.SetFlag(NetworkSession.Flags.HasPassword, !string.IsNullOrEmpty(password));
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000B7390 File Offset: 0x000B5590
		private void OnSyncSteamId(ulong newValue)
		{
			this.NetworkserverSteamId = newValue;
			this.SteamworksAdvertiseGame();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000B73A0 File Offset: 0x000B55A0
		private void SteamworksAdvertiseGame()
		{
			if (RoR2Application.instance.steamworksClient != null)
			{
				ulong num = this.serverSteamId;
				uint num2 = 0U;
				ushort num3 = 0;
				NetworkSession.<SteamworksAdvertiseGame>g__CallMethod|25_1(NetworkSession.<SteamworksAdvertiseGame>g__GetField|25_2(NetworkSession.<SteamworksAdvertiseGame>g__GetField|25_2(Client.Instance, "native"), "user"), "AdvertiseGame", new object[]
				{
					num,
					num2,
					num3
				});
			}
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000B7408 File Offset: 0x000B5608
		private void OnEnable()
		{
			NetworkSession.instance = SingletonHelper.Assign<NetworkSession>(NetworkSession.instance, this);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000B741A File Offset: 0x000B561A
		private void OnDisable()
		{
			NetworkSession.instance = SingletonHelper.Unassign<NetworkSession>(NetworkSession.instance, this);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000B742C File Offset: 0x000B562C
		private void Start()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			if (NetworkServer.active)
			{
				NetworkServer.Spawn(base.gameObject);
			}
			base.StartCoroutine(this.SteamworksLobbyPersistenceCoroutine());
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000B7458 File Offset: 0x000B5658
		private void UpdateServerName(string newHostName)
		{
			this.NetworkserverName = newHostName;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000B7464 File Offset: 0x000B5664
		[Server]
		public Run BeginRun(Run runPrefabComponent, RuleBook ruleBook, ulong seed)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.Run RoR2.NetworkSession::BeginRun(RoR2.Run,RoR2.RuleBook,System.UInt64)' called on client");
				return null;
			}
			if (!Run.instance)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(runPrefabComponent.gameObject);
				Run component = gameObject.GetComponent<Run>();
				component.SetRuleBook(ruleBook);
				component.seed = seed;
				NetworkServer.Spawn(gameObject);
				foreach (ExpansionDef expansionDef in ExpansionCatalog.expansionDefs)
				{
					if (component.IsExpansionEnabled(expansionDef) && expansionDef.runBehaviorPrefab)
					{
						NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(expansionDef.runBehaviorPrefab, gameObject.transform));
					}
				}
				return component;
			}
			return null;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000B753C File Offset: 0x000B573C
		[Server]
		public void EndRun()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkSession::EndRun()' called on client");
				return;
			}
			if (Run.instance)
			{
				UnityEngine.Object.Destroy(Run.instance.gameObject);
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000B756E File Offset: 0x000B576E
		private IEnumerator SteamworksLobbyPersistenceCoroutine()
		{
			for (;;)
			{
				this.UpdateSteamworksLobbyPersistence();
				yield return new WaitForSecondsRealtime(4f);
			}
			yield break;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000B7580 File Offset: 0x000B5780
		private void UpdateSteamworksLobbyPersistence()
		{
			if (Client.Instance == null)
			{
				return;
			}
			if (!NetworkSession.cvSteamLobbyAllowPersistence.value)
			{
				return;
			}
			if (NetworkServer.dontListen)
			{
				return;
			}
			if (PlatformSystems.lobbyManager.awaitingJoin || PlatformSystems.lobbyManager.awaitingCreate)
			{
				return;
			}
			ulong currentLobby = Client.Instance.Lobby.CurrentLobby;
			if (!NetworkServer.active)
			{
				if (this.lobbySteamId != 0UL && this.lobbySteamId != currentLobby)
				{
					PlatformSystems.lobbyManager.JoinLobby(new UserID(this.lobbySteamId));
				}
				return;
			}
			if (PlatformSystems.lobbyManager.isInLobby)
			{
				this.NetworklobbySteamId = currentLobby;
				return;
			}
			PlatformSystems.lobbyManager.CreateLobby();
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000B763C File Offset: 0x000B583C
		[CompilerGenerated]
		internal static uint <SteamworksAdvertiseGame>g__GetServerAddress|25_0()
		{
			byte[] addressBytes = IPAddress.Parse(NetworkClient.allClients[0].connection.address).GetAddressBytes();
			if (addressBytes.Length != 4)
			{
				return 0U;
			}
			return (uint)IPAddress.NetworkToHostOrder((long)((ulong)BitConverter.ToUInt32(addressBytes, 0)));
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000B767F File Offset: 0x000B587F
		[CompilerGenerated]
		internal static void <SteamworksAdvertiseGame>g__CallMethod|25_1(object obj, string methodName, object[] args)
		{
			obj.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(obj, args);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000B7697 File Offset: 0x000B5897
		[CompilerGenerated]
		internal static object <SteamworksAdvertiseGame>g__GetField|25_2(object obj, string fieldName)
		{
			return obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000B76B0 File Offset: 0x000B58B0
		// (set) Token: 0x06002A98 RID: 10904 RVA: 0x000B76C3 File Offset: 0x000B58C3
		public ulong NetworkserverSteamId
		{
			get
			{
				return this.serverSteamId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncSteamId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<ulong>(value, ref this.serverSteamId, 1U);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000B7704 File Offset: 0x000B5904
		// (set) Token: 0x06002A9A RID: 10906 RVA: 0x000B7717 File Offset: 0x000B5917
		public ulong NetworklobbySteamId
		{
			get
			{
				return this.lobbySteamId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<ulong>(value, ref this.lobbySteamId, 2U);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000B772C File Offset: 0x000B592C
		// (set) Token: 0x06002A9C RID: 10908 RVA: 0x000B773F File Offset: 0x000B593F
		public uint Network_flags
		{
			get
			{
				return this._flags;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this._flags, 4U);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x000B7754 File Offset: 0x000B5954
		// (set) Token: 0x06002A9E RID: 10910 RVA: 0x000B7767 File Offset: 0x000B5967
		public string NetworktagsString
		{
			get
			{
				return this.tagsString;
			}
			[param: In]
			set
			{
				base.SetSyncVar<string>(value, ref this.tagsString, 8U);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000B777C File Offset: 0x000B597C
		// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x000B778F File Offset: 0x000B598F
		public uint NetworkmaxPlayers
		{
			get
			{
				return this.maxPlayers;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this.maxPlayers, 16U);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000B77A4 File Offset: 0x000B59A4
		// (set) Token: 0x06002AA2 RID: 10914 RVA: 0x000B77B7 File Offset: 0x000B59B7
		public string NetworkserverName
		{
			get
			{
				return this.serverName;
			}
			[param: In]
			set
			{
				base.SetSyncVar<string>(value, ref this.serverName, 32U);
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000B77CC File Offset: 0x000B59CC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt64(this.serverSteamId);
				writer.WritePackedUInt64(this.lobbySteamId);
				writer.WritePackedUInt32(this._flags);
				writer.Write(this.tagsString);
				writer.WritePackedUInt32(this.maxPlayers);
				writer.Write(this.serverName);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt64(this.serverSteamId);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt64(this.lobbySteamId);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32(this._flags);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.tagsString);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32(this.maxPlayers);
			}
			if ((base.syncVarDirtyBits & 32U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.serverName);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000B7974 File Offset: 0x000B5B74
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.serverSteamId = reader.ReadPackedUInt64();
				this.lobbySteamId = reader.ReadPackedUInt64();
				this._flags = reader.ReadPackedUInt32();
				this.tagsString = reader.ReadString();
				this.maxPlayers = reader.ReadPackedUInt32();
				this.serverName = reader.ReadString();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncSteamId(reader.ReadPackedUInt64());
			}
			if ((num & 2) != 0)
			{
				this.lobbySteamId = reader.ReadPackedUInt64();
			}
			if ((num & 4) != 0)
			{
				this._flags = reader.ReadPackedUInt32();
			}
			if ((num & 8) != 0)
			{
				this.tagsString = reader.ReadString();
			}
			if ((num & 16) != 0)
			{
				this.maxPlayers = reader.ReadPackedUInt32();
			}
			if ((num & 32) != 0)
			{
				this.serverName = reader.ReadString();
			}
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D9C RID: 11676
		[SyncVar(hook = "OnSyncSteamId")]
		private ulong serverSteamId;

		// Token: 0x04002D9D RID: 11677
		[SyncVar]
		public ulong lobbySteamId;

		// Token: 0x04002D9E RID: 11678
		[SyncVar]
		private uint _flags;

		// Token: 0x04002D9F RID: 11679
		[SyncVar]
		public string tagsString;

		// Token: 0x04002DA0 RID: 11680
		[SyncVar]
		public uint maxPlayers;

		// Token: 0x04002DA1 RID: 11681
		[SyncVar]
		public string serverName;

		// Token: 0x04002DA2 RID: 11682
		private TagManager serverManager;

		// Token: 0x04002DA3 RID: 11683
		private static readonly BoolConVar cvSteamLobbyAllowPersistence = new BoolConVar("steam_lobby_allow_persistence", ConVarFlags.None, "1", "Whether or not the application should attempt to reestablish an active game session's Steamworks lobby if it's been lost.");

		// Token: 0x020007D1 RID: 2001
		[Flags]
		public enum Flags
		{
			// Token: 0x04002DA5 RID: 11685
			None = 0,
			// Token: 0x04002DA6 RID: 11686
			HasPassword = 1,
			// Token: 0x04002DA7 RID: 11687
			IsDedicatedServer = 2
		}
	}
}
