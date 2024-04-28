using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Facepunch.Steamworks;
using HG;
using JetBrains.Annotations;
using Rewired;
using RoR2.ContentManagement;
using RoR2.ConVar;
using RoR2.Modding;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Zio.FileSystems;

namespace RoR2
{
	// Token: 0x0200084D RID: 2125
	public class RoR2Application : MonoBehaviour
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000C5A16 File Offset: 0x000C3C16
		public bool IsFullyLoaded
		{
			get
			{
				return this.loaded;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000C5A1E File Offset: 0x000C3C1E
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000C5A26 File Offset: 0x000C3C26
		public Client steamworksClient { get; private set; }

		// Token: 0x06002E60 RID: 11872 RVA: 0x000C5A2F File Offset: 0x000C3C2F
		private static void AssignBuildId()
		{
			RoR2Application.buildId = Application.version;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000C5A3B File Offset: 0x000C3C3B
		public static string GetBuildId()
		{
			return RoR2Application.buildId;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000C5A42 File Offset: 0x000C3C42
		// (set) Token: 0x06002E63 RID: 11875 RVA: 0x000C5A49 File Offset: 0x000C3C49
		public static RoR2Application instance { get; private set; }

		// Token: 0x06002E64 RID: 11876 RVA: 0x000C5A54 File Offset: 0x000C3C54
		private void Awake()
		{
			if (RoR2Application.maxPlayers != 4 || (Application.genuineCheckAvailable && !Application.genuine))
			{
				RoR2Application.isModded = true;
			}
			this.stopwatch.Start();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			if (RoR2Application.instance)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			RoR2Application.instance = this;
			RoR2Application.AssignBuildId();
			Debug.Log("buildId = " + RoR2Application.buildId);
			if (!RoR2Application.loadStarted)
			{
				RoR2Application.loadStarted = true;
				base.StartCoroutine(this.OnLoad());
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000C5AE4 File Offset: 0x000C3CE4
		private void Start()
		{
			if (RoR2Application.instance == this && RoR2Application.onStart != null)
			{
				RoR2Application.onStart();
				RoR2Application.onStart = null;
			}
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000C5B0C File Offset: 0x000C3D0C
		private void Update()
		{
			if (RoR2Application.waitMsConVar.value >= 0)
			{
				Thread.Sleep(RoR2Application.waitMsConVar.value);
			}
			if (!Application.isBatchMode && MPEventSystemManager.kbmEventSystem)
			{
				Cursor.lockState = ((MPEventSystemManager.kbmEventSystem.isCursorVisible || MPEventSystemManager.combinedEventSystem.isCursorVisible) ? CursorLockMode.Confined : CursorLockMode.Locked);
				Cursor.visible = false;
			}
			Action action = RoR2Application.onUpdate;
			if (action != null)
			{
				action();
			}
			Action action2 = Interlocked.Exchange<Action>(ref RoR2Application.onNextUpdate, null);
			if (action2 != null)
			{
				action2();
			}
			RoR2Application.timeTimers.Update(Time.deltaTime);
			RoR2Application.unscaledTimeTimers.Update(Time.unscaledDeltaTime);
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06002E67 RID: 11879 RVA: 0x000C5BB4 File Offset: 0x000C3DB4
		// (remove) Token: 0x06002E68 RID: 11880 RVA: 0x000C5BE8 File Offset: 0x000C3DE8
		public static event Action onUpdate;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06002E69 RID: 11881 RVA: 0x000C5C1C File Offset: 0x000C3E1C
		// (remove) Token: 0x06002E6A RID: 11882 RVA: 0x000C5C50 File Offset: 0x000C3E50
		public static event Action onFixedUpdate;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002E6B RID: 11883 RVA: 0x000C5C84 File Offset: 0x000C3E84
		// (remove) Token: 0x06002E6C RID: 11884 RVA: 0x000C5CB8 File Offset: 0x000C3EB8
		public static event Action onLateUpdate;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06002E6D RID: 11885 RVA: 0x000C5CEC File Offset: 0x000C3EEC
		// (remove) Token: 0x06002E6E RID: 11886 RVA: 0x000C5D20 File Offset: 0x000C3F20
		public static event Action onNextUpdate;

		// Token: 0x06002E6F RID: 11887 RVA: 0x000C5D53 File Offset: 0x000C3F53
		private void FixedUpdate()
		{
			Action action = RoR2Application.onFixedUpdate;
			if (action != null)
			{
				action();
			}
			RoR2Application.fixedTimeTimers.Update(Time.fixedDeltaTime);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000C5D74 File Offset: 0x000C3F74
		private void LateUpdate()
		{
			Action action = RoR2Application.onLateUpdate;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000C5D85 File Offset: 0x000C3F85
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000C5D8C File Offset: 0x000C3F8C
		public static FileSystem fileSystem { get; private set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000C5D94 File Offset: 0x000C3F94
		// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000C5D9B File Offset: 0x000C3F9B
		public static bool loadFinished { get; private set; } = false;

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000C5DA3 File Offset: 0x000C3FA3
		// (set) Token: 0x06002E76 RID: 11894 RVA: 0x000C5DAA File Offset: 0x000C3FAA
		public static bool loadStarted { get; private set; } = false;

		// Token: 0x06002E77 RID: 11895 RVA: 0x000C5DB2 File Offset: 0x000C3FB2
		private IEnumerator OnLoad()
		{
			return this.InitializeGameRoutine();
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000C5DBA File Offset: 0x000C3FBA
		private IEnumerator InitializeGameRoutine()
		{
			RoR2Application.<>c__DisplayClass69_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			RoR2Application.loadStarted = true;
			UnitySystemConsoleRedirector.Redirect();
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode loadSceneMode)
			{
				Debug.LogFormat("Loaded scene {0} loadSceneMode={1}", new object[]
				{
					scene.name,
					loadSceneMode
				});
			};
			SceneManager.sceneUnloaded += delegate(Scene scene)
			{
				Debug.LogFormat("Unloaded scene {0}", new object[]
				{
					scene.name
				});
			};
			SceneManager.activeSceneChanged += delegate(Scene oldScene, Scene newScene)
			{
				Debug.LogFormat("Active scene changed from {0} to {1}", new object[]
				{
					oldScene.name,
					newScene.name
				});
			};
			WwiseIntegrationManager.Init();
			while (SceneManager.GetActiveScene().name != "loadingbasic")
			{
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
			PhysicalFileSystem physicalFileSystem = new PhysicalFileSystem();
			FileSystem fileSystem = new SubFileSystem(physicalFileSystem, physicalFileSystem.ConvertPathFromInternal(Application.dataPath), true);
			Debug.Log("application data path is" + Application.dataPath);
			RoR2Application.fileSystem = fileSystem;
			RoR2Application.cloudStorage = RoR2Application.fileSystem;
			Func<bool> func = RoR2Application.loadSteamworksClient;
			if (func == null || !func())
			{
				Application.Quit();
				yield break;
			}
			RewiredIntegrationManager.Init();
			UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/MPEventSystemManager"));
			PlatformSystems.InitNetworkManagerSystem(UnityEngine.Object.Instantiate<GameObject>(this.networkManagerPrefab));
			if (this.platformManagerPrefab != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.platformManagerPrefab);
			}
			PlatformSystems.InitPlatformManagerObject(base.gameObject);
			if (PlatformSystems.textDataManager != null)
			{
				while (!PlatformSystems.textDataManager.InitializedConfigFiles)
				{
					Debug.Log("Text config stuff still happening...");
					yield return null;
				}
			}
			GameObject gameObject = new GameObject("Console");
			gameObject.AddComponent<SetDontDestroyOnLoad>();
			gameObject.AddComponent<Console>();
			yield return this.LoadGameContent();
			yield return new WaitForEndOfFrame();
			Language.collectLanguageRootFolders += delegate(List<string> list)
			{
				list.Add(Path.Combine(Application.streamingAssetsPath, "Language"));
			};
			Language.Init();
			SystemInitializerAttribute.Execute();
			LocalUserManager.Init();
			PlatformSystems.saveSystem.LoadUserProfiles();
			if (RoR2Application.onLoad != null)
			{
				RoR2Application.onLoad();
				RoR2Application.onLoad = null;
			}
			CS$<>8__locals1.hasStartupError = false;
			try
			{
				AkSoundEngine.IsInitialized();
			}
			catch (DllNotFoundException)
			{
				this.<InitializeGameRoutine>g__IssueStartupError|69_4(new SimpleDialogBox.TokenParamsPair("STARTUP_FAILURE_DIALOG_TITLE", Array.Empty<object>()), new SimpleDialogBox.TokenParamsPair("MSVCR_2015_BAD_INSTALL_DIALOG_BODY", Array.Empty<object>()), new ValueTuple<string, Action>[]
				{
					new ValueTuple<string, Action>("MSVCR_2015_BAD_INSTALL_DIALOG_DOWNLOAD_BUTTON", new Action(RoR2Application.<>c.<>9.<InitializeGameRoutine>g__OpenVCR2015DownloadPage|69_5))
				}, ref CS$<>8__locals1);
			}
			if (!CS$<>8__locals1.hasStartupError)
			{
				while (Console.instance == null)
				{
					Debug.Log("Console not initialized yet...");
					yield return null;
				}
				PlatformSystems.networkManager.ServerChangeScene("splash");
			}
			while (SceneManager.GetActiveScene().name == "loadingbasic")
			{
				yield return new WaitForEndOfFrame();
			}
			RoR2Application.loadStarted = false;
			RoR2Application.loadFinished = true;
			yield break;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000C5DC9 File Offset: 0x000C3FC9
		private IEnumerator LoadGameContent()
		{
			TMP_Text loadingPercentIndicatorLabel = (from label in UnityEngine.Object.FindObjectsOfType<TMP_Text>()
			where label.name.Equals("LoadingPercentIndicator", StringComparison.Ordinal)
			select label).FirstOrDefault<TMP_Text>();
			Animation loadingPercentAnimation = loadingPercentIndicatorLabel ? loadingPercentIndicatorLabel.GetComponentInChildren<Animation>() : null;
			StringBuilder loadingTextStringBuilder = new StringBuilder();
			TimeSpan maxLoadTimePerFrame = TimeSpan.FromSeconds(0.008333333333333333);
			ContentManager.collectContentPackProviders += delegate(ContentManager.AddContentPackProviderDelegate addContentPackProvider)
			{
				addContentPackProvider(new RoR2Content());
				addContentPackProvider(new JunkContent());
				addContentPackProvider(new DLC1Content());
				foreach (IContentPackProvider contentPackProvider2 in from mod in ModLoader.instance.allEnabledModsReadOnly
				select mod.GetGameData<RoR2Mod>().contentPackProvider into contentPackProvider
				where contentPackProvider != null
				select contentPackProvider)
				{
					addContentPackProvider(contentPackProvider2);
				}
			};
			ReadableProgress<float> contentLoadProgressReceiver = new ReadableProgress<float>();
			IEnumerator item = ContentManager.LoadContentPacks(contentLoadProgressReceiver);
			Stopwatch totalLoadStopwatch = new Stopwatch();
			totalLoadStopwatch.Start();
			Stopwatch thisFrameLoadStopwatch = new Stopwatch();
			int previousProgressPercent = 0;
			Stack<IEnumerator> coroutineStack = new Stack<IEnumerator>();
			coroutineStack.Push(item);
			while (coroutineStack.Count > 0)
			{
				thisFrameLoadStopwatch.Restart();
				int num = 0;
				do
				{
					num++;
					IEnumerator enumerator = coroutineStack.Peek();
					IEnumerator item2;
					if ((item2 = (enumerator.Current as IEnumerator)) != null)
					{
						coroutineStack.Push(item2);
					}
					else
					{
						while (!enumerator.MoveNext())
						{
							coroutineStack.Pop();
							if (coroutineStack.Count == 0)
							{
								break;
							}
							enumerator = coroutineStack.Peek();
						}
					}
				}
				while (coroutineStack.Count > 0 && thisFrameLoadStopwatch.Elapsed < maxLoadTimePerFrame);
				thisFrameLoadStopwatch.Stop();
				int num2 = Mathf.FloorToInt(contentLoadProgressReceiver.value * 100f);
				if (previousProgressPercent != num2)
				{
					previousProgressPercent = num2;
					loadingTextStringBuilder.Clear();
					loadingTextStringBuilder.AppendInt(num2, 1U, uint.MaxValue);
					loadingTextStringBuilder.Append("%");
					if (loadingPercentIndicatorLabel)
					{
						loadingPercentIndicatorLabel.SetText(loadingTextStringBuilder);
					}
				}
				if (loadingPercentAnimation)
				{
					AnimationClip clip = loadingPercentAnimation.clip;
					clip.SampleAnimation(loadingPercentAnimation.gameObject, contentLoadProgressReceiver.value * 0.99f * clip.length);
				}
				yield return new WaitForEndOfFrame();
			}
			Console.instance.SubmitCmd(null, IntroCutsceneController.shouldSkip ? "set_scene title" : "set_scene intro", false);
			yield return null;
			Debug.LogFormat("Game content load completed in {0}ms.", new object[]
			{
				totalLoadStopwatch.ElapsedMilliseconds
			});
			yield break;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000C5DD1 File Offset: 0x000C3FD1
		private void OnDestroy()
		{
			if (RoR2Application.instance == this && PlatformSystems.EgsToggleConVar.value != 1)
			{
				Action action = RoR2Application.unloadSteamworksClient;
				if (action == null)
				{
					return;
				}
				action();
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		private void OnApplicationQuit()
		{
			Action action = RoR2Application.onShutDown;
			if (action != null)
			{
				action();
			}
			if (Console.instance)
			{
				Console.instance.SaveArchiveConVars();
			}
			Action action2 = RoR2Application.unloadSteamworksClient;
			if (action2 != null)
			{
				action2();
			}
			UnitySystemConsoleRedirector.Disengage();
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000C5E39 File Offset: 0x000C4039
		public static bool isInSinglePlayer
		{
			get
			{
				return NetworkServer.active && NetworkServer.dontListen && LocalUserManager.readOnlyLocalUsersList.Count == 1;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000C5E58 File Offset: 0x000C4058
		public static bool isInMultiPlayer
		{
			get
			{
				return NetworkClient.active && (!NetworkServer.active || !NetworkServer.dontListen || LocalUserManager.readOnlyLocalUsersList.Count != 1);
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000C5E83 File Offset: 0x000C4083
		[ConCommand(commandName = "quit", flags = ConVarFlags.None, helpText = "Close the application.")]
		private static void CCQuit(ConCommandArgs args)
		{
			Application.Quit();
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000C5E8C File Offset: 0x000C408C
		[NotNull]
		public static string GetBestUserName()
		{
			string text = null;
			string text2;
			if ((text2 = text) == null)
			{
				Client instance = Client.Instance;
				text2 = ((instance != null) ? instance.Username : null);
			}
			text = text2;
			if (LocalUserManager.readOnlyLocalUsersList.Count > 0)
			{
				text = (text ?? LocalUserManager.readOnlyLocalUsersList[0].userProfile.name);
			}
			return text ?? "???";
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000C5EE4 File Offset: 0x000C40E4
		[ConCommand(commandName = "app_info", flags = ConVarFlags.None, helpText = "Get information about the application, including build and version info.")]
		private static void CCAppInfo(ConCommandArgs args)
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			stringBuilder.Append("====================").AppendLine();
			stringBuilder.Append("= Application Info =").AppendLine();
			stringBuilder.Append("====================").AppendLine();
			stringBuilder.Append("> Product: ").AppendLine();
			stringBuilder.Append(">   Product Name: ").Append(Application.productName).AppendLine();
			stringBuilder.Append(">   Company Name: ").Append(Application.companyName).AppendLine();
			stringBuilder.Append("> Build: ").AppendLine();
			stringBuilder.Append(">   Platform: ").Append(Application.platform).AppendLine();
			stringBuilder.Append(">   Version: ").Append(Application.version).AppendLine();
			stringBuilder.Append(">   Unity Version: ").Append(Application.unityVersion).AppendLine();
			stringBuilder.Append(">   Build GUID: ").Append(Application.buildGUID).AppendLine();
			stringBuilder.Append(">   Build Type: ").Append("Production").AppendLine();
			stringBuilder.Append(">   Is Dedicated Server: ").Append(false).AppendLine();
			stringBuilder.Append("> Environment: ").AppendLine();
			stringBuilder.Append(">   Command Line: ").Append(Environment.CommandLine).AppendLine();
			stringBuilder.Append(">   Console Log Path: ").Append(Application.consoleLogPath).AppendLine();
			Client instance = Client.Instance;
			if (instance != null && instance.IsValid)
			{
				stringBuilder.Append("> Steamworks Client: ").AppendLine();
				stringBuilder.Append(">   App ID: ").AppendUint(instance.AppId, 1U, uint.MaxValue).AppendLine();
				stringBuilder.Append(">   Build ID: ").AppendInt(instance.BuildId, 1U, uint.MaxValue).AppendLine();
				stringBuilder.Append(">   Branch: ").Append(instance.BetaName).AppendLine();
			}
			Server instance2 = Server.Instance;
			if (instance2 != null && instance2.IsValid)
			{
				stringBuilder.Append("> Steamworks Game Server: ").AppendLine();
				stringBuilder.Append(">   App ID: ").AppendUint(instance2.AppId, 1U, uint.MaxValue).AppendLine();
			}
			args.Log(stringBuilder.ToString());
			stringBuilder = HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000C613C File Offset: 0x000C433C
		public static void LoadRewiredUIMap()
		{
			foreach (Player player in ReInput.players.Players)
			{
				player.controllers.maps.ClearAllMaps(false);
				foreach (Controller controller in player.controllers.Controllers)
				{
					try
					{
						player.controllers.maps.LoadMap(controller.type, controller.id, 2, 0);
					}
					catch (FormatException exception)
					{
						Debug.LogWarning(string.Format("Excepting loading controller mapping (type:{0},id:{1}) for player (name:{2},id:{3}).", new object[]
						{
							controller.type,
							controller.id,
							player.name,
							player.id
						}));
						Debug.LogException(exception);
					}
				}
				player.controllers.maps.SetAllMapsEnabled(true);
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000C626C File Offset: 0x000C446C
		public static void DebugPrintRewired()
		{
			Debug.Log("---Rewired Start---");
			Debug.Log("Rewired found " + ReInput.controllers.joystickCount + " joysticks attached.");
			for (int i = 0; i < ReInput.controllers.joystickCount; i++)
			{
				Joystick joystick = ReInput.controllers.Joysticks[i];
				Debug.Log(string.Concat(new object[]
				{
					"[",
					i,
					"] Joystick: ",
					joystick.name,
					"\nHardware Name: ",
					joystick.hardwareName,
					"\nIs Recognized: ",
					(joystick.hardwareTypeGuid != Guid.Empty) ? "Yes" : "No",
					"\nIs Assigned: ",
					ReInput.controllers.IsControllerAssigned(joystick.type, joystick) ? "Yes" : "No"
				}));
			}
			foreach (Player player in ReInput.players.Players)
			{
				Debug.Log(string.Concat(new object[]
				{
					"PlayerId = ",
					player.id,
					" is assigned ",
					player.controllers.joystickCount,
					" joysticks."
				}));
				foreach (Joystick joystick2 in player.controllers.Joysticks)
				{
					Debug.Log("Joystick: " + joystick2.name + "\nIs Recognized: " + ((joystick2.hardwareTypeGuid != Guid.Empty) ? "Yes" : "No"));
					foreach (ControllerMap controllerMap in player.controllers.maps.GetMaps(joystick2.type, joystick2.id))
					{
						string name = ReInput.mapping.GetMapCategory(controllerMap.categoryId).name;
						string name2 = ReInput.mapping.GetJoystickLayout(controllerMap.layoutId).name;
						Debug.Log(string.Concat(new string[]
						{
							"Controller Map:\nCategory = ",
							name,
							"\nLayout = ",
							name2,
							"\nenabled = ",
							controllerMap.enabled.ToString()
						}));
						foreach (ActionElementMap actionElementMap in controllerMap.GetElementMaps())
						{
							InputAction action = ReInput.mapping.GetAction(actionElementMap.actionId);
							if (action != null)
							{
								Debug.Log(string.Concat(new string[]
								{
									"Action \"",
									action.name,
									"\" is bound to \"",
									actionElementMap.elementIdentifierName,
									"\""
								}));
							}
						}
					}
				}
			}
			Debug.Log("---Rewired End---");
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000C65E8 File Offset: 0x000C47E8
		private static void AssignJoystickToAvailablePlayer(Controller controller)
		{
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				Player player = players[i];
				if (player.name != "PlayerMain" && player.controllers.joystickCount == 0 && !player.controllers.hasKeyboard && !player.controllers.hasMouse)
				{
					player.controllers.AddController(controller, false);
					return;
				}
			}
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000C6660 File Offset: 0x000C4860
		private static void AssignNewController(ControllerStatusChangedEventArgs args)
		{
			RoR2Application.AssignNewController(ReInput.controllers.GetController(args.controllerType, args.controllerId));
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000C667D File Offset: 0x000C487D
		public static void ClearControllers()
		{
			ReInput.players.GetPlayer("PlayerMain").controllers.ClearAllControllers();
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000C6698 File Offset: 0x000C4898
		private static void AssignNewController(Controller controller)
		{
			ReInput.players.GetPlayer("PlayerMain").controllers.AddController(controller, false);
			if (controller.type == ControllerType.Joystick)
			{
				RoR2Application.AssignJoystickToAvailablePlayer(controller);
			}
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000C6770 File Offset: 0x000C4970
		[CompilerGenerated]
		private void <InitializeGameRoutine>g__IssueStartupError|69_4(SimpleDialogBox.TokenParamsPair headerToken, SimpleDialogBox.TokenParamsPair descriptionToken, [TupleElementNames(new string[]
		{
			"token",
			"action"
		})] ValueTuple<string, Action>[] buttons, ref RoR2Application.<>c__DisplayClass69_0 A_4)
		{
			if (A_4.hasStartupError)
			{
				return;
			}
			A_4.hasStartupError = true;
			LocalUserManager.ClearUsers();
			NetworkManager.singleton.ServerChangeScene("title");
			SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
			simpleDialogBox.headerToken = headerToken;
			simpleDialogBox.descriptionToken = descriptionToken;
			simpleDialogBox.rootObject.transform.SetParent(this.mainCanvas.transform);
			OnDestroyCallback.AddCallback(simpleDialogBox.rootObject, delegate(OnDestroyCallback onDestroyCallbackComponent)
			{
				Console.instance.SubmitCmd(null, "quit", false);
			});
			if (buttons != null)
			{
				foreach (ValueTuple<string, Action> valueTuple in buttons)
				{
					simpleDialogBox.AddActionButton(new UnityAction(valueTuple.Item2.Invoke), valueTuple.Item1, true, Array.Empty<object>());
				}
			}
			simpleDialogBox.AddCancelButton("PAUSE_QUIT_TO_DESKTOP", Array.Empty<object>());
		}

		// Token: 0x0400306A RID: 12394
		[SerializeField]
		[HideInInspector]
		private bool loaded;

		// Token: 0x0400306B RID: 12395
		public static readonly string messageForModders = "We don't officially support modding at this time but if you're going to mod the game please change this value to true if you're modding the game. This will disable some things like Prismatic Trials and put players into a separate matchmaking queue from vanilla users to protect their game experience.";

		// Token: 0x0400306C RID: 12396
		public static bool isModded = false;

		// Token: 0x0400306D RID: 12397
		public GameObject networkManagerPrefab;

		// Token: 0x0400306E RID: 12398
		public GameObject platformManagerPrefab;

		// Token: 0x0400306F RID: 12399
		public GameObject networkSessionPrefab;

		// Token: 0x04003070 RID: 12400
		public PostProcessVolume postProcessSettingsController;

		// Token: 0x04003071 RID: 12401
		public Canvas mainCanvas;

		// Token: 0x04003072 RID: 12402
		public Stopwatch stopwatch = new Stopwatch();

		// Token: 0x04003073 RID: 12403
		public const string gameName = "Risk of Rain 2";

		// Token: 0x04003074 RID: 12404
		private const uint ror1AppId = 248820U;

		// Token: 0x04003075 RID: 12405
		public const uint ror2AppId = 632360U;

		// Token: 0x04003076 RID: 12406
		private const uint ror2DedicatedServerAppId = 1180760U;

		// Token: 0x04003077 RID: 12407
		public const bool isDedicatedServer = false;

		// Token: 0x04003078 RID: 12408
		public const uint appId = 632360U;

		// Token: 0x0400307A RID: 12410
		public static string steamBuildId = "STEAM_UNINITIALIZED";

		// Token: 0x0400307B RID: 12411
		private static string buildId;

		// Token: 0x0400307C RID: 12412
		public static readonly int hardMaxPlayers = 16;

		// Token: 0x0400307D RID: 12413
		public static readonly int maxPlayers = 4;

		// Token: 0x0400307E RID: 12414
		public static readonly int maxLocalPlayers = 4;

		// Token: 0x04003080 RID: 12416
		private static IntConVar waitMsConVar = new IntConVar("wait_ms", ConVarFlags.None, "-1", "How many milliseconds to sleep between each frame. -1 for no sleeping between frames.");

		// Token: 0x04003081 RID: 12417
		public static readonly TimerQueue timeTimers = new TimerQueue();

		// Token: 0x04003082 RID: 12418
		public static readonly TimerQueue fixedTimeTimers = new TimerQueue();

		// Token: 0x04003083 RID: 12419
		public static readonly TimerQueue unscaledTimeTimers = new TimerQueue();

		// Token: 0x04003089 RID: 12425
		public static FileSystem cloudStorage;

		// Token: 0x0400308A RID: 12426
		public static Func<bool> loadSteamworksClient;

		// Token: 0x0400308B RID: 12427
		public static Action unloadSteamworksClient;

		// Token: 0x0400308E RID: 12430
		public static Action onLoad;

		// Token: 0x0400308F RID: 12431
		public static Action onStart;

		// Token: 0x04003090 RID: 12432
		public static Action onShutDown;

		// Token: 0x04003091 RID: 12433
		public static readonly Xoroshiro128Plus rng = new Xoroshiro128Plus((ulong)DateTime.UtcNow.Ticks);
	}
}
