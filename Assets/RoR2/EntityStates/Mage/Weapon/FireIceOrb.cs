using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002A3 RID: 675
	public class FireIceOrb : BaseState
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x00031704 File Offset: 0x0002F904
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireIceOrb.baseDuration / this.attackSpeedStat;
			FireIceOrb.Gauntlet gauntlet = this.gauntlet;
			if (gauntlet != FireIceOrb.Gauntlet.Left)
			{
				if (gauntlet == FireIceOrb.Gauntlet.Right)
				{
					this.muzzleString = "MuzzleRight";
					base.PlayAnimation("Gesture Right, Additive", "FireGauntletRight", "FireGauntlet.playbackRate", this.duration);
				}
			}
			else
			{
				this.muzzleString = "MuzzleLeft";
				base.PlayAnimation("Gesture Left, Additive", "FireGauntletLeft", "FireGauntlet.playbackRate", this.duration);
			}
			base.PlayAnimation("Gesture, Additive", "HoldGauntletsUp", "FireGauntlet.playbackRate", this.duration);
			Util.PlaySound(FireIceOrb.attackString, base.gameObject);
			this.animator = base.GetModelAnimator();
			base.characterBody.SetAimTimer(2f);
			this.FireGauntlet();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000317D8 File Offset: 0x0002F9D8
		private void FireGauntlet()
		{
			this.hasFiredGauntlet = true;
			Ray aimRay = base.GetAimRay();
			if (FireIceOrb.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireIceOrb.effectPrefab, base.gameObject, this.muzzleString, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireIceOrb.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireIceOrb.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00031874 File Offset: 0x0002FA74
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator.GetFloat("FireGauntlet.fire") > 0f && !this.hasFiredGauntlet)
			{
				this.FireGauntlet();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000E21 RID: 3617
		public static GameObject projectilePrefab;

		// Token: 0x04000E22 RID: 3618
		public static GameObject effectPrefab;

		// Token: 0x04000E23 RID: 3619
		public static float baseDuration = 2f;

		// Token: 0x04000E24 RID: 3620
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000E25 RID: 3621
		public static string attackString;

		// Token: 0x04000E26 RID: 3622
		private float duration;

		// Token: 0x04000E27 RID: 3623
		private bool hasFiredGauntlet;

		// Token: 0x04000E28 RID: 3624
		private string muzzleString;

		// Token: 0x04000E29 RID: 3625
		private Animator animator;

		// Token: 0x04000E2A RID: 3626
		public FireIceOrb.Gauntlet gauntlet;

		// Token: 0x020002A4 RID: 676
		public enum Gauntlet
		{
			// Token: 0x04000E2C RID: 3628
			Left,
			// Token: 0x04000E2D RID: 3629
			Right
		}
	}
}
