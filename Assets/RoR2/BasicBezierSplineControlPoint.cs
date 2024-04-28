using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005EF RID: 1519
	public class BasicBezierSplineControlPoint : MonoBehaviour
	{
		// Token: 0x06001BA1 RID: 7073 RVA: 0x00075D7C File Offset: 0x00073F7C
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.SetTRS(base.transform.position, base.transform.rotation, Vector3.one);
			Gizmos.matrix = identity;
			Gizmos.DrawRay(Vector3.zero, this.forwardVelocity);
			Gizmos.DrawRay(Vector3.zero, this.backwardVelocity);
			Gizmos.DrawFrustum(Vector3.zero, 60f, -0.2f, 0f, 1f);
		}

		// Token: 0x0400217D RID: 8573
		public Vector3 forwardVelocity = Vector3.forward;

		// Token: 0x0400217E RID: 8574
		public Vector3 backwardVelocity = Vector3.back;
	}
}
