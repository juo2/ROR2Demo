using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x0200027B RID: 635
	public class GroundLight : BaseState
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x0002E9E8 File Offset: 0x0002CBE8
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.earlyExitDuration = GroundLight.baseEarlyExitDuration / this.attackSpeedStat;
			this.animator = base.GetModelAnimator();
			bool @bool = this.animator.GetBool("isMoving");
			bool bool2 = this.animator.GetBool("isGrounded");
			switch (this.comboState)
			{
			case GroundLight.ComboState.GroundLight1:
				this.attackDuration = GroundLight.baseComboAttackDuration / this.attackSpeedStat;
				this.overlapAttack = base.InitMeleeOverlap(GroundLight.comboDamageCoefficient, this.hitEffectPrefab, base.GetModelTransform(), "Sword");
				if (@bool || !bool2)
				{
					base.PlayAnimation("Gesture, Additive", "GroundLight1", "GroundLight.playbackRate", this.attackDuration);
					base.PlayAnimation("Gesture, Override", "GroundLight1", "GroundLight.playbackRate", this.attackDuration);
				}
				else
				{
					base.PlayAnimation("FullBody, Override", "GroundLight1", "GroundLight.playbackRate", this.attackDuration);
				}
				this.slashChildName = "GroundLight1Slash";
				this.swingEffectPrefab = GroundLight.comboSwingEffectPrefab;
				this.hitEffectPrefab = GroundLight.comboHitEffectPrefab;
				this.attackSoundString = GroundLight.comboAttackSoundString;
				break;
			case GroundLight.ComboState.GroundLight2:
				this.attackDuration = GroundLight.baseComboAttackDuration / this.attackSpeedStat;
				this.overlapAttack = base.InitMeleeOverlap(GroundLight.comboDamageCoefficient, this.hitEffectPrefab, base.GetModelTransform(), "Sword");
				if (@bool || !bool2)
				{
					base.PlayAnimation("Gesture, Additive", "GroundLight2", "GroundLight.playbackRate", this.attackDuration);
					base.PlayAnimation("Gesture, Override", "GroundLight2", "GroundLight.playbackRate", this.attackDuration);
				}
				else
				{
					base.PlayAnimation("FullBody, Override", "GroundLight2", "GroundLight.playbackRate", this.attackDuration);
				}
				this.slashChildName = "GroundLight2Slash";
				this.swingEffectPrefab = GroundLight.comboSwingEffectPrefab;
				this.hitEffectPrefab = GroundLight.comboHitEffectPrefab;
				this.attackSoundString = GroundLight.comboAttackSoundString;
				break;
			case GroundLight.ComboState.GroundLight3:
				this.attackDuration = GroundLight.baseFinisherAttackDuration / this.attackSpeedStat;
				this.overlapAttack = base.InitMeleeOverlap(GroundLight.finisherDamageCoefficient, this.hitEffectPrefab, base.GetModelTransform(), "SwordLarge");
				if (@bool || !bool2)
				{
					base.PlayAnimation("Gesture, Additive", "GroundLight3", "GroundLight.playbackRate", this.attackDuration);
					base.PlayAnimation("Gesture, Override", "GroundLight3", "GroundLight.playbackRate", this.attackDuration);
				}
				else
				{
					base.PlayAnimation("FullBody, Override", "GroundLight3", "GroundLight.playbackRate", this.attackDuration);
				}
				this.slashChildName = "GroundLight3Slash";
				this.swingEffectPrefab = GroundLight.finisherSwingEffectPrefab;
				this.hitEffectPrefab = GroundLight.finisherHitEffectPrefab;
				this.attackSoundString = GroundLight.finisherAttackSoundString;
				break;
			}
			base.characterBody.SetAimTimer(this.attackDuration + 1f);
			this.overlapAttack.hitEffectPrefab = this.hitEffectPrefab;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.hitPauseTimer -= Time.fixedDeltaTime;
			if (base.isAuthority)
			{
				bool flag = base.FireMeleeOverlap(this.overlapAttack, this.animator, "Sword.active", GroundLight.forceMagnitude, true);
				this.hasHit = (this.hasHit || flag);
				if (flag)
				{
					Util.PlaySound(GroundLight.hitSoundString, base.gameObject);
					if (!this.isInHitPause)
					{
						this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "GroundLight.playbackRate");
						this.hitPauseTimer = GroundLight.hitPauseDuration / this.attackSpeedStat;
						this.isInHitPause = true;
					}
				}
				if (this.hitPauseTimer <= 0f && this.isInHitPause)
				{
					base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
					this.isInHitPause = false;
				}
			}
			if (this.animator.GetFloat("Sword.active") > 0f && !this.hasSwung)
			{
				Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, GroundLight.slashPitch);
				HealthComponent healthComponent = base.characterBody.healthComponent;
				CharacterDirection component = base.characterBody.GetComponent<CharacterDirection>();
				if (healthComponent)
				{
					healthComponent.TakeDamageForce(GroundLight.selfForceMagnitude * component.forward, true, false);
				}
				this.hasSwung = true;
				EffectManager.SimpleMuzzleFlash(this.swingEffectPrefab, base.gameObject, this.slashChildName, false);
			}
			if (!this.isInHitPause)
			{
				this.stopwatch += Time.fixedDeltaTime;
			}
			else
			{
				base.characterMotor.velocity = Vector3.zero;
				this.animator.SetFloat("GroundLight.playbackRate", 0f);
			}
			if (base.isAuthority && this.stopwatch >= this.attackDuration - this.earlyExitDuration)
			{
				if (!this.hasSwung)
				{
					this.overlapAttack.Fire(null);
				}
				if (base.inputBank.skill1.down && this.comboState != GroundLight.ComboState.GroundLight3)
				{
					GroundLight groundLight = new GroundLight();
					groundLight.comboState = this.comboState + 1;
					this.outer.SetNextState(groundLight);
					return;
				}
				if (this.stopwatch >= this.attackDuration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002EEFF File Offset: 0x0002D0FF
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.comboState);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002EF15 File Offset: 0x0002D115
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.comboState = (GroundLight.ComboState)reader.ReadByte();
		}

		// Token: 0x04000CFD RID: 3325
		public static float baseComboAttackDuration;

		// Token: 0x04000CFE RID: 3326
		public static float baseFinisherAttackDuration;

		// Token: 0x04000CFF RID: 3327
		public static float baseEarlyExitDuration;

		// Token: 0x04000D00 RID: 3328
		public static string comboAttackSoundString;

		// Token: 0x04000D01 RID: 3329
		public static string finisherAttackSoundString;

		// Token: 0x04000D02 RID: 3330
		public static float comboDamageCoefficient;

		// Token: 0x04000D03 RID: 3331
		public static float finisherDamageCoefficient;

		// Token: 0x04000D04 RID: 3332
		public static float forceMagnitude;

		// Token: 0x04000D05 RID: 3333
		public static GameObject comboHitEffectPrefab;

		// Token: 0x04000D06 RID: 3334
		public static GameObject finisherHitEffectPrefab;

		// Token: 0x04000D07 RID: 3335
		public static GameObject comboSwingEffectPrefab;

		// Token: 0x04000D08 RID: 3336
		public static GameObject finisherSwingEffectPrefab;

		// Token: 0x04000D09 RID: 3337
		public static float hitPauseDuration;

		// Token: 0x04000D0A RID: 3338
		public static float selfForceMagnitude;

		// Token: 0x04000D0B RID: 3339
		public static string hitSoundString;

		// Token: 0x04000D0C RID: 3340
		public static float slashPitch;

		// Token: 0x04000D0D RID: 3341
		private float stopwatch;

		// Token: 0x04000D0E RID: 3342
		private float attackDuration;

		// Token: 0x04000D0F RID: 3343
		private float earlyExitDuration;

		// Token: 0x04000D10 RID: 3344
		private Animator animator;

		// Token: 0x04000D11 RID: 3345
		private OverlapAttack overlapAttack;

		// Token: 0x04000D12 RID: 3346
		private float hitPauseTimer;

		// Token: 0x04000D13 RID: 3347
		private bool isInHitPause;

		// Token: 0x04000D14 RID: 3348
		private bool hasSwung;

		// Token: 0x04000D15 RID: 3349
		private bool hasHit;

		// Token: 0x04000D16 RID: 3350
		private GameObject swingEffectInstance;

		// Token: 0x04000D17 RID: 3351
		public GroundLight.ComboState comboState;

		// Token: 0x04000D18 RID: 3352
		private Vector3 characterForward;

		// Token: 0x04000D19 RID: 3353
		private string slashChildName;

		// Token: 0x04000D1A RID: 3354
		private BaseState.HitStopCachedState hitStopCachedState;

		// Token: 0x04000D1B RID: 3355
		private GameObject swingEffectPrefab;

		// Token: 0x04000D1C RID: 3356
		private GameObject hitEffectPrefab;

		// Token: 0x04000D1D RID: 3357
		private string attackSoundString;

		// Token: 0x0200027C RID: 636
		public enum ComboState
		{
			// Token: 0x04000D1F RID: 3359
			GroundLight1,
			// Token: 0x04000D20 RID: 3360
			GroundLight2,
			// Token: 0x04000D21 RID: 3361
			GroundLight3
		}

		// Token: 0x0200027D RID: 637
		private struct ComboStateInfo
		{
			// Token: 0x04000D22 RID: 3362
			private string mecanimStateName;

			// Token: 0x04000D23 RID: 3363
			private string mecanimPlaybackRateName;
		}
	}
}
