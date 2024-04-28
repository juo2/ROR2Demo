using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Bandit2
{
	// Token: 0x02000475 RID: 1141
	public class StealthMode : BaseState
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x0005AE4C File Offset: 0x0005904C
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.animator;
			if (base.characterBody)
			{
				if (NetworkServer.active)
				{
					base.characterBody.AddBuff(RoR2Content.Buffs.Cloak);
					base.characterBody.AddBuff(RoR2Content.Buffs.CloakSpeed);
				}
				base.characterBody.onSkillActivatedAuthority += this.OnSkillActivatedAuthority;
			}
			this.FireSmokebomb();
			Util.PlaySound(StealthMode.enterStealthSound, base.gameObject);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005AED9 File Offset: 0x000590D9
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > StealthMode.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0005AEFC File Offset: 0x000590FC
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.FireSmokebomb();
			}
			Util.PlaySound(StealthMode.exitStealthSound, base.gameObject);
			if (base.characterBody)
			{
				base.characterBody.onSkillActivatedAuthority -= this.OnSkillActivatedAuthority;
				if (NetworkServer.active)
				{
					base.characterBody.RemoveBuff(RoR2Content.Buffs.CloakSpeed);
					base.characterBody.RemoveBuff(RoR2Content.Buffs.Cloak);
				}
			}
			if (this.animator)
			{
				this.animator.SetLayerWeight(this.animator.GetLayerIndex("Body, StealthWeapon"), 0f);
			}
			base.OnExit();
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005AFAB File Offset: 0x000591AB
		private void OnSkillActivatedAuthority(GenericSkill skill)
		{
			if (skill.skillDef.isCombatSkill)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005AFC8 File Offset: 0x000591C8
		private void FireSmokebomb()
		{
			if (base.isAuthority)
			{
				BlastAttack blastAttack = new BlastAttack();
				blastAttack.radius = StealthMode.blastAttackRadius;
				blastAttack.procCoefficient = StealthMode.blastAttackProcCoefficient;
				blastAttack.position = base.transform.position;
				blastAttack.attacker = base.gameObject;
				blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
				blastAttack.baseDamage = base.characterBody.damage * StealthMode.blastAttackDamageCoefficient;
				blastAttack.falloffModel = BlastAttack.FalloffModel.None;
				blastAttack.damageType = DamageType.Stun1s;
				blastAttack.baseForce = StealthMode.blastAttackForce;
				blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
				blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
				blastAttack.Fire();
			}
			if (StealthMode.smokeBombEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(StealthMode.smokeBombEffectPrefab, base.gameObject, StealthMode.smokeBombMuzzleString, false);
			}
			if (base.characterMotor)
			{
				base.characterMotor.velocity = new Vector3(base.characterMotor.velocity.x, StealthMode.shortHopVelocity, base.characterMotor.velocity.z);
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001A28 RID: 6696
		public static float duration;

		// Token: 0x04001A29 RID: 6697
		public static string enterStealthSound;

		// Token: 0x04001A2A RID: 6698
		public static string exitStealthSound;

		// Token: 0x04001A2B RID: 6699
		public static float blastAttackRadius;

		// Token: 0x04001A2C RID: 6700
		public static float blastAttackDamageCoefficient;

		// Token: 0x04001A2D RID: 6701
		public static float blastAttackProcCoefficient;

		// Token: 0x04001A2E RID: 6702
		public static float blastAttackForce;

		// Token: 0x04001A2F RID: 6703
		public static GameObject smokeBombEffectPrefab;

		// Token: 0x04001A30 RID: 6704
		public static string smokeBombMuzzleString;

		// Token: 0x04001A31 RID: 6705
		public static float shortHopVelocity;

		// Token: 0x04001A32 RID: 6706
		private Animator animator;
	}
}
