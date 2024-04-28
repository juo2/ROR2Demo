using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002A9 RID: 681
	public class IceNova : BaseState
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x000325E8 File Offset: 0x000307E8
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.endDuration = IceNova.baseEndDuration / this.attackSpeedStat;
			this.startDuration = IceNova.baseStartDuration / this.attackSpeedStat;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00032620 File Offset: 0x00030820
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.startDuration && !this.hasCastNova)
			{
				this.hasCastNova = true;
				EffectManager.SpawnEffect(IceNova.novaEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = IceNova.novaRadius
				}, true);
				BlastAttack blastAttack = new BlastAttack();
				blastAttack.radius = IceNova.novaRadius;
				blastAttack.procCoefficient = IceNova.procCoefficient;
				blastAttack.position = base.transform.position;
				blastAttack.attacker = base.gameObject;
				blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
				blastAttack.baseDamage = base.characterBody.damage * IceNova.damageCoefficient;
				blastAttack.falloffModel = BlastAttack.FalloffModel.None;
				blastAttack.damageType = DamageType.Freeze2s;
				blastAttack.baseForce = IceNova.force;
				blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
				blastAttack.Fire();
			}
			if (this.stopwatch >= this.startDuration + this.endDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000E7C RID: 3708
		public static GameObject impactEffectPrefab;

		// Token: 0x04000E7D RID: 3709
		public static GameObject novaEffectPrefab;

		// Token: 0x04000E7E RID: 3710
		public static float baseStartDuration;

		// Token: 0x04000E7F RID: 3711
		public static float baseEndDuration = 2f;

		// Token: 0x04000E80 RID: 3712
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000E81 RID: 3713
		public static float procCoefficient;

		// Token: 0x04000E82 RID: 3714
		public static float force = 20f;

		// Token: 0x04000E83 RID: 3715
		public static float novaRadius;

		// Token: 0x04000E84 RID: 3716
		public static string attackString;

		// Token: 0x04000E85 RID: 3717
		private float stopwatch;

		// Token: 0x04000E86 RID: 3718
		private float startDuration;

		// Token: 0x04000E87 RID: 3719
		private float endDuration;

		// Token: 0x04000E88 RID: 3720
		private bool hasCastNova;
	}
}
