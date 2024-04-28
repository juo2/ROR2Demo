using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200075D RID: 1885
	public class IKTargetPassive : MonoBehaviour, IIKTargetBehavior
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x000026ED File Offset: 0x000008ED
		public void UpdateIKState(int targetState)
		{
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000A96FF File Offset: 0x000A78FF
		private void Awake()
		{
			if (this.cacheFirstPosition)
			{
				this.cachedLocalPosition = base.transform.localPosition;
			}
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000A971C File Offset: 0x000A791C
		private void LateUpdate()
		{
			this.selfPlantTimer -= Time.deltaTime;
			if (this.selfPlant && this.selfPlantTimer <= 0f)
			{
				this.selfPlantTimer = 1f / this.selfPlantFrequency;
				this.UpdateIKTargetPosition();
			}
			this.UpdateYOffset();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000A9770 File Offset: 0x000A7970
		public void UpdateIKTargetPosition()
		{
			this.ResetTransformToCachedPosition();
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * -this.minHeight, Vector3.down, out raycastHit, this.maxHeight - this.minHeight, LayerIndex.world.mask))
			{
				this.targetHeightOffset = raycastHit.point.y - base.transform.position.y;
			}
			else
			{
				this.targetHeightOffset = 0f;
			}
			this.targetHeightOffset += this.baseOffset;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000A9814 File Offset: 0x000A7A14
		public void UpdateYOffset()
		{
			float t = 1f;
			if (this.animator && this.animatorIKWeightFloat.Length > 0)
			{
				t = this.animator.GetFloat(this.animatorIKWeightFloat);
			}
			this.smoothedTargetHeightOffset = Mathf.SmoothDamp(this.smoothedTargetHeightOffset, this.targetHeightOffset, ref this.smoothdampVelocity, this.dampTime, float.PositiveInfinity, Time.deltaTime);
			this.ResetTransformToCachedPosition();
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Mathf.Lerp(0f, this.smoothedTargetHeightOffset, t), base.transform.position.z);
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000A98D9 File Offset: 0x000A7AD9
		private void ResetTransformToCachedPosition()
		{
			if (this.cacheFirstPosition)
			{
				base.transform.localPosition = new Vector3(this.cachedLocalPosition.x, this.cachedLocalPosition.y, this.cachedLocalPosition.z);
			}
		}

		// Token: 0x04002AE3 RID: 10979
		private float smoothedTargetHeightOffset;

		// Token: 0x04002AE4 RID: 10980
		private float targetHeightOffset;

		// Token: 0x04002AE5 RID: 10981
		private float smoothdampVelocity;

		// Token: 0x04002AE6 RID: 10982
		public float minHeight = -0.3f;

		// Token: 0x04002AE7 RID: 10983
		public float maxHeight = 1f;

		// Token: 0x04002AE8 RID: 10984
		public float dampTime = 0.1f;

		// Token: 0x04002AE9 RID: 10985
		public float baseOffset;

		// Token: 0x04002AEA RID: 10986
		[Tooltip("The IK weight float parameter if used")]
		public string animatorIKWeightFloat = "";

		// Token: 0x04002AEB RID: 10987
		public Animator animator;

		// Token: 0x04002AEC RID: 10988
		[Tooltip("The target transform will plant without any calls from external IK chains")]
		public bool selfPlant;

		// Token: 0x04002AED RID: 10989
		public float selfPlantFrequency = 5f;

		// Token: 0x04002AEE RID: 10990
		[Tooltip("Whether or not to cache where the raycast begins. Used when not attached to bones, who reset themselves via animator.")]
		public bool cacheFirstPosition;

		// Token: 0x04002AEF RID: 10991
		private Vector3 cachedLocalPosition;

		// Token: 0x04002AF0 RID: 10992
		private float selfPlantTimer;
	}
}
