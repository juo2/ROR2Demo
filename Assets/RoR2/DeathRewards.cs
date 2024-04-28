using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200068D RID: 1677
	[RequireComponent(typeof(CharacterBody))]
	public class DeathRewards : MonoBehaviour, IOnKilledServerReceiver
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0008D185 File Offset: 0x0008B385
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0008D1B0 File Offset: 0x0008B3B0
		public uint goldReward
		{
			get
			{
				if (!this.characterBody.master)
				{
					return this.fallbackGold;
				}
				return this.characterBody.master.money;
			}
			set
			{
				if (this.characterBody.master)
				{
					this.characterBody.master.money = value;
					return;
				}
				this.fallbackGold = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0008D1DD File Offset: 0x0008B3DD
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x0008D1E5 File Offset: 0x0008B3E5
		public uint expReward { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0008D1EE File Offset: 0x0008B3EE
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x0008D1F6 File Offset: 0x0008B3F6
		public int spawnValue { get; set; }

		// Token: 0x060020CB RID: 8395 RVA: 0x0008D1FF File Offset: 0x0008B3FF
		[RuntimeInitializeOnLoadMethod]
		private static void LoadAssets()
		{
			DeathRewards.coinEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/CoinEmitter");
			DeathRewards.logbookPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LogPickup");
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0008D21F File Offset: 0x0008B41F
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0008D230 File Offset: 0x0008B430
		public void OnKilledServer(DamageReport damageReport)
		{
			CharacterBody attackerBody = damageReport.attackerBody;
			if (attackerBody)
			{
				Vector3 corePosition = this.characterBody.corePosition;
				uint num = this.goldReward;
				if (Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse6)
				{
					num = (uint)(num * 0.8f);
				}
				TeamManager.instance.GiveTeamMoney(damageReport.attackerTeamIndex, num);
				EffectManager.SpawnEffect(DeathRewards.coinEffectPrefab, new EffectData
				{
					origin = corePosition,
					genericFloat = this.goldReward,
					scale = this.characterBody.radius
				}, true);
				float num2 = 1f + (this.characterBody.level - 1f) * 0.3f;
				ExperienceManager.instance.AwardExperience(corePosition, attackerBody, (ulong)((uint)(this.expReward * num2)));
				if (this.logUnlockableDef && Run.instance.CanUnlockableBeGrantedThisRun(this.logUnlockableDef) && Util.CheckRoll(this.characterBody.isChampion ? 3f : 1f, damageReport.attackerMaster))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(DeathRewards.logbookPrefab, corePosition, UnityEngine.Random.rotation);
					gameObject.GetComponentInChildren<UnlockPickup>().unlockableDef = this.logUnlockableDef;
					gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
					NetworkServer.Spawn(gameObject);
				}
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0008D370 File Offset: 0x0008B570
		[ConCommand(commandName = "migrate_death_rewards_unlockables", flags = ConVarFlags.Cheat, helpText = "Migrates CharacterDeath component .logUnlockableName to .LogUnlockableDef for all instances.")]
		private static void CCMigrateDeathRewardUnlockables(ConCommandArgs args)
		{
			foreach (DeathRewards deathRewards in Resources.FindObjectsOfTypeAll<DeathRewards>())
			{
				FieldInfo field = typeof(DeathRewards).GetField("logUnlockableName");
				string text = ((string)field.GetValue(deathRewards)) ?? string.Empty;
				UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(text);
				if (!unlockableDef && text != string.Empty)
				{
					args.Log("DeathRewards component on object " + deathRewards.gameObject.name + " has a defined value for 'logUnlockableName' but it doesn't map to any known unlockable. Migration skipped. logUnlockableName='logUnlockableName'");
				}
				else if (deathRewards.logUnlockableDef == unlockableDef)
				{
					field.SetValue(deathRewards, string.Empty);
					EditorUtil.SetDirty(deathRewards);
					EditorUtil.SetDirty(deathRewards.gameObject);
				}
				else if (deathRewards.logUnlockableDef)
				{
					args.Log(string.Format("DeathRewards component on object {0} has a 'logUnlockableDef' field value which differs from the 'logUnlockableName' lookup. Migration skipped. logUnlockableDef={1} logUnlockableName={2}", deathRewards.gameObject.name, deathRewards.logUnlockableDef, text));
				}
				else
				{
					deathRewards.logUnlockableDef = unlockableDef;
					field.SetValue(deathRewards, string.Empty);
					EditorUtil.SetDirty(deathRewards);
					EditorUtil.SetDirty(deathRewards.gameObject);
					args.Log(string.Format("DeathRewards component on object {0} migrated. logUnlockableDef={1} logUnlockableName={2}", deathRewards.gameObject.name, deathRewards.logUnlockableDef, text));
				}
			}
		}

		// Token: 0x04002613 RID: 9747
		[Tooltip("'logUnlockableName' is discontinued. Use 'logUnlockableDef' instead.")]
		[Obsolete("'logUnlockableName' is discontinued. Use 'logUnlockableDef' instead.", true)]
		public string logUnlockableName = "";

		// Token: 0x04002614 RID: 9748
		public UnlockableDef logUnlockableDef;

		// Token: 0x04002615 RID: 9749
		public PickupDropTable bossDropTable;

		// Token: 0x04002616 RID: 9750
		private uint fallbackGold;

		// Token: 0x04002619 RID: 9753
		private CharacterBody characterBody;

		// Token: 0x0400261A RID: 9754
		private static GameObject coinEffectPrefab;

		// Token: 0x0400261B RID: 9755
		private static GameObject logbookPrefab;

		// Token: 0x0400261C RID: 9756
		[Header("Deprecated")]
		public SerializablePickupIndex bossPickup;
	}
}
