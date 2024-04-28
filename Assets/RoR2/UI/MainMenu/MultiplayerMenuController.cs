using System;
using System.Collections;
using System.Collections.Generic;
using RoR2.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DCE RID: 3534
	[RequireComponent(typeof(FirstSelectedObjectProvider))]
	public class MultiplayerMenuController : BaseMainMenuScreen
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060050EF RID: 20719 RVA: 0x0014E949 File Offset: 0x0014CB49
		// (set) Token: 0x060050F0 RID: 20720 RVA: 0x0014E950 File Offset: 0x0014CB50
		public static MultiplayerMenuController instance { get; private set; }

		// Token: 0x060050F1 RID: 20721 RVA: 0x0014E958 File Offset: 0x0014CB58
		public MultiplayerMenuController.Subview GetCurrentSubview()
		{
			return this._curSubView;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x0014E960 File Offset: 0x0014CB60
		public bool isInHostingState
		{
			get
			{
				return PlatformSystems.lobbyManager.state == LobbyManager.State.Hosting;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x0014E96F File Offset: 0x0014CB6F
		// (set) Token: 0x060050F4 RID: 20724 RVA: 0x0014E977 File Offset: 0x0014CB77
		public bool IsQuickPlaySearching { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x0014E980 File Offset: 0x0014CB80
		// (set) Token: 0x060050F6 RID: 20726 RVA: 0x0014E988 File Offset: 0x0014CB88
		public bool IsQuickPlayButtonLocked { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x0014E991 File Offset: 0x0014CB91
		// (set) Token: 0x060050F8 RID: 20728 RVA: 0x0014E999 File Offset: 0x0014CB99
		public bool CanLeave { get; set; } = true;

		// Token: 0x060050F9 RID: 20729 RVA: 0x0014E9A2 File Offset: 0x0014CBA2
		public void SetSubview(MultiplayerMenuController.Subview targetSubview)
		{
			this._curSubView = targetSubview;
			this.MainMultiplayerMenu.SetActive(targetSubview == MultiplayerMenuController.Subview.Main);
			this.JoinGameMenu.SetActive(targetSubview == MultiplayerMenuController.Subview.FindGame);
			this.HostGameMenu.SetActive(targetSubview == MultiplayerMenuController.Subview.HostGame);
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0014E9D8 File Offset: 0x0014CBD8
		public void ReturnToMainSubview()
		{
			this.SetSubview(MultiplayerMenuController.Subview.Main);
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0014E9E4 File Offset: 0x0014CBE4
		private void ToggleMPFeatures(MPFeatures featureFlags)
		{
			this.Toggle(this.hostGame, featureFlags.HasFlag(MPFeatures.HostGame));
			this.Toggle(this.findGame, featureFlags.HasFlag(MPFeatures.FindGame));
			this.Toggle(this.quickplayButton, featureFlags.HasFlag(MPFeatures.Quickplay));
			this.Toggle(this.startPrivateGameButton, featureFlags.HasFlag(MPFeatures.PrivateGame));
			this.Toggle(this.inviteButton, featureFlags.HasFlag(MPFeatures.Invite));
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0014EA84 File Offset: 0x0014CC84
		private void ToggleMPLobbyFeatures(MPLobbyFeatures lobbyFlags)
		{
			if (PlatformSystems.lobbyManager.HasMPLobbyUI())
			{
				this.Toggle(this.lobbyUserList.createLobbyButton, lobbyFlags.HasFlag(MPLobbyFeatures.CreateLobby));
				this.Toggle(this.lobbyUserList.leaveLobbyButton, lobbyFlags.HasFlag(MPLobbyFeatures.LeaveLobby));
				this.Toggle(this.lobbyUserList.copyLobbyButton, lobbyFlags.HasFlag(MPLobbyFeatures.Clipboard));
				this.Toggle(this.lobbyUserList.joinLobbyButton, lobbyFlags.HasFlag(MPLobbyFeatures.Clipboard));
			}
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0014EB2C File Offset: 0x0014CD2C
		private void ToggleMPFeaturesInteractable(MPFeatures featureFlags)
		{
			this.hostGame.interactable = featureFlags.HasFlag(MPFeatures.HostGame);
			this.findGame.interactable = featureFlags.HasFlag(MPFeatures.FindGame);
			this.quickplayButton.interactable = featureFlags.HasFlag(MPFeatures.Quickplay);
			this.startPrivateGameButton.interactable = featureFlags.HasFlag(MPFeatures.PrivateGame);
			this.inviteButton.interactable = featureFlags.HasFlag(MPFeatures.Invite);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0014EBC8 File Offset: 0x0014CDC8
		private void ToggleMPLobbyFeaturesInteractable(MPLobbyFeatures lobbyFlags)
		{
			this.lobbyUserList.createLobbyButton.interactable = lobbyFlags.HasFlag(MPLobbyFeatures.CreateLobby);
			this.lobbyUserList.leaveLobbyButton.interactable = lobbyFlags.HasFlag(MPLobbyFeatures.LeaveLobby);
			this.lobbyUserList.copyLobbyButton.interactable = lobbyFlags.HasFlag(MPLobbyFeatures.Clipboard);
			this.lobbyUserList.joinLobbyButton.interactable = lobbyFlags.HasFlag(MPLobbyFeatures.Clipboard);
			this.lobbyUserList.lobbyTypeDropdown.interactable = lobbyFlags.HasFlag(MPLobbyFeatures.LobbyDropdownOptions);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0014EC7F File Offset: 0x0014CE7F
		private void Toggle(MonoBehaviour button, bool val)
		{
			if (button)
			{
				button.gameObject.SetActive(val);
				return;
			}
			Debug.LogError("Nullref on Toggle of Button in MultiplayerMenuController on \"" + base.gameObject.name + "\"");
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0014ECB8 File Offset: 0x0014CEB8
		private void Toggle(GameObject[] goToToggle, bool val)
		{
			int num = goToToggle.Length;
			for (int i = 0; i < num; i++)
			{
				goToToggle[i].gameObject.SetActive(val);
			}
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0014ECE4 File Offset: 0x0014CEE4
		private void Toggle(MonoBehaviour[] buttons, bool val)
		{
			int num = buttons.Length;
			for (int i = 0; i < num; i++)
			{
				buttons[i].gameObject.SetActive(val);
			}
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x0014ED10 File Offset: 0x0014CF10
		public void SetQuickplayButtonStateText(string textKey, object[] formatArgs = null)
		{
			if (formatArgs == null)
			{
				formatArgs = Array.Empty<object>();
			}
			TextMeshProUGUI textMeshProUGUI = this.quickplayStateText;
			if (textMeshProUGUI)
			{
				textMeshProUGUI.text = Language.GetStringFormatted(textKey, formatArgs);
			}
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x0014ED44 File Offset: 0x0014CF44
		public void SetButtonText(MPButton button, string textKey, object[] formatArgs = null)
		{
			if (formatArgs == null)
			{
				formatArgs = Array.Empty<object>();
			}
			TextMeshProUGUI component = button.GetComponent<TextMeshProUGUI>();
			if (component)
			{
				component.text = Language.GetStringFormatted(textKey, formatArgs);
			}
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x0014ED77 File Offset: 0x0014CF77
		public void SetNetworkType(bool isInternet)
		{
			PlatformSystems.lobbyManager.SetNetworkType(isInternet);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x0014ED84 File Offset: 0x0014CF84
		public void RefreshFirstObjectSelectedProvider()
		{
			if (!this.firstSelectedObjectProvider)
			{
				return;
			}
			this.firstSelectedObjectProvider.firstSelectedObject = this.hostGame.gameObject;
			this.firstSelectedObjectProvider.fallBackFirstSelectedObjects = new GameObject[]
			{
				this.hostGame.gameObject,
				this.findGame.gameObject,
				this.quickplayButton.gameObject,
				this.startPrivateGameButton.gameObject,
				this.inviteButton.gameObject,
				this.backButton.gameObject
			};
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x0014EE1C File Offset: 0x0014D01C
		public void OnEnable()
		{
			this.ResetState();
			this.RefreshFirstObjectSelectedProvider();
			this.ToggleMPFeatures(PlatformSystems.lobbyManager.GetPlatformMPFeatureFlags());
			this.ToggleMPLobbyFeatures(PlatformSystems.lobbyManager.GetPlatformMPLobbyFeatureFlags());
			this.LerpAllUI(LerpUIRect.LerpState.Entering);
			if (!MultiplayerMenuController.instance)
			{
				MultiplayerMenuController.instance = SingletonHelper.Assign<MultiplayerMenuController>(MultiplayerMenuController.instance, this);
			}
			PlatformSystems.lobbyManager.OnMultiplayerMenuEnabled(new Action<UserID>(this.OnLobbyLeave));
			FirstSelectedObjectProvider firstSelectedObjectProvider = this.firstSelectedObjectProvider;
			if (firstSelectedObjectProvider != null)
			{
				firstSelectedObjectProvider.ResetLastSelected();
			}
			FirstSelectedObjectProvider firstSelectedObjectProvider2 = this.firstSelectedObjectProvider;
			if (firstSelectedObjectProvider2 == null)
			{
				return;
			}
			firstSelectedObjectProvider2.EnsureSelectedObject();
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x0014EEB0 File Offset: 0x0014D0B0
		public void OnDisable()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyLeave = (Action<UserID>)Delegate.Remove(lobbyManager.onLobbyLeave, new Action<UserID>(this.OnLobbyLeave));
			if (!NetworkManagerSystem.singleton.isNetworkActive)
			{
				PlatformSystems.lobbyManager.LeaveLobby();
			}
			MultiplayerMenuController.instance = SingletonHelper.Unassign<MultiplayerMenuController>(MultiplayerMenuController.instance, this);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0014EF09 File Offset: 0x0014D109
		private void OnLobbyLeave(UserID lobbyId)
		{
			if (!(PlatformSystems.lobbyManager as SteamworksLobbyManager).isInLobbyDelayed && !PlatformSystems.lobbyManager.awaitingJoin)
			{
				PlatformSystems.lobbyManager.CreateLobby();
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0014EF34 File Offset: 0x0014D134
		public new void Awake()
		{
			base.Awake();
			this.LerpAllUI(LerpUIRect.LerpState.Entering);
			this.hostGame.onClick.AddListener(delegate()
			{
				PlatformSystems.lobbyManager.SetNetworkType(true);
				this.SetSubview(MultiplayerMenuController.Subview.HostGame);
			});
			this.findGame.onClick.AddListener(delegate()
			{
				this.SetSubview(MultiplayerMenuController.Subview.FindGame);
			});
			this.quickplayButton.onClick.AddListener(delegate()
			{
				PlatformSystems.lobbyManager.ToggleQuickplay();
			});
			this.startPrivateGameButton.onClick.AddListener(delegate()
			{
				PlatformSystems.lobbyManager.SetNetworkType(true);
				PlatformSystems.lobbyManager.OnStartPrivateGame();
			});
			this.SetLobbyVisibilityChoices();
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x0014EFEC File Offset: 0x0014D1EC
		private void SetLobbyVisibilityChoices()
		{
			if (this.lobbyVisibilityController != null && PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				List<CarouselController.Choice> list = new List<CarouselController.Choice>(this.lobbyVisibilityController.choices);
				foreach (CarouselController.Choice choice in list)
				{
					if (choice.convarValue == "Private")
					{
						list.Remove(choice);
						break;
					}
				}
				this.lobbyVisibilityController.choices = list.ToArray();
			}
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0014F088 File Offset: 0x0014D288
		public void LerpAllUI(LerpUIRect.LerpState lerpState)
		{
			LerpUIRect[] array = this.uiToLerp;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].lerpState = lerpState;
			}
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0014F0B3 File Offset: 0x0014D2B3
		private void ResetState()
		{
			if (PlatformSystems.lobbyManager.GetPlatformMPFeatureFlags().HasFlag(MPFeatures.Quickplay))
			{
				this.SetQuickplayText("", new object[0]);
				this.ResetCutoffTime();
			}
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0014F0E8 File Offset: 0x0014D2E8
		public void BackButtonPressed()
		{
			Debug.LogError("BackButtonPressed");
			PlatformSystems.lobbyManager.LeaveLobby();
			this.lobbyUserList.ClearUserList();
			this.ReturnToMainMenu();
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x0014F10F File Offset: 0x0014D30F
		public void ReturnToMainMenu()
		{
			Debug.Log("ReturnToMainMenu");
			this.myMainMenuController.SetDesiredMenuScreen(this.myMainMenuController.titleMenuScreen);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnEnterGameButtonPressed()
		{
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0014F134 File Offset: 0x0014D334
		private new void Update()
		{
			base.Update();
			this.titleStopwatch += Time.deltaTime;
			PlatformSystems.lobbyManager.CheckBusyTimer();
			if (this.IsQuickPlaySearching)
			{
				this.ToggleMPFeaturesInteractable(MPFeatures.Quickplay);
				this.quickplaySpinnerRectTransform.gameObject.SetActive(true);
				Vector3 localEulerAngles = this.quickplaySpinnerRectTransform.localEulerAngles;
				localEulerAngles.z += Time.deltaTime * 360f;
				this.quickplaySpinnerRectTransform.localEulerAngles = localEulerAngles;
				this.quickplayLabelController.token = this.quickplayStopToken;
			}
			else if (PlatformSystems.lobbyManager.state == LobbyManager.State.Hosting)
			{
				this.ToggleMPFeaturesInteractable(MPFeatures.None);
				if (this.inviteButton)
				{
					this.inviteButton.interactable = this.ShouldEnableInviteButton();
				}
				this.quickplaySpinnerRectTransform.gameObject.SetActive(false);
				this.quickplayLabelController.token = this.quickplayStartToken;
			}
			else
			{
				this.ToggleMPFeaturesInteractable(PlatformSystems.lobbyManager.GetPlatformMPFeatureFlags());
				this.quickplaySpinnerRectTransform.gameObject.SetActive(false);
				this.quickplayLabelController.token = this.quickplayStartToken;
				if (this.quickplayButton)
				{
					this.quickplayButton.interactable = this.ShouldEnableQuickplayButton();
				}
				if (this.startPrivateGameButton)
				{
					this.startPrivateGameButton.interactable = this.ShouldEnableStartPrivateGameButton();
				}
				if (this.joinClipboardLobbyButtonController && this.joinClipboardLobbyButtonController.mpButton)
				{
					this.joinClipboardLobbyButtonController.mpButton.interactable = this.ShouldEnableJoinClipboardLobbyButton();
				}
				if (this.inviteButton)
				{
					this.inviteButton.interactable = this.ShouldEnableInviteButton();
				}
				this.backButton.interactable = this.ShouldEnableBackButton();
			}
			FirstSelectedObjectProvider firstSelectedObjectProvider = this.firstSelectedObjectProvider;
			if (firstSelectedObjectProvider == null)
			{
				return;
			}
			firstSelectedObjectProvider.EnsureSelectedObject();
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0014F302 File Offset: 0x0014D502
		public void CreateLocalLobby()
		{
			NetworkManagerSystem.singleton.CreateLocalLobby();
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0014F30E File Offset: 0x0014D50E
		public void InviteButtonPressed()
		{
			if (!PlatformSystems.lobbyManager.isInLobby)
			{
				this.CreateLocalLobby();
			}
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0014F322 File Offset: 0x0014D522
		private bool ShouldEnableQuickplayButton()
		{
			return PlatformSystems.lobbyManager.ShouldEnableQuickplayButton();
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0014F32E File Offset: 0x0014D52E
		private bool ShouldEnableStartPrivateGameButton()
		{
			return PlatformSystems.lobbyManager.ShouldEnableStartPrivateGameButton();
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0014F33A File Offset: 0x0014D53A
		private bool ShouldEnableJoinClipboardLobbyButton()
		{
			return !PlatformSystems.lobbyManager.newestLobbyData.quickplayQueued && this.joinClipboardLobbyButtonController.validClipboardLobbyID;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0014F35A File Offset: 0x0014D55A
		private bool ShouldEnableInviteButton()
		{
			return PlatformSystems.lobbyManager.CanInvite();
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0014F366 File Offset: 0x0014D566
		public void ToggleQuickplay()
		{
			PlatformSystems.lobbyManager.ToggleQuickplay();
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool IsReadyToLeave()
		{
			return true;
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x0014F372 File Offset: 0x0014D572
		private bool ShouldEnableBackButton()
		{
			return !PlatformSystems.lobbyManager.IsBusy && this._curSubView == MultiplayerMenuController.Subview.Main;
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x0014F38B File Offset: 0x0014D58B
		private bool ShouldEnableEnterGameButton()
		{
			return !PlatformSystems.lobbyManager.IsBusy;
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0014F39A File Offset: 0x0014D59A
		public void GoBack()
		{
			if (PlatformSystems.lobbyManager.newestLobbyData.quickplayQueued)
			{
				this.quickplayButton.InvokeClick();
				return;
			}
			if (this.ShouldEnableBackButton())
			{
				this.backButton.InvokeClick();
			}
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x000026ED File Offset: 0x000008ED
		public void ResetLastSelected()
		{
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x0014F3CC File Offset: 0x0014D5CC
		public void ResetCutoffTime()
		{
			if (!PlatformSystems.lobbyManager.HasMPFeature(MPFeatures.Quickplay))
			{
				return;
			}
			if (this.updateCutoffTime != null)
			{
				base.StopCoroutine(this.updateCutoffTime);
				this.updateCutoffTime = null;
			}
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x0014F3F7 File Offset: 0x0014D5F7
		private void OnLobbyPropertyCutoffTimeChanged(double cutoffTime)
		{
			this.hasCutoffTime = true;
			this.cutoffTime = cutoffTime;
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0014F407 File Offset: 0x0014D607
		private IEnumerator UpdateCutoffTime()
		{
			if (PlatformSystems.lobbyManager.HasMPFeature(MPFeatures.Quickplay))
			{
				Debug.LogError("UpdateCutoffTime coroutine start");
				float fullDuration = MultiplayerMenuController.QuickplayWaitTime;
				float age = 0f;
				this.hasCutoffTime = false;
				if (PlatformSystems.networkManager.IsHost())
				{
					Debug.LogError("UpdateCutoffTime coroutine host");
					TimeSpan timeSpan = DateTime.Now + TimeSpan.FromSeconds((double)fullDuration) - Util.dateZero;
					PlatformSystems.lobbyManager.SetQuickplayCutoffTime(timeSpan.TotalSeconds);
				}
				else
				{
					Debug.LogError("UpdateCutoffTime coroutine client");
					this.cutoffTime = PlatformSystems.lobbyManager.GetQuickplayCutoffTime();
					if (this.cutoffTime == 0.0)
					{
						while (!this.hasCutoffTime)
						{
							yield return null;
						}
					}
					Debug.LogError(string.Format("Client cutoffTime = {0}", this.cutoffTime));
					Debug.LogError(string.Format("Client cutoffTime = {0}", this.cutoffTime));
					fullDuration = this.CalculateCutoffTimerDuration();
				}
				while (age < fullDuration)
				{
					if (!PlatformSystems.networkManager.isHost)
					{
						double quickplayCutoffTime = PlatformSystems.lobbyManager.GetQuickplayCutoffTime();
						if (quickplayCutoffTime != this.cutoffTime)
						{
							this.cutoffTime = quickplayCutoffTime;
							fullDuration = this.CalculateCutoffTimerDuration();
						}
					}
					this.UpdateCutoffTimeText(PlatformSystems.lobbyManager.calculatedTotalPlayerCount, fullDuration, age);
					age += Time.unscaledDeltaTime;
					yield return null;
				}
				PlatformSystems.lobbyManager.OnCutoffTimerComplete();
			}
			yield break;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0014F418 File Offset: 0x0014D618
		private float CalculateCutoffTimerDuration()
		{
			DateTime now = DateTime.Now;
			return (float)(Util.dateZero + TimeSpan.FromSeconds(this.cutoffTime) - now).TotalSeconds;
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0014F44F File Offset: 0x0014D64F
		public void StartUpdateCutoffTimer()
		{
			if (!PlatformSystems.lobbyManager.HasMPFeature(MPFeatures.Quickplay))
			{
				return;
			}
			this.updateCutoffTime = base.StartCoroutine(this.UpdateCutoffTime());
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x0014F474 File Offset: 0x0014D674
		public void SetQuickplayTextBelowMinPlayers()
		{
			if (!PlatformSystems.lobbyManager.HasMPFeature(MPFeatures.Quickplay))
			{
				return;
			}
			object[] formatArgs = new object[]
			{
				PlatformSystems.lobbyManager.calculatedTotalPlayerCount,
				RoR2Application.maxPlayers
			};
			this.SetQuickplayText("STEAM_LOBBY_STATUS_QUICKPLAY_WAITING_BELOW_MINIMUM_PLAYERS", formatArgs);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0014F4C4 File Offset: 0x0014D6C4
		private void UpdateCutoffTimeText(int numberOfPlayers, float fullDuration, float age)
		{
			object[] formatArgs = new object[]
			{
				numberOfPlayers,
				RoR2Application.maxPlayers,
				(int)Math.Max(0.0, (double)(fullDuration - age))
			};
			this.SetQuickplayText("STEAM_LOBBY_STATUS_QUICKPLAY_WAITING_ABOVE_MINIMUM_PLAYERS", formatArgs);
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x0014F515 File Offset: 0x0014D715
		public void SetQuickplayText(string text, object[] formatArgs = null)
		{
			if (formatArgs == null)
			{
				formatArgs = Array.Empty<object>();
			}
			this.quickplayStateText.text = Language.GetStringFormatted(text, formatArgs);
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x0014F533 File Offset: 0x0014D733
		public IEnumerator LockQuickPlayButton(float duration)
		{
			this.IsQuickPlayButtonLocked = true;
			yield return new WaitForSeconds(duration);
			this.IsQuickPlayButtonLocked = false;
			yield break;
		}

		// Token: 0x04004D84 RID: 19844
		private const float titleTransitionDuration = 0.5f;

		// Token: 0x04004D85 RID: 19845
		private const float titleTransitionBuffer = 0.1f;

		// Token: 0x04004D86 RID: 19846
		public Image fadeImage;

		// Token: 0x04004D87 RID: 19847
		public LerpUIRect[] uiToLerp;

		// Token: 0x04004D88 RID: 19848
		private float titleStopwatch;

		// Token: 0x04004D89 RID: 19849
		private MultiplayerMenuController.Subview _curSubView;

		// Token: 0x04004D8A RID: 19850
		[Header("Subviews")]
		public GameObject MainMultiplayerMenu;

		// Token: 0x04004D8B RID: 19851
		public GameObject JoinGameMenu;

		// Token: 0x04004D8C RID: 19852
		public GameObject HostGameMenu;

		// Token: 0x04004D8D RID: 19853
		[Header("Buttons")]
		public MPButton hostGame;

		// Token: 0x04004D8E RID: 19854
		public MPButton findGame;

		// Token: 0x04004D8F RID: 19855
		public MPButton quickplayButton;

		// Token: 0x04004D90 RID: 19856
		public MPButton startPrivateGameButton;

		// Token: 0x04004D91 RID: 19857
		public MPButton inviteButton;

		// Token: 0x04004D92 RID: 19858
		public MPButton backButton;

		// Token: 0x04004D93 RID: 19859
		public GameObject[] lobbyActions;

		// Token: 0x04004D94 RID: 19860
		[Header("Quickplay Logic")]
		public LanguageTextMeshController quickplayLabelController;

		// Token: 0x04004D95 RID: 19861
		public string quickplayStartToken;

		// Token: 0x04004D96 RID: 19862
		public string quickplayStopToken;

		// Token: 0x04004D97 RID: 19863
		public RectTransform quickplaySpinnerRectTransform;

		// Token: 0x04004D98 RID: 19864
		public TextMeshProUGUI quickplayStateText;

		// Token: 0x04004D99 RID: 19865
		private Coroutine updateCutoffTime;

		// Token: 0x04004D9A RID: 19866
		private bool hasCutoffTime;

		// Token: 0x04004D9B RID: 19867
		private double cutoffTime;

		// Token: 0x04004D9C RID: 19868
		public static float QuickplayWaitTime = 45f;

		// Token: 0x04004D9F RID: 19871
		[Header("Sub-Views")]
		public SteamJoinClipboardLobby joinClipboardLobbyButtonController;

		// Token: 0x04004DA0 RID: 19872
		public LobbyUserList lobbyUserList;

		// Token: 0x04004DA2 RID: 19874
		[Header("Platform Specific Components")]
		public CarouselController lobbyVisibilityController;

		// Token: 0x02000DCF RID: 3535
		public enum Subview
		{
			// Token: 0x04004DA4 RID: 19876
			Main,
			// Token: 0x04004DA5 RID: 19877
			FindGame,
			// Token: 0x04004DA6 RID: 19878
			HostGame
		}
	}
}
