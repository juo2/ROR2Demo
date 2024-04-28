using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D2A RID: 3370
	public class LeaderboardController : MonoBehaviour
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x0013D43E File Offset: 0x0013B63E
		public bool IsValid
		{
			get
			{
				LeaderboardManager currentLeaderboardManager = this.CurrentLeaderboardManager;
				return currentLeaderboardManager != null && currentLeaderboardManager.IsValid;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x0013D451 File Offset: 0x0013B651
		public bool IsQuerying
		{
			get
			{
				LeaderboardManager currentLeaderboardManager = this.CurrentLeaderboardManager;
				return currentLeaderboardManager != null && currentLeaderboardManager.IsQuerying;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x0013D464 File Offset: 0x0013B664
		public LeaderboardManager CurrentLeaderboardManager
		{
			get
			{
				if (this._currentLeaderboardManager == null)
				{
					this._currentLeaderboardManager = new SteamLeaderboardManager(this);
				}
				return this._currentLeaderboardManager;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x0013D480 File Offset: 0x0013B680
		// (set) Token: 0x06004CE4 RID: 19684 RVA: 0x0013D488 File Offset: 0x0013B688
		public string currentLeaderboardName { get; private set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x0013D491 File Offset: 0x0013B691
		// (set) Token: 0x06004CE6 RID: 19686 RVA: 0x0013D499 File Offset: 0x0013B699
		public int currentPage { get; private set; }

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0013D4A4 File Offset: 0x0013B6A4
		private void Update()
		{
			if (this.CurrentLeaderboardManager != null && this.CurrentLeaderboardManager.IsValid && !this.CurrentLeaderboardManager.IsQuerying && this.isRequestQueued)
			{
				this.leaderboardInfoList = this.CurrentLeaderboardManager.GetLeaderboardInfoList();
				this.Rebuild();
				this.isRequestQueued = false;
			}
			if (this.noEntryObject)
			{
				this.noEntryObject.SetActive(this.leaderboardInfoList.Count == 0);
			}
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0013D520 File Offset: 0x0013B720
		private void SetStripCount(int newCount)
		{
			while (this.stripList.Count > newCount)
			{
				UnityEngine.Object.Destroy(this.stripList[this.stripList.Count - 1].gameObject);
				this.stripList.RemoveAt(this.stripList.Count - 1);
			}
			while (this.stripList.Count < newCount)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.stripPrefab, this.container);
				this.stripList.Add(gameObject.GetComponent<LeaderboardStrip>());
			}
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0013D5AC File Offset: 0x0013B7AC
		private void Rebuild()
		{
			int num = Mathf.Min(this.leaderboardInfoList.Count, this.entriesPerPage);
			this.SetStripCount(this.entriesPerPage);
			for (int i = 0; i < num; i++)
			{
				LeaderboardInfo leaderboardInfo = this.leaderboardInfoList[i];
				int num2 = Mathf.FloorToInt(leaderboardInfo.timeInSeconds / 60f);
				float num3 = leaderboardInfo.timeInSeconds - (float)(num2 * 60);
				string text = string.Format("{0:0}:{1:00.00}", num2, num3);
				LeaderboardStrip leaderboardStrip = this.stripList[i];
				leaderboardStrip.rankLabel.text = leaderboardInfo.rank.ToString();
				leaderboardStrip.usernameLabel.userId = this.CurrentLeaderboardManager.GetUserID(leaderboardInfo);
				leaderboardStrip.timeLabel.text = text;
				leaderboardStrip.classIcon.enabled = true;
				if (leaderboardInfo.survivorIndex != null)
				{
					leaderboardStrip.classIcon.texture = SurvivorCatalog.GetSurvivorPortrait(leaderboardInfo.survivorIndex.Value);
				}
				else
				{
					leaderboardStrip.classIcon.enabled = false;
				}
				leaderboardStrip.isMeImage.enabled = (string.CompareOrdinal(leaderboardInfo.userID, this.CurrentLeaderboardManager.GetLocalUserIdString()) == 0);
				leaderboardStrip.usernameLabel.Refresh();
			}
			for (int j = num; j < this.entriesPerPage; j++)
			{
				this.stripList[j].rankLabel.text = "";
				this.stripList[j].usernameLabel.userId = new UserID(0UL);
				this.stripList[j].timeLabel.text = "";
				this.stripList[j].classIcon.enabled = false;
				this.stripList[j].isMeImage.enabled = false;
				this.stripList[j].usernameLabel.Refresh();
			}
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0013D7B0 File Offset: 0x0013B9B0
		public void SetRequestedInfo(string newLeaderboardName, RequestType newRequestType, int newPage)
		{
			bool flag = this.currentLeaderboardName != newLeaderboardName;
			if (flag)
			{
				this.currentLeaderboardName = newLeaderboardName;
				this.CurrentLeaderboardManager.UpdateLeaderboard();
				newPage = 0;
			}
			bool flag2 = this.currentRequestType != newRequestType || flag;
			bool flag3 = this.currentPage != newPage || flag;
			if (flag2)
			{
				this.currentRequestType = newRequestType;
			}
			if (flag3)
			{
				this.currentPage = newPage;
			}
			this.isRequestQueued = (flag || flag2 || flag3);
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0013D820 File Offset: 0x0013BA20
		private void GenerateFakeLeaderboardList(int count)
		{
			this.leaderboardInfoList.Clear();
			for (int i = 1; i <= count; i++)
			{
				LeaderboardInfo item = default(LeaderboardInfo);
				item.userID = "76561197995890564";
				item.survivorIndex = new SurvivorIndex?((SurvivorIndex)UnityEngine.Random.Range(0, SurvivorCatalog.survivorCount - 1));
				item.timeInSeconds = UnityEngine.Random.Range(120f, 600f);
				this.leaderboardInfoList.Add(item);
			}
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0013D894 File Offset: 0x0013BA94
		public void SetRequestType(string requestTypeName)
		{
			RequestType requestType;
			if (Enum.TryParse<RequestType>(requestTypeName, false, out requestType))
			{
				this.currentRequestType = requestType;
			}
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0013D8B3 File Offset: 0x0013BAB3
		private void OrderLeaderboardListByTime(ref List<LeaderboardInfo> leaderboardInfoList)
		{
			leaderboardInfoList.Sort(new Comparison<LeaderboardInfo>(LeaderboardController.SortByTime));
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0013D8C8 File Offset: 0x0013BAC8
		private static int SortByTime(LeaderboardInfo p1, LeaderboardInfo p2)
		{
			return p1.timeInSeconds.CompareTo(p2.timeInSeconds);
		}

		// Token: 0x040049D9 RID: 18905
		public GameObject stripPrefab;

		// Token: 0x040049DA RID: 18906
		public RectTransform container;

		// Token: 0x040049DB RID: 18907
		public GameObject noEntryObject;

		// Token: 0x040049DC RID: 18908
		public AnimateImageAlpha animateImageAlpha;

		// Token: 0x040049DD RID: 18909
		public RequestType currentRequestType;

		// Token: 0x040049DE RID: 18910
		public int entriesPerPage = 16;

		// Token: 0x040049DF RID: 18911
		public MPButton nextPageButton;

		// Token: 0x040049E0 RID: 18912
		public MPButton previousPageButton;

		// Token: 0x040049E1 RID: 18913
		public MPButton resetPageButton;

		// Token: 0x040049E2 RID: 18914
		private LeaderboardManager _currentLeaderboardManager;

		// Token: 0x040049E3 RID: 18915
		private List<LeaderboardStrip> stripList = new List<LeaderboardStrip>();

		// Token: 0x040049E4 RID: 18916
		private List<LeaderboardInfo> leaderboardInfoList = new List<LeaderboardInfo>();

		// Token: 0x040049E5 RID: 18917
		private bool isRequestQueued;
	}
}
