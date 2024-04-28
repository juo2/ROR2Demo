using System;
using System.Collections.Generic;
using System.Linq;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BB0 RID: 2992
	[RequireComponent(typeof(ProjectileDamage))]
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileProximityBeamController : MonoBehaviour
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x0011B7FB File Offset: 0x001199FB
		private TeamIndex myTeamIndex
		{
			get
			{
				if (!this.teamFilter)
				{
					return TeamIndex.Neutral;
				}
				return this.teamFilter.teamIndex;
			}
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x0011B818 File Offset: 0x00119A18
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.projectileController = base.GetComponent<ProjectileController>();
				this.teamFilter = this.projectileController.teamFilter;
				this.projectileDamage = base.GetComponent<ProjectileDamage>();
				this.attackTimer = 0f;
				this.previousTargets = new List<HealthComponent>();
				this.search = new BullseyeSearch();
				return;
			}
			base.enabled = false;
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x0011B87E File Offset: 0x00119A7E
		private void ClearList()
		{
			this.previousTargets.Clear();
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x0011B88B File Offset: 0x00119A8B
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.UpdateServer();
				return;
			}
			base.enabled = false;
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x0011B8A4 File Offset: 0x00119AA4
		private void UpdateServer()
		{
			this.listClearTimer -= Time.fixedDeltaTime;
			if (this.listClearTimer <= 0f)
			{
				this.ClearList();
				this.listClearTimer = this.listClearInterval;
			}
			this.attackTimer -= Time.fixedDeltaTime;
			if (this.attackTimer <= 0f)
			{
				this.attackTimer += this.attackInterval;
				Vector3 position = base.transform.position;
				Vector3 forward = base.transform.forward;
				for (int i = 0; i < this.attackFireCount; i++)
				{
					HurtBox hurtBox = this.FindNextTarget(position, forward);
					if (hurtBox)
					{
						this.previousTargets.Add(hurtBox.healthComponent);
						LightningOrb lightningOrb = new LightningOrb();
						lightningOrb.bouncedObjects = new List<HealthComponent>();
						lightningOrb.attacker = this.projectileController.owner;
						lightningOrb.inflictor = base.gameObject;
						lightningOrb.teamIndex = this.myTeamIndex;
						lightningOrb.damageValue = this.projectileDamage.damage * this.damageCoefficient;
						lightningOrb.isCrit = this.projectileDamage.crit;
						lightningOrb.origin = position;
						lightningOrb.bouncesRemaining = this.bounces;
						lightningOrb.lightningType = this.lightningType;
						lightningOrb.procCoefficient = this.procCoefficient;
						lightningOrb.target = hurtBox;
						lightningOrb.damageColorIndex = this.projectileDamage.damageColorIndex;
						if (this.inheritDamageType)
						{
							lightningOrb.damageType = this.projectileDamage.damageType;
						}
						OrbManager.instance.AddOrb(lightningOrb);
					}
				}
			}
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x0011BA44 File Offset: 0x00119C44
		public HurtBox FindNextTarget(Vector3 position, Vector3 forward)
		{
			this.search.searchOrigin = position;
			this.search.searchDirection = forward;
			this.search.sortMode = BullseyeSearch.SortMode.Distance;
			this.search.teamMaskFilter = TeamMask.allButNeutral;
			this.search.teamMaskFilter.RemoveTeam(this.myTeamIndex);
			this.search.filterByLoS = false;
			this.search.minAngleFilter = this.minAngleFilter;
			this.search.maxAngleFilter = this.maxAngleFilter;
			this.search.maxDistanceFilter = this.attackRange;
			this.search.RefreshCandidates();
			return this.search.GetResults().FirstOrDefault((HurtBox hurtBox) => !this.previousTargets.Contains(hurtBox.healthComponent));
		}

		// Token: 0x04004289 RID: 17033
		private ProjectileController projectileController;

		// Token: 0x0400428A RID: 17034
		private ProjectileDamage projectileDamage;

		// Token: 0x0400428B RID: 17035
		private List<HealthComponent> previousTargets;

		// Token: 0x0400428C RID: 17036
		private TeamFilter teamFilter;

		// Token: 0x0400428D RID: 17037
		public int attackFireCount = 1;

		// Token: 0x0400428E RID: 17038
		public float attackInterval = 1f;

		// Token: 0x0400428F RID: 17039
		public float listClearInterval = 3f;

		// Token: 0x04004290 RID: 17040
		public float attackRange = 20f;

		// Token: 0x04004291 RID: 17041
		[Range(0f, 180f)]
		public float minAngleFilter;

		// Token: 0x04004292 RID: 17042
		[Range(0f, 180f)]
		public float maxAngleFilter = 180f;

		// Token: 0x04004293 RID: 17043
		public float procCoefficient = 0.1f;

		// Token: 0x04004294 RID: 17044
		public float damageCoefficient = 1f;

		// Token: 0x04004295 RID: 17045
		public int bounces;

		// Token: 0x04004296 RID: 17046
		public bool inheritDamageType;

		// Token: 0x04004297 RID: 17047
		public LightningOrb.LightningType lightningType = LightningOrb.LightningType.BFG;

		// Token: 0x04004298 RID: 17048
		private float attackTimer;

		// Token: 0x04004299 RID: 17049
		private float listClearTimer;

		// Token: 0x0400429A RID: 17050
		private BullseyeSearch search;
	}
}
