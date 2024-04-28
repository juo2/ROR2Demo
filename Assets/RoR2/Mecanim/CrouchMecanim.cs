using System;
using UnityEngine;

namespace RoR2.Mecanim
{
	// Token: 0x02000BC4 RID: 3012
	public class CrouchMecanim : MonoBehaviour
	{
		// Token: 0x06004491 RID: 17553 RVA: 0x0011D7F4 File Offset: 0x0011B9F4
		private void FixedUpdate()
		{
			this.crouchStopwatch -= Time.fixedDeltaTime;
			if (this.crouchStopwatch <= 0f)
			{
				this.crouchStopwatch = 0.5f;
				Transform transform = this.crouchOriginOverride ? this.crouchOriginOverride : base.transform;
				Vector3 up = transform.up;
				RaycastHit raycastHit;
				this.crouchCycle = (Physics.Raycast(new Ray(transform.position - up * this.initialVerticalOffset, up), out raycastHit, this.duckHeight + this.initialVerticalOffset, LayerIndex.world.mask, QueryTriggerInteraction.Ignore) ? Mathf.Clamp01(1f - (raycastHit.distance - this.initialVerticalOffset) / this.duckHeight) : 0f);
			}
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x0011D8C7 File Offset: 0x0011BAC7
		private void Update()
		{
			if (this.animator)
			{
				this.animator.SetFloat(CrouchMecanim.crouchCycleOffsetParamNameHash, this.crouchCycle, this.smoothdamp, Time.deltaTime);
			}
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x0011D8F8 File Offset: 0x0011BAF8
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.up * this.duckHeight);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(base.transform.position, base.transform.position + -base.transform.up * this.initialVerticalOffset);
		}

		// Token: 0x04004311 RID: 17169
		public float duckHeight;

		// Token: 0x04004312 RID: 17170
		public Animator animator;

		// Token: 0x04004313 RID: 17171
		public float smoothdamp;

		// Token: 0x04004314 RID: 17172
		public float initialVerticalOffset;

		// Token: 0x04004315 RID: 17173
		public Transform crouchOriginOverride;

		// Token: 0x04004316 RID: 17174
		private float crouchCycle;

		// Token: 0x04004317 RID: 17175
		private const float crouchRaycastFrequency = 2f;

		// Token: 0x04004318 RID: 17176
		private float crouchStopwatch;

		// Token: 0x04004319 RID: 17177
		private static readonly int crouchCycleOffsetParamNameHash = Animator.StringToHash("crouchCycleOffset");
	}
}
