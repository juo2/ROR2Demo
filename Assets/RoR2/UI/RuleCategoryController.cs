using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D76 RID: 3446
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class RuleCategoryController : MonoBehaviour
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00146662 File Offset: 0x00144862
		public RectTransform popoutPanelContentContainer
		{
			get
			{
				return this.popoutPanelInstance.popoutPanelContentContainer;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x0014666F File Offset: 0x0014486F
		public LanguageTextMeshController popoutPanelTitleText
		{
			get
			{
				return this.popoutPanelInstance.popoutPanelTitleText;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x0014667C File Offset: 0x0014487C
		public LanguageTextMeshController popoutPanelSubtitleText
		{
			get
			{
				return this.popoutPanelInstance.popoutPanelSubtitleText;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00146689 File Offset: 0x00144889
		public LanguageTextMeshController popoutPanelDescriptionText
		{
			get
			{
				return this.popoutPanelInstance.popoutPanelDescriptionText;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x00146696 File Offset: 0x00144896
		public bool shouldHide
		{
			get
			{
				return (this.isEmpty && !this.tipObject) || this.currentCategory == null || this.currentCategory.isHidden;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x001466C2 File Offset: 0x001448C2
		public bool isEmpty
		{
			get
			{
				return this.voteStripAllocator.elements.Count == 0 && this.voteResultIconAllocator.elements.Count == 0;
			}
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x001466EC File Offset: 0x001448EC
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.cachedAvailability = new RuleChoiceMask();
			if (this.popoutPanelPrefab && this.popoutPanelContainer && this.editCategoryButtonObject)
			{
				this.popoutPanelInstance = UnityEngine.Object.Instantiate<GameObject>(this.popoutPanelPrefab, this.popoutPanelContainer).GetComponent<HGPopoutPanel>();
				ChildLocator component = this.popoutPanelInstance.GetComponent<ChildLocator>();
				GameObject gameObject;
				if (component == null)
				{
					gameObject = null;
				}
				else
				{
					Transform transform = component.FindChild("RandomButtonContainer");
					gameObject = ((transform != null) ? transform.gameObject : null);
				}
				this.popoutRandomButtonContainer = gameObject;
				MPButton mpbutton;
				if (component == null)
				{
					mpbutton = null;
				}
				else
				{
					Transform transform2 = component.FindChild("RandomButton");
					mpbutton = ((transform2 != null) ? transform2.GetComponent<HGButton>() : null);
				}
				this.popoutRandomButton = mpbutton;
				if (this.popoutRandomButton)
				{
					this.popoutRandomButton.onClick.AddListener(new UnityAction(this.SetRandomVotes));
				}
				this.editCategoryButtonObject.GetComponent<HGButton>().onClick.AddListener(new UnityAction(this.TogglePopoutPanel));
			}
			this.voteStripAllocator = new UIElementAllocator<RectTransform>(this.stripContainer, this.stripPrefab.gameObject, true, false);
			this.voteResultIconAllocator = new UIElementAllocator<RuleChoiceController>(this.voteResultGridContainer, this.voteResultIconPrefab.gameObject, true, false);
			this.popoutButtonIconAllocator = new UIElementAllocator<RuleChoiceController>(this.popoutPanelContentContainer, this.popoutPanelIconPrefab.gameObject, true, false);
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00146852 File Offset: 0x00144A52
		private void TogglePopoutPanel()
		{
			if (this.popoutPanelInstance)
			{
				this.popoutPanelInstance.gameObject.SetActive(!this.popoutPanelInstance.gameObject.activeSelf);
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x00146884 File Offset: 0x00144A84
		public void SetRandomVotes()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			NetworkUser networkUser;
			if (eventSystem == null)
			{
				networkUser = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				networkUser = ((localUser != null) ? localUser.currentNetworkUser : null);
			}
			PreGameRuleVoteController preGameRuleVoteController = PreGameRuleVoteController.FindForUser(networkUser);
			if (!preGameRuleVoteController)
			{
				return;
			}
			List<RuleChoiceDef> list = new List<RuleChoiceDef>();
			foreach (RuleDef ruleDef in this.currentCategory.children)
			{
				list.Clear();
				foreach (RuleChoiceDef ruleChoiceDef in ruleDef.choices)
				{
					if (this.cachedAvailability[ruleChoiceDef.globalIndex])
					{
						list.Add(ruleChoiceDef);
					}
				}
				int choiceValue = -1;
				if (list.Count > 0 && UnityEngine.Random.value > 0.5f)
				{
					choiceValue = list[UnityEngine.Random.Range(0, list.Count)].localIndex;
				}
				preGameRuleVoteController.SetVote(ruleDef.globalIndex, choiceValue);
			}
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x001469B4 File Offset: 0x00144BB4
		private void SetTip(string tipToken)
		{
			if (tipToken == null)
			{
				UnityEngine.Object.Destroy(this.tipObject);
				this.tipObject = null;
				return;
			}
			this.stripContainer.gameObject.SetActive(false);
			this.voteResultGridContainer.gameObject.SetActive(false);
			if (!this.tipObject)
			{
				this.tipObject = UnityEngine.Object.Instantiate<GameObject>(this.tipPrefab, this.tipContainer);
				this.tipObject.SetActive(true);
			}
			this.tipObject.GetComponentInChildren<LanguageTextMeshController>().token = tipToken;
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x00146A3A File Offset: 0x00144C3A
		private void AllocateStrips(int desiredCount)
		{
			this.voteStripAllocator.AllocateElements(desiredCount);
			this.framePanel.SetAsLastSibling();
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x00146A53 File Offset: 0x00144C53
		private void AllocateResultIcons(int desiredCount)
		{
			this.voteResultIconAllocator.AllocateElements(desiredCount);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x00146A64 File Offset: 0x00144C64
		public void SetData(RuleCategoryDef categoryDef, RuleChoiceMask availability, RuleBook ruleBook)
		{
			this.currentCategory = categoryDef;
			this.ruleCategoryType = categoryDef.ruleCategoryType;
			this.cachedAvailability.Copy(availability);
			this.rulesToDisplay.Clear();
			bool active = false;
			List<RuleDef> children = categoryDef.children;
			for (int i = 0; i < children.Count; i++)
			{
				RuleDef ruleDef = children[i];
				bool flag = false;
				int num = ruleDef.AvailableChoiceCount(availability);
				if (!availability[ruleDef.choices[ruleDef.defaultChoiceIndex].globalIndex] && num != 0)
				{
					flag = true;
				}
				if (num > 1)
				{
					flag = true;
					active = true;
				}
				if (ruleDef.globalName == "Difficulty")
				{
					flag = true;
				}
				flag = (flag || ruleDef.forceLobbyDisplay);
				if (flag)
				{
					this.rulesToDisplay.Add(children[i]);
				}
			}
			Image[] array = this.headerColorImages;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].color = categoryDef.color;
			}
			this.categoryHeaderLanguageController.token = categoryDef.displayToken;
			RuleCatalog.RuleCategoryType ruleCategoryType = this.ruleCategoryType;
			if (ruleCategoryType != RuleCatalog.RuleCategoryType.StripVote)
			{
				if (ruleCategoryType == RuleCatalog.RuleCategoryType.VoteResultGrid)
				{
					this.stripContainer.gameObject.SetActive(false);
					this.voteResultGridContainer.gameObject.SetActive(true);
					Color color = categoryDef.color;
					color.a = 0.2f;
					this.editCategoryButtonObject.SetActive(true);
					this.editCategoryButtonObject.GetComponent<HGButton>().hoverToken = Language.GetStringFormatted("RULE_EDIT_FORMAT", new object[]
					{
						Language.GetString(categoryDef.displayToken),
						Language.GetString(categoryDef.editToken),
						ColorUtility.ToHtmlStringRGBA(color)
					});
					int count = this.rulesToDisplay.Count;
					this.AllocateResultIcons(count);
					for (int k = 0; k < this.rulesToDisplay.Count; k++)
					{
						RuleDef ruleDef2 = this.rulesToDisplay[k];
						RuleChoiceController ruleChoiceController = this.voteResultIconAllocator.elements[k];
						int ruleChoiceIndex = ruleBook.GetRuleChoiceIndex(ruleDef2);
						RuleChoiceDef choice = ruleDef2.choices[ruleChoiceIndex];
						ruleChoiceController.SetChoice(choice);
					}
					this.popoutPanelTitleText.token = categoryDef.displayToken;
					this.popoutPanelSubtitleText.token = categoryDef.subtitleToken;
					this.popoutButtonIconAllocator.AllocateElements(this.rulesToDisplay.Count);
					for (int l = 0; l < this.rulesToDisplay.Count; l++)
					{
						RuleDef ruleDef3 = this.rulesToDisplay[l];
						bool flag2 = ruleDef3.choices.Count == 2;
						bool flag3 = ruleDef3.AvailableChoiceCount(availability) > 1;
						int ruleChoiceIndex2 = ruleBook.GetRuleChoiceIndex(ruleDef3);
						RuleChoiceDef choice2 = ruleDef3.choices[ruleChoiceIndex2];
						RuleChoiceController ruleChoiceController2 = this.popoutButtonIconAllocator.elements[l];
						ruleChoiceController2.displayVoteCounter = false;
						ruleChoiceController2.SetChoice(choice2);
						ruleChoiceController2.cycleThroughOptions = true;
						ruleChoiceController2.requiredTopLayer = this.popoutPanelInstance.GetComponent<UILayerKey>();
						ruleChoiceController2.tooltipProvider.enabled = false;
						ruleChoiceController2.hgButton.updateTextOnHover = true;
						ruleChoiceController2.hgButton.hoverLanguageTextMeshController = this.popoutPanelDescriptionText;
						if (flag2 && flag3)
						{
							ruleChoiceController2.canVote = true;
						}
						else
						{
							ruleChoiceController2.canVote = false;
						}
					}
				}
			}
			else
			{
				this.stripContainer.gameObject.SetActive(true);
				this.voteResultGridContainer.gameObject.SetActive(false);
				this.editCategoryButtonObject.SetActive(false);
				this.AllocateStrips(this.rulesToDisplay.Count);
				List<RuleChoiceDef> list = new List<RuleChoiceDef>();
				for (int m = 0; m < this.rulesToDisplay.Count; m++)
				{
					RuleDef ruleDef4 = this.rulesToDisplay[m];
					list.Clear();
					foreach (RuleChoiceDef ruleChoiceDef in ruleDef4.choices)
					{
						if (availability[ruleChoiceDef.globalIndex])
						{
							list.Add(ruleChoiceDef);
						}
					}
					this.voteStripAllocator.elements[m].GetComponent<RuleBookViewerStrip>().SetData(list, ruleBook.GetRuleChoiceIndex(ruleDef4));
				}
			}
			this.SetTip(this.isEmpty ? categoryDef.emptyTipToken : null);
			if (this.popoutRandomButtonContainer)
			{
				this.popoutRandomButtonContainer.SetActive(active);
			}
		}

		// Token: 0x04004BA1 RID: 19361
		[Header("Header")]
		public Image[] headerColorImages;

		// Token: 0x04004BA2 RID: 19362
		public LanguageTextMeshController categoryHeaderLanguageController;

		// Token: 0x04004BA3 RID: 19363
		public GameObject tipPrefab;

		// Token: 0x04004BA4 RID: 19364
		public RectTransform tipContainer;

		// Token: 0x04004BA5 RID: 19365
		public GameObject editCategoryButtonObject;

		// Token: 0x04004BA6 RID: 19366
		[Header("Rules, Strip")]
		public GameObject stripPrefab;

		// Token: 0x04004BA7 RID: 19367
		public RectTransform stripContainer;

		// Token: 0x04004BA8 RID: 19368
		public RectTransform framePanel;

		// Token: 0x04004BA9 RID: 19369
		[Header("Rules, Grid +  Popout Panel")]
		public RectTransform voteResultGridContainer;

		// Token: 0x04004BAA RID: 19370
		public RectTransform voteResultIconPrefab;

		// Token: 0x04004BAB RID: 19371
		public RectTransform popoutPanelIconPrefab;

		// Token: 0x04004BAC RID: 19372
		public GameObject popoutPanelPrefab;

		// Token: 0x04004BAD RID: 19373
		public RectTransform popoutPanelContainer;

		// Token: 0x04004BAE RID: 19374
		public bool displayOnlyNonDefaultResults;

		// Token: 0x04004BAF RID: 19375
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004BB0 RID: 19376
		private readonly List<RuleDef> rulesToDisplay = new List<RuleDef>(RuleCatalog.ruleCount);

		// Token: 0x04004BB1 RID: 19377
		private RuleCatalog.RuleCategoryType ruleCategoryType;

		// Token: 0x04004BB2 RID: 19378
		private GameObject tipObject;

		// Token: 0x04004BB3 RID: 19379
		private HGPopoutPanel popoutPanelInstance;

		// Token: 0x04004BB4 RID: 19380
		private UIElementAllocator<RectTransform> voteStripAllocator;

		// Token: 0x04004BB5 RID: 19381
		private UIElementAllocator<RuleChoiceController> voteResultIconAllocator;

		// Token: 0x04004BB6 RID: 19382
		private UIElementAllocator<RuleChoiceController> popoutButtonIconAllocator;

		// Token: 0x04004BB7 RID: 19383
		private GameObject popoutRandomButtonContainer;

		// Token: 0x04004BB8 RID: 19384
		private MPButton popoutRandomButton;

		// Token: 0x04004BB9 RID: 19385
		private RuleChoiceMask cachedAvailability;

		// Token: 0x04004BBA RID: 19386
		private RuleCategoryDef currentCategory;
	}
}
