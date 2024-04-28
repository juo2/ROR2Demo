using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006D4 RID: 1748
	public class ExplodeRigidbodiesOnStart : MonoBehaviour
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x00094C5C File Offset: 0x00092E5C
		private void Start()
		{
			Vector3 position = base.transform.position;
			for (int i = 0; i < this.bodies.Length; i++)
			{
				this.bodies[i].AddExplosionForce(this.force, position, this.explosionRadius);
			}
		}

		// Token: 0x0400277A RID: 10106
		public Rigidbody[] bodies;

		// Token: 0x0400277B RID: 10107
		public float force;

		// Token: 0x0400277C RID: 10108
		public float explosionRadius;
	}
}
