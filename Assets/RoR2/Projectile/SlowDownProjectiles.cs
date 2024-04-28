using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BB8 RID: 3000
	[RequireComponent(typeof(Collider))]
	public class SlowDownProjectiles : MonoBehaviour
	{
		// Token: 0x06004464 RID: 17508 RVA: 0x0011CC39 File Offset: 0x0011AE39
		private void Start()
		{
			this.slowDownProjectileInfos = new List<SlowDownProjectiles.SlowDownProjectileInfo>();
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x0011CC48 File Offset: 0x0011AE48
		private void OnTriggerEnter(Collider other)
		{
			TeamFilter component = other.GetComponent<TeamFilter>();
			Rigidbody component2 = other.GetComponent<Rigidbody>();
			if (component2 && component.teamIndex != this.teamFilter.teamIndex)
			{
				this.slowDownProjectileInfos.Add(new SlowDownProjectiles.SlowDownProjectileInfo
				{
					rb = component2,
					previousVelocity = component2.velocity
				});
			}
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x0011CCA8 File Offset: 0x0011AEA8
		private void OnTriggerExit(Collider other)
		{
			TeamFilter component = other.GetComponent<TeamFilter>();
			Rigidbody component2 = other.GetComponent<Rigidbody>();
			if (component2 && component.teamIndex != this.teamFilter.teamIndex)
			{
				this.RemoveFromSlowDownProjectileInfos(component2);
			}
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x0011CCE8 File Offset: 0x0011AEE8
		private void RemoveFromSlowDownProjectileInfos(Rigidbody rb)
		{
			for (int i = 0; i < this.slowDownProjectileInfos.Count; i++)
			{
				if (this.slowDownProjectileInfos[i].rb == rb)
				{
					this.slowDownProjectileInfos.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x0011CD34 File Offset: 0x0011AF34
		private void FixedUpdate()
		{
			for (int i = 0; i < this.slowDownProjectileInfos.Count; i++)
			{
				SlowDownProjectiles.SlowDownProjectileInfo slowDownProjectileInfo = this.slowDownProjectileInfos[i];
				Rigidbody rb = slowDownProjectileInfo.rb;
				Vector3 previousVelocity = slowDownProjectileInfo.previousVelocity;
				if (rb)
				{
					rb.MovePosition(rb.position - Vector3.Lerp(previousVelocity, Vector3.zero, this.slowDownCoefficient) * Time.fixedDeltaTime);
					slowDownProjectileInfo.previousVelocity = rb.velocity;
					this.slowDownProjectileInfos[i] = slowDownProjectileInfo;
				}
				else
				{
					this.RemoveFromSlowDownProjectileInfos(rb);
				}
			}
		}

		// Token: 0x040042DD RID: 17117
		public TeamFilter teamFilter;

		// Token: 0x040042DE RID: 17118
		public float slowDownCoefficient;

		// Token: 0x040042DF RID: 17119
		private List<SlowDownProjectiles.SlowDownProjectileInfo> slowDownProjectileInfos;

		// Token: 0x02000BB9 RID: 3001
		private struct SlowDownProjectileInfo
		{
			// Token: 0x040042E0 RID: 17120
			public Rigidbody rb;

			// Token: 0x040042E1 RID: 17121
			public Vector3 previousVelocity;
		}
	}
}
