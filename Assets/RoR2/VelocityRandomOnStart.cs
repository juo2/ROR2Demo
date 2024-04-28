using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008E9 RID: 2281
	public class VelocityRandomOnStart : MonoBehaviour
	{
		// Token: 0x06003339 RID: 13113 RVA: 0x000D7F5C File Offset: 0x000D615C
		private void Start()
		{
			if (NetworkServer.active)
			{
				Rigidbody component = base.GetComponent<Rigidbody>();
				if (component)
				{
					float num = (this.minSpeed != this.maxSpeed) ? UnityEngine.Random.Range(this.minSpeed, this.maxSpeed) : this.minSpeed;
					if (num != 0f)
					{
						Vector3 vector = Vector3.zero;
						Vector3 vector2 = this.localDirection ? (base.transform.rotation * this.baseDirection) : this.baseDirection;
						switch (this.directionMode)
						{
						case VelocityRandomOnStart.DirectionMode.Sphere:
							vector = UnityEngine.Random.onUnitSphere;
							break;
						case VelocityRandomOnStart.DirectionMode.Hemisphere:
							vector = UnityEngine.Random.onUnitSphere;
							if (Vector3.Dot(vector, vector2) < 0f)
							{
								vector = -vector;
							}
							break;
						case VelocityRandomOnStart.DirectionMode.Cone:
							vector = Util.ApplySpread(vector2, 0f, this.coneAngle, 1f, 1f, 0f, 0f);
							break;
						}
						component.velocity = vector * num;
					}
					float num2 = (this.minAngularSpeed != this.maxAngularSpeed) ? UnityEngine.Random.Range(this.minAngularSpeed, this.maxAngularSpeed) : this.minAngularSpeed;
					if (num2 != 0f)
					{
						component.angularVelocity = UnityEngine.Random.onUnitSphere * (num2 * 0.017453292f);
					}
				}
			}
		}

		// Token: 0x04003447 RID: 13383
		public float minSpeed;

		// Token: 0x04003448 RID: 13384
		public float maxSpeed;

		// Token: 0x04003449 RID: 13385
		public Vector3 baseDirection = Vector3.up;

		// Token: 0x0400344A RID: 13386
		public bool localDirection;

		// Token: 0x0400344B RID: 13387
		public VelocityRandomOnStart.DirectionMode directionMode;

		// Token: 0x0400344C RID: 13388
		public float coneAngle = 30f;

		// Token: 0x0400344D RID: 13389
		[Tooltip("Minimum angular speed in degrees/second.")]
		public float minAngularSpeed;

		// Token: 0x0400344E RID: 13390
		[Tooltip("Maximum angular speed in degrees/second.")]
		public float maxAngularSpeed;

		// Token: 0x020008EA RID: 2282
		public enum DirectionMode
		{
			// Token: 0x04003450 RID: 13392
			Sphere,
			// Token: 0x04003451 RID: 13393
			Hemisphere,
			// Token: 0x04003452 RID: 13394
			Cone
		}
	}
}
