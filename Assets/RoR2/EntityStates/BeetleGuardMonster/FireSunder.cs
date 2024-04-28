using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.BeetleGuardMonster
{
	// Token: 0x0200046C RID: 1132
	public class FireSunder : BaseState
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x0005A57C File Offset: 0x0005877C
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlaySound(FireSunder.initialAttackSoundString, base.gameObject);
			this.duration = FireSunder.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "FireSunder", "FireSunder.playbackRate", this.duration, 0.2f);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration + 2f);
			}
			if (this.modelTransform)
			{
				AimAnimator component = this.modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
				this.modelChildLocator = this.modelTransform.GetComponent<ChildLocator>();
				if (this.modelChildLocator)
				{
					GameObject original = FireSunder.chargeEffectPrefab;
					this.handRTransform = this.modelChildLocator.FindChild("HandR");
					if (this.handRTransform)
					{
						this.rightHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, this.handRTransform);
					}
				}
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0005A690 File Offset: 0x00058890
		public override void OnExit()
		{
			EntityState.Destroy(this.rightHandChargeEffect);
			if (this.modelTransform)
			{
				AimAnimator component = this.modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
			base.OnExit();
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0005A6D8 File Offset: 0x000588D8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("FireSunder.activate") > 0.5f && !this.hasAttacked)
			{
				if (base.isAuthority && this.modelTransform)
				{
					Ray aimRay = base.GetAimRay();
					aimRay.origin = this.handRTransform.position;
					ProjectileManager.instance.FireProjectile(FireSunder.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireSunder.damageCoefficient, FireSunder.forceMagnitude, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
				this.hasAttacked = true;
				EntityState.Destroy(this.rightHandChargeEffect);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A03 RID: 6659
		public static float baseDuration = 3.5f;

		// Token: 0x04001A04 RID: 6660
		public static float damageCoefficient = 4f;

		// Token: 0x04001A05 RID: 6661
		public static float forceMagnitude = 16f;

		// Token: 0x04001A06 RID: 6662
		public static string initialAttackSoundString;

		// Token: 0x04001A07 RID: 6663
		public static GameObject chargeEffectPrefab;

		// Token: 0x04001A08 RID: 6664
		public static GameObject projectilePrefab;

		// Token: 0x04001A09 RID: 6665
		public static GameObject hitEffectPrefab;

		// Token: 0x04001A0A RID: 6666
		private Animator modelAnimator;

		// Token: 0x04001A0B RID: 6667
		private Transform modelTransform;

		// Token: 0x04001A0C RID: 6668
		private bool hasAttacked;

		// Token: 0x04001A0D RID: 6669
		private float duration;

		// Token: 0x04001A0E RID: 6670
		private GameObject rightHandChargeEffect;

		// Token: 0x04001A0F RID: 6671
		private ChildLocator modelChildLocator;

		// Token: 0x04001A10 RID: 6672
		private Transform handRTransform;
	}
}
