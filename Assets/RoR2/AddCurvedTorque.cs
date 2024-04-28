using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005BD RID: 1469
	public class AddCurvedTorque : MonoBehaviour
	{
		// Token: 0x06001A9B RID: 6811 RVA: 0x0007210C File Offset: 0x0007030C
		private void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			float d = this.torqueCurve.Evaluate(this.stopwatch / this.lifetime);
			Rigidbody[] array = this.rigidbodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddRelativeTorque(this.localTorqueVector * d);
			}
		}

		// Token: 0x040020A3 RID: 8355
		public AnimationCurve torqueCurve;

		// Token: 0x040020A4 RID: 8356
		public Vector3 localTorqueVector;

		// Token: 0x040020A5 RID: 8357
		public float lifetime;

		// Token: 0x040020A6 RID: 8358
		public Rigidbody[] rigidbodies;

		// Token: 0x040020A7 RID: 8359
		private float stopwatch;
	}
}
