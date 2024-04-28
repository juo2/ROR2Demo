using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x0200012E RID: 302
	public abstract class BaseFireMultiBeam : BaseMultiBeamState
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x00016C6C File Offset: 0x00014E6C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, BaseMultiBeamState.muzzleName, false);
			}
			Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
			if (base.isAuthority)
			{
				Ray ray;
				Vector3 vector;
				base.CalcBeamPath(out ray, out vector);
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * this.blastDamageCoefficient,
					baseForce = this.blastForceMagnitude,
					position = vector,
					radius = this.blastRadius,
					falloffModel = BlastAttack.FalloffModel.SweetSpot,
					bonusForce = this.blastBonusForce,
					damageType = DamageType.Generic
				}.Fire();
				if (modelTransform)
				{
					ChildLocator component = modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						int childIndex = component.FindChildIndex(BaseMultiBeamState.muzzleName);
						if (this.tracerEffectPrefab)
						{
							EffectData effectData = new EffectData
							{
								origin = vector,
								start = ray.origin,
								scale = this.blastRadius
							};
							effectData.SetChildLocatorTransformReference(base.gameObject, childIndex);
							EffectManager.SpawnEffect(this.tracerEffectPrefab, effectData, true);
							EffectManager.SpawnEffect(this.explosionEffectPrefab, effectData, true);
						}
					}
				}
				this.OnFireBeam(ray.origin, vector);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00016E1C File Offset: 0x0001501C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				EntityState nextState = this.InstantiateNextState();
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x0600055A RID: 1370
		protected abstract EntityState InstantiateNextState();

		// Token: 0x0600055B RID: 1371 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnFireBeam(Vector3 beamStart, Vector3 beamEnd)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000638 RID: 1592
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000639 RID: 1593
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400063A RID: 1594
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400063B RID: 1595
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400063C RID: 1596
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x0400063D RID: 1597
		[SerializeField]
		public GameObject tracerEffectPrefab;

		// Token: 0x0400063E RID: 1598
		[SerializeField]
		public GameObject explosionEffectPrefab;

		// Token: 0x0400063F RID: 1599
		[SerializeField]
		public float blastDamageCoefficient;

		// Token: 0x04000640 RID: 1600
		[SerializeField]
		public float blastForceMagnitude;

		// Token: 0x04000641 RID: 1601
		[SerializeField]
		public float blastRadius;

		// Token: 0x04000642 RID: 1602
		[SerializeField]
		public Vector3 blastBonusForce;

		// Token: 0x04000643 RID: 1603
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000644 RID: 1604
		private float duration;
	}
}
