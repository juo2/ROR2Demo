using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003ED RID: 1005
	public class FireRocket : BaseState
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x000503A8 File Offset: 0x0004E5A8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireRocket.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "MuzzleCenter";
			base.PlayAnimation("Gesture", "FireFMJ", "FireFMJ.playbackRate", this.duration);
			if (FireRocket.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireRocket.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireRocket.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireRocket.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0005047E File Offset: 0x0004E67E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400170D RID: 5901
		public static GameObject projectilePrefab;

		// Token: 0x0400170E RID: 5902
		public static GameObject effectPrefab;

		// Token: 0x0400170F RID: 5903
		public static float damageCoefficient;

		// Token: 0x04001710 RID: 5904
		public static float force;

		// Token: 0x04001711 RID: 5905
		public static float baseDuration = 2f;

		// Token: 0x04001712 RID: 5906
		private float duration;

		// Token: 0x04001713 RID: 5907
		public int bulletCountCurrent = 1;
	}
}
