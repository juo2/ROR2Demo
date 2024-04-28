﻿using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200075E RID: 1886
	public class IKTargetPlant : MonoBehaviour, IIKTargetBehavior
	{
		// Token: 0x06002705 RID: 9989 RVA: 0x000A9953 File Offset: 0x000A7B53
		private void Awake()
		{
			this.ikChain = base.GetComponent<IKSimpleChain>();
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000A9961 File Offset: 0x000A7B61
		public void UpdateIKState(int targetState)
		{
			if (this.ikState != IKTargetPlant.IKState.Reset)
			{
				this.ikState = (IKTargetPlant.IKState)targetState;
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000A9973 File Offset: 0x000A7B73
		public Vector3 GetArcPosition(Vector3 start, Vector3 end, float arcHeight, float t)
		{
			return Vector3.Lerp(start, end, Mathf.Sin(t * 3.1415927f * 0.5f)) + new Vector3(0f, Mathf.Sin(t * 3.1415927f) * arcHeight, 0f);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000A99B4 File Offset: 0x000A7BB4
		public void UpdateIKTargetPosition()
		{
			if (this.animator)
			{
				this.ikWeight = this.animator.GetFloat(this.animatorIKWeightFloat);
			}
			else
			{
				this.ikWeight = 1f;
			}
			IKTargetPlant.IKState ikstate = this.ikState;
			if (ikstate != IKTargetPlant.IKState.Plant)
			{
				if (ikstate == IKTargetPlant.IKState.Reset)
				{
					this.resetTimer += Time.deltaTime;
					this.isPlanted = false;
					this.RaycastIKTarget(base.transform.position);
					base.transform.position = this.GetArcPosition(this.plantPosition, this.targetPosition, this.arcHeight, this.resetTimer / this.timeToReset);
					if (this.resetTimer >= this.timeToReset)
					{
						this.ikState = IKTargetPlant.IKState.Plant;
						this.isPlanted = true;
						this.plantPosition = this.targetPosition;
						UnityEngine.Object.Instantiate<GameObject>(this.plantEffect, this.plantPosition, Quaternion.identity);
					}
				}
			}
			else
			{
				Vector3 position = base.transform.position;
				this.RaycastIKTarget(position);
				if (!this.isPlanted)
				{
					this.plantPosition = this.targetPosition;
					base.transform.position = this.plantPosition;
					this.isPlanted = true;
					if (this.plantEffect)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.plantEffect, this.plantPosition, Quaternion.identity);
					}
				}
				else
				{
					base.transform.position = Vector3.Lerp(position, this.plantPosition, this.ikWeight);
				}
				Vector3 vector = position - base.transform.position;
				vector.y = 0f;
				if (this.ikChain.LegTooShort(this.legScale) || vector.sqrMagnitude >= this.maxXZPositionalError * this.maxXZPositionalError)
				{
					this.plantPosition = base.transform.position;
					this.ikState = IKTargetPlant.IKState.Reset;
					if (this.animator)
					{
						this.animator.SetTrigger(this.animatorLiftTrigger);
					}
					this.resetTimer = 0f;
				}
			}
			base.transform.position = Vector3.SmoothDamp(this.lastTransformPosition, base.transform.position, ref this.smoothDampRefVelocity, this.smoothDampTime);
			this.lastTransformPosition = base.transform.position;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000A9BF0 File Offset: 0x000A7DF0
		public void RaycastIKTarget(Vector3 position)
		{
			RaycastHit raycastHit;
			if (this.useSpherecast)
			{
				Physics.SphereCast(position + Vector3.up * -this.minHeight, this.spherecastRadius, Vector3.down, out raycastHit, this.maxHeight - this.minHeight, LayerIndex.world.mask);
			}
			else
			{
				Physics.Raycast(position + Vector3.up * -this.minHeight, Vector3.down, out raycastHit, this.maxHeight - this.minHeight, LayerIndex.world.mask);
			}
			if (raycastHit.collider)
			{
				this.targetPosition = raycastHit.point;
				return;
			}
			this.targetPosition = position;
		}

		// Token: 0x04002AF1 RID: 10993
		[Tooltip("The max offset to step up")]
		public float minHeight = -0.3f;

		// Token: 0x04002AF2 RID: 10994
		[Tooltip("The max offset to step down")]
		public float maxHeight = 1f;

		// Token: 0x04002AF3 RID: 10995
		[Tooltip("The strength of the IK as a lerp (0-1)")]
		public float ikWeight = 1f;

		// Token: 0x04002AF4 RID: 10996
		[Tooltip("The time to restep")]
		public float timeToReset = 0.6f;

		// Token: 0x04002AF5 RID: 10997
		[Tooltip("The max positional IK error before restepping")]
		public float maxXZPositionalError = 4f;

		// Token: 0x04002AF6 RID: 10998
		public GameObject plantEffect;

		// Token: 0x04002AF7 RID: 10999
		public Animator animator;

		// Token: 0x04002AF8 RID: 11000
		[Tooltip("The IK weight float parameter if used")]
		public string animatorIKWeightFloat;

		// Token: 0x04002AF9 RID: 11001
		[Tooltip("The lift animation trigger string if used")]
		public string animatorLiftTrigger;

		// Token: 0x04002AFA RID: 11002
		[Tooltip("The scale of the leg for calculating if the leg is too short to reach the IK target")]
		public float legScale = 1f;

		// Token: 0x04002AFB RID: 11003
		[Tooltip("The height of the step arc")]
		public float arcHeight = 1f;

		// Token: 0x04002AFC RID: 11004
		[Tooltip("The smoothing duration for the IK. Higher will be smoother but will be delayed.")]
		public float smoothDampTime = 0.1f;

		// Token: 0x04002AFD RID: 11005
		[Tooltip("Spherecasts will have more hits but take higher performance.")]
		public bool useSpherecast;

		// Token: 0x04002AFE RID: 11006
		public float spherecastRadius = 0.5f;

		// Token: 0x04002AFF RID: 11007
		public IKTargetPlant.IKState ikState;

		// Token: 0x04002B00 RID: 11008
		private bool isPlanted;

		// Token: 0x04002B01 RID: 11009
		private Vector3 lastTransformPosition;

		// Token: 0x04002B02 RID: 11010
		private Vector3 smoothDampRefVelocity;

		// Token: 0x04002B03 RID: 11011
		private Vector3 targetPosition;

		// Token: 0x04002B04 RID: 11012
		private Vector3 plantPosition;

		// Token: 0x04002B05 RID: 11013
		private IKSimpleChain ikChain;

		// Token: 0x04002B06 RID: 11014
		private float resetTimer;

		// Token: 0x0200075F RID: 1887
		public enum IKState
		{
			// Token: 0x04002B08 RID: 11016
			Plant,
			// Token: 0x04002B09 RID: 11017
			Reset
		}
	}
}
