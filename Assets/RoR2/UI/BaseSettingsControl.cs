using System;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000CBD RID: 3261
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class BaseSettingsControl : MonoBehaviour
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x00131365 File Offset: 0x0012F565
		public bool hasBeenChanged
		{
			get
			{
				return this.originalValue != null;
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00131370 File Offset: 0x0012F570
		protected void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			if (this.nameLabel && !string.IsNullOrEmpty(this.nameToken))
			{
				this.nameLabel.token = this.nameToken;
			}
			if (this.settingSource == BaseSettingsControl.SettingSource.ConVar && this.GetConVar() == null)
			{
				Debug.LogErrorFormat("Null convar {0} detected in options", new object[]
				{
					this.settingName
				});
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x001313DD File Offset: 0x0012F5DD
		protected void Start()
		{
			this.Initialize();
			this.UpdateControls();
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x001313EB File Offset: 0x0012F5EB
		protected void OnEnable()
		{
			this.UpdateControls();
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x001313F3 File Offset: 0x0012F5F3
		protected virtual void Update()
		{
			if (this.updateControlsInUpdate)
			{
				this.UpdateControls();
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void Initialize()
		{
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00131403 File Offset: 0x0012F603
		public void SubmitSetting(string newValue)
		{
			if (this.useConfirmationDialog)
			{
				this.SubmitSettingTemporary(newValue);
				return;
			}
			this.SubmitSettingInternal(newValue);
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0013141C File Offset: 0x0012F61C
		private void SubmitSettingInternal(string newValue)
		{
			if (this.originalValue == null)
			{
				this.originalValue = this.GetCurrentValue();
			}
			if (this.originalValue == newValue)
			{
				this.originalValue = null;
			}
			BaseSettingsControl.SettingSource settingSource = this.settingSource;
			if (settingSource != BaseSettingsControl.SettingSource.ConVar)
			{
				if (settingSource == BaseSettingsControl.SettingSource.UserProfilePref)
				{
					UserProfile currentUserProfile = this.GetCurrentUserProfile();
					if (currentUserProfile != null)
					{
						currentUserProfile.SetSaveFieldString(this.settingName, newValue);
					}
					UserProfile currentUserProfile2 = this.GetCurrentUserProfile();
					if (currentUserProfile2 != null)
					{
						currentUserProfile2.RequestEventualSave();
					}
				}
			}
			else
			{
				BaseConVar conVar = this.GetConVar();
				if (conVar != null)
				{
					conVar.AttemptSetString(newValue);
				}
			}
			RoR2Application.onNextUpdate += this.UpdateControls;
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x001314B0 File Offset: 0x0012F6B0
		private void SubmitSettingTemporary(string newValue)
		{
			string oldValue = this.GetCurrentValue();
			if (newValue == oldValue)
			{
				return;
			}
			this.SubmitSettingInternal(newValue);
			SimpleDialogBox dialogBox = SimpleDialogBox.Create(null);
			Action revertFunction = delegate()
			{
				if (dialogBox)
				{
					this.SubmitSettingInternal(oldValue);
				}
			};
			float num = 10f;
			float timeEnd = Time.unscaledTime + num;
			MPButton revertButton = dialogBox.AddActionButton(delegate
			{
				revertFunction();
			}, "OPTION_REVERT", true, Array.Empty<object>());
			dialogBox.AddActionButton(delegate
			{
			}, "OPTION_ACCEPT", true, Array.Empty<object>());
			Action updateText = null;
			updateText = delegate()
			{
				if (dialogBox)
				{
					int num2 = Mathf.FloorToInt(timeEnd - Time.unscaledTime);
					if (num2 < 0)
					{
						num2 = 0;
					}
					dialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair
					{
						token = "OPTION_AUTOREVERT_DIALOG_DESCRIPTION",
						formatParams = new object[]
						{
							num2
						}
					};
					if (num2 > 0)
					{
						RoR2Application.unscaledTimeTimers.CreateTimer(1f, updateText);
					}
				}
			};
			updateText();
			dialogBox.headerToken = new SimpleDialogBox.TokenParamsPair
			{
				token = "OPTION_AUTOREVERT_DIALOG_TITLE"
			};
			RoR2Application.unscaledTimeTimers.CreateTimer(num, delegate
			{
				if (revertButton)
				{
					revertButton.onClick.Invoke();
				}
			});
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x001315D8 File Offset: 0x0012F7D8
		public string GetCurrentValue()
		{
			BaseSettingsControl.SettingSource settingSource = this.settingSource;
			if (settingSource != BaseSettingsControl.SettingSource.ConVar)
			{
				if (settingSource != BaseSettingsControl.SettingSource.UserProfilePref)
				{
					return "";
				}
				UserProfile currentUserProfile = this.GetCurrentUserProfile();
				return ((currentUserProfile != null) ? currentUserProfile.GetSaveFieldString(this.settingName) : null) ?? "";
			}
			else
			{
				BaseConVar conVar = this.GetConVar();
				if (conVar == null)
				{
					return null;
				}
				return conVar.GetString();
			}
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x0013162E File Offset: 0x0012F82E
		protected BaseConVar GetConVar()
		{
			return Console.instance.FindConVar(this.settingName);
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00131640 File Offset: 0x0012F840
		public UserProfile GetCurrentUserProfile()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			if (eventSystem == null)
			{
				return null;
			}
			LocalUser localUser = eventSystem.localUser;
			if (localUser == null)
			{
				return null;
			}
			return localUser.userProfile;
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00131663 File Offset: 0x0012F863
		public void Revert()
		{
			if (this.hasBeenChanged)
			{
				this.SubmitSetting(this.originalValue);
				this.originalValue = null;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x00131680 File Offset: 0x0012F880
		// (set) Token: 0x06004A63 RID: 19043 RVA: 0x00131688 File Offset: 0x0012F888
		private protected bool inUpdateControls { protected get; private set; }

		// Token: 0x06004A64 RID: 19044 RVA: 0x00131691 File Offset: 0x0012F891
		protected void UpdateControls()
		{
			if (!this)
			{
				return;
			}
			if (this.inUpdateControls)
			{
				return;
			}
			this.inUpdateControls = true;
			this.OnUpdateControls();
			this.inUpdateControls = false;
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnUpdateControls()
		{
		}

		// Token: 0x0400470B RID: 18187
		public BaseSettingsControl.SettingSource settingSource;

		// Token: 0x0400470C RID: 18188
		[FormerlySerializedAs("convarName")]
		public string settingName;

		// Token: 0x0400470D RID: 18189
		public string nameToken;

		// Token: 0x0400470E RID: 18190
		public LanguageTextMeshController nameLabel;

		// Token: 0x0400470F RID: 18191
		[Tooltip("Whether or not this setting requires a confirmation dialog. This is mainly for video options.")]
		public bool useConfirmationDialog;

		// Token: 0x04004710 RID: 18192
		[Tooltip("Whether or not this updates every frame. Should be disabled unless the setting is being modified from some other source.")]
		public bool updateControlsInUpdate;

		// Token: 0x04004711 RID: 18193
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004712 RID: 18194
		private string originalValue;

		// Token: 0x02000CBE RID: 3262
		public enum SettingSource
		{
			// Token: 0x04004715 RID: 18197
			ConVar,
			// Token: 0x04004716 RID: 18198
			UserProfilePref
		}
	}
}
