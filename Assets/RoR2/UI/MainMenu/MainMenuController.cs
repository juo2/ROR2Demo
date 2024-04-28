using System;
using System.Runtime.CompilerServices;
using RoR2.ConVar;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DC7 RID: 3527
	public sealed class MainMenuController : MonoBehaviour
	{
		// Token: 0x060050AB RID: 20651 RVA: 0x0014D718 File Offset: 0x0014B918
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			NetworkManagerSystem.onStartClientGlobal += delegate(NetworkClient client)
			{
				if (!NetworkServer.active || !NetworkServer.dontListen)
				{
					MainMenuController.wasInMultiplayer = true;
				}
			};
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x0014D73E File Offset: 0x0014B93E
		// (set) Token: 0x060050AD RID: 20653 RVA: 0x0014D745 File Offset: 0x0014B945
		public static bool IsOnMultiplayerScreen { get; set; } = false;

		// Token: 0x060050AE RID: 20654 RVA: 0x0014D74D File Offset: 0x0014B94D
		private void Awake()
		{
			RoR2Application.onStart = (Action)Delegate.Combine(RoR2Application.onStart, new Action(this.StartManaged));
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0014D770 File Offset: 0x0014B970
		private void Start()
		{
			if (MainMenuController.instance != null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			MainMenuController.instance = this;
			this.StartWaitForLoad();
			MainMenuController.wasInMultiplayer = false;
			if (this.LoadingScreen)
			{
				this.LoadingScreen.gameObject.SetActive(true);
			}
			if (this.EngagementScreen)
			{
				this.EngagementScreen.gameObject.SetActive(false);
			}
			this.titleMenuScreen.gameObject.SetActive(false);
			this.multiplayerMenuScreen.gameObject.SetActive(false);
			this.settingsMenuScreen.gameObject.SetActive(false);
			this.moreMenuScreen.gameObject.SetActive(false);
			this.extraGameModeMenuScreen.gameObject.SetActive(false);
			this.desiredMenuScreen = (MainMenuController.wasInMultiplayer ? this.multiplayerMenuScreen : this.titleMenuScreen);
			this.StartManaged();
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0014D85C File Offset: 0x0014BA5C
		private void StartManaged()
		{
			this.isInitialized = true;
			this.CheckWarningScreen();
			this.currentMenuScreen = this.desiredMenuScreen;
			if (!this.currentMenuScreen.gameObject.activeInHierarchy)
			{
				this.currentMenuScreen.gameObject.SetActive(true);
			}
			if (this.currentMenuScreen.desiredCameraTransform != null)
			{
				this.cameraTransform.rotation = this.currentMenuScreen.desiredCameraTransform.rotation;
			}
			if (this.currentMenuScreen)
			{
				this.currentMenuScreen.OnEnter(this);
			}
			MainMenuController.IsOnMultiplayerScreen = (this.currentMenuScreen == this.multiplayerMenuScreen);
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private bool CheckWarningScreen()
		{
			return false;
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0014D903 File Offset: 0x0014BB03
		private static bool IsMainUserSignedIn()
		{
			return LocalUserManager.FindLocalUser(0) != null;
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0013E6A8 File Offset: 0x0013C8A8
		private bool IsInLobby()
		{
			return PlatformSystems.lobbyManager.isInLobby;
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0014D910 File Offset: 0x0014BB10
		private void Update()
		{
			if (!this.isInitialized)
			{
				return;
			}
			if (this.currentMenuScreen == this.multiplayerMenuScreen && PlayerPrefs.GetInt("HasShownFirstTimePopup", 0) == 0)
			{
				PlayerPrefs.SetInt("HasShownFirstTimePopup", 1);
				this.ShowFirstTimeCrossPlayPopup();
			}
			if (this.IsInLobby() && this.currentMenuScreen != this.multiplayerMenuScreen)
			{
				this.desiredMenuScreen = this.multiplayerMenuScreen;
			}
			if (!MainMenuController.IsMainUserSignedIn() && this.currentMenuScreen != this.EAwarningProfileMenu && this.currentMenuScreen != this.EngagementScreen)
			{
				this.desiredMenuScreen = this.profileMenuScreen;
			}
			if (this.currentMenuScreen == null)
			{
				return;
			}
			this.UpdateMenuTransition();
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0014D9CC File Offset: 0x0014BBCC
		private void UpdateMenuTransition()
		{
			if (this.desiredMenuScreen != this.currentMenuScreen)
			{
				this.currentMenuScreen.shouldDisplay = false;
				if (this.currentMenuScreen.IsReadyToLeave())
				{
					MPEventSystemLocator component = base.GetComponent<MPEventSystemLocator>();
					if (component)
					{
						component.eventSystem.SetSelectedObject(null);
					}
					this.currentMenuScreen.OnExit(this);
					this.currentMenuScreen.gameObject.SetActive(false);
					this.currentMenuScreen = this.desiredMenuScreen;
					this.camTransitionTimer = this.camTransitionDuration;
					this.currentMenuScreen.OnEnter(this);
					MainMenuController.IsOnMultiplayerScreen = (this.currentMenuScreen == this.multiplayerMenuScreen);
					return;
				}
			}
			else if (this.currentMenuScreen.desiredCameraTransform != null)
			{
				this.camTransitionTimer -= Time.deltaTime;
				this.cameraTransform.position = Vector3.SmoothDamp(this.cameraTransform.position, this.currentMenuScreen.desiredCameraTransform.position, ref this.camSmoothDampPositionVelocity, this.camTranslationSmoothDampTime);
				Vector3 eulerAngles = this.cameraTransform.eulerAngles;
				Vector3 eulerAngles2 = this.currentMenuScreen.desiredCameraTransform.eulerAngles;
				eulerAngles.x = Mathf.SmoothDampAngle(eulerAngles.x, eulerAngles2.x, ref this.camSmoothDampRotationVelocity.x, this.camRotationSmoothDampTime, float.PositiveInfinity, Time.unscaledDeltaTime);
				eulerAngles.y = Mathf.SmoothDampAngle(eulerAngles.y, eulerAngles2.y, ref this.camSmoothDampRotationVelocity.y, this.camRotationSmoothDampTime, float.PositiveInfinity, Time.unscaledDeltaTime);
				eulerAngles.z = Mathf.SmoothDampAngle(eulerAngles.z, eulerAngles2.z, ref this.camSmoothDampRotationVelocity.z, this.camRotationSmoothDampTime, float.PositiveInfinity, Time.unscaledDeltaTime);
				this.cameraTransform.eulerAngles = eulerAngles;
				if (this.camTransitionTimer <= 0f)
				{
					this.currentMenuScreen.gameObject.SetActive(true);
					this.currentMenuScreen.shouldDisplay = true;
				}
			}
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0014DBC9 File Offset: 0x0014BDC9
		public void SetDesiredMenuScreen(BaseMainMenuScreen newDesiredMenuScreen)
		{
			this.desiredMenuScreen = newDesiredMenuScreen;
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0014DBD2 File Offset: 0x0014BDD2
		public void ClearEngagementScreen()
		{
			if (!this.CheckWarningScreen())
			{
				this.desiredMenuScreen = this.titleMenuScreen;
			}
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0014DBE8 File Offset: 0x0014BDE8
		private void UpdateEoSLoginState()
		{
			if (MainMenuController.lastEOSLoginState != EOSLoginManager.loginState)
			{
				switch (EOSLoginManager.loginState)
				{
				case EOSLoginManager.EOSLoginState.None:
					Debug.Log("EOSLoginState: None");
					break;
				case EOSLoginManager.EOSLoginState.AttemptingLogin:
					Debug.Log("EOSLoginState: AttemptingLogin");
					break;
				case EOSLoginManager.EOSLoginState.AttemptingLink:
					Debug.Log("EOSLoginState: AttemptingLink");
					this.AddAccountLinkPopup();
					break;
				case EOSLoginManager.EOSLoginState.FailedLogin:
					Debug.Log("EOSLoginState: FailedLogin");
					this.AddAccountLinkPopup();
					break;
				case EOSLoginManager.EOSLoginState.FailedLink:
					Debug.Log("EOSLoginState: FailedLink");
					this.AddAccountLinkPopup();
					break;
				case EOSLoginManager.EOSLoginState.Success:
					Debug.Log("EOSLoginState: Success");
					if (this.titleMenuScreen.isActiveAndEnabled)
					{
						GameObject gameObject = this.mainMenuButtonPanel;
						if (gameObject != null)
						{
							gameObject.SetActive(true);
						}
					}
					break;
				}
				MainMenuController.lastEOSLoginState = EOSLoginManager.loginState;
			}
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0014DCAC File Offset: 0x0014BEAC
		private void AddAccountLinkPopup()
		{
			SimpleDialogBox dialogBox = SimpleDialogBox.Create(null);
			Action retryLoginFunction = delegate()
			{
				if (dialogBox)
				{
					this.RetryLogin();
				}
			};
			Action deactiveCrossplayAndRestartFunction = delegate()
			{
				if (dialogBox)
				{
					this.DeactivateCrossplayAndRestart();
				}
			};
			dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "EOS_NOT_LINKED_TITLE",
				formatParams = Array.Empty<object>()
			};
			dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "EOS_NOT_LINKED_MESSAGE",
				formatParams = Array.Empty<object>()
			};
			dialogBox.AddActionButton(delegate
			{
				retryLoginFunction();
			}, "EOS_RETRY_LOGIN", true, Array.Empty<object>());
			dialogBox.AddActionButton(delegate
			{
				deactiveCrossplayAndRestartFunction();
			}, "EOS_DEACTIVATE_CROSSPLAY_RESTART", true, Array.Empty<object>());
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0014DD94 File Offset: 0x0014BF94
		private void RetryLogin()
		{
			if (EOSLoginManager.loggedInAuthId == null)
			{
				new EOSLoginManager().TryLogin();
				return;
			}
			if (this.titleMenuScreen.isActiveAndEnabled)
			{
				GameObject gameObject = this.mainMenuButtonPanel;
				if (gameObject == null)
				{
					return;
				}
				gameObject.SetActive(true);
			}
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0014DDCC File Offset: 0x0014BFCC
		private void DeactivateCrossplayAndRestart()
		{
			BaseConVar baseConVar = Console.instance.FindConVar("egsToggle");
			if (baseConVar != null)
			{
				baseConVar.AttemptSetString("0");
			}
			Console.instance.SubmitCmd(null, "quit", false);
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0014DE00 File Offset: 0x0014C000
		private void ShowFirstTimeCrossPlayPopup()
		{
			SimpleDialogBox dialogBox = SimpleDialogBox.Create(null);
			Action activateCrossplayFunction = delegate()
			{
				if (dialogBox)
				{
					MainMenuController.<ShowFirstTimeCrossPlayPopup>g__ActivateCrossPlay|51_6();
				}
			};
			Action deactivateCrossplayFunction = delegate()
			{
				if (dialogBox)
				{
					MainMenuController.<ShowFirstTimeCrossPlayPopup>g__DeactivateCrossPlay|51_7();
				}
			};
			Action ShowCrossplayInfo = delegate()
			{
				if (dialogBox)
				{
					Application.OpenURL("https://support.gearboxsoftware.com/hc/en-us/articles/4440999200269");
				}
			};
			dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "FIRST_TIME_CROSSPLAY_POPUP_HEADER",
				formatParams = Array.Empty<object>()
			};
			dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "FIRST_TIME_CROSSPLAY_POPUP_DESCRIPTION",
				formatParams = Array.Empty<object>()
			};
			dialogBox.AddActionButton(delegate
			{
				activateCrossplayFunction();
			}, "STAT_CONTINUE", true, Array.Empty<object>());
			dialogBox.AddActionButton(delegate
			{
				ShowCrossplayInfo();
			}, "FIRST_TIME_CROSSPLAY_POPUP_INFO", false, Array.Empty<object>());
			dialogBox.AddActionButton(delegate
			{
				deactivateCrossplayFunction();
			}, "FIRST_TIME_CROSSPLAY_POPUP_DISABLE_CROSSPLAY", true, Array.Empty<object>());
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0014DF16 File Offset: 0x0014C116
		public void StartWaitForLoad()
		{
			MainMenuController.IsMainUserSignedIn();
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0014DF1E File Offset: 0x0014C11E
		public void EnableContinueEAWarningButton()
		{
			this.continueButtonTransition.interactable = true;
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0014DF2C File Offset: 0x0014C12C
		public void StartSinglePlayer()
		{
			PlatformSystems.networkManager.StartSinglePlayer();
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x0014DF6C File Offset: 0x0014C16C
		[CompilerGenerated]
		internal static void <ShowFirstTimeCrossPlayPopup>g__ActivateCrossPlay|51_6()
		{
			PlayerPrefs.SetInt("ShownFirstTimePopup", 1);
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0014DF79 File Offset: 0x0014C179
		[CompilerGenerated]
		internal static void <ShowFirstTimeCrossPlayPopup>g__DeactivateCrossPlay|51_7()
		{
			PlayerPrefs.SetInt("ShownFirstTimePopup", 1);
			BaseConVar baseConVar = Console.instance.FindConVar("egsToggle");
			if (baseConVar != null)
			{
				baseConVar.AttemptSetString("1");
			}
			Console.instance.SubmitCmd(null, "quit", false);
		}

		// Token: 0x04004D3E RID: 19774
		[NonSerialized]
		public BaseMainMenuScreen desiredMenuScreen;

		// Token: 0x04004D3F RID: 19775
		public BaseMainMenuScreen LoadingScreen;

		// Token: 0x04004D40 RID: 19776
		public BaseMainMenuScreen EngagementScreen;

		// Token: 0x04004D41 RID: 19777
		public BaseMainMenuScreen profileMenuScreen;

		// Token: 0x04004D42 RID: 19778
		public BaseMainMenuScreen EAwarningProfileMenu;

		// Token: 0x04004D43 RID: 19779
		public BaseMainMenuScreen multiplayerMenuScreen;

		// Token: 0x04004D44 RID: 19780
		public BaseMainMenuScreen titleMenuScreen;

		// Token: 0x04004D45 RID: 19781
		public BaseMainMenuScreen settingsMenuScreen;

		// Token: 0x04004D46 RID: 19782
		public BaseMainMenuScreen moreMenuScreen;

		// Token: 0x04004D47 RID: 19783
		public BaseMainMenuScreen extraGameModeMenuScreen;

		// Token: 0x04004D48 RID: 19784
		[HideInInspector]
		public BaseMainMenuScreen currentMenuScreen;

		// Token: 0x04004D49 RID: 19785
		public static MainMenuController instance;

		// Token: 0x04004D4A RID: 19786
		public HGButton exitButtonTransition;

		// Token: 0x04004D4B RID: 19787
		public HGButton profileButtonTransition;

		// Token: 0x04004D4C RID: 19788
		public HGButton onlineMultiplayerButtonTransition;

		// Token: 0x04004D4D RID: 19789
		public HGButton localMultiplayerButtonTransition;

		// Token: 0x04004D4E RID: 19790
		public GameObject steamBuildLabel;

		// Token: 0x04004D4F RID: 19791
		public HGButton continueButtonTransition;

		// Token: 0x04004D50 RID: 19792
		public GameObject EA_Panel;

		// Token: 0x04004D51 RID: 19793
		public GameObject mainMenuButtonPanel;

		// Token: 0x04004D52 RID: 19794
		public Transform cameraTransform;

		// Token: 0x04004D53 RID: 19795
		public float camRotationSmoothDampTime;

		// Token: 0x04004D54 RID: 19796
		public float camTranslationSmoothDampTime;

		// Token: 0x04004D55 RID: 19797
		private Vector3 camSmoothDampPositionVelocity;

		// Token: 0x04004D56 RID: 19798
		private Vector3 camSmoothDampRotationVelocity;

		// Token: 0x04004D57 RID: 19799
		public float camTransitionDuration;

		// Token: 0x04004D58 RID: 19800
		private float camTransitionTimer;

		// Token: 0x04004D59 RID: 19801
		private static bool wasInMultiplayer = false;

		// Token: 0x04004D5A RID: 19802
		private static EOSLoginManager.EOSLoginState lastEOSLoginState = EOSLoginManager.EOSLoginState.None;

		// Token: 0x04004D5B RID: 19803
		private static bool eaWarningShown = false;

		// Token: 0x04004D5C RID: 19804
		private static BoolConVar eaMessageSkipConVar = new BoolConVar("ea_message_skip", ConVarFlags.None, "0", "Whether or not to skip the early access splash screen.");

		// Token: 0x04004D5E RID: 19806
		private bool isInitialized;
	}
}
