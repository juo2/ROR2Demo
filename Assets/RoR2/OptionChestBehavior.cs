using System;
using EntityStates;
using EntityStates.Barrel;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007EB RID: 2027
	public class OptionChestBehavior : NetworkBehaviour, IChestBehavior
	{
		// Token: 0x06002BBC RID: 11196 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000BB3C5 File Offset: 0x000B95C5
		[Server]
		public void Roll()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.OptionChestBehavior::Roll()' called on client");
				return;
			}
			this.generatedDrops = this.dropTable.GenerateUniqueDrops(this.numOptions, this.rng);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000BB3F9 File Offset: 0x000B95F9
		private void Awake()
		{
			if (this.dropTransform == null)
			{
				this.dropTransform = base.transform;
			}
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000BB415 File Offset: 0x000B9615
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
				this.Roll();
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000BB440 File Offset: 0x000B9640
		[Server]
		public void Open()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.OptionChestBehavior::Open()' called on client");
				return;
			}
			EntityStateMachine component = base.GetComponent<EntityStateMachine>();
			if (component)
			{
				component.SetNextState(EntityStateCatalog.InstantiateState(this.openState));
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000BB484 File Offset: 0x000B9684
		[Server]
		public void ItemDrop()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.OptionChestBehavior::ItemDrop()' called on client");
				return;
			}
			if (this.generatedDrops == null || this.generatedDrops.Length == 0)
			{
				return;
			}
			PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
			{
				pickerOptions = PickupPickerController.GenerateOptionsFromArray(this.generatedDrops),
				prefabOverride = this.pickupPrefab,
				position = this.dropTransform.position,
				rotation = Quaternion.identity,
				pickupIndex = PickupCatalog.FindPickupIndex(this.displayTier)
			}, this.dropTransform.position, Vector3.up * this.dropUpVelocityStrength + this.dropTransform.forward * this.dropForwardVelocityStrength);
			this.generatedDrops = null;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000BB554 File Offset: 0x000B9754
		public bool HasRolledPickup(PickupIndex pickupIndex)
		{
			if (this.generatedDrops != null)
			{
				PickupIndex[] array = this.generatedDrops;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == pickupIndex)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000BB5C4 File Offset: 0x000B97C4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002E28 RID: 11816
		[SerializeField]
		private PickupDropTable dropTable;

		// Token: 0x04002E29 RID: 11817
		[SerializeField]
		private Transform dropTransform;

		// Token: 0x04002E2A RID: 11818
		[SerializeField]
		private float dropUpVelocityStrength = 20f;

		// Token: 0x04002E2B RID: 11819
		[SerializeField]
		private float dropForwardVelocityStrength = 2f;

		// Token: 0x04002E2C RID: 11820
		[SerializeField]
		private SerializableEntityStateType openState = new SerializableEntityStateType(typeof(Opening));

		// Token: 0x04002E2D RID: 11821
		[SerializeField]
		private GameObject pickupPrefab;

		// Token: 0x04002E2E RID: 11822
		[SerializeField]
		private int numOptions;

		// Token: 0x04002E2F RID: 11823
		[SerializeField]
		private ItemTier displayTier;

		// Token: 0x04002E30 RID: 11824
		private PickupIndex[] generatedDrops;

		// Token: 0x04002E31 RID: 11825
		private Xoroshiro128Plus rng;
	}
}
