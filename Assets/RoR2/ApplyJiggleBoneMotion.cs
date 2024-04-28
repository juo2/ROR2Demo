using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005CE RID: 1486
	public class ApplyJiggleBoneMotion : MonoBehaviour
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x00073694 File Offset: 0x00071894
		private void FixedUpdate()
		{
			Vector3 position = this.rootTransform.position;
			Rigidbody[] array = this.rigidbodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AddForce((this.lastRootPosition - position) * this.forceScale * Time.fixedDeltaTime);
			}
			this.lastRootPosition = position;
		}

		// Token: 0x0400210F RID: 8463
		public float forceScale = 100f;

		// Token: 0x04002110 RID: 8464
		public Transform rootTransform;

		// Token: 0x04002111 RID: 8465
		public Rigidbody[] rigidbodies;

		// Token: 0x04002112 RID: 8466
		private Vector3 lastRootPosition;
	}
}
