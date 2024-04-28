using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DB4 RID: 3508
	public class WeeklyRunScreenController : MonoBehaviour
	{
		// Token: 0x06005051 RID: 20561 RVA: 0x0014C5AF File Offset: 0x0014A7AF
		private void OnEnable()
		{
			this.currentCycle = WeeklyRun.GetCurrentSeedCycle();
			this.UpdateLeaderboard();
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0014C5C2 File Offset: 0x0014A7C2
		private void UpdateLeaderboard()
		{
			if (this.leaderboard && this.leaderboard.IsValid && !this.leaderboard.IsQuerying)
			{
				this.InitializeLeaderboardInfo();
			}
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0014C5F1 File Offset: 0x0014A7F1
		private void InitializeLeaderboardInfo()
		{
			this.leaderboard.SetRequestedInfo(WeeklyRun.GetLeaderboardName(1, this.currentCycle), this.leaderboard.currentRequestType, this.leaderboard.currentPage);
			this.leaderboardInitiated = true;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x0014C627 File Offset: 0x0014A827
		public void SetCurrentLeaderboard(GameObject leaderboardGameObject)
		{
			this.leaderboard = leaderboardGameObject.GetComponent<LeaderboardController>();
			this.UpdateLeaderboard();
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x0014C63C File Offset: 0x0014A83C
		private void Update()
		{
			uint currentSeedCycle = WeeklyRun.GetCurrentSeedCycle();
			if (currentSeedCycle != this.currentCycle)
			{
				this.currentCycle = currentSeedCycle;
				this.UpdateLeaderboard();
			}
			if (!this.leaderboardInitiated)
			{
				this.UpdateLeaderboard();
			}
			TimeSpan t = WeeklyRun.GetSeedCycleStartDateTime(this.currentCycle + 1U) - WeeklyRun.now;
			string @string = Language.GetString("WEEKLY_RUN_NEXT_CYCLE_COUNTDOWN_FORMAT");
			this.countdownLabel.text = string.Format(@string, t.Hours + t.Days * 24, t.Minutes, t.Seconds);
			if (t != this.lastCountdown)
			{
				this.lastCountdown = t;
				this.labelFadeValue = 0f;
			}
			this.labelFadeValue = Mathf.Max(this.labelFadeValue + Time.deltaTime * 1f, 0f);
			Color white = Color.white;
			if (t.Days == 0 && t.Hours == 0)
			{
				white.g = this.labelFadeValue;
				white.b = this.labelFadeValue;
			}
			this.countdownLabel.color = white;
		}

		// Token: 0x04004CFB RID: 19707
		public LeaderboardController leaderboard;

		// Token: 0x04004CFC RID: 19708
		public TextMeshProUGUI countdownLabel;

		// Token: 0x04004CFD RID: 19709
		private uint currentCycle;

		// Token: 0x04004CFE RID: 19710
		private TimeSpan lastCountdown;

		// Token: 0x04004CFF RID: 19711
		private float labelFadeValue;

		// Token: 0x04004D00 RID: 19712
		private bool leaderboardInitiated;
	}
}
