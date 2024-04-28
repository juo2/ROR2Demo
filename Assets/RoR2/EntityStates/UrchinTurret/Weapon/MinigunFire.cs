using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.UrchinTurret.Weapon
{
	// Token: 0x02000171 RID: 369
	public class MinigunFire : MinigunState
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001B838 File Offset: 0x00019A38
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.muzzleTransform && MinigunFire.muzzleVfxPrefab)
			{
				this.muzzleVfxTransform = UnityEngine.Object.Instantiate<GameObject>(MinigunFire.muzzleVfxPrefab, this.muzzleTransform).transform;
			}
			this.baseFireRate = 1f / MinigunFire.baseFireInterval;
			this.baseBulletsPerSecond = this.baseFireRate;
			this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
			this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
			Util.PlaySound(MinigunFire.startSound, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "ShootLoop", 0.2f);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001B8D9 File Offset: 0x00019AD9
		private void UpdateCrits()
		{
			if (this.lastCritCheck.timeSince >= 1f)
			{
				this.lastCritCheck = Run.FixedTimeStamp.now;
				if (base.RollCrit())
				{
					this.critEndTime = Run.FixedTimeStamp.now + 2f;
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001B918 File Offset: 0x00019B18
		public override void OnExit()
		{
			Util.PlaySound(MinigunFire.endSound, base.gameObject);
			if (this.muzzleVfxTransform)
			{
				EntityState.Destroy(this.muzzleVfxTransform.gameObject);
				this.muzzleVfxTransform = null;
			}
			base.PlayCrossfade("Gesture, Additive", "BufferEmpty", 0.2f);
			base.OnExit();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001B975 File Offset: 0x00019B75
		private void OnFireShared()
		{
			Util.PlaySound(MinigunFire.fireSound, base.gameObject);
			if (base.isAuthority)
			{
				this.OnFireAuthority();
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001B998 File Offset: 0x00019B98
		private void OnFireAuthority()
		{
			this.UpdateCrits();
			bool crit = !this.critEndTime.hasPassed;
			float damage = MinigunFire.baseDamagePerSecondCoefficient / this.baseBulletsPerSecond * this.damageStat;
			Ray aimRay = base.GetAimRay();
			Vector3 forward = Util.ApplySpread(aimRay.direction, MinigunFire.bulletMinSpread, MinigunFire.bulletMaxSpread, 1f, 1f, 0f, 0f);
			ProjectileManager.instance.FireProjectile(MinigunFire.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, damage, 0f, crit, DamageColorIndex.Default, null, -1f);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001BA30 File Offset: 0x00019C30
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				float num = MinigunFire.baseFireInterval / this.attackSpeedStat;
				this.fireTimer += num;
				this.OnFireShared();
			}
			if (base.isAuthority && !base.skillButtonState.down)
			{
				this.outer.SetNextState(new MinigunSpinDown());
				return;
			}
		}

		// Token: 0x040007C9 RID: 1993
		public static GameObject muzzleVfxPrefab;

		// Token: 0x040007CA RID: 1994
		public static GameObject projectilePrefab;

		// Token: 0x040007CB RID: 1995
		public static float baseFireInterval;

		// Token: 0x040007CC RID: 1996
		public static float baseDamagePerSecondCoefficient;

		// Token: 0x040007CD RID: 1997
		public static float bulletMinSpread;

		// Token: 0x040007CE RID: 1998
		public static float bulletMaxSpread;

		// Token: 0x040007CF RID: 1999
		public static string fireSound;

		// Token: 0x040007D0 RID: 2000
		public static string startSound;

		// Token: 0x040007D1 RID: 2001
		public static string endSound;

		// Token: 0x040007D2 RID: 2002
		private float fireTimer;

		// Token: 0x040007D3 RID: 2003
		private Transform muzzleVfxTransform;

		// Token: 0x040007D4 RID: 2004
		private float baseFireRate;

		// Token: 0x040007D5 RID: 2005
		private float baseBulletsPerSecond;

		// Token: 0x040007D6 RID: 2006
		private Run.FixedTimeStamp critEndTime;

		// Token: 0x040007D7 RID: 2007
		private Run.FixedTimeStamp lastCritCheck;
	}
}
