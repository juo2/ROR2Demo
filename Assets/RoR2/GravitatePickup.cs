using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000716 RID: 1814
	public class GravitatePickup : MonoBehaviour
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000A20F0 File Offset: 0x000A02F0
		private void OnTriggerEnter(Collider other)
		{
			if (NetworkServer.active && !this.gravitateTarget && this.teamFilter.teamIndex != TeamIndex.None)
			{
				HealthComponent component = other.gameObject.GetComponent<HealthComponent>();
				if (TeamComponent.GetObjectTeam(other.gameObject) == this.teamFilter.teamIndex && (this.gravitateAtFullHealth || !component || component.health < component.fullHealth))
				{
					this.gravitateTarget = other.gameObject.transform;
				}
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000A2174 File Offset: 0x000A0374
		private void FixedUpdate()
		{
			if (this.gravitateTarget)
			{
				this.rigidbody.velocity = Vector3.MoveTowards(this.rigidbody.velocity, (this.gravitateTarget.transform.position - base.transform.position).normalized * this.maxSpeed, this.acceleration);
			}
		}

		// Token: 0x0400294C RID: 10572
		private Transform gravitateTarget;

		// Token: 0x0400294D RID: 10573
		[Tooltip("The rigidbody to set the velocity of.")]
		public Rigidbody rigidbody;

		// Token: 0x0400294E RID: 10574
		[Tooltip("The TeamFilter which controls which team can activate this trigger.")]
		public TeamFilter teamFilter;

		// Token: 0x0400294F RID: 10575
		public float acceleration;

		// Token: 0x04002950 RID: 10576
		public float maxSpeed;

		// Token: 0x04002951 RID: 10577
		public bool gravitateAtFullHealth = true;
	}
}
