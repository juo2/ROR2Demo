using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F1 RID: 1009
	public class FireSweepBarrage : BaseState
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x00050A84 File Offset: 0x0004EC84
		public override void OnEnter()
		{
			base.OnEnter();
			this.totalDuration = FireSweepBarrage.baseTotalDuration / this.attackSpeedStat;
			this.firingDuration = FireSweepBarrage.baseFiringDuration / this.attackSpeedStat;
			base.characterBody.SetAimTimer(3f);
			base.PlayAnimation("Gesture, Additive", "FireSweepBarrage", "FireSweepBarrage.playbackRate", this.totalDuration);
			base.PlayAnimation("Gesture, Override", "FireSweepBarrage", "FireSweepBarrage.playbackRate", this.totalDuration);
			Util.PlaySound(FireSweepBarrage.enterSound, base.gameObject);
			Ray aimRay = base.GetAimRay();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
			bullseyeSearch.maxAngleFilter = FireSweepBarrage.fieldOfView * 0.5f;
			bullseyeSearch.maxDistanceFilter = FireSweepBarrage.maxDistance;
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.RefreshCandidates();
			this.targetHurtboxes = bullseyeSearch.GetResults().Where(new Func<HurtBox, bool>(Util.IsValid)).Distinct(default(HurtBox.EntityEqualityComparer)).ToList<HurtBox>();
			this.totalBulletsToFire = Mathf.Max(this.targetHurtboxes.Count, FireSweepBarrage.minimumFireCount);
			this.timeBetweenBullets = this.firingDuration / (float)this.totalBulletsToFire;
			this.childLocator = base.GetModelTransform().GetComponent<ChildLocator>();
			this.muzzleIndex = this.childLocator.FindChildIndex(FireSweepBarrage.muzzle);
			this.muzzleTransform = this.childLocator.FindChild(this.muzzleIndex);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00050C1C File Offset: 0x0004EE1C
		private void Fire()
		{
			if (this.totalBulletsFired < this.totalBulletsToFire)
			{
				if (!string.IsNullOrEmpty(FireSweepBarrage.muzzle))
				{
					EffectManager.SimpleMuzzleFlash(FireSweepBarrage.muzzleEffectPrefab, base.gameObject, FireSweepBarrage.muzzle, false);
				}
				Util.PlaySound(FireSweepBarrage.fireSoundString, base.gameObject);
				this.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
				if (NetworkServer.active && this.targetHurtboxes.Count > 0)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = this.damageStat * FireSweepBarrage.damageCoefficient;
					damageInfo.attacker = base.gameObject;
					damageInfo.procCoefficient = FireSweepBarrage.procCoefficient;
					damageInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
					if (this.targetHurtboxIndex >= this.targetHurtboxes.Count)
					{
						this.targetHurtboxIndex = 0;
					}
					HurtBox hurtBox = this.targetHurtboxes[this.targetHurtboxIndex];
					if (hurtBox)
					{
						HealthComponent healthComponent = hurtBox.healthComponent;
						if (healthComponent)
						{
							this.targetHurtboxIndex++;
							Vector3 normalized = (hurtBox.transform.position - base.characterBody.corePosition).normalized;
							damageInfo.force = FireSweepBarrage.force * normalized;
							damageInfo.position = hurtBox.transform.position;
							EffectManager.SimpleImpactEffect(FireSweepBarrage.impactEffectPrefab, hurtBox.transform.position, normalized, true);
							healthComponent.TakeDamage(damageInfo);
							GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
						}
						if (FireSweepBarrage.tracerEffectPrefab && this.childLocator)
						{
							int childIndex = this.childLocator.FindChildIndex(FireSweepBarrage.muzzle);
							this.childLocator.FindChild(childIndex);
							EffectData effectData = new EffectData
							{
								origin = hurtBox.transform.position,
								start = this.muzzleTransform.position
							};
							effectData.SetChildLocatorTransformReference(base.gameObject, childIndex);
							EffectManager.SpawnEffect(FireSweepBarrage.tracerEffectPrefab, effectData, true);
						}
					}
				}
				this.totalBulletsFired++;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00050E3C File Offset: 0x0004F03C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				this.Fire();
				this.fireTimer += this.timeBetweenBullets;
			}
			if (base.fixedAge >= this.totalDuration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001732 RID: 5938
		public static string enterSound;

		// Token: 0x04001733 RID: 5939
		public static string muzzle;

		// Token: 0x04001734 RID: 5940
		public static string fireSoundString;

		// Token: 0x04001735 RID: 5941
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04001736 RID: 5942
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001737 RID: 5943
		public static float baseTotalDuration;

		// Token: 0x04001738 RID: 5944
		public static float baseFiringDuration;

		// Token: 0x04001739 RID: 5945
		public static float fieldOfView;

		// Token: 0x0400173A RID: 5946
		public static float maxDistance;

		// Token: 0x0400173B RID: 5947
		public static float damageCoefficient;

		// Token: 0x0400173C RID: 5948
		public static float procCoefficient;

		// Token: 0x0400173D RID: 5949
		public static float force;

		// Token: 0x0400173E RID: 5950
		public static int minimumFireCount;

		// Token: 0x0400173F RID: 5951
		public static GameObject impactEffectPrefab;

		// Token: 0x04001740 RID: 5952
		private float totalDuration;

		// Token: 0x04001741 RID: 5953
		private float firingDuration;

		// Token: 0x04001742 RID: 5954
		private int totalBulletsToFire;

		// Token: 0x04001743 RID: 5955
		private int totalBulletsFired;

		// Token: 0x04001744 RID: 5956
		private int targetHurtboxIndex;

		// Token: 0x04001745 RID: 5957
		private float timeBetweenBullets;

		// Token: 0x04001746 RID: 5958
		private List<HurtBox> targetHurtboxes = new List<HurtBox>();

		// Token: 0x04001747 RID: 5959
		private float fireTimer;

		// Token: 0x04001748 RID: 5960
		private ChildLocator childLocator;

		// Token: 0x04001749 RID: 5961
		private int muzzleIndex;

		// Token: 0x0400174A RID: 5962
		private Transform muzzleTransform;
	}
}
