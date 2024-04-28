using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc.Weapon
{
	// Token: 0x02000283 RID: 643
	public class GroundLight2 : BasicMeleeAttack, SteppedSkillDef.IStepSetter
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002F675 File Offset: 0x0002D875
		private bool isComboFinisher
		{
			get
			{
				return this.step == 2;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00014341 File Offset: 0x00012541
		protected override bool allowExitFire
		{
			get
			{
				return base.characterBody && !base.characterBody.isSprinting;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002F680 File Offset: 0x0002D880
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.step = i;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002F68C File Offset: 0x0002D88C
		public override void OnEnter()
		{
			if (this.isComboFinisher)
			{
				this.swingEffectPrefab = GroundLight2.comboFinisherSwingEffectPrefab;
				this.hitPauseDuration = GroundLight2.comboFinisherhitPauseDuration;
				this.damageCoefficient = GroundLight2.comboFinisherDamageCoefficient;
				this.bloom = GroundLight2.comboFinisherBloom;
				this.hitBoxGroupName = "SwordLarge";
				this.baseDuration = GroundLight2.comboFinisherBaseDuration;
			}
			base.OnEnter();
			base.characterDirection.forward = base.GetAimRay().direction;
			this.durationBeforeInterruptable = (this.isComboFinisher ? (GroundLight2.comboFinisherBaseDurationBeforeInterruptable / this.attackSpeedStat) : (GroundLight2.baseDurationBeforeInterruptable / this.attackSpeedStat));
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001438C File Offset: 0x0001258C
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002F72A File Offset: 0x0002D92A
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			if (this.isComboFinisher)
			{
				overlapAttack.damageType = DamageType.ApplyMercExpose;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002F748 File Offset: 0x0002D948
		protected override void PlayAnimation()
		{
			this.animationStateName = "";
			string soundString = null;
			switch (this.step)
			{
			case 0:
				this.animationStateName = "GroundLight1";
				soundString = GroundLight2.slash1Sound;
				break;
			case 1:
				this.animationStateName = "GroundLight2";
				soundString = GroundLight2.slash1Sound;
				break;
			case 2:
				this.animationStateName = "GroundLight3";
				soundString = GroundLight2.slash3Sound;
				break;
			}
			bool @bool = this.animator.GetBool("isMoving");
			bool bool2 = this.animator.GetBool("isGrounded");
			if (!@bool && bool2)
			{
				base.PlayCrossfade("FullBody, Override", this.animationStateName, "GroundLight.playbackRate", this.duration, 0.05f);
			}
			else
			{
				base.PlayCrossfade("Gesture, Additive", this.animationStateName, "GroundLight.playbackRate", this.duration, 0.05f);
				base.PlayCrossfade("Gesture, Override", this.animationStateName, "GroundLight.playbackRate", this.duration, 0.05f);
			}
			Util.PlaySound(soundString, base.gameObject);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002F84F File Offset: 0x0002DA4F
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			base.characterBody.AddSpreadBloom(this.bloom);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002F868 File Offset: 0x0002DA68
		protected override void BeginMeleeAttackEffect()
		{
			this.swingEffectMuzzleString = this.animationStateName;
			base.AddRecoil(-0.1f * GroundLight2.recoilAmplitude, 0.1f * GroundLight2.recoilAmplitude, -1f * GroundLight2.recoilAmplitude, 1f * GroundLight2.recoilAmplitude);
			base.BeginMeleeAttackEffect();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002F8B9 File Offset: 0x0002DAB9
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.step);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002F8CF File Offset: 0x0002DACF
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.step = (int)reader.ReadByte();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002F8E4 File Offset: 0x0002DAE4
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge >= this.durationBeforeInterruptable)
			{
				return InterruptPriority.Skill;
			}
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000D4F RID: 3407
		public int step;

		// Token: 0x04000D50 RID: 3408
		public static float recoilAmplitude;

		// Token: 0x04000D51 RID: 3409
		public static float baseDurationBeforeInterruptable;

		// Token: 0x04000D52 RID: 3410
		[SerializeField]
		public float bloom;

		// Token: 0x04000D53 RID: 3411
		public static float comboFinisherBaseDuration;

		// Token: 0x04000D54 RID: 3412
		public static GameObject comboFinisherSwingEffectPrefab;

		// Token: 0x04000D55 RID: 3413
		public static float comboFinisherhitPauseDuration;

		// Token: 0x04000D56 RID: 3414
		public static float comboFinisherDamageCoefficient;

		// Token: 0x04000D57 RID: 3415
		public static float comboFinisherBloom;

		// Token: 0x04000D58 RID: 3416
		public static float comboFinisherBaseDurationBeforeInterruptable;

		// Token: 0x04000D59 RID: 3417
		public static string slash1Sound;

		// Token: 0x04000D5A RID: 3418
		public static string slash3Sound;

		// Token: 0x04000D5B RID: 3419
		private string animationStateName;

		// Token: 0x04000D5C RID: 3420
		private float durationBeforeInterruptable;
	}
}
