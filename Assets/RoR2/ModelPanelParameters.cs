using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007B8 RID: 1976
	public class ModelPanelParameters : MonoBehaviour
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000B4A86 File Offset: 0x000B2C86
		public Vector3 cameraDirection
		{
			get
			{
				return this.focusPointTransform.position - this.cameraPositionTransform.position;
			}
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
		public void OnDrawGizmos()
		{
			if (this.focusPointTransform)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(this.focusPointTransform.position, 0.1f);
				Gizmos.color = Color.gray;
				Gizmos.DrawWireSphere(this.focusPointTransform.position, this.minDistance);
				Gizmos.DrawWireSphere(this.focusPointTransform.position, this.maxDistance);
			}
			if (this.cameraPositionTransform)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(this.cameraPositionTransform.position, 0.2f);
			}
			if (this.focusPointTransform && this.cameraPositionTransform)
			{
				Gizmos.DrawLine(this.focusPointTransform.position, this.cameraPositionTransform.position);
			}
		}

		// Token: 0x04002D22 RID: 11554
		public Transform focusPointTransform;

		// Token: 0x04002D23 RID: 11555
		public Transform cameraPositionTransform;

		// Token: 0x04002D24 RID: 11556
		public Quaternion modelRotation = Quaternion.identity;

		// Token: 0x04002D25 RID: 11557
		public float minDistance = 1f;

		// Token: 0x04002D26 RID: 11558
		public float maxDistance = 10f;
	}
}
