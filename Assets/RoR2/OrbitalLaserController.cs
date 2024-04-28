using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007ED RID: 2029
	public class OrbitalLaserController : MonoBehaviour
	{
		// Token: 0x06002BCC RID: 11212 RVA: 0x000BB7A6 File Offset: 0x000B99A6
		private void Start()
		{
			this.chargeEffect.SetActive(true);
			this.chargeEffect.GetComponent<ObjectScaleCurve>().timeMax = this.chargeDuration;
			this.mostRecentPointerPosition = base.transform.position;
			this.mostRecentPointerNormal = Vector3.up;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000BB7E8 File Offset: 0x000B99E8
		private void UpdateLaserPointer()
		{
			if (this.ownerBody)
			{
				this.ownerInputBank = this.ownerBody.GetComponent<InputBankTest>();
				Ray ray = new Ray
				{
					origin = this.ownerInputBank.aimOrigin,
					direction = this.ownerInputBank.aimDirection
				};
				this.mostRecentMuzzlePosition = ray.origin;
				float num = 900f;
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, num, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
				{
					this.mostRecentPointerPosition = raycastHit.point;
				}
				else
				{
					this.mostRecentPointerPosition = ray.GetPoint(num);
				}
				this.mostRecentPointerNormal = -ray.direction;
			}
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000BB8B9 File Offset: 0x000B9AB9
		private void Update()
		{
			this.UpdateLaserPointer();
			this.laserPointerEffectTransform.SetPositionAndRotation(this.mostRecentPointerPosition, Quaternion.LookRotation(this.mostRecentPointerNormal));
			this.muzzleEffectTransform.SetPositionAndRotation(this.mostRecentMuzzlePosition, Quaternion.identity);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000BB8F4 File Offset: 0x000B9AF4
		private void FixedUpdate()
		{
			this.UpdateLaserPointer();
			if (NetworkServer.active)
			{
				base.transform.position = Vector3.SmoothDamp(base.transform.position, this.mostRecentPointerPosition, ref this.velocity, this.smoothDampTime, this.maxSpeed, Time.fixedDeltaTime);
			}
		}

		// Token: 0x04002E39 RID: 11833
		[NonSerialized]
		public CharacterBody ownerBody;

		// Token: 0x04002E3A RID: 11834
		private InputBankTest ownerInputBank;

		// Token: 0x04002E3B RID: 11835
		[Header("Movement Parameters")]
		public float smoothDampTime = 0.3f;

		// Token: 0x04002E3C RID: 11836
		private Vector3 velocity;

		// Token: 0x04002E3D RID: 11837
		private float maxSpeed;

		// Token: 0x04002E3E RID: 11838
		[Header("Attack Parameters")]
		public float fireFrequency = 5f;

		// Token: 0x04002E3F RID: 11839
		public float damageCoefficientInitial = 6f;

		// Token: 0x04002E40 RID: 11840
		public float damageCoefficientFinal = 6f;

		// Token: 0x04002E41 RID: 11841
		public float procCoefficient = 0.5f;

		// Token: 0x04002E42 RID: 11842
		public float force;

		// Token: 0x04002E43 RID: 11843
		[Header("Charge")]
		public GameObject chargeEffect;

		// Token: 0x04002E44 RID: 11844
		public float chargeDuration = 3f;

		// Token: 0x04002E45 RID: 11845
		public float chargeMaxVelocity = 20f;

		// Token: 0x04002E46 RID: 11846
		private Transform chargeEffectTransform;

		// Token: 0x04002E47 RID: 11847
		[Header("Fire")]
		public GameObject fireEffect;

		// Token: 0x04002E48 RID: 11848
		public float fireDuration = 6f;

		// Token: 0x04002E49 RID: 11849
		public float fireMaxVelocity = 1f;

		// Token: 0x04002E4A RID: 11850
		public GameObject tracerEffectPrefab;

		// Token: 0x04002E4B RID: 11851
		public GameObject hitEffectPrefab;

		// Token: 0x04002E4C RID: 11852
		[Header("Decay")]
		public float decayDuration = 1.5f;

		// Token: 0x04002E4D RID: 11853
		[Tooltip("The transform of the child laser pointer effect.")]
		[Header("Laser Pointer")]
		public Transform laserPointerEffectTransform;

		// Token: 0x04002E4E RID: 11854
		[Tooltip("The transform of the muzzle effect.")]
		public Transform muzzleEffectTransform;

		// Token: 0x04002E4F RID: 11855
		private Vector3 mostRecentPointerPosition;

		// Token: 0x04002E50 RID: 11856
		private Vector3 mostRecentPointerNormal;

		// Token: 0x04002E51 RID: 11857
		private Vector3 mostRecentMuzzlePosition;

		// Token: 0x020007EE RID: 2030
		private abstract class OrbitalLaserBaseState : BaseState
		{
			// Token: 0x06002BD1 RID: 11217 RVA: 0x000BB9C9 File Offset: 0x000B9BC9
			public override void OnEnter()
			{
				base.OnEnter();
				this.controller = base.GetComponent<OrbitalLaserController>();
			}

			// Token: 0x04002E52 RID: 11858
			protected OrbitalLaserController controller;
		}

		// Token: 0x020007EF RID: 2031
		private class OrbitalLaserChargeState : OrbitalLaserController.OrbitalLaserBaseState
		{
			// Token: 0x06002BD3 RID: 11219 RVA: 0x000BB9DD File Offset: 0x000B9BDD
			public override void OnEnter()
			{
				base.OnEnter();
				this.controller.chargeEffect.SetActive(true);
				this.controller.maxSpeed = this.controller.chargeMaxVelocity;
			}

			// Token: 0x06002BD4 RID: 11220 RVA: 0x000BBA0C File Offset: 0x000B9C0C
			public override void OnExit()
			{
				this.controller.chargeEffect.SetActive(false);
				base.OnExit();
			}

			// Token: 0x06002BD5 RID: 11221 RVA: 0x000BBA25 File Offset: 0x000B9C25
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= this.controller.chargeDuration)
				{
					this.outer.SetNextState(new OrbitalLaserController.OrbitalLaserFireState());
					return;
				}
			}
		}

		// Token: 0x020007F0 RID: 2032
		private class OrbitalLaserFireState : OrbitalLaserController.OrbitalLaserBaseState
		{
			// Token: 0x06002BD7 RID: 11223 RVA: 0x000BBA60 File Offset: 0x000B9C60
			public override void OnEnter()
			{
				base.OnEnter();
				this.controller.fireEffect.SetActive(true);
				this.controller.maxSpeed = this.controller.fireMaxVelocity;
			}

			// Token: 0x06002BD8 RID: 11224 RVA: 0x000BBA8F File Offset: 0x000B9C8F
			public override void OnExit()
			{
				this.controller.fireEffect.SetActive(false);
				base.OnExit();
			}

			// Token: 0x06002BD9 RID: 11225 RVA: 0x000BBAA8 File Offset: 0x000B9CA8
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active)
				{
					if (base.fixedAge >= this.controller.fireDuration || !this.controller.ownerBody)
					{
						this.outer.SetNextState(new OrbitalLaserController.OrbitalLaserDecayState());
						return;
					}
					this.bulletAttackTimer -= Time.fixedDeltaTime;
					if (this.controller.ownerBody && this.bulletAttackTimer < 0f)
					{
						this.bulletAttackTimer += 1f / this.controller.fireFrequency;
						new BulletAttack
						{
							owner = this.controller.ownerBody.gameObject,
							weapon = base.gameObject,
							origin = base.transform.position + Vector3.up * 600f,
							maxDistance = 1200f,
							aimVector = Vector3.down,
							minSpread = 0f,
							maxSpread = 0f,
							damage = Mathf.Lerp(this.controller.damageCoefficientInitial, this.controller.damageCoefficientFinal, base.fixedAge / this.controller.fireDuration) * this.controller.ownerBody.damage / this.controller.fireFrequency,
							force = this.controller.force,
							tracerEffectPrefab = this.controller.tracerEffectPrefab,
							muzzleName = "",
							hitEffectPrefab = this.controller.hitEffectPrefab,
							isCrit = Util.CheckRoll(this.controller.ownerBody.crit, this.controller.ownerBody.master),
							stopperMask = LayerIndex.world.mask,
							damageColorIndex = DamageColorIndex.Item,
							procCoefficient = this.controller.procCoefficient / this.controller.fireFrequency,
							radius = 2f
						}.Fire();
					}
				}
			}

			// Token: 0x04002E53 RID: 11859
			private float bulletAttackTimer;
		}

		// Token: 0x020007F1 RID: 2033
		private class OrbitalLaserDecayState : OrbitalLaserController.OrbitalLaserBaseState
		{
			// Token: 0x06002BDB RID: 11227 RVA: 0x000BBCC8 File Offset: 0x000B9EC8
			public override void OnEnter()
			{
				base.OnEnter();
				this.controller.maxSpeed = 0f;
			}

			// Token: 0x06002BDC RID: 11228 RVA: 0x000BBCE0 File Offset: 0x000B9EE0
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= this.controller.decayDuration)
				{
					EntityState.Destroy(base.gameObject);
				}
			}
		}
	}
}
