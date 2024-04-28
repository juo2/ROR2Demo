using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EntityStates;
using EntityStates.GummyClone;
using RoR2.CharacterAI;
using RoR2.Items;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000632 RID: 1586
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MinionOwnership))]
	[RequireComponent(typeof(Inventory))]
	public class CharacterMaster : NetworkBehaviour
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x00080C10 File Offset: 0x0007EE10
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x00080C1D File Offset: 0x0007EE1D
		public MasterCatalog.MasterIndex masterIndex
		{
			get
			{
				return (MasterCatalog.MasterIndex)this._masterIndex;
			}
			set
			{
				this._masterIndex = (int)value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00080C2B File Offset: 0x0007EE2B
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x00080C33 File Offset: 0x0007EE33
		public NetworkIdentity networkIdentity { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00080C3C File Offset: 0x0007EE3C
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x00080C44 File Offset: 0x0007EE44
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001E05 RID: 7685 RVA: 0x00080C50 File Offset: 0x0007EE50
		// (remove) Token: 0x06001E06 RID: 7686 RVA: 0x00080C84 File Offset: 0x0007EE84
		public static event Action<CharacterMaster> onStartGlobal;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001E07 RID: 7687 RVA: 0x00080CB8 File Offset: 0x0007EEB8
		// (remove) Token: 0x06001E08 RID: 7688 RVA: 0x00080CEC File Offset: 0x0007EEEC
		public static event Action<CharacterMaster> onCharacterMasterDiscovered;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001E09 RID: 7689 RVA: 0x00080D20 File Offset: 0x0007EF20
		// (remove) Token: 0x06001E0A RID: 7690 RVA: 0x00080D54 File Offset: 0x0007EF54
		public static event Action<CharacterMaster> onCharacterMasterLost;

		// Token: 0x06001E0B RID: 7691 RVA: 0x00080D87 File Offset: 0x0007EF87
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00080D9A File Offset: 0x0007EF9A
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00080DA8 File Offset: 0x0007EFA8
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
			base.OnStopAuthority();
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001E0E RID: 7694 RVA: 0x00080DB8 File Offset: 0x0007EFB8
		// (remove) Token: 0x06001E0F RID: 7695 RVA: 0x00080DF0 File Offset: 0x0007EFF0
		public event Action<CharacterBody> onBodyStart;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001E10 RID: 7696 RVA: 0x00080E28 File Offset: 0x0007F028
		// (remove) Token: 0x06001E11 RID: 7697 RVA: 0x00080E60 File Offset: 0x0007F060
		public event Action<CharacterBody> onBodyDestroyed;

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00080E95 File Offset: 0x0007F095
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x00080E9D File Offset: 0x0007F09D
		public TeamIndex teamIndex
		{
			get
			{
				return this._teamIndex;
			}
			set
			{
				if (this._teamIndex == value)
				{
					return;
				}
				this._teamIndex = value;
				if (NetworkServer.active)
				{
					base.SetDirtyBit(8U);
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00080EBE File Offset: 0x0007F0BE
		public static ReadOnlyCollection<CharacterMaster> readOnlyInstancesList
		{
			get
			{
				return CharacterMaster._readOnlyInstancesList;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00080ECE File Offset: 0x0007F0CE
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x00080EC5 File Offset: 0x0007F0C5
		public Inventory inventory { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00080ED6 File Offset: 0x0007F0D6
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x00080EDE File Offset: 0x0007F0DE
		public PlayerCharacterMasterController playerCharacterMasterController { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00080EE7 File Offset: 0x0007F0E7
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00080EEF File Offset: 0x0007F0EF
		public PlayerStatsComponent playerStatsComponent { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00080EF8 File Offset: 0x0007F0F8
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x00080F00 File Offset: 0x0007F100
		public MinionOwnership minionOwnership { get; private set; }

		// Token: 0x06001E1D RID: 7709 RVA: 0x00080F09 File Offset: 0x0007F109
		[Server]
		public void SetLoadoutServer(Loadout newLoadout)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::SetLoadoutServer(RoR2.Loadout)' called on client");
				return;
			}
			newLoadout.Copy(this.loadout);
			base.SetDirtyBit(16U);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00080F34 File Offset: 0x0007F134
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x00080F3C File Offset: 0x0007F13C
		private NetworkInstanceId bodyInstanceId
		{
			get
			{
				return this._bodyInstanceId;
			}
			set
			{
				if (value == this._bodyInstanceId)
				{
					return;
				}
				base.SetDirtyBit(1U);
				this._bodyInstanceId = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00080F5B File Offset: 0x0007F15B
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x00080F63 File Offset: 0x0007F163
		public BodyIndex backupBodyIndex { get; private set; }

		// Token: 0x06001E22 RID: 7714 RVA: 0x00080F6C File Offset: 0x0007F16C
		private void StoreBackupBodyIndex()
		{
			if (this.resolvedBodyInstance)
			{
				CharacterBody component = this.resolvedBodyInstance.GetComponent<CharacterBody>();
				if (component)
				{
					this.backupBodyIndex = component.bodyIndex;
				}
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00080FA6 File Offset: 0x0007F1A6
		private void OnSyncBodyInstanceId(NetworkInstanceId value)
		{
			this.resolvedBodyInstance = null;
			this.bodyResolved = (value == NetworkInstanceId.Invalid);
			this._bodyInstanceId = value;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00080FC7 File Offset: 0x0007F1C7
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x00081004 File Offset: 0x0007F204
		private GameObject bodyInstanceObject
		{
			get
			{
				if (!this.bodyResolved)
				{
					this.resolvedBodyInstance = Util.FindNetworkObject(this.bodyInstanceId);
					if (this.resolvedBodyInstance)
					{
						this.bodyResolved = true;
						this.StoreBackupBodyIndex();
					}
				}
				return this.resolvedBodyInstance;
			}
			set
			{
				NetworkInstanceId bodyInstanceId = NetworkInstanceId.Invalid;
				this.resolvedBodyInstance = null;
				this.bodyResolved = true;
				if (value)
				{
					NetworkIdentity component = value.GetComponent<NetworkIdentity>();
					if (component)
					{
						bodyInstanceId = component.netId;
						this.resolvedBodyInstance = value;
						this.StoreBackupBodyIndex();
					}
				}
				this.bodyInstanceId = bodyInstanceId;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00081057 File Offset: 0x0007F257
		[Server]
		public void GiveExperience(ulong amount)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::GiveExperience(System.UInt64)' called on client");
				return;
			}
			TeamManager.instance.GiveTeamExperience(this.teamIndex, amount);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0008107F File Offset: 0x0007F27F
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x00081087 File Offset: 0x0007F287
		public uint money
		{
			get
			{
				return this._money;
			}
			set
			{
				if (value == this._money)
				{
					return;
				}
				base.SetDirtyBit(2U);
				this._money = value;
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000810A1 File Offset: 0x0007F2A1
		public void GiveMoney(uint amount)
		{
			this.money += amount;
			StatManager.OnGoldCollected(this, (ulong)amount);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x000810B9 File Offset: 0x0007F2B9
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x000810C1 File Offset: 0x0007F2C1
		public uint voidCoins
		{
			get
			{
				return this._voidCoins;
			}
			set
			{
				if (value == this._voidCoins)
				{
					return;
				}
				base.SetDirtyBit(64U);
				this._voidCoins = value;
			}
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000810DC File Offset: 0x0007F2DC
		public void GiveVoidCoins(uint amount)
		{
			this.voidCoins += amount;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000810EC File Offset: 0x0007F2EC
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x000810F4 File Offset: 0x0007F2F4
		public float luck { get; set; }

		// Token: 0x06001E2F RID: 7727 RVA: 0x00081100 File Offset: 0x0007F300
		public int GetDeployableSameSlotLimit(DeployableSlot slot)
		{
			int result = 0;
			int num = 1;
			if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.swarmsArtifactDef))
			{
				num = 2;
			}
			switch (slot)
			{
			case DeployableSlot.EngiMine:
				result = 4;
				if (this.bodyInstanceObject)
				{
					result = this.bodyInstanceObject.GetComponent<SkillLocator>().secondary.maxStock;
				}
				break;
			case DeployableSlot.EngiTurret:
				if (this.inventory.GetItemCount(DLC1Content.Items.EquipmentMagazineVoid) > 0)
				{
					result = 3;
				}
				else
				{
					result = 2;
				}
				break;
			case DeployableSlot.BeetleGuardAlly:
				result = this.inventory.GetItemCount(RoR2Content.Items.BeetleGland) * num;
				break;
			case DeployableSlot.EngiBubbleShield:
				result = 1;
				break;
			case DeployableSlot.LoaderPylon:
				result = 3;
				break;
			case DeployableSlot.EngiSpiderMine:
				result = 4;
				if (this.bodyInstanceObject)
				{
					result = this.bodyInstanceObject.GetComponent<SkillLocator>().secondary.maxStock;
				}
				break;
			case DeployableSlot.RoboBallMini:
				result = 3;
				break;
			case DeployableSlot.ParentPodAlly:
				result = this.inventory.GetItemCount(JunkContent.Items.Incubator) * num;
				break;
			case DeployableSlot.ParentAlly:
				result = this.inventory.GetItemCount(JunkContent.Items.Incubator) * num;
				break;
			case DeployableSlot.PowerWard:
				result = 1;
				break;
			case DeployableSlot.CrippleWard:
				result = 5;
				break;
			case DeployableSlot.DeathProjectile:
				result = 3;
				break;
			case DeployableSlot.RoboBallRedBuddy:
			case DeployableSlot.RoboBallGreenBuddy:
				result = num;
				break;
			case DeployableSlot.GummyClone:
				result = 3;
				break;
			case DeployableSlot.LunarSunBomb:
				result = LunarSunBehavior.GetMaxProjectiles(this.inventory);
				break;
			case DeployableSlot.VendingMachine:
				result = 1;
				break;
			case DeployableSlot.VoidMegaCrabItem:
				result = VoidMegaCrabItemBehavior.GetMaxProjectiles(this.inventory);
				break;
			case DeployableSlot.DroneWeaponsDrone:
				result = 1;
				break;
			case DeployableSlot.MinorConstructOnKill:
				result = this.inventory.GetItemCount(DLC1Content.Items.MinorConstructOnKill) * 4;
				break;
			case DeployableSlot.CaptainSupplyDrop:
				result = 2;
				break;
			}
			return result;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000812A4 File Offset: 0x0007F4A4
		[Server]
		public void AddDeployable(Deployable deployable, DeployableSlot slot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::AddDeployable(RoR2.Deployable,RoR2.DeployableSlot)' called on client");
				return;
			}
			if (deployable.ownerMaster)
			{
				Debug.LogErrorFormat("Attempted to add deployable {0} which already belongs to master {1} to master {2}.", new object[]
				{
					deployable.gameObject,
					deployable.ownerMaster.gameObject,
					base.gameObject
				});
			}
			if (this.deployablesList == null)
			{
				this.deployablesList = new List<DeployableInfo>();
			}
			int num = 0;
			int deployableSameSlotLimit = this.GetDeployableSameSlotLimit(slot);
			for (int i = this.deployablesList.Count - 1; i >= 0; i--)
			{
				if (this.deployablesList[i].slot == slot)
				{
					num++;
					if (num >= deployableSameSlotLimit)
					{
						Deployable deployable2 = this.deployablesList[i].deployable;
						this.deployablesList.RemoveAt(i);
						deployable2.ownerMaster = null;
						deployable2.onUndeploy.Invoke();
					}
				}
			}
			this.deployablesList.Add(new DeployableInfo
			{
				deployable = deployable,
				slot = slot
			});
			deployable.ownerMaster = this;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000813B0 File Offset: 0x0007F5B0
		[Server]
		public int GetDeployableCount(DeployableSlot slot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Int32 RoR2.CharacterMaster::GetDeployableCount(RoR2.DeployableSlot)' called on client");
				return 0;
			}
			if (this.deployablesList == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = this.deployablesList.Count - 1; i >= 0; i--)
			{
				if (this.deployablesList[i].slot == slot)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00081410 File Offset: 0x0007F610
		[Server]
		public bool IsDeployableLimited(DeployableSlot slot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.CharacterMaster::IsDeployableLimited(RoR2.DeployableSlot)' called on client");
				return false;
			}
			return this.GetDeployableCount(slot) >= this.GetDeployableSameSlotLimit(slot);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0008143C File Offset: 0x0007F63C
		[Server]
		public void RemoveDeployable(Deployable deployable)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::RemoveDeployable(RoR2.Deployable)' called on client");
				return;
			}
			if (this.deployablesList == null || deployable.ownerMaster != this)
			{
				return;
			}
			for (int i = this.deployablesList.Count - 1; i >= 0; i--)
			{
				if (this.deployablesList[i].deployable == deployable)
				{
					this.deployablesList.RemoveAt(i);
				}
			}
			deployable.ownerMaster = null;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000814B9 File Offset: 0x0007F6B9
		[Server]
		public bool IsDeployableSlotAvailable(DeployableSlot deployableSlot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.CharacterMaster::IsDeployableSlotAvailable(RoR2.DeployableSlot)' called on client");
				return false;
			}
			return this.GetDeployableCount(deployableSlot) < this.GetDeployableSameSlotLimit(deployableSlot);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000814E4 File Offset: 0x0007F6E4
		[Server]
		public CharacterBody SpawnBody(Vector3 position, Quaternion rotation)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterBody RoR2.CharacterMaster::SpawnBody(UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
				return null;
			}
			if (this.bodyInstanceObject)
			{
				Debug.LogError("Character cannot have more than one body at this time.");
				return null;
			}
			if (!this.bodyPrefab)
			{
				Debug.LogErrorFormat("Attempted to spawn body of character master {0} with no body prefab.", new object[]
				{
					base.gameObject
				});
			}
			if (!this.bodyPrefab.GetComponent<CharacterBody>())
			{
				Debug.LogErrorFormat("Attempted to spawn body of character master {0} with a body prefab that has no {1} component attached.", new object[]
				{
					base.gameObject,
					typeof(CharacterBody).Name
				});
			}
			bool flag = this.bodyPrefab.GetComponent<CharacterDirection>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bodyPrefab, position, flag ? Quaternion.identity : rotation);
			CharacterBody component = gameObject.GetComponent<CharacterBody>();
			component.masterObject = base.gameObject;
			component.teamComponent.teamIndex = this.teamIndex;
			component.SetLoadoutServer(this.loadout);
			if (flag)
			{
				CharacterDirection component2 = gameObject.GetComponent<CharacterDirection>();
				float y = rotation.eulerAngles.y;
				component2.yaw = y;
			}
			NetworkConnection clientAuthorityOwner = base.GetComponent<NetworkIdentity>().clientAuthorityOwner;
			if (clientAuthorityOwner != null)
			{
				clientAuthorityOwner.isReady = true;
				NetworkServer.SpawnWithClientAuthority(gameObject, clientAuthorityOwner);
			}
			else
			{
				NetworkServer.Spawn(gameObject);
			}
			this.bodyInstanceObject = gameObject;
			Run.instance.OnServerCharacterBodySpawned(component);
			return component;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00081640 File Offset: 0x0007F840
		[Server]
		public void DestroyBody()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::DestroyBody()' called on client");
				return;
			}
			if (this.bodyInstanceObject)
			{
				CharacterBody body = this.GetBody();
				UnityEngine.Object.Destroy(this.bodyInstanceObject);
				this.OnBodyDestroyed(body);
				this.bodyInstanceObject = null;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0008168F File Offset: 0x0007F88F
		public GameObject GetBodyObject()
		{
			return this.bodyInstanceObject;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00081697 File Offset: 0x0007F897
		public bool hasBody
		{
			get
			{
				return this.bodyInstanceObject;
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000816A4 File Offset: 0x0007F8A4
		public CharacterBody GetBody()
		{
			GameObject bodyObject = this.GetBodyObject();
			if (!bodyObject)
			{
				return null;
			}
			return bodyObject.GetComponent<CharacterBody>();
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000816C8 File Offset: 0x0007F8C8
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.inventory = base.GetComponent<Inventory>();
			this.aiComponents = (NetworkServer.active ? base.GetComponents<BaseAI>() : Array.Empty<BaseAI>());
			this.playerCharacterMasterController = base.GetComponent<PlayerCharacterMasterController>();
			this.playerStatsComponent = base.GetComponent<PlayerStatsComponent>();
			this.minionOwnership = base.GetComponent<MinionOwnership>();
			this.inventory.onInventoryChanged += this.OnInventoryChanged;
			this.inventory.onItemAddedClient += this.OnItemAddedClient;
			this.inventory.onEquipmentExternalRestockServer += this.OnInventoryEquipmentExternalRestockServer;
			this.OnInventoryChanged();
			Stage.onServerStageBegin += this.OnServerStageBegin;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00081787 File Offset: 0x0007F987
		private void OnItemAddedClient(ItemIndex itemIndex)
		{
			base.StartCoroutine(this.HighlightNewItem(itemIndex));
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00081797 File Offset: 0x0007F997
		private IEnumerator HighlightNewItem(ItemIndex itemIndex)
		{
			yield return new WaitForSeconds(0.05f);
			GameObject bodyObject = this.GetBodyObject();
			if (bodyObject)
			{
				ModelLocator component = bodyObject.GetComponent<ModelLocator>();
				if (component)
				{
					Transform modelTransform = component.modelTransform;
					if (modelTransform)
					{
						CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
						if (component2)
						{
							component2.HighlightItemDisplay(itemIndex);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000817AD File Offset: 0x0007F9AD
		private void Start()
		{
			this.UpdateAuthority();
			if (NetworkServer.active && this.spawnOnStart && !this.bodyInstanceObject)
			{
				this.SpawnBodyHere();
			}
			Action<CharacterMaster> action = CharacterMaster.onStartGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000817E8 File Offset: 0x0007F9E8
		private void OnInventoryChanged()
		{
			this.luck = 0f;
			this.luck += (float)this.inventory.GetItemCount(RoR2Content.Items.Clover);
			this.luck -= (float)this.inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);
			if (NetworkServer.active && this.inventory)
			{
				CharacterBody body = this.GetBody();
				if (body && body.bodyIndex != BodyCatalog.FindBodyIndex("HereticBody") && this.inventory.GetItemCount(RoR2Content.Items.LunarPrimaryReplacement.itemIndex) > 0 && this.inventory.GetItemCount(RoR2Content.Items.LunarSecondaryReplacement.itemIndex) > 0 && this.inventory.GetItemCount(RoR2Content.Items.LunarSpecialReplacement.itemIndex) > 0 && this.inventory.GetItemCount(RoR2Content.Items.LunarUtilityReplacement.itemIndex) > 0)
				{
					this.TransformBody("HereticBody");
				}
			}
			this.SetUpGummyClone();
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000818E8 File Offset: 0x0007FAE8
		private void OnInventoryEquipmentExternalRestockServer()
		{
			CharacterBody body = this.GetBody();
			if (body)
			{
				EffectData effectData = new EffectData();
				effectData.origin = body.corePosition;
				effectData.SetNetworkedObjectReference(body.gameObject);
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/EquipmentRestockEffect"), effectData, true);
			}
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00081933 File Offset: 0x0007FB33
		[Server]
		public void SpawnBodyHere()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::SpawnBodyHere()' called on client");
				return;
			}
			this.SpawnBody(base.transform.position, base.transform.rotation);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00081967 File Offset: 0x0007FB67
		private void OnEnable()
		{
			CharacterMaster.instancesList.Add(this);
			Action<CharacterMaster> action = CharacterMaster.onCharacterMasterDiscovered;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00081984 File Offset: 0x0007FB84
		private void OnDisable()
		{
			try
			{
				Action<CharacterMaster> action = CharacterMaster.onCharacterMasterLost;
				if (action != null)
				{
					action(this);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			CharacterMaster.instancesList.Remove(this);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000819C8 File Offset: 0x0007FBC8
		private void OnDestroy()
		{
			if (this.isBoss)
			{
				this.isBoss = false;
			}
			Stage.onServerStageBegin -= this.OnServerStageBegin;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000819EC File Offset: 0x0007FBEC
		public void OnBodyStart(CharacterBody body)
		{
			if (NetworkServer.active)
			{
				this.lostBodyToDeath = false;
			}
			this.preventGameOver = true;
			this.killerBodyIndex = BodyIndex.None;
			this.killedByUnsafeArea = false;
			body.RecalculateStats();
			if (NetworkServer.active)
			{
				BaseAI[] array = this.aiComponents;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnBodyStart(body);
				}
			}
			if (this.playerCharacterMasterController)
			{
				if (this.playerCharacterMasterController.networkUserObject)
				{
					bool isLocalPlayer = this.playerCharacterMasterController.networkUserObject.GetComponent<NetworkIdentity>().isLocalPlayer;
				}
				this.playerCharacterMasterController.OnBodyStart();
			}
			if (this.inventory.GetItemCount(RoR2Content.Items.Ghost) > 0)
			{
				Util.PlaySound("Play_item_proc_ghostOnKill", body.gameObject);
			}
			if (NetworkServer.active)
			{
				HealthComponent healthComponent = body.healthComponent;
				if (healthComponent)
				{
					if (this.teamIndex == TeamIndex.Player && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse1)
					{
						healthComponent.Networkhealth = healthComponent.fullHealth * 0.5f;
					}
					else
					{
						healthComponent.Networkhealth = healthComponent.fullHealth;
					}
				}
				this.UpdateBodyGodMode();
				this.StartLifeStopwatch();
			}
			this.SetUpGummyClone();
			Action<CharacterBody> action = this.onBodyStart;
			if (action == null)
			{
				return;
			}
			action(body);
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00081B1A File Offset: 0x0007FD1A
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x00081B22 File Offset: 0x0007FD22
		public Vector3 deathFootPosition { get; private set; } = Vector3.zero;

		// Token: 0x06001E47 RID: 7751 RVA: 0x00081B2B File Offset: 0x0007FD2B
		[Server]
		public bool IsExtraLifePendingServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.CharacterMaster::IsExtraLifePendingServer()' called on client");
				return false;
			}
			return base.IsInvoking("RespawnExtraLife") || base.IsInvoking("RespawnExtraLifeVoid");
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00081B60 File Offset: 0x0007FD60
		[Server]
		public bool IsDeadAndOutOfLivesServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.CharacterMaster::IsDeadAndOutOfLivesServer()' called on client");
				return false;
			}
			CharacterBody body = this.GetBody();
			return (!body || !body.healthComponent.alive) && (this.inventory.GetItemCount(RoR2Content.Items.ExtraLife) <= 0 && this.inventory.GetItemCount(DLC1Content.Items.ExtraLifeVoid) <= 0) && !this.IsExtraLifePendingServer();
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00081BD4 File Offset: 0x0007FDD4
		public void OnBodyDeath(CharacterBody body)
		{
			if (NetworkServer.active)
			{
				this.lostBodyToDeath = true;
				this.deathFootPosition = body.footPosition;
				BaseAI[] array = this.aiComponents;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnBodyDeath(body);
				}
				if (this.playerCharacterMasterController)
				{
					this.playerCharacterMasterController.OnBodyDeath();
				}
				if (this.inventory.GetItemCount(RoR2Content.Items.ExtraLife) > 0)
				{
					this.inventory.RemoveItem(RoR2Content.Items.ExtraLife, 1);
					base.Invoke("RespawnExtraLife", 2f);
					base.Invoke("PlayExtraLifeSFX", 1f);
				}
				else if (this.inventory.GetItemCount(DLC1Content.Items.ExtraLifeVoid) > 0)
				{
					this.inventory.RemoveItem(DLC1Content.Items.ExtraLifeVoid, 1);
					base.Invoke("RespawnExtraLifeVoid", 2f);
					base.Invoke("PlayExtraLifeVoidSFX", 1f);
				}
				else
				{
					if (this.destroyOnBodyDeath)
					{
						UnityEngine.Object.Destroy(base.gameObject, 1f);
					}
					this.preventGameOver = false;
					this.preventRespawnUntilNextStageServer = true;
				}
				this.ResetLifeStopwatch();
			}
			UnityEvent unityEvent = this.onBodyDeath;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00081CFC File Offset: 0x0007FEFC
		public void TrueKill()
		{
			this.TrueKill(null, null, DamageType.Generic);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00081D08 File Offset: 0x0007FF08
		public void TrueKill(GameObject killerOverride = null, GameObject inflictorOverride = null, DamageType damageTypeOverride = DamageType.Generic)
		{
			int itemCount = this.inventory.GetItemCount(RoR2Content.Items.ExtraLife);
			if (itemCount > 0)
			{
				this.inventory.ResetItem(RoR2Content.Items.ExtraLife);
				this.inventory.GiveItem(RoR2Content.Items.ExtraLifeConsumed, itemCount);
				CharacterMasterNotificationQueue.SendTransformNotification(this, RoR2Content.Items.ExtraLife.itemIndex, RoR2Content.Items.ExtraLifeConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
			}
			if (this.inventory.GetItemCount(DLC1Content.Items.ExtraLifeVoid) > 0)
			{
				this.inventory.ResetItem(DLC1Content.Items.ExtraLifeVoid);
				this.inventory.GiveItem(DLC1Content.Items.ExtraLifeVoidConsumed, itemCount);
				CharacterMasterNotificationQueue.SendTransformNotification(this, DLC1Content.Items.ExtraLifeVoid.itemIndex, DLC1Content.Items.ExtraLifeVoidConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
			}
			base.CancelInvoke("RespawnExtraLife");
			base.CancelInvoke("PlayExtraLifeSFX");
			base.CancelInvoke("RespawnExtraLifeVoid");
			base.CancelInvoke("PlayExtraLifeVoidSFX");
			CharacterBody body = this.GetBody();
			if (body)
			{
				body.healthComponent.Suicide(killerOverride, inflictorOverride, damageTypeOverride);
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00081E00 File Offset: 0x00080000
		private void PlayExtraLifeSFX()
		{
			GameObject bodyInstanceObject = this.bodyInstanceObject;
			if (bodyInstanceObject)
			{
				Util.PlaySound("Play_item_proc_extraLife", bodyInstanceObject);
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00081E28 File Offset: 0x00080028
		private void PlayExtraLifeVoidSFX()
		{
			GameObject bodyInstanceObject = this.bodyInstanceObject;
			if (bodyInstanceObject)
			{
				Util.PlaySound("Play_item_void_extraLife", bodyInstanceObject);
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00081E50 File Offset: 0x00080050
		public void RespawnExtraLife()
		{
			this.inventory.GiveItem(RoR2Content.Items.ExtraLifeConsumed, 1);
			CharacterMasterNotificationQueue.SendTransformNotification(this, RoR2Content.Items.ExtraLife.itemIndex, RoR2Content.Items.ExtraLifeConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
			Vector3 vector = this.deathFootPosition;
			if (this.killedByUnsafeArea)
			{
				vector = (TeleportHelper.FindSafeTeleportDestination(this.deathFootPosition, this.bodyPrefab.GetComponent<CharacterBody>(), RoR2Application.rng) ?? this.deathFootPosition);
			}
			this.Respawn(vector, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f));
			this.GetBody().AddTimedBuff(RoR2Content.Buffs.Immune, 3f);
			GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/HippoRezEffect");
			if (this.bodyInstanceObject)
			{
				foreach (EntityStateMachine entityStateMachine in this.bodyInstanceObject.GetComponents<EntityStateMachine>())
				{
					entityStateMachine.initialStateType = entityStateMachine.mainStateType;
				}
				if (gameObject)
				{
					EffectManager.SpawnEffect(gameObject, new EffectData
					{
						origin = vector,
						rotation = this.bodyInstanceObject.transform.rotation
					}, true);
				}
			}
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00081F80 File Offset: 0x00080180
		public void RespawnExtraLifeVoid()
		{
			this.inventory.GiveItem(DLC1Content.Items.ExtraLifeVoidConsumed, 1);
			CharacterMasterNotificationQueue.SendTransformNotification(this, DLC1Content.Items.ExtraLifeVoid.itemIndex, DLC1Content.Items.ExtraLifeVoidConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
			Vector3 vector = this.deathFootPosition;
			if (this.killedByUnsafeArea)
			{
				vector = (TeleportHelper.FindSafeTeleportDestination(this.deathFootPosition, this.bodyPrefab.GetComponent<CharacterBody>(), RoR2Application.rng) ?? this.deathFootPosition);
			}
			this.Respawn(vector, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f));
			this.GetBody().AddTimedBuff(RoR2Content.Buffs.Immune, 3f);
			if (this.bodyInstanceObject)
			{
				foreach (EntityStateMachine entityStateMachine in this.bodyInstanceObject.GetComponents<EntityStateMachine>())
				{
					entityStateMachine.initialStateType = entityStateMachine.mainStateType;
				}
				if (ExtraLifeVoidManager.rezEffectPrefab)
				{
					EffectManager.SpawnEffect(ExtraLifeVoidManager.rezEffectPrefab, new EffectData
					{
						origin = vector,
						rotation = this.bodyInstanceObject.transform.rotation
					}, true);
				}
			}
			foreach (ContagiousItemManager.TransformationInfo transformationInfo in ContagiousItemManager.transformationInfos)
			{
				ContagiousItemManager.TryForceReplacement(this.inventory, transformationInfo.originalItem);
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000820F8 File Offset: 0x000802F8
		public void OnBodyDamaged(DamageReport damageReport)
		{
			BaseAI[] array = this.aiComponents;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnBodyDamaged(damageReport);
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00082124 File Offset: 0x00080324
		public void OnBodyDestroyed(CharacterBody characterBody)
		{
			if (characterBody != this.GetBody())
			{
				return;
			}
			if (NetworkServer.active)
			{
				BaseAI[] array = this.aiComponents;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].OnBodyDestroyed(characterBody);
				}
				this.PauseLifeStopwatch();
			}
			Action<CharacterBody> action = this.onBodyDestroyed;
			if (action == null)
			{
				return;
			}
			action(characterBody);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00082177 File Offset: 0x00080377
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x0008217F File Offset: 0x0008037F
		private float internalSurvivalTime
		{
			get
			{
				return this._internalSurvivalTime;
			}
			set
			{
				if (value == this._internalSurvivalTime)
				{
					return;
				}
				base.SetDirtyBit(4U);
				this._internalSurvivalTime = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00082199 File Offset: 0x00080399
		public float currentLifeStopwatch
		{
			get
			{
				if (this.internalSurvivalTime <= 0f)
				{
					return -this.internalSurvivalTime;
				}
				if (Run.instance)
				{
					return Run.instance.GetRunStopwatch() - this.internalSurvivalTime;
				}
				return 0f;
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000821D3 File Offset: 0x000803D3
		[Server]
		private void StartLifeStopwatch()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::StartLifeStopwatch()' called on client");
				return;
			}
			if (this.internalSurvivalTime > 0f)
			{
				return;
			}
			this.internalSurvivalTime = Run.instance.GetRunStopwatch() - this.currentLifeStopwatch;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0008220F File Offset: 0x0008040F
		[Server]
		private void PauseLifeStopwatch()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::PauseLifeStopwatch()' called on client");
				return;
			}
			if (this.internalSurvivalTime <= 0f)
			{
				return;
			}
			this.internalSurvivalTime = -this.currentLifeStopwatch;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00082241 File Offset: 0x00080441
		[Server]
		private void ResetLifeStopwatch()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::ResetLifeStopwatch()' called on client");
				return;
			}
			this.internalSurvivalTime = 0f;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00082264 File Offset: 0x00080464
		[Server]
		public BodyIndex GetKillerBodyIndex()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.BodyIndex RoR2.CharacterMaster::GetKillerBodyIndex()' called on client");
				return (BodyIndex)0;
			}
			return this.killerBodyIndex;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00082298 File Offset: 0x00080498
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			GlobalEventManager.onCharacterDeathGlobal += delegate(DamageReport damageReport)
			{
				CharacterMaster victimMaster = damageReport.victimMaster;
				if (victimMaster)
				{
					victimMaster.killerBodyIndex = BodyCatalog.FindBodyIndex(damageReport.damageInfo.attacker);
					victimMaster.killedByUnsafeArea = (damageReport.damageInfo.inflictor && damageReport.damageInfo.inflictor.GetComponent<MapZone>());
				}
			};
			Stage.onServerStageBegin += delegate(Stage stage)
			{
				foreach (CharacterMaster characterMaster in CharacterMaster.instancesList)
				{
					characterMaster.preventRespawnUntilNextStageServer = false;
				}
			};
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000822F0 File Offset: 0x000804F0
		[Command]
		public void CmdRespawn(string bodyName)
		{
			if (this.preventRespawnUntilNextStageServer)
			{
				return;
			}
			if (!string.IsNullOrEmpty(bodyName))
			{
				this.bodyPrefab = BodyCatalog.FindBodyPrefab(bodyName);
				if (!this.bodyPrefab)
				{
					Debug.LogError("CmdRespawn failed to find bodyPrefab for name '" + bodyName + "'.");
				}
			}
			if (Stage.instance)
			{
				Stage.instance.RespawnCharacter(this);
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00082354 File Offset: 0x00080554
		[Server]
		public void TransformBody(string bodyName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::TransformBody(System.String)' called on client");
				return;
			}
			if (string.IsNullOrEmpty(bodyName))
			{
				Debug.LogError("Can't TransformBody with null or empty body name.");
				return;
			}
			this.bodyPrefab = BodyCatalog.FindBodyPrefab(bodyName);
			if (!(this.bodyPrefab != null))
			{
				Debug.LogError("Can't TransformBody because there's no prefab for body named '" + bodyName + "'");
				return;
			}
			Transform component = this.bodyInstanceObject.GetComponent<Transform>();
			Vector3 vector = component.position;
			Quaternion rotation = component.rotation;
			this.DestroyBody();
			CharacterBody component2 = this.bodyPrefab.GetComponent<CharacterBody>();
			if (component2)
			{
				vector = this.CalculateSafeGroundPosition(vector, component2);
				this.SpawnBody(vector, rotation);
				return;
			}
			Debug.LogErrorFormat("Trying to respawn as object {0} who has no Character Body!", new object[]
			{
				this.bodyPrefab
			});
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0008241A File Offset: 0x0008061A
		[Server]
		private void OnServerStageBegin(Stage stage)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::OnServerStageBegin(RoR2.Stage)' called on client");
				return;
			}
			this.TryCloverVoidUpgrades();
			this.TryRegenerateScrap();
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00082440 File Offset: 0x00080640
		private void TryRegenerateScrap()
		{
			int itemCount = this.inventory.GetItemCount(DLC1Content.Items.RegeneratingScrapConsumed);
			if (itemCount > 0)
			{
				this.inventory.RemoveItem(DLC1Content.Items.RegeneratingScrapConsumed, itemCount);
				this.inventory.GiveItem(DLC1Content.Items.RegeneratingScrap, itemCount);
				CharacterMasterNotificationQueue.SendTransformNotification(this, DLC1Content.Items.RegeneratingScrapConsumed.itemIndex, DLC1Content.Items.RegeneratingScrap.itemIndex, CharacterMasterNotificationQueue.TransformationType.RegeneratingScrapRegen);
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000824A0 File Offset: 0x000806A0
		[Server]
		private void TryCloverVoidUpgrades()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterMaster::TryCloverVoidUpgrades()' called on client");
				return;
			}
			if (this.cloverVoidRng == null)
			{
				this.cloverVoidRng = new Xoroshiro128Plus(Run.instance.seed);
			}
			int itemCount = this.inventory.GetItemCount(DLC1Content.Items.CloverVoid);
			List<PickupIndex> list = new List<PickupIndex>(Run.instance.availableTier2DropList);
			List<PickupIndex> list2 = new List<PickupIndex>(Run.instance.availableTier3DropList);
			List<ItemIndex> list3 = new List<ItemIndex>(this.inventory.itemAcquisitionOrder);
			Util.ShuffleList<ItemIndex>(list3, this.cloverVoidRng);
			int num = itemCount * 3;
			int num2 = 0;
			int num3 = 0;
			while (num2 < num && num3 < list3.Count)
			{
				CharacterMaster.<>c__DisplayClass161_0 CS$<>8__locals1 = new CharacterMaster.<>c__DisplayClass161_0();
				CS$<>8__locals1.startingItemDef = ItemCatalog.GetItemDef(list3[num3]);
				ItemDef itemDef = null;
				List<PickupIndex> list4 = null;
				ItemTier tier = CS$<>8__locals1.startingItemDef.tier;
				if (tier != ItemTier.Tier1)
				{
					if (tier == ItemTier.Tier2)
					{
						list4 = list2;
					}
				}
				else
				{
					list4 = list;
				}
				if (list4 != null && list4.Count > 0)
				{
					Util.ShuffleList<PickupIndex>(list4, this.cloverVoidRng);
					list4.Sort(new Comparison<PickupIndex>(CS$<>8__locals1.<TryCloverVoidUpgrades>g__CompareTags|0));
					itemDef = ItemCatalog.GetItemDef(list4[0].itemIndex);
				}
				if (itemDef != null)
				{
					if (this.inventory.GetItemCount(itemDef.itemIndex) == 0)
					{
						list3.Add(itemDef.itemIndex);
					}
					num2++;
					int itemCount2 = this.inventory.GetItemCount(CS$<>8__locals1.startingItemDef.itemIndex);
					this.inventory.RemoveItem(CS$<>8__locals1.startingItemDef.itemIndex, itemCount2);
					this.inventory.GiveItem(itemDef.itemIndex, itemCount2);
					CharacterMasterNotificationQueue.SendTransformNotification(this, CS$<>8__locals1.startingItemDef.itemIndex, itemDef.itemIndex, CharacterMasterNotificationQueue.TransformationType.CloverVoid);
				}
				num3++;
			}
			if (num2 > 0)
			{
				GameObject bodyInstanceObject = this.bodyInstanceObject;
				if (bodyInstanceObject)
				{
					Util.PlaySound("Play_item_proc_extraLife", bodyInstanceObject);
				}
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00082694 File Offset: 0x00080894
		private unsafe static GameObject PickRandomSurvivorBodyPrefab(Xoroshiro128Plus rng, NetworkUser networkUser, bool allowHidden)
		{
			CharacterMaster.<>c__DisplayClass162_0 CS$<>8__locals1 = new CharacterMaster.<>c__DisplayClass162_0();
			CS$<>8__locals1.allowHidden = allowHidden;
			CS$<>8__locals1.networkUser = networkUser;
			SurvivorDef[] array = SurvivorCatalog.allSurvivorDefs.Where(new Func<SurvivorDef, bool>(CS$<>8__locals1.<PickRandomSurvivorBodyPrefab>g__SurvivorIsUnlockedAndAvailable|0)).ToArray<SurvivorDef>();
			return rng.NextElementUniform<SurvivorDef>(array)->bodyPrefab;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000826E0 File Offset: 0x000808E0
		[Server]
		public CharacterBody Respawn(Vector3 footPosition, Quaternion rotation)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterBody RoR2.CharacterMaster::Respawn(UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
				return null;
			}
			this.DestroyBody();
			if (this.playerCharacterMasterController && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef))
			{
				this.bodyPrefab = CharacterMaster.PickRandomSurvivorBodyPrefab(Run.instance.randomSurvivorOnRespawnRng, this.playerCharacterMasterController.networkUser, false);
			}
			if (this.bodyPrefab)
			{
				CharacterBody component = this.bodyPrefab.GetComponent<CharacterBody>();
				if (component)
				{
					Vector3 position = footPosition;
					if (true)
					{
						position = this.CalculateSafeGroundPosition(footPosition, component);
					}
					return this.SpawnBody(position, rotation);
				}
				Debug.LogErrorFormat("Trying to respawn as object {0} who has no Character Body!", new object[]
				{
					this.bodyPrefab
				});
			}
			else
			{
				Debug.LogErrorFormat("CharacterMaster.Respawn failed. {0} does not have a valid body prefab assigned.", new object[]
				{
					base.gameObject.name
				});
			}
			return null;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000827C8 File Offset: 0x000809C8
		private Vector3 CalculateSafeGroundPosition(Vector3 desiredFootPos, CharacterBody body)
		{
			if (body)
			{
				Vector3 result = desiredFootPos;
				RaycastHit raycastHit = default(RaycastHit);
				Ray ray = new Ray(desiredFootPos + Vector3.up * 2f, Vector3.down);
				float maxDistance = 4f;
				if (Physics.SphereCast(ray, body.radius, out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					result.y = ray.origin.y - raycastHit.distance;
				}
				float bodyPrefabFootOffset = Util.GetBodyPrefabFootOffset(this.bodyPrefab);
				result.y += bodyPrefabFootOffset;
				return result;
			}
			Debug.LogError("Can't calculate safe ground position if the CharacterBody is null");
			return desiredFootPos;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00082878 File Offset: 0x00080A78
		private void SetUpGummyClone()
		{
			if (NetworkServer.active && this.inventory && this.inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier.itemIndex) > 0)
			{
				if (!base.gameObject.GetComponent<MasterSuicideOnTimer>())
				{
					base.gameObject.AddComponent<MasterSuicideOnTimer>().lifeTimer = 30f;
				}
				CharacterBody body = this.GetBody();
				if (body)
				{
					CharacterDeathBehavior component = body.GetComponent<CharacterDeathBehavior>();
					if (component && component.deathState.stateType != typeof(GummyCloneDeathState))
					{
						component.deathState = new SerializableEntityStateType(typeof(GummyCloneDeathState));
					}
					body.portraitIcon = LegacyResourcesAPI.Load<Texture>("Textures/BodyIcons/texGummyCloneBody");
				}
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00082940 File Offset: 0x00080B40
		private void ToggleGod()
		{
			this.godMode = !this.godMode;
			this.UpdateBodyGodMode();
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00082958 File Offset: 0x00080B58
		private void UpdateBodyGodMode()
		{
			if (this.bodyInstanceObject)
			{
				HealthComponent component = this.bodyInstanceObject.GetComponent<HealthComponent>();
				if (component)
				{
					component.godMode = this.godMode;
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00082992 File Offset: 0x00080B92
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0008299A File Offset: 0x00080B9A
		private uint miscFlags
		{
			get
			{
				return this._miscFlags;
			}
			set
			{
				if (value == this._miscFlags)
				{
					return;
				}
				this._miscFlags = value;
				if (NetworkServer.active)
				{
					base.SetDirtyBit(32U);
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000829BC File Offset: 0x00080BBC
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x000829CE File Offset: 0x00080BCE
		public bool lostBodyToDeath
		{
			get
			{
				return (this.miscFlags & this.lostBodyToDeathFlag) > 0U;
			}
			private set
			{
				if (value)
				{
					this.miscFlags |= this.lostBodyToDeathFlag;
					return;
				}
				this.miscFlags &= ~this.lostBodyToDeathFlag;
			}
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000829FC File Offset: 0x00080BFC
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 127U;
			}
			bool flag = (num & 1U) > 0U;
			bool flag2 = (num & 2U) > 0U;
			bool flag3 = (num & 64U) > 0U;
			bool flag4 = (num & 4U) > 0U;
			bool flag5 = (num & 8U) > 0U;
			bool flag6 = (num & 16U) > 0U;
			bool flag7 = (num & 32U) > 0U;
			writer.Write((byte)num);
			if (flag)
			{
				writer.Write(this._bodyInstanceId);
			}
			if (flag2)
			{
				writer.WritePackedUInt32(this._money);
			}
			if (flag3)
			{
				writer.WritePackedUInt32(this._voidCoins);
			}
			if (flag4)
			{
				writer.Write(this._internalSurvivalTime);
			}
			if (flag5)
			{
				writer.Write(this.teamIndex);
			}
			if (flag6)
			{
				this.loadout.Serialize(writer);
			}
			if (flag7)
			{
				writer.WritePackedUInt32(this.miscFlags);
			}
			return num > 0U;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00082AC4 File Offset: 0x00080CC4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			byte b = reader.ReadByte();
			bool flag = (b & 1) > 0;
			bool flag2 = (b & 2) > 0;
			bool flag3 = (b & 64) > 0;
			bool flag4 = (b & 4) > 0;
			bool flag5 = (b & 8) > 0;
			bool flag6 = (b & 16) > 0;
			bool flag7 = (b & 32) > 0;
			if (flag)
			{
				NetworkInstanceId value = reader.ReadNetworkId();
				this.OnSyncBodyInstanceId(value);
			}
			if (flag2)
			{
				this._money = reader.ReadPackedUInt32();
			}
			if (flag3)
			{
				this._voidCoins = reader.ReadPackedUInt32();
			}
			if (flag4)
			{
				this._internalSurvivalTime = reader.ReadSingle();
			}
			if (flag5)
			{
				this.teamIndex = reader.ReadTeamIndex();
			}
			if (flag6)
			{
				this.loadout.Deserialize(reader);
			}
			if (flag7)
			{
				this.miscFlags = reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00082BD8 File Offset: 0x00080DD8
		static CharacterMaster()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterMaster), CharacterMaster.kCmdCmdRespawn, new NetworkBehaviour.CmdDelegate(CharacterMaster.InvokeCmdCmdRespawn));
			NetworkCRC.RegisterBehaviour("CharacterMaster", 0);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00082C37 File Offset: 0x00080E37
		protected static void InvokeCmdCmdRespawn(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdRespawn called on client.");
				return;
			}
			((CharacterMaster)obj).CmdRespawn(reader.ReadString());
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00082C60 File Offset: 0x00080E60
		public void CallCmdRespawn(string bodyName)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdRespawn called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdRespawn(bodyName);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterMaster.kCmdCmdRespawn);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(bodyName);
			base.SendCommandInternal(networkWriter, 0, "CmdRespawn");
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040023E1 RID: 9185
		[Tooltip("This is assigned to the prefab automatically by MasterCatalog at runtime. Do not set this value manually.")]
		[HideInInspector]
		[SerializeField]
		private int _masterIndex;

		// Token: 0x040023E2 RID: 9186
		[Tooltip("The prefab of this character's body.")]
		public GameObject bodyPrefab;

		// Token: 0x040023E3 RID: 9187
		[Tooltip("Whether or not to spawn the body at the position of this manager object as soon as Start runs.")]
		public bool spawnOnStart;

		// Token: 0x040023E4 RID: 9188
		[FormerlySerializedAs("teamIndex")]
		[SerializeField]
		[Tooltip("The team of the body.")]
		private TeamIndex _teamIndex;

		// Token: 0x040023EC RID: 9196
		public UnityEvent onBodyDeath;

		// Token: 0x040023ED RID: 9197
		[Tooltip("Whether or not to destroy this master when the body dies.")]
		public bool destroyOnBodyDeath = true;

		// Token: 0x040023EE RID: 9198
		private static List<CharacterMaster> instancesList = new List<CharacterMaster>();

		// Token: 0x040023EF RID: 9199
		private static ReadOnlyCollection<CharacterMaster> _readOnlyInstancesList = new ReadOnlyCollection<CharacterMaster>(CharacterMaster.instancesList);

		// Token: 0x040023F1 RID: 9201
		private BaseAI[] aiComponents;

		// Token: 0x040023F5 RID: 9205
		private const uint bodyDirtyBit = 1U;

		// Token: 0x040023F6 RID: 9206
		private const uint moneyDirtyBit = 2U;

		// Token: 0x040023F7 RID: 9207
		private const uint survivalTimeDirtyBit = 4U;

		// Token: 0x040023F8 RID: 9208
		private const uint teamDirtyBit = 8U;

		// Token: 0x040023F9 RID: 9209
		private const uint loadoutDirtyBit = 16U;

		// Token: 0x040023FA RID: 9210
		private const uint miscFlagsDirtyBit = 32U;

		// Token: 0x040023FB RID: 9211
		private const uint voidCoinsDirtyBit = 64U;

		// Token: 0x040023FC RID: 9212
		private const uint allDirtyBits = 127U;

		// Token: 0x040023FD RID: 9213
		public readonly Loadout loadout = new Loadout();

		// Token: 0x040023FE RID: 9214
		private NetworkInstanceId _bodyInstanceId = NetworkInstanceId.Invalid;

		// Token: 0x040023FF RID: 9215
		private GameObject resolvedBodyInstance;

		// Token: 0x04002400 RID: 9216
		private bool bodyResolved;

		// Token: 0x04002402 RID: 9218
		private uint _money;

		// Token: 0x04002403 RID: 9219
		private uint _voidCoins;

		// Token: 0x04002405 RID: 9221
		public bool isBoss;

		// Token: 0x04002406 RID: 9222
		private Xoroshiro128Plus cloverVoidRng;

		// Token: 0x04002407 RID: 9223
		[NonSerialized]
		private List<DeployableInfo> deployablesList;

		// Token: 0x04002408 RID: 9224
		public bool preventGameOver = true;

		// Token: 0x0400240A RID: 9226
		private Vector3 deathAimVector = Vector3.zero;

		// Token: 0x0400240B RID: 9227
		private bool killedByUnsafeArea;

		// Token: 0x0400240C RID: 9228
		private const float respawnDelayDuration = 2f;

		// Token: 0x0400240D RID: 9229
		private float _internalSurvivalTime;

		// Token: 0x0400240E RID: 9230
		private BodyIndex killerBodyIndex = BodyIndex.None;

		// Token: 0x0400240F RID: 9231
		private bool preventRespawnUntilNextStageServer;

		// Token: 0x04002410 RID: 9232
		private bool godMode;

		// Token: 0x04002411 RID: 9233
		private uint lostBodyToDeathFlag = 1U;

		// Token: 0x04002412 RID: 9234
		private uint _miscFlags;

		// Token: 0x04002413 RID: 9235
		private static int kCmdCmdRespawn = 1097984413;
	}
}
