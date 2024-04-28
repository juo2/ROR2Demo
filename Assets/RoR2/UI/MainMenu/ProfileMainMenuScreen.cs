using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DCB RID: 3531
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class ProfileMainMenuScreen : BaseMainMenuScreen
	{
		// Token: 0x060050D3 RID: 20691 RVA: 0x0014E08E File Offset: 0x0014C28E
		private string GuessDefaultProfileName()
		{
			return PlatformSystems.saveSystem.GetPlatformUsernameOrDefault("Nameless Survivor");
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0014E0A0 File Offset: 0x0014C2A0
		public UserProfile Editor_TryOpenTestProfile()
		{
			List<string> availableProfileNames = PlatformSystems.saveSystem.GetAvailableProfileNames();
			if (availableProfileNames.Count > 0)
			{
				Debug.LogWarning("Got existing profile!");
				return PlatformSystems.saveSystem.GetProfile(availableProfileNames[0]);
			}
			return null;
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0014E0E0 File Offset: 0x0014C2E0
		public void OpenDefaultProfile()
		{
			string name = this.GuessDefaultProfileName();
			UserProfile mainProfile = PlatformSystems.saveSystem.CreateProfile(RoR2Application.cloudStorage, name);
			this.SetMainProfile(mainProfile);
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x0014E10E File Offset: 0x0014C30E
		protected new void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.existingProfileListController.onProfileSelected += this.SetMainProfile;
			this.existingProfileListController.onListRebuilt += this.OnListRebuilt;
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0014E14C File Offset: 0x0014C34C
		protected void OnEnable()
		{
			this.firstTimeConfiguration = true;
			List<string> availableProfileNames = PlatformSystems.saveSystem.GetAvailableProfileNames();
			for (int i = 0; i < availableProfileNames.Count; i++)
			{
				if (ProfileMainMenuScreen.IsProfileCustom(PlatformSystems.saveSystem.GetProfile(availableProfileNames[i])))
				{
					this.firstTimeConfiguration = false;
					break;
				}
			}
			if (this.firstTimeConfiguration)
			{
				Debug.Log("First-Time Profile Configuration");
				this.OpenCreateProfileMenu(true);
				return;
			}
			this.createProfilePanel.SetActive(false);
			this.selectProfilePanel.SetActive(true);
			this.OnListRebuilt();
			this.gotoSelectProfilePanelButtonContainer.SetActive(true);
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0014E1E1 File Offset: 0x0014C3E1
		public void OpenCreateProfileMenu(bool firstTime)
		{
			this.selectProfilePanel.SetActive(false);
			this.createProfilePanel.SetActive(true);
			this.createProfileNameInputField.text = this.GuessDefaultProfileName();
			if (firstTime)
			{
				this.gotoSelectProfilePanelButtonContainer.SetActive(false);
			}
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x0014E21B File Offset: 0x0014C41B
		private void OnListRebuilt()
		{
			this.existingProfileListController.GetReadOnlyElementsList();
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x000026ED File Offset: 0x000008ED
		protected void OnDisable()
		{
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0014E22C File Offset: 0x0014C42C
		private void SetMainProfile(UserProfile profile)
		{
			LocalUserManager.SetLocalUsers(new LocalUserManager.LocalUserInitializationInfo[]
			{
				new LocalUserManager.LocalUserInitializationInfo
				{
					profile = profile
				}
			});
			if (this.myMainMenuController != null)
			{
				this.myMainMenuController.desiredMenuScreen = this.myMainMenuController.titleMenuScreen;
				return;
			}
			Debug.LogError("myMainMenuController reference null on ProfileMainMenuScreen.cs while trying to run SetMainProfile(UserProfile profile)");
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0014E28B File Offset: 0x0014C48B
		private static bool IsProfileCustom(UserProfile profile)
		{
			return profile.fileName != "default";
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0014E29D File Offset: 0x0014C49D
		private static bool IsNewProfileNameAcceptable(string newProfileName)
		{
			return PlatformSystems.saveSystem.GetProfile(newProfileName) == null && !(newProfileName == "");
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0014E2C0 File Offset: 0x0014C4C0
		public void OnAddProfilePressed()
		{
			if (this.eventSystemLocator.eventSystem.currentSelectedGameObject == this.createProfileNameInputField.gameObject && !Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				return;
			}
			string text = this.createProfileNameInputField.text;
			if (!ProfileMainMenuScreen.IsNewProfileNameAcceptable(text))
			{
				return;
			}
			this.createProfileNameInputField.text = "";
			UserProfile userProfile = PlatformSystems.saveSystem.CreateProfile(RoR2Application.cloudStorage, text);
			if (userProfile != null)
			{
				this.SetMainProfile(userProfile);
			}
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0014E348 File Offset: 0x0014C548
		protected new void Update()
		{
			if (this.eventSystemLocator.eventSystem && this.eventSystemLocator.eventSystem.player != null && this.eventSystemLocator.eventSystem.player.GetButton(31))
			{
				GameObject currentSelectedGameObject = MPEventSystemManager.combinedEventSystem.currentSelectedGameObject;
				if (currentSelectedGameObject)
				{
					UserProfileListElementController component = currentSelectedGameObject.GetComponent<UserProfileListElementController>();
					if (component)
					{
						if (component.userProfile == null)
						{
							Debug.LogError("!!!???");
							return;
						}
						SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
						string consoleString = "user_profile_delete \"" + component.userProfile.fileName + "\"";
						simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
						{
							token = "USER_PROFILE_DELETE_HEADER",
							formatParams = null
						};
						simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
						{
							token = "USER_PROFILE_DELETE_DESCRIPTION",
							formatParams = new object[]
							{
								component.userProfile.name
							}
						};
						simpleDialogBox.AddCommandButton(consoleString, "USER_PROFILE_DELETE_YES", Array.Empty<object>());
						simpleDialogBox.AddCancelButton("USER_PROFILE_DELETE_NO", Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x04004D69 RID: 19817
		public GameObject createProfilePanel;

		// Token: 0x04004D6A RID: 19818
		public TMP_InputField createProfileNameInputField;

		// Token: 0x04004D6B RID: 19819
		public MPButton submitProfileNameButton;

		// Token: 0x04004D6C RID: 19820
		public GameObject gotoSelectProfilePanelButtonContainer;

		// Token: 0x04004D6D RID: 19821
		public GameObject selectProfilePanel;

		// Token: 0x04004D6E RID: 19822
		public MPButton gotoCreateProfilePanelButton;

		// Token: 0x04004D6F RID: 19823
		public UserProfileListController existingProfileListController;

		// Token: 0x04004D70 RID: 19824
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004D71 RID: 19825
		private bool firstTimeConfiguration;

		// Token: 0x04004D72 RID: 19826
		private const string defaultName = "Nameless Survivor";
	}
}
