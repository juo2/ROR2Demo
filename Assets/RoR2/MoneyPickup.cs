using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007BA RID: 1978
	public class MoneyPickup : MonoBehaviour
	{
		// Token: 0x060029DD RID: 10717 RVA: 0x000B4C28 File Offset: 0x000B2E28
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.goldReward = (this.shouldScale ? Run.instance.GetDifficultyScaledCost(this.baseGoldReward) : this.baseGoldReward);
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000B4C58 File Offset: 0x000B2E58
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.alive)
			{
				TeamIndex objectTeam = TeamComponent.GetObjectTeam(other.gameObject);
				if (objectTeam == this.teamFilter.teamIndex)
				{
					this.alive = false;
					Vector3 position = base.transform.position;
					TeamManager.instance.GiveTeamMoney(objectTeam, (uint)this.goldReward);
					if (this.pickupEffectPrefab)
					{
						EffectManager.SimpleEffect(this.pickupEffectPrefab, position, Quaternion.identity, true);
					}
					UnityEngine.Object.Destroy(this.baseObject);
				}
			}
		}

		// Token: 0x04002D2A RID: 11562
		[Tooltip("The base object to destroy when this pickup is consumed.")]
		public GameObject baseObject;

		// Token: 0x04002D2B RID: 11563
		[Tooltip("The team filter object which determines who can pick up this pack.")]
		public TeamFilter teamFilter;

		// Token: 0x04002D2C RID: 11564
		public GameObject pickupEffectPrefab;

		// Token: 0x04002D2D RID: 11565
		public int baseGoldReward;

		// Token: 0x04002D2E RID: 11566
		public bool shouldScale;

		// Token: 0x04002D2F RID: 11567
		private bool alive = true;

		// Token: 0x04002D30 RID: 11568
		private int goldReward;
	}
}
