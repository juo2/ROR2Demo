using System;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Squid.SquidWeapon
{
	// Token: 0x020001C2 RID: 450
	public class FireSpine : BaseState
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00022078 File Offset: 0x00020278
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireSpine.baseDuration / this.attackSpeedStat;
			base.GetAimRay();
			this.PlayAnimation("Gesture", "FireGoo");
			if (base.isAuthority)
			{
				this.FireOrbArrow();
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000220B8 File Offset: 0x000202B8
		private void FireOrbArrow()
		{
			if (this.hasFiredArrow || !NetworkServer.active)
			{
				return;
			}
			Ray aimRay = base.GetAimRay();
			this.enemyFinder = new BullseyeSearch();
			this.enemyFinder.viewer = base.characterBody;
			this.enemyFinder.maxDistanceFilter = float.PositiveInfinity;
			this.enemyFinder.searchOrigin = aimRay.origin;
			this.enemyFinder.searchDirection = aimRay.direction;
			this.enemyFinder.sortMode = BullseyeSearch.SortMode.Distance;
			this.enemyFinder.teamMaskFilter = TeamMask.allButNeutral;
			this.enemyFinder.minDistanceFilter = 0f;
			this.enemyFinder.maxAngleFilter = (this.fullVision ? 180f : 90f);
			this.enemyFinder.filterByLoS = true;
			if (base.teamComponent)
			{
				this.enemyFinder.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
			}
			this.enemyFinder.RefreshCandidates();
			HurtBox hurtBox = this.enemyFinder.GetResults().FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				Vector3 vector = hurtBox.transform.position - base.GetAimRay().origin;
				aimRay.origin = base.GetAimRay().origin;
				aimRay.direction = vector;
				base.inputBank.aimDirection = vector;
				base.StartAimMode(aimRay, 2f, false);
				this.hasFiredArrow = true;
				SquidOrb squidOrb = new SquidOrb();
				squidOrb.forceScalar = FireSpine.forceScalar;
				squidOrb.damageValue = base.characterBody.damage * FireSpine.damageCoefficient;
				squidOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
				squidOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
				squidOrb.attacker = base.gameObject;
				squidOrb.procCoefficient = FireSpine.procCoefficient;
				HurtBox hurtBox2 = hurtBox;
				if (hurtBox2)
				{
					Transform transform = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Muzzle");
					EffectManager.SimpleMuzzleFlash(FireSpine.muzzleflashEffectPrefab, base.gameObject, "Muzzle", true);
					squidOrb.origin = transform.position;
					squidOrb.target = hurtBox2;
					OrbManager.instance.AddOrb(squidOrb);
				}
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00022304 File Offset: 0x00020504
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000978 RID: 2424
		public static GameObject hitEffectPrefab;

		// Token: 0x04000979 RID: 2425
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x0400097A RID: 2426
		public static float damageCoefficient;

		// Token: 0x0400097B RID: 2427
		public static float baseDuration = 2f;

		// Token: 0x0400097C RID: 2428
		public static float procCoefficient = 1f;

		// Token: 0x0400097D RID: 2429
		public static float forceScalar = 1f;

		// Token: 0x0400097E RID: 2430
		private bool hasFiredArrow;

		// Token: 0x0400097F RID: 2431
		private ChildLocator childLocator;

		// Token: 0x04000980 RID: 2432
		private BullseyeSearch enemyFinder;

		// Token: 0x04000981 RID: 2433
		private const float maxVisionDistance = float.PositiveInfinity;

		// Token: 0x04000982 RID: 2434
		public bool fullVision = true;

		// Token: 0x04000983 RID: 2435
		private float duration;
	}
}
