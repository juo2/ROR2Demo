using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000929 RID: 2345
	public class HealingFollowerController : NetworkBehaviour
	{
		// Token: 0x060034F9 RID: 13561 RVA: 0x000E0439 File Offset: 0x000DE639
		private void FixedUpdate()
		{
			if (this.cachedTargetBodyObject != this.targetBodyObject)
			{
				this.cachedTargetBodyObject = this.targetBodyObject;
				this.OnTargetChanged();
			}
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000E0470 File Offset: 0x000DE670
		[Server]
		public void AssignNewTarget(GameObject target)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealingFollowerController::AssignNewTarget(UnityEngine.GameObject)' called on client");
				return;
			}
			this.NetworktargetBodyObject = (target ? target : this.ownerBodyObject);
			this.cachedTargetBodyObject = this.targetBodyObject;
			this.cachedTargetHealthComponent = (this.cachedTargetBodyObject ? this.cachedTargetBodyObject.GetComponent<HealthComponent>() : null);
			this.OnTargetChanged();
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000E04DC File Offset: 0x000DE6DC
		private void OnTargetChanged()
		{
			this.cachedTargetHealthComponent = (this.cachedTargetBodyObject ? this.cachedTargetBodyObject.GetComponent<HealthComponent>() : null);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000E0500 File Offset: 0x000DE700
		private void FixedUpdateServer()
		{
			this.healingTimer -= Time.fixedDeltaTime;
			if (this.healingTimer <= 0f)
			{
				this.healingTimer = this.healingInterval;
				this.DoHeal(this.fractionHealthHealing * this.healingInterval);
			}
			if (!this.targetBodyObject)
			{
				this.NetworktargetBodyObject = this.ownerBodyObject;
			}
			if (!this.ownerBodyObject)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000E057C File Offset: 0x000DE77C
		private void Update()
		{
			this.UpdateMotion();
			base.transform.position += this.velocity * Time.deltaTime;
			base.transform.rotation = Quaternion.AngleAxis(this.rotationAngularVelocity * Time.deltaTime, Vector3.up) * base.transform.rotation;
			if (this.targetBodyObject)
			{
				this.indicator.transform.position = this.GetTargetPosition();
			}
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000E060C File Offset: 0x000DE80C
		[Server]
		private void DoHeal(float healFraction)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealingFollowerController::DoHeal(System.Single)' called on client");
				return;
			}
			if (!this.cachedTargetHealthComponent)
			{
				return;
			}
			this.cachedTargetHealthComponent.HealFraction(healFraction, default(ProcChainMask));
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000E0652 File Offset: 0x000DE852
		public override void OnStartClient()
		{
			base.OnStartClient();
			base.transform.position = this.GetDesiredPosition();
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000E066C File Offset: 0x000DE86C
		private Vector3 GetTargetPosition()
		{
			GameObject gameObject = this.targetBodyObject ?? this.ownerBodyObject;
			if (!gameObject)
			{
				return base.transform.position;
			}
			CharacterBody component = gameObject.GetComponent<CharacterBody>();
			if (!component)
			{
				return gameObject.transform.position;
			}
			return component.corePosition;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000E06BF File Offset: 0x000DE8BF
		private Vector3 GetDesiredPosition()
		{
			return this.GetTargetPosition();
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000E06C8 File Offset: 0x000DE8C8
		private void UpdateMotion()
		{
			Vector3 desiredPosition = this.GetDesiredPosition();
			if (this.enableSpringMotion)
			{
				Vector3 lhs = desiredPosition - base.transform.position;
				if (lhs != Vector3.zero)
				{
					Vector3 a = lhs.normalized * this.acceleration;
					Vector3 b = this.velocity * -this.damping;
					this.velocity += (a + b) * Time.deltaTime;
					return;
				}
			}
			else
			{
				base.transform.position = Vector3.SmoothDamp(base.transform.position, desiredPosition, ref this.velocity, this.damping);
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x000E07B4 File Offset: 0x000DE9B4
		// (set) Token: 0x06003506 RID: 13574 RVA: 0x000E07C7 File Offset: 0x000DE9C7
		public GameObject NetworkownerBodyObject
		{
			get
			{
				return this.ownerBodyObject;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.ownerBodyObject, 1U, ref this.___ownerBodyObjectNetId);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000E07E4 File Offset: 0x000DE9E4
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x000E07F7 File Offset: 0x000DE9F7
		public GameObject NetworktargetBodyObject
		{
			get
			{
				return this.targetBodyObject;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.targetBodyObject, 2U, ref this.___targetBodyObjectNetId);
			}
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000E0814 File Offset: 0x000DEA14
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.ownerBodyObject);
				writer.Write(this.targetBodyObject);
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
				writer.Write(this.ownerBodyObject);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.targetBodyObject);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000E08C0 File Offset: 0x000DEAC0
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___ownerBodyObjectNetId = reader.ReadNetworkId();
				this.___targetBodyObjectNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.ownerBodyObject = reader.ReadGameObject();
			}
			if ((num & 2) != 0)
			{
				this.targetBodyObject = reader.ReadGameObject();
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000E0928 File Offset: 0x000DEB28
		public override void PreStartClient()
		{
			if (!this.___ownerBodyObjectNetId.IsEmpty())
			{
				this.NetworkownerBodyObject = ClientScene.FindLocalObject(this.___ownerBodyObjectNetId);
			}
			if (!this.___targetBodyObjectNetId.IsEmpty())
			{
				this.NetworktargetBodyObject = ClientScene.FindLocalObject(this.___targetBodyObjectNetId);
			}
		}

		// Token: 0x040035E0 RID: 13792
		public float fractionHealthHealing = 0.01f;

		// Token: 0x040035E1 RID: 13793
		public float fractionHealthBurst = 0.05f;

		// Token: 0x040035E2 RID: 13794
		public float healingInterval = 1f;

		// Token: 0x040035E3 RID: 13795
		public float rotationAngularVelocity;

		// Token: 0x040035E4 RID: 13796
		public float acceleration = 20f;

		// Token: 0x040035E5 RID: 13797
		public float damping = 0.1f;

		// Token: 0x040035E6 RID: 13798
		public bool enableSpringMotion;

		// Token: 0x040035E7 RID: 13799
		[SyncVar]
		public GameObject ownerBodyObject;

		// Token: 0x040035E8 RID: 13800
		[SyncVar]
		public GameObject targetBodyObject;

		// Token: 0x040035E9 RID: 13801
		public GameObject indicator;

		// Token: 0x040035EA RID: 13802
		private GameObject cachedTargetBodyObject;

		// Token: 0x040035EB RID: 13803
		private HealthComponent cachedTargetHealthComponent;

		// Token: 0x040035EC RID: 13804
		private float healingTimer;

		// Token: 0x040035ED RID: 13805
		private Vector3 velocity;

		// Token: 0x040035EE RID: 13806
		private NetworkInstanceId ___ownerBodyObjectNetId;

		// Token: 0x040035EF RID: 13807
		private NetworkInstanceId ___targetBodyObjectNetId;
	}
}
