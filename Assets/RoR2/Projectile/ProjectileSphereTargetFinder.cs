using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2.Projectile
{
	// Token: 0x02000BB5 RID: 2997
	[RequireComponent(typeof(ProjectileTargetComponent))]
	[RequireComponent(typeof(TeamFilter))]
	public class ProjectileSphereTargetFinder : MonoBehaviour
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x0011C1EC File Offset: 0x0011A3EC
		private void Start()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
				return;
			}
			this.transform = base.transform;
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.targetComponent = base.GetComponent<ProjectileTargetComponent>();
			this.sphereSearch = new SphereSearch();
			this.searchTimer = 0f;
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x0011C244 File Offset: 0x0011A444
		private void FixedUpdate()
		{
			this.searchTimer -= Time.fixedDeltaTime;
			if (this.searchTimer <= 0f)
			{
				this.searchTimer += this.targetSearchInterval;
				if (this.allowTargetLoss && this.targetComponent.target != null && this.lastFoundTransform == this.targetComponent.target && !this.PassesFilters(this.lastFoundHurtBox))
				{
					this.SetTarget(null);
				}
				if (!this.onlySearchIfNoTarget || this.targetComponent.target == null)
				{
					this.SearchForTarget();
				}
				this.hasTarget = (this.targetComponent.target != null);
				if (this.hadTargetLastUpdate != this.hasTarget)
				{
					if (this.hasTarget)
					{
						UnityEvent unityEvent = this.onNewTargetFound;
						if (unityEvent != null)
						{
							unityEvent.Invoke();
						}
					}
					else
					{
						UnityEvent unityEvent2 = this.onTargetLost;
						if (unityEvent2 != null)
						{
							unityEvent2.Invoke();
						}
					}
				}
				this.hadTargetLastUpdate = this.hasTarget;
			}
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x0011C344 File Offset: 0x0011A544
		private bool PassesFilters(HurtBox result)
		{
			CharacterBody body = result.healthComponent.body;
			return body && (!this.ignoreAir || !body.isFlying) && (!body.isFlying || float.IsInfinity(this.flierAltitudeTolerance) || this.flierAltitudeTolerance >= Mathf.Abs(result.transform.position.y - this.transform.position.y));
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x0011C3C0 File Offset: 0x0011A5C0
		private void SearchForTarget()
		{
			this.sphereSearch.origin = this.transform.position;
			this.sphereSearch.radius = this.lookRange;
			this.sphereSearch.mask = LayerIndex.entityPrecise.mask;
			this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			this.sphereSearch.RefreshCandidates();
			this.sphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(this.teamFilter.teamIndex));
			this.sphereSearch.OrderCandidatesByDistance();
			this.sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			this.sphereSearch.GetHurtBoxes(ProjectileSphereTargetFinder.foundHurtBoxes);
			HurtBox target = null;
			if (ProjectileSphereTargetFinder.foundHurtBoxes.Count > 0)
			{
				int i = 0;
				int count = ProjectileSphereTargetFinder.foundHurtBoxes.Count;
				while (i < count)
				{
					if (this.PassesFilters(ProjectileSphereTargetFinder.foundHurtBoxes[i]))
					{
						target = ProjectileSphereTargetFinder.foundHurtBoxes[i];
						break;
					}
					i++;
				}
				ProjectileSphereTargetFinder.foundHurtBoxes.Clear();
			}
			this.SetTarget(target);
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x0011C4C0 File Offset: 0x0011A6C0
		private void SetTarget(HurtBox hurtBox)
		{
			this.lastFoundHurtBox = hurtBox;
			this.lastFoundTransform = ((hurtBox != null) ? hurtBox.transform : null);
			this.targetComponent.target = this.lastFoundTransform;
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x0011C4EC File Offset: 0x0011A6EC
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Vector3 position = base.transform.position;
			Gizmos.DrawWireSphere(position, this.lookRange);
			if (!float.IsInfinity(this.flierAltitudeTolerance))
			{
				Gizmos.DrawWireCube(position, new Vector3(this.lookRange * 2f, this.flierAltitudeTolerance * 2f, this.lookRange * 2f));
			}
		}

		// Token: 0x040042B8 RID: 17080
		[Tooltip("How far ahead the projectile should look to find a target.")]
		public float lookRange;

		// Token: 0x040042B9 RID: 17081
		[Tooltip("How long before searching for a target.")]
		public float targetSearchInterval = 0.5f;

		// Token: 0x040042BA RID: 17082
		[Tooltip("Will not search for new targets once it has one.")]
		public bool onlySearchIfNoTarget;

		// Token: 0x040042BB RID: 17083
		[Tooltip("Allows the target to be lost if it's outside the acceptable range.")]
		public bool allowTargetLoss;

		// Token: 0x040042BC RID: 17084
		[Tooltip("If set, targets can only be found when there is a free line of sight.")]
		public bool testLoS;

		// Token: 0x040042BD RID: 17085
		[Tooltip("Whether or not airborne characters should be ignored.")]
		public bool ignoreAir;

		// Token: 0x040042BE RID: 17086
		[Tooltip("The difference in altitude at which a result will be ignored.")]
		[FormerlySerializedAs("altitudeTolerance")]
		public float flierAltitudeTolerance = float.PositiveInfinity;

		// Token: 0x040042BF RID: 17087
		public UnityEvent onNewTargetFound;

		// Token: 0x040042C0 RID: 17088
		public UnityEvent onTargetLost;

		// Token: 0x040042C1 RID: 17089
		private new Transform transform;

		// Token: 0x040042C2 RID: 17090
		private TeamFilter teamFilter;

		// Token: 0x040042C3 RID: 17091
		private ProjectileTargetComponent targetComponent;

		// Token: 0x040042C4 RID: 17092
		private float searchTimer;

		// Token: 0x040042C5 RID: 17093
		private SphereSearch sphereSearch;

		// Token: 0x040042C6 RID: 17094
		private bool hasTarget;

		// Token: 0x040042C7 RID: 17095
		private bool hadTargetLastUpdate;

		// Token: 0x040042C8 RID: 17096
		private HurtBox lastFoundHurtBox;

		// Token: 0x040042C9 RID: 17097
		private Transform lastFoundTransform;

		// Token: 0x040042CA RID: 17098
		private static readonly List<HurtBox> foundHurtBoxes = new List<HurtBox>();
	}
}
