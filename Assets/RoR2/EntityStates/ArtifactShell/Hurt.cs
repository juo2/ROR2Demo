using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000491 RID: 1169
	public class Hurt : ArtifactShellBaseState
	{
		// Token: 0x060014EE RID: 5358 RVA: 0x0005CE00 File Offset: 0x0005B000
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Hurt.baseDuration;
			Vector3 position = base.transform.position;
			if (NetworkServer.active)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.position = position;
				damageInfo.attacker = null;
				damageInfo.inflictor = null;
				damageInfo.damage = Mathf.Ceil(base.healthComponent.fullCombinedHealth * 0.25f);
				damageInfo.damageType = (DamageType.BypassArmor | DamageType.Silent);
				damageInfo.procCoefficient = 0f;
				base.healthComponent.TakeDamage(damageInfo);
				new BlastAttack
				{
					position = position + Hurt.blastOriginOffset * Vector3.up,
					attacker = base.gameObject,
					inflictor = base.gameObject,
					attackerFiltering = AttackerFiltering.NeverHitSelf,
					baseDamage = 0f,
					baseForce = Hurt.knockbackForce,
					bonusForce = Vector3.up * Hurt.knockbackLiftForce,
					falloffModel = BlastAttack.FalloffModel.Linear,
					radius = Hurt.blastRadius,
					procCoefficient = 0f,
					teamIndex = TeamIndex.None
				}.Fire();
				ArtifactTrialMissionController.RemoveAllMissionKeys();
			}
			EffectData effectData = new EffectData
			{
				origin = position,
				scale = Hurt.blastRadius
			};
			EffectManager.SpawnEffect(Hurt.novaEffectPrefab, effectData, false);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0005CF4A File Offset: 0x0005B14A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new WaitForKey());
			}
		}

		// Token: 0x04001ABC RID: 6844
		public static float baseDuration = 2f;

		// Token: 0x04001ABD RID: 6845
		public static float blastRadius = 50f;

		// Token: 0x04001ABE RID: 6846
		public static float knockbackForce = 5000f;

		// Token: 0x04001ABF RID: 6847
		public static float knockbackLiftForce = 2000f;

		// Token: 0x04001AC0 RID: 6848
		public static float blastOriginOffset = -10f;

		// Token: 0x04001AC1 RID: 6849
		public static GameObject novaEffectPrefab;

		// Token: 0x04001AC2 RID: 6850
		private float duration;
	}
}
