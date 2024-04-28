using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047F RID: 1151
	public abstract class BaseFireSidearmRevolverState : BaseSidearmState
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x0005B88C File Offset: 0x00059A8C
		public override void OnEnter()
		{
			base.OnEnter();
			base.AddRecoil(-3f * this.recoilAmplitude, -4f * this.recoilAmplitude, -0.5f * this.recoilAmplitude, 0.5f * this.recoilAmplitude);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "MuzzlePistol";
			Util.PlaySound(this.attackSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "FireSideWeapon", "FireSideWeapon.playbackRate", this.duration);
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = new BulletAttack();
				bulletAttack.owner = base.gameObject;
				bulletAttack.weapon = base.gameObject;
				bulletAttack.origin = aimRay.origin;
				bulletAttack.aimVector = aimRay.direction;
				bulletAttack.minSpread = this.minSpread;
				bulletAttack.maxSpread = this.maxSpread;
				bulletAttack.bulletCount = 1U;
				bulletAttack.damage = this.damageCoefficient * this.damageStat;
				bulletAttack.force = this.force;
				bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
				bulletAttack.tracerEffectPrefab = this.tracerEffectPrefab;
				bulletAttack.muzzleName = muzzleName;
				bulletAttack.hitEffectPrefab = this.hitEffectPrefab;
				bulletAttack.isCrit = base.RollCrit();
				bulletAttack.HitEffectNormal = false;
				bulletAttack.radius = this.bulletRadius;
				bulletAttack.damageType |= DamageType.BonusToLowHealth;
				bulletAttack.smartCollision = true;
				this.ModifyBullet(bulletAttack);
				bulletAttack.Fire();
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005B854 File Offset: 0x00059A54
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005BA28 File Offset: 0x00059C28
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new ExitSidearmRevolver());
				return;
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void ModifyBullet(BulletAttack bulletAttack)
		{
		}

		// Token: 0x04001A56 RID: 6742
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x04001A57 RID: 6743
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x04001A58 RID: 6744
		[SerializeField]
		public GameObject tracerEffectPrefab;

		// Token: 0x04001A59 RID: 6745
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04001A5A RID: 6746
		[SerializeField]
		public float force;

		// Token: 0x04001A5B RID: 6747
		[SerializeField]
		public float minSpread;

		// Token: 0x04001A5C RID: 6748
		[SerializeField]
		public float maxSpread;

		// Token: 0x04001A5D RID: 6749
		[SerializeField]
		public string attackSoundString;

		// Token: 0x04001A5E RID: 6750
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x04001A5F RID: 6751
		[SerializeField]
		public float bulletRadius;
	}
}
