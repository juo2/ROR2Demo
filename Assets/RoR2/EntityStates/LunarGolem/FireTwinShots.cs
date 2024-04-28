using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LunarGolem
{
	// Token: 0x020002B7 RID: 695
	public class FireTwinShots : BaseState
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x00033CA4 File Offset: 0x00031EA4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireTwinShots.baseDuration / this.attackSpeedStat;
			this.aimDelay = FireTwinShots.baseAimDelay / this.attackSpeedStat;
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(FireTwinShots.aimTime);
			}
			if (!FireTwinShots.useSeriesFire)
			{
				base.PlayAnimation("Gesture, Additive", "FireTwinShot", "TwinShot.playbackRate", this.duration);
			}
			else
			{
				this.PlayAnimation("Gesture, Additive", "BufferEmpty");
			}
			Util.PlayAttackSpeedSound(FireTwinShots.attackSoundString, base.gameObject, FireTwinShots.fireSoundPlaybackRate);
			this.initialAimRay = base.GetAimRay();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00033D50 File Offset: 0x00031F50
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.fired && this.aimDelay <= base.fixedAge)
			{
				this.fired = true;
				if (base.isAuthority)
				{
					this.Fire();
				}
			}
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			if (this.refireIndex < FireTwinShots.refireCount)
			{
				this.outer.SetNextState(new FireTwinShots
				{
					refireIndex = this.refireIndex + 1
				});
				return;
			}
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00033DDC File Offset: 0x00031FDC
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.WritePackedUInt32((uint)this.refireIndex);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00033DF1 File Offset: 0x00031FF1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.refireIndex = (int)reader.ReadPackedUInt32();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00033E08 File Offset: 0x00032008
		private void Fire()
		{
			Ray aimRay = base.GetAimRay();
			Quaternion a = Quaternion.LookRotation(this.initialAimRay.direction);
			Quaternion b = Quaternion.LookRotation(aimRay.direction);
			float num = Util.Remap(Util.Remap((float)this.refireIndex, 0f, (float)(FireTwinShots.refireCount - 1), 0f, 1f), 0f, 1f, FireTwinShots.minLeadTime, FireTwinShots.maxLeadTime) / this.aimDelay;
			Quaternion rotation = Quaternion.SlerpUnclamped(a, b, 1f + num);
			Ray ray = new Ray(aimRay.origin, rotation * Vector3.forward);
			if (this.refireIndex == 0 && FireTwinShots.dustEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTwinShots.dustEffectPrefab, base.gameObject, "Root", false);
			}
			int num2 = this.refireIndex;
			if (!FireTwinShots.useSeriesFire)
			{
				num2 = this.refireIndex + 3;
			}
			while (this.refireIndex <= num2)
			{
				string muzzleName = "";
				bool flipProjectile = false;
				switch (this.refireIndex % 4)
				{
				case 0:
					muzzleName = FireTwinShots.rightMuzzleTop;
					this.PlayAnimation("Gesture, Right Additive", "FireRightShot");
					flipProjectile = true;
					break;
				case 1:
					muzzleName = FireTwinShots.leftMuzzleTop;
					this.PlayAnimation("Gesture, Left Additive", "FireLeftShot");
					break;
				case 2:
					muzzleName = FireTwinShots.rightMuzzleBot;
					this.PlayAnimation("Gesture, Right Additive", "FireRightShot");
					flipProjectile = true;
					break;
				case 3:
					muzzleName = FireTwinShots.leftMuzzleBot;
					this.PlayAnimation("Gesture, Left Additive", "FireLeftShot");
					break;
				}
				this.FireSingle(muzzleName, ray.direction, flipProjectile);
				this.refireIndex++;
			}
			this.refireIndex--;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00033FC0 File Offset: 0x000321C0
		private void FireSingle(string muzzleName, Vector3 aimDirection, bool flipProjectile)
		{
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				Transform transform = modelChildLocator.FindChild(muzzleName);
				if (transform)
				{
					if (FireTwinShots.effectPrefab)
					{
						EffectManager.SimpleMuzzleFlash(FireTwinShots.effectPrefab, base.gameObject, muzzleName, false);
					}
					if (base.isAuthority)
					{
						ProjectileManager.instance.FireProjectile(FireTwinShots.projectilePrefab, transform.position, Util.QuaternionSafeLookRotation(aimDirection, (!flipProjectile) ? Vector3.up : Vector3.down), base.gameObject, this.damageStat * FireTwinShots.damageCoefficient, FireTwinShots.force, base.RollCrit(), DamageColorIndex.Default, null, -1f);
					}
				}
			}
		}

		// Token: 0x04000EF4 RID: 3828
		public static GameObject projectilePrefab;

		// Token: 0x04000EF5 RID: 3829
		public static GameObject effectPrefab;

		// Token: 0x04000EF6 RID: 3830
		public static GameObject dustEffectPrefab;

		// Token: 0x04000EF7 RID: 3831
		public static GameObject hitEffectPrefab;

		// Token: 0x04000EF8 RID: 3832
		public static GameObject tracerEffectPrefab;

		// Token: 0x04000EF9 RID: 3833
		public static float damageCoefficient;

		// Token: 0x04000EFA RID: 3834
		public static float blastRadius;

		// Token: 0x04000EFB RID: 3835
		public static float force;

		// Token: 0x04000EFC RID: 3836
		public static float baseDuration = 2f;

		// Token: 0x04000EFD RID: 3837
		public static string attackSoundString;

		// Token: 0x04000EFE RID: 3838
		public static float aimTime = 2f;

		// Token: 0x04000EFF RID: 3839
		public static string leftMuzzleTop;

		// Token: 0x04000F00 RID: 3840
		public static string rightMuzzleTop;

		// Token: 0x04000F01 RID: 3841
		public static string leftMuzzleBot;

		// Token: 0x04000F02 RID: 3842
		public static string rightMuzzleBot;

		// Token: 0x04000F03 RID: 3843
		public static int refireCount = 6;

		// Token: 0x04000F04 RID: 3844
		public static float baseAimDelay = 0.1f;

		// Token: 0x04000F05 RID: 3845
		public static float minLeadTime = 2f;

		// Token: 0x04000F06 RID: 3846
		public static float maxLeadTime = 2f;

		// Token: 0x04000F07 RID: 3847
		public static float fireSoundPlaybackRate;

		// Token: 0x04000F08 RID: 3848
		public static bool useSeriesFire = true;

		// Token: 0x04000F09 RID: 3849
		private int refireIndex;

		// Token: 0x04000F0A RID: 3850
		private Ray initialAimRay;

		// Token: 0x04000F0B RID: 3851
		private bool fired;

		// Token: 0x04000F0C RID: 3852
		private float aimDelay;

		// Token: 0x04000F0D RID: 3853
		private float duration;
	}
}
