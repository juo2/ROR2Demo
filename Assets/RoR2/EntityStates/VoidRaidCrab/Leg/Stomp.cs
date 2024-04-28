using System;
using System.Runtime.CompilerServices;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x0200013E RID: 318
	public class Stomp : BaseStompState
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00018155 File Offset: 0x00016355
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new PostStompReturnToBase
			{
				target = this.target
			});
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018173 File Offset: 0x00016373
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.hasDoneBlast && base.legController.mainBody && base.legController.mainBody.hasEffectiveAuthority)
			{
				this.TryStompCollisionAuthority();
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000181B0 File Offset: 0x000163B0
		private void TryStompCollisionAuthority()
		{
			Stomp.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.currentToePosition = base.legController.toeTipTransform.position;
			this.<TryStompCollisionAuthority>g__TryAttack|8_0(ref CS$<>8__locals1);
			this.previousToePosition = new Vector3?(CS$<>8__locals1.currentToePosition);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000181F8 File Offset: 0x000163F8
		private void DoBlastAuthority(Vector3 blastOrigin)
		{
			CharacterBody mainBody = base.legController.mainBody;
			EffectData effectData = new EffectData();
			effectData.origin = blastOrigin;
			effectData.scale = Stomp.blastRadius;
			effectData.rotation = Quaternion.LookRotation(Vector3.up);
			EffectManager.SpawnEffect(Stomp.blastEffectPrefab, effectData, true);
			BlastAttack blastAttack = new BlastAttack();
			blastAttack.attacker = mainBody.gameObject;
			blastAttack.inflictor = blastAttack.attacker;
			blastAttack.baseDamage = Stomp.blastDamageCoefficient * mainBody.damage;
			blastAttack.baseForce = Stomp.blastForce;
			blastAttack.position = blastOrigin;
			blastAttack.radius = Stomp.blastRadius;
			blastAttack.falloffModel = BlastAttack.FalloffModel.SweetSpot;
			blastAttack.teamIndex = mainBody.teamComponent.teamIndex;
			blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
			blastAttack.damageType = DamageType.Generic;
			blastAttack.crit = mainBody.RollCrit();
			blastAttack.damageColorIndex = DamageColorIndex.Default;
			blastAttack.impactEffect = EffectIndex.Invalid;
			blastAttack.losType = BlastAttack.LoSType.None;
			blastAttack.procChainMask = default(ProcChainMask);
			blastAttack.procCoefficient = 1f;
			blastAttack.Fire();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000182F8 File Offset: 0x000164F8
		private void DoStompAttackAuthority(Vector3 hitPosition)
		{
			CharacterBody mainBody = base.legController.mainBody;
			if (!mainBody)
			{
				return;
			}
			Vector3 position = base.legController.footTranform.position;
			Vector3 position2 = base.legController.toeTipTransform.position;
			RaycastHit raycastHit;
			Vector3 vector = Physics.Linecast(position, position2, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore) ? raycastHit.point : position2;
			EffectData effectData = new EffectData();
			effectData.origin = vector;
			effectData.scale = Stomp.blastRadius;
			effectData.rotation = Quaternion.LookRotation(-raycastHit.normal);
			EffectManager.SpawnEffect(Stomp.blastEffectPrefab, effectData, true);
			BlastAttack blastAttack = new BlastAttack();
			blastAttack.attacker = mainBody.gameObject;
			blastAttack.inflictor = blastAttack.attacker;
			blastAttack.baseDamage = Stomp.blastDamageCoefficient * mainBody.damage;
			blastAttack.baseForce = Stomp.blastForce;
			blastAttack.position = vector;
			blastAttack.radius = Stomp.blastRadius;
			blastAttack.falloffModel = BlastAttack.FalloffModel.Linear;
			blastAttack.teamIndex = mainBody.teamComponent.teamIndex;
			blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
			blastAttack.damageType = DamageType.Generic;
			blastAttack.crit = mainBody.RollCrit();
			blastAttack.damageColorIndex = DamageColorIndex.Default;
			blastAttack.impactEffect = EffectIndex.Invalid;
			blastAttack.losType = BlastAttack.LoSType.None;
			blastAttack.procChainMask = default(ProcChainMask);
			blastAttack.procCoefficient = 1f;
			blastAttack.Fire();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018454 File Offset: 0x00016654
		[CompilerGenerated]
		private void <TryStompCollisionAuthority>g__TryAttack|8_0(ref Stomp.<>c__DisplayClass8_0 A_1)
		{
			if (this.previousToePosition == null)
			{
				return;
			}
			Vector3 value = this.previousToePosition.Value;
			if ((A_1.currentToePosition - value).sqrMagnitude < 0.0025000002f)
			{
				return;
			}
			RaycastHit raycastHit;
			if (!Physics.Linecast(this.previousToePosition.Value, A_1.currentToePosition, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				return;
			}
			this.DoBlastAuthority(raycastHit.point);
			base.legController.DoToeConcussionBlastAuthority(new Vector3?(raycastHit.point), false);
			this.hasDoneBlast = true;
		}

		// Token: 0x040006AD RID: 1709
		public static float blastDamageCoefficient;

		// Token: 0x040006AE RID: 1710
		public static float blastRadius;

		// Token: 0x040006AF RID: 1711
		public static float blastForce;

		// Token: 0x040006B0 RID: 1712
		public static GameObject blastEffectPrefab;

		// Token: 0x040006B1 RID: 1713
		private Vector3? previousToePosition;

		// Token: 0x040006B2 RID: 1714
		private bool hasDoneBlast;
	}
}
