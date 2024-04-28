using System;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BEA RID: 3050
	public class SummonedEchoBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x0600452D RID: 17709 RVA: 0x0011FF86 File Offset: 0x0011E186
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.SummonedEcho;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0011FF90 File Offset: 0x0011E190
		private void FixedUpdate()
		{
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				this.fireTimer = this.fireInterval;
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					crit = false,
					damage = base.body.damage * this.damageCoefficient,
					damageColorIndex = DamageColorIndex.Default,
					damageTypeOverride = new DamageType?(DamageType.SlowOnHit),
					owner = base.body.gameObject,
					position = base.body.aimOrigin,
					rotation = Quaternion.LookRotation(Vector3.up),
					procChainMask = default(ProcChainMask),
					projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/EchoHunterProjectile"),
					force = 400f,
					target = null
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x0400438A RID: 17290
		private float fireTimer;

		// Token: 0x0400438B RID: 17291
		private float fireInterval = 3f;

		// Token: 0x0400438C RID: 17292
		private float damageCoefficient = 3f;
	}
}
