using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.BrotherMonster.Weapon
{
	// Token: 0x02000451 RID: 1105
	public class FireLunarShards : BaseSkillState
	{
		// Token: 0x060013C3 RID: 5059 RVA: 0x00057EBC File Offset: 0x000560BC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLunarShards.baseDuration / this.attackSpeedStat;
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				Transform transform = base.FindModelChild(FireLunarShards.muzzleString);
				if (transform)
				{
					aimRay.origin = transform.position;
				}
				aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, this.maxSpread, this.spreadYawScale, this.spreadPitchScale, 0f, 0f);
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.position = aimRay.origin;
				fireProjectileInfo.rotation = Quaternion.LookRotation(aimRay.direction);
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				fireProjectileInfo.damage = base.characterBody.damage * this.damageCoefficient;
				fireProjectileInfo.damageColorIndex = DamageColorIndex.Default;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.procChainMask = default(ProcChainMask);
				fireProjectileInfo.force = 0f;
				fireProjectileInfo.useFuseOverride = false;
				fireProjectileInfo.useSpeedOverride = false;
				fireProjectileInfo.target = null;
				fireProjectileInfo.projectilePrefab = FireLunarShards.projectilePrefab;
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			this.PlayAnimation("Gesture, Additive", "FireLunarShards");
			this.PlayAnimation("Gesture, Override", "FireLunarShards");
			base.AddRecoil(-0.4f * FireLunarShards.recoilAmplitude, -0.8f * FireLunarShards.recoilAmplitude, -0.3f * FireLunarShards.recoilAmplitude, 0.3f * FireLunarShards.recoilAmplitude);
			base.characterBody.AddSpreadBloom(FireLunarShards.spreadBloomValue);
			EffectManager.SimpleMuzzleFlash(FireLunarShards.muzzleFlashEffectPrefab, base.gameObject, FireLunarShards.muzzleString, false);
			Util.PlaySound(FireLunarShards.fireSound, base.gameObject);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00058082 File Offset: 0x00056282
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400193C RID: 6460
		public static float baseDuration;

		// Token: 0x0400193D RID: 6461
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x0400193E RID: 6462
		public static GameObject projectilePrefab;

		// Token: 0x0400193F RID: 6463
		public static float recoilAmplitude;

		// Token: 0x04001940 RID: 6464
		public static float spreadBloomValue;

		// Token: 0x04001941 RID: 6465
		public static string muzzleString;

		// Token: 0x04001942 RID: 6466
		public static GameObject muzzleFlashEffectPrefab;

		// Token: 0x04001943 RID: 6467
		public static string fireSound;

		// Token: 0x04001944 RID: 6468
		[SerializeField]
		public float maxSpread;

		// Token: 0x04001945 RID: 6469
		[SerializeField]
		public float spreadYawScale;

		// Token: 0x04001946 RID: 6470
		[SerializeField]
		public float spreadPitchScale;

		// Token: 0x04001947 RID: 6471
		private float duration;
	}
}
