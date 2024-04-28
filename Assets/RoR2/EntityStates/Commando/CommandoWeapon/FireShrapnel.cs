using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F0 RID: 1008
	public class FireShrapnel : BaseState
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00050828 File Offset: 0x0004EA28
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireShrapnel.baseDuration / this.attackSpeedStat;
			this.modifiedAimRay = base.GetAimRay();
			Util.PlaySound(FireShrapnel.attackSoundString, base.gameObject);
			string muzzleName = "MuzzleLaser";
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
			base.PlayAnimation("Gesture", "FireLaser", "FireLaser.playbackRate", this.duration);
			if (FireShrapnel.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireShrapnel.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(this.modifiedAimRay, out raycastHit, 1000f, LayerIndex.world.mask | LayerIndex.defaultLayer.mask))
				{
					new BlastAttack
					{
						attacker = base.gameObject,
						inflictor = base.gameObject,
						teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
						baseDamage = this.damageStat * FireShrapnel.damageCoefficient,
						baseForce = FireShrapnel.force * 0.2f,
						position = raycastHit.point,
						radius = FireShrapnel.blastRadius
					}.Fire();
				}
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.modifiedAimRay.origin,
					aimVector = this.modifiedAimRay.direction,
					radius = 0.25f,
					minSpread = FireShrapnel.minSpread,
					maxSpread = FireShrapnel.maxSpread,
					bulletCount = (uint)((FireShrapnel.bulletCount > 0) ? FireShrapnel.bulletCount : 0),
					damage = 0f,
					procCoefficient = 0f,
					force = FireShrapnel.force,
					tracerEffectPrefab = FireShrapnel.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireShrapnel.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master)
				}.Fire();
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00050A4E File Offset: 0x0004EC4E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001724 RID: 5924
		public static GameObject effectPrefab;

		// Token: 0x04001725 RID: 5925
		public static GameObject hitEffectPrefab;

		// Token: 0x04001726 RID: 5926
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001727 RID: 5927
		public static float damageCoefficient;

		// Token: 0x04001728 RID: 5928
		public static float blastRadius;

		// Token: 0x04001729 RID: 5929
		public static float force;

		// Token: 0x0400172A RID: 5930
		public static float minSpread;

		// Token: 0x0400172B RID: 5931
		public static float maxSpread;

		// Token: 0x0400172C RID: 5932
		public static int bulletCount;

		// Token: 0x0400172D RID: 5933
		public static float baseDuration = 2f;

		// Token: 0x0400172E RID: 5934
		public static string attackSoundString;

		// Token: 0x0400172F RID: 5935
		public static float maxDistance;

		// Token: 0x04001730 RID: 5936
		private float duration;

		// Token: 0x04001731 RID: 5937
		private Ray modifiedAimRay;
	}
}
