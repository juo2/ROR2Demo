using System;
using System.Collections.Generic;
using EntityStates;
using HG;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005FC RID: 1532
	[RequireComponent(typeof(CombatSquad))]
	public class BossGroup : MonoBehaviour
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0007711B File Offset: 0x0007531B
		public float fixedAge
		{
			get
			{
				return this.combatSquad.awakeTime.timeSince;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0007712D File Offset: 0x0007532D
		public float fixedTimeSinceEnabled
		{
			get
			{
				return this.enabledTime.timeSince;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0007713A File Offset: 0x0007533A
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00077142 File Offset: 0x00075342
		public int bonusRewardCount { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0007714B File Offset: 0x0007534B
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00077153 File Offset: 0x00075353
		public CombatSquad combatSquad { get; private set; }

		// Token: 0x06001BFD RID: 7165 RVA: 0x0007715C File Offset: 0x0007535C
		private void Awake()
		{
			base.enabled = false;
			this.combatSquad = base.GetComponent<CombatSquad>();
			this.combatSquad.onMemberDiscovered += this.OnMemberDiscovered;
			this.combatSquad.onMemberLost += this.OnMemberLost;
			if (NetworkServer.active)
			{
				this.combatSquad.onDefeatedServer += this.OnDefeatedServer;
				this.combatSquad.onMemberAddedServer += this.OnMemberAddedServer;
				this.combatSquad.onMemberDefeatedServer += this.OnMemberDefeatedServer;
				this.rng = new Xoroshiro128Plus(Run.instance.bossRewardRng.nextUlong);
				this.bossDrops = new List<PickupIndex>();
				this.bossDropTables = new List<PickupDropTable>();
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00077226 File Offset: 0x00075426
		private void Start()
		{
			if (NetworkServer.active)
			{
				Action<BossGroup> action = BossGroup.onBossGroupStartServer;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0007723F File Offset: 0x0007543F
		private void OnEnable()
		{
			InstanceTracker.Add<BossGroup>(this);
			ObjectivePanelController.collectObjectiveSources += this.ReportObjective;
			this.enabledTime = Run.FixedTimeStamp.now;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00077263 File Offset: 0x00075463
		private void OnDisable()
		{
			ObjectivePanelController.collectObjectiveSources -= this.ReportObjective;
			InstanceTracker.Remove<BossGroup>(this);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0007727C File Offset: 0x0007547C
		private void FixedUpdate()
		{
			this.UpdateBossMemories();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00077284 File Offset: 0x00075484
		private void OnDefeatedServer()
		{
			this.DropRewards();
			Run.instance.OnServerBossDefeated(this);
			Action<BossGroup> action = BossGroup.onBossGroupDefeatedServer;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000772A7 File Offset: 0x000754A7
		private void OnMemberAddedServer(CharacterMaster memberMaster)
		{
			Run.instance.OnServerBossAdded(this, memberMaster);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000772B8 File Offset: 0x000754B8
		private void OnMemberDefeatedServer(CharacterMaster memberMaster, DamageReport damageReport)
		{
			GameObject bodyObject = memberMaster.GetBodyObject();
			DeathRewards deathRewards = (bodyObject != null) ? bodyObject.GetComponent<DeathRewards>() : null;
			if (deathRewards)
			{
				if (deathRewards.bossDropTable)
				{
					this.bossDropTables.Add(deathRewards.bossDropTable);
					return;
				}
				PickupIndex pickupIndex = (PickupIndex)deathRewards.bossPickup;
				if (pickupIndex != PickupIndex.none)
				{
					this.bossDrops.Add(pickupIndex);
				}
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00077324 File Offset: 0x00075524
		private void OnMemberDiscovered(CharacterMaster memberMaster)
		{
			base.enabled = true;
			memberMaster.isBoss = true;
			BossGroup.totalBossCountDirty = true;
			this.RememberBoss(memberMaster);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00077341 File Offset: 0x00075541
		private void OnMemberLost(CharacterMaster memberMaster)
		{
			memberMaster.isBoss = false;
			BossGroup.totalBossCountDirty = true;
			if (this.combatSquad.memberCount == 0)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00077364 File Offset: 0x00075564
		private void DropRewards()
		{
			if (!Run.instance)
			{
				Debug.LogError("No valid run instance!");
				return;
			}
			if (this.rng == null)
			{
				Debug.LogError("RNG is null!");
				return;
			}
			int participatingPlayerCount = Run.instance.participatingPlayerCount;
			if (participatingPlayerCount != 0)
			{
				if (this.dropPosition)
				{
					PickupIndex pickupIndex = PickupIndex.none;
					if (this.dropTable)
					{
						pickupIndex = this.dropTable.GenerateDrop(this.rng);
					}
					else
					{
						List<PickupIndex> list = Run.instance.availableTier2DropList;
						if (this.forceTier3Reward)
						{
							list = Run.instance.availableTier3DropList;
						}
						pickupIndex = this.rng.NextElementUniform<PickupIndex>(list);
					}
					int num = 1 + this.bonusRewardCount;
					if (this.scaleRewardsByPlayerCount)
					{
						num *= participatingPlayerCount;
					}
					float angle = 360f / (float)num;
					Vector3 vector = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 40f + Vector3.forward * 5f);
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
					bool flag = this.bossDrops != null && this.bossDrops.Count > 0;
					bool flag2 = this.bossDropTables != null && this.bossDropTables.Count > 0;
					int i = 0;
					while (i < num)
					{
						PickupIndex pickupIndex2 = pickupIndex;
						if (this.bossDrops != null && ((flag || flag2) && this.rng.nextNormalizedFloat <= this.bossDropChance))
						{
							if (flag2)
							{
								PickupDropTable pickupDropTable = this.rng.NextElementUniform<PickupDropTable>(this.bossDropTables);
								if (pickupDropTable != null)
								{
									pickupIndex2 = pickupDropTable.GenerateDrop(this.rng);
								}
							}
							else
							{
								pickupIndex2 = this.rng.NextElementUniform<PickupIndex>(this.bossDrops);
							}
						}
						PickupDropletController.CreatePickupDroplet(pickupIndex2, this.dropPosition.position, vector);
						i++;
						vector = rotation * vector;
					}
					return;
				}
				Debug.LogWarning("dropPosition not set for BossGroup! No item will be spawned.");
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001C08 RID: 7176 RVA: 0x00077560 File Offset: 0x00075760
		// (remove) Token: 0x06001C09 RID: 7177 RVA: 0x00077594 File Offset: 0x00075794
		public static event Action<BossGroup> onBossGroupStartServer;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001C0A RID: 7178 RVA: 0x000775C8 File Offset: 0x000757C8
		// (remove) Token: 0x06001C0B RID: 7179 RVA: 0x000775FC File Offset: 0x000757FC
		public static event Action<BossGroup> onBossGroupDefeatedServer;

		// Token: 0x06001C0C RID: 7180 RVA: 0x00077630 File Offset: 0x00075830
		private int FindBossMemoryIndex(NetworkInstanceId id)
		{
			for (int i = 0; i < this.bossMemoryCount; i++)
			{
				if (this.bossMemories[i].masterInstanceId == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0007766C File Offset: 0x0007586C
		private void RememberBoss(CharacterMaster master)
		{
			if (!master)
			{
				return;
			}
			int num = this.FindBossMemoryIndex(master.netId);
			if (num == -1)
			{
				num = this.AddBossMemory(master);
			}
			ref BossGroup.BossMemory ptr = ref this.bossMemories[num];
			ptr.cachedMaster = master;
			ptr.cachedBody = master.GetBody();
			this.UpdateObservations(ref ptr);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000776C4 File Offset: 0x000758C4
		private void UpdateObservations(ref BossGroup.BossMemory memory)
		{
			memory.lastObservedHealth = 0f;
			if (memory.cachedMaster && !memory.cachedBody)
			{
				memory.cachedBody = memory.cachedMaster.GetBody();
			}
			if (memory.cachedBody)
			{
				if (this.bestObservedName.Length == 0 && this.bestObservedSubtitle.Length == 0 && Time.fixedDeltaTime * 3f < memory.cachedBody.localStartTime.timeSince)
				{
					this.bestObservedName = Util.GetBestBodyName(memory.cachedBody.gameObject);
					this.bestObservedSubtitle = memory.cachedBody.GetSubtitle();
					if (this.bestObservedSubtitle.Length == 0)
					{
						this.bestObservedSubtitle = Language.GetString("NULL_SUBTITLE");
					}
					this.bestObservedSubtitle = "<sprite name=\"CloudLeft\" tint=1> " + this.bestObservedSubtitle + "<sprite name=\"CloudRight\" tint=1>";
				}
				HealthComponent healthComponent = memory.cachedBody.healthComponent;
				memory.maxObservedMaxHealth = Mathf.Max(memory.maxObservedMaxHealth, healthComponent.fullCombinedHealth);
				memory.lastObservedHealth = healthComponent.combinedHealth;
			}
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000777E8 File Offset: 0x000759E8
		private int AddBossMemory(CharacterMaster master)
		{
			BossGroup.BossMemory bossMemory = new BossGroup.BossMemory
			{
				masterInstanceId = master.netId,
				maxObservedMaxHealth = 0f,
				cachedMaster = master
			};
			ArrayUtils.ArrayAppend<BossGroup.BossMemory>(ref this.bossMemories, ref this.bossMemoryCount, bossMemory);
			return this.bossMemoryCount - 1;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0007783B File Offset: 0x00075A3B
		// (set) Token: 0x06001C11 RID: 7185 RVA: 0x00077843 File Offset: 0x00075A43
		public string bestObservedName { get; private set; } = "";

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0007784C File Offset: 0x00075A4C
		// (set) Token: 0x06001C13 RID: 7187 RVA: 0x00077854 File Offset: 0x00075A54
		public string bestObservedSubtitle { get; private set; } = "";

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0007785D File Offset: 0x00075A5D
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x00077865 File Offset: 0x00075A65
		public float totalMaxObservedMaxHealth { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0007786E File Offset: 0x00075A6E
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x00077876 File Offset: 0x00075A76
		public float totalObservedHealth { get; private set; }

		// Token: 0x06001C18 RID: 7192 RVA: 0x00077880 File Offset: 0x00075A80
		private void UpdateBossMemories()
		{
			this.totalMaxObservedMaxHealth = 0f;
			this.totalObservedHealth = 0f;
			for (int i = 0; i < this.bossMemoryCount; i++)
			{
				ref BossGroup.BossMemory ptr = ref this.bossMemories[i];
				this.UpdateObservations(ref ptr);
				this.totalMaxObservedMaxHealth += ptr.maxObservedMaxHealth;
				this.totalObservedHealth += Mathf.Max(ptr.lastObservedHealth, 0f);
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000778F8 File Offset: 0x00075AF8
		public static int GetTotalBossCount()
		{
			if (BossGroup.totalBossCountDirty)
			{
				BossGroup.totalBossCountDirty = false;
				BossGroup.lastTotalBossCount = 0;
				List<BossGroup> instancesList = InstanceTracker.GetInstancesList<BossGroup>();
				for (int i = 0; i < instancesList.Count; i++)
				{
					BossGroup.lastTotalBossCount += instancesList[i].combatSquad.readOnlyMembersList.Count;
				}
			}
			return BossGroup.lastTotalBossCount;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00077958 File Offset: 0x00075B58
		public static BossGroup FindBossGroup(CharacterBody body)
		{
			if (!body || !body.isBoss)
			{
				return null;
			}
			CharacterMaster master = body.master;
			if (!master)
			{
				return null;
			}
			List<BossGroup> instancesList = InstanceTracker.GetInstancesList<BossGroup>();
			for (int i = 0; i < instancesList.Count; i++)
			{
				BossGroup bossGroup = instancesList[i];
				if (bossGroup.combatSquad.ContainsMember(master))
				{
					return bossGroup;
				}
			}
			return null;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000779B8 File Offset: 0x00075BB8
		public void ReportObjective(CharacterMaster master, List<ObjectivePanelController.ObjectiveSourceDescriptor> output)
		{
			if (this.combatSquad.readOnlyMembersList.Count != 0)
			{
				output.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					source = this,
					master = master,
					objectiveType = typeof(BossGroup.DefeatBossObjectiveTracker)
				});
			}
		}

		// Token: 0x040021C7 RID: 8647
		public float bossDropChance = 0.15f;

		// Token: 0x040021C8 RID: 8648
		public Transform dropPosition;

		// Token: 0x040021C9 RID: 8649
		public PickupDropTable dropTable;

		// Token: 0x040021CA RID: 8650
		public bool scaleRewardsByPlayerCount = true;

		// Token: 0x040021CB RID: 8651
		[Tooltip("Whether or not this boss group should display a health bar on the HUD while any of its members are alive. Other scripts can change this at runtime to suppress a health bar until the boss is angered, for example. This field is not networked, so whatever is driving the value should be synchronized over the network.")]
		public bool shouldDisplayHealthBarOnHud = true;

		// Token: 0x040021CE RID: 8654
		private Xoroshiro128Plus rng;

		// Token: 0x040021CF RID: 8655
		private List<PickupDropTable> bossDropTables;

		// Token: 0x040021D0 RID: 8656
		private Run.FixedTimeStamp enabledTime;

		// Token: 0x040021D1 RID: 8657
		[Header("Deprecated")]
		public bool forceTier3Reward;

		// Token: 0x040021D2 RID: 8658
		private List<PickupIndex> bossDrops;

		// Token: 0x040021D5 RID: 8661
		private static readonly int initialBossMemoryCapacity = 8;

		// Token: 0x040021D6 RID: 8662
		private BossGroup.BossMemory[] bossMemories = new BossGroup.BossMemory[BossGroup.initialBossMemoryCapacity];

		// Token: 0x040021D7 RID: 8663
		private int bossMemoryCount;

		// Token: 0x040021DC RID: 8668
		private static int lastTotalBossCount = 0;

		// Token: 0x040021DD RID: 8669
		private static bool totalBossCountDirty = false;

		// Token: 0x020005FD RID: 1533
		private struct BossMemory
		{
			// Token: 0x040021DE RID: 8670
			public NetworkInstanceId masterInstanceId;

			// Token: 0x040021DF RID: 8671
			public float maxObservedMaxHealth;

			// Token: 0x040021E0 RID: 8672
			public float lastObservedHealth;

			// Token: 0x040021E1 RID: 8673
			public CharacterMaster cachedMaster;

			// Token: 0x040021E2 RID: 8674
			public CharacterBody cachedBody;
		}

		// Token: 0x020005FE RID: 1534
		private class DefeatBossObjectiveTracker : ObjectivePanelController.ObjectiveTracker
		{
			// Token: 0x06001C1E RID: 7198 RVA: 0x00077A6E File Offset: 0x00075C6E
			public DefeatBossObjectiveTracker()
			{
				this.baseToken = "OBJECTIVE_DEFEAT_BOSS";
			}
		}

		// Token: 0x020005FF RID: 1535
		public class EnableHudHealthBarState : EntityState
		{
			// Token: 0x06001C1F RID: 7199 RVA: 0x00077A81 File Offset: 0x00075C81
			public override void OnEnter()
			{
				base.OnEnter();
				this.bossGroup = base.GetComponent<BossGroup>();
				if (this.bossGroup != null)
				{
					this.bossGroup.shouldDisplayHealthBarOnHud = true;
				}
			}

			// Token: 0x06001C20 RID: 7200 RVA: 0x00077AA9 File Offset: 0x00075CA9
			public override void OnExit()
			{
				if (this.bossGroup != null)
				{
					this.bossGroup.shouldDisplayHealthBarOnHud = false;
				}
				base.OnExit();
			}

			// Token: 0x040021E3 RID: 8675
			private BossGroup bossGroup;
		}
	}
}
