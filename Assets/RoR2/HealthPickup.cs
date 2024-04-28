using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000727 RID: 1831
	public class HealthPickup : MonoBehaviour
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x000A6094 File Offset: 0x000A4294
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.alive && TeamComponent.GetObjectTeam(other.gameObject) == this.teamFilter.teamIndex)
			{
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component)
				{
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						component.healthComponent.Heal(this.flatHealing + healthComponent.fullHealth * this.fractionalHealing, default(ProcChainMask), true);
						EffectManager.SpawnEffect(this.pickupEffect, new EffectData
						{
							origin = base.transform.position
						}, true);
					}
					UnityEngine.Object.Destroy(this.baseObject);
				}
			}
		}

		// Token: 0x040029E2 RID: 10722
		[Tooltip("The base object to destroy when this pickup is consumed.")]
		public GameObject baseObject;

		// Token: 0x040029E3 RID: 10723
		[Tooltip("The team filter object which determines who can pick up this pack.")]
		public TeamFilter teamFilter;

		// Token: 0x040029E4 RID: 10724
		public GameObject pickupEffect;

		// Token: 0x040029E5 RID: 10725
		public float flatHealing;

		// Token: 0x040029E6 RID: 10726
		public float fractionalHealing;

		// Token: 0x040029E7 RID: 10727
		private bool alive = true;
	}
}
