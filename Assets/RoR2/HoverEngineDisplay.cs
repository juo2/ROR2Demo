using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000738 RID: 1848
	public class HoverEngineDisplay : MonoBehaviour
	{
		// Token: 0x06002675 RID: 9845 RVA: 0x000A77F8 File Offset: 0x000A59F8
		private void FixedUpdate()
		{
			ref Vector3 localEulerAngles = base.transform.localEulerAngles;
			float t = Mathf.Clamp01(this.hoverEngine.forceStrength / this.hoverEngine.hoverForce * this.forceScale);
			float target = Mathf.LerpAngle(this.minPitch, this.maxPitch, t);
			float x = Mathf.SmoothDampAngle(localEulerAngles.x, target, ref this.smoothVelocity, this.smoothTime);
			base.transform.localRotation = Quaternion.Euler(x, 0f, 0f);
		}

		// Token: 0x04002A4B RID: 10827
		public HoverEngine hoverEngine;

		// Token: 0x04002A4C RID: 10828
		[Tooltip("The local pitch at zero engine strength")]
		public float minPitch = -20f;

		// Token: 0x04002A4D RID: 10829
		[Tooltip("The local pitch at max engine strength")]
		public float maxPitch = 60f;

		// Token: 0x04002A4E RID: 10830
		public float smoothTime = 0.2f;

		// Token: 0x04002A4F RID: 10831
		public float forceScale = 1f;

		// Token: 0x04002A50 RID: 10832
		private float smoothVelocity;
	}
}
