using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003EB RID: 1003
	public class FireLightsOut : BaseState
	{
		// Token: 0x06001202 RID: 4610 RVA: 0x0004FF48 File Offset: 0x0004E148
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLightsOut.baseDuration / this.attackSpeedStat;
			base.AddRecoil(-3f * FireLightsOut.recoilAmplitude, -4f * FireLightsOut.recoilAmplitude, -0.5f * FireLightsOut.recoilAmplitude, 0.5f * FireLightsOut.recoilAmplitude);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "MuzzlePistol";
			Util.PlaySound(FireLightsOut.attackSoundString, base.gameObject);
			this.PlayAnimation("Gesture, Additive", "FireRevolver");
			this.PlayAnimation("Gesture, Override", "FireRevolver");
			if (FireLightsOut.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireLightsOut.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = new BulletAttack();
				bulletAttack.owner = base.gameObject;
				bulletAttack.weapon = base.gameObject;
				bulletAttack.origin = aimRay.origin;
				bulletAttack.aimVector = aimRay.direction;
				bulletAttack.minSpread = FireLightsOut.minSpread;
				bulletAttack.maxSpread = FireLightsOut.maxSpread;
				bulletAttack.bulletCount = (uint)((FireLightsOut.bulletCount > 0) ? FireLightsOut.bulletCount : 0);
				bulletAttack.damage = FireLightsOut.damageCoefficient * this.damageStat;
				bulletAttack.force = FireLightsOut.force;
				bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
				bulletAttack.tracerEffectPrefab = FireLightsOut.tracerEffectPrefab;
				bulletAttack.muzzleName = muzzleName;
				bulletAttack.hitEffectPrefab = FireLightsOut.hitEffectPrefab;
				bulletAttack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
				bulletAttack.HitEffectNormal = false;
				bulletAttack.radius = 0.5f;
				bulletAttack.damageType |= DamageType.ResetCooldownsOnKill;
				bulletAttack.smartCollision = true;
				bulletAttack.Fire();
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000500FF File Offset: 0x0004E2FF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x040016F2 RID: 5874
		public static GameObject effectPrefab;

		// Token: 0x040016F3 RID: 5875
		public static GameObject hitEffectPrefab;

		// Token: 0x040016F4 RID: 5876
		public static GameObject tracerEffectPrefab;

		// Token: 0x040016F5 RID: 5877
		public static float damageCoefficient;

		// Token: 0x040016F6 RID: 5878
		public static float force;

		// Token: 0x040016F7 RID: 5879
		public static float minSpread;

		// Token: 0x040016F8 RID: 5880
		public static float maxSpread;

		// Token: 0x040016F9 RID: 5881
		public static int bulletCount;

		// Token: 0x040016FA RID: 5882
		public static float baseDuration = 2f;

		// Token: 0x040016FB RID: 5883
		public static string attackSoundString;

		// Token: 0x040016FC RID: 5884
		public static float recoilAmplitude;

		// Token: 0x040016FD RID: 5885
		private ChildLocator childLocator;

		// Token: 0x040016FE RID: 5886
		public int bulletCountCurrent = 1;

		// Token: 0x040016FF RID: 5887
		private float duration;
	}
}
