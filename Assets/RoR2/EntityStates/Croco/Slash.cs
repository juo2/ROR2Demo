using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Croco
{
	// Token: 0x020003DF RID: 991
	public class Slash : BasicMeleeAttack, SteppedSkillDef.IStepSetter
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004E287 File Offset: 0x0004C487
		private bool isComboFinisher
		{
			get
			{
				return this.step == 2;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00014341 File Offset: 0x00012541
		protected override bool allowExitFire
		{
			get
			{
				return base.characterBody && !base.characterBody.isSprinting;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0004E292 File Offset: 0x0004C492
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.step = i;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004E29C File Offset: 0x0004C49C
		public override void OnEnter()
		{
			if (this.isComboFinisher)
			{
				this.swingEffectPrefab = Slash.comboFinisherSwingEffectPrefab;
				this.hitPauseDuration = Slash.comboFinisherhitPauseDuration;
				this.damageCoefficient = Slash.comboFinisherDamageCoefficient;
				this.bloom = Slash.comboFinisherBloom;
			}
			base.OnEnter();
			base.characterDirection.forward = base.GetAimRay().direction;
			this.durationBeforeInterruptable = (this.isComboFinisher ? (Slash.comboFinisherBaseDurationBeforeInterruptable / this.attackSpeedStat) : (Slash.baseDurationBeforeInterruptable / this.attackSpeedStat));
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0001438C File Offset: 0x0001258C
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00014394 File Offset: 0x00012594
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004E324 File Offset: 0x0004C524
		protected override void PlayAnimation()
		{
			this.animationStateName = "";
			string soundString = null;
			switch (this.step)
			{
			case 0:
				this.animationStateName = "Slash1";
				soundString = Slash.slash1Sound;
				break;
			case 1:
				this.animationStateName = "Slash2";
				soundString = Slash.slash1Sound;
				break;
			case 2:
				this.animationStateName = "Slash3";
				soundString = Slash.slash3Sound;
				break;
			}
			float duration = Mathf.Max(this.duration, 0.2f);
			base.PlayCrossfade("Gesture, Additive", this.animationStateName, "Slash.playbackRate", duration, 0.05f);
			base.PlayCrossfade("Gesture, Override", this.animationStateName, "Slash.playbackRate", duration, 0.05f);
			Util.PlaySound(soundString, base.gameObject);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			base.characterBody.AddSpreadBloom(this.bloom);
			if (!this.hasGrantedBuff && this.isComboFinisher)
			{
				this.hasGrantedBuff = true;
				base.characterBody.AddTimedBuffAuthority(RoR2Content.Buffs.CrocoRegen.buffIndex, 0.5f);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004E440 File Offset: 0x0004C640
		protected override void BeginMeleeAttackEffect()
		{
			this.swingEffectMuzzleString = this.animationStateName;
			base.AddRecoil(-0.1f * Slash.recoilAmplitude, 0.1f * Slash.recoilAmplitude, -1f * Slash.recoilAmplitude, 1f * Slash.recoilAmplitude);
			base.BeginMeleeAttackEffect();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004E491 File Offset: 0x0004C691
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.step);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004E4A7 File Offset: 0x0004C6A7
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.step = (int)reader.ReadByte();
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004E4BC File Offset: 0x0004C6BC
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge >= this.durationBeforeInterruptable)
			{
				return InterruptPriority.Skill;
			}
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400167F RID: 5759
		public int step;

		// Token: 0x04001680 RID: 5760
		public static float recoilAmplitude;

		// Token: 0x04001681 RID: 5761
		public static float baseDurationBeforeInterruptable;

		// Token: 0x04001682 RID: 5762
		[SerializeField]
		public float bloom;

		// Token: 0x04001683 RID: 5763
		public static GameObject comboFinisherSwingEffectPrefab;

		// Token: 0x04001684 RID: 5764
		public static float comboFinisherhitPauseDuration;

		// Token: 0x04001685 RID: 5765
		public static float comboFinisherDamageCoefficient;

		// Token: 0x04001686 RID: 5766
		public static float comboFinisherBloom;

		// Token: 0x04001687 RID: 5767
		public static float comboFinisherBaseDurationBeforeInterruptable;

		// Token: 0x04001688 RID: 5768
		public static string slash1Sound;

		// Token: 0x04001689 RID: 5769
		public static string slash3Sound;

		// Token: 0x0400168A RID: 5770
		private string animationStateName;

		// Token: 0x0400168B RID: 5771
		private float durationBeforeInterruptable;

		// Token: 0x0400168C RID: 5772
		private bool hasGrantedBuff;
	}
}
