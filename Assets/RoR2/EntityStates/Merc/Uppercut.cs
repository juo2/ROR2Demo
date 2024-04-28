using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Merc
{
	// Token: 0x0200027E RID: 638
	public class Uppercut : BaseState
	{
		// Token: 0x06000B44 RID: 2884 RVA: 0x0002EF2C File Offset: 0x0002D12C
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.duration = Uppercut.baseDuration / this.attackSpeedStat;
			this.overlapAttack = base.InitMeleeOverlap(Uppercut.baseDamageCoefficient, Uppercut.hitEffectPrefab, base.GetModelTransform(), Uppercut.hitboxString);
			this.overlapAttack.forceVector = Vector3.up * Uppercut.upwardForceStrength;
			if (base.characterDirection && base.inputBank)
			{
				base.characterDirection.forward = base.inputBank.aimDirection;
			}
			Util.PlaySound(Uppercut.enterSoundString, base.gameObject);
			this.PlayAnim();
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002EFDF File Offset: 0x0002D1DF
		protected virtual void PlayAnim()
		{
			base.PlayCrossfade("FullBody, Override", "Uppercut", "Uppercut.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002F001 File Offset: 0x0002D201
		public override void OnExit()
		{
			base.OnExit();
			this.PlayAnimation("FullBody, Override", "UppercutExit");
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002F01C File Offset: 0x0002D21C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.hitPauseTimer -= Time.fixedDeltaTime;
			if (base.isAuthority)
			{
				if (this.animator.GetFloat("Sword.active") > 0.2f && !this.hasSwung)
				{
					this.hasSwung = true;
					base.characterMotor.Motor.ForceUnground();
					Util.PlayAttackSpeedSound(Uppercut.attackSoundString, base.gameObject, Uppercut.slashPitch);
					EffectManager.SimpleMuzzleFlash(Uppercut.swingEffectPrefab, base.gameObject, Uppercut.slashChildName, true);
				}
				if (base.FireMeleeOverlap(this.overlapAttack, this.animator, "Sword.active", 0f, false))
				{
					Util.PlaySound(Uppercut.hitSoundString, base.gameObject);
					if (!this.isInHitPause)
					{
						this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "Uppercut.playbackRate");
						this.hitPauseTimer = Uppercut.hitPauseDuration / this.attackSpeedStat;
						this.isInHitPause = true;
					}
				}
				if (this.hitPauseTimer <= 0f && this.isInHitPause)
				{
					base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
					base.characterMotor.Motor.ForceUnground();
					this.isInHitPause = false;
				}
				if (!this.isInHitPause)
				{
					if (base.characterMotor && base.characterDirection)
					{
						Vector3 velocity = base.characterDirection.forward * this.moveSpeedStat * Mathf.Lerp(Uppercut.moveSpeedBonusCoefficient, 0f, base.age / this.duration);
						velocity.y = Uppercut.yVelocityCurve.Evaluate(base.fixedAge / this.duration);
						base.characterMotor.velocity = velocity;
					}
				}
				else
				{
					base.fixedAge -= Time.fixedDeltaTime;
					base.characterMotor.velocity = Vector3.zero;
					this.hitPauseTimer -= Time.fixedDeltaTime;
					this.animator.SetFloat("Uppercut.playbackRate", 0f);
				}
				if (base.fixedAge >= this.duration)
				{
					if (this.hasSwung)
					{
						this.hasSwung = true;
						this.overlapAttack.Fire(null);
					}
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x04000D24 RID: 3364
		public static GameObject swingEffectPrefab;

		// Token: 0x04000D25 RID: 3365
		public static GameObject hitEffectPrefab;

		// Token: 0x04000D26 RID: 3366
		public static string enterSoundString;

		// Token: 0x04000D27 RID: 3367
		public static string attackSoundString;

		// Token: 0x04000D28 RID: 3368
		public static string hitSoundString;

		// Token: 0x04000D29 RID: 3369
		public static float slashPitch;

		// Token: 0x04000D2A RID: 3370
		public static float hitPauseDuration;

		// Token: 0x04000D2B RID: 3371
		public static float upwardForceStrength;

		// Token: 0x04000D2C RID: 3372
		public static float baseDuration;

		// Token: 0x04000D2D RID: 3373
		public static float baseDamageCoefficient;

		// Token: 0x04000D2E RID: 3374
		public static string slashChildName;

		// Token: 0x04000D2F RID: 3375
		public static float moveSpeedBonusCoefficient;

		// Token: 0x04000D30 RID: 3376
		public static string hitboxString;

		// Token: 0x04000D31 RID: 3377
		public static AnimationCurve yVelocityCurve;

		// Token: 0x04000D32 RID: 3378
		protected Animator animator;

		// Token: 0x04000D33 RID: 3379
		protected float duration;

		// Token: 0x04000D34 RID: 3380
		protected float hitInterval;

		// Token: 0x04000D35 RID: 3381
		protected bool hasSwung;

		// Token: 0x04000D36 RID: 3382
		protected float hitPauseTimer;

		// Token: 0x04000D37 RID: 3383
		protected bool isInHitPause;

		// Token: 0x04000D38 RID: 3384
		protected OverlapAttack overlapAttack;

		// Token: 0x04000D39 RID: 3385
		protected BaseState.HitStopCachedState hitStopCachedState;
	}
}
