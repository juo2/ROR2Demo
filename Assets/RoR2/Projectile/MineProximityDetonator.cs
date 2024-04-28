using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B82 RID: 2946
	public class MineProximityDetonator : MonoBehaviour
	{
		// Token: 0x06004311 RID: 17169 RVA: 0x00116410 File Offset: 0x00114610
		public void OnTriggerEnter(Collider collider)
		{
			if (NetworkServer.active)
			{
				if (collider)
				{
					HurtBox component = collider.GetComponent<HurtBox>();
					if (component)
					{
						HealthComponent healthComponent = component.healthComponent;
						if (healthComponent)
						{
							TeamComponent component2 = healthComponent.GetComponent<TeamComponent>();
							if (component2 && component2.teamIndex == this.myTeamFilter.teamIndex)
							{
								return;
							}
							UnityEvent unityEvent = this.triggerEvents;
							if (unityEvent == null)
							{
								return;
							}
							unityEvent.Invoke();
						}
					}
				}
				return;
			}
		}

		// Token: 0x04004120 RID: 16672
		public TeamFilter myTeamFilter;

		// Token: 0x04004121 RID: 16673
		public UnityEvent triggerEvents;
	}
}
