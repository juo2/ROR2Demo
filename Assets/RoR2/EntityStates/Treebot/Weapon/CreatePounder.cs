using System;
using RoR2;
using RoR2.Projectile;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000182 RID: 386
	public class CreatePounder : BaseState
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001CF38 File Offset: 0x0001B138
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = CreatePounder.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "PrepWall", "PrepWall.playbackRate", this.duration);
			Util.PlaySound(CreatePounder.prepSoundString, base.gameObject);
			this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(CreatePounder.areaIndicatorPrefab);
			this.UpdateAreaIndicator();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		private void UpdateAreaIndicator()
		{
			bool flag = this.goodPlacement;
			this.goodPlacement = false;
			this.areaIndicatorInstance.SetActive(true);
			if (this.areaIndicatorInstance)
			{
				float num = CreatePounder.maxDistance;
				float num2 = 0f;
				RaycastHit raycastHit;
				if (Physics.Raycast(CameraRigController.ModifyAimRayIfApplicable(base.GetAimRay(), base.gameObject, out num2), out raycastHit, num + num2, LayerIndex.world.mask))
				{
					this.areaIndicatorInstance.transform.position = raycastHit.point;
					this.areaIndicatorInstance.transform.up = raycastHit.normal;
					this.goodPlacement = (Vector3.Angle(Vector3.up, raycastHit.normal) < CreatePounder.maxSlopeAngle);
				}
				if (flag != this.goodPlacement || this.crosshairOverrideRequest == null)
				{
					CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
					if (overrideRequest != null)
					{
						overrideRequest.Dispose();
					}
					GameObject crosshairPrefab = this.goodPlacement ? CreatePounder.goodCrosshairPrefab : CreatePounder.badCrosshairPrefab;
					this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, crosshairPrefab, CrosshairUtils.OverridePriority.Skill);
				}
			}
			this.areaIndicatorInstance.SetActive(this.goodPlacement);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001D0BE File Offset: 0x0001B2BE
		public override void Update()
		{
			base.Update();
			this.UpdateAreaIndicator();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && !base.inputBank.skill4.down && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001D124 File Offset: 0x0001B324
		public override void OnExit()
		{
			if (this.goodPlacement)
			{
				Util.PlaySound(CreatePounder.fireSoundString, base.gameObject);
				if (this.areaIndicatorInstance && base.isAuthority)
				{
					bool crit = Util.CheckRoll(this.critStat, base.characterBody.master);
					ProjectileManager.instance.FireProjectile(CreatePounder.projectilePrefab, this.areaIndicatorInstance.transform.position, Quaternion.identity, base.gameObject, this.damageStat * CreatePounder.damageCoefficient, 0f, crit, DamageColorIndex.Default, null, -1f);
				}
			}
			else
			{
				base.skillLocator.special.AddOneStock();
			}
			EntityState.Destroy(this.areaIndicatorInstance.gameObject);
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x0400083F RID: 2111
		public static float baseDuration;

		// Token: 0x04000840 RID: 2112
		public static GameObject areaIndicatorPrefab;

		// Token: 0x04000841 RID: 2113
		public static GameObject projectilePrefab;

		// Token: 0x04000842 RID: 2114
		public static float damageCoefficient;

		// Token: 0x04000843 RID: 2115
		public static GameObject muzzleflashEffect;

		// Token: 0x04000844 RID: 2116
		public static GameObject goodCrosshairPrefab;

		// Token: 0x04000845 RID: 2117
		public static GameObject badCrosshairPrefab;

		// Token: 0x04000846 RID: 2118
		public static string prepSoundString;

		// Token: 0x04000847 RID: 2119
		public static float maxDistance;

		// Token: 0x04000848 RID: 2120
		public static string fireSoundString;

		// Token: 0x04000849 RID: 2121
		public static float maxSlopeAngle;

		// Token: 0x0400084A RID: 2122
		private float duration;

		// Token: 0x0400084B RID: 2123
		private float stopwatch;

		// Token: 0x0400084C RID: 2124
		private bool goodPlacement;

		// Token: 0x0400084D RID: 2125
		private GameObject areaIndicatorInstance;

		// Token: 0x0400084E RID: 2126
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
