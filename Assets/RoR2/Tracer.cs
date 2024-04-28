using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020008CB RID: 2251
	[RequireComponent(typeof(EffectComponent))]
	public class Tracer : MonoBehaviour
	{
		// Token: 0x06003273 RID: 12915 RVA: 0x000D4FDC File Offset: 0x000D31DC
		private void Start()
		{
			EffectComponent component = base.GetComponent<EffectComponent>();
			this.endPos = component.effectData.origin;
			Transform transform = component.effectData.ResolveChildLocatorTransformReference();
			this.startPos = (transform ? transform.position : component.effectData.start);
			if (this.reverse)
			{
				Util.Swap<Vector3>(ref this.endPos, ref this.startPos);
			}
			Vector3 vector = this.endPos - this.startPos;
			this.distanceTraveled = 0f;
			this.totalDistance = Vector3.Magnitude(vector);
			if (this.totalDistance != 0f)
			{
				this.normal = vector * (1f / this.totalDistance);
				base.transform.rotation = Util.QuaternionSafeLookRotation(this.normal);
			}
			else
			{
				this.normal = Vector3.zero;
			}
			if (this.beamObject)
			{
				this.beamObject.transform.position = this.startPos + vector * 0.5f;
				ParticleSystem component2 = this.beamObject.GetComponent<ParticleSystem>();
				if (component2)
				{
					component2.shape.radius = this.totalDistance * 0.5f;
					component2.Emit(Mathf.FloorToInt(this.totalDistance * this.beamDensity) - 1);
				}
			}
			if (this.startTransform)
			{
				this.startTransform.position = this.startPos;
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000D5154 File Offset: 0x000D3354
		private void Update()
		{
			if (this.distanceTraveled > this.totalDistance)
			{
				this.onTailReachedDestination.Invoke();
				return;
			}
			this.distanceTraveled += this.speed * Time.deltaTime;
			float d = Mathf.Clamp(this.distanceTraveled, 0f, this.totalDistance);
			float d2 = Mathf.Clamp(this.distanceTraveled - this.length, 0f, this.totalDistance);
			if (this.headTransform)
			{
				this.headTransform.position = this.startPos + d * this.normal;
			}
			if (this.tailTransform)
			{
				this.tailTransform.position = this.startPos + d2 * this.normal;
			}
		}

		// Token: 0x04003391 RID: 13201
		[Tooltip("A child transform which will be placed at the start of the tracer path upon creation.")]
		public Transform startTransform;

		// Token: 0x04003392 RID: 13202
		[Tooltip("Child object to scale to the length of this tracer and burst particles on based on that length. Optional.")]
		public GameObject beamObject;

		// Token: 0x04003393 RID: 13203
		[Tooltip("The number of particles to emit per meter of length if using a beam object.")]
		public float beamDensity = 10f;

		// Token: 0x04003394 RID: 13204
		[Tooltip("The travel speed of this tracer.")]
		public float speed = 1f;

		// Token: 0x04003395 RID: 13205
		[Tooltip("Child transform which will be moved to the head of the tracer.")]
		public Transform headTransform;

		// Token: 0x04003396 RID: 13206
		[Tooltip("Child transform which will be moved to the tail of the tracer.")]
		public Transform tailTransform;

		// Token: 0x04003397 RID: 13207
		[Tooltip("The maximum distance between head and tail transforms.")]
		public float length = 1f;

		// Token: 0x04003398 RID: 13208
		[Tooltip("Reverses the travel direction of the tracer.")]
		public bool reverse;

		// Token: 0x04003399 RID: 13209
		[Tooltip("The event that runs when the tail reaches the destination.")]
		public UnityEvent onTailReachedDestination;

		// Token: 0x0400339A RID: 13210
		private Vector3 startPos;

		// Token: 0x0400339B RID: 13211
		private Vector3 endPos;

		// Token: 0x0400339C RID: 13212
		private float distanceTraveled;

		// Token: 0x0400339D RID: 13213
		private float totalDistance;

		// Token: 0x0400339E RID: 13214
		private Vector3 normal;
	}
}
