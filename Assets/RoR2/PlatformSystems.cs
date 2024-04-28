using System;
using System.Linq;
using RoR2.ConVar;
using RoR2.EntitlementManagement;
using RoR2.Networking;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009BE RID: 2494
	public static class PlatformSystems
	{
		// Token: 0x0600390A RID: 14602 RVA: 0x000EE438 File Offset: 0x000EC638
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void Init()
		{
			SteamworksClientManager.Init();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs.Contains("--disableCrossplay"))
			{
				PlatformSystems.EgsToggleConVar.value = 0;
			}
			else if (commandLineArgs.Contains("--enableCrossplay"))
			{
				PlatformSystems.EgsToggleConVar.value = 1;
			}
			if (PlatformSystems.EgsToggleConVar.value == 1)
			{
				try
				{
					PlatformSystems.crossPlayEnabledOnStartup = true;
					PlatformSystems.platformManager = new EOSPlatformManager();
					PlatformSystems.userManager = new UserManagerEOS();
					(PlatformSystems.userManager as UserManagerEOS).InitializeUserManager();
					PlatformSystems.textDataManager = new StreamingAssetsTextDataManager();
					PlatformSystems.lobbyManager = new EOSLobbyManager();
					(PlatformSystems.lobbyManager as EOSLobbyManager).Init();
					new EOSLoginManager().TryLogin();
					goto IL_D3;
				}
				catch
				{
					PlatformSystems.EgsToggleConVar.value = 0;
					Application.Quit();
					goto IL_D3;
				}
			}
			PlatformSystems.lobbyManager = new SteamworksLobbyManager();
			PlatformSystems.userManager = new SteamUserManager();
			PlatformSystems.textDataManager = new StreamingAssetsTextDataManager();
			IL_D3:
			SteamworksRichPresenceManager.Init();
			PlatformSystems.saveSystem = new SaveSystemSteam();
			PlatformSystems.achievementSystem = new AchievementSystemSteam();
			PlatformSystems.entitlementsSystem = new SteamworksEntitlementResolver();
			PlatformManager platformManager = PlatformSystems.platformManager;
			if (platformManager != null)
			{
				platformManager.InitializePlatformManager();
			}
			UserProfile.GenerateSaveFieldFunctions();
			RoR2Application.onUpdate += PlatformSystems.saveSystem.StaticUpdate;
			if (PlatformSystems.entitlementsSystem != null)
			{
				EntitlementManager.collectLocalUserEntitlementResolvers += delegate(Action<IUserEntitlementResolver<LocalUser>> add)
				{
					add(PlatformSystems.entitlementsSystem);
				};
				EntitlementManager.collectNetworkUserEntitlementResolvers += delegate(Action<IUserEntitlementResolver<NetworkUser>> add)
				{
					add(PlatformSystems.entitlementsSystem);
				};
			}
			RoR2Application.onShutDown = (Action)Delegate.Combine(RoR2Application.onShutDown, new Action(PlatformSystems.Shutdown));
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000EE5E4 File Offset: 0x000EC7E4
		private static void Shutdown()
		{
			PlatformSystems.saveSystem.HandleShutDown();
			PlatformSystems.lobbyManager.Shutdown();
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000EE5FC File Offset: 0x000EC7FC
		private static T BuildMonoSingleton<T>() where T : MonoBehaviour
		{
			GameObject gameObject = new GameObject("T");
			T result = gameObject.AddComponent<T>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			return result;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000EE620 File Offset: 0x000EC820
		public static void InitNetworkManagerSystem(GameObject networkManagerPrefabObject)
		{
			Debug.Log("PlatformSystems:InitNetworkManagerSystem");
			if (networkManagerPrefabObject.GetComponent<NetworkManagerSystem>() != null)
			{
				return;
			}
			if (PlatformSystems.EgsToggleConVar.value == 1)
			{
				PlatformSystems.networkManager = networkManagerPrefabObject.AddComponent<NetworkManagerSystemEOS>();
			}
			else
			{
				PlatformSystems.networkManager = networkManagerPrefabObject.AddComponent<NetworkManagerSystemSteam>();
			}
			NetworkManagerConfiguration component = networkManagerPrefabObject.GetComponent<NetworkManagerConfiguration>();
			if (component == null || PlatformSystems.networkManager == null)
			{
				Debug.LogError("Missing NetworkManagerConfiguration on NetworkManagerPrefab or platform NetworkManagerSystem not found");
				return;
			}
			PlatformSystems.networkManager.Init(component);
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000026ED File Offset: 0x000008ED
		public static void InitPlatformManagerObject(GameObject platformManagerPrefabObject)
		{
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000EE69E File Offset: 0x000EC89E
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x000026ED File Offset: 0x000008ED
		public static bool ShouldUseEpicOnlineSystems
		{
			get
			{
				return PlatformSystems.crossPlayEnabledOnStartup;
			}
			set
			{
			}
		}

		// Token: 0x040038BF RID: 14527
		public static SaveSystem saveSystem;

		// Token: 0x040038C0 RID: 14528
		public static UserManager userManager;

		// Token: 0x040038C1 RID: 14529
		public static AchievementSystem achievementSystem;

		// Token: 0x040038C2 RID: 14530
		public static LobbyManager lobbyManager;

		// Token: 0x040038C3 RID: 14531
		public static TextDataManager textDataManager;

		// Token: 0x040038C4 RID: 14532
		public static PlatformManager platformManager;

		// Token: 0x040038C5 RID: 14533
		public static NetworkManagerSystem networkManager;

		// Token: 0x040038C6 RID: 14534
		public static IUserEntitementsResolverNetworkAndLocal entitlementsSystem;

		// Token: 0x040038C7 RID: 14535
		public static object statManager;

		// Token: 0x040038C8 RID: 14536
		public static object friendsManager;

		// Token: 0x040038C9 RID: 14537
		public static bool crossPlayEnabledOnStartup = false;

		// Token: 0x040038CA RID: 14538
		public static PlayerPrefsIntConVar EgsToggleConVar = new PlayerPrefsIntConVar("egsToggle", ConVarFlags.Engine, "0", "If EGS is used. If false, use Steam.");
	}
}
