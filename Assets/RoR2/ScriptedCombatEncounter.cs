using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000882 RID: 2178
	[RequireComponent(typeof(CombatSquad))]
	public class ScriptedCombatEncounter : MonoBehaviour
	{
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06002FBA RID: 12218 RVA: 0x000CB3AC File Offset: 0x000C95AC
		// (remove) Token: 0x06002FBB RID: 12219 RVA: 0x000CB3E4 File Offset: 0x000C95E4
		public event Action<ScriptedCombatEncounter> onBeginEncounter;

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000CB419 File Offset: 0x000C9619
		// (set) Token: 0x06002FBD RID: 12221 RVA: 0x000CB421 File Offset: 0x000C9621
		public CombatSquad combatSquad { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000CB42A File Offset: 0x000C962A
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x000CB432 File Offset: 0x000C9632
		public bool hasSpawnedServer { get; private set; }

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000CB43C File Offset: 0x000C963C
		private void Awake()
		{
			this.combatSquad = base.GetComponent<CombatSquad>();
			this.hasSpawnedServer = false;
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(this.randomizeSeed ? Run.instance.stageRng.nextUlong : this.seed);
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000CB48D File Offset: 0x000C968D
		private void Start()
		{
			if (NetworkServer.active && this.spawnOnStart)
			{
				this.BeginEncounter();
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000CB4A4 File Offset: 0x000C96A4
		private void Spawn(ref ScriptedCombatEncounter.SpawnInfo spawnInfo)
		{
			Vector3 position = base.transform.position;
			DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
				minDistance = 0f,
				maxDistance = 1000f,
				position = position
			};
			if (spawnInfo.explicitSpawnPosition)
			{
				directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.Direct;
				directorPlacementRule.spawnOnTarget = spawnInfo.explicitSpawnPosition;
			}
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(spawnInfo.spawnCard, directorPlacementRule, this.rng);
			directorSpawnRequest.ignoreTeamMemberLimit = true;
			directorSpawnRequest.teamIndexOverride = new TeamIndex?(this.teamIndex);
			DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
			directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(this.<Spawn>g__HandleSpawn|21_0));
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000CB560 File Offset: 0x000C9760
		public void BeginEncounter()
		{
			if (this.hasSpawnedServer || !NetworkServer.active)
			{
				return;
			}
			for (int i = 0; i < this.spawns.Length; i++)
			{
				ref ScriptedCombatEncounter.SpawnInfo ptr = ref this.spawns[i];
				if (this.rng.nextNormalizedFloat * 100f >= ptr.cullChance)
				{
					this.Spawn(ref ptr);
				}
			}
			Action<ScriptedCombatEncounter> action = this.onBeginEncounter;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000CB5E0 File Offset: 0x000C97E0
		[CompilerGenerated]
		private void <Spawn>g__HandleSpawn|21_0(SpawnCard.SpawnResult spawnResult)
		{
			GameObject spawnedInstance = spawnResult.spawnedInstance;
			if (spawnedInstance)
			{
				this.hasSpawnedServer = true;
				CharacterMaster component = spawnedInstance.GetComponent<CharacterMaster>();
				if (this.grantUniqueBonusScaling)
				{
					float num = 1f;
					float num2 = 1f;
					num += Run.instance.difficultyCoefficient / 2.5f;
					num2 += Run.instance.difficultyCoefficient / 30f;
					int num3 = Mathf.Max(1, Run.instance.livingPlayerCount);
					num *= Mathf.Pow((float)num3, 0.5f);
					Debug.LogFormat("Scripted Combat Encounter: currentBoostHpCoefficient={0}, currentBoostDamageCoefficient={1}", new object[]
					{
						num,
						num2
					});
					component.inventory.GiveItem(RoR2Content.Items.BoostHp, Mathf.RoundToInt((num - 1f) * 10f));
					component.inventory.GiveItem(RoR2Content.Items.BoostDamage, Mathf.RoundToInt((num2 - 1f) * 10f));
				}
				if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.eliteOnlyArtifactDef))
				{
					EliteDef[] array = new EliteDef[]
					{
						RoR2Content.Elites.Fire,
						RoR2Content.Elites.Lightning,
						RoR2Content.Elites.Ice,
						DLC1Content.Elites.Earth
					};
					int num4 = this.rng.RangeInt(0, array.Length);
					EliteDef eliteDef = array[num4];
					EquipmentIndex? equipmentIndex;
					if (eliteDef == null)
					{
						equipmentIndex = null;
					}
					else
					{
						EquipmentDef eliteEquipmentDef = eliteDef.eliteEquipmentDef;
						equipmentIndex = ((eliteEquipmentDef != null) ? new EquipmentIndex?(eliteEquipmentDef.equipmentIndex) : null);
					}
					EquipmentIndex equipmentIndex2 = equipmentIndex ?? EquipmentIndex.None;
					if (equipmentIndex2 != EquipmentIndex.None)
					{
						component.inventory.SetEquipmentIndex(equipmentIndex2);
					}
				}
				this.combatSquad.AddMember(component);
				return;
			}
			Debug.LogFormat("No spawned master from combat group!", Array.Empty<object>());
		}

		// Token: 0x04003180 RID: 12672
		public ulong seed;

		// Token: 0x04003181 RID: 12673
		public bool randomizeSeed;

		// Token: 0x04003182 RID: 12674
		public TeamIndex teamIndex;

		// Token: 0x04003183 RID: 12675
		public ScriptedCombatEncounter.SpawnInfo[] spawns;

		// Token: 0x04003184 RID: 12676
		public bool spawnOnStart;

		// Token: 0x04003185 RID: 12677
		public bool grantUniqueBonusScaling = true;

		// Token: 0x04003188 RID: 12680
		private Xoroshiro128Plus rng;

		// Token: 0x02000883 RID: 2179
		[Serializable]
		public struct SpawnInfo
		{
			// Token: 0x04003189 RID: 12681
			public SpawnCard spawnCard;

			// Token: 0x0400318A RID: 12682
			public Transform explicitSpawnPosition;

			// Token: 0x0400318B RID: 12683
			[Tooltip("The chance that this spawn card will be culled, removing it from the list. A value of 0 means it is guaranteed.")]
			[Range(0f, 100f)]
			public float cullChance;
		}
	}
}
