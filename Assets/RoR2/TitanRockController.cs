using System;
using System.Runtime.InteropServices;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008CA RID: 2250
	public class TitanRockController : NetworkBehaviour
	{
		// Token: 0x06003266 RID: 12902 RVA: 0x000D4A64 File Offset: 0x000D2C64
		private void Start()
		{
			if (!NetworkServer.active)
			{
				this.SetOwner(this.owner);
				return;
			}
			this.fireTimer = this.startDelay;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000D4A88 File Offset: 0x000D2C88
		public void SetOwner(GameObject newOwner)
		{
			this.ownerInputBank = null;
			this.ownerCharacterBody = null;
			this.isCrit = false;
			this.Networkowner = newOwner;
			if (this.owner)
			{
				this.ownerInputBank = this.owner.GetComponent<InputBankTest>();
				this.ownerCharacterBody = this.owner.GetComponent<CharacterBody>();
				ModelLocator component = this.owner.GetComponent<ModelLocator>();
				if (component)
				{
					Transform modelTransform = component.modelTransform;
					if (modelTransform)
					{
						ChildLocator component2 = modelTransform.GetComponent<ChildLocator>();
						if (component2)
						{
							this.targetTransform = component2.FindChild("Chest");
							if (this.targetTransform)
							{
								base.transform.rotation = this.targetTransform.rotation;
							}
						}
					}
				}
				base.transform.position = this.owner.transform.position + Vector3.down * 20f;
				if (NetworkServer.active && this.ownerCharacterBody)
				{
					CharacterMaster master = this.ownerCharacterBody.master;
					if (master)
					{
						this.isCrit = Util.CheckRoll(this.ownerCharacterBody.crit, master);
					}
				}
			}
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000D4BB8 File Offset: 0x000D2DB8
		public void FixedUpdate()
		{
			if (this.targetTransform)
			{
				this.foundOwner = true;
				base.transform.position = Vector3.SmoothDamp(base.transform.position, this.targetTransform.TransformPoint(TitanRockController.targetLocalPosition), ref this.velocity, 1f);
				base.transform.rotation = this.targetTransform.rotation;
			}
			else if (this.foundOwner)
			{
				this.foundOwner = false;
				foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.main.gravityModifier = 1f;
					particleSystem.emission.enabled = false;
					particleSystem.noise.enabled = false;
					particleSystem.limitVelocityOverLifetime.enabled = false;
				}
				base.transform.Find("Debris").GetComponent<ParticleSystem>().collision.enabled = true;
				Light[] componentsInChildren2 = base.GetComponentsInChildren<Light>();
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].enabled = false;
				}
				Util.PlaySound("Stop_titanboss_shift_loop", base.gameObject);
			}
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000D4D00 File Offset: 0x000D2F00
		[Server]
		private void FixedUpdateServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TitanRockController::FixedUpdateServer()' called on client");
				return;
			}
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				this.Fire();
				this.fireTimer += this.fireInterval;
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000D4D5C File Offset: 0x000D2F5C
		[Server]
		private void Fire()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TitanRockController::Fire()' called on client");
				return;
			}
			if (this.ownerInputBank)
			{
				Vector3 position = this.fireTransform.position;
				Vector3 forward = this.ownerInputBank.aimDirection;
				RaycastHit raycastHit;
				if (Util.CharacterRaycast(this.owner, new Ray(this.ownerInputBank.aimOrigin, this.ownerInputBank.aimDirection), out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
				{
					forward = raycastHit.point - position;
				}
				float num = this.ownerCharacterBody ? this.ownerCharacterBody.damage : 1f;
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, position, Util.QuaternionSafeLookRotation(forward), this.owner, this.damageCoefficient * num, this.damageForce, this.isCrit, DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000D4EA4 File Offset: 0x000D30A4
		// (set) Token: 0x0600326F RID: 12911 RVA: 0x000D4EB8 File Offset: 0x000D30B8
		public GameObject Networkowner
		{
			get
			{
				return this.owner;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetOwner(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVarGameObject(value, ref this.owner, 1U, ref this.___ownerNetId);
			}
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000D4F08 File Offset: 0x000D3108
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.owner);
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
				writer.Write(this.owner);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000D4F74 File Offset: 0x000D3174
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___ownerNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetOwner(reader.ReadGameObject());
			}
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000D4FB5 File Offset: 0x000D31B5
		public override void PreStartClient()
		{
			if (!this.___ownerNetId.IsEmpty())
			{
				this.Networkowner = ClientScene.FindLocalObject(this.___ownerNetId);
			}
		}

		// Token: 0x04003381 RID: 13185
		[Tooltip("The child transform from which projectiles will be fired.")]
		public Transform fireTransform;

		// Token: 0x04003382 RID: 13186
		[Tooltip("How long it takes to start firing.")]
		public float startDelay = 4f;

		// Token: 0x04003383 RID: 13187
		[Tooltip("Firing interval.")]
		public float fireInterval = 1f;

		// Token: 0x04003384 RID: 13188
		[Tooltip("The prefab to fire as a projectile.")]
		public GameObject projectilePrefab;

		// Token: 0x04003385 RID: 13189
		[Tooltip("The damage coefficient to multiply by the owner's damage stat for the projectile's final damage value.")]
		public float damageCoefficient;

		// Token: 0x04003386 RID: 13190
		[Tooltip("The force of the projectile's damage.")]
		public float damageForce;

		// Token: 0x04003387 RID: 13191
		[SyncVar(hook = "SetOwner")]
		private GameObject owner;

		// Token: 0x04003388 RID: 13192
		private Transform targetTransform;

		// Token: 0x04003389 RID: 13193
		private Vector3 velocity;

		// Token: 0x0400338A RID: 13194
		private static readonly Vector3 targetLocalPosition = new Vector3(0f, 12f, -3f);

		// Token: 0x0400338B RID: 13195
		private float fireTimer;

		// Token: 0x0400338C RID: 13196
		private InputBankTest ownerInputBank;

		// Token: 0x0400338D RID: 13197
		private CharacterBody ownerCharacterBody;

		// Token: 0x0400338E RID: 13198
		private bool isCrit;

		// Token: 0x0400338F RID: 13199
		private bool foundOwner;

		// Token: 0x04003390 RID: 13200
		private NetworkInstanceId ___ownerNetId;
	}
}
