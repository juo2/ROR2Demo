using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B1C RID: 2844
	public class Orb
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0010C85F File Offset: 0x0010AA5F
		// (set) Token: 0x060040DA RID: 16602 RVA: 0x0010C867 File Offset: 0x0010AA67
		public float duration { get; protected set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x0010C870 File Offset: 0x0010AA70
		public float timeUntilArrival
		{
			get
			{
				return this.arrivalTime - OrbManager.instance.time;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x0010C883 File Offset: 0x0010AA83
		protected float distanceToTarget
		{
			get
			{
				if (this.target)
				{
					return Vector3.Distance(this.target.transform.position, this.origin);
				}
				return 0f;
			}
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void Begin()
		{
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnArrival()
		{
		}

		// Token: 0x04003F4E RID: 16206
		public Vector3 origin;

		// Token: 0x04003F4F RID: 16207
		public HurtBox target;

		// Token: 0x04003F51 RID: 16209
		public float arrivalTime;

		// Token: 0x04003F52 RID: 16210
		public Orb nextOrb;
	}
}
