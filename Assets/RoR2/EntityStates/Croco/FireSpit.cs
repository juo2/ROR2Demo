using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003DA RID: 986
	public class FireSpit : BaseState
	{
		// Token: 0x060011A0 RID: 4512 RVA: 0x0004DAA0 File Offset: 0x0004BCA0
		public override void OnEnter()
		{
			base.OnEnter();
			this.crocoDamageTypeController = base.GetComponent<CrocoDamageTypeController>();
			Ray aimRay = base.GetAimRay();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			base.PlayAnimation("Gesture, Mouth", "FireSpit", "FireSpit.playbackRate", this.duration);
			Util.PlaySound(this.attackString, base.gameObject);
			base.AddRecoil(-1f * this.recoilAmplitude, -1.5f * this.recoilAmplitude, -0.25f * this.recoilAmplitude, 0.25f * this.recoilAmplitude);
			base.characterBody.AddSpreadBloom(this.bloom);
			string muzzleName = "MouthMuzzle";
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				DamageType value = this.crocoDamageTypeController ? this.crocoDamageTypeController.GetDamageType() : DamageType.Generic;
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = this.projectilePrefab;
				fireProjectileInfo.position = aimRay.origin;
				fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat * this.damageCoefficient;
				fireProjectileInfo.damageTypeOverride = new DamageType?(value);
				fireProjectileInfo.force = this.force;
				fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004DC49 File Offset: 0x0004BE49
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001658 RID: 5720
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04001659 RID: 5721
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x0400165A RID: 5722
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x0400165B RID: 5723
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x0400165C RID: 5724
		[SerializeField]
		public float force = 20f;

		// Token: 0x0400165D RID: 5725
		[SerializeField]
		public string attackString;

		// Token: 0x0400165E RID: 5726
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x0400165F RID: 5727
		[SerializeField]
		public float bloom;

		// Token: 0x04001660 RID: 5728
		private float duration;

		// Token: 0x04001661 RID: 5729
		private CrocoDamageTypeController crocoDamageTypeController;
	}
}
