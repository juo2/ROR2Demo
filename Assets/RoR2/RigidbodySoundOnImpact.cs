using System;
using RoR2.Audio;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200084B RID: 2123
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public class RigidbodySoundOnImpact : MonoBehaviour
	{
		// Token: 0x06002E55 RID: 11861 RVA: 0x000C57CA File Offset: 0x000C39CA
		private void Start()
		{
			this.rb = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000C57D8 File Offset: 0x000C39D8
		private void FixedUpdate()
		{
			this.ditherTimer -= Time.fixedDeltaTime;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000C57EC File Offset: 0x000C39EC
		private void OnCollisionEnter(Collision collision)
		{
			if (this.ditherTimer > 0f)
			{
				return;
			}
			if (this.rb.isKinematic)
			{
				return;
			}
			if (collision.transform.gameObject.layer != LayerIndex.world.intVal)
			{
				return;
			}
			if (collision.relativeVelocity.sqrMagnitude > this.minimumRelativeVelocityMagnitude * this.minimumRelativeVelocityMagnitude)
			{
				if (this.impactSoundString != null)
				{
					Util.PlaySound(this.impactSoundString, base.gameObject);
				}
				if (this.networkedSoundEvent != null)
				{
					PointSoundManager.EmitSoundServer(this.networkedSoundEvent.index, base.transform.position);
				}
				this.ditherTimer = 0.5f;
			}
		}

		// Token: 0x0400305B RID: 12379
		private Rigidbody rb;

		// Token: 0x0400305C RID: 12380
		public string impactSoundString;

		// Token: 0x0400305D RID: 12381
		public NetworkSoundEventDef networkedSoundEvent;

		// Token: 0x0400305E RID: 12382
		public float minimumRelativeVelocityMagnitude;

		// Token: 0x0400305F RID: 12383
		private float ditherTimer;
	}
}
