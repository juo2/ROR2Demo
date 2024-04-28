using System;
using System.Collections.Generic;
using System.Linq;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000160 RID: 352
	public class Achievements : IDisposable
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0003402E File Offset: 0x0003222E
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00034036 File Offset: 0x00032236
		public Achievement[] All { get; private set; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000A45 RID: 2629 RVA: 0x00034040 File Offset: 0x00032240
		// (remove) Token: 0x06000A46 RID: 2630 RVA: 0x00034078 File Offset: 0x00032278
		public event Action OnUpdated;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000A47 RID: 2631 RVA: 0x000340B0 File Offset: 0x000322B0
		// (remove) Token: 0x06000A48 RID: 2632 RVA: 0x000340E8 File Offset: 0x000322E8
		public event Action<Achievement> OnAchievementStateChanged;

		// Token: 0x06000A49 RID: 2633 RVA: 0x00034120 File Offset: 0x00032320
		internal Achievements(Client c)
		{
			this.client = c;
			this.All = new Achievement[0];
			c.RegisterCallback<UserStatsReceived_t>(new Action<UserStatsReceived_t>(this.UserStatsReceived));
			c.RegisterCallback<UserStatsStored_t>(new Action<UserStatsStored_t>(this.UserStatsStored));
			this.Refresh();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0003417C File Offset: 0x0003237C
		public void Refresh()
		{
			Achievement[] old = this.All;
			this.All = Enumerable.Range(0, (int)this.client.native.userstats.GetNumAchievements()).Select(delegate(int x)
			{
				if (old != null)
				{
					string name = this.client.native.userstats.GetAchievementName((uint)x);
					Achievement achievement = old.FirstOrDefault((Achievement y) => y.Id == name);
					if (achievement != null)
					{
						if (achievement.Refresh())
						{
							this.unlockedRecently.Add(achievement);
						}
						return achievement;
					}
				}
				return new Achievement(this.client, x);
			}).ToArray<Achievement>();
			foreach (Achievement a in this.unlockedRecently)
			{
				this.OnUnlocked(a);
			}
			this.unlockedRecently.Clear();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0003422C File Offset: 0x0003242C
		internal void OnUnlocked(Achievement a)
		{
			Action<Achievement> onAchievementStateChanged = this.OnAchievementStateChanged;
			if (onAchievementStateChanged == null)
			{
				return;
			}
			onAchievementStateChanged(a);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0003423F File Offset: 0x0003243F
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00034248 File Offset: 0x00032448
		public Achievement Find(string identifier)
		{
			return this.All.FirstOrDefault((Achievement x) => x.Id == identifier);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0003427C File Offset: 0x0003247C
		public bool Trigger(string identifier, bool apply = true)
		{
			Achievement achievement = this.Find(identifier);
			return achievement != null && achievement.Trigger(apply);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003429D File Offset: 0x0003249D
		public bool Reset(string identifier)
		{
			return this.client.native.userstats.ClearAchievement(identifier);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000342B5 File Offset: 0x000324B5
		private void UserStatsReceived(UserStatsReceived_t stats)
		{
			if (stats.GameID != (ulong)this.client.AppId)
			{
				return;
			}
			this.Refresh();
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000342E2 File Offset: 0x000324E2
		private void UserStatsStored(UserStatsStored_t stats)
		{
			if (stats.GameID != (ulong)this.client.AppId)
			{
				return;
			}
			this.Refresh();
			Action onUpdated = this.OnUpdated;
			if (onUpdated == null)
			{
				return;
			}
			onUpdated();
		}

		// Token: 0x040007D3 RID: 2003
		internal Client client;

		// Token: 0x040007D7 RID: 2007
		private List<Achievement> unlockedRecently = new List<Achievement>();
	}
}
