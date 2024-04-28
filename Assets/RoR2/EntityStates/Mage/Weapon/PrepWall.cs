using System;
using RoR2;
using RoR2.Projectile;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002AA RID: 682
	public class PrepWall : BaseState
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x00032784 File Offset: 0x00030984
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepWall.baseDuration / this.attackSpeedStat;
			base.characterBody.SetAimTimer(this.duration + 2f);
			base.PlayAnimation("Gesture, Additive", "PrepWall", "PrepWall.playbackRate", this.duration);
			Util.PlaySound(PrepWall.prepWallSoundString, base.gameObject);
			this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(PrepWall.areaIndicatorPrefab);
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00032804 File Offset: 0x00030A04
		private void UpdateAreaIndicator()
		{
			bool flag = this.goodPlacement;
			this.goodPlacement = false;
			this.areaIndicatorInstance.SetActive(true);
			if (this.areaIndicatorInstance)
			{
				float num = PrepWall.maxDistance;
				float num2 = 0f;
				Ray aimRay = base.GetAimRay();
				RaycastHit raycastHit;
				if (Physics.Raycast(CameraRigController.ModifyAimRayIfApplicable(aimRay, base.gameObject, out num2), out raycastHit, num + num2, LayerIndex.world.mask))
				{
					this.areaIndicatorInstance.transform.position = raycastHit.point;
					this.areaIndicatorInstance.transform.up = raycastHit.normal;
					this.areaIndicatorInstance.transform.forward = -aimRay.direction;
					this.goodPlacement = (Vector3.Angle(Vector3.up, raycastHit.normal) < PrepWall.maxSlopeAngle);
				}
				if (flag != this.goodPlacement || this.crosshairOverrideRequest == null)
				{
					CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
					if (overrideRequest != null)
					{
						overrideRequest.Dispose();
					}
					GameObject crosshairPrefab = this.goodPlacement ? PrepWall.goodCrosshairPrefab : PrepWall.badCrosshairPrefab;
					this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, crosshairPrefab, CrosshairUtils.OverridePriority.Skill);
				}
			}
			this.areaIndicatorInstance.SetActive(this.goodPlacement);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00032942 File Offset: 0x00030B42
		public override void Update()
		{
			base.Update();
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00032950 File Offset: 0x00030B50
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && !base.inputBank.skill3.down && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000329A8 File Offset: 0x00030BA8
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				if (this.goodPlacement)
				{
					this.PlayAnimation("Gesture, Additive", "FireWall");
					Util.PlaySound(PrepWall.fireSoundString, base.gameObject);
					if (this.areaIndicatorInstance && base.isAuthority)
					{
						EffectManager.SimpleMuzzleFlash(PrepWall.muzzleflashEffect, base.gameObject, "MuzzleLeft", true);
						EffectManager.SimpleMuzzleFlash(PrepWall.muzzleflashEffect, base.gameObject, "MuzzleRight", true);
						Vector3 forward = this.areaIndicatorInstance.transform.forward;
						forward.y = 0f;
						forward.Normalize();
						Vector3 vector = Vector3.Cross(Vector3.up, forward);
						bool crit = Util.CheckRoll(this.critStat, base.characterBody.master);
						ProjectileManager.instance.FireProjectile(PrepWall.projectilePrefab, this.areaIndicatorInstance.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(vector), base.gameObject, this.damageStat * PrepWall.damageCoefficient, 0f, crit, DamageColorIndex.Default, null, -1f);
						ProjectileManager.instance.FireProjectile(PrepWall.projectilePrefab, this.areaIndicatorInstance.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(-vector), base.gameObject, this.damageStat * PrepWall.damageCoefficient, 0f, crit, DamageColorIndex.Default, null, -1f);
					}
				}
				else
				{
					base.skillLocator.utility.AddOneStock();
					base.PlayCrossfade("Gesture, Additive", "BufferEmpty", 0.2f);
				}
			}
			EntityState.Destroy(this.areaIndicatorInstance.gameObject);
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04000E89 RID: 3721
		public static float baseDuration;

		// Token: 0x04000E8A RID: 3722
		public static GameObject areaIndicatorPrefab;

		// Token: 0x04000E8B RID: 3723
		public static GameObject projectilePrefab;

		// Token: 0x04000E8C RID: 3724
		public static float damageCoefficient;

		// Token: 0x04000E8D RID: 3725
		public static GameObject muzzleflashEffect;

		// Token: 0x04000E8E RID: 3726
		public static GameObject goodCrosshairPrefab;

		// Token: 0x04000E8F RID: 3727
		public static GameObject badCrosshairPrefab;

		// Token: 0x04000E90 RID: 3728
		public static string prepWallSoundString;

		// Token: 0x04000E91 RID: 3729
		public static float maxDistance;

		// Token: 0x04000E92 RID: 3730
		public static string fireSoundString;

		// Token: 0x04000E93 RID: 3731
		public static float maxSlopeAngle;

		// Token: 0x04000E94 RID: 3732
		private float duration;

		// Token: 0x04000E95 RID: 3733
		private float stopwatch;

		// Token: 0x04000E96 RID: 3734
		private bool goodPlacement;

		// Token: 0x04000E97 RID: 3735
		private GameObject areaIndicatorInstance;

		// Token: 0x04000E98 RID: 3736
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
