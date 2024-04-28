using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006AA RID: 1706
	[RequireComponent(typeof(SphereCollider))]
	public class DisableCollisionsIfInTrigger : MonoBehaviour
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x0008EB89 File Offset: 0x0008CD89
		public void Awake()
		{
			this.trigger = base.GetComponent<SphereCollider>();
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0008EB98 File Offset: 0x0008CD98
		private void OnTriggerEnter(Collider other)
		{
			if (this.trigger)
			{
				Vector3 position = base.transform.position;
				Vector3 position2 = other.transform.position;
				float num = this.trigger.radius * Mathf.Max(new float[]
				{
					base.transform.lossyScale.x,
					base.transform.lossyScale.y,
					base.transform.lossyScale.z
				});
				float num2 = num * num;
				if ((position - position2).sqrMagnitude < num2)
				{
					Physics.IgnoreCollision(this.colliderToIgnore, other);
				}
			}
		}

		// Token: 0x04002699 RID: 9881
		public Collider colliderToIgnore;

		// Token: 0x0400269A RID: 9882
		private SphereCollider trigger;
	}
}
