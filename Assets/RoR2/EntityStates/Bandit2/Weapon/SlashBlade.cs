using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000484 RID: 1156
	public class SlashBlade : BasicMeleeAttack
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0005BAED File Offset: 0x00059CED
		private float minimumDuration
		{
			get
			{
				return SlashBlade.minimumBaseDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005BAFC File Offset: 0x00059CFC
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Gesture, Additive", "SlashBlade", "SlashBlade.playbackRate", this.duration);
			this.bladeMeshObject = base.FindModelChild("BladeMesh").gameObject;
			if (this.bladeMeshObject)
			{
				this.bladeMeshObject.SetActive(true);
			}
			base.characterMotor.ApplyForce(base.inputBank.moveVector * SlashBlade.selfForceStrength, true, false);
			if (base.characterMotor)
			{
				base.characterMotor.velocity = new Vector3(base.characterMotor.velocity.x, Mathf.Max(base.characterMotor.velocity.y, SlashBlade.shortHopVelocity), base.characterMotor.velocity.z);
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005BBD2 File Offset: 0x00059DD2
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			overlapAttack.damageType = DamageType.SuperBleedOnCrit;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005BBE6 File Offset: 0x00059DE6
		public override void Update()
		{
			base.Update();
			base.characterBody.SetSpreadBloom(SlashBlade.bloomCurve.Evaluate(base.age / this.duration), false);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0005BC11 File Offset: 0x00059E11
		public override void OnExit()
		{
			if (this.bladeMeshObject)
			{
				this.bladeMeshObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005BC32 File Offset: 0x00059E32
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (!base.inputBank)
			{
				return InterruptPriority.Skill;
			}
			if (base.fixedAge <= this.minimumDuration)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04001A60 RID: 6752
		public static float shortHopVelocity;

		// Token: 0x04001A61 RID: 6753
		public static float selfForceStrength;

		// Token: 0x04001A62 RID: 6754
		public static float minimumBaseDuration;

		// Token: 0x04001A63 RID: 6755
		public static AnimationCurve bloomCurve;

		// Token: 0x04001A64 RID: 6756
		private GameObject bladeMeshObject;
	}
}
