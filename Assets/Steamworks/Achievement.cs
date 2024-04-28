using System;

namespace Facepunch.Steamworks
{
	// Token: 0x02000161 RID: 353
	public class Achievement
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0003430F File Offset: 0x0003250F
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00034317 File Offset: 0x00032517
		public string Id { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00034320 File Offset: 0x00032520
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00034328 File Offset: 0x00032528
		public string Name { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00034331 File Offset: 0x00032531
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00034339 File Offset: 0x00032539
		public string Description { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00034342 File Offset: 0x00032542
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0003434A File Offset: 0x0003254A
		public bool State { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00034353 File Offset: 0x00032553
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0003435B File Offset: 0x0003255B
		public DateTime UnlockTime { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00034364 File Offset: 0x00032564
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0003436C File Offset: 0x0003256C
		private int iconId { get; set; } = -1;

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00034378 File Offset: 0x00032578
		public float GlobalUnlockedPercentage
		{
			get
			{
				if (this.State)
				{
					return 1f;
				}
				float result = 0f;
				if (!this.client.native.userstats.GetAchievementAchievedPercent(this.Id, out result))
				{
					return -1f;
				}
				return result;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x000343C0 File Offset: 0x000325C0
		public Image Icon
		{
			get
			{
				if (this.iconId <= 0)
				{
					return null;
				}
				if (this._icon == null)
				{
					this._icon = new Image();
					this._icon.Id = this.iconId;
				}
				if (this._icon.IsLoaded)
				{
					return this._icon;
				}
				if (!this._icon.TryLoad(this.client.native.utils))
				{
					return null;
				}
				return this._icon;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00034438 File Offset: 0x00032638
		public Achievement(Client client, int index)
		{
			this.client = client;
			this.Id = client.native.userstats.GetAchievementName((uint)index);
			this.Name = client.native.userstats.GetAchievementDisplayAttribute(this.Id, "name");
			this.Description = client.native.userstats.GetAchievementDisplayAttribute(this.Id, "desc");
			this.iconId = client.native.userstats.GetAchievementIcon(this.Id);
			this.Refresh();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000344D8 File Offset: 0x000326D8
		public bool Trigger(bool apply = true)
		{
			if (this.State)
			{
				return false;
			}
			this.State = true;
			this.UnlockTime = DateTime.Now;
			bool result = this.client.native.userstats.SetAchievement(this.Id);
			if (apply)
			{
				this.client.Stats.StoreStats();
			}
			this.client.Achievements.OnUnlocked(this);
			return result;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00034541 File Offset: 0x00032741
		public bool Reset()
		{
			this.State = false;
			this.UnlockTime = DateTime.Now;
			return this.client.native.userstats.ClearAchievement(this.Id);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00034570 File Offset: 0x00032770
		public bool Refresh()
		{
			bool state = this.State;
			bool state2 = false;
			this.State = false;
			uint value;
			if (this.client.native.userstats.GetAchievementAndUnlockTime(this.Id, ref state2, out value))
			{
				this.State = state2;
				this.UnlockTime = Utility.Epoch.ToDateTime(value);
			}
			this.refreshCount++;
			return state != this.State && this.refreshCount > 1;
		}

		// Token: 0x040007D8 RID: 2008
		private Client client;

		// Token: 0x040007DF RID: 2015
		private int refreshCount;

		// Token: 0x040007E0 RID: 2016
		private Image _icon;
	}
}
