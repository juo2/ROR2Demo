using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B7D RID: 2941
	public struct ProjectileImpactInfo
	{
		// Token: 0x04004105 RID: 16645
		public Collider collider;

		// Token: 0x04004106 RID: 16646
		public Vector3 estimatedPointOfImpact;

		// Token: 0x04004107 RID: 16647
		public Vector3 estimatedImpactNormal;
	}
}
