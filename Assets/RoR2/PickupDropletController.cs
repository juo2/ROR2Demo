using System;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000800 RID: 2048
	public class PickupDropletController : NetworkBehaviour
	{
		// Token: 0x06002C25 RID: 11301 RVA: 0x000BCEF4 File Offset: 0x000BB0F4
		public static void CreatePickupDroplet(PickupIndex pickupIndex, Vector3 position, Vector3 velocity)
		{
			PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
			{
				rotation = Quaternion.identity,
				pickupIndex = pickupIndex
			}, position, velocity);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000BCF28 File Offset: 0x000BB128
		public static void CreatePickupDroplet(GenericPickupController.CreatePickupInfo pickupInfo, Vector3 position, Vector3 velocity)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(PickupDropletController.pickupDropletPrefab, position, Quaternion.identity);
			PickupDropletController component = gameObject.GetComponent<PickupDropletController>();
			if (component)
			{
				component.createPickupInfo = pickupInfo;
				component.NetworkpickupIndex = pickupInfo.pickupIndex;
			}
			Rigidbody component2 = gameObject.GetComponent<Rigidbody>();
			component2.velocity = velocity;
			component2.AddTorque(UnityEngine.Random.Range(150f, 120f) * UnityEngine.Random.onUnitSphere);
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000BCF98 File Offset: 0x000BB198
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			PickupDropletController.pickupDropletPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/PickupDroplet");
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000BCFAC File Offset: 0x000BB1AC
		public void OnCollisionEnter(Collision collision)
		{
			if (NetworkServer.active && this.alive)
			{
				this.alive = false;
				this.createPickupInfo.position = base.transform.position;
				bool flag = true;
				PickupDropletController.PickupDropletHitGroundDelegate pickupDropletHitGroundDelegate = PickupDropletController.onDropletHitGroundServer;
				if (pickupDropletHitGroundDelegate != null)
				{
					pickupDropletHitGroundDelegate(ref this.createPickupInfo, ref flag);
				}
				if (flag)
				{
					GenericPickupController.CreatePickup(this.createPickupInfo);
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06002C29 RID: 11305 RVA: 0x000BD01C File Offset: 0x000BB21C
		// (remove) Token: 0x06002C2A RID: 11306 RVA: 0x000BD050 File Offset: 0x000BB250
		public static event PickupDropletController.PickupDropletHitGroundDelegate onDropletHitGroundServer;

		// Token: 0x06002C2B RID: 11307 RVA: 0x000BD084 File Offset: 0x000BB284
		private void Start()
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
			GameObject gameObject = (pickupDef != null) ? pickupDef.dropletDisplayPrefab : null;
			if (gameObject)
			{
				UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform);
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000BD0D8 File Offset: 0x000BB2D8
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x000BD0EB File Offset: 0x000BB2EB
		public PickupIndex NetworkpickupIndex
		{
			get
			{
				return this.pickupIndex;
			}
			[param: In]
			set
			{
				base.SetSyncVar<PickupIndex>(value, ref this.pickupIndex, 1U);
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000BD100 File Offset: 0x000BB300
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
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
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000BD16C File Offset: 0x000BB36C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EA0 RID: 11936
		[SyncVar]
		[NonSerialized]
		public PickupIndex pickupIndex = PickupIndex.none;

		// Token: 0x04002EA1 RID: 11937
		private bool alive = true;

		// Token: 0x04002EA2 RID: 11938
		private GenericPickupController.CreatePickupInfo createPickupInfo;

		// Token: 0x04002EA3 RID: 11939
		private static GameObject pickupDropletPrefab;

		// Token: 0x02000801 RID: 2049
		// (Invoke) Token: 0x06002C34 RID: 11316
		public delegate void PickupDropletHitGroundDelegate(ref GenericPickupController.CreatePickupInfo createPickupInfo, ref bool shouldSpawn);
	}
}
