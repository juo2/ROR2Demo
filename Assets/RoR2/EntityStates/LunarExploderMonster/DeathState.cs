using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LunarExploderMonster
{
	// Token: 0x020002BA RID: 698
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000341E6 File Offset: 0x000323E6
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			base.PlayCrossfade("Body", "Death", "Death.playbackRate", DeathState.duration, 0.1f);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00034208 File Offset: 0x00032408
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(DeathState.muzzleName);
			if (this.muzzleTransform)
			{
				this.explosionPosition = new Vector3?(this.muzzleTransform.position);
				if (DeathState.deathPreExplosionVFX)
				{
					this.deathPreExplosionInstance = UnityEngine.Object.Instantiate<GameObject>(DeathState.deathPreExplosionVFX, this.muzzleTransform.position, this.muzzleTransform.rotation);
					this.deathPreExplosionInstance.transform.parent = this.muzzleTransform;
					this.deathPreExplosionInstance.transform.localScale = Vector3.one * DeathState.deathExplosionRadius;
					ScaleParticleSystemDuration component = this.deathPreExplosionInstance.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = DeathState.duration;
					}
				}
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000342D8 File Offset: 0x000324D8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.muzzleTransform)
			{
				this.explosionPosition = new Vector3?(this.muzzleTransform.position);
			}
			if (base.fixedAge >= DeathState.duration && !this.hasExploded)
			{
				if (base.isAuthority)
				{
					this.FireExplosion();
				}
				base.DestroyModel();
				if (NetworkServer.active)
				{
					base.DestroyBodyAsapServer();
				}
				this.hasExploded = true;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003434C File Offset: 0x0003254C
		private void FireExplosion()
		{
			if (this.hasExploded || !base.cachedModelTransform)
			{
				return;
			}
			if (base.isAuthority)
			{
				for (int i = 0; i < DeathState.projectileCount; i++)
				{
					float num = 360f / (float)DeathState.projectileCount;
					Vector3 forward = Util.QuaternionSafeLookRotation(base.cachedModelTransform.forward, base.cachedModelTransform.up) * Util.ApplySpread(Vector3.forward, 0f, 0f, 1f, 1f, num * (float)i, 0f);
					FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
					fireProjectileInfo.projectilePrefab = DeathState.projectilePrefab;
					fireProjectileInfo.position = base.characterBody.corePosition + Vector3.up * DeathState.projectileVerticalSpawnOffset;
					fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(forward);
					fireProjectileInfo.owner = base.gameObject;
					fireProjectileInfo.damage = this.damageStat * DeathState.projectileDamageCoefficient;
					fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
					ProjectileManager.instance.FireProjectile(fireProjectileInfo);
				}
				if (DeathState.deathExplosionEffect)
				{
					EffectManager.SpawnEffect(DeathState.deathExplosionEffect, new EffectData
					{
						origin = base.characterBody.corePosition,
						scale = DeathState.deathExplosionRadius
					}, true);
				}
			}
		}

		// Token: 0x04000F19 RID: 3865
		public static GameObject deathPreExplosionVFX;

		// Token: 0x04000F1A RID: 3866
		public static GameObject deathExplosionEffect;

		// Token: 0x04000F1B RID: 3867
		public static GameObject projectilePrefab;

		// Token: 0x04000F1C RID: 3868
		public static float projectileVerticalSpawnOffset;

		// Token: 0x04000F1D RID: 3869
		public static float projectileDamageCoefficient;

		// Token: 0x04000F1E RID: 3870
		public static int projectileCount;

		// Token: 0x04000F1F RID: 3871
		public static float duration;

		// Token: 0x04000F20 RID: 3872
		public static float deathExplosionRadius;

		// Token: 0x04000F21 RID: 3873
		public static string muzzleName;

		// Token: 0x04000F22 RID: 3874
		private GameObject deathPreExplosionInstance;

		// Token: 0x04000F23 RID: 3875
		private Transform muzzleTransform;

		// Token: 0x04000F24 RID: 3876
		private bool hasExploded;

		// Token: 0x04000F25 RID: 3877
		private Vector3? explosionPosition;
	}
}
