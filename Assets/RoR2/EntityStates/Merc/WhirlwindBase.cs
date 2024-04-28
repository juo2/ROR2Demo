using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Merc
{
	// Token: 0x0200027F RID: 639
	public class WhirlwindBase : BaseState
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x0002F270 File Offset: 0x0002D470
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.overlapAttack = base.InitMeleeOverlap(this.baseDamageCoefficient, WhirlwindBase.hitEffectPrefab, base.GetModelTransform(), this.hitboxString);
			if (base.characterDirection && base.inputBank)
			{
				base.characterDirection.forward = base.inputBank.aimDirection;
			}
			base.SmallHop(base.characterMotor, this.smallHopVelocity);
			this.PlayAnim();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void PlayAnim()
		{
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002F310 File Offset: 0x0002D510
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.hitPauseTimer -= Time.fixedDeltaTime;
			if (this.animator.GetFloat("Sword.active") > (float)this.swingCount)
			{
				this.swingCount++;
				Util.PlayAttackSpeedSound(WhirlwindBase.attackSoundString, base.gameObject, WhirlwindBase.slashPitch);
				EffectManager.SimpleMuzzleFlash(WhirlwindBase.swingEffectPrefab, base.gameObject, this.slashChildName, false);
				if (base.isAuthority)
				{
					this.overlapAttack.ResetIgnoredHealthComponents();
					if (base.characterMotor)
					{
						base.characterMotor.ApplyForce(this.selfForceMagnitude * base.characterDirection.forward, true, false);
					}
				}
			}
			if (base.isAuthority)
			{
				if (base.FireMeleeOverlap(this.overlapAttack, this.animator, "Sword.active", 0f, true))
				{
					Util.PlaySound(WhirlwindBase.hitSoundString, base.gameObject);
					if (!this.isInHitPause)
					{
						this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "Whirlwind.playbackRate");
						this.hitPauseTimer = WhirlwindBase.hitPauseDuration / this.attackSpeedStat;
						this.isInHitPause = true;
					}
				}
				if (this.hitPauseTimer <= 0f && this.isInHitPause)
				{
					base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
					this.isInHitPause = false;
				}
				if (!this.isInHitPause)
				{
					if (base.characterMotor && base.characterDirection)
					{
						Vector3 velocity = base.characterDirection.forward * this.moveSpeedStat * Mathf.Lerp(this.moveSpeedBonusCoefficient, 1f, base.age / this.duration);
						velocity.y = base.characterMotor.velocity.y;
						base.characterMotor.velocity = velocity;
					}
				}
				else
				{
					base.characterMotor.velocity = Vector3.zero;
					this.hitPauseTimer -= Time.fixedDeltaTime;
					this.animator.SetFloat("Whirlwind.playbackRate", 0f);
				}
				if (base.fixedAge >= this.duration)
				{
					while (this.swingCount < 2)
					{
						this.swingCount++;
						this.overlapAttack.Fire(null);
					}
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x04000D3A RID: 3386
		public static GameObject swingEffectPrefab;

		// Token: 0x04000D3B RID: 3387
		public static GameObject hitEffectPrefab;

		// Token: 0x04000D3C RID: 3388
		public static string attackSoundString;

		// Token: 0x04000D3D RID: 3389
		public static string hitSoundString;

		// Token: 0x04000D3E RID: 3390
		public static float slashPitch;

		// Token: 0x04000D3F RID: 3391
		public static float hitPauseDuration;

		// Token: 0x04000D40 RID: 3392
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000D41 RID: 3393
		[SerializeField]
		public float baseDamageCoefficient;

		// Token: 0x04000D42 RID: 3394
		[SerializeField]
		public string slashChildName;

		// Token: 0x04000D43 RID: 3395
		[SerializeField]
		public float selfForceMagnitude;

		// Token: 0x04000D44 RID: 3396
		[SerializeField]
		public float moveSpeedBonusCoefficient;

		// Token: 0x04000D45 RID: 3397
		[SerializeField]
		public float smallHopVelocity;

		// Token: 0x04000D46 RID: 3398
		[SerializeField]
		public string hitboxString;

		// Token: 0x04000D47 RID: 3399
		protected Animator animator;

		// Token: 0x04000D48 RID: 3400
		protected float duration;

		// Token: 0x04000D49 RID: 3401
		protected float hitInterval;

		// Token: 0x04000D4A RID: 3402
		protected int swingCount;

		// Token: 0x04000D4B RID: 3403
		protected float hitPauseTimer;

		// Token: 0x04000D4C RID: 3404
		protected bool isInHitPause;

		// Token: 0x04000D4D RID: 3405
		protected OverlapAttack overlapAttack;

		// Token: 0x04000D4E RID: 3406
		protected BaseState.HitStopCachedState hitStopCachedState;
	}
}
