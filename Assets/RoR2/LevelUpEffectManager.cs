using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200094C RID: 2380
	public class LevelUpEffectManager
	{
		// Token: 0x060035E3 RID: 13795 RVA: 0x000E3C1E File Offset: 0x000E1E1E
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			GlobalEventManager.onTeamLevelUp += LevelUpEffectManager.OnTeamLevelUp;
			GlobalEventManager.onCharacterLevelUp += LevelUpEffectManager.OnCharacterLevelUp;
			Run.onRunAmbientLevelUp += LevelUpEffectManager.OnRunAmbientLevelUp;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000E3C53 File Offset: 0x000E1E53
		private static void OnTeamLevelUp(TeamIndex teamIndex)
		{
			if (TeamComponent.GetTeamMembers(teamIndex).Count > 0)
			{
				TeamDef teamDef = TeamCatalog.GetTeamDef(teamIndex);
				Util.PlaySound((teamDef != null) ? teamDef.levelUpSound : null, RoR2Application.instance.gameObject);
			}
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000E3C88 File Offset: 0x000E1E88
		private static void OnCharacterLevelUp(CharacterBody characterBody)
		{
			TeamDef teamDef = TeamCatalog.GetTeamDef(characterBody.teamComponent.teamIndex);
			GameObject levelUpEffect = (teamDef != null) ? teamDef.levelUpEffect : null;
			if (characterBody)
			{
				Transform transform = characterBody.mainHurtBox ? characterBody.mainHurtBox.transform : characterBody.transform;
				EffectData effectData = new EffectData
				{
					origin = transform.position
				};
				if (characterBody.mainHurtBox)
				{
					effectData.SetHurtBoxReference(characterBody.gameObject);
					effectData.scale = characterBody.radius;
				}
				RoR2Application.fixedTimeTimers.CreateTimer((float)LevelUpEffectManager.pendingLevelUpEffects * LevelUpEffectManager.levelUpEffectInterval + UnityEngine.Random.value * LevelUpEffectManager.levelUpEffectJitter, delegate
				{
					if (characterBody)
					{
						EffectManager.SpawnEffect(levelUpEffect, effectData, false);
					}
					LevelUpEffectManager.levelUpEffectInterval -= 1f;
				});
				LevelUpEffectManager.levelUpEffectInterval += 1f;
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000E3DC9 File Offset: 0x000E1FC9
		private static void OnRunAmbientLevelUp(Run run)
		{
			Util.PlaySound("Play_UI_levelUp_enemy", RoR2Application.instance.gameObject);
		}

		// Token: 0x0400369D RID: 13981
		private static float levelUpEffectInterval = 0.25f;

		// Token: 0x0400369E RID: 13982
		private static float levelUpEffectJitter = 0.25f;

		// Token: 0x0400369F RID: 13983
		private static int pendingLevelUpEffects = 0;
	}
}
