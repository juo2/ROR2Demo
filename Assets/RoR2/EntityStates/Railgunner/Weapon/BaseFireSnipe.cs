using System;
using EntityStates.Railgunner.Reload;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F1 RID: 497
	public abstract class BaseFireSnipe : GenericBulletBaseState, IBaseWeaponState
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060008D3 RID: 2259 RVA: 0x000252FC File Offset: 0x000234FC
		// (remove) Token: 0x060008D4 RID: 2260 RVA: 0x00025330 File Offset: 0x00023530
		public static event Action<DamageInfo> onWeakPointHit;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060008D5 RID: 2261 RVA: 0x00025364 File Offset: 0x00023564
		// (remove) Token: 0x060008D6 RID: 2262 RVA: 0x00025398 File Offset: 0x00023598
		public static event Action onWeakPointMissed;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060008D7 RID: 2263 RVA: 0x000253CC File Offset: 0x000235CC
		// (remove) Token: 0x060008D8 RID: 2264 RVA: 0x00025400 File Offset: 0x00023600
		public static event Action<BaseFireSnipe> onFireSnipe;

		// Token: 0x060008D9 RID: 2265 RVA: 0x00025434 File Offset: 0x00023634
		public override void OnEnter()
		{
			this.wasMiss = true;
			Action<BaseFireSnipe> action = BaseFireSnipe.onFireSnipe;
			if (action != null)
			{
				action(this);
			}
			base.OnEnter();
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			if (base.isAuthority && this.useSecondaryStocks && base.skillLocator && base.skillLocator.secondary)
			{
				base.skillLocator.secondary.DeductStock(1);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000254C4 File Offset: 0x000236C4
		public override void OnExit()
		{
			if (base.isAuthority && (this.wasMiss || (!this.wasAtLeastOneWeakpoint && !this.wasMiss)))
			{
				Action action = BaseFireSnipe.onWeakPointMissed;
				if (action != null)
				{
					action();
				}
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00025518 File Offset: 0x00023718
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			bulletAttack.sniper = true;
			bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
			EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Reload");
			if (entityStateMachine)
			{
				Boosted boosted = entityStateMachine.state as Boosted;
				if (boosted != null)
				{
					bulletAttack.damage += boosted.GetBonusDamage();
					boosted.ConsumeBoost(this.queueReload);
				}
				else if (this.queueReload)
				{
					Waiting waiting = entityStateMachine.state as Waiting;
					if (waiting != null)
					{
						waiting.QueueReload();
					}
				}
			}
			if (this.isPiercing)
			{
				bulletAttack.stopperMask = LayerIndex.world.mask;
			}
			bulletAttack.modifyOutgoingDamageCallback = delegate(BulletAttack _bulletAttack, ref BulletAttack.BulletHit hitInfo, DamageInfo damageInfo)
			{
				_bulletAttack.damage *= this.piercingDamageCoefficientPerTarget;
				this.wasMiss = false;
				if (damageInfo.crit)
				{
					damageInfo.damage *= this.critDamageMultiplier;
					Action<DamageInfo> action = BaseFireSnipe.onWeakPointHit;
					if (action != null)
					{
						action(damageInfo);
					}
					this.wasAtLeastOneWeakpoint = true;
				}
			};
			EntityStateMachine entityStateMachine2 = EntityStateMachine.FindByCustomName(base.gameObject, "Backpack");
			EntityState entityState = this.InstantiateBackpackState();
			if (entityStateMachine2 && entityState != null)
			{
				entityStateMachine2.SetNextState(entityState);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000255F4 File Offset: 0x000237F4
		protected override void OnFireBulletAuthority(Ray aimRay)
		{
			base.characterBody.characterMotor.ApplyForce(-this.selfKnockbackForce * aimRay.direction, false, false);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002561B File Offset: 0x0002381B
		protected override void PlayFireAnimation()
		{
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool CanScope()
		{
			return true;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual EntityState InstantiateBackpackState()
		{
			return null;
		}

		// Token: 0x04000A6A RID: 2666
		private const string reloadStateMachineName = "Reload";

		// Token: 0x04000A6B RID: 2667
		private const string backpackStateMachineName = "Backpack";

		// Token: 0x04000A6F RID: 2671
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000A70 RID: 2672
		[SerializeField]
		public bool useSecondaryStocks;

		// Token: 0x04000A71 RID: 2673
		[SerializeField]
		public bool queueReload;

		// Token: 0x04000A72 RID: 2674
		[Header("Projectile")]
		[SerializeField]
		public float critDamageMultiplier;

		// Token: 0x04000A73 RID: 2675
		[SerializeField]
		public float selfKnockbackForce;

		// Token: 0x04000A74 RID: 2676
		[SerializeField]
		public bool isPiercing;

		// Token: 0x04000A75 RID: 2677
		[SerializeField]
		public float piercingDamageCoefficientPerTarget;

		// Token: 0x04000A76 RID: 2678
		[Header("Animation")]
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A77 RID: 2679
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A78 RID: 2680
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A79 RID: 2681
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000A7A RID: 2682
		private bool wasMiss;

		// Token: 0x04000A7B RID: 2683
		private bool wasAtLeastOneWeakpoint;
	}
}
