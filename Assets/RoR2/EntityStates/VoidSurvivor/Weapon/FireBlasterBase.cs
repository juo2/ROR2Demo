using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000FE RID: 254
	public class FireBlasterBase : BaseSkillState, SteppedSkillDef.IStepSetter
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x00012D90 File Offset: 0x00010F90
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.step = i;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012D9C File Offset: 0x00010F9C
		public override void OnEnter()
		{
			base.OnEnter();
			base.activatorSkillSlot = base.skillLocator.primary;
			base.GetAimRay();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, 1f + (float)this.step * this.attackSoundPitchPerStep);
			base.AddRecoil(-1f * this.recoilAmplitude, -1.5f * this.recoilAmplitude, -0.25f * this.recoilAmplitude, 0.25f * this.recoilAmplitude);
			base.characterBody.AddSpreadBloom(this.bloom);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				this.FireProjectiles();
			}
			Debug.Log(this.step);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00012EBC File Offset: 0x000110BC
		private void FireProjectiles()
		{
			for (int i = 0; i < this.projectileCount; i++)
			{
				float num = (float)i - (float)(this.projectileCount - 1) / 2f;
				float bonusYaw = num * this.yawPerProjectile;
				float d = num * this.offsetPerProjectile;
				Ray aimRay = base.GetAimRay();
				aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, base.characterBody.spreadBloomAngle + this.spread, 1f, 1f, bonusYaw, 0f);
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = this.projectilePrefab;
				fireProjectileInfo.position = aimRay.origin + Vector3.Cross(aimRay.direction, Vector3.up) * d;
				fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat * this.damageCoefficient;
				fireProjectileInfo.force = this.force;
				fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00012FF4 File Offset: 0x000111F4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				if (this.step >= this.burstCount && base.IsKeyDownAuthority() && this.canCharge)
				{
					this.outer.SetNextState(new ChargeMegaBlaster());
					return;
				}
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001305C File Offset: 0x0001125C
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.step);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013072 File Offset: 0x00011272
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.step = (int)reader.ReadByte();
		}

		// Token: 0x040004D0 RID: 1232
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040004D1 RID: 1233
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040004D2 RID: 1234
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040004D3 RID: 1235
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x040004D4 RID: 1236
		[SerializeField]
		public float force = 20f;

		// Token: 0x040004D5 RID: 1237
		[SerializeField]
		public string attackSoundString;

		// Token: 0x040004D6 RID: 1238
		[SerializeField]
		public float attackSoundPitchPerStep;

		// Token: 0x040004D7 RID: 1239
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x040004D8 RID: 1240
		[SerializeField]
		public float bloom;

		// Token: 0x040004D9 RID: 1241
		[SerializeField]
		public string muzzle;

		// Token: 0x040004DA RID: 1242
		[SerializeField]
		public float spread;

		// Token: 0x040004DB RID: 1243
		[SerializeField]
		public int projectileCount;

		// Token: 0x040004DC RID: 1244
		[SerializeField]
		public float yawPerProjectile;

		// Token: 0x040004DD RID: 1245
		[SerializeField]
		public float offsetPerProjectile;

		// Token: 0x040004DE RID: 1246
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004DF RID: 1247
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004E0 RID: 1248
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040004E1 RID: 1249
		[SerializeField]
		public int burstCount;

		// Token: 0x040004E2 RID: 1250
		[SerializeField]
		public bool canCharge;

		// Token: 0x040004E3 RID: 1251
		private float duration;

		// Token: 0x040004E4 RID: 1252
		private float interruptDuration;

		// Token: 0x040004E5 RID: 1253
		public int step;
	}
}
