using System;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.LogBook
{
	// Token: 0x02000DD3 RID: 3539
	public class CategoryDef
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600513A RID: 20794 RVA: 0x0014F80B File Offset: 0x0014DA0B
		// (set) Token: 0x0600513B RID: 20795 RVA: 0x0014F813 File Offset: 0x0014DA13
		public GameObject iconPrefab
		{
			get
			{
				return this._iconPrefab;
			}
			[NotNull]
			set
			{
				this._iconPrefab = value;
				this.iconSize = ((RectTransform)this._iconPrefab.transform).sizeDelta;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x0014F837 File Offset: 0x0014DA37
		// (set) Token: 0x0600513D RID: 20797 RVA: 0x0014F83F File Offset: 0x0014DA3F
		[NotNull]
		public CategoryDef.BuildEntriesDelegate buildEntries { private get; set; }

		// Token: 0x0600513E RID: 20798 RVA: 0x0014F848 File Offset: 0x0014DA48
		public Entry[] BuildEntries([CanBeNull] UserProfile viewerProfile)
		{
			Entry[] array = this.buildEntries(viewerProfile);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].category = this;
			}
			return array;
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x0014F880 File Offset: 0x0014DA80
		public static void InitializeDefault(GameObject gameObject, Entry entry, EntryStatus status, UserProfile userProfile)
		{
			Texture texture = null;
			Color color = Color.white;
			Texture texture2;
			switch (status)
			{
			case EntryStatus.Unimplemented:
				texture2 = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texWIPIcon");
				break;
			case EntryStatus.Locked:
				texture2 = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texUnlockIcon");
				color = Color.gray;
				break;
			case EntryStatus.Unencountered:
				texture2 = entry.iconTexture;
				color = Color.black;
				break;
			case EntryStatus.Available:
				texture2 = entry.iconTexture;
				texture = entry.bgTexture;
				color = Color.white;
				break;
			case EntryStatus.New:
				texture2 = entry.iconTexture;
				texture = entry.bgTexture;
				color = new Color(1f, 0.8f, 0.5f, 1f);
				break;
			default:
				throw new ArgumentOutOfRangeException("status", status, null);
			}
			RawImage rawImage = null;
			ChildLocator component = gameObject.GetComponent<ChildLocator>();
			RawImage rawImage2;
			if (component)
			{
				rawImage2 = component.FindChild("Icon").GetComponent<RawImage>();
				rawImage = component.FindChild("BG").GetComponent<RawImage>();
			}
			else
			{
				rawImage2 = gameObject.GetComponentInChildren<RawImage>();
			}
			rawImage2.texture = texture2;
			rawImage2.color = color;
			if (rawImage)
			{
				if (texture != null)
				{
					rawImage.texture = texture;
				}
				else
				{
					rawImage.enabled = false;
				}
			}
			TextMeshProUGUI componentInChildren = gameObject.GetComponentInChildren<TextMeshProUGUI>();
			if (componentInChildren)
			{
				if (status >= EntryStatus.Available)
				{
					componentInChildren.text = entry.GetDisplayName(userProfile);
					return;
				}
				componentInChildren.text = Language.GetString("UNIDENTIFIED");
			}
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x0014F9E4 File Offset: 0x0014DBE4
		public static void InitializeChallenge(GameObject gameObject, Entry entry, EntryStatus status, UserProfile userProfile)
		{
			TextMeshProUGUI textMeshProUGUI = null;
			TextMeshProUGUI textMeshProUGUI2 = null;
			RawImage rawImage = null;
			AchievementDef achievementDef = (AchievementDef)entry.extraData;
			float achievementProgress = AchievementManager.GetUserAchievementManager(LocalUserManager.readOnlyLocalUsersList.FirstOrDefault((LocalUser v) => v.userProfile == userProfile)).GetAchievementProgress(achievementDef);
			HGButton component = gameObject.GetComponent<HGButton>();
			if (component)
			{
				component.disablePointerClick = true;
				component.disableGamepadClick = true;
			}
			ChildLocator component2 = gameObject.GetComponent<ChildLocator>();
			if (component2)
			{
				textMeshProUGUI = component2.FindChild("DescriptionLabel").GetComponent<TextMeshProUGUI>();
				textMeshProUGUI2 = component2.FindChild("NameLabel").GetComponent<TextMeshProUGUI>();
				rawImage = component2.FindChild("RewardImage").GetComponent<RawImage>();
				textMeshProUGUI2.text = Language.GetString(achievementDef.nameToken);
				textMeshProUGUI.text = Language.GetString(achievementDef.descriptionToken);
			}
			Texture texture = null;
			Color color = Color.white;
			switch (status)
			{
			case EntryStatus.None:
				component2.FindChild("RewardImageContainer").gameObject.SetActive(true);
				textMeshProUGUI2.text = "";
				textMeshProUGUI.text = "";
				break;
			case EntryStatus.Unimplemented:
				texture = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texWIPIcon");
				break;
			case EntryStatus.Locked:
				texture = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texUnlockIcon");
				color = Color.black;
				textMeshProUGUI2.text = Language.GetString("UNIDENTIFIED");
				textMeshProUGUI.text = Language.GetString("UNIDENTIFIED_DESCRIPTION");
				component2.FindChild("CantBeAchieved").gameObject.SetActive(true);
				break;
			case EntryStatus.Unencountered:
				texture = LegacyResourcesAPI.Load<Texture2D>("Textures/MiscIcons/texUnlockIcon");
				color = Color.gray;
				component2.FindChild("ProgressTowardsUnlocking").GetComponent<Image>().fillAmount = achievementProgress;
				break;
			case EntryStatus.Available:
				texture = entry.iconTexture;
				color = Color.white;
				component2.FindChild("HasBeenUnlocked").gameObject.SetActive(true);
				break;
			case EntryStatus.New:
				texture = entry.iconTexture;
				color = new Color(1f, 0.8f, 0.5f, 1f);
				component2.FindChild("HasBeenUnlocked").gameObject.SetActive(true);
				break;
			default:
				throw new ArgumentOutOfRangeException("status", status, null);
			}
			if (texture != null)
			{
				rawImage.texture = texture;
				rawImage.color = color;
				return;
			}
			rawImage.enabled = false;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0014FC48 File Offset: 0x0014DE48
		public static void InitializeMorgue(GameObject gameObject, Entry entry, EntryStatus status, UserProfile userProfile)
		{
			RunReport runReport = entry.extraData as RunReport;
			GameEndingDef gameEnding = runReport.gameEnding;
			ChildLocator component = gameObject.GetComponent<ChildLocator>();
			if (component)
			{
				TextMeshProUGUI component2 = component.FindChild("DescriptionLabel").GetComponent<TextMeshProUGUI>();
				TMP_Text component3 = component.FindChild("NameLabel").GetComponent<TextMeshProUGUI>();
				RawImage component4 = component.FindChild("IconImage").GetComponent<RawImage>();
				Image component5 = component.FindChild("BackgroundImage").GetComponent<Image>();
				Texture iconTexture = entry.iconTexture;
				component3.text = entry.GetDisplayName(userProfile);
				GameEndingDef gameEnding2 = runReport.gameEnding;
				component2.text = Language.GetString(((gameEnding2 != null) ? gameEnding2.endingTextToken : null) ?? string.Empty);
				component2.color = ((gameEnding != null) ? gameEnding.foregroundColor : Color.white);
				component5.color = ((gameEnding != null) ? gameEnding.backgroundColor : Color.black);
				if (iconTexture != null)
				{
					component4.texture = iconTexture;
					return;
				}
				component4.enabled = false;
			}
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0014FD40 File Offset: 0x0014DF40
		public static void InitializeStats(GameObject gameObject, Entry entry, EntryStatus status, UserProfile userProfile)
		{
			UserProfile userProfile2 = entry.extraData as UserProfile;
			ChildLocator component = gameObject.GetComponent<ChildLocator>();
			if (component)
			{
				TextMeshProUGUI component2 = component.FindChild("NameLabel").GetComponent<TextMeshProUGUI>();
				TextMeshProUGUI component3 = component.FindChild("TimeLabel").GetComponent<TextMeshProUGUI>();
				TMP_Text component4 = component.FindChild("CompletionLabel").GetComponent<TextMeshProUGUI>();
				RawImage component5 = component.FindChild("IconImage").GetComponent<RawImage>();
				component.FindChild("BackgroundImage").GetComponent<Image>();
				Texture iconTexture = entry.iconTexture;
				IntFraction totalCompletion = new GameCompletionStatsHelper().GetTotalCompletion(userProfile2);
				float num = (float)totalCompletion;
				string text = string.Format("{0:0%}", num);
				TimeSpan timeSpan = TimeSpan.FromSeconds(userProfile2.totalLoginSeconds);
				string text2 = string.Format("{0}:{1:D2}", (uint)timeSpan.TotalHours, (uint)timeSpan.Minutes);
				component2.text = entry.GetDisplayName(userProfile2);
				component3.text = text2;
				component4.text = text;
				if (iconTexture != null)
				{
					component5.texture = iconTexture;
					return;
				}
				component5.enabled = false;
			}
		}

		// Token: 0x04004DB3 RID: 19891
		[NotNull]
		public string nameToken = string.Empty;

		// Token: 0x04004DB4 RID: 19892
		private GameObject _iconPrefab;

		// Token: 0x04004DB5 RID: 19893
		public Vector2 iconSize = Vector2.one;

		// Token: 0x04004DB6 RID: 19894
		public bool fullWidth;

		// Token: 0x04004DB7 RID: 19895
		public Action<GameObject, Entry, EntryStatus, UserProfile> initializeElementGraphics = new Action<GameObject, Entry, EntryStatus, UserProfile>(CategoryDef.InitializeDefault);

		// Token: 0x04004DB8 RID: 19896
		[CanBeNull]
		public ViewablesCatalog.Node viewableNode;

		// Token: 0x02000DD4 RID: 3540
		// (Invoke) Token: 0x06005145 RID: 20805
		[NotNull]
		public delegate Entry[] BuildEntriesDelegate([CanBeNull] UserProfile viewerProfile);
	}
}
