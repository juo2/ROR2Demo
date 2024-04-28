using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000131 RID: 305
	public class ChargeGravityBump : BaseGravityBumpState
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0001708C File Offset: 0x0001528C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.isLeft = (UnityEngine.Random.value > 0.5f);
				CharacterDirection component = base.GetComponent<CharacterDirection>();
				if (component)
				{
					Vector3 a = Vector3.Cross(component.forward, Vector3.up);
					if (!this.isLeft)
					{
						a *= -1f;
					}
					this.airborneForce = Vector3.up * -1f * this.verticalAirborneForce + a * this.horizontalAirborneForce;
					this.groundedForce = Vector3.up * this.verticalGroundedForce + a * this.horizontalGroundedForce;
				}
			}
			this.airborneForceOrientation = Util.QuaternionSafeLookRotation(this.airborneForce);
			this.groundedForceOrientation = Util.QuaternionSafeLookRotation(this.groundedForce);
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator && this.chargeEffectPrefab)
			{
				Transform transform = modelChildLocator.FindChild(this.muzzleName) ?? base.characterBody.coreTransform;
				if (transform)
				{
					this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
					this.chargeEffectInstance.transform.parent = transform;
					ScaleParticleSystemDuration component2 = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component2)
					{
						component2.newDuration = this.duration;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.enterSoundString))
			{
				if (this.isSoundScaledByAttackSpeed)
				{
					Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
				}
				else
				{
					Util.PlaySound(this.enterSoundString, base.gameObject);
				}
			}
			this.characterMotorsToIndicatorTransforms = new Dictionary<CharacterMotor, Transform>();
			this.AssignIndicators();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00017281 File Offset: 0x00015481
		public override void OnExit()
		{
			this.CleanUpIndicators();
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001729C File Offset: 0x0001549C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			foreach (KeyValuePair<CharacterMotor, Transform> keyValuePair in this.characterMotorsToIndicatorTransforms)
			{
				if (keyValuePair.Key.isGrounded)
				{
					keyValuePair.Value.rotation = this.groundedForceOrientation;
				}
				else
				{
					keyValuePair.Value.rotation = this.airborneForceOrientation;
				}
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FireGravityBump());
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00017348 File Offset: 0x00015548
		private void AssignIndicators()
		{
			foreach (HurtBox hurtBox in base.GetTargets())
			{
				GameObject gameObject = hurtBox.healthComponent.gameObject;
				if (gameObject)
				{
					CharacterMotor component = gameObject.GetComponent<CharacterMotor>();
					if (component && !this.characterMotorsToIndicatorTransforms.ContainsKey(component))
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.forceIndicatorPrefab, gameObject.transform);
						this.characterMotorsToIndicatorTransforms.Add(component, gameObject2.transform);
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000173E4 File Offset: 0x000155E4
		private void CleanUpIndicators()
		{
			foreach (KeyValuePair<CharacterMotor, Transform> keyValuePair in this.characterMotorsToIndicatorTransforms)
			{
				Transform value = keyValuePair.Value;
				if (value)
				{
					EntityState.Destroy(value.gameObject);
				}
			}
			this.characterMotorsToIndicatorTransforms.Clear();
		}

		// Token: 0x0400064C RID: 1612
		[SerializeField]
		public float baseDuration;

		// Token: 0x0400064D RID: 1613
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400064E RID: 1614
		[SerializeField]
		public string muzzleName;

		// Token: 0x0400064F RID: 1615
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000650 RID: 1616
		[SerializeField]
		public bool isSoundScaledByAttackSpeed;

		// Token: 0x04000651 RID: 1617
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000652 RID: 1618
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000653 RID: 1619
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000654 RID: 1620
		[SerializeField]
		public float horizontalAirborneForce;

		// Token: 0x04000655 RID: 1621
		[SerializeField]
		public float verticalAirborneForce;

		// Token: 0x04000656 RID: 1622
		[SerializeField]
		public float horizontalGroundedForce;

		// Token: 0x04000657 RID: 1623
		[SerializeField]
		public float verticalGroundedForce;

		// Token: 0x04000658 RID: 1624
		[SerializeField]
		public GameObject forceIndicatorPrefab;

		// Token: 0x04000659 RID: 1625
		private float duration;

		// Token: 0x0400065A RID: 1626
		private GameObject chargeEffectInstance;

		// Token: 0x0400065B RID: 1627
		private Dictionary<CharacterMotor, Transform> characterMotorsToIndicatorTransforms;

		// Token: 0x0400065C RID: 1628
		private Quaternion airborneForceOrientation;

		// Token: 0x0400065D RID: 1629
		private Quaternion groundedForceOrientation;
	}
}
