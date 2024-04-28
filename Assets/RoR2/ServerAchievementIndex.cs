using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004B9 RID: 1209
	public struct ServerAchievementIndex : IEquatable<ServerAchievementIndex>
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00060B40 File Offset: 0x0005ED40
		public bool Equals(ServerAchievementIndex other)
		{
			return this.intValue == other.intValue;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00060B50 File Offset: 0x0005ED50
		public override bool Equals(object obj)
		{
			return obj != null && obj is ServerAchievementIndex && this.Equals((ServerAchievementIndex)obj);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00060B6D File Offset: 0x0005ED6D
		public override int GetHashCode()
		{
			return this.intValue.GetHashCode();
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00060B7A File Offset: 0x0005ED7A
		public static ServerAchievementIndex operator ++(ServerAchievementIndex achievementIndex)
		{
			achievementIndex.intValue++;
			return achievementIndex;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00060B40 File Offset: 0x0005ED40
		public static bool operator ==(ServerAchievementIndex a, ServerAchievementIndex b)
		{
			return a.intValue == b.intValue;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00060B89 File Offset: 0x0005ED89
		public static bool operator !=(ServerAchievementIndex a, ServerAchievementIndex b)
		{
			return a.intValue != b.intValue;
		}

		// Token: 0x04001BAC RID: 7084
		[SerializeField]
		public int intValue;
	}
}
