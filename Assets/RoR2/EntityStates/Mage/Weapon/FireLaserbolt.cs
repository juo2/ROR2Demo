using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002A5 RID: 677
	public class FireLaserbolt : BaseState
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x000318E4 File Offset: 0x0002FAE4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLaserbolt.baseDuration / this.attackSpeedStat;
			FireLaserbolt.Gauntlet gauntlet = this.gauntlet;
			if (gauntlet != FireLaserbolt.Gauntlet.Left)
			{
				if (gauntlet == FireLaserbolt.Gauntlet.Right)
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
			Util.PlaySound(FireLaserbolt.attackString, base.gameObject);
			this.animator = base.GetModelAnimator();
			base.characterBody.SetAimTimer(2f);
			this.FireGauntlet();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000319B8 File Offset: 0x0002FBB8
		private void FireGauntlet()
		{
			this.hasFiredGauntlet = true;
			Ray aimRay = base.GetAimRay();
			if (FireLaserbolt.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireLaserbolt.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					damage = FireLaserbolt.damageCoefficient * this.damageStat,
					force = FireLaserbolt.force,
					tracerEffectPrefab = FireLaserbolt.tracerEffectPrefab,
					muzzleName = this.muzzleString,
					hitEffectPrefab = FireLaserbolt.impactEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = 0.1f,
					smartCollision = false
				}.Fire();
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00031AC8 File Offset: 0x0002FCC8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator.GetFloat("FireGauntlet.fire") > 0f && !this.hasFiredGauntlet)
			{
				this.FireGauntlet();
			}
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			if (base.inputBank.skill1.down)
			{
				FireLaserbolt fireLaserbolt = new FireLaserbolt();
				fireLaserbolt.gauntlet = ((this.gauntlet == FireLaserbolt.Gauntlet.Left) ? FireLaserbolt.Gauntlet.Right : FireLaserbolt.Gauntlet.Left);
				this.outer.SetNextState(fireLaserbolt);
				return;
			}
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000E2E RID: 3630
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04000E2F RID: 3631
		public static GameObject tracerEffectPrefab;

		// Token: 0x04000E30 RID: 3632
		public static GameObject impactEffectPrefab;

		// Token: 0x04000E31 RID: 3633
		public static float baseDuration = 2f;

		// Token: 0x04000E32 RID: 3634
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000E33 RID: 3635
		public static float force = 20f;

		// Token: 0x04000E34 RID: 3636
		public static string attackString;

		// Token: 0x04000E35 RID: 3637
		private float duration;

		// Token: 0x04000E36 RID: 3638
		private bool hasFiredGauntlet;

		// Token: 0x04000E37 RID: 3639
		private string muzzleString;

		// Token: 0x04000E38 RID: 3640
		private Animator animator;

		// Token: 0x04000E39 RID: 3641
		public FireLaserbolt.Gauntlet gauntlet;

		// Token: 0x020002A6 RID: 678
		public enum Gauntlet
		{
			// Token: 0x04000E3B RID: 3643
			Left,
			// Token: 0x04000E3C RID: 3644
			Right
		}
	}
}
