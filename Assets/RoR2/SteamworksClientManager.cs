using System;
using System.IO;
using System.Text;
using Facepunch.Steamworks;
using HG;
using RoR2.Networking;
using SteamAPIValidator;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A68 RID: 2664
	public sealed class SteamworksClientManager : IDisposable
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x000FC403 File Offset: 0x000FA603
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x000FC40A File Offset: 0x000FA60A
		public static SteamworksClientManager instance { get; private set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000FC412 File Offset: 0x000FA612
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x000FC41A File Offset: 0x000FA61A
		public Client steamworksClient { get; private set; }

		// Token: 0x06003D17 RID: 15639 RVA: 0x000FC424 File Offset: 0x000FA624
		private SteamworksClientManager()
		{
			if (!Application.isEditor && File.Exists("steam_appid.txt"))
			{
				try
				{
					File.Delete("steam_appid.txt");
				}
				catch (Exception ex)
				{
					Debug.Log(ex.Message);
				}
				if (File.Exists("steam_appid.txt"))
				{
					Debug.Log("Cannot delete steam_appid.txt. Quitting...");
					this.Dispose();
					return;
				}
			}
			Config.ForUnity(Application.platform.ToString());
			this.steamworksClient = new Client(632360U);
			if (!this.steamworksClient.IsValid)
			{
				this.Dispose();
				return;
			}
			if (!Application.isEditor)
			{
				if (Client.RestartIfNecessary(632360U) || !this.steamworksClient.IsValid || !SteamApiValidator.IsValidSteamApiDll())
				{
					Debug.Log("Unable to initialize Facepunch.Steamworks.");
					this.Dispose();
					return;
				}
				if (!this.steamworksClient.App.IsSubscribed(632360U))
				{
					Debug.Log("Steam user not subscribed to app. Quitting...");
					this.Dispose();
					return;
				}
			}
			RoR2Application.steamBuildId = TextSerialization.ToStringInvariant(this.steamworksClient.BuildId);
			RoR2Application.onUpdate += this.Update;
			RoR2Application.cloudStorage = new SteamworksRemoteStorageFileSystem();
			SteamworksLobbyManager steamworksLobbyManager = PlatformSystems.lobbyManager as SteamworksLobbyManager;
			if (steamworksLobbyManager == null)
			{
				return;
			}
			steamworksLobbyManager.Init();
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000FC570 File Offset: 0x000FA770
		private void Update()
		{
			this.steamworksClient.Update();
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000FC580 File Offset: 0x000FA780
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			RoR2Application.onUpdate -= this.Update;
			if (this.steamworksClient != null)
			{
				if (NetworkManagerSystem.singleton)
				{
					NetworkManagerSystem.singleton.ForceCloseAllConnections();
				}
				Debug.Log("Shutting down Steamworks...");
				this.steamworksClient.Lobby.Leave();
				if (Server.Instance != null)
				{
					Server.Instance.Dispose();
				}
				this.steamworksClient.Update();
				this.steamworksClient.Dispose();
				this.steamworksClient = null;
				Debug.Log("Shut down Steamworks.");
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000FC620 File Offset: 0x000FA820
		public static void Init()
		{
			RoR2Application.loadSteamworksClient = delegate()
			{
				SteamworksClientManager.instance = new SteamworksClientManager();
				if (!SteamworksClientManager.instance.disposed)
				{
					Action onLoaded = SteamworksClientManager._onLoaded;
					if (onLoaded != null)
					{
						onLoaded();
					}
					SteamworksClientManager._onLoaded = null;
					return true;
				}
				return false;
			};
			RoR2Application.unloadSteamworksClient = delegate()
			{
				SteamworksClientManager instance = SteamworksClientManager.instance;
				if (instance != null)
				{
					instance.Dispose();
				}
				SteamworksClientManager.instance = null;
			};
		}

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06003D1B RID: 15643 RVA: 0x000FC678 File Offset: 0x000FA878
		// (remove) Token: 0x06003D1C RID: 15644 RVA: 0x000FC6AC File Offset: 0x000FA8AC
		private static event Action _onLoaded;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06003D1D RID: 15645 RVA: 0x000FC6DF File Offset: 0x000FA8DF
		// (remove) Token: 0x06003D1E RID: 15646 RVA: 0x000FC701 File Offset: 0x000FA901
		public static event Action onLoaded
		{
			add
			{
				if (SteamworksClientManager.instance == null)
				{
					SteamworksClientManager._onLoaded += value;
					return;
				}
				if (!SteamworksClientManager.instance.disposed)
				{
					value();
				}
			}
			remove
			{
				SteamworksClientManager._onLoaded -= value;
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000FC70C File Offset: 0x000FA90C
		[ConCommand(commandName = "steamworks_client_print_p2p_connection_status", flags = ConVarFlags.None, helpText = "Prints debug information for any established P2P connection to the specified Steam ID.")]
		private static void CCSteamworksClientPrintP2PConnectionStatus(ConCommandArgs args)
		{
			Networking.P2PSessionState p2PSessionState = default(Networking.P2PSessionState);
			if (Client.Instance.Networking.GetP2PSessionState(args.GetArgSteamID(0).steamValue, ref p2PSessionState))
			{
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				stringBuilder.Append("BytesQueuedForSend").Append("=").Append(p2PSessionState.BytesQueuedForSend).AppendLine();
				stringBuilder.Append("Connecting").Append("=").Append(p2PSessionState.Connecting).AppendLine();
				stringBuilder.Append("ConnectionActive").Append("=").Append(p2PSessionState.ConnectionActive).AppendLine();
				stringBuilder.Append("PacketsQueuedForSend").Append("=").Append(p2PSessionState.PacketsQueuedForSend).AppendLine();
				stringBuilder.Append("P2PSessionError").Append("=").Append(p2PSessionState.P2PSessionError).AppendLine();
				stringBuilder.Append("UsingRelay").Append("=").Append(p2PSessionState.UsingRelay).AppendLine();
				Debug.Log(stringBuilder.ToString());
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				return;
			}
			Debug.Log("Failed to retrieve P2P info for the specified Steam ID.");
		}

		// Token: 0x04003C41 RID: 15425
		private bool disposed;
	}
}
