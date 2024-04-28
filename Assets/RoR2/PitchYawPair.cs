using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000995 RID: 2453
	[Serializable]
	public struct PitchYawPair
	{
		// Token: 0x060037BF RID: 14271 RVA: 0x000EA78D File Offset: 0x000E898D
		public PitchYawPair(float pitch, float yaw)
		{
			this.pitch = pitch;
			this.yaw = yaw;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000EA7A0 File Offset: 0x000E89A0
		public static PitchYawPair Lerp(PitchYawPair a, PitchYawPair b, float t)
		{
			float num = Mathf.LerpAngle(a.pitch, b.pitch, t);
			float num2 = Mathf.LerpAngle(a.yaw, b.yaw, t);
			return new PitchYawPair(num, num2);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000EA7D8 File Offset: 0x000E89D8
		public static PitchYawPair SmoothDamp(PitchYawPair current, PitchYawPair target, ref PitchYawPair velocity, float smoothTime, float maxSpeed)
		{
			float num = Mathf.SmoothDampAngle(current.pitch, target.pitch, ref velocity.pitch, smoothTime, maxSpeed);
			float num2 = Mathf.SmoothDampAngle(current.yaw, target.yaw, ref velocity.yaw, smoothTime, maxSpeed);
			return new PitchYawPair(num, num2);
		}

		// Token: 0x040037F6 RID: 14326
		public static readonly PitchYawPair zero = new PitchYawPair(0f, 0f);

		// Token: 0x040037F7 RID: 14327
		public float pitch;

		// Token: 0x040037F8 RID: 14328
		public float yaw;
	}
}
