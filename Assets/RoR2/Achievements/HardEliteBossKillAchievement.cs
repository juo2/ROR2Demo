using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000EA1 RID: 3745
	[RegisterAchievement("HardEliteBossKill", "Items.KillEliteFrenzy", null, typeof(HardEliteBossKillAchievement.EliteBossKillServerAchievement))]
	internal class HardEliteBossKillAchievement : BaseAchievement
	{
		// Token: 0x06005584 RID: 21892 RVA: 0x0015E4CA File Offset: 0x0015C6CA
		public override void OnInstall()
		{
			base.OnInstall();
			NetworkUser.OnPostNetworkUserStart += this.OnPostNetworkUserStart;
			Run.onRunStartGlobal += this.OnRunStart;
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x0015E4F4 File Offset: 0x0015C6F4
		public override void OnUninstall()
		{
			NetworkUser.OnPostNetworkUserStart -= this.OnPostNetworkUserStart;
			Run.onRunStartGlobal -= this.OnRunStart;
			base.OnUninstall();
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x0015E51E File Offset: 0x0015C71E
		private void UpdateTracking()
		{
			bool serverTracked;
			if (Run.instance)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty);
				serverTracked = (difficultyDef != null && difficultyDef.countsAsHardMode);
			}
			else
			{
				serverTracked = false;
			}
			base.SetServerTracked(serverTracked);
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x0015E550 File Offset: 0x0015C750
		private void OnPostNetworkUserStart(NetworkUser networkUser)
		{
			this.UpdateTracking();
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0015E550 File Offset: 0x0015C750
		private void OnRunStart(Run run)
		{
			this.UpdateTracking();
		}

		// Token: 0x02000EA2 RID: 3746
		private class EliteBossKillServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600558A RID: 21898 RVA: 0x0015E558 File Offset: 0x0015C758
			public override void OnInstall()
			{
				base.OnInstall();
				HardEliteBossKillAchievement.EliteBossKillServerAchievement.instancesList.Add(this);
				if (HardEliteBossKillAchievement.EliteBossKillServerAchievement.instancesList.Count == 1)
				{
					GlobalEventManager.onCharacterDeathGlobal += HardEliteBossKillAchievement.EliteBossKillServerAchievement.OnCharacterDeath;
				}
			}

			// Token: 0x0600558B RID: 21899 RVA: 0x0015E589 File Offset: 0x0015C789
			public override void OnUninstall()
			{
				if (HardEliteBossKillAchievement.EliteBossKillServerAchievement.instancesList.Count == 1)
				{
					GlobalEventManager.onCharacterDeathGlobal -= HardEliteBossKillAchievement.EliteBossKillServerAchievement.OnCharacterDeath;
				}
				HardEliteBossKillAchievement.EliteBossKillServerAchievement.instancesList.Remove(this);
				base.OnUninstall();
			}

			// Token: 0x0600558C RID: 21900 RVA: 0x0015E5BC File Offset: 0x0015C7BC
			private static void OnCharacterDeath(DamageReport damageReport)
			{
				if (!damageReport.victim)
				{
					return;
				}
				CharacterBody component = damageReport.victim.GetComponent<CharacterBody>();
				if (!component || !component.isChampion || !component.isElite)
				{
					return;
				}
				foreach (HardEliteBossKillAchievement.EliteBossKillServerAchievement eliteBossKillServerAchievement in HardEliteBossKillAchievement.EliteBossKillServerAchievement.instancesList)
				{
					GameObject masterObject = eliteBossKillServerAchievement.serverAchievementTracker.networkUser.masterObject;
					if (masterObject)
					{
						CharacterMaster component2 = masterObject.GetComponent<CharacterMaster>();
						if (component2)
						{
							CharacterBody body = component2.GetBody();
							if (body && body.healthComponent && body.healthComponent.alive)
							{
								eliteBossKillServerAchievement.Grant();
							}
						}
					}
				}
			}

			// Token: 0x0400505C RID: 20572
			private static readonly List<HardEliteBossKillAchievement.EliteBossKillServerAchievement> instancesList = new List<HardEliteBossKillAchievement.EliteBossKillServerAchievement>();
		}
	}
}
