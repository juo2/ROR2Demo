using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003EC RID: 1004
	public class FirePistol2 : BaseSkillState, SteppedSkillDef.IStepSetter
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x00050144 File Offset: 0x0004E344
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.pistol = i;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00050150 File Offset: 0x0004E350
		private void FireBullet(string targetMuzzle)
		{
			Util.PlaySound(FirePistol2.firePistolSoundString, base.gameObject);
			if (FirePistol2.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FirePistol2.muzzleEffectPrefab, base.gameObject, targetMuzzle, false);
			}
			base.AddRecoil(-0.4f * FirePistol2.recoilAmplitude, -0.8f * FirePistol2.recoilAmplitude, -0.3f * FirePistol2.recoilAmplitude, 0.3f * FirePistol2.recoilAmplitude);
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.aimRay.origin,
					aimVector = this.aimRay.direction,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					damage = FirePistol2.damageCoefficient * this.damageStat,
					force = FirePistol2.force,
					tracerEffectPrefab = FirePistol2.tracerEffectPrefab,
					muzzleName = targetMuzzle,
					hitEffectPrefab = FirePistol2.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = 0.1f,
					smartCollision = true
				}.Fire();
			}
			base.characterBody.AddSpreadBloom(FirePistol2.spreadBloomValue);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000502A4 File Offset: 0x0004E4A4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FirePistol2.baseDuration / this.attackSpeedStat;
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 3f, false);
			if (this.pistol % 2 == 0)
			{
				this.PlayAnimation("Gesture Additive, Left", "FirePistol, Left");
				this.FireBullet("MuzzleLeft");
				return;
			}
			this.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
			this.FireBullet("MuzzleRight");
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00050328 File Offset: 0x0004E528
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			if (base.activatorSkillSlot.stock <= 0)
			{
				this.outer.SetNextState(new ReloadPistols());
				return;
			}
			this.outer.SetNextStateToMain();
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001700 RID: 5888
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04001701 RID: 5889
		public static GameObject hitEffectPrefab;

		// Token: 0x04001702 RID: 5890
		public static GameObject tracerEffectPrefab;

		// Token: 0x04001703 RID: 5891
		public static float damageCoefficient;

		// Token: 0x04001704 RID: 5892
		public static float force;

		// Token: 0x04001705 RID: 5893
		public static float baseDuration = 2f;

		// Token: 0x04001706 RID: 5894
		public static string firePistolSoundString;

		// Token: 0x04001707 RID: 5895
		public static float recoilAmplitude = 1f;

		// Token: 0x04001708 RID: 5896
		public static float spreadBloomValue = 0.3f;

		// Token: 0x04001709 RID: 5897
		public static float commandoBoostBuffCoefficient = 0.4f;

		// Token: 0x0400170A RID: 5898
		private int pistol;

		// Token: 0x0400170B RID: 5899
		private Ray aimRay;

		// Token: 0x0400170C RID: 5900
		private float duration;
	}
}
