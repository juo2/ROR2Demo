using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006A9 RID: 1705
	public class DisableCollisionsBetweenColliders : MonoBehaviour
	{
		// Token: 0x06002134 RID: 8500 RVA: 0x0008EB3C File Offset: 0x0008CD3C
		public void Awake()
		{
			foreach (Collider collider in this.collidersA)
			{
				foreach (Collider collider2 in this.collidersB)
				{
					Physics.IgnoreCollision(collider, collider2);
				}
			}
		}

		// Token: 0x04002697 RID: 9879
		public Collider[] collidersA;

		// Token: 0x04002698 RID: 9880
		public Collider[] collidersB;
	}
}
