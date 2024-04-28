using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F7 RID: 247
	public class FireCrabCannon : BaseState
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00012796 File Offset: 0x00010996
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			base.StartAimMode(2f, false);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000127CC File Offset: 0x000109CC
		private void FireProjectile()
		{
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.perGrenadeSoundString, base.gameObject);
			base.AddRecoil(-1f * FireCrabCannon.recoilAmplitude, -2f * FireCrabCannon.recoilAmplitude, -1f * FireCrabCannon.recoilAmplitude, 1f * FireCrabCannon.recoilAmplitude);
			base.characterBody.AddSpreadBloom(this.spreadBloomValue);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, this.maxSpread, 1f, 1f, 0f, 0f);
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				Vector3.ProjectOnPlane(onUnitSphere, aimRay.direction);
				Quaternion rotation = Util.QuaternionSafeLookRotation(aimRay.direction, onUnitSphere);
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, rotation, base.gameObject, this.damageStat * this.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012918 File Offset: 0x00010B18
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			float num = this.fireDuration / this.attackSpeedStat / (float)this.grenadeCountMax;
			if (this.fireTimer <= 0f && this.grenadeCount < this.grenadeCountMax)
			{
				this.FireProjectile();
				this.fireTimer += num;
				this.grenadeCount++;
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040004A8 RID: 1192
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004A9 RID: 1193
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004AA RID: 1194
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040004AB RID: 1195
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040004AC RID: 1196
		[SerializeField]
		public string muzzle;

		// Token: 0x040004AD RID: 1197
		[SerializeField]
		public int grenadeCountMax = 3;

		// Token: 0x040004AE RID: 1198
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x040004AF RID: 1199
		[SerializeField]
		public float maxSpread;

		// Token: 0x040004B0 RID: 1200
		[SerializeField]
		public float fireDuration = 1f;

		// Token: 0x040004B1 RID: 1201
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040004B2 RID: 1202
		[SerializeField]
		private static float recoilAmplitude = 1f;

		// Token: 0x040004B3 RID: 1203
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040004B4 RID: 1204
		[SerializeField]
		public string perGrenadeSoundString;

		// Token: 0x040004B5 RID: 1205
		[SerializeField]
		public float spreadBloomValue = 0.3f;

		// Token: 0x040004B6 RID: 1206
		private Transform modelTransform;

		// Token: 0x040004B7 RID: 1207
		private float duration;

		// Token: 0x040004B8 RID: 1208
		private float fireTimer;

		// Token: 0x040004B9 RID: 1209
		private int grenadeCount;
	}
}
