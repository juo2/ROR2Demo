using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A1 RID: 417
	public class DroneProjectileHover : BaseState
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x0002019C File Offset: 0x0001E39C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.rigidbody)
			{
				base.rigidbody.velocity = Vector3.zero;
				base.rigidbody.useGravity = false;
			}
			if (NetworkServer.active && base.projectileController)
			{
				this.teamFilter = base.projectileController.teamFilter;
			}
			this.interval = this.duration / (float)(this.pulseCount + 1);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00020214 File Offset: 0x0001E414
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				if (base.age >= this.duration)
				{
					EntityState.Destroy(base.gameObject);
					return;
				}
				if (base.age >= this.interval * (float)(this.pulses + 1))
				{
					this.pulses++;
					this.Pulse();
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void Pulse()
		{
		}

		// Token: 0x0400090E RID: 2318
		[SerializeField]
		public float duration;

		// Token: 0x0400090F RID: 2319
		[SerializeField]
		public int pulseCount = 3;

		// Token: 0x04000910 RID: 2320
		[SerializeField]
		public float pulseRadius = 7f;

		// Token: 0x04000911 RID: 2321
		protected TeamFilter teamFilter;

		// Token: 0x04000912 RID: 2322
		protected float interval;

		// Token: 0x04000913 RID: 2323
		protected int pulses;
	}
}
