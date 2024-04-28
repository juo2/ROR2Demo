using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GlobalSkills.WoundSlash
{
	// Token: 0x02000371 RID: 881
	public class WoundSlash : BaseSkillState
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00046438 File Offset: 0x00044638
		private float duration
		{
			get
			{
				return WoundSlash.baseDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00046448 File Offset: 0x00044648
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			Vector3 vector = base.characterBody.corePosition + aimRay.direction * WoundSlash.slashEffectOffset;
			Util.PlaySound(WoundSlash.soundString, base.gameObject);
			EffectData effectData = new EffectData
			{
				origin = vector,
				rotation = Util.QuaternionSafeLookRotation(aimRay.direction)
			};
			EffectManager.SpawnEffect(WoundSlash.slashEffectPrefab, effectData, true);
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					baseDamage = WoundSlash.blastDamageCoefficient * this.damageStat,
					baseForce = WoundSlash.blastForce,
					attackerFiltering = AttackerFiltering.NeverHitSelf,
					crit = base.RollCrit(),
					damageType = DamageType.Generic,
					falloffModel = BlastAttack.FalloffModel.None,
					inflictor = base.gameObject,
					position = vector,
					procChainMask = default(ProcChainMask),
					procCoefficient = WoundSlash.blastProcCoefficient,
					radius = WoundSlash.blastRadius,
					teamIndex = base.teamComponent.teamIndex,
					impactEffect = EffectCatalog.FindEffectIndexFromPrefab(WoundSlash.blastImpactEffectPrefab)
				}.Fire();
			}
			if (base.isAuthority && base.characterMotor)
			{
				base.characterMotor.velocity.y = Mathf.Max(base.characterMotor.velocity.y, WoundSlash.shortHopVelocity);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000465B4 File Offset: 0x000447B4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04001450 RID: 5200
		public static float blastRadius;

		// Token: 0x04001451 RID: 5201
		public static float blastDamageCoefficient;

		// Token: 0x04001452 RID: 5202
		public static float blastForce;

		// Token: 0x04001453 RID: 5203
		public static float blastProcCoefficient;

		// Token: 0x04001454 RID: 5204
		public static GameObject blastImpactEffectPrefab;

		// Token: 0x04001455 RID: 5205
		public static GameObject slashEffectPrefab;

		// Token: 0x04001456 RID: 5206
		public static float slashEffectOffset;

		// Token: 0x04001457 RID: 5207
		public static float baseDuration;

		// Token: 0x04001458 RID: 5208
		public static string soundString;

		// Token: 0x04001459 RID: 5209
		public static float shortHopVelocity;
	}
}
