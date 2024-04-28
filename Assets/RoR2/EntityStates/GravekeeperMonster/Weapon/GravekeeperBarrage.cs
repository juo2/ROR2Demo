using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GravekeeperMonster.Weapon
{
	// Token: 0x02000348 RID: 840
	public class GravekeeperBarrage : BaseState
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00040CF8 File Offset: 0x0003EEF8
		public override void OnEnter()
		{
			base.OnEnter();
			this.missileStopwatch -= GravekeeperBarrage.missileSpawnDelay;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					this.childLocator.FindChild("JarEffectLoop").gameObject.SetActive(true);
				}
			}
			this.PlayAnimation("Jar, Override", "BeginGravekeeperBarrage");
			EffectManager.SimpleMuzzleFlash(GravekeeperBarrage.jarOpenEffectPrefab, base.gameObject, GravekeeperBarrage.jarEffectChildLocatorString, false);
			Util.PlaySound(GravekeeperBarrage.jarOpenSoundString, base.gameObject);
			base.characterBody.SetAimTimer(GravekeeperBarrage.baseDuration + 2f);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		private void FireBlob(Ray projectileRay, float bonusPitch, float bonusYaw)
		{
			projectileRay.direction = Util.ApplySpread(projectileRay.direction, 0f, GravekeeperBarrage.maxSpread, 1f, 1f, bonusYaw, bonusPitch);
			EffectManager.SimpleMuzzleFlash(GravekeeperBarrage.muzzleflashPrefab, base.gameObject, GravekeeperBarrage.muzzleString, false);
			if (NetworkServer.active)
			{
				ProjectileManager.instance.FireProjectile(GravekeeperBarrage.projectilePrefab, projectileRay.origin, Util.QuaternionSafeLookRotation(projectileRay.direction), base.gameObject, this.damageStat * GravekeeperBarrage.damageCoefficient, GravekeeperBarrage.missileForce, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00040E5C File Offset: 0x0003F05C
		public override void OnExit()
		{
			base.PlayCrossfade("Jar, Override", "EndGravekeeperBarrage", 0.06f);
			EffectManager.SimpleMuzzleFlash(GravekeeperBarrage.jarCloseEffectPrefab, base.gameObject, GravekeeperBarrage.jarEffectChildLocatorString, false);
			Util.PlaySound(GravekeeperBarrage.jarCloseSoundString, base.gameObject);
			if (this.childLocator)
			{
				this.childLocator.FindChild("JarEffectLoop").gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00040ED4 File Offset: 0x0003F0D4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.missileStopwatch += Time.fixedDeltaTime;
			if (this.missileStopwatch >= 1f / GravekeeperBarrage.missileSpawnFrequency)
			{
				this.missileStopwatch -= 1f / GravekeeperBarrage.missileSpawnFrequency;
				Transform transform = this.childLocator.FindChild(GravekeeperBarrage.muzzleString);
				if (transform)
				{
					Ray projectileRay = default(Ray);
					projectileRay.origin = transform.position;
					projectileRay.direction = base.GetAimRay().direction;
					float maxDistance = 1000f;
					RaycastHit raycastHit;
					if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask))
					{
						projectileRay.direction = raycastHit.point - transform.position;
					}
					this.FireBlob(projectileRay, 0f, 0f);
				}
			}
			if (this.stopwatch >= GravekeeperBarrage.baseDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040012C5 RID: 4805
		private float stopwatch;

		// Token: 0x040012C6 RID: 4806
		private float missileStopwatch;

		// Token: 0x040012C7 RID: 4807
		public static float baseDuration;

		// Token: 0x040012C8 RID: 4808
		public static string muzzleString;

		// Token: 0x040012C9 RID: 4809
		public static float missileSpawnFrequency;

		// Token: 0x040012CA RID: 4810
		public static float missileSpawnDelay;

		// Token: 0x040012CB RID: 4811
		public static float missileForce;

		// Token: 0x040012CC RID: 4812
		public static float damageCoefficient;

		// Token: 0x040012CD RID: 4813
		public static float maxSpread;

		// Token: 0x040012CE RID: 4814
		public static GameObject projectilePrefab;

		// Token: 0x040012CF RID: 4815
		public static GameObject muzzleflashPrefab;

		// Token: 0x040012D0 RID: 4816
		public static string jarEffectChildLocatorString;

		// Token: 0x040012D1 RID: 4817
		public static string jarOpenSoundString;

		// Token: 0x040012D2 RID: 4818
		public static string jarCloseSoundString;

		// Token: 0x040012D3 RID: 4819
		public static GameObject jarOpenEffectPrefab;

		// Token: 0x040012D4 RID: 4820
		public static GameObject jarCloseEffectPrefab;

		// Token: 0x040012D5 RID: 4821
		private ChildLocator childLocator;
	}
}
