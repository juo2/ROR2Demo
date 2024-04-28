using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008AB RID: 2219
	public class StriderLegController : MonoBehaviour
	{
		// Token: 0x06003141 RID: 12609 RVA: 0x000D0FBC File Offset: 0x000CF1BC
		public Vector3 GetCenterOfStance()
		{
			Vector3 a = Vector3.zero;
			for (int i = 0; i < this.feet.Length; i++)
			{
				a += this.feet[i].transform.position;
			}
			return a / (float)this.feet.Length;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000D1010 File Offset: 0x000CF210
		private void Awake()
		{
			for (int i = 0; i < this.feet.Length; i++)
			{
				this.feet[i].footState = StriderLegController.FootState.Planted;
				this.feet[i].plantPosition = this.feet[i].referenceTransform.position;
				this.feet[i].trailingTargetPosition = this.feet[i].plantPosition;
				this.feet[i].footRaycastTimer = UnityEngine.Random.Range(0f, 1f / this.footRaycastFrequency);
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000D10B8 File Offset: 0x000CF2B8
		private void Update()
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.feet.Length; i++)
			{
				if (this.feet[i].footState == StriderLegController.FootState.Replanting)
				{
					num2++;
				}
			}
			for (int j = 0; j < this.feet.Length; j++)
			{
				StriderLegController.FootInfo[] array = this.feet;
				int num3 = j;
				array[num3].footRaycastTimer = array[num3].footRaycastTimer - Time.deltaTime;
				Transform transform = this.feet[j].transform;
				Transform referenceTransform = this.feet[j].referenceTransform;
				Vector3 position = transform.position;
				Vector3 vector = Vector3.zero;
				float num4 = 0f;
				StriderLegController.FootState footState = this.feet[j].footState;
				if (footState != StriderLegController.FootState.Planted)
				{
					if (footState == StriderLegController.FootState.Replanting)
					{
						StriderLegController.FootInfo[] array2 = this.feet;
						int num5 = j;
						array2[num5].stopwatch = array2[num5].stopwatch + Time.deltaTime;
						Vector3 plantPosition = this.feet[j].plantPosition;
						Vector3 vector2 = referenceTransform.position;
						vector2 += Vector3.ProjectOnPlane(vector2 - plantPosition, Vector3.up).normalized * this.overstepDistance;
						float num6 = this.lerpCurve.Evaluate(this.feet[j].stopwatch / this.replantDuration);
						vector = Vector3.Lerp(plantPosition, vector2, num6);
						num4 = Mathf.Sin(num6 * 3.1415927f) * this.replantHeight;
						if (this.feet[j].stopwatch >= this.replantDuration)
						{
							this.feet[j].plantPosition = vector2;
							this.feet[j].stopwatch = 0f;
							this.feet[j].footState = StriderLegController.FootState.Planted;
							Util.PlaySound(this.footPlantString, transform.gameObject);
							if (this.footPlantEffect)
							{
								EffectManager.SimpleEffect(this.footPlantEffect, transform.position, Quaternion.identity, false);
							}
						}
					}
				}
				else
				{
					num++;
					vector = this.feet[j].plantPosition;
					if ((referenceTransform.position - vector).sqrMagnitude > this.stabilityRadius * this.stabilityRadius && num2 < this.maxFeetReplantingAtOnce)
					{
						this.feet[j].footState = StriderLegController.FootState.Replanting;
						Util.PlaySound(this.footMoveString, transform.gameObject);
						num2++;
					}
				}
				Ray ray = default(Ray);
				ray.direction = transform.TransformDirection(this.footRaycastDirection.normalized);
				ray.origin = vector - ray.direction * this.raycastVerticalOffset;
				if (this.feet[j].footRaycastTimer <= 0f)
				{
					this.feet[j].footRaycastTimer = 1f / this.footRaycastFrequency;
					this.feet[j].lastYOffsetFromRaycast = this.feet[j].currentYOffsetFromRaycast;
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, this.maxRaycastDistance + this.raycastVerticalOffset, LayerIndex.world.mask))
					{
						this.feet[j].currentYOffsetFromRaycast = raycastHit.point.y - vector.y;
					}
					else
					{
						this.feet[j].currentYOffsetFromRaycast = 0f;
					}
				}
				float num7 = Mathf.Lerp(this.feet[j].currentYOffsetFromRaycast, this.feet[j].lastYOffsetFromRaycast, this.feet[j].footRaycastTimer / (1f / this.footRaycastFrequency));
				vector.y += num4 + num7;
				this.feet[j].trailingTargetPosition = Vector3.SmoothDamp(this.feet[j].trailingTargetPosition, vector, ref this.feet[j].velocity, this.footDampTime);
				transform.position = this.feet[j].trailingTargetPosition;
			}
			if (this.rootTransform)
			{
				Vector3 localPosition = this.rootTransform.localPosition;
				float num8 = (1f - (float)num / (float)this.feet.Length) * this.rootOffsetHeight;
				float target = localPosition.z - num8;
				float z = Mathf.SmoothDamp(localPosition.z, target, ref this.rootVelocity, this.rootSmoothDamp);
				this.rootTransform.localPosition = new Vector3(localPosition.x, localPosition.y, z);
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000A9973 File Offset: 0x000A7B73
		public Vector3 GetArcPosition(Vector3 start, Vector3 end, float arcHeight, float t)
		{
			return Vector3.Lerp(start, end, Mathf.Sin(t * 3.1415927f * 0.5f)) + new Vector3(0f, Mathf.Sin(t * 3.1415927f) * arcHeight, 0f);
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000D1568 File Offset: 0x000CF768
		public void OnDrawGizmos()
		{
			for (int i = 0; i < this.feet.Length; i++)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawRay(this.feet[i].transform.position, this.feet[i].transform.TransformVector(this.footRaycastDirection));
			}
		}

		// Token: 0x040032C2 RID: 12994
		[Header("Foot Settings")]
		public Transform centerOfGravity;

		// Token: 0x040032C3 RID: 12995
		public StriderLegController.FootInfo[] feet;

		// Token: 0x040032C4 RID: 12996
		public Vector3 footRaycastDirection;

		// Token: 0x040032C5 RID: 12997
		public float raycastVerticalOffset;

		// Token: 0x040032C6 RID: 12998
		public float maxRaycastDistance;

		// Token: 0x040032C7 RID: 12999
		public float footDampTime;

		// Token: 0x040032C8 RID: 13000
		public float stabilityRadius;

		// Token: 0x040032C9 RID: 13001
		public float replantDuration;

		// Token: 0x040032CA RID: 13002
		public float replantHeight;

		// Token: 0x040032CB RID: 13003
		public float overstepDistance;

		// Token: 0x040032CC RID: 13004
		public AnimationCurve lerpCurve;

		// Token: 0x040032CD RID: 13005
		public GameObject footPlantEffect;

		// Token: 0x040032CE RID: 13006
		public string footPlantString;

		// Token: 0x040032CF RID: 13007
		public string footMoveString;

		// Token: 0x040032D0 RID: 13008
		public float footRaycastFrequency = 0.2f;

		// Token: 0x040032D1 RID: 13009
		public int maxFeetReplantingAtOnce = 9999;

		// Token: 0x040032D2 RID: 13010
		[Header("Root Settings")]
		public Transform rootTransform;

		// Token: 0x040032D3 RID: 13011
		public float rootSpringConstant;

		// Token: 0x040032D4 RID: 13012
		public float rootDampingConstant;

		// Token: 0x040032D5 RID: 13013
		public float rootOffsetHeight;

		// Token: 0x040032D6 RID: 13014
		public float rootSmoothDamp;

		// Token: 0x040032D7 RID: 13015
		private float rootVelocity;

		// Token: 0x020008AC RID: 2220
		[Serializable]
		public struct FootInfo
		{
			// Token: 0x040032D8 RID: 13016
			public Transform transform;

			// Token: 0x040032D9 RID: 13017
			public Transform referenceTransform;

			// Token: 0x040032DA RID: 13018
			[HideInInspector]
			public Vector3 velocity;

			// Token: 0x040032DB RID: 13019
			[HideInInspector]
			public StriderLegController.FootState footState;

			// Token: 0x040032DC RID: 13020
			[HideInInspector]
			public Vector3 plantPosition;

			// Token: 0x040032DD RID: 13021
			[HideInInspector]
			public Vector3 trailingTargetPosition;

			// Token: 0x040032DE RID: 13022
			[HideInInspector]
			public float stopwatch;

			// Token: 0x040032DF RID: 13023
			[HideInInspector]
			public float currentYOffsetFromRaycast;

			// Token: 0x040032E0 RID: 13024
			[HideInInspector]
			public float lastYOffsetFromRaycast;

			// Token: 0x040032E1 RID: 13025
			[HideInInspector]
			public float footRaycastTimer;
		}

		// Token: 0x020008AD RID: 2221
		public enum FootState
		{
			// Token: 0x040032E3 RID: 13027
			Planted,
			// Token: 0x040032E4 RID: 13028
			Replanting
		}
	}
}
