using System;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000324 RID: 804
	public class IdleTracking : BaseState
	{
		// Token: 0x06000E65 RID: 3685 RVA: 0x0003E108 File Offset: 0x0003C308
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003E136 File Offset: 0x0003C336
		public override void OnExit()
		{
			if (this.trackingIndicatorTransform)
			{
				EntityState.Destroy(this.trackingIndicatorTransform.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003E15C File Offset: 0x0003C35C
		private void FireOrbArrow()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			HuntressArrowOrb huntressArrowOrb = new HuntressArrowOrb();
			huntressArrowOrb.damageValue = base.characterBody.damage * IdleTracking.orbDamageCoefficient;
			huntressArrowOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
			huntressArrowOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			huntressArrowOrb.attacker = base.gameObject;
			huntressArrowOrb.damageColorIndex = DamageColorIndex.Poison;
			huntressArrowOrb.procChainMask.AddProc(ProcType.HealOnHit);
			huntressArrowOrb.procCoefficient = IdleTracking.orbProcCoefficient;
			HurtBox hurtBox = this.trackingTarget;
			if (hurtBox)
			{
				Transform transform = this.childLocator.FindChild(IdleTracking.muzzleString).transform;
				EffectManager.SimpleMuzzleFlash(IdleTracking.muzzleflashEffectPrefab, base.gameObject, IdleTracking.muzzleString, true);
				huntressArrowOrb.origin = transform.position;
				huntressArrowOrb.target = hurtBox;
				this.PlayAnimation("Gesture, Override", "FireSeekingArrow");
				this.PlayAnimation("Gesture, Additive", "FireSeekingArrow");
				Util.PlaySound(IdleTracking.attackSoundString, base.gameObject);
				OrbManager.instance.AddOrb(huntressArrowOrb);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003E274 File Offset: 0x0003C474
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				if (base.characterBody)
				{
					float num = 0f;
					Ray ray = CameraRigController.ModifyAimRayIfApplicable(base.GetAimRay(), base.gameObject, out num);
					BullseyeSearch bullseyeSearch = new BullseyeSearch();
					bullseyeSearch.searchOrigin = ray.origin;
					bullseyeSearch.searchDirection = ray.direction;
					bullseyeSearch.maxDistanceFilter = IdleTracking.maxTrackingDistance + num;
					bullseyeSearch.maxAngleFilter = IdleTracking.maxTrackingAngle;
					bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
					bullseyeSearch.teamMaskFilter.RemoveTeam(TeamComponent.GetObjectTeam(base.gameObject));
					bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
					bullseyeSearch.RefreshCandidates();
					this.trackingTarget = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				}
				if (this.trackingTarget)
				{
					if (!this.trackingIndicatorTransform)
					{
						this.trackingIndicatorTransform = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/ShieldTransferIndicator"), this.trackingTarget.transform.position, Quaternion.identity).transform;
					}
					this.trackingIndicatorTransform.position = this.trackingTarget.transform.position;
					if (base.inputBank && base.inputBank.skill1.down && this.fireTimer <= 0f)
					{
						this.fireTimer = 1f / IdleTracking.fireFrequency / this.attackSpeedStat;
						this.FireOrbArrow();
						return;
					}
				}
				else if (this.trackingIndicatorTransform)
				{
					EntityState.Destroy(this.trackingIndicatorTransform.gameObject);
					this.trackingIndicatorTransform = null;
				}
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x04001205 RID: 4613
		public static float maxTrackingDistance = 20f;

		// Token: 0x04001206 RID: 4614
		public static float maxTrackingAngle = 20f;

		// Token: 0x04001207 RID: 4615
		public static float orbDamageCoefficient;

		// Token: 0x04001208 RID: 4616
		public static float orbProcCoefficient;

		// Token: 0x04001209 RID: 4617
		public static string muzzleString;

		// Token: 0x0400120A RID: 4618
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x0400120B RID: 4619
		public static string attackSoundString;

		// Token: 0x0400120C RID: 4620
		public static float fireFrequency;

		// Token: 0x0400120D RID: 4621
		private float fireTimer;

		// Token: 0x0400120E RID: 4622
		private Transform trackingIndicatorTransform;

		// Token: 0x0400120F RID: 4623
		private HurtBox trackingTarget;

		// Token: 0x04001210 RID: 4624
		private ChildLocator childLocator;
	}
}
