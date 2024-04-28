using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000DB2 RID: 3506
	public class VoteInfoPanelController : MonoBehaviour
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x0014C335 File Offset: 0x0014A535
		private bool votesArePossible
		{
			get
			{
				return RoR2Application.isInMultiPlayer;
			}
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0014C33C File Offset: 0x0014A53C
		private void Awake()
		{
			if (!this.votesArePossible)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0014C354 File Offset: 0x0014A554
		private void AllocateIndicators(int desiredIndicatorCount)
		{
			while (this.indicators.Count > desiredIndicatorCount)
			{
				int index = this.indicators.Count - 1;
				UnityEngine.Object.Destroy(this.indicators[index].gameObject);
				this.indicators.RemoveAt(index);
			}
			while (this.indicators.Count < desiredIndicatorCount)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.indicatorPrefab, this.container);
				gameObject.SetActive(true);
				this.indicators.Add(new VoteInfoPanelController.IndicatorInfo
				{
					gameObject = gameObject,
					image = gameObject.GetComponentInChildren<Image>(),
					tooltipProvider = gameObject.GetComponentInChildren<TooltipProvider>()
				});
			}
			this.timerPanelObject.transform.SetAsLastSibling();
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x0014C410 File Offset: 0x0014A610
		public void UpdateElements()
		{
			int num = 0;
			if (this.voteController)
			{
				num = this.voteController.GetVoteCount();
			}
			this.AllocateIndicators(num);
			for (int i = 0; i < num; i++)
			{
				UserVote vote = this.voteController.GetVote(i);
				this.indicators[i].image.sprite = (vote.receivedVote ? this.hasVotedSprite : this.hasNotVotedSprite);
				string userName;
				if (vote.networkUserObject)
				{
					NetworkUser component = vote.networkUserObject.GetComponent<NetworkUser>();
					if (component)
					{
						userName = component.GetNetworkPlayerName().GetResolvedName();
					}
					else
					{
						userName = Language.GetString("PLAYER_NAME_UNAVAILABLE");
					}
				}
				else
				{
					userName = Language.GetString("PLAYER_NAME_DISCONNECTED");
				}
				this.indicators[i].tooltipProvider.SetContent(TooltipProvider.GetPlayerNameTooltipContent(userName));
			}
			bool flag = this.voteController && this.voteController.timerStartCondition != VoteController.TimerStartCondition.Never && !float.IsInfinity(this.voteController.timer);
			this.timerPanelObject.SetActive(flag);
			if (flag)
			{
				float num2 = this.voteController.timer;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				int num3 = Mathf.FloorToInt(num2 * 0.016666668f);
				int num4 = (int)num2 - num3 * 60;
				this.timerLabel.text = string.Format("{0}:{1:00}", num3, num4);
			}
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x0014C594 File Offset: 0x0014A794
		private void Update()
		{
			this.UpdateElements();
		}

		// Token: 0x04004CF0 RID: 19696
		public GameObject indicatorPrefab;

		// Token: 0x04004CF1 RID: 19697
		public Sprite hasNotVotedSprite;

		// Token: 0x04004CF2 RID: 19698
		public Sprite hasVotedSprite;

		// Token: 0x04004CF3 RID: 19699
		public RectTransform container;

		// Token: 0x04004CF4 RID: 19700
		public GameObject timerPanelObject;

		// Token: 0x04004CF5 RID: 19701
		public TextMeshProUGUI timerLabel;

		// Token: 0x04004CF6 RID: 19702
		public VoteController voteController;

		// Token: 0x04004CF7 RID: 19703
		private readonly List<VoteInfoPanelController.IndicatorInfo> indicators = new List<VoteInfoPanelController.IndicatorInfo>();

		// Token: 0x02000DB3 RID: 3507
		private struct IndicatorInfo
		{
			// Token: 0x04004CF8 RID: 19704
			public GameObject gameObject;

			// Token: 0x04004CF9 RID: 19705
			public Image image;

			// Token: 0x04004CFA RID: 19706
			public TooltipProvider tooltipProvider;
		}
	}
}
