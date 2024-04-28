using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B78 RID: 2936
	[RequireComponent(typeof(ProjectileTargetComponent))]
	[RequireComponent(typeof(ProjectileController))]
	public class GatewayProjectileController : NetworkBehaviour, IInteractable
	{
		// Token: 0x060042E3 RID: 17123 RVA: 0x00062756 File Offset: 0x00060956
		public string GetContextString(Interactor activator)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x001152FC File Offset: 0x001134FC
		public Interactability GetInteractability(Interactor activator)
		{
			if (!this.linkedObject)
			{
				return Interactability.ConditionsNotMet;
			}
			return Interactability.Available;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x00062756 File Offset: 0x00060956
		public void OnInteractionBegin(Interactor activator)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldShowOnScanner()
		{
			return false;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x00115310 File Offset: 0x00113510
		private void SetLinkedObject(GameObject newLinkedObject)
		{
			if (this.linkedObject != newLinkedObject)
			{
				this.NetworklinkedObject = newLinkedObject;
				this.linkedGatewayProjectileController = (this.linkedObject ? this.linkedObject.GetComponent<GatewayProjectileController>() : null);
				if (this.linkedGatewayProjectileController)
				{
					this.linkedGatewayProjectileController.SetLinkedObject(base.gameObject);
				}
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x00115371 File Offset: 0x00113571
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileTargetComponent = base.GetComponent<ProjectileTargetComponent>();
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0011538C File Offset: 0x0011358C
		public override void OnStartServer()
		{
			base.OnStartServer();
			if (this.projectileTargetComponent.target)
			{
				this.SetLinkedObject(this.projectileTargetComponent.target.gameObject);
				return;
			}
			FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
			fireProjectileInfo.position = base.transform.position;
			fireProjectileInfo.rotation = base.transform.rotation;
			fireProjectileInfo.target = base.gameObject;
			fireProjectileInfo.owner = this.projectileController.owner;
			fireProjectileInfo.speedOverride = 0f;
			fireProjectileInfo.projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/GatewayProjectile");
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x0011543C File Offset: 0x0011363C
		// (set) Token: 0x060042EE RID: 17134 RVA: 0x00115450 File Offset: 0x00113650
		public GameObject NetworklinkedObject
		{
			get
			{
				return this.linkedObject;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetLinkedObject(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVarGameObject(value, ref this.linkedObject, 1U, ref this.___linkedObjectNetId);
			}
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x001154A0 File Offset: 0x001136A0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.linkedObject);
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
				writer.Write(this.linkedObject);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0011550C File Offset: 0x0011370C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___linkedObjectNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetLinkedObject(reader.ReadGameObject());
			}
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0011554D File Offset: 0x0011374D
		public override void PreStartClient()
		{
			if (!this.___linkedObjectNetId.IsEmpty())
			{
				this.NetworklinkedObject = ClientScene.FindLocalObject(this.___linkedObjectNetId);
			}
		}

		// Token: 0x040040E4 RID: 16612
		private ProjectileController projectileController;

		// Token: 0x040040E5 RID: 16613
		private ProjectileTargetComponent projectileTargetComponent;

		// Token: 0x040040E6 RID: 16614
		[SyncVar(hook = "SetLinkedObject")]
		private GameObject linkedObject;

		// Token: 0x040040E7 RID: 16615
		private GatewayProjectileController linkedGatewayProjectileController;

		// Token: 0x040040E8 RID: 16616
		private NetworkInstanceId ___linkedObjectNetId;
	}
}
