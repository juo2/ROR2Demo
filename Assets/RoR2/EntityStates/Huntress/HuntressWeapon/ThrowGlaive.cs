using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000325 RID: 805
	public class ThrowGlaive : BaseState
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x0003E434 File Offset: 0x0003C634
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = ThrowGlaive.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			this.animator = base.GetModelAnimator();
			this.huntressTracker = base.GetComponent<HuntressTracker>();
			Util.PlayAttackSpeedSound(ThrowGlaive.attackSoundString, base.gameObject, this.attackSpeedStat);
			if (this.huntressTracker && base.isAuthority)
			{
				this.initialOrbTarget = this.huntressTracker.GetTrackingTarget();
			}
			if (base.characterMotor && ThrowGlaive.smallHopStrength != 0f)
			{
				base.characterMotor.velocity.y = ThrowGlaive.smallHopStrength;
			}
			base.PlayAnimation("FullBody, Override", "ThrowGlaive", "ThrowGlaive.playbackRate", this.duration);
			if (this.modelTransform)
			{
				this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					Transform transform = this.childLocator.FindChild("HandR");
					if (transform && ThrowGlaive.chargePrefab)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ThrowGlaive.chargePrefab, transform.position, transform.rotation);
						this.chargeEffect.transform.parent = transform;
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003E5AC File Offset: 0x0003C7AC
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 1.5f);
				this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
			if (!this.hasTriedToThrowGlaive)
			{
				this.FireOrbGlaive();
			}
			if (!this.hasSuccessfullyThrownGlaive && NetworkServer.active)
			{
				base.skillLocator.secondary.AddOneStock();
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E640 File Offset: 0x0003C840
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (!this.hasTriedToThrowGlaive && this.animator.GetFloat("ThrowGlaive.fire") > 0f)
			{
				if (this.chargeEffect)
				{
					EntityState.Destroy(this.chargeEffect);
				}
				this.FireOrbGlaive();
			}
			CharacterMotor characterMotor = base.characterMotor;
			characterMotor.velocity.y = characterMotor.velocity.y + ThrowGlaive.antigravityStrength * Time.fixedDeltaTime * (1f - this.stopwatch / this.duration);
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		private void FireOrbGlaive()
		{
			if (!NetworkServer.active || this.hasTriedToThrowGlaive)
			{
				return;
			}
			this.hasTriedToThrowGlaive = true;
			LightningOrb lightningOrb = new LightningOrb();
			lightningOrb.lightningType = LightningOrb.LightningType.HuntressGlaive;
			lightningOrb.damageValue = base.characterBody.damage * ThrowGlaive.damageCoefficient;
			lightningOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
			lightningOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			lightningOrb.attacker = base.gameObject;
			lightningOrb.procCoefficient = ThrowGlaive.glaiveProcCoefficient;
			lightningOrb.bouncesRemaining = ThrowGlaive.maxBounceCount;
			lightningOrb.speed = ThrowGlaive.glaiveTravelSpeed;
			lightningOrb.bouncedObjects = new List<HealthComponent>();
			lightningOrb.range = ThrowGlaive.glaiveBounceRange;
			lightningOrb.damageCoefficientPerBounce = ThrowGlaive.damageCoefficientPerBounce;
			HurtBox hurtBox = this.initialOrbTarget;
			if (hurtBox)
			{
				this.hasSuccessfullyThrownGlaive = true;
				Transform transform = this.childLocator.FindChild("HandR");
				EffectManager.SimpleMuzzleFlash(ThrowGlaive.muzzleFlashPrefab, base.gameObject, "HandR", true);
				lightningOrb.origin = transform.position;
				lightningOrb.target = hurtBox;
				OrbManager.instance.AddOrb(lightningOrb);
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003E81B File Offset: 0x0003CA1B
		public override void OnSerialize(NetworkWriter writer)
		{
			writer.Write(HurtBoxReference.FromHurtBox(this.initialOrbTarget));
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003E830 File Offset: 0x0003CA30
		public override void OnDeserialize(NetworkReader reader)
		{
			this.initialOrbTarget = reader.ReadHurtBoxReference().ResolveHurtBox();
		}

		// Token: 0x04001211 RID: 4625
		public static float baseDuration = 3f;

		// Token: 0x04001212 RID: 4626
		public static GameObject chargePrefab;

		// Token: 0x04001213 RID: 4627
		public static GameObject muzzleFlashPrefab;

		// Token: 0x04001214 RID: 4628
		public static float smallHopStrength;

		// Token: 0x04001215 RID: 4629
		public static float antigravityStrength;

		// Token: 0x04001216 RID: 4630
		public static float damageCoefficient = 1.2f;

		// Token: 0x04001217 RID: 4631
		public static float damageCoefficientPerBounce = 1.1f;

		// Token: 0x04001218 RID: 4632
		public static float glaiveProcCoefficient;

		// Token: 0x04001219 RID: 4633
		public static int maxBounceCount;

		// Token: 0x0400121A RID: 4634
		public static float glaiveTravelSpeed;

		// Token: 0x0400121B RID: 4635
		public static float glaiveBounceRange;

		// Token: 0x0400121C RID: 4636
		public static string attackSoundString;

		// Token: 0x0400121D RID: 4637
		private float duration;

		// Token: 0x0400121E RID: 4638
		private float stopwatch;

		// Token: 0x0400121F RID: 4639
		private Animator animator;

		// Token: 0x04001220 RID: 4640
		private GameObject chargeEffect;

		// Token: 0x04001221 RID: 4641
		private Transform modelTransform;

		// Token: 0x04001222 RID: 4642
		private HuntressTracker huntressTracker;

		// Token: 0x04001223 RID: 4643
		private ChildLocator childLocator;

		// Token: 0x04001224 RID: 4644
		private bool hasTriedToThrowGlaive;

		// Token: 0x04001225 RID: 4645
		private bool hasSuccessfullyThrownGlaive;

		// Token: 0x04001226 RID: 4646
		private HurtBox initialOrbTarget;
	}
}
