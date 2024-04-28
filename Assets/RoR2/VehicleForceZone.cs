using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008E5 RID: 2277
	[RequireComponent(typeof(Collider))]
	public class VehicleForceZone : MonoBehaviour
	{
		// Token: 0x06003304 RID: 13060 RVA: 0x000D6E1B File Offset: 0x000D501B
		private void Start()
		{
			this.collider = base.GetComponent<Collider>();
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000D6E2C File Offset: 0x000D502C
		public void OnTriggerEnter(Collider other)
		{
			CharacterMotor component = other.GetComponent<CharacterMotor>();
			HealthComponent component2 = other.GetComponent<HealthComponent>();
			if (component && component2)
			{
				Vector3 position = base.transform.position;
				Vector3 normalized = this.vehicleRigidbody.velocity.normalized;
				Vector3 pointVelocity = this.vehicleRigidbody.GetPointVelocity(position);
				Vector3 vector = pointVelocity * this.vehicleRigidbody.mass * this.impactMultiplier;
				float mass = this.vehicleRigidbody.mass;
				Mathf.Pow(pointVelocity.magnitude, 2f);
				float num = component.mass / (component.mass + this.vehicleRigidbody.mass);
				this.vehicleRigidbody.AddForceAtPosition(-vector * num, position);
				Debug.LogFormat("Impulse: {0}, Ratio: {1}", new object[]
				{
					vector.magnitude,
					num
				});
				component2.TakeDamageForce(new DamageInfo
				{
					attacker = base.gameObject,
					force = vector,
					position = position
				}, true, false);
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000D6F54 File Offset: 0x000D5154
		public void OnCollisionEnter(Collision collision)
		{
			Debug.LogFormat("Hit {0}", new object[]
			{
				collision.gameObject
			});
			Rigidbody component = collision.collider.GetComponent<Rigidbody>();
			if (component)
			{
				Debug.Log("Hit?");
				HealthComponent component2 = component.GetComponent<HealthComponent>();
				if (component2)
				{
					Vector3 point = collision.contacts[0].point;
					Vector3 normal = collision.contacts[0].normal;
					this.vehicleRigidbody.GetPointVelocity(point);
					Vector3 impulse = collision.impulse;
					float num = 0f;
					this.vehicleRigidbody.AddForceAtPosition(impulse * num, point);
					Debug.LogFormat("Impulse: {0}, Ratio: {1}", new object[]
					{
						impulse,
						num
					});
					component2.TakeDamageForce(new DamageInfo
					{
						attacker = base.gameObject,
						force = -impulse * (1f - num),
						position = point
					}, true, false);
				}
			}
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x0400341E RID: 13342
		public Rigidbody vehicleRigidbody;

		// Token: 0x0400341F RID: 13343
		public float impactMultiplier;

		// Token: 0x04003420 RID: 13344
		private Collider collider;
	}
}
