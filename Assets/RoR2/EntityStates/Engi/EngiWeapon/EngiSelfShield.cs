using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A5 RID: 933
	public class EngiSelfShield : BaseState
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x000490FC File Offset: 0x000472FC
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.EngiShield);
				base.characterBody.RecalculateStats();
				if (base.healthComponent)
				{
					base.healthComponent.RechargeShieldFull();
				}
			}
			this.friendLocator = new BullseyeSearch();
			this.friendLocator.teamMaskFilter = TeamMask.none;
			if (base.teamComponent)
			{
				this.friendLocator.teamMaskFilter.AddTeam(base.teamComponent.teamIndex);
			}
			this.friendLocator.maxDistanceFilter = 80f;
			this.friendLocator.maxAngleFilter = 20f;
			this.friendLocator.sortMode = BullseyeSearch.SortMode.Angle;
			this.friendLocator.filterByLoS = false;
			this.indicator = new Indicator(base.gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/ShieldTransferIndicator"));
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000491EC File Offset: 0x000473EC
		public override void OnExit()
		{
			base.skillLocator.utility = base.skillLocator.FindSkill("RetractShield");
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.EngiShield);
			}
			if (base.isAuthority)
			{
				base.skillLocator.utility.RemoveAllStocks();
			}
			this.indicator.active = false;
			base.OnExit();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00049258 File Offset: 0x00047458
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= EngiSelfShield.transferDelay && base.skillLocator.utility.IsReady())
			{
				if (base.characterBody)
				{
					float num = 0f;
					Ray ray = CameraRigController.ModifyAimRayIfApplicable(base.GetAimRay(), base.gameObject, out num);
					this.friendLocator.searchOrigin = ray.origin;
					this.friendLocator.searchDirection = ray.direction;
					this.friendLocator.maxDistanceFilter += num;
					this.friendLocator.RefreshCandidates();
					this.friendLocator.FilterOutGameObject(base.gameObject);
					this.transferTarget = this.friendLocator.GetResults().FirstOrDefault<HurtBox>();
				}
				HealthComponent healthComponent = this.transferTarget ? this.transferTarget.healthComponent : null;
				if (healthComponent)
				{
					this.indicator.targetTransform = Util.GetCoreTransform(healthComponent.gameObject);
					if (base.inputBank && base.inputBank.skill3.justPressed)
					{
						EngiOtherShield engiOtherShield = new EngiOtherShield();
						engiOtherShield.target = healthComponent.gameObject.GetComponent<CharacterBody>();
						this.outer.SetNextState(engiOtherShield);
						return;
					}
				}
				else
				{
					this.indicator.targetTransform = null;
				}
				this.indicator.active = this.indicator.targetTransform;
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400150A RID: 5386
		public static float transferDelay = 0.1f;

		// Token: 0x0400150B RID: 5387
		private HurtBox transferTarget;

		// Token: 0x0400150C RID: 5388
		private BullseyeSearch friendLocator;

		// Token: 0x0400150D RID: 5389
		private Indicator indicator;
	}
}
