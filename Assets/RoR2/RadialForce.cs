using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200083A RID: 2106
	public class RadialForce : MonoBehaviour
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000C3741 File Offset: 0x000C1941
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x000C3749 File Offset: 0x000C1949
		private protected new Transform transform { protected get; private set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000C3752 File Offset: 0x000C1952
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x000C375A File Offset: 0x000C195A
		private protected TeamFilter teamFilter { protected get; private set; }

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000C3763 File Offset: 0x000C1963
		protected void Awake()
		{
			this.transform = base.GetComponent<Transform>();
			this.teamFilter = base.GetComponent<TeamFilter>();
			this.sphereSearch = new SphereSearch();
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000C3788 File Offset: 0x000C1988
		protected void FixedUpdate()
		{
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			this.SearchForTargets(list);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				this.ApplyPullToHurtBox(list[i]);
				i++;
			}
			if (this.tetherVfxOrigin)
			{
				List<Transform> list2 = CollectionPool<Transform, List<Transform>>.RentCollection();
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					HurtBox hurtBox = list[j];
					if (hurtBox)
					{
						Transform item = hurtBox.transform;
						HealthComponent healthComponent = hurtBox.healthComponent;
						if (healthComponent)
						{
							Transform coreTransform = healthComponent.body.coreTransform;
							if (coreTransform)
							{
								item = coreTransform;
							}
						}
						list2.Add(item);
					}
					j++;
				}
				this.tetherVfxOrigin.SetTetheredTransforms(list2);
				CollectionPool<Transform, List<Transform>>.ReturnCollection(list2);
			}
			CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x000C385C File Offset: 0x000C1A5C
		protected void SearchForTargets(List<HurtBox> dest)
		{
			this.sphereSearch.mask = LayerIndex.entityPrecise.mask;
			this.sphereSearch.origin = this.transform.position;
			this.sphereSearch.radius = this.radius;
			this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			this.sphereSearch.RefreshCandidates();
			this.sphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(this.teamFilter.teamIndex));
			this.sphereSearch.OrderCandidatesByDistance();
			this.sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			this.sphereSearch.GetHurtBoxes(dest);
			this.sphereSearch.ClearCandidates();
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000C390C File Offset: 0x000C1B0C
		protected void ApplyPullToHurtBox(HurtBox hurtBox)
		{
			if (!hurtBox)
			{
				return;
			}
			HealthComponent healthComponent = hurtBox.healthComponent;
			if (healthComponent && healthComponent.body && hurtBox.transform && this.transform && NetworkServer.active)
			{
				CharacterMotor characterMotor = healthComponent.body.characterMotor;
				Vector3 a = hurtBox.transform.position - this.transform.position;
				float num = 1f - Mathf.Clamp(a.magnitude / this.radius, 0f, 1f - this.forceCoefficientAtEdge);
				a = a.normalized * this.forceMagnitude * (1f - num);
				Vector3 a2 = Vector3.zero;
				float d = 0f;
				if (characterMotor)
				{
					a2 = characterMotor.velocity;
					d = characterMotor.mass;
				}
				else
				{
					Rigidbody rigidbody = healthComponent.body.rigidbody;
					if (rigidbody)
					{
						a2 = rigidbody.velocity;
						d = rigidbody.mass;
					}
				}
				a2.y += Physics.gravity.y * Time.fixedDeltaTime;
				healthComponent.TakeDamageForce(a - a2 * this.damping * d * num, true, false);
			}
		}

		// Token: 0x04002FDA RID: 12250
		public float radius;

		// Token: 0x04002FDB RID: 12251
		public float damping = 0.2f;

		// Token: 0x04002FDC RID: 12252
		public float forceMagnitude;

		// Token: 0x04002FDD RID: 12253
		public float forceCoefficientAtEdge = 0.5f;

		// Token: 0x04002FDE RID: 12254
		public TetherVfxOrigin tetherVfxOrigin;

		// Token: 0x04002FE1 RID: 12257
		private SphereSearch sphereSearch;
	}
}
