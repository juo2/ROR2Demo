using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2
{
	// Token: 0x0200077D RID: 1917
	public class DroneWeaponsBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002820 RID: 10272 RVA: 0x000AE054 File Offset: 0x000AC254
		private void OnEnable()
		{
			ulong seed = Run.instance.seed ^ (ulong)((long)Run.instance.stageClearCount);
			this.rng = new Xoroshiro128Plus(seed);
			this.droneSpawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/DroneCommander/cscDroneCommander.asset").WaitForCompletion();
			this.placementRule = new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Approximate,
				minDistance = 3f,
				maxDistance = 40f,
				spawnOnTarget = base.transform
			};
			this.UpdateAllMinions(this.stack);
			MasterSummon.onServerMasterSummonGlobal += this.OnServerMasterSummonGlobal;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000AE0ED File Offset: 0x000AC2ED
		private void OnDisable()
		{
			MasterSummon.onServerMasterSummonGlobal -= this.OnServerMasterSummonGlobal;
			this.UpdateAllMinions(0);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000AE108 File Offset: 0x000AC308
		private void FixedUpdate()
		{
			if (this.previousStack != this.stack)
			{
				this.UpdateAllMinions(this.stack);
			}
			this.spawnDelay -= Time.fixedDeltaTime;
			if (!this.hasSpawnedDrone && this.body && this.spawnDelay <= 0f)
			{
				this.TrySpawnDrone();
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000AE16C File Offset: 0x000AC36C
		private void OnServerMasterSummonGlobal(MasterSummon.MasterSummonReport summonReport)
		{
			if (this.body && this.body.master && this.body.master == summonReport.leaderMasterInstance)
			{
				CharacterMaster summonMasterInstance = summonReport.summonMasterInstance;
				if (summonMasterInstance)
				{
					CharacterBody body = summonMasterInstance.GetBody();
					if (body)
					{
						this.UpdateMinionInventory(summonMasterInstance.inventory, body.bodyFlags, this.stack);
					}
				}
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
		private void UpdateAllMinions(int newStack)
		{
			if (this.body)
			{
				CharacterBody body = this.body;
				if ((body != null) ? body.master : null)
				{
					MinionOwnership.MinionGroup minionGroup = MinionOwnership.MinionGroup.FindGroup(this.body.master.netId);
					if (minionGroup != null)
					{
						foreach (MinionOwnership minionOwnership in minionGroup.members)
						{
							if (minionOwnership)
							{
								CharacterMaster component = minionOwnership.GetComponent<CharacterMaster>();
								if (component && component.inventory)
								{
									CharacterBody body2 = component.GetBody();
									if (body2)
									{
										this.UpdateMinionInventory(component.inventory, body2.bodyFlags, newStack);
									}
								}
							}
						}
						this.previousStack = newStack;
					}
				}
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000AE2A8 File Offset: 0x000AC4A8
		private void UpdateMinionInventory(Inventory inventory, CharacterBody.BodyFlags bodyFlags, int newStack)
		{
			if (inventory && newStack > 0 && (bodyFlags & CharacterBody.BodyFlags.Mechanical) > CharacterBody.BodyFlags.None)
			{
				int itemCount = inventory.GetItemCount(DLC1Content.Items.DroneWeaponsBoost);
				int itemCount2 = inventory.GetItemCount(DLC1Content.Items.DroneWeaponsDisplay1);
				int itemCount3 = inventory.GetItemCount(DLC1Content.Items.DroneWeaponsDisplay2);
				if (itemCount < this.stack)
				{
					inventory.GiveItem(DLC1Content.Items.DroneWeaponsBoost, this.stack - itemCount);
				}
				else if (itemCount > this.stack)
				{
					inventory.RemoveItem(DLC1Content.Items.DroneWeaponsBoost, itemCount - this.stack);
				}
				if (itemCount2 + itemCount3 <= 0)
				{
					ItemDef itemDef = DLC1Content.Items.DroneWeaponsDisplay1;
					if (UnityEngine.Random.value < 0.1f)
					{
						itemDef = DLC1Content.Items.DroneWeaponsDisplay2;
					}
					inventory.GiveItem(itemDef, 1);
					return;
				}
			}
			else
			{
				inventory.ResetItem(DLC1Content.Items.DroneWeaponsBoost);
				inventory.ResetItem(DLC1Content.Items.DroneWeaponsDisplay1);
				inventory.ResetItem(DLC1Content.Items.DroneWeaponsDisplay2);
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000AE378 File Offset: 0x000AC578
		private void TrySpawnDrone()
		{
			if (!this.body.master.IsDeployableLimited(DeployableSlot.DroneWeaponsDrone))
			{
				this.spawnDelay = 1f;
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.droneSpawnCard, this.placementRule, this.rng);
				directorSpawnRequest.summonerBodyObject = base.gameObject;
				directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(this.OnMasterSpawned);
				DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			}
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000AE3E8 File Offset: 0x000AC5E8
		private void OnMasterSpawned(SpawnCard.SpawnResult spawnResult)
		{
			this.hasSpawnedDrone = true;
			GameObject spawnedInstance = spawnResult.spawnedInstance;
			if (!spawnedInstance)
			{
				return;
			}
			CharacterMaster component = spawnedInstance.GetComponent<CharacterMaster>();
			if (component)
			{
				Deployable component2 = component.GetComponent<Deployable>();
				if (component2)
				{
					this.body.master.AddDeployable(component2, DeployableSlot.DroneWeaponsDrone);
				}
			}
		}

		// Token: 0x04002BC6 RID: 11206
		private const float display2Chance = 0.1f;

		// Token: 0x04002BC7 RID: 11207
		private int previousStack;

		// Token: 0x04002BC8 RID: 11208
		private CharacterSpawnCard droneSpawnCard;

		// Token: 0x04002BC9 RID: 11209
		private Xoroshiro128Plus rng;

		// Token: 0x04002BCA RID: 11210
		private DirectorPlacementRule placementRule;

		// Token: 0x04002BCB RID: 11211
		private const float minSpawnDist = 3f;

		// Token: 0x04002BCC RID: 11212
		private const float maxSpawnDist = 40f;

		// Token: 0x04002BCD RID: 11213
		private const float spawnRetryDelay = 1f;

		// Token: 0x04002BCE RID: 11214
		private bool hasSpawnedDrone;

		// Token: 0x04002BCF RID: 11215
		private float spawnDelay;
	}
}
