using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ImpBossMonster
{
	// Token: 0x02000307 RID: 775
	public class FireVoidspikes : BaseState
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0003AB84 File Offset: 0x00038D84
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireVoidspikes.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			base.characterMotor.walkSpeedPenaltyCoefficient = FireVoidspikes.walkSpeedPenaltyCoefficient;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = base.GetTeam();
			this.attack.damage = FireVoidspikes.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = FireVoidspikes.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			this.attack.procCoefficient = FireVoidspikes.procCoefficient;
			this.attack.damageType = DamageType.BleedOnHit;
			Util.PlaySound(FireVoidspikes.enterSoundString, base.gameObject);
			if (base.isAuthority)
			{
				this.chosenAnim = (Util.CheckRoll(50f, 0f, null) ? 0 : 1);
			}
			if (this.modelAnimator)
			{
				string animationStateName = (this.chosenAnim == 1) ? "FireVoidspikesL" : "FireVoidspikesR";
				base.PlayAnimation("Gesture, Additive", animationStateName, "FireVoidspikes.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Override", animationStateName, "FireVoidspikes.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration + 3f);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00010A88 File Offset: 0x0000EC88
		public override void OnExit()
		{
			if (base.characterMotor)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			}
			base.OnExit();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003AD24 File Offset: 0x00038F24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.slashCount <= 0)
			{
				if (this.modelAnimator.GetFloat("HandR.hitBoxActive") > 0.1f)
				{
					this.FireSpikeFan(base.GetAimRay(), "FireVoidspikesR", "HandR");
				}
				if (this.modelAnimator.GetFloat("HandL.hitBoxActive") > 0.1f)
				{
					this.FireSpikeFan(base.GetAimRay(), "FireVoidspikesL", "HandL");
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003ADCC File Offset: 0x00038FCC
		private void FireSpikeFan(Ray aimRay, string muzzleName, string hitBoxGroupName)
		{
			Util.PlaySound(FireVoidspikes.attackSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(FireVoidspikes.swipeEffectPrefab, base.gameObject, muzzleName, false);
			this.slashCount++;
			if (base.isAuthority)
			{
				Vector3 forward = base.characterDirection.forward;
				if (this.modelTransform)
				{
					this.attack.hitBoxGroup = base.FindHitBoxGroup(hitBoxGroupName);
					this.attack.forceVector = forward * FireVoidspikes.forceMagnitude;
					this.attack.Fire(null);
				}
				if (base.characterMotor)
				{
					base.characterMotor.ApplyForce(forward * FireVoidspikes.selfForce, true, false);
				}
				for (int i = 0; i < FireVoidspikes.projectileCount; i++)
				{
					this.FireSpikeAuthority(aimRay, 0f, ((float)FireVoidspikes.projectileCount / 2f - (float)i) * FireVoidspikes.projectileYawSpread, FireVoidspikes.projectileSpeed + FireVoidspikes.projectileSpeedPerProjectile * (float)i);
				}
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003AEC8 File Offset: 0x000390C8
		private void FireSpikeAuthority(Ray aimRay, float bonusPitch, float bonusYaw, float speed)
		{
			Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, bonusPitch);
			ProjectileManager.instance.FireProjectile(FireVoidspikes.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireVoidspikes.projectileDamageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speed);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003AF45 File Offset: 0x00039145
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((char)this.chosenAnim);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003AF5B File Offset: 0x0003915B
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.chosenAnim = (int)reader.ReadChar();
		}

		// Token: 0x0400110C RID: 4364
		public static float baseDuration = 3.5f;

		// Token: 0x0400110D RID: 4365
		public static float damageCoefficient = 4f;

		// Token: 0x0400110E RID: 4366
		public static float procCoefficient;

		// Token: 0x0400110F RID: 4367
		public static float selfForce;

		// Token: 0x04001110 RID: 4368
		public static float forceMagnitude = 16f;

		// Token: 0x04001111 RID: 4369
		public static GameObject hitEffectPrefab;

		// Token: 0x04001112 RID: 4370
		public static GameObject swipeEffectPrefab;

		// Token: 0x04001113 RID: 4371
		public static string enterSoundString;

		// Token: 0x04001114 RID: 4372
		public static string attackSoundString;

		// Token: 0x04001115 RID: 4373
		public static float walkSpeedPenaltyCoefficient;

		// Token: 0x04001116 RID: 4374
		public static int projectileCount;

		// Token: 0x04001117 RID: 4375
		public static float projectileYawSpread;

		// Token: 0x04001118 RID: 4376
		public static float projectileDamageCoefficient;

		// Token: 0x04001119 RID: 4377
		public static float projectileSpeed;

		// Token: 0x0400111A RID: 4378
		public static float projectileSpeedPerProjectile;

		// Token: 0x0400111B RID: 4379
		public static GameObject projectilePrefab;

		// Token: 0x0400111C RID: 4380
		private OverlapAttack attack;

		// Token: 0x0400111D RID: 4381
		private Animator modelAnimator;

		// Token: 0x0400111E RID: 4382
		private float duration;

		// Token: 0x0400111F RID: 4383
		private int slashCount;

		// Token: 0x04001120 RID: 4384
		private Transform modelTransform;

		// Token: 0x04001121 RID: 4385
		private int chosenAnim = -1;
	}
}
