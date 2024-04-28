using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B72 RID: 2930
	[RequireComponent(typeof(HealthComponent))]
	public class AssignTeamFilterToTeamComponent : MonoBehaviour
	{
		// Token: 0x060042C4 RID: 17092 RVA: 0x001148B8 File Offset: 0x00112AB8
		private void Start()
		{
			if (NetworkServer.active)
			{
				TeamComponent component = base.GetComponent<TeamComponent>();
				TeamFilter component2 = base.GetComponent<TeamFilter>();
				if (component2 && component)
				{
					component.teamIndex = component2.teamIndex;
				}
			}
		}
	}
}
