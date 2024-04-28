using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using RoR2.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D00 RID: 3328
	[RequireComponent(typeof(MPEventSystemProvider))]
	public class GameEndReportPanelController : MonoBehaviour
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x00137CA8 File Offset: 0x00135EA8
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x00137CB0 File Offset: 0x00135EB0
		public GameEndReportPanelController.DisplayData displayData { get; private set; }

		// Token: 0x06004BCF RID: 19407 RVA: 0x00137CB9 File Offset: 0x00135EB9
		private void Awake()
		{
			this.playerNavigationController.onPageChangeSubmitted += this.OnPlayerNavigationControllerPageChangeSubmitted;
			this.SetContinueButtonAction(null);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x00137CDC File Offset: 0x00135EDC
		private void AllocateStatStrips(int count)
		{
			while (this.statStrips.Count > count)
			{
				int index = this.statStrips.Count - 1;
				UnityEngine.Object.Destroy(this.statStrips[index].gameObject);
				this.statStrips.RemoveAt(index);
			}
			while (this.statStrips.Count < count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.statStripPrefab, this.statContentArea);
				gameObject.SetActive(true);
				this.statStrips.Add(gameObject);
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x00137D60 File Offset: 0x00135F60
		private void AllocateUnlockStrips(int count)
		{
			while (this.unlockStrips.Count > count)
			{
				int index = this.unlockStrips.Count - 1;
				UnityEngine.Object.Destroy(this.unlockStrips[index].gameObject);
				this.unlockStrips.RemoveAt(index);
			}
			while (this.unlockStrips.Count < count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.unlockStripPrefab, this.unlockContentArea);
				gameObject.SetActive(true);
				this.unlockStrips.Add(gameObject);
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00137DE4 File Offset: 0x00135FE4
		public void SetDisplayData(GameEndReportPanelController.DisplayData newDisplayData)
		{
			if (this.displayData.Equals(newDisplayData))
			{
				return;
			}
			this.displayData = newDisplayData;
			bool flag = Run.instance && this.displayData.runReport != null && Run.instance.GetUniqueId() == this.displayData.runReport.runGuid;
			if (this.resultLabel && this.resultIconBackgroundImage && this.resultIconForegroundImage)
			{
				GameEndingDef gameEndingDef = null;
				if (this.displayData.runReport != null)
				{
					gameEndingDef = this.displayData.runReport.gameEnding;
				}
				this.resultLabel.gameObject.SetActive(gameEndingDef);
				this.resultIconBackgroundImage.gameObject.SetActive(gameEndingDef);
				this.resultIconForegroundImage.gameObject.SetActive(gameEndingDef);
				if (gameEndingDef)
				{
					this.resultLabel.text = Language.GetString(gameEndingDef.endingTextToken);
					this.resultIconBackgroundImage.color = gameEndingDef.backgroundColor;
					this.resultIconForegroundImage.color = gameEndingDef.foregroundColor;
					this.resultIconForegroundImage.sprite = gameEndingDef.icon;
					this.resultIconBackgroundImage.material = gameEndingDef.material;
				}
			}
			DifficultyIndex difficultyIndex = DifficultyIndex.Invalid;
			if (this.displayData.runReport != null)
			{
				difficultyIndex = this.displayData.runReport.ruleBook.FindDifficulty();
			}
			DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(difficultyIndex);
			if (this.selectedDifficultyImage)
			{
				this.selectedDifficultyImage.sprite = ((difficultyDef != null) ? difficultyDef.GetIconSprite() : null);
			}
			if (this.selectedDifficultyLabel)
			{
				this.selectedDifficultyLabel.token = ((difficultyDef != null) ? difficultyDef.nameToken : null);
			}
			if (this.artifactDisplayPanelController)
			{
				RuleBook ruleBook = this.displayData.runReport.ruleBook;
				List<ArtifactDef> list = new List<ArtifactDef>(ArtifactCatalog.artifactCount);
				for (int i = 0; i < RuleCatalog.choiceCount; i++)
				{
					RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(i);
					if (choiceDef.artifactIndex != ArtifactIndex.None && ruleBook.GetRuleChoice(choiceDef.ruleDef) == choiceDef)
					{
						list.Add(ArtifactCatalog.GetArtifactDef(choiceDef.artifactIndex));
					}
				}
				List<ArtifactDef>.Enumerator enumerator = list.GetEnumerator();
				this.artifactDisplayPanelController.SetDisplayData<List<ArtifactDef>.Enumerator>(ref enumerator);
			}
			RunReport runReport = this.displayData.runReport;
			RunReport.PlayerInfo playerInfo = (runReport != null) ? runReport.GetPlayerInfoSafe(this.displayData.playerIndex) : null;
			this.SetPlayerInfo(playerInfo);
			RunReport runReport2 = this.displayData.runReport;
			int pageCount = (runReport2 != null) ? runReport2.playerInfoCount : 0;
			GameObject gameObject = this.playerNavigationController.gameObject;
			RunReport runReport3 = this.displayData.runReport;
			gameObject.SetActive(((runReport3 != null) ? runReport3.playerInfoCount : 0) > 1);
			if (this.chatboxTransform)
			{
				this.chatboxTransform.gameObject.SetActive(!RoR2Application.isInSinglePlayer && flag);
			}
			this.playerNavigationController.SetDisplayData(new CarouselNavigationController.DisplayData(pageCount, this.displayData.playerIndex));
			ReadOnlyCollection<MPButton> elements = this.playerNavigationController.buttonAllocator.elements;
			for (int j = 0; j < elements.Count; j++)
			{
				MPButton mpbutton = elements[j];
				RunReport.PlayerInfo playerInfo2 = this.displayData.runReport.GetPlayerInfo(j);
				CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(playerInfo2.bodyIndex);
				Texture texture = bodyPrefabBodyComponent ? bodyPrefabBodyComponent.portraitIcon : null;
				mpbutton.GetComponentInChildren<RawImage>().texture = texture;
				mpbutton.GetComponent<TooltipProvider>().SetContent(TooltipProvider.GetPlayerNameTooltipContent(playerInfo2.name));
			}
			this.selectedPlayerEffectRoot.transform.SetParent(this.playerNavigationController.buttonAllocator.elements[this.displayData.playerIndex].transform);
			this.selectedPlayerEffectRoot.gameObject.SetActive(false);
			this.selectedPlayerEffectRoot.gameObject.SetActive(true);
			this.selectedPlayerEffectRoot.offsetMin = Vector2.zero;
			this.selectedPlayerEffectRoot.offsetMax = Vector2.zero;
			this.selectedPlayerEffectRoot.localScale = Vector3.one;
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0013820D File Offset: 0x0013640D
		public void SetContinueButtonAction(UnityAction action)
		{
			this.continueButton.onClick.RemoveAllListeners();
			if (action != null)
			{
				this.continueButton.onClick.AddListener(action);
			}
			this.acceptButtonArea.gameObject.SetActive(action != null);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00138248 File Offset: 0x00136448
		private void OnPlayerNavigationControllerPageChangeSubmitted(int newPage)
		{
			GameEndReportPanelController.DisplayData displayData = this.displayData;
			displayData.playerIndex = newPage;
			this.SetDisplayData(displayData);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0013826C File Offset: 0x0013646C
		private void SetPlayerInfo([CanBeNull] RunReport.PlayerInfo playerInfo)
		{
			ulong num = 0UL;
			if (playerInfo != null && playerInfo.statSheet != null)
			{
				StatSheet statSheet = playerInfo.statSheet;
				this.AllocateStatStrips(this.statsToDisplay.Length);
				for (int i = 0; i < this.statsToDisplay.Length; i++)
				{
					string text = this.statsToDisplay[i];
					StatDef statDef = StatDef.Find(text);
					if (statDef == null)
					{
						Debug.LogWarningFormat("GameEndReportPanelController.SetStatSheet: Could not find stat def \"{0}\".", new object[]
						{
							text
						});
					}
					else
					{
						this.AssignStatToStrip(statSheet, statDef, this.statStrips[i]);
						num += statSheet.GetStatPointValue(statDef);
					}
				}
				int unlockableCount = statSheet.GetUnlockableCount();
				int num2 = 0;
				for (int j = 0; j < unlockableCount; j++)
				{
					UnlockableDef unlockable = statSheet.GetUnlockable(j);
					if (unlockable != null && !unlockable.hidden)
					{
						num2++;
					}
				}
				this.AllocateUnlockStrips(num2);
				int num3 = 0;
				for (int k = 0; k < unlockableCount; k++)
				{
					UnlockableDef unlockable2 = statSheet.GetUnlockable(k);
					if (unlockable2 != null && !unlockable2.hidden)
					{
						this.AssignUnlockToStrip(unlockable2, this.unlockStrips[num3]);
						num3++;
					}
				}
				if (this.itemInventoryDisplay && playerInfo.itemAcquisitionOrder != null)
				{
					this.itemInventoryDisplay.SetItems(playerInfo.itemAcquisitionOrder, playerInfo.itemAcquisitionOrder.Length, playerInfo.itemStacks);
					this.itemInventoryDisplay.UpdateDisplay();
				}
				string token = playerInfo.finalMessageToken + "_2P";
				if (Language.IsTokenInvalid(token))
				{
					token = playerInfo.finalMessageToken;
				}
				this.finalMessageLabel.SetText(Language.GetStringFormatted(token, new object[]
				{
					playerInfo.name
				}), true);
			}
			else
			{
				this.AllocateStatStrips(0);
				this.AllocateUnlockStrips(0);
				if (this.itemInventoryDisplay)
				{
					this.itemInventoryDisplay.ResetItems();
				}
				this.finalMessageLabel.SetText(string.Empty, true);
			}
			string @string = Language.GetString("STAT_POINTS_FORMAT");
			this.totalPointsLabel.text = string.Format(@string, TextSerialization.ToStringNumeric(num));
			GameObject gameObject = null;
			if (playerInfo != null)
			{
				gameObject = BodyCatalog.GetBodyPrefab(playerInfo.bodyIndex);
				this.playerUsernameLabel.text = playerInfo.name;
			}
			string arg = "";
			Texture texture = null;
			if (gameObject)
			{
				texture = gameObject.GetComponent<CharacterBody>().portraitIcon;
				arg = Language.GetString(gameObject.GetComponent<CharacterBody>().baseNameToken);
			}
			string string2 = Language.GetString("STAT_CLASS_NAME_FORMAT");
			this.playerBodyLabel.text = string.Format(string2, arg);
			this.playerBodyPortraitImage.texture = texture;
			GameObject gameObject2 = null;
			if (playerInfo != null)
			{
				gameObject2 = BodyCatalog.GetBodyPrefab(playerInfo.killerBodyIndex);
				GameObject gameObject3 = this.killerPanelObject;
				if (gameObject3 != null)
				{
					gameObject3.SetActive(playerInfo.isDead);
				}
			}
			string string3 = Language.GetString("UNIDENTIFIED_KILLER_NAME");
			Texture texture2 = LegacyResourcesAPI.Load<Texture>("Textures/BodyIcons/texUnidentifiedKillerIcon");
			if (gameObject2)
			{
				Texture portraitIcon = gameObject2.GetComponent<CharacterBody>().portraitIcon;
				string baseNameToken = gameObject2.GetComponent<CharacterBody>().baseNameToken;
				if (portraitIcon != null)
				{
					texture2 = portraitIcon;
				}
				if (!Language.IsTokenInvalid(baseNameToken))
				{
					string3 = Language.GetString(gameObject2.GetComponent<CharacterBody>().baseNameToken);
				}
			}
			string string4 = Language.GetString("STAT_KILLER_NAME_FORMAT");
			this.killerBodyLabel.text = string.Format(string4, string3);
			this.killerBodyPortraitImage.texture = texture2;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x001385B8 File Offset: 0x001367B8
		private void AssignStatToStrip([CanBeNull] StatSheet srcStatSheet, [NotNull] StatDef statDef, GameObject destStatStrip)
		{
			string arg = "0";
			ulong value = 0UL;
			if (srcStatSheet != null)
			{
				arg = srcStatSheet.GetStatDisplayValue(statDef);
				value = srcStatSheet.GetStatPointValue(statDef);
			}
			string @string = Language.GetString(statDef.displayToken);
			string text = string.Format(Language.GetString("STAT_NAME_VALUE_FORMAT"), @string, arg);
			destStatStrip.transform.Find("StatNameLabel").GetComponent<TextMeshProUGUI>().text = text;
			string string2 = Language.GetString("STAT_POINTS_FORMAT");
			destStatStrip.transform.Find("PointValueLabel").GetComponent<TextMeshProUGUI>().text = string.Format(string2, TextSerialization.ToStringNumeric(value));
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00138650 File Offset: 0x00136850
		private void AssignUnlockToStrip(UnlockableDef unlockableDef, GameObject destUnlockableStrip)
		{
			AchievementDef achievementDefFromUnlockable = AchievementManager.GetAchievementDefFromUnlockable(unlockableDef.cachedName);
			Texture texture = null;
			string @string = Language.GetString("TOOLTIP_UNLOCK_GENERIC_NAME");
			string string2 = Language.GetString("TOOLTIP_UNLOCK_GENERIC_DESCRIPTION");
			if (unlockableDef.cachedName.Contains("Items."))
			{
				@string = Language.GetString("TOOLTIP_UNLOCK_ITEM_NAME");
				string2 = Language.GetString("TOOLTIP_UNLOCK_ITEM_DESCRIPTION");
			}
			else if (unlockableDef.cachedName.Contains("Logs."))
			{
				@string = Language.GetString("TOOLTIP_UNLOCK_LOG_NAME");
				string2 = Language.GetString("TOOLTIP_UNLOCK_LOG_DESCRIPTION");
			}
			else if (unlockableDef.cachedName.Contains("Characters."))
			{
				@string = Language.GetString("TOOLTIP_UNLOCK_SURVIVOR_NAME");
				string2 = Language.GetString("TOOLTIP_UNLOCK_SURVIVOR_DESCRIPTION");
			}
			else if (unlockableDef.cachedName.Contains("Artifacts."))
			{
				@string = Language.GetString("TOOLTIP_UNLOCK_ARTIFACT_NAME");
				string2 = Language.GetString("TOOLTIP_UNLOCK_ARTIFACT_DESCRIPTION");
			}
			string string3;
			if (achievementDefFromUnlockable != null)
			{
				texture = achievementDefFromUnlockable.GetAchievedIcon().texture;
				string3 = Language.GetString(achievementDefFromUnlockable.nameToken);
			}
			else
			{
				string3 = Language.GetString(unlockableDef.nameToken);
			}
			if (texture != null)
			{
				destUnlockableStrip.transform.Find("IconImage").GetComponent<RawImage>().texture = texture;
			}
			destUnlockableStrip.transform.Find("NameLabel").GetComponent<TextMeshProUGUI>().text = string3;
			destUnlockableStrip.GetComponent<TooltipProvider>().overrideTitleText = @string;
			destUnlockableStrip.GetComponent<TooltipProvider>().overrideBodyText = string2;
		}

		// Token: 0x04004890 RID: 18576
		[Tooltip("The TextMeshProUGUI component to use to display the result of the game: Win or Loss")]
		[Header("Result")]
		public TextMeshProUGUI resultLabel;

		// Token: 0x04004891 RID: 18577
		public Image resultIconBackgroundImage;

		// Token: 0x04004892 RID: 18578
		public Image resultIconForegroundImage;

		// Token: 0x04004893 RID: 18579
		public HGTextMeshProUGUI finalMessageLabel;

		// Token: 0x04004894 RID: 18580
		[Header("Run Settings")]
		[Tooltip("The Image component to use to display the selected difficulty of the run.")]
		public Image selectedDifficultyImage;

		// Token: 0x04004895 RID: 18581
		[Tooltip("The LanguageTextMeshController component to use to display the selected difficulty of the run.")]
		public LanguageTextMeshController selectedDifficultyLabel;

		// Token: 0x04004896 RID: 18582
		[Tooltip("The ArtifactDisplayPanelController component to send the enabled artifacts of the run to.")]
		public ArtifactDisplayPanelController artifactDisplayPanelController;

		// Token: 0x04004897 RID: 18583
		[Tooltip("A list of StatDef names to display in the stats section.")]
		[Header("Stats")]
		public string[] statsToDisplay;

		// Token: 0x04004898 RID: 18584
		[Tooltip("Prefab to be used for stat display.")]
		public GameObject statStripPrefab;

		// Token: 0x04004899 RID: 18585
		[Tooltip("The RectTransform in which to build the stat strips.")]
		public RectTransform statContentArea;

		// Token: 0x0400489A RID: 18586
		[Tooltip("The TextMeshProUGUI component used to display the total points.")]
		public TextMeshProUGUI totalPointsLabel;

		// Token: 0x0400489B RID: 18587
		[Header("Unlocks")]
		[Tooltip("Prefab to be used for unlock display.")]
		public GameObject unlockStripPrefab;

		// Token: 0x0400489C RID: 18588
		[Tooltip("The RectTransform in which to build the unlock strips.")]
		public RectTransform unlockContentArea;

		// Token: 0x0400489D RID: 18589
		[Header("Items")]
		[Tooltip("The inventory display controller.")]
		public ItemInventoryDisplay itemInventoryDisplay;

		// Token: 0x0400489E RID: 18590
		[Header("Player Info")]
		[Tooltip("The RawImage component to use to display the player character's portrait.")]
		public RawImage playerBodyPortraitImage;

		// Token: 0x0400489F RID: 18591
		[Tooltip("The TextMeshProUGUI component to use to display the player character's body name.")]
		public TextMeshProUGUI playerBodyLabel;

		// Token: 0x040048A0 RID: 18592
		[Tooltip("The TextMeshProUGUI component to use to display the player's username.")]
		public TextMeshProUGUI playerUsernameLabel;

		// Token: 0x040048A1 RID: 18593
		[Tooltip("The RawImage component to use to display the killer character's portrait.")]
		[Header("Killer Info")]
		public RawImage killerBodyPortraitImage;

		// Token: 0x040048A2 RID: 18594
		[Tooltip("The TextMeshProUGUI component to use to display the killer character's body name.")]
		public TextMeshProUGUI killerBodyLabel;

		// Token: 0x040048A3 RID: 18595
		[Tooltip("The GameObject used as the panel for the killer information. This is used to disable the killer panel when the player has won the game.")]
		public GameObject killerPanelObject;

		// Token: 0x040048A4 RID: 18596
		[Header("Navigation and Misc")]
		public MPButton continueButton;

		// Token: 0x040048A5 RID: 18597
		public RectTransform chatboxTransform;

		// Token: 0x040048A6 RID: 18598
		public CarouselNavigationController playerNavigationController;

		// Token: 0x040048A7 RID: 18599
		public RectTransform selectedPlayerEffectRoot;

		// Token: 0x040048A8 RID: 18600
		public RectTransform acceptButtonArea;

		// Token: 0x040048A9 RID: 18601
		private readonly List<GameObject> statStrips = new List<GameObject>();

		// Token: 0x040048AA RID: 18602
		private readonly List<GameObject> unlockStrips = new List<GameObject>();

		// Token: 0x02000D01 RID: 3329
		public struct DisplayData : IEquatable<GameEndReportPanelController.DisplayData>
		{
			// Token: 0x06004BD9 RID: 19417 RVA: 0x001387CB File Offset: 0x001369CB
			public bool Equals(GameEndReportPanelController.DisplayData other)
			{
				return object.Equals(this.runReport, other.runReport) && this.playerIndex == other.playerIndex;
			}

			// Token: 0x06004BDA RID: 19418 RVA: 0x001387F0 File Offset: 0x001369F0
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is GameEndReportPanelController.DisplayData)
				{
					GameEndReportPanelController.DisplayData other = (GameEndReportPanelController.DisplayData)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06004BDB RID: 19419 RVA: 0x0013881C File Offset: 0x00136A1C
			public override int GetHashCode()
			{
				return ((-1418150836 * -1521134295 + base.GetHashCode()) * -1521134295 + EqualityComparer<RunReport>.Default.GetHashCode(this.runReport)) * -1521134295 + this.playerIndex.GetHashCode();
			}

			// Token: 0x040048AB RID: 18603
			[CanBeNull]
			public RunReport runReport;

			// Token: 0x040048AC RID: 18604
			public int playerIndex;
		}
	}
}
