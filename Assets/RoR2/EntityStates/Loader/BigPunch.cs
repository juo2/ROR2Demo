using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Orbs;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002BF RID: 703
	public class BigPunch : LoaderMeleeAttack
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00034690 File Offset: 0x00032890
		private Vector3 punchVector
		{
			get
			{
				return base.characterDirection.forward.normalized;
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000346B0 File Offset: 0x000328B0
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterMotor.velocity.y = BigPunch.shorthopVelocityOnEnter;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000346CD File Offset: 0x000328CD
		protected override void PlayAnimation()
		{
			base.PlayAnimation();
			base.PlayAnimation("FullBody, Override", "BigPunch", "BigPunch.playbackRate", this.duration);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000346F0 File Offset: 0x000328F0
		protected override void AuthorityFixedUpdate()
		{
			base.AuthorityFixedUpdate();
			if (this.hasHit && !this.hasKnockbackedSelf && !base.authorityInHitPause)
			{
				this.hasKnockbackedSelf = true;
				base.healthComponent.TakeDamageForce(this.punchVector * -BigPunch.knockbackForce, true, false);
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00034740 File Offset: 0x00032940
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			overlapAttack.maximumOverlapTargets = 1;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00034750 File Offset: 0x00032950
		protected override void OnMeleeHitAuthority()
		{
			if (this.hasHit)
			{
				return;
			}
			base.OnMeleeHitAuthority();
			this.hasHit = true;
			if (base.FindModelChild(this.swingEffectMuzzleString))
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.position = base.GetAimRay().origin;
				fireProjectileInfo.rotation = Quaternion.LookRotation(this.punchVector);
				fireProjectileInfo.crit = base.RollCrit();
				fireProjectileInfo.damage = 1f * this.damageStat;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LoaderZapCone");
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00034800 File Offset: 0x00032A00
		private void FireSecondaryRaysServer()
		{
			Ray aimRay = base.GetAimRay();
			TeamIndex team = base.GetTeam();
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(team);
			bullseyeSearch.maxAngleFilter = BigPunch.maxShockFOV * 0.5f;
			bullseyeSearch.maxDistanceFilter = BigPunch.maxShockDistance;
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = this.punchVector;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.RefreshCandidates();
			List<HurtBox> list = bullseyeSearch.GetResults().Where(new Func<HurtBox, bool>(Util.IsValid)).ToList<HurtBox>();
			Transform transform = base.FindModelChild(this.swingEffectMuzzleString);
			if (transform)
			{
				for (int i = 0; i < Mathf.Min(list.Count, BigPunch.maxShockCount); i++)
				{
					HurtBox hurtBox = list[i];
					if (hurtBox)
					{
						LightningOrb lightningOrb = new LightningOrb();
						lightningOrb.bouncedObjects = new List<HealthComponent>();
						lightningOrb.attacker = base.gameObject;
						lightningOrb.teamIndex = team;
						lightningOrb.damageValue = this.damageStat * BigPunch.shockDamageCoefficient;
						lightningOrb.isCrit = base.RollCrit();
						lightningOrb.origin = transform.position;
						lightningOrb.bouncesRemaining = 0;
						lightningOrb.lightningType = LightningOrb.LightningType.Loader;
						lightningOrb.procCoefficient = BigPunch.shockProcCoefficient;
						lightningOrb.target = hurtBox;
						OrbManager.instance.AddOrb(lightningOrb);
					}
				}
			}
		}

		// Token: 0x04000F31 RID: 3889
		public static int maxShockCount;

		// Token: 0x04000F32 RID: 3890
		public static float maxShockFOV;

		// Token: 0x04000F33 RID: 3891
		public static float maxShockDistance;

		// Token: 0x04000F34 RID: 3892
		public static float shockDamageCoefficient;

		// Token: 0x04000F35 RID: 3893
		public static float shockProcCoefficient;

		// Token: 0x04000F36 RID: 3894
		public static float knockbackForce;

		// Token: 0x04000F37 RID: 3895
		public static float shorthopVelocityOnEnter;

		// Token: 0x04000F38 RID: 3896
		private bool hasHit;

		// Token: 0x04000F39 RID: 3897
		private bool hasKnockbackedSelf;
	}
}
