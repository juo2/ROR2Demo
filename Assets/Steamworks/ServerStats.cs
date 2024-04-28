using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200017E RID: 382
	public class ServerStats
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0003A0C6 File Offset: 0x000382C6
		internal ServerStats(Server s)
		{
			this.server = s;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003A0D8 File Offset: 0x000382D8
		public void Refresh(ulong steamid, Action<ulong, bool> Callback = null)
		{
			if (Callback == null)
			{
				this.server.native.gameServerStats.RequestUserStats(steamid, null);
				return;
			}
			this.server.native.gameServerStats.RequestUserStats(steamid, delegate(GSStatsReceived_t o, bool failed)
			{
				Callback(steamid, o.Result == Result.OK && !failed);
			});
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003A154 File Offset: 0x00038354
		public void Commit(ulong steamid, Action<ulong, bool> Callback = null)
		{
			if (Callback == null)
			{
				this.server.native.gameServerStats.StoreUserStats(steamid, null);
				return;
			}
			this.server.native.gameServerStats.StoreUserStats(steamid, delegate(GSStatsStored_t o, bool failed)
			{
				Callback(steamid, o.Result == Result.OK && !failed);
			});
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0003A1CD File Offset: 0x000383CD
		public bool SetInt(ulong steamid, string name, int stat)
		{
			return this.server.native.gameServerStats.SetUserStat(steamid, name, stat);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003A1EC File Offset: 0x000383EC
		public bool SetFloat(ulong steamid, string name, float stat)
		{
			return this.server.native.gameServerStats.SetUserStat0(steamid, name, stat);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003A20C File Offset: 0x0003840C
		public int GetInt(ulong steamid, string name, int defaultValue = 0)
		{
			int result = defaultValue;
			if (!this.server.native.gameServerStats.GetUserStat(steamid, name, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003A240 File Offset: 0x00038440
		public float GetFloat(ulong steamid, string name, float defaultValue = 0f)
		{
			float result = defaultValue;
			if (!this.server.native.gameServerStats.GetUserStat0(steamid, name, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003A272 File Offset: 0x00038472
		public bool SetAchievement(ulong steamid, string name)
		{
			return this.server.native.gameServerStats.SetUserAchievement(steamid, name);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003A290 File Offset: 0x00038490
		public bool ClearAchievement(ulong steamid, string name)
		{
			return this.server.native.gameServerStats.ClearUserAchievement(steamid, name);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003A2B0 File Offset: 0x000384B0
		public bool GetAchievement(ulong steamid, string name)
		{
			bool flag = false;
			return this.server.native.gameServerStats.GetUserAchievement(steamid, name, ref flag) && flag;
		}

		// Token: 0x0400088A RID: 2186
		internal Server server;

		// Token: 0x020002A0 RID: 672
		public struct StatsReceived
		{
			// Token: 0x04000D1C RID: 3356
			public int Result;

			// Token: 0x04000D1D RID: 3357
			public ulong SteamId;
		}
	}
}
