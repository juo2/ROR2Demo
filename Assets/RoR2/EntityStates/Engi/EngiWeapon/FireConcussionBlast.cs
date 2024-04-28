using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A7 RID: 935
	public class FireConcussionBlast : BaseState
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x000494B8 File Offset: 0x000476B8
		private void FireGrenade(string targetMuzzle)
		{
			Util.PlaySound(FireConcussionBlast.attackSoundString, base.gameObject);
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
			base.AddRecoil(-1f * FireConcussionBlast.recoilAmplitude, -2f * FireConcussionBlast.recoilAmplitude, -1f * FireConcussionBlast.recoilAmplitude, 1f * FireConcussionBlast.recoilAmplitude);
			if (FireConcussionBlast.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireConcussionBlast.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.aimRay.origin,
					aimVector = this.aimRay.direction,
					minSpread = FireConcussionBlast.minSpread,
					maxSpread = FireConcussionBlast.maxSpread,
					damage = FireConcussionBlast.damageCoefficient * this.damageStat,
					force = FireConcussionBlast.force,
					tracerEffectPrefab = FireConcussionBlast.tracerEffectPrefab,
					muzzleName = targetMuzzle,
					hitEffectPrefab = FireConcussionBlast.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					maxDistance = FireConcussionBlast.maxDistance,
					radius = FireConcussionBlast.radius,
					stopperMask = 0
				}.Fire();
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00049654 File Offset: 0x00047854
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireConcussionBlast.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 2f, false);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000496A4 File Offset: 0x000478A4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				float num = FireConcussionBlast.fireDuration / this.attackSpeedStat / (float)FireConcussionBlast.grenadeCountMax;
				if (this.fireTimer <= 0f && this.grenadeCount < FireConcussionBlast.grenadeCountMax)
				{
					this.fireTimer += num;
					if (this.grenadeCount % 2 == 0)
					{
						this.FireGrenade("MuzzleLeft");
						base.PlayCrossfade("Gesture, Left Cannon", "FireGrenadeLeft", 0.1f);
					}
					else
					{
						this.FireGrenade("MuzzleRight");
						base.PlayCrossfade("Gesture, Right Cannon", "FireGrenadeRight", 0.1f);
					}
					this.grenadeCount++;
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001510 RID: 5392
		public static GameObject effectPrefab;

		// Token: 0x04001511 RID: 5393
		public static GameObject hitEffectPrefab;

		// Token: 0x04001512 RID: 5394
		public static int grenadeCountMax = 3;

		// Token: 0x04001513 RID: 5395
		public static float damageCoefficient;

		// Token: 0x04001514 RID: 5396
		public static float fireDuration = 1f;

		// Token: 0x04001515 RID: 5397
		public static float baseDuration = 2f;

		// Token: 0x04001516 RID: 5398
		public static float minSpread = 0f;

		// Token: 0x04001517 RID: 5399
		public static float maxSpread = 5f;

		// Token: 0x04001518 RID: 5400
		public static float recoilAmplitude = 1f;

		// Token: 0x04001519 RID: 5401
		public static string attackSoundString;

		// Token: 0x0400151A RID: 5402
		public static float force;

		// Token: 0x0400151B RID: 5403
		public static float maxDistance;

		// Token: 0x0400151C RID: 5404
		public static float radius;

		// Token: 0x0400151D RID: 5405
		public static GameObject tracerEffectPrefab;

		// Token: 0x0400151E RID: 5406
		private Ray aimRay;

		// Token: 0x0400151F RID: 5407
		private Transform modelTransform;

		// Token: 0x04001520 RID: 5408
		private float duration;

		// Token: 0x04001521 RID: 5409
		private float fireTimer;

		// Token: 0x04001522 RID: 5410
		private int grenadeCount;
	}
}
