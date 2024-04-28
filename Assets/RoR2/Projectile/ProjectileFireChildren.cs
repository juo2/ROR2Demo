using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B8F RID: 2959
	public class ProjectileFireChildren : MonoBehaviour
	{
		// Token: 0x06004364 RID: 17252 RVA: 0x00117DA8 File Offset: 0x00115FA8
		private void Start()
		{
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.projectileController = base.GetComponent<ProjectileController>();
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x00117DC4 File Offset: 0x00115FC4
		private void Update()
		{
			this.timer += Time.deltaTime;
			this.nextSpawnTimer += Time.deltaTime;
			if (this.timer >= this.duration)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.nextSpawnTimer >= this.duration / (float)this.count)
			{
				this.nextSpawnTimer -= this.duration / (float)this.count;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.childProjectilePrefab, base.transform.position, Util.QuaternionSafeLookRotation(base.transform.forward));
				ProjectileController component = gameObject.GetComponent<ProjectileController>();
				if (component)
				{
					component.procChainMask = this.projectileController.procChainMask;
					component.procCoefficient = this.projectileController.procCoefficient * this.childProcCoefficient;
					component.Networkowner = this.projectileController.owner;
				}
				gameObject.GetComponent<TeamFilter>().teamIndex = base.GetComponent<TeamFilter>().teamIndex;
				ProjectileDamage component2 = gameObject.GetComponent<ProjectileDamage>();
				if (component2)
				{
					component2.damage = this.projectileDamage.damage * this.childDamageCoefficient;
					component2.crit = this.projectileDamage.crit;
					component2.force = this.projectileDamage.force;
					component2.damageColorIndex = this.projectileDamage.damageColorIndex;
				}
				NetworkServer.Spawn(gameObject);
			}
		}

		// Token: 0x040041AD RID: 16813
		public float duration = 5f;

		// Token: 0x040041AE RID: 16814
		public int count = 5;

		// Token: 0x040041AF RID: 16815
		public GameObject childProjectilePrefab;

		// Token: 0x040041B0 RID: 16816
		private float timer;

		// Token: 0x040041B1 RID: 16817
		private float nextSpawnTimer;

		// Token: 0x040041B2 RID: 16818
		public float childDamageCoefficient = 1f;

		// Token: 0x040041B3 RID: 16819
		public float childProcCoefficient = 1f;

		// Token: 0x040041B4 RID: 16820
		private ProjectileDamage projectileDamage;

		// Token: 0x040041B5 RID: 16821
		private ProjectileController projectileController;

		// Token: 0x040041B6 RID: 16822
		public bool ignoreParentForChainController;
	}
}
