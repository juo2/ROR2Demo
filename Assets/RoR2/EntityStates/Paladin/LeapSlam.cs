using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Paladin
{
	// Token: 0x0200022D RID: 557
	public class LeapSlam : BaseState
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00028B48 File Offset: 0x00026D48
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

		// Token: 0x060009D3 RID: 2515 RVA: 0x00028BA8 File Offset: 0x00026DA8
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

		// Token: 0x060009D4 RID: 2516 RVA: 0x00028BE8 File Offset: 0x00026DE8
		public override void OnEnter()
		{
			base.OnEnter();
			this.leapVelocity = base.characterBody.moveSpeed * LeapSlam.leapVelocityCoefficient;
			this.modelTransform = base.GetModelTransform();
			Util.PlaySound(LeapSlam.initialAttackSoundString, base.gameObject);
			this.initialAimVector = base.GetAimRay().direction;
			this.initialAimVector.y = Mathf.Max(this.initialAimVector.y, 0f);
			this.initialAimVector.y = this.initialAimVector.y + LeapSlam.yBias;
			this.initialAimVector = this.initialAimVector.normalized;
			base.characterMotor.velocity.y = this.leapVelocity * this.initialAimVector.y * LeapSlam.verticalLeapBonusCoefficient;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = LeapSlam.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = LeapSlam.hitEffectPrefab;
			this.attack.damageType = DamageType.Stun1s;
			this.attack.forceVector = Vector3.up * LeapSlam.forceMagnitude;
			if (this.modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "GroundSlam");
			}
			if (this.modelTransform)
			{
				this.modelChildLocator = this.modelTransform.GetComponent<ChildLocator>();
				if (this.modelChildLocator)
				{
					GameObject original = LeapSlam.chargeEffectPrefab;
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

		// Token: 0x060009D5 RID: 2517 RVA: 0x00028E20 File Offset: 0x00027020
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				this.attack.Fire(null);
			}
			if (base.isAuthority && this.modelTransform)
			{
				EffectManager.SimpleMuzzleFlash(LeapSlam.slamEffectPrefab, base.gameObject, "SlamZone", true);
			}
			EntityState.Destroy(this.leftHandChargeEffect);
			EntityState.Destroy(this.rightHandChargeEffect);
			this.DisableIndicator("GroundSlamIndicator", this.modelChildLocator);
			base.OnExit();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00028E9C File Offset: 0x0002709C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (base.characterMotor)
			{
				Vector3 velocity = base.characterMotor.velocity;
				Vector3 velocity2 = new Vector3(this.initialAimVector.x * this.leapVelocity, velocity.y, this.initialAimVector.z * this.leapVelocity);
				base.characterMotor.velocity = velocity2;
				base.characterMotor.moveDirection = this.initialAimVector;
			}
			if (base.characterMotor.isGrounded && this.stopwatch > LeapSlam.minimumDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000B69 RID: 2921
		private float stopwatch;

		// Token: 0x04000B6A RID: 2922
		public static float damageCoefficient = 4f;

		// Token: 0x04000B6B RID: 2923
		public static float forceMagnitude = 16f;

		// Token: 0x04000B6C RID: 2924
		public static float yBias;

		// Token: 0x04000B6D RID: 2925
		public static string initialAttackSoundString;

		// Token: 0x04000B6E RID: 2926
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000B6F RID: 2927
		public static GameObject slamEffectPrefab;

		// Token: 0x04000B70 RID: 2928
		public static GameObject hitEffectPrefab;

		// Token: 0x04000B71 RID: 2929
		public static float leapVelocityCoefficient;

		// Token: 0x04000B72 RID: 2930
		public static float verticalLeapBonusCoefficient;

		// Token: 0x04000B73 RID: 2931
		public static float minimumDuration;

		// Token: 0x04000B74 RID: 2932
		private float leapVelocity;

		// Token: 0x04000B75 RID: 2933
		private OverlapAttack attack;

		// Token: 0x04000B76 RID: 2934
		private Transform modelTransform;

		// Token: 0x04000B77 RID: 2935
		private GameObject leftHandChargeEffect;

		// Token: 0x04000B78 RID: 2936
		private GameObject rightHandChargeEffect;

		// Token: 0x04000B79 RID: 2937
		private ChildLocator modelChildLocator;

		// Token: 0x04000B7A RID: 2938
		private Vector3 initialAimVector;
	}
}
