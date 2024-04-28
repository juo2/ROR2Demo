using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.ClayBoss
{
	// Token: 0x02000406 RID: 1030
	public class FireTarball : BaseState
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x00052A98 File Offset: 0x00050C98
		private void FireSingleTarball(string targetMuzzle)
		{
			base.PlayCrossfade("Body", "FireTarBall", 0.1f);
			Util.PlaySound(FireTarball.attackSoundString, base.gameObject);
			this.aimRay = base.GetAimRay();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(targetMuzzle);
					if (transform)
					{
						this.aimRay.origin = transform.position;
					}
				}
			}
			base.AddRecoil(-1f * FireTarball.recoilAmplitude, -2f * FireTarball.recoilAmplitude, -1f * FireTarball.recoilAmplitude, 1f * FireTarball.recoilAmplitude);
			if (FireTarball.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTarball.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				Vector3 forward = Vector3.ProjectOnPlane(this.aimRay.direction, Vector3.up);
				ProjectileManager.instance.FireProjectile(FireTarball.projectilePrefab, this.aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireTarball.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			base.characterBody.AddSpreadBloom(FireTarball.spreadBloomValue);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00052BEC File Offset: 0x00050DEC
		public override void OnEnter()
		{
			base.OnEnter();
			this.timeBetweenShots = FireTarball.baseTimeBetweenShots / this.attackSpeedStat;
			this.duration = (FireTarball.baseTimeBetweenShots * (float)FireTarball.tarballCountMax + FireTarball.cooldownDuration) / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00052C5C File Offset: 0x00050E5C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				if (this.tarballCount < FireTarball.tarballCountMax)
				{
					this.fireTimer += this.timeBetweenShots;
					this.FireSingleTarball("BottomMuzzle");
					this.tarballCount++;
				}
				else
				{
					this.fireTimer += 9999f;
					base.PlayCrossfade("Body", "ExitTarBall", "ExitTarBall.playbackRate", (FireTarball.cooldownDuration - FireTarball.baseTimeBetweenShots) / this.attackSpeedStat, 0.1f);
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040017D9 RID: 6105
		public static GameObject effectPrefab;

		// Token: 0x040017DA RID: 6106
		public static GameObject projectilePrefab;

		// Token: 0x040017DB RID: 6107
		public static int tarballCountMax = 3;

		// Token: 0x040017DC RID: 6108
		public static float damageCoefficient;

		// Token: 0x040017DD RID: 6109
		public static float baseTimeBetweenShots = 1f;

		// Token: 0x040017DE RID: 6110
		public static float cooldownDuration = 2f;

		// Token: 0x040017DF RID: 6111
		public static float recoilAmplitude = 1f;

		// Token: 0x040017E0 RID: 6112
		public static string attackSoundString;

		// Token: 0x040017E1 RID: 6113
		public static float spreadBloomValue = 0.3f;

		// Token: 0x040017E2 RID: 6114
		private int tarballCount;

		// Token: 0x040017E3 RID: 6115
		private Ray aimRay;

		// Token: 0x040017E4 RID: 6116
		private Transform modelTransform;

		// Token: 0x040017E5 RID: 6117
		private float duration;

		// Token: 0x040017E6 RID: 6118
		private float fireTimer;

		// Token: 0x040017E7 RID: 6119
		private float timeBetweenShots;
	}
}
