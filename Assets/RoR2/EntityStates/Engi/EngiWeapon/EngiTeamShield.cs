using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A6 RID: 934
	public class EngiTeamShield : BaseState
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x000493E0 File Offset: 0x000475E0
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.teamComponent && NetworkServer.active)
			{
				ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(base.teamComponent.teamIndex);
				float num = EngiTeamShield.radius * EngiTeamShield.radius;
				Vector3 position = base.transform.position;
				for (int i = 0; i < teamMembers.Count; i++)
				{
					if ((teamMembers[i].transform.position - position).sqrMagnitude <= num)
					{
						CharacterBody component = teamMembers[i].GetComponent<CharacterBody>();
						if (component)
						{
							component.AddTimedBuff(JunkContent.Buffs.EngiTeamShield, EngiTeamShield.duration);
							HealthComponent component2 = component.GetComponent<HealthComponent>();
							if (component2)
							{
								component2.RechargeShieldFull();
							}
						}
					}
				}
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400150E RID: 5390
		public static float duration = 3f;

		// Token: 0x0400150F RID: 5391
		public static float radius;
	}
}
