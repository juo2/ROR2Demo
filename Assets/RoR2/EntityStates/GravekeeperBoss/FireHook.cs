using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GravekeeperBoss
{
	// Token: 0x02000349 RID: 841
	public class FireHook : BaseState
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		public override void OnEnter()
		{
			base.OnEnter();
			base.fixedAge = 0f;
			this.duration = FireHook.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayCrossfade("Body", "FireHook", "FireHook.playbackRate", this.duration, 0.03f);
			}
			ChildLocator component = this.modelAnimator.GetComponent<ChildLocator>();
			if (component)
			{
				component.FindChild(FireHook.muzzleString);
			}
			Util.PlayAttackSpeedSound(FireHook.soundString, base.gameObject, this.attackSpeedStat);
			EffectManager.SimpleMuzzleFlash(FireHook.muzzleflashEffectPrefab, base.gameObject, FireHook.muzzleString, false);
			Ray aimRay = base.GetAimRay();
			if (NetworkServer.active)
			{
				this.FireSingleHook(aimRay, 0f, 0f);
				for (int i = 0; i < FireHook.projectileCount; i++)
				{
					float bonusPitch = UnityEngine.Random.Range(-FireHook.spread, FireHook.spread) / 2f;
					float bonusYaw = UnityEngine.Random.Range(-FireHook.spread, FireHook.spread) / 2f;
					this.FireSingleHook(aimRay, bonusPitch, bonusYaw);
				}
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00041110 File Offset: 0x0003F310
		private void FireSingleHook(Ray aimRay, float bonusPitch, float bonusYaw)
		{
			Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, bonusPitch);
			ProjectileManager.instance.FireProjectile(FireHook.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireHook.projectileDamageCoefficient, FireHook.projectileForce, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00041190 File Offset: 0x0003F390
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040012D6 RID: 4822
		public static float baseDuration = 3f;

		// Token: 0x040012D7 RID: 4823
		public static string soundString;

		// Token: 0x040012D8 RID: 4824
		public static string muzzleString;

		// Token: 0x040012D9 RID: 4825
		public static float projectileDamageCoefficient;

		// Token: 0x040012DA RID: 4826
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x040012DB RID: 4827
		public static GameObject projectilePrefab;

		// Token: 0x040012DC RID: 4828
		public static float spread;

		// Token: 0x040012DD RID: 4829
		public static int projectileCount;

		// Token: 0x040012DE RID: 4830
		public static float projectileForce;

		// Token: 0x040012DF RID: 4831
		private float duration;

		// Token: 0x040012E0 RID: 4832
		private Animator modelAnimator;
	}
}
