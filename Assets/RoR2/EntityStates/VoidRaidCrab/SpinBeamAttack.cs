using System;
using RoR2;
using RoR2.Audio;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000125 RID: 293
	public class SpinBeamAttack : BaseSpinBeamAttackState
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00016173 File Offset: 0x00014373
		public override void OnEnter()
		{
			base.OnEnter();
			base.CreateBeamVFXInstance(SpinBeamAttack.beamVfxPrefab);
			this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, SpinBeamAttack.loopSound);
			Util.PlaySound(SpinBeamAttack.enterSoundString, base.gameObject);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000161AD File Offset: 0x000143AD
		public override void OnExit()
		{
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			base.OnExit();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000161C0 File Offset: 0x000143C0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= base.duration && base.isAuthority)
			{
				this.outer.SetNextState(new SpinBeamWindDown());
			}
			if (base.isAuthority)
			{
				if (this.beamTickTimer <= 0f)
				{
					this.beamTickTimer += 1f / SpinBeamAttack.beamTickFrequency;
					this.FireBeamBulletAuthority();
				}
				this.beamTickTimer -= Time.fixedDeltaTime;
			}
			base.SetHeadYawRevolutions(SpinBeamAttack.revolutionsCurve.Evaluate(base.normalizedFixedAge));
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00016254 File Offset: 0x00014454
		private void FireBeamBulletAuthority()
		{
			Ray beamRay = base.GetBeamRay();
			new BulletAttack
			{
				muzzleName = BaseSpinBeamAttackState.muzzleTransformNameInChildLocator,
				origin = beamRay.origin,
				aimVector = beamRay.direction,
				minSpread = 0f,
				maxSpread = 0f,
				maxDistance = 400f,
				hitMask = LayerIndex.CommonMasks.bullet,
				stopperMask = 0,
				bulletCount = 1U,
				radius = SpinBeamAttack.beamRadius,
				smartCollision = false,
				queryTriggerInteraction = QueryTriggerInteraction.Ignore,
				procCoefficient = 1f,
				procChainMask = default(ProcChainMask),
				owner = base.gameObject,
				weapon = base.gameObject,
				damage = SpinBeamAttack.beamDpsCoefficient * this.damageStat / SpinBeamAttack.beamTickFrequency,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				falloffModel = BulletAttack.FalloffModel.None,
				force = 0f,
				hitEffectPrefab = SpinBeamAttack.beamImpactEffectPrefab,
				tracerEffectPrefab = null,
				isCrit = false,
				HitEffectNormal = false
			}.Fire();
		}

		// Token: 0x04000609 RID: 1545
		public static AnimationCurve revolutionsCurve;

		// Token: 0x0400060A RID: 1546
		public static GameObject beamVfxPrefab;

		// Token: 0x0400060B RID: 1547
		public static float beamRadius = 16f;

		// Token: 0x0400060C RID: 1548
		public static float beamMaxDistance = 400f;

		// Token: 0x0400060D RID: 1549
		public static float beamDpsCoefficient = 1f;

		// Token: 0x0400060E RID: 1550
		public static float beamTickFrequency = 4f;

		// Token: 0x0400060F RID: 1551
		public static GameObject beamImpactEffectPrefab;

		// Token: 0x04000610 RID: 1552
		public static LoopSoundDef loopSound;

		// Token: 0x04000611 RID: 1553
		public static string enterSoundString;

		// Token: 0x04000612 RID: 1554
		private float beamTickTimer;

		// Token: 0x04000613 RID: 1555
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
