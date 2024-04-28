using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x0200031E RID: 798
	public class ChargeArrow : BaseState
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003D358 File Offset: 0x0003B558
		public override void OnEnter()
		{
			base.OnEnter();
			this.totalDuration = ChargeArrow.baseTotalDuration / this.attackSpeedStat;
			this.maxChargeTime = ChargeArrow.baseMaxChargeTime / this.attackSpeedStat;
			this.muzzleString = "Muzzle";
			Transform modelTransform = base.GetModelTransform();
			this.childLocator = modelTransform.GetComponent<ChildLocator>();
			this.animator = base.GetModelAnimator();
			this.cachedSprinting = base.characterBody.isSprinting;
			if (!this.cachedSprinting)
			{
				this.animator.SetBool("chargingArrow", true);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.maxChargeTime + 1f);
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0003D408 File Offset: 0x0003B608
		public override void OnExit()
		{
			base.OnExit();
			this.animator.SetBool("chargingArrow", false);
			if (!this.cachedSprinting)
			{
				this.PlayAnimation("Gesture, Override", "BufferEmpty");
				this.PlayAnimation("Gesture, Additive", "BufferEmpty");
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003D454 File Offset: 0x0003B654
		private void FireOrbArrow()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			HuntressArrowOrb huntressArrowOrb = new HuntressArrowOrb();
			huntressArrowOrb.damageValue = base.characterBody.damage * ChargeArrow.orbDamageCoefficient;
			huntressArrowOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
			huntressArrowOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			huntressArrowOrb.attacker = base.gameObject;
			huntressArrowOrb.damageColorIndex = DamageColorIndex.Poison;
			huntressArrowOrb.procChainMask.AddProc(ProcType.HealOnHit);
			huntressArrowOrb.procCoefficient = ChargeArrow.orbProcCoefficient;
			Ray aimRay = base.GetAimRay();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.maxDistanceFilter = ChargeArrow.orbRange;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(huntressArrowOrb.teamIndex);
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.RefreshCandidates();
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			HurtBox hurtBox = (list.Count > 0) ? list[UnityEngine.Random.Range(0, list.Count)] : null;
			if (hurtBox)
			{
				Transform transform = this.childLocator.FindChild(this.muzzleString).transform;
				EffectManager.SimpleMuzzleFlash(ChargeArrow.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, true);
				huntressArrowOrb.origin = transform.position;
				huntressArrowOrb.target = hurtBox;
				this.PlayAnimation("Gesture, Override", "FireSeekingArrow");
				this.PlayAnimation("Gesture, Additive", "FireSeekingArrow");
				OrbManager.instance.AddOrb(huntressArrowOrb);
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003D5DC File Offset: 0x0003B7DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.cachedSprinting != base.characterBody.isSprinting && base.isAuthority)
			{
				Debug.Log("switched states");
				this.outer.SetNextStateToMain();
				return;
			}
			if (!this.cachedSprinting)
			{
				this.lastCharge = this.charge;
				this.stopwatch += Time.fixedDeltaTime;
				this.charge = Mathf.Min((int)(this.stopwatch / this.maxChargeTime * (float)ChargeArrow.maxCharges), ChargeArrow.maxCharges);
				float damageCoefficient = Mathf.Lerp(ChargeArrow.minArrowDamageCoefficient, ChargeArrow.maxArrowDamageCoefficient, (float)this.charge);
				if (this.lastCharge < this.charge && this.charge == ChargeArrow.maxCharges)
				{
					EffectManager.SimpleMuzzleFlash(ChargeArrow.chargeEffectPrefab, base.gameObject, this.muzzleString, false);
				}
				if ((this.stopwatch >= this.totalDuration || !base.inputBank || !base.inputBank.skill1.down) && base.isAuthority)
				{
					FireArrow fireArrow = new FireArrow();
					fireArrow.damageCoefficient = damageCoefficient;
					this.outer.SetNextState(fireArrow);
					return;
				}
			}
			else
			{
				this.stopwatch += Time.fixedDeltaTime;
				if (this.stopwatch >= 1f / ChargeArrow.orbFrequency / this.attackSpeedStat)
				{
					this.stopwatch -= 1f / ChargeArrow.orbFrequency / this.attackSpeedStat;
					this.FireOrbArrow();
				}
				if ((!base.inputBank || !base.inputBank.skill1.down) && base.isAuthority)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040011B8 RID: 4536
		public static float baseTotalDuration;

		// Token: 0x040011B9 RID: 4537
		public static float baseMaxChargeTime;

		// Token: 0x040011BA RID: 4538
		public static int maxCharges;

		// Token: 0x040011BB RID: 4539
		public static GameObject chargeEffectPrefab;

		// Token: 0x040011BC RID: 4540
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x040011BD RID: 4541
		public static string chargeStockSoundString;

		// Token: 0x040011BE RID: 4542
		public static string chargeLoopStartSoundString;

		// Token: 0x040011BF RID: 4543
		public static string chargeLoopStopSoundString;

		// Token: 0x040011C0 RID: 4544
		public static float minBonusBloom;

		// Token: 0x040011C1 RID: 4545
		public static float maxBonusBloom;

		// Token: 0x040011C2 RID: 4546
		public static float minArrowDamageCoefficient;

		// Token: 0x040011C3 RID: 4547
		public static float maxArrowDamageCoefficient;

		// Token: 0x040011C4 RID: 4548
		public static float orbDamageCoefficient;

		// Token: 0x040011C5 RID: 4549
		public static float orbRange;

		// Token: 0x040011C6 RID: 4550
		public static float orbFrequency;

		// Token: 0x040011C7 RID: 4551
		public static float orbProcCoefficient;

		// Token: 0x040011C8 RID: 4552
		private float stopwatch;

		// Token: 0x040011C9 RID: 4553
		private GameObject chargeLeftInstance;

		// Token: 0x040011CA RID: 4554
		private GameObject chargeRightInstance;

		// Token: 0x040011CB RID: 4555
		private Animator animator;

		// Token: 0x040011CC RID: 4556
		private int charge;

		// Token: 0x040011CD RID: 4557
		private int lastCharge;

		// Token: 0x040011CE RID: 4558
		private ChildLocator childLocator;

		// Token: 0x040011CF RID: 4559
		private float totalDuration;

		// Token: 0x040011D0 RID: 4560
		private float maxChargeTime;

		// Token: 0x040011D1 RID: 4561
		private bool cachedSprinting;

		// Token: 0x040011D2 RID: 4562
		private float originalMinYaw;

		// Token: 0x040011D3 RID: 4563
		private float originalMaxYaw;

		// Token: 0x040011D4 RID: 4564
		private string muzzleString;
	}
}
