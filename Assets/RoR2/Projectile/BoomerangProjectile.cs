using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B73 RID: 2931
	[RequireComponent(typeof(ProjectileController))]
	public class BoomerangProjectile : NetworkBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060042C6 RID: 17094 RVA: 0x001148F8 File Offset: 0x00112AF8
		private void Awake()
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			if (this.projectileController && this.projectileController.owner)
			{
				this.ownerTransform = this.projectileController.owner.transform;
			}
			this.maxFlyStopwatch = this.charge * this.distanceMultiplier;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x00114974 File Offset: 0x00112B74
		private void Start()
		{
			float num = this.charge * 7f;
			if (num < 1f)
			{
				num = 1f;
			}
			Vector3 localScale = new Vector3(num * base.transform.localScale.x, num * base.transform.localScale.y, num * base.transform.localScale.z);
			base.transform.localScale = localScale;
			base.gameObject.GetComponent<ProjectileController>().ghost.transform.localScale = localScale;
			base.GetComponent<ProjectileDotZone>().damageCoefficient *= num;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x00114A14 File Offset: 0x00112C14
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (!this.canHitWorld)
			{
				return;
			}
			this.NetworkboomerangState = BoomerangProjectile.BoomerangState.FlyBack;
			UnityEvent unityEvent = this.onFlyBack;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			EffectManager.SimpleImpactEffect(this.impactSpark, impactInfo.estimatedPointOfImpact, -base.transform.forward, true);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x00114A64 File Offset: 0x00112C64
		private bool Reel()
		{
			Vector3 vector = this.projectileController.owner.transform.position - base.transform.position;
			Vector3 normalized = vector.normalized;
			return vector.magnitude <= 2f;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x00114AB0 File Offset: 0x00112CB0
		public void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				if (!this.setScale)
				{
					this.setScale = true;
				}
				if (!this.projectileController.owner)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				switch (this.boomerangState)
				{
				case BoomerangProjectile.BoomerangState.FlyOut:
					if (NetworkServer.active)
					{
						this.rigidbody.velocity = this.travelSpeed * base.transform.forward;
						this.stopwatch += Time.fixedDeltaTime;
						if (this.stopwatch >= this.maxFlyStopwatch)
						{
							this.stopwatch = 0f;
							this.NetworkboomerangState = BoomerangProjectile.BoomerangState.Transition;
							return;
						}
					}
					break;
				case BoomerangProjectile.BoomerangState.Transition:
				{
					this.stopwatch += Time.fixedDeltaTime;
					float num = this.stopwatch / this.transitionDuration;
					Vector3 a = this.<FixedUpdate>g__CalculatePullDirection|24_0();
					this.rigidbody.velocity = Vector3.Lerp(this.travelSpeed * base.transform.forward, this.travelSpeed * a, num);
					if (num >= 1f)
					{
						this.NetworkboomerangState = BoomerangProjectile.BoomerangState.FlyBack;
						UnityEvent unityEvent = this.onFlyBack;
						if (unityEvent == null)
						{
							return;
						}
						unityEvent.Invoke();
						return;
					}
					break;
				}
				case BoomerangProjectile.BoomerangState.FlyBack:
				{
					bool flag = this.Reel();
					if (NetworkServer.active)
					{
						this.canHitWorld = false;
						Vector3 a2 = this.<FixedUpdate>g__CalculatePullDirection|24_0();
						this.rigidbody.velocity = this.travelSpeed * a2;
						if (flag)
						{
							UnityEngine.Object.Destroy(base.gameObject);
						}
					}
					break;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x00114C4C File Offset: 0x00112E4C
		[CompilerGenerated]
		private Vector3 <FixedUpdate>g__CalculatePullDirection|24_0()
		{
			if (this.projectileController.owner)
			{
				return (this.projectileController.owner.transform.position - base.transform.position).normalized;
			}
			return base.transform.forward;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x00114CA4 File Offset: 0x00112EA4
		// (set) Token: 0x060042CF RID: 17103 RVA: 0x00114CB8 File Offset: 0x00112EB8
		public BoomerangProjectile.BoomerangState NetworkboomerangState
		{
			get
			{
				return this.boomerangState;
			}
			[param: In]
			set
			{
				ulong newValueAsUlong = (ulong)((long)value);
				ulong fieldValueAsUlong = (ulong)((long)this.boomerangState);
				base.SetSyncVarEnum<BoomerangProjectile.BoomerangState>(value, newValueAsUlong, ref this.boomerangState, fieldValueAsUlong, 1U);
			}
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x00114CE8 File Offset: 0x00112EE8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write((int)this.boomerangState);
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
				writer.Write((int)this.boomerangState);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x00114D54 File Offset: 0x00112F54
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.boomerangState = (BoomerangProjectile.BoomerangState)reader.ReadInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.boomerangState = (BoomerangProjectile.BoomerangState)reader.ReadInt32();
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040040AF RID: 16559
		public float travelSpeed = 40f;

		// Token: 0x040040B0 RID: 16560
		public float charge;

		// Token: 0x040040B1 RID: 16561
		public float transitionDuration;

		// Token: 0x040040B2 RID: 16562
		private float maxFlyStopwatch;

		// Token: 0x040040B3 RID: 16563
		public GameObject impactSpark;

		// Token: 0x040040B4 RID: 16564
		public GameObject crosshairPrefab;

		// Token: 0x040040B5 RID: 16565
		public bool canHitCharacters;

		// Token: 0x040040B6 RID: 16566
		public bool canHitWorld;

		// Token: 0x040040B7 RID: 16567
		private ProjectileController projectileController;

		// Token: 0x040040B8 RID: 16568
		[SyncVar]
		private BoomerangProjectile.BoomerangState boomerangState;

		// Token: 0x040040B9 RID: 16569
		private Transform ownerTransform;

		// Token: 0x040040BA RID: 16570
		private ProjectileDamage projectileDamage;

		// Token: 0x040040BB RID: 16571
		private Rigidbody rigidbody;

		// Token: 0x040040BC RID: 16572
		private float stopwatch;

		// Token: 0x040040BD RID: 16573
		private float fireAge;

		// Token: 0x040040BE RID: 16574
		private float fireFrequency;

		// Token: 0x040040BF RID: 16575
		public float distanceMultiplier = 2f;

		// Token: 0x040040C0 RID: 16576
		public UnityEvent onFlyBack;

		// Token: 0x040040C1 RID: 16577
		private bool setScale;

		// Token: 0x02000B74 RID: 2932
		private enum BoomerangState
		{
			// Token: 0x040040C3 RID: 16579
			FlyOut,
			// Token: 0x040040C4 RID: 16580
			Transition,
			// Token: 0x040040C5 RID: 16581
			FlyBack
		}
	}
}
