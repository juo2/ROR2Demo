using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using RoR2.DirectionalSearch;
using RoR2.Orbs;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006C3 RID: 1731
	[RequireComponent(typeof(CharacterBody))]
	public class EquipmentSlot : NetworkBehaviour
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x00090F4D File Offset: 0x0008F14D
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x00090F55 File Offset: 0x0008F155
		public byte activeEquipmentSlot { get; private set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x00090F5E File Offset: 0x0008F15E
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x00090F66 File Offset: 0x0008F166
		public EquipmentIndex equipmentIndex { get; private set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x00090F6F File Offset: 0x0008F16F
		// (set) Token: 0x060021C2 RID: 8642 RVA: 0x00090F77 File Offset: 0x0008F177
		public int stock { get; private set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x00090F80 File Offset: 0x0008F180
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x00090F88 File Offset: 0x0008F188
		public int maxStock { get; private set; }

		// Token: 0x060021C5 RID: 8645 RVA: 0x00090F91 File Offset: 0x0008F191
		public override void OnStartServer()
		{
			base.OnStartServer();
			this.UpdateAuthority();
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00090F9F File Offset: 0x0008F19F
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00090FAD File Offset: 0x0008F1AD
		public override void OnStopAuthority()
		{
			base.OnStopAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00090FBB File Offset: 0x0008F1BB
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x00090FD0 File Offset: 0x0008F1D0
		private void UpdateInventory()
		{
			this.inventory = this.characterBody.inventory;
			if (this.inventory)
			{
				this.activeEquipmentSlot = this.inventory.activeEquipmentSlot;
				this.equipmentIndex = this.inventory.GetEquipmentIndex();
				this.stock = (int)this.inventory.GetEquipment((uint)this.inventory.activeEquipmentSlot).charges;
				this.maxStock = this.inventory.GetActiveEquipmentMaxCharges();
				this._rechargeTime = this.inventory.GetEquipment((uint)this.inventory.activeEquipmentSlot).chargeFinishTime;
				return;
			}
			this.activeEquipmentSlot = 0;
			this.equipmentIndex = EquipmentIndex.None;
			this.stock = 0;
			this.maxStock = 0;
			this._rechargeTime = Run.FixedTimeStamp.positiveInfinity;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00091098 File Offset: 0x0008F298
		// (set) Token: 0x060021CB RID: 8651 RVA: 0x000910A0 File Offset: 0x0008F2A0
		public CharacterBody characterBody { get; private set; }

		// Token: 0x060021CC RID: 8652 RVA: 0x000910AC File Offset: 0x0008F2AC
		[Server]
		private void UpdateGoldGat()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.EquipmentSlot::UpdateGoldGat()' called on client");
				return;
			}
			bool flag = this.equipmentIndex == RoR2Content.Equipment.GoldGat.equipmentIndex;
			if (flag != this.goldgatControllerObject)
			{
				if (flag)
				{
					this.goldgatControllerObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GoldGatController"));
					this.goldgatControllerObject.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
					return;
				}
				UnityEngine.Object.Destroy(this.goldgatControllerObject);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x0009112A File Offset: 0x0008F32A
		public float cooldownTimer
		{
			get
			{
				return this._rechargeTime.timeUntil;
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00091138 File Offset: 0x0008F338
		public Transform FindActiveEquipmentDisplay()
		{
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component)
			{
				Transform modelTransform = component.modelTransform;
				if (modelTransform)
				{
					CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
					if (component2)
					{
						List<GameObject> equipmentDisplayObjects = component2.GetEquipmentDisplayObjects(this.equipmentIndex);
						if (equipmentDisplayObjects.Count > 0)
						{
							return equipmentDisplayObjects[0].transform;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00091198 File Offset: 0x0008F398
		[ClientRpc]
		private void RpcOnClientEquipmentActivationRecieved()
		{
			Util.PlaySound(EquipmentSlot.equipmentActivateString, base.gameObject);
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(this.equipmentIndex);
			if (equipmentDef == RoR2Content.Equipment.DroneBackup)
			{
				Util.PlaySound("Play_item_use_radio", base.gameObject);
				return;
			}
			if (equipmentDef == RoR2Content.Equipment.BFG)
			{
				Transform transform = this.FindActiveEquipmentDisplay();
				if (transform)
				{
					Animator componentInChildren = transform.GetComponentInChildren<Animator>();
					if (componentInChildren)
					{
						componentInChildren.SetTrigger("Fire");
						return;
					}
				}
			}
			else if (equipmentDef == RoR2Content.Equipment.Blackhole)
			{
				Transform transform2 = this.FindActiveEquipmentDisplay();
				if (transform2)
				{
					GravCubeController component = transform2.GetComponent<GravCubeController>();
					if (component)
					{
						component.ActivateCube(9f);
						return;
					}
				}
			}
			else if (equipmentDef == RoR2Content.Equipment.CritOnUse)
			{
				Transform transform3 = this.FindActiveEquipmentDisplay();
				if (transform3)
				{
					Animator componentInChildren2 = transform3.GetComponentInChildren<Animator>();
					if (componentInChildren2)
					{
						componentInChildren2.SetBool("active", true);
						componentInChildren2.SetFloat("activeDuration", 8f);
						componentInChildren2.SetFloat("activeStopwatch", 0f);
						return;
					}
				}
			}
			else if (equipmentDef == RoR2Content.Equipment.GainArmor)
			{
				Util.PlaySound("Play_item_use_gainArmor", base.gameObject);
			}
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000912DC File Offset: 0x0008F4DC
		private void Start()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.inputBank = base.GetComponent<InputBankTest>();
			this.teamComponent = base.GetComponent<TeamComponent>();
			this.targetIndicator = new Indicator(base.gameObject, null);
			this.rng = new Xoroshiro128Plus(Run.instance.seed ^ (ulong)((long)Run.instance.stageClearCount));
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0009134C File Offset: 0x0008F54C
		private void OnDestroy()
		{
			if (this.targetIndicator != null)
			{
				this.targetIndicator.active = false;
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00091364 File Offset: 0x0008F564
		private void FixedUpdate()
		{
			this.UpdateInventory();
			if (NetworkServer.active)
			{
				UnityEngine.Object equipmentDef = EquipmentCatalog.GetEquipmentDef(this.equipmentIndex);
				this.subcooldownTimer -= Time.fixedDeltaTime;
				if (this.missileTimer > 0f)
				{
					this.missileTimer = Mathf.Max(this.missileTimer - Time.fixedDeltaTime, 0f);
				}
				if (this.missileTimer == 0f && this.remainingMissiles > 0)
				{
					this.remainingMissiles--;
					this.missileTimer = 0.125f;
					this.FireMissile();
				}
				this.UpdateGoldGat();
				if (this.bfgChargeTimer > 0f)
				{
					this.bfgChargeTimer -= Time.fixedDeltaTime;
					if (this.bfgChargeTimer < 0f)
					{
						Vector3 position = base.transform.position;
						Ray aimRay = this.GetAimRay();
						Transform transform = this.FindActiveEquipmentDisplay();
						if (transform)
						{
							ChildLocator componentInChildren = transform.GetComponentInChildren<ChildLocator>();
							if (componentInChildren)
							{
								Transform transform2 = componentInChildren.FindChild("Muzzle");
								if (transform2)
								{
									aimRay.origin = transform2.position;
								}
							}
						}
						this.healthComponent.TakeDamageForce(aimRay.direction * -1500f, true, false);
						ProjectileManager.instance.FireProjectile(LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/BeamSphere"), aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.characterBody.damage * 2f, 0f, Util.CheckRoll(this.characterBody.crit, this.characterBody.master), DamageColorIndex.Item, null, -1f);
						this.bfgChargeTimer = 0f;
					}
				}
				if (equipmentDef == RoR2Content.Equipment.PassiveHealing != this.passiveHealingFollower)
				{
					if (!this.passiveHealingFollower)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HealingFollower"), base.transform.position, Quaternion.identity);
						this.passiveHealingFollower = gameObject.GetComponent<HealingFollowerController>();
						this.passiveHealingFollower.NetworkownerBodyObject = base.gameObject;
						NetworkServer.Spawn(gameObject);
					}
					else
					{
						UnityEngine.Object.Destroy(this.passiveHealingFollower.gameObject);
						this.passiveHealingFollower = null;
					}
				}
			}
			bool flag;
			if (!this.inputBank.activateEquipment.justPressed)
			{
				Inventory inventory = this.inventory;
				flag = (((inventory != null) ? inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) : 0) > 0);
			}
			else
			{
				flag = true;
			}
			bool isEquipmentActivationAllowed = this.characterBody.isEquipmentActivationAllowed;
			if (flag && isEquipmentActivationAllowed && this.hasEffectiveAuthority)
			{
				if (NetworkServer.active)
				{
					this.ExecuteIfReady();
					return;
				}
				this.CallCmdExecuteIfReady();
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000915F6 File Offset: 0x0008F7F6
		[Command]
		private void CmdExecuteIfReady()
		{
			this.ExecuteIfReady();
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x000915FF File Offset: 0x0008F7FF
		[Server]
		public bool ExecuteIfReady()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.EquipmentSlot::ExecuteIfReady()' called on client");
				return false;
			}
			if (this.equipmentIndex != EquipmentIndex.None && this.stock > 0)
			{
				this.Execute();
				return true;
			}
			return false;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00091634 File Offset: 0x0008F834
		[Server]
		private void Execute()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.EquipmentSlot::Execute()' called on client");
				return;
			}
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(this.equipmentIndex);
			if (equipmentDef != null && this.subcooldownTimer <= 0f && this.PerformEquipmentAction(equipmentDef))
			{
				this.OnEquipmentExecuted();
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00091681 File Offset: 0x0008F881
		[Command]
		public void CmdOnEquipmentExecuted()
		{
			this.OnEquipmentExecuted();
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0009168C File Offset: 0x0008F88C
		[Server]
		public void OnEquipmentExecuted()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.EquipmentSlot::OnEquipmentExecuted()' called on client");
				return;
			}
			EquipmentIndex equipmentIndex = this.equipmentIndex;
			this.inventory.DeductEquipmentCharges(this.activeEquipmentSlot, 1);
			this.UpdateInventory();
			this.CallRpcOnClientEquipmentActivationRecieved();
			Action<EquipmentSlot, EquipmentIndex> action = EquipmentSlot.onServerEquipmentActivated;
			if (action != null)
			{
				action(this, equipmentIndex);
			}
			if (this.characterBody && this.inventory)
			{
				int itemCount = this.inventory.GetItemCount(RoR2Content.Items.EnergizedOnEquipmentUse);
				if (itemCount > 0)
				{
					this.characterBody.AddTimedBuff(RoR2Content.Buffs.Energized, (float)(8 + 4 * (itemCount - 1)));
				}
				int itemCount2 = this.inventory.GetItemCount(DLC1Content.Items.RandomEquipmentTrigger);
				if (itemCount2 > 0 && EquipmentCatalog.randomTriggerEquipmentList.Count > 0)
				{
					List<EquipmentIndex> list = new List<EquipmentIndex>(EquipmentCatalog.randomTriggerEquipmentList);
					if (this.inventory.currentEquipmentIndex != EquipmentIndex.None)
					{
						list.Remove(this.inventory.currentEquipmentIndex);
					}
					Util.ShuffleList<EquipmentIndex>(list, this.rng);
					if (this.inventory.currentEquipmentIndex != EquipmentIndex.None)
					{
						list.Add(this.inventory.currentEquipmentIndex);
					}
					int num = 0;
					bool flag = false;
					bool flag2 = false;
					int num2 = 0;
					while (num2 < itemCount2 && !flag2)
					{
						EquipmentIndex equipmentIndex2 = EquipmentIndex.None;
						do
						{
							if (num >= list.Count)
							{
								if (!flag)
								{
									goto Block_11;
								}
								flag = false;
								num %= list.Count;
							}
							equipmentIndex2 = list[num];
							num++;
						}
						while (!this.PerformEquipmentAction(EquipmentCatalog.GetEquipmentDef(equipmentIndex2)));
						IL_16B:
						if (equipmentIndex2 == RoR2Content.Equipment.BFG.equipmentIndex)
						{
							ModelLocator component = base.GetComponent<ModelLocator>();
							if (component)
							{
								Transform modelTransform = component.modelTransform;
								if (modelTransform)
								{
									CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
									if (component2)
									{
										List<GameObject> itemDisplayObjects = component2.GetItemDisplayObjects(DLC1Content.Items.RandomEquipmentTrigger.itemIndex);
										if (itemDisplayObjects.Count > 0)
										{
											UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/BFG/ChargeBFG.prefab").WaitForCompletion(), itemDisplayObjects[0].transform);
										}
									}
								}
							}
						}
						flag = true;
						num2++;
						continue;
						Block_11:
						flag2 = true;
						goto IL_16B;
					}
					EffectData effectData = new EffectData();
					effectData.origin = this.characterBody.corePosition;
					effectData.SetNetworkedObjectReference(base.gameObject);
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/RandomEquipmentTriggerProcEffect"), effectData, true);
				}
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060021D8 RID: 8664 RVA: 0x000918D8 File Offset: 0x0008FAD8
		// (remove) Token: 0x060021D9 RID: 8665 RVA: 0x0009190C File Offset: 0x0008FB0C
		public static event Action<EquipmentSlot, EquipmentIndex> onServerEquipmentActivated;

		// Token: 0x060021DA RID: 8666 RVA: 0x00091940 File Offset: 0x0008FB40
		private void FireMissile()
		{
			GameObject projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/MissileProjectile");
			float num = 3f;
			bool isCrit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master);
			MissileUtils.FireMissile(this.characterBody.corePosition, this.characterBody, default(ProcChainMask), null, this.characterBody.damage * num, isCrit, projectilePrefab, DamageColorIndex.Item, false);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000919AC File Offset: 0x0008FBAC
		[Server]
		private bool PerformEquipmentAction(EquipmentDef equipmentDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.EquipmentSlot::PerformEquipmentAction(RoR2.EquipmentDef)' called on client");
				return false;
			}
			Func<bool> func = null;
			if (equipmentDef == RoR2Content.Equipment.CommandMissile)
			{
				func = new Func<bool>(this.FireCommandMissile);
			}
			else if (equipmentDef == RoR2Content.Equipment.Fruit)
			{
				func = new Func<bool>(this.FireFruit);
			}
			else if (equipmentDef == RoR2Content.Equipment.DroneBackup)
			{
				func = new Func<bool>(this.FireDroneBackup);
			}
			else if (equipmentDef == RoR2Content.Equipment.Meteor)
			{
				func = new Func<bool>(this.FireMeteor);
			}
			else if (equipmentDef == RoR2Content.Equipment.Blackhole)
			{
				func = new Func<bool>(this.FireBlackhole);
			}
			else if (equipmentDef == RoR2Content.Equipment.Saw)
			{
				func = new Func<bool>(this.FireSaw);
			}
			else if (equipmentDef == JunkContent.Equipment.OrbitalLaser)
			{
				func = new Func<bool>(this.FireOrbitalLaser);
			}
			else if (equipmentDef == JunkContent.Equipment.GhostGun)
			{
				func = new Func<bool>(this.FireGhostGun);
			}
			else if (equipmentDef == RoR2Content.Equipment.CritOnUse)
			{
				func = new Func<bool>(this.FireCritOnUse);
			}
			else if (equipmentDef == RoR2Content.Equipment.BFG)
			{
				func = new Func<bool>(this.FireBfg);
			}
			else if (equipmentDef == RoR2Content.Equipment.Jetpack)
			{
				func = new Func<bool>(this.FireJetpack);
			}
			else if (equipmentDef == RoR2Content.Equipment.Lightning)
			{
				func = new Func<bool>(this.FireLightning);
			}
			else if (equipmentDef == RoR2Content.Equipment.PassiveHealing)
			{
				func = new Func<bool>(this.FirePassiveHealing);
			}
			else if (equipmentDef == RoR2Content.Equipment.BurnNearby)
			{
				func = new Func<bool>(this.FireBurnNearby);
			}
			else if (equipmentDef == JunkContent.Equipment.SoulCorruptor)
			{
				func = new Func<bool>(this.FireSoulCorruptor);
			}
			else if (equipmentDef == RoR2Content.Equipment.Scanner)
			{
				func = new Func<bool>(this.FireScanner);
			}
			else if (equipmentDef == RoR2Content.Equipment.CrippleWard)
			{
				func = new Func<bool>(this.FireCrippleWard);
			}
			else if (equipmentDef == RoR2Content.Equipment.Gateway)
			{
				func = new Func<bool>(this.FireGateway);
			}
			else if (equipmentDef == RoR2Content.Equipment.Tonic)
			{
				func = new Func<bool>(this.FireTonic);
			}
			else if (equipmentDef == RoR2Content.Equipment.Cleanse)
			{
				func = new Func<bool>(this.FireCleanse);
			}
			else if (equipmentDef == RoR2Content.Equipment.FireBallDash)
			{
				func = new Func<bool>(this.FireFireBallDash);
			}
			else if (equipmentDef == RoR2Content.Equipment.Recycle)
			{
				func = new Func<bool>(this.FireRecycle);
			}
			else if (equipmentDef == RoR2Content.Equipment.GainArmor)
			{
				func = new Func<bool>(this.FireGainArmor);
			}
			else if (equipmentDef == RoR2Content.Equipment.LifestealOnHit)
			{
				func = new Func<bool>(this.FireLifeStealOnHit);
			}
			else if (equipmentDef == RoR2Content.Equipment.TeamWarCry)
			{
				func = new Func<bool>(this.FireTeamWarCry);
			}
			else if (equipmentDef == RoR2Content.Equipment.DeathProjectile)
			{
				func = new Func<bool>(this.FireDeathProjectile);
			}
			else if (equipmentDef == DLC1Content.Equipment.Molotov)
			{
				func = new Func<bool>(this.FireMolotov);
			}
			else if (equipmentDef == DLC1Content.Equipment.VendingMachine)
			{
				func = new Func<bool>(this.FireVendingMachine);
			}
			else if (equipmentDef == DLC1Content.Equipment.BossHunter)
			{
				func = new Func<bool>(this.FireBossHunter);
			}
			else if (equipmentDef == DLC1Content.Equipment.BossHunterConsumed)
			{
				func = new Func<bool>(this.FireBossHunterConsumed);
			}
			else if (equipmentDef == DLC1Content.Equipment.GummyClone)
			{
				func = new Func<bool>(this.FireGummyClone);
			}
			else if (equipmentDef == DLC1Content.Equipment.LunarPortalOnUse)
			{
				func = new Func<bool>(this.FireLunarPortalOnUse);
			}
			return func != null && func();
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00091DAC File Offset: 0x0008FFAC
		private Ray GetAimRay()
		{
			return new Ray
			{
				direction = this.inputBank.aimDirection,
				origin = this.inputBank.aimOrigin
			};
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00091DE8 File Offset: 0x0008FFE8
		[Server]
		[CanBeNull]
		private CharacterMaster SummonMaster([NotNull] GameObject masterPrefab, Vector3 position, Quaternion rotation)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterMaster RoR2.EquipmentSlot::SummonMaster(UnityEngine.GameObject,UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
				return null;
			}
			return new MasterSummon
			{
				masterPrefab = masterPrefab,
				position = position,
				rotation = rotation,
				summonerBodyObject = base.gameObject,
				ignoreTeamMemberLimit = false
			}.Perform();
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00091E48 File Offset: 0x00090048
		private void ConfigureTargetFinderBase()
		{
			this.targetFinder.teamMaskFilter = TeamMask.allButNeutral;
			this.targetFinder.teamMaskFilter.RemoveTeam(this.teamComponent.teamIndex);
			this.targetFinder.sortMode = BullseyeSearch.SortMode.Angle;
			this.targetFinder.filterByLoS = true;
			float num;
			Ray ray = CameraRigController.ModifyAimRayIfApplicable(this.GetAimRay(), base.gameObject, out num);
			this.targetFinder.searchOrigin = ray.origin;
			this.targetFinder.searchDirection = ray.direction;
			this.targetFinder.maxAngleFilter = 10f;
			this.targetFinder.viewer = this.characterBody;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00091EF1 File Offset: 0x000900F1
		private void ConfigureTargetFinderForEnemies()
		{
			this.ConfigureTargetFinderBase();
			this.targetFinder.teamMaskFilter = TeamMask.GetUnprotectedTeams(this.teamComponent.teamIndex);
			this.targetFinder.RefreshCandidates();
			this.targetFinder.FilterOutGameObject(base.gameObject);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00091EF1 File Offset: 0x000900F1
		private void ConfigureTargetFinderForBossesWithRewards()
		{
			this.ConfigureTargetFinderBase();
			this.targetFinder.teamMaskFilter = TeamMask.GetUnprotectedTeams(this.teamComponent.teamIndex);
			this.targetFinder.RefreshCandidates();
			this.targetFinder.FilterOutGameObject(base.gameObject);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00091F30 File Offset: 0x00090130
		private void ConfigureTargetFinderForFriendlies()
		{
			this.ConfigureTargetFinderBase();
			this.targetFinder.teamMaskFilter = TeamMask.none;
			this.targetFinder.teamMaskFilter.AddTeam(this.teamComponent.teamIndex);
			this.targetFinder.RefreshCandidates();
			this.targetFinder.FilterOutGameObject(base.gameObject);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00091F8A File Offset: 0x0009018A
		private void InvalidateCurrentTarget()
		{
			this.currentTarget = default(EquipmentSlot.UserTargetInfo);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x00091F98 File Offset: 0x00090198
		private void UpdateTargets(EquipmentIndex targetingEquipmentIndex, bool userShouldAnticipateTarget)
		{
			bool flag = targetingEquipmentIndex == DLC1Content.Equipment.BossHunter.equipmentIndex;
			bool flag2 = (targetingEquipmentIndex == RoR2Content.Equipment.Lightning.equipmentIndex || targetingEquipmentIndex == JunkContent.Equipment.SoulCorruptor.equipmentIndex || flag) && userShouldAnticipateTarget;
			bool flag3 = targetingEquipmentIndex == RoR2Content.Equipment.PassiveHealing.equipmentIndex && userShouldAnticipateTarget;
			bool flag4 = flag2 || flag3;
			bool flag5 = targetingEquipmentIndex == RoR2Content.Equipment.Recycle.equipmentIndex;
			if (flag4)
			{
				if (flag2)
				{
					this.ConfigureTargetFinderForEnemies();
				}
				if (flag3)
				{
					this.ConfigureTargetFinderForFriendlies();
				}
				HurtBox source = null;
				if (flag)
				{
					using (IEnumerator<HurtBox> enumerator = this.targetFinder.GetResults().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							HurtBox hurtBox = enumerator.Current;
							if (hurtBox && hurtBox.healthComponent && hurtBox.healthComponent.body)
							{
								DeathRewards component = hurtBox.healthComponent.body.gameObject.GetComponent<DeathRewards>();
								if (component && component.bossDropTable && !hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.Immune))
								{
									source = hurtBox;
									break;
								}
							}
						}
						goto IL_134;
					}
				}
				source = this.targetFinder.GetResults().FirstOrDefault<HurtBox>();
				IL_134:
				this.currentTarget = new EquipmentSlot.UserTargetInfo(source);
			}
			else if (flag5)
			{
				this.currentTarget = new EquipmentSlot.UserTargetInfo(this.FindPickupController(this.GetAimRay(), 10f, 30f, true, targetingEquipmentIndex == RoR2Content.Equipment.Recycle.equipmentIndex));
			}
			else
			{
				this.currentTarget = default(EquipmentSlot.UserTargetInfo);
			}
			GenericPickupController pickupController = this.currentTarget.pickupController;
			bool flag6 = this.currentTarget.transformToIndicateAt;
			if (flag6)
			{
				if (targetingEquipmentIndex == RoR2Content.Equipment.Lightning.equipmentIndex)
				{
					this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/LightningIndicator");
				}
				else if (targetingEquipmentIndex == RoR2Content.Equipment.PassiveHealing.equipmentIndex)
				{
					this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/WoodSpriteIndicator");
				}
				else if (targetingEquipmentIndex == RoR2Content.Equipment.Recycle.equipmentIndex)
				{
					if (!pickupController.Recycled)
					{
						this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator");
					}
					else
					{
						this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerBadIndicator");
					}
				}
				else if (targetingEquipmentIndex == DLC1Content.Equipment.BossHunter.equipmentIndex)
				{
					this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/BossHunterIndicator");
				}
				else
				{
					this.targetIndicator.visualizerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/LightningIndicator");
				}
			}
			this.targetIndicator.active = flag6;
			this.targetIndicator.targetTransform = (flag6 ? this.currentTarget.transformToIndicateAt : null);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00092250 File Offset: 0x00090450
		private GenericPickupController FindPickupController(Ray aimRay, float maxAngle, float maxDistance, bool requireLoS, bool requireTransmutable)
		{
			if (this.pickupSearch == null)
			{
				this.pickupSearch = new PickupSearch();
			}
			float num;
			aimRay = CameraRigController.ModifyAimRayIfApplicable(aimRay, base.gameObject, out num);
			this.pickupSearch.searchOrigin = aimRay.origin;
			this.pickupSearch.searchDirection = aimRay.direction;
			this.pickupSearch.minAngleFilter = 0f;
			this.pickupSearch.maxAngleFilter = maxAngle;
			this.pickupSearch.minDistanceFilter = 0f;
			this.pickupSearch.maxDistanceFilter = maxDistance + num;
			this.pickupSearch.filterByDistinctEntity = false;
			this.pickupSearch.filterByLoS = requireLoS;
			this.pickupSearch.sortMode = SortMode.DistanceAndAngle;
			this.pickupSearch.requireTransmutable = requireTransmutable;
			return this.pickupSearch.SearchCandidatesForSingleTarget<List<GenericPickupController>>(InstanceTracker.GetInstancesList<GenericPickupController>());
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00092320 File Offset: 0x00090520
		private void Update()
		{
			this.UpdateTargets(this.equipmentIndex, this.stock > 0);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00092337 File Offset: 0x00090537
		private bool FireCommandMissile()
		{
			this.remainingMissiles += 12;
			return true;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0009234C File Offset: 0x0009054C
		private bool FireFruit()
		{
			if (this.healthComponent)
			{
				EffectData effectData = new EffectData();
				effectData.origin = base.transform.position;
				effectData.SetNetworkedObjectReference(base.gameObject);
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/FruitHealEffect"), effectData, true);
				this.healthComponent.HealFraction(0.5f, default(ProcChainMask));
			}
			return true;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000923B8 File Offset: 0x000905B8
		private bool FireDroneBackup()
		{
			int sliceCount = 4;
			float num = 25f;
			if (NetworkServer.active)
			{
				float y = Quaternion.LookRotation(this.GetAimRay().direction).eulerAngles.y;
				float d = 3f;
				foreach (float num2 in new DegreeSlices(sliceCount, 0.5f))
				{
					Quaternion rotation = Quaternion.Euler(-30f, y + num2, 0f);
					Quaternion rotation2 = Quaternion.Euler(0f, y + num2 + 180f, 0f);
					Vector3 position = base.transform.position + rotation * (Vector3.forward * d);
					CharacterMaster characterMaster = this.SummonMaster(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/DroneBackupMaster"), position, rotation2);
					if (characterMaster)
					{
						characterMaster.gameObject.AddComponent<MasterSuicideOnTimer>().lifeTimer = num + UnityEngine.Random.Range(0f, 3f);
					}
				}
			}
			this.subcooldownTimer = 0.5f;
			return true;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000924F4 File Offset: 0x000906F4
		private bool FireMeteor()
		{
			MeteorStormController component = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MeteorStorm"), this.characterBody.corePosition, Quaternion.identity).GetComponent<MeteorStormController>();
			component.owner = base.gameObject;
			component.ownerDamage = this.characterBody.damage;
			component.isCrit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master);
			NetworkServer.Spawn(component.gameObject);
			return true;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00092570 File Offset: 0x00090770
		private bool FireBlackhole()
		{
			Vector3 position = base.transform.position;
			Ray aimRay = this.GetAimRay();
			ProjectileManager.instance.FireProjectile(LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/GravSphere"), position, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, 0f, 0f, false, DamageColorIndex.Default, null, -1f);
			return true;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000925CC File Offset: 0x000907CC
		private bool FireSaw()
		{
			Ray aimRay = this.GetAimRay();
			Quaternion quaternion = Quaternion.LookRotation(aimRay.direction);
			float num = 15f;
			this.<FireSaw>g__FireSingleSaw|80_0(this.characterBody, aimRay.origin, quaternion * Quaternion.Euler(0f, -num, 0f));
			this.<FireSaw>g__FireSingleSaw|80_0(this.characterBody, aimRay.origin, quaternion);
			this.<FireSaw>g__FireSingleSaw|80_0(this.characterBody, aimRay.origin, quaternion * Quaternion.Euler(0f, num, 0f));
			return true;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0009265C File Offset: 0x0009085C
		private bool FireOrbitalLaser()
		{
			Vector3 position = base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(this.GetAimRay(), out raycastHit, 900f, LayerIndex.world.mask | LayerIndex.defaultLayer.mask))
			{
				position = raycastHit.point;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/OrbitalLaser"), position, Quaternion.identity);
			gameObject.GetComponent<OrbitalLaserController>().ownerBody = this.characterBody;
			NetworkServer.Spawn(gameObject);
			return true;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000926E2 File Offset: 0x000908E2
		private bool FireGhostGun()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GhostGun"), base.transform.position, Quaternion.identity);
			gameObject.GetComponent<GhostGunController>().owner = base.gameObject;
			NetworkServer.Spawn(gameObject);
			return true;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0009271A File Offset: 0x0009091A
		private bool FireCritOnUse()
		{
			this.characterBody.AddTimedBuff(RoR2Content.Buffs.FullCrit, 8f);
			return true;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00092732 File Offset: 0x00090932
		private bool FireBfg()
		{
			this.bfgChargeTimer = 2f;
			this.subcooldownTimer = 2.2f;
			return true;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0009274C File Offset: 0x0009094C
		private bool FireJetpack()
		{
			JetpackController jetpackController = JetpackController.FindJetpackController(base.gameObject);
			if (!jetpackController)
			{
				UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/JetpackController")).GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
			}
			else
			{
				jetpackController.ResetTimer();
			}
			return true;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00092798 File Offset: 0x00090998
		private bool FireLightning()
		{
			this.UpdateTargets(RoR2Content.Equipment.Lightning.equipmentIndex, true);
			HurtBox hurtBox = this.currentTarget.hurtBox;
			if (hurtBox)
			{
				this.subcooldownTimer = 0.2f;
				OrbManager.instance.AddOrb(new LightningStrikeOrb
				{
					attacker = base.gameObject,
					damageColorIndex = DamageColorIndex.Item,
					damageValue = this.characterBody.damage * 30f,
					isCrit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master),
					procChainMask = default(ProcChainMask),
					procCoefficient = 1f,
					target = hurtBox
				});
				this.InvalidateCurrentTarget();
				return true;
			}
			return false;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0009285C File Offset: 0x00090A5C
		private bool FireBossHunter()
		{
			this.UpdateTargets(DLC1Content.Equipment.BossHunter.equipmentIndex, true);
			HurtBox hurtBox = this.currentTarget.hurtBox;
			DeathRewards deathRewards;
			if (hurtBox == null)
			{
				deathRewards = null;
			}
			else
			{
				HealthComponent healthComponent = hurtBox.healthComponent;
				if (healthComponent == null)
				{
					deathRewards = null;
				}
				else
				{
					CharacterBody body = healthComponent.body;
					if (body == null)
					{
						deathRewards = null;
					}
					else
					{
						GameObject gameObject = body.gameObject;
						deathRewards = ((gameObject != null) ? gameObject.GetComponent<DeathRewards>() : null);
					}
				}
			}
			DeathRewards deathRewards2 = deathRewards;
			if (hurtBox && deathRewards2)
			{
				Vector3 vector = hurtBox.transform ? hurtBox.transform.position : Vector3.zero;
				Vector3 normalized = (vector - this.characterBody.corePosition).normalized;
				PickupDropletController.CreatePickupDroplet(deathRewards2.bossDropTable.GenerateDrop(this.rng), vector, normalized * 15f);
				UnityEngine.Object exists;
				if (hurtBox == null)
				{
					exists = null;
				}
				else
				{
					HealthComponent healthComponent2 = hurtBox.healthComponent;
					if (healthComponent2 == null)
					{
						exists = null;
					}
					else
					{
						CharacterBody body2 = healthComponent2.body;
						exists = ((body2 != null) ? body2.master : null);
					}
				}
				if (exists)
				{
					hurtBox.healthComponent.body.master.TrueKill(base.gameObject, null, DamageType.Generic);
				}
				CharacterModel component = hurtBox.hurtBoxGroup.GetComponent<CharacterModel>();
				if (component)
				{
					TemporaryOverlay temporaryOverlay = component.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = 0.1f;
					temporaryOverlay.animateShaderAlpha = true;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
					temporaryOverlay.AddToCharacerModel(component);
					TemporaryOverlay temporaryOverlay2 = component.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay2.duration = 1.2f;
					temporaryOverlay2.animateShaderAlpha = true;
					temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay2.destroyComponentOnEnd = true;
					temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGhostEffect");
					temporaryOverlay2.AddToCharacerModel(component);
				}
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.attacker = base.gameObject;
				damageInfo.force = -normalized * 2500f;
				this.healthComponent.TakeDamageForce(damageInfo, true, false);
				GameObject effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BossHunterKillEffect");
				Quaternion rotation = Util.QuaternionSafeLookRotation(normalized, Vector3.up);
				EffectManager.SpawnEffect(effectPrefab, new EffectData
				{
					origin = vector,
					rotation = rotation
				}, true);
				ModelLocator component2 = base.gameObject.GetComponent<ModelLocator>();
				CharacterModel characterModel;
				if (component2 == null)
				{
					characterModel = null;
				}
				else
				{
					Transform modelTransform = component2.modelTransform;
					characterModel = ((modelTransform != null) ? modelTransform.GetComponent<CharacterModel>() : null);
				}
				CharacterModel characterModel2 = characterModel;
				if (characterModel2)
				{
					foreach (GameObject gameObject2 in characterModel2.GetEquipmentDisplayObjects(DLC1Content.Equipment.BossHunter.equipmentIndex))
					{
						if (gameObject2.name.Contains("DisplayTricorn"))
						{
							EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BossHunterHatEffect"), new EffectData
							{
								origin = gameObject2.transform.position,
								rotation = gameObject2.transform.rotation,
								scale = gameObject2.transform.localScale.x
							}, true);
						}
						else
						{
							EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BossHunterGunEffect"), new EffectData
							{
								origin = gameObject2.transform.position,
								rotation = Util.QuaternionSafeLookRotation(vector - gameObject2.transform.position, Vector3.up),
								scale = gameObject2.transform.localScale.x
							}, true);
						}
					}
				}
				CharacterBody characterBody = this.characterBody;
				if ((characterBody != null) ? characterBody.inventory : null)
				{
					CharacterMasterNotificationQueue.SendTransformNotification(this.characterBody.master, this.characterBody.inventory.currentEquipmentIndex, DLC1Content.Equipment.BossHunterConsumed.equipmentIndex, CharacterMasterNotificationQueue.TransformationType.Default);
					this.characterBody.inventory.SetEquipmentIndex(DLC1Content.Equipment.BossHunterConsumed.equipmentIndex);
				}
				this.InvalidateCurrentTarget();
				return true;
			}
			return false;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00092C60 File Offset: 0x00090E60
		private bool FireBossHunterConsumed()
		{
			if (this.characterBody)
			{
				Chat.SendBroadcastChat(new Chat.BodyChatMessage
				{
					bodyObject = this.characterBody.gameObject,
					token = "EQUIPMENT_BOSSHUNTERCONSUMED_CHAT"
				});
				this.subcooldownTimer = 1f;
			}
			return true;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00092CAC File Offset: 0x00090EAC
		private bool FirePassiveHealing()
		{
			this.UpdateTargets(RoR2Content.Equipment.PassiveHealing.equipmentIndex, true);
			GameObject rootObject = this.currentTarget.rootObject;
			CharacterBody characterBody = ((rootObject != null) ? rootObject.GetComponent<CharacterBody>() : null) ?? this.characterBody;
			if (characterBody)
			{
				EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/WoodSpriteHeal"), characterBody.corePosition, Vector3.up, true);
				HealthComponent healthComponent = characterBody.healthComponent;
				if (healthComponent != null)
				{
					healthComponent.HealFraction(0.1f, default(ProcChainMask));
				}
			}
			if (this.passiveHealingFollower)
			{
				this.passiveHealingFollower.AssignNewTarget(this.currentTarget.rootObject);
				this.InvalidateCurrentTarget();
			}
			return true;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00092D59 File Offset: 0x00090F59
		private bool FireBurnNearby()
		{
			if (this.characterBody)
			{
				this.characterBody.AddHelfireDuration(12f);
			}
			return true;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00092D7C File Offset: 0x00090F7C
		private bool FireSoulCorruptor()
		{
			this.UpdateTargets(JunkContent.Equipment.SoulCorruptor.equipmentIndex, true);
			HurtBox hurtBox = this.currentTarget.hurtBox;
			if (!hurtBox)
			{
				return false;
			}
			if (!hurtBox.healthComponent || hurtBox.healthComponent.combinedHealthFraction > 0.25f)
			{
				return false;
			}
			Util.TryToCreateGhost(hurtBox.healthComponent.body, this.characterBody, 30);
			hurtBox.healthComponent.Suicide(base.gameObject, null, DamageType.Generic);
			this.InvalidateCurrentTarget();
			return true;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00092E04 File Offset: 0x00091004
		private bool FireScanner()
		{
			NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/ChestScanner"), this.characterBody.corePosition, Quaternion.identity));
			return true;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00092E2C File Offset: 0x0009102C
		private bool FireCrippleWard()
		{
			this.characterBody.master.GetDeployableCount(DeployableSlot.PowerWard);
			Ray aimRay = this.GetAimRay();
			float maxDistance = 1000f;
			RaycastHit raycastHit;
			if (Physics.Raycast(aimRay, out raycastHit, maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/CrippleWard"), raycastHit.point, Util.QuaternionSafeLookRotation(raycastHit.normal, Vector3.forward));
				Deployable component = gameObject.GetComponent<Deployable>();
				this.characterBody.master.AddDeployable(component, DeployableSlot.CrippleWard);
				NetworkServer.Spawn(gameObject);
				return true;
			}
			return true;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00092EC0 File Offset: 0x000910C0
		private bool FireTonic()
		{
			this.characterBody.AddTimedBuff(RoR2Content.Buffs.TonicBuff, EquipmentSlot.tonicBuffDuration);
			if (!Util.CheckRoll(80f, this.characterBody.master))
			{
				this.characterBody.pendingTonicAfflictionCount++;
			}
			return true;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00092F10 File Offset: 0x00091110
		private bool FireCleanse()
		{
			Vector3 corePosition = this.characterBody.corePosition;
			EffectData effectData = new EffectData
			{
				origin = corePosition
			};
			effectData.SetHurtBoxReference(this.characterBody.mainHurtBox);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/CleanseEffect"), effectData, true);
			Util.CleanseBody(this.characterBody, true, false, true, true, true, true);
			return true;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00092F6C File Offset: 0x0009116C
		private bool FireFireBallDash()
		{
			Ray aimRay = this.GetAimRay();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/FireballVehicle"), aimRay.origin, Quaternion.LookRotation(aimRay.direction));
			gameObject.GetComponent<VehicleSeat>().AssignPassenger(base.gameObject);
			CharacterBody characterBody = this.characterBody;
			NetworkUser networkUser;
			if (characterBody == null)
			{
				networkUser = null;
			}
			else
			{
				CharacterMaster master = characterBody.master;
				if (master == null)
				{
					networkUser = null;
				}
				else
				{
					PlayerCharacterMasterController playerCharacterMasterController = master.playerCharacterMasterController;
					networkUser = ((playerCharacterMasterController != null) ? playerCharacterMasterController.networkUser : null);
				}
			}
			NetworkUser networkUser2 = networkUser;
			if (networkUser2)
			{
				NetworkServer.SpawnWithClientAuthority(gameObject, networkUser2.gameObject);
			}
			else
			{
				NetworkServer.Spawn(gameObject);
			}
			this.subcooldownTimer = 2f;
			return true;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00093009 File Offset: 0x00091209
		private bool FireGainArmor()
		{
			this.characterBody.AddTimedBuff(RoR2Content.Buffs.ElephantArmorBoost, 5f);
			return true;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00093024 File Offset: 0x00091224
		private unsafe bool FireRecycle()
		{
			this.UpdateTargets(RoR2Content.Equipment.Recycle.equipmentIndex, false);
			GenericPickupController pickupController = this.currentTarget.pickupController;
			if (!pickupController || pickupController.Recycled)
			{
				return false;
			}
			PickupIndex initialPickupIndex = pickupController.pickupIndex;
			this.subcooldownTimer = 0.2f;
			PickupIndex[] array = (from pickupIndex in PickupTransmutationManager.GetAvailableGroupFromPickupIndex(pickupController.pickupIndex)
			where pickupIndex != initialPickupIndex
			select pickupIndex).ToArray<PickupIndex>();
			if (array == null)
			{
				return false;
			}
			if (array.Length == 0)
			{
				return false;
			}
			pickupController.NetworkpickupIndex = *Run.instance.treasureRng.NextElementUniform<PickupIndex>(array);
			EffectManager.SimpleEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OmniEffect/OmniRecycleEffect"), pickupController.pickupDisplay.transform.position, Quaternion.identity, true);
			pickupController.NetworkRecycled = true;
			this.InvalidateCurrentTarget();
			return true;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00093100 File Offset: 0x00091300
		private bool FireGateway()
		{
			Vector3 footPosition = this.characterBody.footPosition;
			Ray aimRay = this.GetAimRay();
			float num = 2f;
			float num2 = num * 2f;
			float maxDistance = 1000f;
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (!component)
			{
				return false;
			}
			Vector3 position = base.transform.position;
			RaycastHit raycastHit;
			if (Physics.Raycast(aimRay, out raycastHit, maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				Vector3 vector = raycastHit.point + raycastHit.normal * num;
				Vector3 vector2 = vector - position;
				Vector3 normalized = vector2.normalized;
				Vector3 pointBPosition = vector;
				RaycastHit raycastHit2;
				if (component.SweepTest(normalized, out raycastHit2, vector2.magnitude))
				{
					if (raycastHit2.distance < num2)
					{
						return false;
					}
					pointBPosition = position + normalized * raycastHit2.distance;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Zipline"));
				ZiplineController component2 = gameObject.GetComponent<ZiplineController>();
				component2.SetPointAPosition(position + normalized * num);
				component2.SetPointBPosition(pointBPosition);
				gameObject.AddComponent<DestroyOnTimer>().duration = 30f;
				NetworkServer.Spawn(gameObject);
				return true;
			}
			return false;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00093224 File Offset: 0x00091424
		private bool FireLifeStealOnHit()
		{
			EffectData effectData = new EffectData
			{
				origin = this.characterBody.corePosition
			};
			effectData.SetHurtBoxReference(this.characterBody.gameObject);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/LifeStealOnHitActivation"), effectData, false);
			this.characterBody.AddTimedBuff(RoR2Content.Buffs.LifeSteal, 8f);
			return true;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00093280 File Offset: 0x00091480
		private bool FireTeamWarCry()
		{
			Util.PlaySound("Play_teamWarCry_activate", this.characterBody.gameObject);
			Vector3 corePosition = this.characterBody.corePosition;
			EffectData effectData = new EffectData
			{
				origin = corePosition
			};
			effectData.SetNetworkedObjectReference(this.characterBody.gameObject);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TeamWarCryActivation"), effectData, true);
			this.characterBody.AddTimedBuff(RoR2Content.Buffs.TeamWarCry, 7f);
			TeamComponent[] array = UnityEngine.Object.FindObjectsOfType<TeamComponent>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].teamIndex == this.teamComponent.teamIndex)
				{
					array[i].GetComponent<CharacterBody>().AddTimedBuff(RoR2Content.Buffs.TeamWarCry, 7f);
				}
			}
			return true;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00093334 File Offset: 0x00091534
		private bool FireDeathProjectile()
		{
			CharacterMaster master = this.characterBody.master;
			if (!master)
			{
				return false;
			}
			if (master.IsDeployableLimited(DeployableSlot.DeathProjectile))
			{
				return false;
			}
			Ray aimRay = this.GetAimRay();
			Quaternion rotation = Quaternion.LookRotation(aimRay.direction);
			GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/DeathProjectile");
			gameObject.GetComponent<DeathProjectile>().teamIndex = this.teamComponent.teamIndex;
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = gameObject,
				crit = this.characterBody.RollCrit(),
				damage = this.characterBody.damage,
				damageColorIndex = DamageColorIndex.Item,
				force = 0f,
				owner = base.gameObject,
				position = aimRay.origin,
				rotation = rotation
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			return true;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00093414 File Offset: 0x00091614
		private bool FireMolotov()
		{
			Ray aimRay = this.GetAimRay();
			GameObject prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/MolotovClusterProjectile");
			ProjectileManager.instance.FireProjectile(prefab, aimRay.origin, Quaternion.LookRotation(aimRay.direction), base.gameObject, this.characterBody.damage, 0f, Util.CheckRoll(this.characterBody.crit, this.characterBody.master), DamageColorIndex.Default, null, -1f);
			return true;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0009348C File Offset: 0x0009168C
		private bool FireVendingMachine()
		{
			Ray ray = new Ray(this.GetAimRay().origin, Vector3.down);
			RaycastHit raycastHit;
			if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, 1000f, LayerIndex.world.mask, QueryTriggerInteraction.UseGlobal))
			{
				GameObject prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/VendingMachineProjectile");
				ProjectileManager.instance.FireProjectile(prefab, raycastHit.point, Quaternion.identity, base.gameObject, this.characterBody.damage, 0f, Util.CheckRoll(this.characterBody.crit, this.characterBody.master), DamageColorIndex.Default, null, -1f);
				this.subcooldownTimer = 0.5f;
				return true;
			}
			return false;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00093540 File Offset: 0x00091740
		private bool FireGummyClone()
		{
			CharacterBody characterBody = this.characterBody;
			CharacterMaster characterMaster = (characterBody != null) ? characterBody.master : null;
			if (!characterMaster || characterMaster.IsDeployableLimited(DeployableSlot.GummyClone))
			{
				return false;
			}
			Ray aimRay = this.GetAimRay();
			Quaternion rotation = Quaternion.LookRotation(aimRay.direction);
			GameObject projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/GummyCloneProjectile");
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = projectilePrefab,
				crit = this.characterBody.RollCrit(),
				damage = 0f,
				damageColorIndex = DamageColorIndex.Item,
				force = 0f,
				owner = base.gameObject,
				position = aimRay.origin,
				rotation = rotation
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			return true;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00093609 File Offset: 0x00091809
		private bool FireLunarPortalOnUse()
		{
			TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal = true;
			return true;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0009362C File Offset: 0x0009182C
		static EquipmentSlot()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(EquipmentSlot), EquipmentSlot.kCmdCmdExecuteIfReady, new NetworkBehaviour.CmdDelegate(EquipmentSlot.InvokeCmdCmdExecuteIfReady));
			EquipmentSlot.kCmdCmdOnEquipmentExecuted = 1725820338;
			NetworkBehaviour.RegisterCommandDelegate(typeof(EquipmentSlot), EquipmentSlot.kCmdCmdOnEquipmentExecuted, new NetworkBehaviour.CmdDelegate(EquipmentSlot.InvokeCmdCmdOnEquipmentExecuted));
			EquipmentSlot.kRpcRpcOnClientEquipmentActivationRecieved = 1342577121;
			NetworkBehaviour.RegisterRpcDelegate(typeof(EquipmentSlot), EquipmentSlot.kRpcRpcOnClientEquipmentActivationRecieved, new NetworkBehaviour.CmdDelegate(EquipmentSlot.InvokeRpcRpcOnClientEquipmentActivationRecieved));
			NetworkCRC.RegisterBehaviour("EquipmentSlot", 0);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000936DC File Offset: 0x000918DC
		[CompilerGenerated]
		private void <FireSaw>g__FireSingleSaw|80_0(CharacterBody firingCharacterBody, Vector3 origin, Quaternion rotation)
		{
			GameObject projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/Sawmerang");
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = projectilePrefab,
				crit = this.characterBody.RollCrit(),
				damage = this.characterBody.damage,
				damageColorIndex = DamageColorIndex.Item,
				force = 0f,
				owner = base.gameObject,
				position = origin,
				rotation = rotation
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00093766 File Offset: 0x00091966
		protected static void InvokeCmdCmdExecuteIfReady(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdExecuteIfReady called on client.");
				return;
			}
			((EquipmentSlot)obj).CmdExecuteIfReady();
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00093789 File Offset: 0x00091989
		protected static void InvokeCmdCmdOnEquipmentExecuted(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdOnEquipmentExecuted called on client.");
				return;
			}
			((EquipmentSlot)obj).CmdOnEquipmentExecuted();
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000937AC File Offset: 0x000919AC
		public void CallCmdExecuteIfReady()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdExecuteIfReady called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdExecuteIfReady();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)EquipmentSlot.kCmdCmdExecuteIfReady);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdExecuteIfReady");
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00093828 File Offset: 0x00091A28
		public void CallCmdOnEquipmentExecuted()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdOnEquipmentExecuted called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdOnEquipmentExecuted();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)EquipmentSlot.kCmdCmdOnEquipmentExecuted);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdOnEquipmentExecuted");
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000938A4 File Offset: 0x00091AA4
		protected static void InvokeRpcRpcOnClientEquipmentActivationRecieved(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnClientEquipmentActivationRecieved called on server.");
				return;
			}
			((EquipmentSlot)obj).RpcOnClientEquipmentActivationRecieved();
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000938C8 File Offset: 0x00091AC8
		public void CallRpcOnClientEquipmentActivationRecieved()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnClientEquipmentActivationRecieved called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)EquipmentSlot.kRpcRpcOnClientEquipmentActivationRecieved);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnClientEquipmentActivationRecieved");
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00093934 File Offset: 0x00091B34
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400271F RID: 10015
		private Inventory inventory;

		// Token: 0x04002724 RID: 10020
		private Run.FixedTimeStamp _rechargeTime;

		// Token: 0x04002725 RID: 10021
		private bool hasEffectiveAuthority;

		// Token: 0x04002726 RID: 10022
		private Xoroshiro128Plus rng;

		// Token: 0x04002728 RID: 10024
		private HealthComponent healthComponent;

		// Token: 0x04002729 RID: 10025
		private InputBankTest inputBank;

		// Token: 0x0400272A RID: 10026
		private TeamComponent teamComponent;

		// Token: 0x0400272B RID: 10027
		private const float fullCritDuration = 8f;

		// Token: 0x0400272C RID: 10028
		private static readonly float tonicBuffDuration = 20f;

		// Token: 0x0400272D RID: 10029
		public static string equipmentActivateString = "Play_UI_equipment_activate";

		// Token: 0x0400272E RID: 10030
		private float missileTimer;

		// Token: 0x0400272F RID: 10031
		private float bfgChargeTimer;

		// Token: 0x04002730 RID: 10032
		private float subcooldownTimer;

		// Token: 0x04002731 RID: 10033
		private const float missileInterval = 0.125f;

		// Token: 0x04002732 RID: 10034
		private int remainingMissiles;

		// Token: 0x04002733 RID: 10035
		private HealingFollowerController passiveHealingFollower;

		// Token: 0x04002734 RID: 10036
		private GameObject goldgatControllerObject;

		// Token: 0x04002736 RID: 10038
		private Indicator targetIndicator;

		// Token: 0x04002737 RID: 10039
		private BullseyeSearch targetFinder = new BullseyeSearch();

		// Token: 0x04002738 RID: 10040
		private EquipmentSlot.UserTargetInfo currentTarget;

		// Token: 0x04002739 RID: 10041
		private PickupSearch pickupSearch;

		// Token: 0x0400273A RID: 10042
		private static int kRpcRpcOnClientEquipmentActivationRecieved;

		// Token: 0x0400273B RID: 10043
		private static int kCmdCmdExecuteIfReady = -303452611;

		// Token: 0x0400273C RID: 10044
		private static int kCmdCmdOnEquipmentExecuted;

		// Token: 0x020006C4 RID: 1732
		private struct UserTargetInfo
		{
			// Token: 0x06002213 RID: 8723 RVA: 0x00093944 File Offset: 0x00091B44
			public UserTargetInfo(HurtBox source)
			{
				this.hurtBox = source;
				this.rootObject = (this.hurtBox ? this.hurtBox.healthComponent.gameObject : null);
				this.pickupController = null;
				this.transformToIndicateAt = (this.hurtBox ? this.hurtBox.transform : null);
			}

			// Token: 0x06002214 RID: 8724 RVA: 0x000939A8 File Offset: 0x00091BA8
			public UserTargetInfo(GenericPickupController source)
			{
				this.pickupController = source;
				this.hurtBox = null;
				this.rootObject = (this.pickupController ? this.pickupController.gameObject : null);
				this.transformToIndicateAt = (this.pickupController ? this.pickupController.pickupDisplay.transform : null);
			}

			// Token: 0x0400273D RID: 10045
			public readonly HurtBox hurtBox;

			// Token: 0x0400273E RID: 10046
			public readonly GameObject rootObject;

			// Token: 0x0400273F RID: 10047
			public readonly GenericPickupController pickupController;

			// Token: 0x04002740 RID: 10048
			public readonly Transform transformToIndicateAt;
		}
	}
}
