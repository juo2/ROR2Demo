using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006D9 RID: 1753
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(VehicleSeat))]
	public class FireballVehicle : MonoBehaviour, ICameraStateProvider
	{
		// Token: 0x06002291 RID: 8849 RVA: 0x000952A4 File Offset: 0x000934A4
		private void Awake()
		{
			this.vehicleSeat = base.GetComponent<VehicleSeat>();
			this.vehicleSeat.onPassengerEnter += this.OnPassengerEnter;
			this.vehicleSeat.onPassengerExit += this.OnPassengerExit;
			this.rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000952F8 File Offset: 0x000934F8
		private void OnPassengerExit(GameObject passenger)
		{
			if (NetworkServer.active)
			{
				this.DetonateServer();
			}
			foreach (CameraRigController cameraRigController in CameraRigController.readOnlyInstancesList)
			{
				if (cameraRigController.target == passenger)
				{
					cameraRigController.SetOverrideCam(this, 0f);
					cameraRigController.SetOverrideCam(null, this.cameraLerpTime);
				}
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00095374 File Offset: 0x00093574
		private void OnPassengerEnter(GameObject passenger)
		{
			if (!this.vehicleSeat.currentPassengerInputBank)
			{
				return;
			}
			Vector3 aimDirection = this.vehicleSeat.currentPassengerInputBank.aimDirection;
			this.rigidbody.rotation = Quaternion.LookRotation(aimDirection);
			this.rigidbody.velocity = aimDirection * this.initialSpeed;
			CharacterBody currentPassengerBody = this.vehicleSeat.currentPassengerBody;
			this.overlapAttack = new OverlapAttack
			{
				attacker = currentPassengerBody.gameObject,
				damage = this.overlapDamageCoefficient * currentPassengerBody.damage,
				pushAwayForce = this.overlapForce,
				isCrit = currentPassengerBody.RollCrit(),
				damageColorIndex = DamageColorIndex.Item,
				inflictor = base.gameObject,
				procChainMask = default(ProcChainMask),
				procCoefficient = this.overlapProcCoefficient,
				teamIndex = currentPassengerBody.teamComponent.teamIndex,
				hitBoxGroup = base.gameObject.GetComponent<HitBoxGroup>(),
				hitEffectPrefab = this.overlapHitEffectPrefab
			};
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00095474 File Offset: 0x00093674
		private void DetonateServer()
		{
			if (this.hasDetonatedServer)
			{
				return;
			}
			this.hasDetonatedServer = true;
			CharacterBody currentPassengerBody = this.vehicleSeat.currentPassengerBody;
			if (currentPassengerBody)
			{
				EffectData effectData = new EffectData
				{
					origin = base.transform.position,
					scale = this.blastRadius
				};
				EffectManager.SpawnEffect(this.explosionEffectPrefab, effectData, true);
				new BlastAttack
				{
					attacker = currentPassengerBody.gameObject,
					baseDamage = this.blastDamageCoefficient * currentPassengerBody.damage,
					baseForce = this.blastForce,
					bonusForce = this.blastBonusForce,
					attackerFiltering = AttackerFiltering.NeverHitSelf,
					crit = currentPassengerBody.RollCrit(),
					damageColorIndex = DamageColorIndex.Item,
					damageType = this.blastDamageType,
					falloffModel = this.blastFalloffModel,
					inflictor = base.gameObject,
					position = base.transform.position,
					procChainMask = default(ProcChainMask),
					procCoefficient = this.blastProcCoefficient,
					radius = this.blastRadius,
					teamIndex = currentPassengerBody.teamComponent.teamIndex
				}.Fire();
			}
			Util.PlaySound(this.explosionSoundString, base.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000955BC File Offset: 0x000937BC
		private void FixedUpdate()
		{
			if (!this.vehicleSeat)
			{
				return;
			}
			if (!this.vehicleSeat.currentPassengerInputBank)
			{
				return;
			}
			this.age += Time.fixedDeltaTime;
			this.overlapFireAge += Time.fixedDeltaTime;
			this.overlapResetAge += Time.fixedDeltaTime;
			if (NetworkServer.active)
			{
				if (this.overlapFireAge > 1f / this.overlapFireFrequency)
				{
					if (this.overlapAttack.Fire(null))
					{
						this.age = Mathf.Max(0f, this.age - this.overlapVehicleDurationBonusPerHit);
					}
					this.overlapFireAge = 0f;
				}
				if (this.overlapResetAge >= 1f / this.overlapResetFrequency)
				{
					this.overlapAttack.ResetIgnoredHealthComponents();
					this.overlapResetAge = 0f;
				}
			}
			Ray originalAimRay = this.vehicleSeat.currentPassengerInputBank.GetAimRay();
			float num;
			originalAimRay = CameraRigController.ModifyAimRayIfApplicable(originalAimRay, base.gameObject, out num);
			Vector3 velocity = this.rigidbody.velocity;
			Vector3 target = originalAimRay.direction * this.targetSpeed;
			Vector3 a = Vector3.MoveTowards(velocity, target, this.acceleration * Time.fixedDeltaTime);
			this.rigidbody.MoveRotation(Quaternion.LookRotation(originalAimRay.direction));
			this.rigidbody.AddForce(a - velocity, ForceMode.VelocityChange);
			if (NetworkServer.active && this.duration <= this.age)
			{
				this.DetonateServer();
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00095735 File Offset: 0x00093935
		private void OnCollisionEnter(Collision collision)
		{
			if (this.detonateOnCollision && NetworkServer.active)
			{
				this.DetonateServer();
			}
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000026ED File Offset: 0x000008ED
		public void GetCameraState(CameraRigController cameraRigController, ref CameraState cameraState)
		{
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool IsUserLookAllowed(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool IsUserControlAllowed(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool IsHudAllowed(CameraRigController cameraRigController)
		{
			return true;
		}

		// Token: 0x04002796 RID: 10134
		[Header("Vehicle Parameters")]
		public float duration = 3f;

		// Token: 0x04002797 RID: 10135
		public float initialSpeed = 120f;

		// Token: 0x04002798 RID: 10136
		public float targetSpeed = 40f;

		// Token: 0x04002799 RID: 10137
		public float acceleration = 20f;

		// Token: 0x0400279A RID: 10138
		public float cameraLerpTime = 1f;

		// Token: 0x0400279B RID: 10139
		[Header("Blast Parameters")]
		public bool detonateOnCollision;

		// Token: 0x0400279C RID: 10140
		public GameObject explosionEffectPrefab;

		// Token: 0x0400279D RID: 10141
		public float blastDamageCoefficient;

		// Token: 0x0400279E RID: 10142
		public float blastRadius;

		// Token: 0x0400279F RID: 10143
		public float blastForce;

		// Token: 0x040027A0 RID: 10144
		public BlastAttack.FalloffModel blastFalloffModel;

		// Token: 0x040027A1 RID: 10145
		public DamageType blastDamageType;

		// Token: 0x040027A2 RID: 10146
		public Vector3 blastBonusForce;

		// Token: 0x040027A3 RID: 10147
		public float blastProcCoefficient;

		// Token: 0x040027A4 RID: 10148
		public string explosionSoundString;

		// Token: 0x040027A5 RID: 10149
		[Header("Overlap Parameters")]
		public float overlapDamageCoefficient;

		// Token: 0x040027A6 RID: 10150
		public float overlapProcCoefficient;

		// Token: 0x040027A7 RID: 10151
		public float overlapForce;

		// Token: 0x040027A8 RID: 10152
		public float overlapFireFrequency;

		// Token: 0x040027A9 RID: 10153
		public float overlapResetFrequency;

		// Token: 0x040027AA RID: 10154
		public float overlapVehicleDurationBonusPerHit;

		// Token: 0x040027AB RID: 10155
		public GameObject overlapHitEffectPrefab;

		// Token: 0x040027AC RID: 10156
		private float age;

		// Token: 0x040027AD RID: 10157
		private bool hasDetonatedServer;

		// Token: 0x040027AE RID: 10158
		private VehicleSeat vehicleSeat;

		// Token: 0x040027AF RID: 10159
		private Rigidbody rigidbody;

		// Token: 0x040027B0 RID: 10160
		private OverlapAttack overlapAttack;

		// Token: 0x040027B1 RID: 10161
		private float overlapFireAge;

		// Token: 0x040027B2 RID: 10162
		private float overlapResetAge;
	}
}
