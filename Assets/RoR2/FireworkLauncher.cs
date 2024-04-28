using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006DA RID: 1754
	public class FireworkLauncher : MonoBehaviour
	{
		// Token: 0x0600229C RID: 8860 RVA: 0x0009578C File Offset: 0x0009398C
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				if (this.remaining <= 0 || !this.owner)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
				this.nextFireTimer -= Time.fixedDeltaTime;
				if (this.nextFireTimer <= 0f)
				{
					this.remaining--;
					this.nextFireTimer += this.launchInterval;
					this.FireMissile();
				}
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00095808 File Offset: 0x00093A08
		private void FireMissile()
		{
			CharacterBody component = this.owner.GetComponent<CharacterBody>();
			if (component)
			{
				ProcChainMask procChainMask = default(ProcChainMask);
				Vector2 vector = UnityEngine.Random.insideUnitCircle * this.randomCircleRange;
				MissileUtils.FireMissile(base.transform.position + new Vector3(vector.x, 0f, vector.y), component, procChainMask, null, component.damage * this.damageCoefficient, this.crit, this.projectilePrefab, DamageColorIndex.Item, false);
			}
		}

		// Token: 0x040027B3 RID: 10163
		public GameObject projectilePrefab;

		// Token: 0x040027B4 RID: 10164
		public float launchInterval = 0.1f;

		// Token: 0x040027B5 RID: 10165
		public float damageCoefficient = 3f;

		// Token: 0x040027B6 RID: 10166
		public float coneAngle = 10f;

		// Token: 0x040027B7 RID: 10167
		public float randomCircleRange;

		// Token: 0x040027B8 RID: 10168
		[HideInInspector]
		public GameObject owner;

		// Token: 0x040027B9 RID: 10169
		[HideInInspector]
		public TeamIndex team;

		// Token: 0x040027BA RID: 10170
		[HideInInspector]
		public int remaining;

		// Token: 0x040027BB RID: 10171
		[HideInInspector]
		public bool crit;

		// Token: 0x040027BC RID: 10172
		private float nextFireTimer;
	}
}
