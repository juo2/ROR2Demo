using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B7B RID: 2939
	[RequireComponent(typeof(ProjectileController))]
	public class HookProjectileImpact : NetworkBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060042F8 RID: 17144 RVA: 0x0011580C File Offset: 0x00113A0C
		private void Start()
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.ownerTransform = this.projectileController.owner.transform;
			if (this.ownerTransform)
			{
				ModelLocator component = this.ownerTransform.GetComponent<ModelLocator>();
				if (component)
				{
					Transform modelTransform = component.modelTransform;
					if (modelTransform)
					{
						ChildLocator component2 = modelTransform.GetComponent<ChildLocator>();
						if (component2)
						{
							this.ownerTransform = component2.FindChild(this.attachmentString);
						}
					}
				}
			}
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x001158A4 File Offset: 0x00113AA4
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			EffectManager.SimpleImpactEffect(this.impactSpark, impactInfo.estimatedPointOfImpact, -base.transform.forward, true);
			if (this.hookState != HookProjectileImpact.HookState.Flying)
			{
				return;
			}
			HurtBox component = impactInfo.collider.GetComponent<HurtBox>();
			if (component)
			{
				HealthComponent healthComponent = component.healthComponent;
				if (healthComponent)
				{
					TeamIndex teamIndex = this.projectileController.teamFilter.teamIndex;
					if (!FriendlyFireManager.ShouldDirectHitProceed(healthComponent, teamIndex))
					{
						return;
					}
					this.Networkvictim = healthComponent.gameObject;
					DamageInfo damageInfo = new DamageInfo();
					if (this.projectileDamage)
					{
						damageInfo.damage = this.projectileDamage.damage;
						damageInfo.crit = this.projectileDamage.crit;
						damageInfo.attacker = (this.projectileController.owner ? this.projectileController.owner.gameObject : null);
						damageInfo.inflictor = base.gameObject;
						damageInfo.position = impactInfo.estimatedPointOfImpact;
						damageInfo.force = this.projectileDamage.force * base.transform.forward;
						damageInfo.procChainMask = this.projectileController.procChainMask;
						damageInfo.procCoefficient = this.projectileController.procCoefficient;
						damageInfo.damageColorIndex = this.projectileDamage.damageColorIndex;
					}
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					this.NetworkhookState = HookProjectileImpact.HookState.HitDelay;
					EffectManager.SimpleImpactEffect(this.impactSuccess, impactInfo.estimatedPointOfImpact, -base.transform.forward, true);
					base.gameObject.layer = LayerIndex.noCollision.intVal;
				}
			}
			if (!this.victim)
			{
				this.NetworkhookState = HookProjectileImpact.HookState.ReelFail;
			}
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x00115A68 File Offset: 0x00113C68
		private bool Reel()
		{
			Vector3 vector = this.ownerTransform.position - this.victim.transform.position;
			Vector3 normalized = vector.normalized;
			float num = vector.magnitude;
			Collider component = this.projectileController.owner.GetComponent<Collider>();
			Collider component2 = this.victim.GetComponent<Collider>();
			if (component && component2)
			{
				num = Util.EstimateSurfaceDistance(component, component2);
			}
			bool flag = num <= this.pullMinimumDistance;
			float num2 = -1f;
			CharacterMotor component3 = this.projectileController.owner.GetComponent<CharacterMotor>();
			if (component3)
			{
				num2 = component3.mass;
			}
			else
			{
				Rigidbody component4 = this.projectileController.owner.GetComponent<Rigidbody>();
				if (component4)
				{
					num2 = component4.mass;
				}
			}
			Rigidbody rigidbody = null;
			float num3 = -1f;
			CharacterMotor component5 = this.victim.GetComponent<CharacterMotor>();
			if (component5)
			{
				num3 = component5.mass;
			}
			else
			{
				rigidbody = this.victim.GetComponent<Rigidbody>();
				if (rigidbody)
				{
					num3 = rigidbody.mass;
				}
			}
			float num4 = 0f;
			if (num2 > 0f && num3 > 0f)
			{
				float num5 = 1f - num2 / (num2 + num3);
				num4 = 1f - num5;
			}
			else if (num2 <= 0f)
			{
				if (num3 > 0f)
				{
					num4 = 1f;
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				num4 = 0f;
			}
			Vector3 velocity = normalized * (num4 * this.victimPullFactor * this.reelSpeed);
			if (component5)
			{
				component5.velocity = velocity;
			}
			if (rigidbody)
			{
				rigidbody.velocity = velocity;
			}
			return flag;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00115C40 File Offset: 0x00113E40
		public void FixedUpdate()
		{
			if (NetworkServer.active && !this.projectileController.owner)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			if (this.victim)
			{
				this.rigidbody.MovePosition(this.victim.transform.position);
			}
			switch (this.hookState)
			{
			case HookProjectileImpact.HookState.Flying:
				if (NetworkServer.active)
				{
					this.flyTimer += Time.fixedDeltaTime;
					if (this.flyTimer >= this.liveTimer)
					{
						this.NetworkhookState = HookProjectileImpact.HookState.ReelFail;
						return;
					}
				}
				break;
			case HookProjectileImpact.HookState.HitDelay:
				if (NetworkServer.active)
				{
					if (!this.victim)
					{
						this.NetworkhookState = HookProjectileImpact.HookState.Reel;
						return;
					}
					this.delayTimer += Time.fixedDeltaTime;
					if (this.delayTimer >= this.reelDelayTime)
					{
						this.NetworkhookState = HookProjectileImpact.HookState.Reel;
						return;
					}
				}
				break;
			case HookProjectileImpact.HookState.Reel:
			{
				bool flag = true;
				if (this.victim)
				{
					flag = this.Reel();
				}
				if (NetworkServer.active)
				{
					if (!this.victim)
					{
						this.NetworkhookState = HookProjectileImpact.HookState.ReelFail;
					}
					if (flag)
					{
						UnityEngine.Object.Destroy(base.gameObject);
						return;
					}
				}
				break;
			}
			case HookProjectileImpact.HookState.ReelFail:
				if (NetworkServer.active)
				{
					if (this.rigidbody)
					{
						this.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
						this.rigidbody.isKinematic = true;
					}
					if (this.ownerTransform)
					{
						this.rigidbody.MovePosition(Vector3.MoveTowards(base.transform.position, this.ownerTransform.position, this.reelSpeed * Time.fixedDeltaTime));
						if (base.transform.position == this.ownerTransform.position)
						{
							UnityEngine.Object.Destroy(base.gameObject);
						}
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x00115E3C File Offset: 0x0011403C
		// (set) Token: 0x060042FF RID: 17151 RVA: 0x00115E50 File Offset: 0x00114050
		public HookProjectileImpact.HookState NetworkhookState
		{
			get
			{
				return this.hookState;
			}
			[param: In]
			set
			{
				ulong newValueAsUlong = (ulong)((long)value);
				ulong fieldValueAsUlong = (ulong)((long)this.hookState);
				base.SetSyncVarEnum<HookProjectileImpact.HookState>(value, newValueAsUlong, ref this.hookState, fieldValueAsUlong, 1U);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x00115E80 File Offset: 0x00114080
		// (set) Token: 0x06004301 RID: 17153 RVA: 0x00115E93 File Offset: 0x00114093
		public GameObject Networkvictim
		{
			get
			{
				return this.victim;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.victim, 2U, ref this.___victimNetId);
			}
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x00115EB0 File Offset: 0x001140B0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write((int)this.hookState);
				writer.Write(this.victim);
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
				writer.Write((int)this.hookState);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.victim);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x00115F5C File Offset: 0x0011415C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.hookState = (HookProjectileImpact.HookState)reader.ReadInt32();
				this.___victimNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.hookState = (HookProjectileImpact.HookState)reader.ReadInt32();
			}
			if ((num & 2) != 0)
			{
				this.victim = reader.ReadGameObject();
			}
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x00115FC2 File Offset: 0x001141C2
		public override void PreStartClient()
		{
			if (!this.___victimNetId.IsEmpty())
			{
				this.Networkvictim = ClientScene.FindLocalObject(this.___victimNetId);
			}
		}

		// Token: 0x040040EF RID: 16623
		private ProjectileController projectileController;

		// Token: 0x040040F0 RID: 16624
		public float reelDelayTime;

		// Token: 0x040040F1 RID: 16625
		public float reelSpeed = 40f;

		// Token: 0x040040F2 RID: 16626
		public string attachmentString;

		// Token: 0x040040F3 RID: 16627
		public float victimPullFactor = 1f;

		// Token: 0x040040F4 RID: 16628
		public float pullMinimumDistance = 10f;

		// Token: 0x040040F5 RID: 16629
		public GameObject impactSpark;

		// Token: 0x040040F6 RID: 16630
		public GameObject impactSuccess;

		// Token: 0x040040F7 RID: 16631
		[SyncVar]
		private HookProjectileImpact.HookState hookState;

		// Token: 0x040040F8 RID: 16632
		[SyncVar]
		private GameObject victim;

		// Token: 0x040040F9 RID: 16633
		private Transform ownerTransform;

		// Token: 0x040040FA RID: 16634
		private ProjectileDamage projectileDamage;

		// Token: 0x040040FB RID: 16635
		private Rigidbody rigidbody;

		// Token: 0x040040FC RID: 16636
		public float liveTimer;

		// Token: 0x040040FD RID: 16637
		private float delayTimer;

		// Token: 0x040040FE RID: 16638
		private float flyTimer;

		// Token: 0x040040FF RID: 16639
		private NetworkInstanceId ___victimNetId;

		// Token: 0x02000B7C RID: 2940
		private enum HookState
		{
			// Token: 0x04004101 RID: 16641
			Flying,
			// Token: 0x04004102 RID: 16642
			HitDelay,
			// Token: 0x04004103 RID: 16643
			Reel,
			// Token: 0x04004104 RID: 16644
			ReelFail
		}
	}
}
