using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D77 RID: 3447
	public class RuleChoiceController : MonoBehaviour
	{
		// Token: 0x06004F06 RID: 20230 RVA: 0x00146EF0 File Offset: 0x001450F0
		private void OnEnable()
		{
			RuleChoiceController.instancesList.Add(this);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00146EFD File Offset: 0x001450FD
		private void OnDisable()
		{
			RuleChoiceController.instancesList.Remove(this);
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00146F0B File Offset: 0x0014510B
		static RuleChoiceController()
		{
			PreGameRuleVoteController.onVotesUpdated += delegate()
			{
				foreach (RuleChoiceController ruleChoiceController in RuleChoiceController.instancesList)
				{
					ruleChoiceController.UpdateFromVotes();
				}
			};
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x00146F2C File Offset: 0x0014512C
		private void Start()
		{
			this.UpdateFromVotes();
			this.UpdateChoiceDisplay(this.choiceDef);
			if (this.requiredTopLayer && this.hgButton)
			{
				this.hgButton.requiredTopLayer = this.requiredTopLayer;
			}
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x00146F6C File Offset: 0x0014516C
		public void UpdateFromVotes()
		{
			int num = PreGameRuleVoteController.votesForEachChoice[this.choiceDef.globalIndex];
			bool isInSinglePlayer = RoR2Application.isInSinglePlayer;
			if (this.voteCounter)
			{
				if (this.displayVoteCounter && num > 0 && !isInSinglePlayer)
				{
					this.voteCounter.enabled = true;
					this.voteCounter.text = num.ToString();
				}
				else
				{
					this.voteCounter.enabled = false;
				}
			}
			bool flag = false;
			NetworkUser networkUser = this.FindNetworkUser();
			if (networkUser)
			{
				PreGameRuleVoteController preGameRuleVoteController = PreGameRuleVoteController.FindForUser(networkUser);
				if (preGameRuleVoteController)
				{
					flag = preGameRuleVoteController.IsChoiceVoted(this.choiceDef);
				}
			}
			bool flag2 = this.choiceDef.globalName.Contains(".Off");
			if (this.chosenDisplayObject)
			{
				this.chosenDisplayObject.SetActive(flag && !flag2);
			}
			if (this.disabledDisplayObject)
			{
				this.disabledDisplayObject.SetActive(flag && flag2);
			}
			if (this.cantVoteDisplayObject)
			{
				this.cantVoteDisplayObject.SetActive(!this.canVote);
			}
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00147080 File Offset: 0x00145280
		public void SetChoice([NotNull] RuleChoiceDef newChoiceDef)
		{
			if (newChoiceDef == this.choiceDef)
			{
				return;
			}
			this.choiceDef = newChoiceDef;
			this.UpdateChoiceDisplay(this.choiceDef);
			this.UpdateFromVotes();
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x001470A8 File Offset: 0x001452A8
		private void UpdateChoiceDisplay(RuleChoiceDef displayChoiceDef)
		{
			base.gameObject.name = "Choice (" + displayChoiceDef.globalName + ")";
			this.image.sprite = displayChoiceDef.sprite;
			if (this.tooltipProvider)
			{
				if (displayChoiceDef.tooltipNameToken == null)
				{
					Debug.LogErrorFormat("Rule choice {0} .tooltipNameToken is null", new object[]
					{
						displayChoiceDef.globalName
					});
				}
				if (displayChoiceDef.tooltipBodyToken == null)
				{
					Debug.LogErrorFormat("Rule choice {0} .tooltipBodyToken is null", new object[]
					{
						displayChoiceDef.tooltipBodyToken
					});
				}
				this.tooltipProvider.overrideTitleText = displayChoiceDef.getTooltipName(displayChoiceDef);
				this.tooltipProvider.titleColor = displayChoiceDef.tooltipNameColor;
				this.tooltipProvider.bodyToken = displayChoiceDef.tooltipBodyToken;
				this.tooltipProvider.bodyColor = displayChoiceDef.tooltipBodyColor;
			}
			if (this.hgButton)
			{
				if (this.hgButton.updateTextOnHover && this.hgButton.hoverLanguageTextMeshController)
				{
					string token = "RULE_DESCRIPTION_FORMAT";
					string @string = Language.GetString(displayChoiceDef.tooltipNameToken);
					string string2 = Language.GetString(displayChoiceDef.tooltipBodyToken);
					Color tooltipNameColor = displayChoiceDef.tooltipNameColor;
					tooltipNameColor.a = 0.2f;
					string stringFormatted = Language.GetStringFormatted(token, new object[]
					{
						@string,
						string2,
						ColorUtility.ToHtmlStringRGBA(tooltipNameColor)
					});
					this.hgButton.hoverToken = stringFormatted;
					this.hgButton.hoverLanguageTextMeshController.token = stringFormatted;
				}
				this.hgButton.uiClickSoundOverride = this.choiceDef.selectionUISound;
			}
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00147231 File Offset: 0x00145431
		private NetworkUser FindNetworkUser()
		{
			// LocalUser localUser = ((MPEventSystem)EventSystem.current).localUser;
			LocalUser localUser = GameObject.Find("MPEventSystem Player0").GetComponent<MPEventSystem>().localUser;
			if (localUser == null)
			{
				return null;
			}
			return localUser.currentNetworkUser;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x00147250 File Offset: 0x00145450
		public void OnClick()
		{
			if (!this.canVote)
			{
				return;
			}
			NetworkUser networkUser = this.FindNetworkUser();
			Debug.Log(networkUser);
			if (networkUser)
			{
				PreGameRuleVoteController preGameRuleVoteController = PreGameRuleVoteController.FindForUser(networkUser);
				if (preGameRuleVoteController)
				{
					RuleChoiceDef ruleChoiceDef2;
					RuleChoiceDef ruleChoiceDef = ruleChoiceDef2 = this.choiceDef;
					RuleDef ruleDef = ruleChoiceDef.ruleDef;
					int count = ruleDef.choices.Count;
					int num = ruleChoiceDef.localIndex;
					bool flag = false;
					Debug.LogFormat("maxRuleCount={0}, currentChoiceIndex={1}", new object[]
					{
						count,
						num
					});
					if (this.cycleThroughOptions)
					{
						if (preGameRuleVoteController.IsChoiceVoted(this.choiceDef))
						{
							num++;
						}
						else
						{
							num = 0;
						}
						if (num > count - 1)
						{
							num = ruleDef.defaultChoiceIndex;
							flag = true;
						}
						ruleChoiceDef2 = ruleDef.choices[num];
					}
					else if (preGameRuleVoteController.IsChoiceVoted(this.choiceDef))
					{
						flag = true;
					}
					this.SetChoice(ruleChoiceDef2);
					preGameRuleVoteController.SetVote(ruleChoiceDef2.ruleDef.globalIndex, flag ? -1 : ruleChoiceDef2.localIndex);
					return;
				}
				Debug.Log("voteController=null");
			}
		}

		// Token: 0x04004BBB RID: 19387
		private static readonly List<RuleChoiceController> instancesList = new List<RuleChoiceController>();

		// Token: 0x04004BBC RID: 19388
		[HideInInspector]
		public RuleBookViewerStrip strip;

		// Token: 0x04004BBD RID: 19389
		public HGButton hgButton;

		// Token: 0x04004BBE RID: 19390
		public Image image;

		// Token: 0x04004BBF RID: 19391
		public TooltipProvider tooltipProvider;

		// Token: 0x04004BC0 RID: 19392
		public TextMeshProUGUI voteCounter;

		// Token: 0x04004BC1 RID: 19393
		public GameObject chosenDisplayObject;

		// Token: 0x04004BC2 RID: 19394
		public GameObject disabledDisplayObject;

		// Token: 0x04004BC3 RID: 19395
		public GameObject cantVoteDisplayObject;

		// Token: 0x04004BC4 RID: 19396
		public UILayerKey requiredTopLayer;

		// Token: 0x04004BC5 RID: 19397
		public bool displayVoteCounter = true;

		// Token: 0x04004BC6 RID: 19398
		public bool canVote;

		// Token: 0x04004BC7 RID: 19399
		public bool cycleThroughOptions;

		// Token: 0x04004BC8 RID: 19400
		private RuleChoiceDef choiceDef;
	}
}
