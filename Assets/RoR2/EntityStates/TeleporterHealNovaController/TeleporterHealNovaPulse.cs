using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TeleporterHealNovaController
{
	// Token: 0x020001B2 RID: 434
	public class TeleporterHealNovaPulse : BaseState
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x000213F4 File Offset: 0x0001F5F4
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.transform.parent)
			{
				HoldoutZoneController componentInParent = base.gameObject.GetComponentInParent<HoldoutZoneController>();
				if (componentInParent)
				{
					this.radius = componentInParent.currentRadius;
				}
			}
			TeamFilter component = base.GetComponent<TeamFilter>();
			TeamIndex teamIndex = component ? component.teamIndex : TeamIndex.None;
			if (NetworkServer.active)
			{
				this.healPulse = new TeleporterHealNovaPulse.HealPulse(base.transform.position, this.radius, 0.5f, TeleporterHealNovaPulse.duration, teamIndex);
			}
			this.effectTransform = base.transform.Find("PulseEffect");
			if (this.effectTransform)
			{
				this.effectTransform.gameObject.SetActive(true);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public override void OnExit()
		{
			if (this.effectTransform)
			{
				this.effectTransform.gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000214DA File Offset: 0x0001F6DA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.healPulse.Update(Time.fixedDeltaTime);
				if (TeleporterHealNovaPulse.duration < base.fixedAge)
				{
					EntityState.Destroy(this.outer.gameObject);
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00021518 File Offset: 0x0001F718
		public override void Update()
		{
			if (this.effectTransform)
			{
				float num = this.radius * TeleporterHealNovaPulse.novaRadiusCurve.Evaluate(base.fixedAge / TeleporterHealNovaPulse.duration);
				this.effectTransform.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x04000958 RID: 2392
		public static AnimationCurve novaRadiusCurve;

		// Token: 0x04000959 RID: 2393
		public static float duration;

		// Token: 0x0400095A RID: 2394
		private Transform effectTransform;

		// Token: 0x0400095B RID: 2395
		private TeleporterHealNovaPulse.HealPulse healPulse;

		// Token: 0x0400095C RID: 2396
		private float radius;

		// Token: 0x020001B3 RID: 435
		private class HealPulse
		{
			// Token: 0x060007D0 RID: 2000 RVA: 0x00021564 File Offset: 0x0001F764
			public HealPulse(Vector3 origin, float finalRadius, float healFractionValue, float duration, TeamIndex teamIndex)
			{
				this.sphereSearch = new SphereSearch
				{
					mask = LayerIndex.entityPrecise.mask,
					origin = origin,
					queryTriggerInteraction = QueryTriggerInteraction.Collide,
					radius = 0f
				};
				this.finalRadius = finalRadius;
				this.healFractionValue = healFractionValue;
				this.rate = 1f / duration;
				this.teamMask = default(TeamMask);
				this.teamMask.AddTeam(teamIndex);
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000215F9 File Offset: 0x0001F7F9
			public bool isFinished
			{
				get
				{
					return this.t >= 1f;
				}
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x0002160C File Offset: 0x0001F80C
			public void Update(float deltaTime)
			{
				this.t += this.rate * deltaTime;
				this.t = ((this.t > 1f) ? 1f : this.t);
				this.sphereSearch.radius = this.finalRadius * TeleporterHealNovaPulse.novaRadiusCurve.Evaluate(this.t);
				this.sphereSearch.RefreshCandidates().FilterCandidatesByHurtBoxTeam(this.teamMask).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes(this.hurtBoxesList);
				int i = 0;
				int count = this.hurtBoxesList.Count;
				while (i < count)
				{
					HealthComponent healthComponent = this.hurtBoxesList[i].healthComponent;
					if (!this.healedTargets.Contains(healthComponent))
					{
						this.healedTargets.Add(healthComponent);
						this.HealTarget(healthComponent);
					}
					i++;
				}
				this.hurtBoxesList.Clear();
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x000216EC File Offset: 0x0001F8EC
			private void HealTarget(HealthComponent target)
			{
				target.HealFraction(this.healFractionValue, default(ProcChainMask));
				Util.PlaySound("Play_item_proc_TPhealingNova_hitPlayer", target.gameObject);
			}

			// Token: 0x0400095D RID: 2397
			private readonly List<HealthComponent> healedTargets = new List<HealthComponent>();

			// Token: 0x0400095E RID: 2398
			private readonly SphereSearch sphereSearch;

			// Token: 0x0400095F RID: 2399
			private float rate;

			// Token: 0x04000960 RID: 2400
			private float t;

			// Token: 0x04000961 RID: 2401
			private float finalRadius;

			// Token: 0x04000962 RID: 2402
			private float healFractionValue;

			// Token: 0x04000963 RID: 2403
			private TeamMask teamMask;

			// Token: 0x04000964 RID: 2404
			private readonly List<HurtBox> hurtBoxesList = new List<HurtBox>();
		}
	}
}
