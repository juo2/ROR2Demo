using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Huntress
{
	// Token: 0x02000318 RID: 792
	public class BackflipState : BaseState
	{
		// Token: 0x06000E27 RID: 3623 RVA: 0x0003C65C File Offset: 0x0003A85C
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			this.childLocator = modelTransform.GetComponent<ChildLocator>();
			base.characterMotor.velocity.y = Mathf.Max(base.characterMotor.velocity.y, 0f);
			this.animator = base.GetModelAnimator();
			Util.PlaySound(BackflipState.dodgeSoundString, base.gameObject);
			this.orbStopwatch = -BackflipState.orbPrefireDuration;
			if (base.characterMotor && BackflipState.smallHopStrength != 0f)
			{
				base.characterMotor.velocity.y = BackflipState.smallHopStrength;
			}
			if (base.isAuthority && base.inputBank)
			{
				this.forwardDirection = -Vector3.ProjectOnPlane(base.inputBank.aimDirection, Vector3.up);
			}
			base.characterDirection.moveVector = -this.forwardDirection;
			base.PlayAnimation("FullBody, Override", "Backflip", "Backflip.playbackRate", BackflipState.duration);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003C768 File Offset: 0x0003A968
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.orbStopwatch += Time.fixedDeltaTime;
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = Mathf.Lerp(BackflipState.dodgeFOV, 60f, this.stopwatch / BackflipState.duration);
			}
			if (base.characterMotor && base.characterDirection)
			{
				Vector3 velocity = base.characterMotor.velocity;
				Vector3 velocity2 = this.forwardDirection * (this.moveSpeedStat * Mathf.Lerp(BackflipState.initialSpeedCoefficient, BackflipState.finalSpeedCoefficient, this.stopwatch / BackflipState.duration));
				base.characterMotor.velocity = velocity2;
				base.characterMotor.velocity.y = velocity.y;
				base.characterMotor.moveDirection = this.forwardDirection;
			}
			if (this.orbStopwatch >= 1f / BackflipState.orbFrequency / this.attackSpeedStat && this.orbCount < BackflipState.orbCountMax)
			{
				this.orbStopwatch -= 1f / BackflipState.orbFrequency / this.attackSpeedStat;
				this.FireOrbArrow();
			}
			if (this.stopwatch >= BackflipState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003C8C8 File Offset: 0x0003AAC8
		private void FireOrbArrow()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.orbCount++;
			HuntressArrowOrb huntressArrowOrb = new HuntressArrowOrb();
			huntressArrowOrb.damageValue = base.characterBody.damage * BackflipState.orbDamageCoefficient;
			huntressArrowOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
			huntressArrowOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			huntressArrowOrb.attacker = base.gameObject;
			huntressArrowOrb.damageColorIndex = DamageColorIndex.Poison;
			huntressArrowOrb.procChainMask.AddProc(ProcType.HealOnHit);
			huntressArrowOrb.procCoefficient = BackflipState.orbProcCoefficient;
			Ray aimRay = base.GetAimRay();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.maxDistanceFilter = BackflipState.orbRange;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(huntressArrowOrb.teamIndex);
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.RefreshCandidates();
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			HurtBox hurtBox = (list.Count > 0) ? list[UnityEngine.Random.Range(0, list.Count)] : null;
			if (hurtBox)
			{
				Transform transform = this.childLocator.FindChild(BackflipState.muzzleString).transform;
				EffectManager.SimpleMuzzleFlash(BackflipState.muzzleflashEffectPrefab, base.gameObject, BackflipState.muzzleString, true);
				huntressArrowOrb.origin = transform.position;
				huntressArrowOrb.target = hurtBox;
				this.PlayAnimation("Gesture, Override", "FireSeekingArrow");
				this.PlayAnimation("Gesture, Additive", "FireSeekingArrow");
				OrbManager.instance.AddOrb(huntressArrowOrb);
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		public override void OnExit()
		{
			base.OnExit();
			if (base.cameraTargetParams)
			{
				base.cameraTargetParams.fovOverride = -1f;
			}
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 1.5f);
				this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
		}

		// Token: 0x0400118B RID: 4491
		public static float duration = 0.9f;

		// Token: 0x0400118C RID: 4492
		public static float initialSpeedCoefficient;

		// Token: 0x0400118D RID: 4493
		public static float finalSpeedCoefficient;

		// Token: 0x0400118E RID: 4494
		public static string dodgeSoundString;

		// Token: 0x0400118F RID: 4495
		public static float dodgeFOV;

		// Token: 0x04001190 RID: 4496
		public static float orbDamageCoefficient;

		// Token: 0x04001191 RID: 4497
		public static float orbRange;

		// Token: 0x04001192 RID: 4498
		public static int orbCountMax;

		// Token: 0x04001193 RID: 4499
		public static float orbPrefireDuration;

		// Token: 0x04001194 RID: 4500
		public static float orbFrequency;

		// Token: 0x04001195 RID: 4501
		public static float orbProcCoefficient;

		// Token: 0x04001196 RID: 4502
		public static string muzzleString;

		// Token: 0x04001197 RID: 4503
		public static float smallHopStrength;

		// Token: 0x04001198 RID: 4504
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04001199 RID: 4505
		private ChildLocator childLocator;

		// Token: 0x0400119A RID: 4506
		private float stopwatch;

		// Token: 0x0400119B RID: 4507
		private float orbStopwatch;

		// Token: 0x0400119C RID: 4508
		private Vector3 forwardDirection;

		// Token: 0x0400119D RID: 4509
		private Animator animator;

		// Token: 0x0400119E RID: 4510
		private int orbCount;
	}
}
