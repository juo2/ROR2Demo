using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000602 RID: 1538
	public class BuffPickup : MonoBehaviour
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x00077D04 File Offset: 0x00075F04
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.alive && TeamComponent.GetObjectTeam(other.gameObject) == this.teamFilter.teamIndex)
			{
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component)
				{
					component.AddTimedBuff(this.buffDef.buffIndex, this.buffDuration);
					UnityEngine.Object.Instantiate<GameObject>(this.pickupEffect, other.transform.position, Quaternion.identity);
					UnityEngine.Object.Destroy(this.baseObject);
				}
			}
		}

		// Token: 0x040021E7 RID: 8679
		[Tooltip("The base object to destroy when this pickup is consumed.")]
		public GameObject baseObject;

		// Token: 0x040021E8 RID: 8680
		[Tooltip("The team filter object which determines who can pick up this pack.")]
		public TeamFilter teamFilter;

		// Token: 0x040021E9 RID: 8681
		public GameObject pickupEffect;

		// Token: 0x040021EA RID: 8682
		public BuffDef buffDef;

		// Token: 0x040021EB RID: 8683
		public float buffDuration;

		// Token: 0x040021EC RID: 8684
		private bool alive = true;
	}
}
