using System;
using EntityStates.Sniper.Scope;
using RoR2;
using UnityEngine;

namespace EntityStates.Sniper.SniperWeapon
{
	// Token: 0x020001C5 RID: 453
	public class FireRifle : BaseState
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x000224C4 File Offset: 0x000206C4
		public override void OnEnter()
		{
			base.OnEnter();
			float num = 0f;
			if (base.skillLocator)
			{
				GenericSkill secondary = base.skillLocator.secondary;
				if (secondary)
				{
					EntityStateMachine stateMachine = secondary.stateMachine;
					if (stateMachine)
					{
						ScopeSniper scopeSniper = stateMachine.state as ScopeSniper;
						if (scopeSniper != null)
						{
							num = scopeSniper.charge;
							scopeSniper.charge = 0f;
						}
					}
				}
			}
			base.AddRecoil(-1f * FireRifle.recoilAmplitude, -2f * FireRifle.recoilAmplitude, -0.5f * FireRifle.recoilAmplitude, 0.5f * FireRifle.recoilAmplitude);
			this.duration = FireRifle.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			Util.PlaySound(FireRifle.attackSoundString, base.gameObject);
			base.PlayAnimation("Gesture", "FireShotgun", "FireShotgun.playbackRate", this.duration * 1.1f);
			string muzzleName = "MuzzleShotgun";
			if (FireRifle.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireRifle.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = new BulletAttack();
				bulletAttack.owner = base.gameObject;
				bulletAttack.weapon = base.gameObject;
				bulletAttack.origin = aimRay.origin;
				bulletAttack.aimVector = aimRay.direction;
				bulletAttack.minSpread = 0f;
				bulletAttack.maxSpread = base.characterBody.spreadBloomAngle;
				bulletAttack.bulletCount = (uint)((FireRifle.bulletCount > 0) ? FireRifle.bulletCount : 0);
				bulletAttack.procCoefficient = 1f / (float)FireRifle.bulletCount;
				bulletAttack.damage = Mathf.LerpUnclamped(FireRifle.minChargeDamageCoefficient, FireRifle.maxChargeDamageCoefficient, num) * this.damageStat / (float)FireRifle.bulletCount;
				bulletAttack.force = Mathf.LerpUnclamped(FireRifle.minChargeForce, FireRifle.maxChargeForce, num);
				bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
				bulletAttack.tracerEffectPrefab = FireRifle.tracerEffectPrefab;
				bulletAttack.muzzleName = muzzleName;
				bulletAttack.hitEffectPrefab = FireRifle.hitEffectPrefab;
				bulletAttack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
				if (num == 1f)
				{
					bulletAttack.stopperMask = LayerIndex.world.mask;
				}
				bulletAttack.HitEffectNormal = false;
				bulletAttack.radius = 0f;
				bulletAttack.sniper = true;
				bulletAttack.Fire();
			}
			base.characterBody.AddSpreadBloom(FireRifle.spreadBloomValue);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00022744 File Offset: 0x00020944
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.inputBank)
			{
				this.inputReleased |= !base.inputBank.skill1.down;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000227A6 File Offset: 0x000209A6
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (this.inputReleased && base.fixedAge >= FireRifle.interruptInterval / this.attackSpeedStat)
			{
				return InterruptPriority.Any;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04000987 RID: 2439
		public static GameObject effectPrefab;

		// Token: 0x04000988 RID: 2440
		public static GameObject hitEffectPrefab;

		// Token: 0x04000989 RID: 2441
		public static GameObject tracerEffectPrefab;

		// Token: 0x0400098A RID: 2442
		public static float minChargeDamageCoefficient;

		// Token: 0x0400098B RID: 2443
		public static float maxChargeDamageCoefficient;

		// Token: 0x0400098C RID: 2444
		public static float minChargeForce;

		// Token: 0x0400098D RID: 2445
		public static float maxChargeForce;

		// Token: 0x0400098E RID: 2446
		public static int bulletCount;

		// Token: 0x0400098F RID: 2447
		public static float baseDuration = 2f;

		// Token: 0x04000990 RID: 2448
		public static string attackSoundString;

		// Token: 0x04000991 RID: 2449
		public static float recoilAmplitude;

		// Token: 0x04000992 RID: 2450
		public static float spreadBloomValue = 0.3f;

		// Token: 0x04000993 RID: 2451
		public static float interruptInterval = 0.2f;

		// Token: 0x04000994 RID: 2452
		private float duration;

		// Token: 0x04000995 RID: 2453
		private bool inputReleased;
	}
}
