using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Paladin
{
	// Token: 0x0200022C RID: 556
	public class DashSlam : BaseState
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x000286DC File Offset: 0x000268DC
		private void EnableIndicator(string childLocatorName, ChildLocator childLocator = null)
		{
			if (!childLocator)
			{
				childLocator = base.GetModelTransform().GetComponent<ChildLocator>();
			}
			Transform transform = childLocator.FindChild(childLocatorName);
			if (transform)
			{
				transform.gameObject.SetActive(true);
				ObjectScaleCurve component = transform.gameObject.GetComponent<ObjectScaleCurve>();
				if (component)
				{
					component.time = 0f;
				}
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002873C File Offset: 0x0002693C
		private void DisableIndicator(string childLocatorName, ChildLocator childLocator = null)
		{
			if (!childLocator)
			{
				childLocator = base.GetModelTransform().GetComponent<ChildLocator>();
			}
			Transform transform = childLocator.FindChild(childLocatorName);
			if (transform)
			{
				transform.gameObject.SetActive(false);
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002877C File Offset: 0x0002697C
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelTransform = base.GetModelTransform();
			Util.PlaySound(DashSlam.initialAttackSoundString, base.gameObject);
			this.initialAimVector = Vector3.ProjectOnPlane(base.GetAimRay().direction, Vector3.up);
			base.characterMotor.velocity.y = 0f;
			base.characterDirection.forward = this.initialAimVector;
			this.attack = new BlastAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.baseDamage = DashSlam.damageCoefficient * this.damageStat;
			this.attack.damageType = DamageType.Stun1s;
			this.attack.baseForce = DashSlam.baseForceMagnitude;
			this.attack.radius = DashSlam.blastAttackRadius + base.characterBody.radius;
			this.attack.falloffModel = BlastAttack.FalloffModel.None;
			this.attack.attackerFiltering = AttackerFiltering.NeverHitSelf;
			if (this.modelTransform)
			{
				this.modelChildLocator = this.modelTransform.GetComponent<ChildLocator>();
				if (this.modelChildLocator)
				{
					GameObject original = DashSlam.chargeEffectPrefab;
					Transform transform = this.modelChildLocator.FindChild("HandL");
					Transform transform2 = this.modelChildLocator.FindChild("HandR");
					if (transform)
					{
						this.leftHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform);
					}
					if (transform2)
					{
						this.rightHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform2);
					}
					this.EnableIndicator("GroundSlamIndicator", this.modelChildLocator);
				}
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00028934 File Offset: 0x00026B34
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				this.attack.position = base.transform.position;
				this.attack.bonusForce = (this.initialAimVector + Vector3.up * 0.3f) * DashSlam.bonusImpactForce;
				this.attack.Fire();
			}
			if (base.isAuthority && this.modelTransform)
			{
				EffectManager.SimpleMuzzleFlash(DashSlam.slamEffectPrefab, base.gameObject, "SlamZone", true);
			}
			EntityState.Destroy(this.leftHandChargeEffect);
			EntityState.Destroy(this.rightHandChargeEffect);
			this.DisableIndicator("GroundSlamIndicator", this.modelChildLocator);
			base.OnExit();
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000289F4 File Offset: 0x00026BF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (base.isAuthority)
			{
				Collider[] array = Physics.OverlapSphere(base.transform.position, base.characterBody.radius + DashSlam.overlapSphereRadius, LayerIndex.entityPrecise.mask);
				for (int i = 0; i < array.Length; i++)
				{
					HurtBox component = array[i].GetComponent<HurtBox>();
					if (component && component.healthComponent != base.healthComponent)
					{
						this.outer.SetNextStateToMain();
						return;
					}
				}
			}
			if (base.characterMotor)
			{
				float num = Mathf.Lerp(DashSlam.initialSpeedCoefficient, DashSlam.finalSpeedCoefficient, this.stopwatch / DashSlam.duration) * base.characterBody.moveSpeed;
				Vector3 velocity = new Vector3(this.initialAimVector.x * num, 0f, this.initialAimVector.z * num);
				base.characterMotor.velocity = velocity;
				base.characterMotor.moveDirection = this.initialAimVector;
			}
			if (this.stopwatch > DashSlam.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000B56 RID: 2902
		private float stopwatch;

		// Token: 0x04000B57 RID: 2903
		public static float damageCoefficient = 4f;

		// Token: 0x04000B58 RID: 2904
		public static float baseForceMagnitude = 16f;

		// Token: 0x04000B59 RID: 2905
		public static float bonusImpactForce;

		// Token: 0x04000B5A RID: 2906
		public static string initialAttackSoundString;

		// Token: 0x04000B5B RID: 2907
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000B5C RID: 2908
		public static GameObject slamEffectPrefab;

		// Token: 0x04000B5D RID: 2909
		public static GameObject hitEffectPrefab;

		// Token: 0x04000B5E RID: 2910
		public static float initialSpeedCoefficient;

		// Token: 0x04000B5F RID: 2911
		public static float finalSpeedCoefficient;

		// Token: 0x04000B60 RID: 2912
		public static float duration;

		// Token: 0x04000B61 RID: 2913
		public static float overlapSphereRadius;

		// Token: 0x04000B62 RID: 2914
		public static float blastAttackRadius;

		// Token: 0x04000B63 RID: 2915
		private BlastAttack attack;

		// Token: 0x04000B64 RID: 2916
		private Transform modelTransform;

		// Token: 0x04000B65 RID: 2917
		private GameObject leftHandChargeEffect;

		// Token: 0x04000B66 RID: 2918
		private GameObject rightHandChargeEffect;

		// Token: 0x04000B67 RID: 2919
		private ChildLocator modelChildLocator;

		// Token: 0x04000B68 RID: 2920
		private Vector3 initialAimVector;
	}
}
