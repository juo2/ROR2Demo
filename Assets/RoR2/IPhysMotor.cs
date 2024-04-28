using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000762 RID: 1890
	public interface IPhysMotor
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600270D RID: 9997
		float mass { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600270E RID: 9998
		Vector3 velocity { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600270F RID: 9999
		// (set) Token: 0x06002710 RID: 10000
		Vector3 velocityAuthority { get; set; }

		// Token: 0x06002711 RID: 10001
		void ApplyForceImpulse(in PhysForceInfo physForceInfo);
	}
}
