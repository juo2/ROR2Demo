using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004FA RID: 1274
	public struct CharacterAnimatorWalkParamCalculator
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x00066464 File Offset: 0x00064664
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0006646C File Offset: 0x0006466C
		public Vector2 animatorWalkSpeed { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00066475 File Offset: 0x00064675
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x0006647D File Offset: 0x0006467D
		public float remainingTurnAngle { get; private set; }

		// Token: 0x0600172F RID: 5935 RVA: 0x00066488 File Offset: 0x00064688
		public void Update(Vector3 worldMoveVector, Vector3 animatorForward, in BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters, float deltaTime)
		{
			ref Vector3 ptr = ref animatorForward;
			Vector3 rhs = Vector3.Cross(Vector3.up, ptr);
			float x = Vector3.Dot(worldMoveVector, ptr);
			float y = Vector3.Dot(worldMoveVector, rhs);
			Vector2 to = new Vector2(x, y);
			float magnitude = to.magnitude;
			float num = (magnitude > 0f) ? Vector2.SignedAngle(Vector2.right, to) : 0f;
			float magnitude2 = this.animatorWalkSpeed.magnitude;
			float current = (magnitude2 > 0f) ? Vector2.SignedAngle(Vector2.right, this.animatorWalkSpeed) : 0f;
			float d = Mathf.SmoothDamp(magnitude2, magnitude, ref this.animatorReferenceMagnitudeVelocity, smoothingParameters.walkMagnitudeSmoothDamp, float.PositiveInfinity, deltaTime);
			float num2 = Mathf.SmoothDampAngle(current, num, ref this.animatorReferenceAngleVelocity, smoothingParameters.walkAngleSmoothDamp, float.PositiveInfinity, deltaTime);
			this.remainingTurnAngle = num2 - num;
			this.animatorWalkSpeed = new Vector2(Mathf.Cos(num2 * 0.017453292f), Mathf.Sin(num2 * 0.017453292f)) * d;
		}

		// Token: 0x04001CE5 RID: 7397
		private float animatorReferenceMagnitudeVelocity;

		// Token: 0x04001CE6 RID: 7398
		private float animatorReferenceAngleVelocity;
	}
}
