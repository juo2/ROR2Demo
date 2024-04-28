using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A85 RID: 2693
	[Serializable]
	public struct TeamMask
	{
		// Token: 0x06003DD2 RID: 15826 RVA: 0x000FF432 File Offset: 0x000FD632
		public bool HasTeam(TeamIndex teamIndex)
		{
			return teamIndex >= TeamIndex.Neutral && teamIndex < TeamIndex.Count && ((ulong)this.a & 1UL << (int)teamIndex) > 0UL;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000FF451 File Offset: 0x000FD651
		public void AddTeam(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return;
			}
			this.a |= (byte)(1 << (int)teamIndex);
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000FF471 File Offset: 0x000FD671
		public void RemoveTeam(TeamIndex teamIndex)
		{
			if (teamIndex < TeamIndex.Neutral || teamIndex >= TeamIndex.Count)
			{
				return;
			}
			this.a &= (byte)(~(byte)(1 << (int)teamIndex));
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000FF494 File Offset: 0x000FD694
		static TeamMask()
		{
			for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
			{
				TeamMask.all.AddTeam(teamIndex);
			}
			TeamMask.allButNeutral = TeamMask.all;
			TeamMask.allButNeutral.RemoveTeam(TeamIndex.Neutral);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000FF4DC File Offset: 0x000FD6DC
		public static TeamMask AllExcept(TeamIndex teamIndexToExclude)
		{
			TeamMask result = TeamMask.all;
			result.RemoveTeam(teamIndexToExclude);
			return result;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000FF4F8 File Offset: 0x000FD6F8
		public static TeamMask GetUnprotectedTeams(TeamIndex attackerTeam)
		{
			if (FriendlyFireManager.friendlyFireMode == FriendlyFireManager.FriendlyFireMode.Off)
			{
				return TeamMask.AllExcept(attackerTeam);
			}
			return TeamMask.all;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000FF50D File Offset: 0x000FD70D
		public static TeamMask GetEnemyTeams(TeamIndex teamIndex)
		{
			if (FriendlyFireManager.friendlyFireMode == FriendlyFireManager.FriendlyFireMode.FreeForAll)
			{
				return TeamMask.all;
			}
			return TeamMask.AllExcept(teamIndex);
		}

		// Token: 0x04003CA3 RID: 15523
		[SerializeField]
		public byte a;

		// Token: 0x04003CA4 RID: 15524
		public static readonly TeamMask none;

		// Token: 0x04003CA5 RID: 15525
		public static readonly TeamMask allButNeutral;

		// Token: 0x04003CA6 RID: 15526
		public static readonly TeamMask all = default(TeamMask);
	}
}
