using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200088A RID: 2186
	[RequireComponent(typeof(WheelCollider))]
	public class SetWheelVisuals : MonoBehaviour
	{
		// Token: 0x0600300A RID: 12298 RVA: 0x000CC5FE File Offset: 0x000CA7FE
		private void Start()
		{
			this.wheelCollider = base.GetComponent<WheelCollider>();
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000CC60C File Offset: 0x000CA80C
		private void FixedUpdate()
		{
			Vector3 position;
			Quaternion rotation;
			this.wheelCollider.GetWorldPose(out position, out rotation);
			this.visualTransform.position = position;
			this.visualTransform.rotation = rotation;
		}

		// Token: 0x040031B2 RID: 12722
		public Transform visualTransform;

		// Token: 0x040031B3 RID: 12723
		private WheelCollider wheelCollider;
	}
}
