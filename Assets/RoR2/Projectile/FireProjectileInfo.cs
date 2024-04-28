using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000BA7 RID: 2983
	public struct FireProjectileInfo
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x00119CEB File Offset: 0x00117EEB
		// (set) Token: 0x060043C5 RID: 17349 RVA: 0x00119CD1 File Offset: 0x00117ED1
		public float speedOverride
		{
			get
			{
				if (!this.useSpeedOverride)
				{
					return -1f;
				}
				return this._speedOverride;
			}
			set
			{
				this.useSpeedOverride = (value != -1f);
				this._speedOverride = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x00119D1B File Offset: 0x00117F1B
		// (set) Token: 0x060043C7 RID: 17351 RVA: 0x00119D01 File Offset: 0x00117F01
		public float fuseOverride
		{
			get
			{
				if (!this.useFuseOverride)
				{
					return -1f;
				}
				return this._fuseOverride;
			}
			set
			{
				this.useFuseOverride = (value != -1f);
				this._fuseOverride = value;
			}
		}

		// Token: 0x04004233 RID: 16947
		public GameObject projectilePrefab;

		// Token: 0x04004234 RID: 16948
		public Vector3 position;

		// Token: 0x04004235 RID: 16949
		public Quaternion rotation;

		// Token: 0x04004236 RID: 16950
		public GameObject owner;

		// Token: 0x04004237 RID: 16951
		public GameObject target;

		// Token: 0x04004238 RID: 16952
		public bool useSpeedOverride;

		// Token: 0x04004239 RID: 16953
		private float _speedOverride;

		// Token: 0x0400423A RID: 16954
		public bool useFuseOverride;

		// Token: 0x0400423B RID: 16955
		private float _fuseOverride;

		// Token: 0x0400423C RID: 16956
		public float damage;

		// Token: 0x0400423D RID: 16957
		public float force;

		// Token: 0x0400423E RID: 16958
		public bool crit;

		// Token: 0x0400423F RID: 16959
		public DamageColorIndex damageColorIndex;

		// Token: 0x04004240 RID: 16960
		public ProcChainMask procChainMask;

		// Token: 0x04004241 RID: 16961
		public DamageType? damageTypeOverride;
	}
}
