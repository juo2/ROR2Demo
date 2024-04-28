using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003D8 RID: 984
	public class Bite : BasicMeleeAttack
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00014341 File Offset: 0x00012541
		protected override bool allowExitFire
		{
			get
			{
				return base.characterBody && !base.characterBody.isSprinting;
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0004D760 File Offset: 0x0004B960
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterDirection.forward = base.GetAimRay().direction;
			this.durationBeforeInterruptable = Bite.baseDurationBeforeInterruptable / this.attackSpeedStat;
			this.crocoDamageTypeController = base.GetComponent<CrocoDamageTypeController>();
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0001438C File Offset: 0x0001258C
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0004D7AC File Offset: 0x0004B9AC
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			DamageType damageType = this.crocoDamageTypeController ? this.crocoDamageTypeController.GetDamageType() : DamageType.Generic;
			overlapAttack.damageType = (damageType | DamageType.BonusToLowHealth);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004D7EC File Offset: 0x0004B9EC
		protected override void PlayAnimation()
		{
			float duration = Mathf.Max(this.duration, 0.2f);
			base.PlayCrossfade("Gesture, Additive", "Bite", "Bite.playbackRate", duration, 0.05f);
			base.PlayCrossfade("Gesture, Override", "Bite", "Bite.playbackRate", duration, 0.05f);
			Util.PlaySound(Bite.biteSound, base.gameObject);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004D854 File Offset: 0x0004BA54
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			base.characterBody.AddSpreadBloom(this.bloom);
			if (!this.hasGrantedBuff)
			{
				this.hasGrantedBuff = true;
				base.characterBody.AddTimedBuffAuthority(RoR2Content.Buffs.CrocoRegen.buffIndex, 0.5f);
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004D8A1 File Offset: 0x0004BAA1
		protected override void BeginMeleeAttackEffect()
		{
			base.AddRecoil(0.9f * Bite.recoilAmplitude, 1.1f * Bite.recoilAmplitude, -0.1f * Bite.recoilAmplitude, 0.1f * Bite.recoilAmplitude);
			base.BeginMeleeAttackEffect();
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004D8DB File Offset: 0x0004BADB
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge >= this.durationBeforeInterruptable)
			{
				return InterruptPriority.Skill;
			}
			return InterruptPriority.Pain;
		}

		// Token: 0x04001647 RID: 5703
		public static float recoilAmplitude;

		// Token: 0x04001648 RID: 5704
		public static float baseDurationBeforeInterruptable;

		// Token: 0x04001649 RID: 5705
		[SerializeField]
		public float bloom;

		// Token: 0x0400164A RID: 5706
		public static string biteSound;

		// Token: 0x0400164B RID: 5707
		private string animationStateName;

		// Token: 0x0400164C RID: 5708
		private float durationBeforeInterruptable;

		// Token: 0x0400164D RID: 5709
		private CrocoDamageTypeController crocoDamageTypeController;

		// Token: 0x0400164E RID: 5710
		private bool hasGrantedBuff;
	}
}
