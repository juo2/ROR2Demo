using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008F5 RID: 2293
	public class VoidStageMissionController : NetworkBehaviour
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000D9798 File Offset: 0x000D7998
		public int numBatteriesSpawned
		{
			get
			{
				return this._numBatteriesSpawned;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000D97A0 File Offset: 0x000D79A0
		public int numBatteriesActivated
		{
			get
			{
				return this._numBatteriesActivated;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000D97A8 File Offset: 0x000D79A8
		// (set) Token: 0x060033A1 RID: 13217 RVA: 0x000D97AF File Offset: 0x000D79AF
		public static VoidStageMissionController instance { get; private set; }

		// Token: 0x060033A2 RID: 13218 RVA: 0x000D97B8 File Offset: 0x000D79B8
		public VoidStageMissionController.FogRequest RequestFog(IZone zone)
		{
			if (this.fogDamageController)
			{
				if (!this.fogDamageController.enabled)
				{
					this.fogDamageController.enabled = true;
				}
				this.fogRefCount++;
				this.fogDamageController.AddSafeZone(zone);
				return new VoidStageMissionController.FogRequest(zone, new Action<VoidStageMissionController.FogRequest>(this.OnFogUnrequested));
			}
			return null;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000D981C File Offset: 0x000D7A1C
		private void OnFogUnrequested(VoidStageMissionController.FogRequest request)
		{
			if (this.fogDamageController)
			{
				this.fogDamageController.RemoveSafeZone(request.safeZone);
				Behaviour behaviour = this.fogDamageController;
				int num = this.fogRefCount - 1;
				this.fogRefCount = num;
				behaviour.enabled = (num > 0);
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000D9868 File Offset: 0x000D7A68
		private void Start()
		{
			if (this.deepVoidPortalObjectiveProvider)
			{
				this.deepVoidPortalObjectiveProvider.enabled = false;
			}
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
				this.Network_numBatteriesSpawned = 0;
				for (int i = 0; i < this.batteryCount; i++)
				{
					DirectorPlacementRule placementRule = new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Random
					};
					DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.batterySpawnCard, placementRule, this.rng);
					GameObject gameObject = DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
					if (gameObject && gameObject.GetComponent<PurchaseInteraction>())
					{
						this.Network_numBatteriesSpawned = this._numBatteriesSpawned + 1;
					}
				}
			}
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000D9919 File Offset: 0x000D7B19
		private void FixedUpdate()
		{
			if (this.deepVoidPortalObjectiveProvider && this.numBatteriesActivated >= this.numBatteriesSpawned && this.numBatteriesSpawned > 0)
			{
				this.deepVoidPortalObjectiveProvider.enabled = true;
			}
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000D994C File Offset: 0x000D7B4C
		public void OnBatteryActivated()
		{
			this.Network_numBatteriesActivated = this._numBatteriesActivated + 1;
			if (this._numBatteriesActivated >= this._numBatteriesSpawned && this.portalRoot)
			{
				CombatDirector[] array = new CombatDirector[CombatDirector.instancesList.Count];
				CombatDirector.instancesList.CopyTo(array);
				CombatDirector[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i].enabled = false;
				}
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Direct,
					spawnOnTarget = this.portalRoot
				};
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.deepVoidPortalSpawnCard, placementRule, this.rng);
				this.voidPortal = DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
				this.voidPortal.transform.rotation = this.portalRoot.rotation;
				NetworkServer.Spawn(this.voidPortal);
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000D9A24 File Offset: 0x000D7C24
		private void OnEnable()
		{
			VoidStageMissionController.instance = SingletonHelper.Assign<VoidStageMissionController>(VoidStageMissionController.instance, this);
			ObjectivePanelController.collectObjectiveSources += this.OnCollectObjectiveSources;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000D9A47 File Offset: 0x000D7C47
		private void OnDisable()
		{
			VoidStageMissionController.instance = SingletonHelper.Unassign<VoidStageMissionController>(VoidStageMissionController.instance, this);
			ObjectivePanelController.collectObjectiveSources -= this.OnCollectObjectiveSources;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000D9A6C File Offset: 0x000D7C6C
		private void OnCollectObjectiveSources(CharacterMaster master, List<ObjectivePanelController.ObjectiveSourceDescriptor> objectiveSourcesList)
		{
			if (this._numBatteriesSpawned > 0 && this._numBatteriesActivated < this._numBatteriesSpawned)
			{
				objectiveSourcesList.Add(new ObjectivePanelController.ObjectiveSourceDescriptor
				{
					master = master,
					objectiveType = typeof(VoidStageBatteryMissionObjectiveTracker),
					source = this
				});
			}
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000D9AC0 File Offset: 0x000D7CC0
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000D9AD3 File Offset: 0x000D7CD3
		public int Network_numBatteriesSpawned
		{
			get
			{
				return this._numBatteriesSpawned;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._numBatteriesSpawned, 1U);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000D9AE8 File Offset: 0x000D7CE8
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x000D9AFB File Offset: 0x000D7CFB
		public int Network_numBatteriesActivated
		{
			get
			{
				return this._numBatteriesActivated;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._numBatteriesActivated, 2U);
			}
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000D9B10 File Offset: 0x000D7D10
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this._numBatteriesSpawned);
				writer.WritePackedUInt32((uint)this._numBatteriesActivated);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this._numBatteriesSpawned);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this._numBatteriesActivated);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000D9BBC File Offset: 0x000D7DBC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._numBatteriesSpawned = (int)reader.ReadPackedUInt32();
				this._numBatteriesActivated = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._numBatteriesSpawned = (int)reader.ReadPackedUInt32();
			}
			if ((num & 2) != 0)
			{
				this._numBatteriesActivated = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003495 RID: 13461
		[SerializeField]
		private InteractableSpawnCard batterySpawnCard;

		// Token: 0x04003496 RID: 13462
		[SerializeField]
		private int batteryCount;

		// Token: 0x04003497 RID: 13463
		[SerializeField]
		private Transform portalRoot;

		// Token: 0x04003498 RID: 13464
		[SerializeField]
		private string portalOpenChatToken;

		// Token: 0x04003499 RID: 13465
		[SerializeField]
		private GenericObjectiveProvider deepVoidPortalObjectiveProvider;

		// Token: 0x0400349A RID: 13466
		[SerializeField]
		private InteractableSpawnCard deepVoidPortalSpawnCard;

		// Token: 0x0400349B RID: 13467
		[SerializeField]
		private InteractableSpawnCard voidPortalSpawnCard;

		// Token: 0x0400349C RID: 13468
		[SerializeField]
		public string batteryObjectiveToken;

		// Token: 0x0400349D RID: 13469
		[SerializeField]
		private FogDamageController fogDamageController;

		// Token: 0x0400349E RID: 13470
		private Xoroshiro128Plus rng;

		// Token: 0x0400349F RID: 13471
		private GameObject voidPortal;

		// Token: 0x040034A0 RID: 13472
		private int fogRefCount;

		// Token: 0x040034A1 RID: 13473
		[SyncVar]
		private int _numBatteriesSpawned;

		// Token: 0x040034A2 RID: 13474
		[SyncVar]
		private int _numBatteriesActivated;

		// Token: 0x020008F6 RID: 2294
		public class FogRequest : IDisposable
		{
			// Token: 0x060033B3 RID: 13235 RVA: 0x000D9C22 File Offset: 0x000D7E22
			public FogRequest(IZone zone, Action<VoidStageMissionController.FogRequest> onDispose)
			{
				this.safeZone = zone;
				this.disposeCallback = onDispose;
			}

			// Token: 0x060033B4 RID: 13236 RVA: 0x000D9C38 File Offset: 0x000D7E38
			public void Dispose()
			{
				Action<VoidStageMissionController.FogRequest> action = this.disposeCallback;
				if (action != null)
				{
					action(this);
				}
				this.disposeCallback = null;
			}

			// Token: 0x040034A4 RID: 13476
			public readonly IZone safeZone;

			// Token: 0x040034A5 RID: 13477
			private Action<VoidStageMissionController.FogRequest> disposeCallback;
		}
	}
}
