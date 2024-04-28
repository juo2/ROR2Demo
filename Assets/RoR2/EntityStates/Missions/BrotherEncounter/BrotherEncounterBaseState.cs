using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x0200024E RID: 590
	public class BrotherEncounterBaseState : EntityState
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool shouldEnableArenaWalls
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool shouldEnableArenaNodes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002B568 File Offset: 0x00029768
		public override void OnEnter()
		{
			base.OnEnter();
			this.childLocator = base.GetComponent<ChildLocator>();
			Transform transform = this.childLocator.FindChild("ArenaWalls");
			Transform transform2 = this.childLocator.FindChild("ArenaNodes");
			if (transform)
			{
				transform.gameObject.SetActive(this.shouldEnableArenaWalls);
			}
			if (transform2)
			{
				transform2.gameObject.SetActive(this.shouldEnableArenaNodes);
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002B5DC File Offset: 0x000297DC
		public void KillAllMonsters()
		{
			if (NetworkServer.active)
			{
				foreach (TeamComponent teamComponent in new List<TeamComponent>(TeamComponent.GetTeamMembers(TeamIndex.Monster)))
				{
					if (teamComponent)
					{
						HealthComponent component = teamComponent.GetComponent<HealthComponent>();
						if (component)
						{
							component.Suicide(null, null, DamageType.Generic);
						}
					}
				}
			}
		}

		// Token: 0x04000C29 RID: 3113
		protected ChildLocator childLocator;
	}
}
