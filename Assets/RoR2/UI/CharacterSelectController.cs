using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using Rewired;
using RoR2.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CC9 RID: 3273
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class CharacterSelectController : MonoBehaviour
	{
		// Token: 0x06004A9B RID: 19099 RVA: 0x001322C4 File Offset: 0x001304C4
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.skillStripAllocator = new UIElementAllocator<RectTransform>(this.skillStripContainer, this.skillStripPrefab, true, false);
			this.skillStripFillerAllocator = new UIElementAllocator<RectTransform>(this.skillStripContainer, this.skillStripFillerPrefab, true, false);
			bool active = true;
			this.loadoutHeaderButton.SetActive(active);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00132320 File Offset: 0x00130520
		private void OnEnable()
		{
			UserProfile.onLoadoutChangedGlobal += this.OnLoadoutChangedGlobal;
			this.eventSystemLocator.onEventSystemDiscovered += this.OnEventSystemDiscovered;
			this.eventSystemLocator.onEventSystemLost += this.OnEventSystemLost;
			if (this.eventSystemLocator.eventSystem)
			{
				this.OnEventSystemDiscovered(this.eventSystemLocator.eventSystem);
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00132390 File Offset: 0x00130590
		private void OnDisable()
		{
			this.eventSystemLocator.onEventSystemLost -= this.OnEventSystemLost;
			this.eventSystemLocator.onEventSystemDiscovered -= this.OnEventSystemDiscovered;
			if (this.eventSystemLocator.eventSystem)
			{
				this.OnEventSystemLost(this.eventSystemLocator.eventSystem);
			}
			UserProfile.onLoadoutChangedGlobal -= this.OnLoadoutChangedGlobal;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00132400 File Offset: 0x00130600
		private void Update()
		{
			SurvivorDef survivorDef = null;
			LocalUser localUser = this.localUser;
			NetworkUser networkUser = (localUser != null) ? localUser.currentNetworkUser : null;
			if (networkUser)
			{
				survivorDef = networkUser.GetSurvivorPreference();
			}
			if (this.currentSurvivorDef != survivorDef)
			{
				this.currentSurvivorDef = survivorDef;
				this.shouldRebuild = true;
			}
			if (this.shouldRebuild)
			{
				this.shouldRebuild = false;
				this.RebuildLocal();
			}
			this.UpdateSurvivorInfoPanel();
			if (!RoR2Application.isInSinglePlayer)
			{
				bool flag = this.IsClientReady();
				this.readyButton.gameObject.SetActive(!flag);
				this.unreadyButton.gameObject.SetActive(flag);
			}
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00132496 File Offset: 0x00130696
		private void OnEventSystemDiscovered(MPEventSystem discoveredEventSystem)
		{
			this.eventSystem = discoveredEventSystem;
			this.localUser = (this.eventSystem ? this.eventSystem.localUser : null);
			this.shouldRebuild = true;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x001324C7 File Offset: 0x001306C7
		private void OnEventSystemLost(MPEventSystem lostEventSystem)
		{
			this.eventSystem = null;
			this.localUser = null;
			this.shouldRebuild = true;
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x001324E0 File Offset: 0x001306E0
		private static UnlockableDef[] GenerateLoadoutAssociatedUnlockableDefs()
		{
			CharacterSelectController.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.encounteredUnlockables = new HashSet<UnlockableDef>();
			foreach (SkillFamily skillFamily in SkillCatalog.allSkillFamilies)
			{
				for (int i = 0; i < skillFamily.variants.Length; i++)
				{
					CharacterSelectController.<GenerateLoadoutAssociatedUnlockableDefs>g__TryAddUnlockableByDef|27_0(skillFamily.variants[i].unlockableDef, ref CS$<>8__locals1);
				}
			}
			foreach (CharacterBody characterBody in BodyCatalog.allBodyPrefabBodyBodyComponents)
			{
				SkinDef[] bodySkins = BodyCatalog.GetBodySkins(characterBody.bodyIndex);
				for (int j = 0; j < bodySkins.Length; j++)
				{
					CharacterSelectController.<GenerateLoadoutAssociatedUnlockableDefs>g__TryAddUnlockableByDef|27_0(bodySkins[j].unlockableDef, ref CS$<>8__locals1);
				}
			}
			return CS$<>8__locals1.encounteredUnlockables.ToArray<UnlockableDef>();
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x001325D0 File Offset: 0x001307D0
		private static bool UserHasAnyLoadoutUnlockables(LocalUser localUser)
		{
			if (CharacterSelectController.loadoutAssociatedUnlockableDefs == null)
			{
				CharacterSelectController.loadoutAssociatedUnlockableDefs = CharacterSelectController.GenerateLoadoutAssociatedUnlockableDefs();
			}
			UserProfile userProfile = localUser.userProfile;
			foreach (UnlockableDef unlockableDef in CharacterSelectController.loadoutAssociatedUnlockableDefs)
			{
				if (userProfile.HasUnlockable(unlockableDef))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0013261C File Offset: 0x0013081C
		private void RebuildLocal()
		{
			Loadout loadout = Loadout.RequestInstance();
			try
			{
				LocalUser localUser = this.localUser;
				NetworkUser networkUser = (localUser != null) ? localUser.currentNetworkUser : null;
				SurvivorDef survivorDef = networkUser ? networkUser.GetSurvivorPreference() : null;
				LocalUser localUser2 = this.localUser;
				if (localUser2 != null)
				{
					localUser2.userProfile.CopyLoadout(loadout);
				}
				CharacterSelectController.BodyInfo bodyInfo = new CharacterSelectController.BodyInfo(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorDef ? survivorDef.survivorIndex : SurvivorIndex.None));
				string sourceText = string.Empty;
				string sourceText2 = string.Empty;
				if (survivorDef)
				{
					sourceText = Language.GetString(survivorDef.displayNameToken);
					sourceText2 = Language.GetString(survivorDef.descriptionToken);
				}
				this.survivorName.SetText(sourceText, true);
				this.survivorDescription.SetText(sourceText2, true);
				Image[] array = this.primaryColorImages;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].color = bodyInfo.bodyColor;
				}
				TextMeshProUGUI[] array2 = this.primaryColorTexts;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].color = bodyInfo.bodyColor;
				}
				List<CharacterSelectController.StripDisplayData> list = new List<CharacterSelectController.StripDisplayData>();
				this.BuildSkillStripDisplayData(loadout, bodyInfo, list);
				this.skillStripFillerAllocator.AllocateElements(0);
				this.skillStripAllocator.AllocateElements(list.Count);
				for (int j = 0; j < list.Count; j++)
				{
					this.RebuildStrip(this.skillStripAllocator.elements[j], list[j]);
				}
				this.skillStripFillerAllocator.AllocateElements(Mathf.Max(0, 5 - list.Count));
				string viewableName = string.Empty;
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				stringBuilder.Append("/Loadout/Bodies/").Append(BodyCatalog.GetBodyName(bodyInfo.bodyIndex)).Append("/");
				viewableName = stringBuilder.ToString();
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				this.loadoutViewableTag.viewableName = viewableName;
				bool active = !RoR2Application.isInSinglePlayer;
				GameObject[] array3 = this.multiplayerOnlyObjects;
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i].SetActive(active);
				}
			}
			finally
			{
				Loadout.ReturnInstance(loadout);
			}
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00132850 File Offset: 0x00130A50
		private void RebuildStrip(RectTransform skillStrip, CharacterSelectController.StripDisplayData stripDisplayData)
		{
			GameObject gameObject = skillStrip.gameObject;
			Image component = skillStrip.Find("Inner/Icon").GetComponent<Image>();
			HGTextMeshProUGUI component2 = skillStrip.Find("Inner/SkillDescriptionPanel/SkillName").GetComponent<HGTextMeshProUGUI>();
			HGTextMeshProUGUI component3 = skillStrip.Find("Inner/SkillDescriptionPanel/SkillDescription").GetComponent<HGTextMeshProUGUI>();
			HGButton component4 = skillStrip.gameObject.GetComponent<HGButton>();
			if (stripDisplayData.enabled)
			{
				gameObject.SetActive(true);
				component.sprite = stripDisplayData.icon;
				component2.SetText(stripDisplayData.titleString, true);
				component2.color = stripDisplayData.primaryColor;
				component3.SetText(stripDisplayData.descriptionString, true);
				component4.hoverToken = stripDisplayData.keywordString;
				return;
			}
			gameObject.SetActive(false);
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001328FC File Offset: 0x00130AFC
		private void BuildSkillStripDisplayData(Loadout loadout, in CharacterSelectController.BodyInfo bodyInfo, List<CharacterSelectController.StripDisplayData> dest)
		{
			if (!bodyInfo.bodyPrefab || !bodyInfo.bodyPrefabBodyComponent || !bodyInfo.skillLocator)
			{
				return;
			}
			BodyIndex bodyIndex = bodyInfo.bodyIndex;
			SkillLocator skillLocator = bodyInfo.skillLocator;
			GenericSkill[] skillSlots = bodyInfo.skillSlots;
			Color bodyColor = bodyInfo.bodyColor;
			if (skillLocator.passiveSkill.enabled)
			{
				CharacterSelectController.StripDisplayData item = new CharacterSelectController.StripDisplayData
				{
					enabled = true,
					primaryColor = bodyColor,
					icon = skillLocator.passiveSkill.icon,
					titleString = Language.GetString(skillLocator.passiveSkill.skillNameToken),
					descriptionString = Language.GetString(skillLocator.passiveSkill.skillDescriptionToken),
					keywordString = (string.IsNullOrEmpty(skillLocator.passiveSkill.keywordToken) ? "" : Language.GetString(skillLocator.passiveSkill.keywordToken)),
					actionName = ""
				};
				dest.Add(item);
			}
			for (int i = 0; i < skillSlots.Length; i++)
			{
				GenericSkill genericSkill = skillSlots[i];
				if (!genericSkill.hideInCharacterSelect)
				{
					uint skillVariant = loadout.bodyLoadoutManager.GetSkillVariant(bodyIndex, i);
					SkillDef skillDef = genericSkill.skillFamily.variants[(int)skillVariant].skillDef;
					string actionName = "";
					switch (skillLocator.FindSkillSlot(genericSkill))
					{
					case SkillSlot.Primary:
						actionName = "PrimarySkill";
						break;
					case SkillSlot.Secondary:
						actionName = "SecondarySkill";
						break;
					case SkillSlot.Utility:
						actionName = "UtilitySkill";
						break;
					case SkillSlot.Special:
						actionName = "SpecialSkill";
						break;
					}
					string keywordString = string.Empty;
					if (skillDef.keywordTokens != null)
					{
						StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
						for (int j = 0; j < skillDef.keywordTokens.Length; j++)
						{
							string @string = Language.GetString(skillDef.keywordTokens[j]);
							stringBuilder.Append(@string).Append("\n\n");
						}
						keywordString = stringBuilder.ToString();
						stringBuilder = HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
					}
					CharacterSelectController.StripDisplayData item = new CharacterSelectController.StripDisplayData
					{
						enabled = true,
						primaryColor = bodyColor,
						icon = skillDef.icon,
						titleString = Language.GetString(skillDef.skillNameToken),
						descriptionString = Language.GetString(skillDef.skillDescriptionToken),
						keywordString = keywordString,
						actionName = actionName
					};
					dest.Add(item);
				}
			}
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x00132B65 File Offset: 0x00130D65
		private void OnLoadoutChangedGlobal(UserProfile userProfile)
		{
			LocalUser localUser = this.localUser;
			if (userProfile == ((localUser != null) ? localUser.userProfile : null))
			{
				this.shouldRebuild = true;
			}
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00132B84 File Offset: 0x00130D84
		private void UpdateSurvivorInfoPanel()
		{
			if (this.eventSystem && this.eventSystem.currentInputSource == MPEventSystem.InputSource.MouseAndKeyboard)
			{
				this.shouldShowSurvivorInfoPanel = true;
			}
			this.activeSurvivorInfoPanel.SetActive(this.shouldShowSurvivorInfoPanel);
			this.inactiveSurvivorInfoPanel.SetActive(!this.shouldShowSurvivorInfoPanel);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00132BD7 File Offset: 0x00130DD7
		public void SetSurvivorInfoPanelActive(bool active)
		{
			this.shouldShowSurvivorInfoPanel = active;
			this.UpdateSurvivorInfoPanel();
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00132BE8 File Offset: 0x00130DE8
		private static bool InputPlayerIsAssigned(Player inputPlayer)
		{
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				if (readOnlyInstancesList[i].inputPlayer == inputPlayer)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00132C20 File Offset: 0x00130E20
		public bool IsClientReady()
		{
			int num = 0;
			if (!PreGameController.instance)
			{
				return false;
			}
			VoteController component = PreGameController.instance.GetComponent<VoteController>();
			if (!component)
			{
				return false;
			}
			int i = 0;
			int voteCount = component.GetVoteCount();
			while (i < voteCount)
			{
				UserVote vote = component.GetVote(i);
				if (vote.networkUserObject && vote.receivedVote)
				{
					NetworkUser component2 = vote.networkUserObject.GetComponent<NetworkUser>();
					if (component2 && component2.isLocalPlayer)
					{
						num++;
					}
				}
				i++;
			}
			return num == NetworkUser.readOnlyLocalPlayersList.Count;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00132CB8 File Offset: 0x00130EB8
		public void ClientSetReady()
		{
			foreach (NetworkUser networkUser in NetworkUser.readOnlyLocalPlayersList)
			{
				if (networkUser)
				{
					networkUser.CallCmdSubmitVote(PreGameController.instance.gameObject, 0);
				}
				else
				{
					Debug.Log("Null network user in readonly local player list!!");
				}
			}
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00132D24 File Offset: 0x00130F24
		public void ClientSetUnready()
		{
			foreach (NetworkUser networkUser in NetworkUser.readOnlyLocalPlayersList)
			{
				networkUser.CallCmdSubmitVote(PreGameController.instance.gameObject, -1);
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00132D87 File Offset: 0x00130F87
		[CompilerGenerated]
		internal static void <GenerateLoadoutAssociatedUnlockableDefs>g__TryAddUnlockableByDef|27_0(UnlockableDef unlockableDef, ref CharacterSelectController.<>c__DisplayClass27_0 A_1)
		{
			if (unlockableDef != null)
			{
				A_1.encounteredUnlockables.Add(unlockableDef);
			}
		}

		// Token: 0x04004748 RID: 18248
		[Header("Survivor Panel")]
		public TextMeshProUGUI survivorName;

		// Token: 0x04004749 RID: 18249
		public Image[] primaryColorImages;

		// Token: 0x0400474A RID: 18250
		public TextMeshProUGUI[] primaryColorTexts;

		// Token: 0x0400474B RID: 18251
		public GameObject activeSurvivorInfoPanel;

		// Token: 0x0400474C RID: 18252
		public GameObject inactiveSurvivorInfoPanel;

		// Token: 0x0400474D RID: 18253
		[Header("Overview Panel")]
		public TextMeshProUGUI survivorDescription;

		// Token: 0x0400474E RID: 18254
		[Header("Skill Panel")]
		public GameObject skillStripPrefab;

		// Token: 0x0400474F RID: 18255
		public GameObject skillStripFillerPrefab;

		// Token: 0x04004750 RID: 18256
		public RectTransform skillStripContainer;

		// Token: 0x04004751 RID: 18257
		private UIElementAllocator<RectTransform> skillStripAllocator;

		// Token: 0x04004752 RID: 18258
		private UIElementAllocator<RectTransform> skillStripFillerAllocator;

		// Token: 0x04004753 RID: 18259
		[Tooltip("The header button for the loadout tab. Will be disabled if the user has no unlocked loadout options.")]
		[Header("Loadout Panel")]
		public GameObject loadoutHeaderButton;

		// Token: 0x04004754 RID: 18260
		public ViewableTag loadoutViewableTag;

		// Token: 0x04004755 RID: 18261
		[Header("Ready and Misc")]
		public MPButton readyButton;

		// Token: 0x04004756 RID: 18262
		public MPButton unreadyButton;

		// Token: 0x04004757 RID: 18263
		public GameObject[] multiplayerOnlyObjects;

		// Token: 0x04004758 RID: 18264
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004759 RID: 18265
		private MPEventSystem eventSystem;

		// Token: 0x0400475A RID: 18266
		private LocalUser localUser;

		// Token: 0x0400475B RID: 18267
		private SurvivorDef currentSurvivorDef;

		// Token: 0x0400475C RID: 18268
		private static UnlockableDef[] loadoutAssociatedUnlockableDefs;

		// Token: 0x0400475D RID: 18269
		private bool shouldRebuild = true;

		// Token: 0x0400475E RID: 18270
		private bool shouldShowSurvivorInfoPanel;

		// Token: 0x02000CCA RID: 3274
		[Serializable]
		public struct SkillStrip
		{
			// Token: 0x0400475F RID: 18271
			public GameObject stripRoot;

			// Token: 0x04004760 RID: 18272
			public Image skillIcon;

			// Token: 0x04004761 RID: 18273
			public TextMeshProUGUI skillName;

			// Token: 0x04004762 RID: 18274
			public TextMeshProUGUI skillDescription;
		}

		// Token: 0x02000CCB RID: 3275
		private struct BodyInfo
		{
			// Token: 0x06004AAF RID: 19119 RVA: 0x00132DA0 File Offset: 0x00130FA0
			public BodyInfo(BodyIndex bodyIndex)
			{
				this.bodyIndex = bodyIndex;
				this.bodyPrefab = BodyCatalog.GetBodyPrefab(bodyIndex);
				this.bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndex);
				this.bodyColor = (this.bodyPrefabBodyComponent ? this.bodyPrefabBodyComponent.bodyColor : Color.gray);
				this.skillLocator = (this.bodyPrefab ? this.bodyPrefab.GetComponent<SkillLocator>() : null);
				this.skillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndex);
			}

			// Token: 0x04004763 RID: 18275
			public readonly BodyIndex bodyIndex;

			// Token: 0x04004764 RID: 18276
			public readonly GameObject bodyPrefab;

			// Token: 0x04004765 RID: 18277
			public readonly CharacterBody bodyPrefabBodyComponent;

			// Token: 0x04004766 RID: 18278
			public readonly Color bodyColor;

			// Token: 0x04004767 RID: 18279
			public readonly SkillLocator skillLocator;

			// Token: 0x04004768 RID: 18280
			public readonly GenericSkill[] skillSlots;
		}

		// Token: 0x02000CCC RID: 3276
		private struct StripDisplayData
		{
			// Token: 0x04004769 RID: 18281
			public bool enabled;

			// Token: 0x0400476A RID: 18282
			public Color primaryColor;

			// Token: 0x0400476B RID: 18283
			public Sprite icon;

			// Token: 0x0400476C RID: 18284
			public string titleString;

			// Token: 0x0400476D RID: 18285
			public string descriptionString;

			// Token: 0x0400476E RID: 18286
			public string keywordString;

			// Token: 0x0400476F RID: 18287
			public string actionName;
		}
	}
}
