using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F5 RID: 245
	public class FireCorruptHandBeam : BaseSkillState
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00012168 File Offset: 0x00010368
		public override void OnEnter()
		{
			base.OnEnter();
			this.minimumDuration = this.baseMinimumDuration / this.attackSpeedStat;
			this.PlayAnimation(this.animationLayerName, this.animationEnterStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			this.blinkVfxInstance = UnityEngine.Object.Instantiate<GameObject>(this.beamVfxPrefab);
			this.blinkVfxInstance.transform.SetParent(base.characterBody.aimOriginTransform, false);
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Slow50);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000121F8 File Offset: 0x000103F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireCountdown -= Time.fixedDeltaTime;
			if (this.fireCountdown <= 0f)
			{
				this.fireCountdown = 1f / this.tickRate / this.attackSpeedStat;
				this.FireBullet();
			}
			base.characterBody.SetAimTimer(3f);
			if (this.blinkVfxInstance)
			{
				Vector3 point = base.GetAimRay().GetPoint(this.maxDistance);
				RaycastHit raycastHit;
				if (Util.CharacterRaycast(base.gameObject, base.GetAimRay(), out raycastHit, this.maxDistance, LayerIndex.world.mask, QueryTriggerInteraction.UseGlobal))
				{
					point = raycastHit.point;
				}
				this.blinkVfxInstance.transform.forward = point - this.blinkVfxInstance.transform.position;
			}
			if (((base.fixedAge >= this.minimumDuration && !base.IsKeyDownAuthority()) || base.characterBody.isSprinting) && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001230C File Offset: 0x0001050C
		public override void OnExit()
		{
			if (this.blinkVfxInstance)
			{
				VfxKillBehavior.KillVfxObject(this.blinkVfxInstance);
			}
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.Slow50);
			}
			this.PlayAnimation(this.animationLayerName, this.animationExitStateName);
			Util.PlaySound(this.exitSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012374 File Offset: 0x00010574
		private void FireBullet()
		{
			Ray aimRay = base.GetAimRay();
			base.AddRecoil(-1f * this.recoilAmplitude, -2f * this.recoilAmplitude, -0.5f * this.recoilAmplitude, 0.5f * this.recoilAmplitude);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					muzzleName = this.muzzle,
					maxDistance = Mathf.Lerp(this.minDistance, this.maxDistance, UnityEngine.Random.value),
					minSpread = 0f,
					maxSpread = this.maxSpread,
					radius = this.bulletRadius,
					falloffModel = BulletAttack.FalloffModel.None,
					smartCollision = false,
					stopperMask = default(LayerMask),
					hitMask = LayerIndex.entityPrecise.mask,
					damage = this.damageCoefficientPerSecond * this.damageStat / this.tickRate,
					procCoefficient = this.procCoefficientPerSecond / this.tickRate,
					force = this.forcePerSecond / this.tickRate,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					hitEffectPrefab = this.hitEffectPrefab
				}.Fire();
			}
			base.characterBody.AddSpreadBloom(this.spreadBloomValue);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000480 RID: 1152
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000481 RID: 1153
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x04000482 RID: 1154
		[SerializeField]
		public GameObject beamVfxPrefab;

		// Token: 0x04000483 RID: 1155
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000484 RID: 1156
		[SerializeField]
		public string exitSoundString;

		// Token: 0x04000485 RID: 1157
		[SerializeField]
		public float tickRate;

		// Token: 0x04000486 RID: 1158
		[SerializeField]
		public float damageCoefficientPerSecond;

		// Token: 0x04000487 RID: 1159
		[SerializeField]
		public float procCoefficientPerSecond;

		// Token: 0x04000488 RID: 1160
		[SerializeField]
		public float forcePerSecond;

		// Token: 0x04000489 RID: 1161
		[SerializeField]
		public float maxDistance;

		// Token: 0x0400048A RID: 1162
		[SerializeField]
		public float minDistance;

		// Token: 0x0400048B RID: 1163
		[SerializeField]
		public float bulletRadius;

		// Token: 0x0400048C RID: 1164
		[SerializeField]
		public float baseMinimumDuration = 2f;

		// Token: 0x0400048D RID: 1165
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x0400048E RID: 1166
		[SerializeField]
		public float spreadBloomValue = 0.3f;

		// Token: 0x0400048F RID: 1167
		[SerializeField]
		public float maxSpread;

		// Token: 0x04000490 RID: 1168
		[SerializeField]
		public string muzzle;

		// Token: 0x04000491 RID: 1169
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000492 RID: 1170
		[SerializeField]
		public string animationEnterStateName;

		// Token: 0x04000493 RID: 1171
		[SerializeField]
		public string animationExitStateName;

		// Token: 0x04000494 RID: 1172
		private GameObject blinkVfxInstance;

		// Token: 0x04000495 RID: 1173
		private float minimumDuration;

		// Token: 0x04000496 RID: 1174
		private float fireCountdown;
	}
}
