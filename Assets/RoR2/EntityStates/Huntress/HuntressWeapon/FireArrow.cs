using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x0200031F RID: 799
	public class FireArrow : BaseState
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0003D790 File Offset: 0x0003B990
		private void FireGrenade(string targetMuzzle)
		{
			Util.PlaySound(FireArrow.attackSoundString, base.gameObject);
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
			base.AddRecoil(-1f * FireArrow.recoilAmplitude, -2f * FireArrow.recoilAmplitude, -1f * FireArrow.recoilAmplitude, 1f * FireArrow.recoilAmplitude);
			if (FireArrow.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireArrow.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				float x = UnityEngine.Random.Range(0f, base.characterBody.spreadBloomAngle);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 up = Vector3.up;
				Vector3 axis = Vector3.Cross(up, this.aimRay.direction);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f + FireArrow.arcAngle;
				Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * this.aimRay.direction);
				ProjectileManager.instance.FireProjectile(FireArrow.projectilePrefab, this.aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * this.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			base.characterBody.AddSpreadBloom(FireArrow.spreadBloomValue);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireArrow.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			if (base.characterMotor && FireArrow.smallHopStrength != 0f)
			{
				base.characterMotor.velocity.y = FireArrow.smallHopStrength;
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003DA24 File Offset: 0x0003BC24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				float num = FireArrow.fireDuration / this.attackSpeedStat / (float)FireArrow.arrowCountMax;
				if (this.fireTimer <= 0f && this.grenadeCount < FireArrow.arrowCountMax)
				{
					base.PlayAnimation("Gesture, Additive", "FireArrow", "FireArrow.playbackRate", this.duration - num);
					base.PlayAnimation("Gesture, Override", "FireArrow", "FireArrow.playbackRate", this.duration - num);
					this.FireGrenade("Muzzle");
					this.fireTimer += num;
					this.grenadeCount++;
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040011D5 RID: 4565
		public static GameObject effectPrefab;

		// Token: 0x040011D6 RID: 4566
		public static GameObject hitEffectPrefab;

		// Token: 0x040011D7 RID: 4567
		public static GameObject projectilePrefab;

		// Token: 0x040011D8 RID: 4568
		public static int arrowCountMax = 1;

		// Token: 0x040011D9 RID: 4569
		public float damageCoefficient;

		// Token: 0x040011DA RID: 4570
		public static float fireDuration = 1f;

		// Token: 0x040011DB RID: 4571
		public static float baseDuration = 2f;

		// Token: 0x040011DC RID: 4572
		public static float arcAngle = 5f;

		// Token: 0x040011DD RID: 4573
		public static float recoilAmplitude = 1f;

		// Token: 0x040011DE RID: 4574
		public static string attackSoundString;

		// Token: 0x040011DF RID: 4575
		public static float spreadBloomValue = 0.3f;

		// Token: 0x040011E0 RID: 4576
		public static float smallHopStrength;

		// Token: 0x040011E1 RID: 4577
		private Ray aimRay;

		// Token: 0x040011E2 RID: 4578
		private Transform modelTransform;

		// Token: 0x040011E3 RID: 4579
		private float duration;

		// Token: 0x040011E4 RID: 4580
		private float fireTimer;

		// Token: 0x040011E5 RID: 4581
		private int grenadeCount;
	}
}
