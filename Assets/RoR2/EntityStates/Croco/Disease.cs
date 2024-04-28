using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003D9 RID: 985
	public class Disease : BaseState
	{
		// Token: 0x0600119D RID: 4509 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Disease.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			Transform transform = base.FindModelChild(Disease.muzzleString);
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.maxDistanceFilter = Disease.orbRange;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(TeamComponent.GetObjectTeam(base.gameObject));
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
			bullseyeSearch.RefreshCandidates();
			EffectManager.SimpleMuzzleFlash(Disease.muzzleflashEffectPrefab, base.gameObject, Disease.muzzleString, true);
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			if (list.Count > 0)
			{
				Debug.LogFormat("Shooting at {0}", new object[]
				{
					list[0]
				});
				HurtBox target = list.FirstOrDefault<HurtBox>();
				LightningOrb lightningOrb = new LightningOrb();
				lightningOrb.attacker = base.gameObject;
				lightningOrb.bouncedObjects = new List<HealthComponent>();
				lightningOrb.lightningType = LightningOrb.LightningType.CrocoDisease;
				lightningOrb.damageType = DamageType.PoisonOnHit;
				lightningOrb.damageValue = this.damageStat * Disease.damageCoefficient;
				lightningOrb.isCrit = base.RollCrit();
				lightningOrb.procCoefficient = Disease.procCoefficient;
				lightningOrb.bouncesRemaining = Disease.maxBounces;
				lightningOrb.origin = transform.position;
				lightningOrb.target = target;
				lightningOrb.teamIndex = base.GetTeam();
				lightningOrb.range = Disease.bounceRange;
				OrbManager.instance.AddOrb(lightningOrb);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0004DA74 File Offset: 0x0004BC74
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400164F RID: 5711
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04001650 RID: 5712
		public static string muzzleString;

		// Token: 0x04001651 RID: 5713
		public static float orbRange;

		// Token: 0x04001652 RID: 5714
		public static float baseDuration;

		// Token: 0x04001653 RID: 5715
		public static float damageCoefficient;

		// Token: 0x04001654 RID: 5716
		public static int maxBounces;

		// Token: 0x04001655 RID: 5717
		public static float bounceRange;

		// Token: 0x04001656 RID: 5718
		public static float procCoefficient;

		// Token: 0x04001657 RID: 5719
		private float duration;
	}
}
