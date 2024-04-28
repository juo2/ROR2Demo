using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005C6 RID: 1478
	public class AmmoPickup : MonoBehaviour
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x00072D8C File Offset: 0x00070F8C
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.alive && TeamComponent.GetObjectTeam(other.gameObject) == this.teamFilter.teamIndex)
			{
				SkillLocator component = other.GetComponent<SkillLocator>();
				if (component)
				{
					this.alive = false;
					component.ApplyAmmoPack();
					EffectManager.SimpleEffect(this.pickupEffect, base.transform.position, Quaternion.identity, true);
					UnityEngine.Object.Destroy(this.baseObject);
				}
			}
		}

		// Token: 0x040020E4 RID: 8420
		[Tooltip("The base object to destroy when this pickup is consumed.")]
		public GameObject baseObject;

		// Token: 0x040020E5 RID: 8421
		[Tooltip("The team filter object which determines who can pick up this pack.")]
		public TeamFilter teamFilter;

		// Token: 0x040020E6 RID: 8422
		public GameObject pickupEffect;

		// Token: 0x040020E7 RID: 8423
		private bool alive = true;
	}
}
