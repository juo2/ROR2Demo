using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200084C RID: 2124
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class RigidbodyStickOnImpact : MonoBehaviour
	{
		// Token: 0x06002E59 RID: 11865 RVA: 0x000C589D File Offset: 0x000C3A9D
		private void Awake()
		{
			this.rb = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000C58AC File Offset: 0x000C3AAC
		private void Update()
		{
			if (this.stuck)
			{
				this.stopwatchSinceStuck += Time.deltaTime;
				base.transform.position = this.transformPositionWhenContacted + this.embedDistanceCurve.Evaluate(this.stopwatchSinceStuck) * this.contactNormal;
			}
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000C5908 File Offset: 0x000C3B08
		private void OnCollisionEnter(Collision collision)
		{
			if (this.stuck || this.rb.isKinematic)
			{
				return;
			}
			if (collision.transform.gameObject.layer != LayerIndex.world.intVal)
			{
				return;
			}
			if (collision.relativeVelocity.sqrMagnitude > this.minimumRelativeVelocityMagnitude * this.minimumRelativeVelocityMagnitude)
			{
				this.stuck = true;
				ContactPoint contact = collision.GetContact(0);
				this.contactNormal = contact.normal;
				this.contactPosition = contact.point;
				this.transformPositionWhenContacted = base.transform.position;
				EffectManager.SpawnEffect(this.stickEffectPrefab, new EffectData
				{
					origin = this.contactPosition,
					rotation = Util.QuaternionSafeLookRotation(this.contactNormal)
				}, false);
				Util.PlaySound(this.stickSoundString, base.gameObject);
				this.rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
				this.rb.detectCollisions = false;
				this.rb.isKinematic = true;
				this.rb.velocity = Vector3.zero;
			}
		}

		// Token: 0x04003060 RID: 12384
		private Rigidbody rb;

		// Token: 0x04003061 RID: 12385
		public string stickSoundString;

		// Token: 0x04003062 RID: 12386
		public GameObject stickEffectPrefab;

		// Token: 0x04003063 RID: 12387
		public float minimumRelativeVelocityMagnitude;

		// Token: 0x04003064 RID: 12388
		public AnimationCurve embedDistanceCurve;

		// Token: 0x04003065 RID: 12389
		private bool stuck;

		// Token: 0x04003066 RID: 12390
		private float stopwatchSinceStuck;

		// Token: 0x04003067 RID: 12391
		private Vector3 contactNormal;

		// Token: 0x04003068 RID: 12392
		private Vector3 contactPosition;

		// Token: 0x04003069 RID: 12393
		private Vector3 transformPositionWhenContacted;
	}
}
