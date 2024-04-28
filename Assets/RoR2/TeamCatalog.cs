using System;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A87 RID: 2695
	public static class TeamCatalog
	{
		// Token: 0x06003DDA RID: 15834 RVA: 0x000FF524 File Offset: 0x000FD724
		static TeamCatalog()
		{
			TeamCatalog.Register(TeamIndex.Neutral, new TeamDef
			{
				nameToken = "TEAM_NEUTRAL_NAME",
				softCharacterLimit = 40,
				friendlyFireScaling = 1f
			});
			TeamCatalog.Register(TeamIndex.Player, new TeamDef
			{
				nameToken = "TEAM_PLAYER_NAME",
				softCharacterLimit = 20,
				friendlyFireScaling = 0.5f,
				levelUpEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/LevelUpEffect"),
				levelUpSound = "Play_UI_levelUp_player"
			});
			TeamCatalog.Register(TeamIndex.Monster, new TeamDef
			{
				nameToken = "TEAM_MONSTER_NAME",
				softCharacterLimit = 40,
				friendlyFireScaling = 2f,
				levelUpEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/LevelUpEffectEnemy"),
				levelUpSound = "Play_UI_levelUp_enemy"
			});
			TeamCatalog.Register(TeamIndex.Lunar, new TeamDef
			{
				nameToken = "TEAM_LUNAR_NAME",
				softCharacterLimit = 40,
				friendlyFireScaling = 2f,
				levelUpEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/LevelUpEffectEnemy"),
				levelUpSound = "Play_UI_levelUp_enemy"
			});
			TeamCatalog.Register(TeamIndex.Void, new TeamDef
			{
				nameToken = "TEAM_VOID_NAME",
				softCharacterLimit = 40,
				friendlyFireScaling = 2f,
				levelUpEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/LevelUpEffectEnemy"),
				levelUpSound = "Play_UI_levelUp_enemy"
			});
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000FF675 File Offset: 0x000FD875
		private static void Register(TeamIndex teamIndex, TeamDef teamDef)
		{
			TeamCatalog.teamDefs[(int)teamIndex] = teamDef;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000FF67F File Offset: 0x000FD87F
		[CanBeNull]
		public static TeamDef GetTeamDef(TeamIndex teamIndex)
		{
			return ArrayUtils.GetSafe<TeamDef>(TeamCatalog.teamDefs, (int)teamIndex);
		}

		// Token: 0x04003CAC RID: 15532
		private static TeamDef[] teamDefs = new TeamDef[5];
	}
}
