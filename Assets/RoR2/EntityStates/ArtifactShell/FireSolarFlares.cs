using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ArtifactShell
{
	// Token: 0x02000494 RID: 1172
	public class FireSolarFlares : BaseState
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x0005D020 File Offset: 0x0005B220
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.healthComponent)
			{
				this.projectileCount = (int)Util.Remap(base.healthComponent.combinedHealthFraction, 0f, 1f, (float)FireSolarFlares.maximumProjectileCount, (float)FireSolarFlares.minimumProjectileCount);
			}
			if (NetworkServer.active)
			{
				Vector3 aimDirection = base.inputBank.aimDirection;
				this.currentRotation = Quaternion.LookRotation(aimDirection);
				float num = Run.FixedTimeStamp.now.t * 2f % 1f;
				Vector3 rhs = this.currentRotation * Vector3.forward;
				Vector3 lhs = this.currentRotation * Vector3.right;
				this.currentRotation * Vector3.up;
				this.deltaRotation = Quaternion.AngleAxis(FireSolarFlares.arc / (float)this.projectileCount, Vector3.Cross(lhs, rhs));
			}
			this.duration = FireSolarFlares.baseDuration / this.attackSpeedStat;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0005D10C File Offset: 0x0005B30C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				float num = this.duration / (float)this.projectileCount;
				if (base.fixedAge >= (float)this.projectilesFired * num)
				{
					this.projectilesFired++;
					FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
					fireProjectileInfo.owner = base.gameObject;
					fireProjectileInfo.position = base.transform.position + this.currentRotation * Vector3.forward * FireSolarFlares.radius;
					fireProjectileInfo.rotation = this.currentRotation;
					fireProjectileInfo.projectilePrefab = FireSolarFlares.projectilePrefab;
					fireProjectileInfo.fuseOverride = FireSolarFlares.projectileFuse;
					fireProjectileInfo.useFuseOverride = true;
					fireProjectileInfo.speedOverride = FireSolarFlares.projectileSpeed;
					fireProjectileInfo.useSpeedOverride = true;
					fireProjectileInfo.damage = this.damageStat * FireSolarFlares.projectileDamageCoefficient;
					fireProjectileInfo.force = FireSolarFlares.projectileForce;
					ProjectileManager.instance.FireProjectile(fireProjectileInfo);
					this.currentRotation *= this.deltaRotation;
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x04001AC5 RID: 6853
		public static GameObject projectilePrefab;

		// Token: 0x04001AC6 RID: 6854
		public static int minimumProjectileCount;

		// Token: 0x04001AC7 RID: 6855
		public static int maximumProjectileCount;

		// Token: 0x04001AC8 RID: 6856
		public static float arc;

		// Token: 0x04001AC9 RID: 6857
		public static float baseDuration;

		// Token: 0x04001ACA RID: 6858
		public static float radius;

		// Token: 0x04001ACB RID: 6859
		public static float projectileDamageCoefficient;

		// Token: 0x04001ACC RID: 6860
		public static float projectileForce;

		// Token: 0x04001ACD RID: 6861
		public static float projectileFuse;

		// Token: 0x04001ACE RID: 6862
		public static float projectileSpeed;

		// Token: 0x04001ACF RID: 6863
		private float duration;

		// Token: 0x04001AD0 RID: 6864
		private int projectileCount;

		// Token: 0x04001AD1 RID: 6865
		private int projectilesFired;

		// Token: 0x04001AD2 RID: 6866
		private Quaternion currentRotation;

		// Token: 0x04001AD3 RID: 6867
		private Quaternion deltaRotation;
	}
}
