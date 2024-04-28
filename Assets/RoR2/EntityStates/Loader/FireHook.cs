using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002C4 RID: 708
	public class FireHook : BaseSkillState
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00034E34 File Offset: 0x00033034
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					position = aimRay.origin,
					rotation = Quaternion.LookRotation(aimRay.direction),
					crit = base.characterBody.RollCrit(),
					damage = this.damageStat * FireHook.damageCoefficient,
					force = 0f,
					damageColorIndex = DamageColorIndex.Default,
					procChainMask = default(ProcChainMask),
					projectilePrefab = this.projectilePrefab,
					owner = base.gameObject
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			EffectManager.SimpleMuzzleFlash(FireHook.muzzleflashEffectPrefab, base.gameObject, "MuzzleLeft", false);
			Util.PlaySound(FireHook.fireSoundString, base.gameObject);
			this.PlayAnimation("Grapple", "FireHookIntro");
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00034F26 File Offset: 0x00033126
		public void SetHookReference(GameObject hook)
		{
			this.hookInstance = hook;
			this.hookStickOnImpact = hook.GetComponent<ProjectileStickOnImpact>();
			this.hadHookInstance = true;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00034F44 File Offset: 0x00033144
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.hookStickOnImpact)
			{
				if (this.hookStickOnImpact.stuck && !this.isStuck)
				{
					this.PlayAnimation("Grapple", "FireHookLoop");
				}
				this.isStuck = this.hookStickOnImpact.stuck;
			}
			if (base.isAuthority && !this.hookInstance && this.hadHookInstance)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00034FC2 File Offset: 0x000331C2
		public override void OnExit()
		{
			this.PlayAnimation("Grapple", "FireHookExit");
			EffectManager.SimpleMuzzleFlash(FireHook.muzzleflashEffectPrefab, base.gameObject, "MuzzleLeft", false);
			base.OnExit();
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000F50 RID: 3920
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000F51 RID: 3921
		public static float damageCoefficient;

		// Token: 0x04000F52 RID: 3922
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04000F53 RID: 3923
		public static string fireSoundString;

		// Token: 0x04000F54 RID: 3924
		public GameObject hookInstance;

		// Token: 0x04000F55 RID: 3925
		protected ProjectileStickOnImpact hookStickOnImpact;

		// Token: 0x04000F56 RID: 3926
		private bool isStuck;

		// Token: 0x04000F57 RID: 3927
		private bool hadHookInstance;

		// Token: 0x04000F58 RID: 3928
		private uint soundID;
	}
}
