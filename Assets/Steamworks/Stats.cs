using System;

namespace Facepunch.Steamworks
{
	// Token: 0x02000171 RID: 369
	public class Stats : IDisposable
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x00037BAE File Offset: 0x00035DAE
		internal Stats(Client c)
		{
			this.client = c;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00037BBD File Offset: 0x00035DBD
		public bool StoreStats()
		{
			return this.client.native.userstats.StoreStats();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00037BD4 File Offset: 0x00035DD4
		public void UpdateStats()
		{
			this.client.native.userstats.RequestCurrentStats();
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00037BEC File Offset: 0x00035DEC
		public void UpdateGlobalStats(int days = 1)
		{
			this.client.native.userstats.GetNumberOfCurrentPlayers(null);
			this.client.native.userstats.RequestGlobalAchievementPercentages(null);
			this.client.native.userstats.RequestGlobalStats(days, null);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00037C40 File Offset: 0x00035E40
		public int GetInt(string name)
		{
			int result = 0;
			this.client.native.userstats.GetStat(name, out result);
			return result;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00037C6C File Offset: 0x00035E6C
		public long GetGlobalInt(string name)
		{
			long result = 0L;
			this.client.native.userstats.GetGlobalStat(name, out result);
			return result;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00037C98 File Offset: 0x00035E98
		public float GetFloat(string name)
		{
			float result = 0f;
			this.client.native.userstats.GetStat0(name, out result);
			return result;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00037CC8 File Offset: 0x00035EC8
		public double GetGlobalFloat(string name)
		{
			double result = 0.0;
			this.client.native.userstats.GetGlobalStat0(name, out result);
			return result;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00037CFC File Offset: 0x00035EFC
		public bool Set(string name, int value, bool store = true)
		{
			bool flag = this.client.native.userstats.SetStat(name, value);
			if (store)
			{
				return flag && this.client.native.userstats.StoreStats();
			}
			return flag;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00037D40 File Offset: 0x00035F40
		public bool Set(string name, float value, bool store = true)
		{
			bool flag = this.client.native.userstats.SetStat0(name, value);
			if (store)
			{
				return flag && this.client.native.userstats.StoreStats();
			}
			return flag;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00037D84 File Offset: 0x00035F84
		public bool Add(string name, int amount = 1, bool store = true)
		{
			int num = this.GetInt(name);
			num += amount;
			return this.Set(name, num, store);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00037DA6 File Offset: 0x00035FA6
		public bool ResetAll(bool includeAchievements)
		{
			return this.client.native.userstats.ResetAllStats(includeAchievements);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00037DBE File Offset: 0x00035FBE
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x04000833 RID: 2099
		internal Client client;
	}
}
