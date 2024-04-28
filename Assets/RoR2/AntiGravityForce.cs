using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005CC RID: 1484
	[RequireComponent(typeof(Rigidbody))]
	public class AntiGravityForce : MonoBehaviour
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00073647 File Offset: 0x00071847
		private void FixedUpdate()
		{
			this.rb.AddForce(-Physics.gravity * this.antiGravityCoefficient, ForceMode.Acceleration);
		}

		// Token: 0x0400210C RID: 8460
		public Rigidbody rb;

		// Token: 0x0400210D RID: 8461
		[Tooltip("How much to oppose gravity. A value of 1 means it is unaffected by gravity.")]
		public float antiGravityCoefficient;
	}
}
