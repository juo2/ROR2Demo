using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002A7 RID: 679
	public class FireRoller : BaseState
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00031B7C File Offset: 0x0002FD7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.InitElement(MageElement.Ice);
			this.stopwatch = 0f;
			this.entryDuration = FireRoller.baseEntryDuration / this.attackSpeedStat;
			this.fireDuration = FireRoller.baseDuration / this.attackSpeedStat;
			this.exitDuration = FireRoller.baseExitDuration / this.attackSpeedStat;
			Util.PlaySound(this.attackString, base.gameObject);
			base.characterBody.SetAimTimer(this.fireDuration + this.entryDuration + this.exitDuration + 2f);
			this.animator = base.GetModelAnimator();
			if (this.animator)
			{
				this.childLocator = this.animator.GetComponent<ChildLocator>();
			}
			this.muzzleString = "MuzzleRight";
			if (this.childLocator)
			{
				this.muzzleTransform = this.childLocator.FindChild(this.muzzleString);
			}
			this.PlayAnimation("Gesture Left, Additive", "Empty");
			this.PlayAnimation("Gesture Right, Additive", "Empty");
			base.PlayAnimation("Gesture, Additive", "EnterRoller", "EnterRoller.playbackRate", this.entryDuration);
			if (this.areaIndicatorPrefab)
			{
				this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(this.areaIndicatorPrefab);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00031CC0 File Offset: 0x0002FEC0
		private void UpdateAreaIndicator()
		{
			if (this.areaIndicatorInstance)
			{
				float maxDistance = 1000f;
				RaycastHit raycastHit;
				if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					this.areaIndicatorInstance.transform.position = raycastHit.point;
					this.areaIndicatorInstance.transform.rotation = Util.QuaternionSafeLookRotation(base.transform.position - this.areaIndicatorInstance.transform.position, raycastHit.normal);
				}
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00031D55 File Offset: 0x0002FF55
		public override void Update()
		{
			base.Update();
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00031D63 File Offset: 0x0002FF63
		public override void OnExit()
		{
			base.OnExit();
			if (this.areaIndicatorInstance)
			{
				EntityState.Destroy(this.areaIndicatorInstance);
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00031D84 File Offset: 0x0002FF84
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.entryDuration && !this.hasFiredRoller)
			{
				base.PlayAnimation("Gesture, Additive", "FireRoller", "FireRoller.playbackRate", this.fireDuration);
				this.FireRollerProjectile();
				EntityState.Destroy(this.areaIndicatorInstance);
			}
			if (this.stopwatch >= this.entryDuration + this.fireDuration && !this.hasBegunExit)
			{
				this.hasBegunExit = true;
				base.PlayAnimation("Gesture, Additive", "ExitRoller", "ExitRoller.playbackRate", this.exitDuration);
			}
			if (this.stopwatch >= this.entryDuration + this.fireDuration + this.exitDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00031E5C File Offset: 0x0003005C
		private void FireRollerProjectile()
		{
			this.hasFiredRoller = true;
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, false);
			}
			if (base.isAuthority && this.projectilePrefab != null)
			{
				float maxDistance = 1000f;
				Ray aimRay = base.GetAimRay();
				Vector3 forward = aimRay.direction;
				Vector3 vector = aimRay.origin;
				float magnitude = FireRoller.targetProjectileSpeed;
				if (this.muzzleTransform)
				{
					vector = this.muzzleTransform.position;
					RaycastHit raycastHit;
					if (Physics.Raycast(aimRay, out raycastHit, maxDistance, LayerIndex.world.mask))
					{
						float num = magnitude;
						Vector3 vector2 = raycastHit.point - vector;
						Vector2 vector3 = new Vector2(vector2.x, vector2.z);
						float magnitude2 = vector3.magnitude;
						float y = Trajectory.CalculateInitialYSpeed(magnitude2 / num, vector2.y);
						Vector3 a = new Vector3(vector3.x / magnitude2 * num, y, vector3.y / magnitude2 * num);
						magnitude = a.magnitude;
						forward = a / magnitude;
					}
				}
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, vector, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * this.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, magnitude);
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00031FD4 File Offset: 0x000301D4
		private void InitElement(MageElement defaultElement)
		{
			MageCalibrationController component = base.GetComponent<MageCalibrationController>();
			if (component)
			{
				MageElement activeCalibrationElement = component.GetActiveCalibrationElement();
				if (activeCalibrationElement != MageElement.None)
				{
					defaultElement = activeCalibrationElement;
				}
			}
			switch (defaultElement)
			{
			case MageElement.Fire:
				this.damageCoefficient = FireRoller.fireDamageCoefficient;
				this.attackString = FireRoller.fireAttackSoundString;
				this.projectilePrefab = FireRoller.fireProjectilePrefab;
				this.muzzleflashEffectPrefab = FireRoller.fireMuzzleflashEffectPrefab;
				this.areaIndicatorPrefab = FireRoller.fireAreaIndicatorPrefab;
				return;
			case MageElement.Ice:
				this.damageCoefficient = FireRoller.iceDamageCoefficient;
				this.attackString = FireRoller.iceAttackSoundString;
				this.projectilePrefab = FireRoller.iceProjectilePrefab;
				this.muzzleflashEffectPrefab = FireRoller.iceMuzzleflashEffectPrefab;
				this.areaIndicatorPrefab = FireRoller.iceAreaIndicatorPrefab;
				return;
			case MageElement.Lightning:
				this.damageCoefficient = FireRoller.lightningDamageCoefficient;
				this.attackString = FireRoller.lightningAttackSoundString;
				this.projectilePrefab = FireRoller.lightningProjectilePrefab;
				this.muzzleflashEffectPrefab = FireRoller.lightningMuzzleflashEffectPrefab;
				this.areaIndicatorPrefab = FireRoller.lightningAreaIndicatorPrefab;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000E3D RID: 3645
		public static GameObject fireProjectilePrefab;

		// Token: 0x04000E3E RID: 3646
		public static GameObject iceProjectilePrefab;

		// Token: 0x04000E3F RID: 3647
		public static GameObject lightningProjectilePrefab;

		// Token: 0x04000E40 RID: 3648
		public static GameObject fireMuzzleflashEffectPrefab;

		// Token: 0x04000E41 RID: 3649
		public static GameObject iceMuzzleflashEffectPrefab;

		// Token: 0x04000E42 RID: 3650
		public static GameObject lightningMuzzleflashEffectPrefab;

		// Token: 0x04000E43 RID: 3651
		public static GameObject fireAreaIndicatorPrefab;

		// Token: 0x04000E44 RID: 3652
		public static GameObject iceAreaIndicatorPrefab;

		// Token: 0x04000E45 RID: 3653
		public static GameObject lightningAreaIndicatorPrefab;

		// Token: 0x04000E46 RID: 3654
		public static string fireAttackSoundString;

		// Token: 0x04000E47 RID: 3655
		public static string iceAttackSoundString;

		// Token: 0x04000E48 RID: 3656
		public static string lightningAttackSoundString;

		// Token: 0x04000E49 RID: 3657
		public static float targetProjectileSpeed;

		// Token: 0x04000E4A RID: 3658
		public static float baseEntryDuration = 2f;

		// Token: 0x04000E4B RID: 3659
		public static float baseDuration = 2f;

		// Token: 0x04000E4C RID: 3660
		public static float baseExitDuration = 2f;

		// Token: 0x04000E4D RID: 3661
		public static float fireDamageCoefficient;

		// Token: 0x04000E4E RID: 3662
		public static float iceDamageCoefficient;

		// Token: 0x04000E4F RID: 3663
		public static float lightningDamageCoefficient;

		// Token: 0x04000E50 RID: 3664
		private float stopwatch;

		// Token: 0x04000E51 RID: 3665
		private float fireDuration;

		// Token: 0x04000E52 RID: 3666
		private float entryDuration;

		// Token: 0x04000E53 RID: 3667
		private float exitDuration;

		// Token: 0x04000E54 RID: 3668
		private bool hasFiredRoller;

		// Token: 0x04000E55 RID: 3669
		private bool hasBegunExit;

		// Token: 0x04000E56 RID: 3670
		private GameObject areaIndicatorInstance;

		// Token: 0x04000E57 RID: 3671
		private string muzzleString;

		// Token: 0x04000E58 RID: 3672
		private Transform muzzleTransform;

		// Token: 0x04000E59 RID: 3673
		private Animator animator;

		// Token: 0x04000E5A RID: 3674
		private ChildLocator childLocator;

		// Token: 0x04000E5B RID: 3675
		private GameObject areaIndicatorPrefab;

		// Token: 0x04000E5C RID: 3676
		private float damageCoefficient = 1.2f;

		// Token: 0x04000E5D RID: 3677
		private string attackString;

		// Token: 0x04000E5E RID: 3678
		private GameObject projectilePrefab;

		// Token: 0x04000E5F RID: 3679
		private GameObject muzzleflashEffectPrefab;
	}
}
