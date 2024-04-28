using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200060A RID: 1546
	public struct CameraState
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x00078814 File Offset: 0x00076A14
		public static CameraState Lerp(ref CameraState a, ref CameraState b, float t)
		{
			return new CameraState
			{
				position = Vector3.LerpUnclamped(a.position, b.position, t),
				rotation = Quaternion.SlerpUnclamped(a.rotation, b.rotation, t),
				fov = Mathf.LerpUnclamped(a.fov, b.fov, t)
			};
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00078878 File Offset: 0x00076A78
		public static CameraState SmoothDamp(CameraState current, CameraState target, ref Vector3 positionVelocity, ref float angleVelocity, ref float fovVelocity, float smoothTime)
		{
			return new CameraState
			{
				position = Vector3.SmoothDamp(current.position, target.position, ref positionVelocity, smoothTime),
				rotation = Util.SmoothDampQuaternion(current.rotation, target.rotation, ref angleVelocity, smoothTime),
				fov = Mathf.SmoothDamp(current.fov, target.fov, ref fovVelocity, smoothTime)
			};
		}

		// Token: 0x0400221B RID: 8731
		public Vector3 position;

		// Token: 0x0400221C RID: 8732
		public Quaternion rotation;

		// Token: 0x0400221D RID: 8733
		public float fov;
	}
}
