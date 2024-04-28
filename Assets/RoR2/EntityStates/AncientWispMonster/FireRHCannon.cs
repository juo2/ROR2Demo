using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x0200049D RID: 1181
	public class FireRHCannon : BaseState
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0005DF64 File Offset: 0x0005C164
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			string text = "MuzzleRight";
			this.duration = FireRHCannon.baseDuration / this.attackSpeedStat;
			this.durationBetweenShots = FireRHCannon.baseDurationBetweenShots / this.attackSpeedStat;
			if (FireRHCannon.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireRHCannon.effectPrefab, base.gameObject, text, false);
			}
			base.PlayAnimation("Gesture", "FireRHCannon", "FireRHCannon.playbackRate", this.duration);
			if (base.isAuthority && base.modelLocator && base.modelLocator.modelTransform)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(text);
					if (transform)
					{
						Vector3 forward = aimRay.direction;
						RaycastHit raycastHit;
						if (Physics.Raycast(aimRay, out raycastHit, (float)LayerIndex.world.mask))
						{
							forward = raycastHit.point - transform.position;
						}
						ProjectileManager.instance.FireProjectile(FireRHCannon.projectilePrefab, transform.position, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireRHCannon.damageCoefficient, FireRHCannon.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
					}
				}
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0005E0CC File Offset: 0x0005C2CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (this.bulletCountCurrent == FireRHCannon.bulletCount && base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
				if (this.bulletCountCurrent < FireRHCannon.bulletCount && base.fixedAge >= this.durationBetweenShots)
				{
					FireRHCannon fireRHCannon = new FireRHCannon();
					fireRHCannon.bulletCountCurrent = this.bulletCountCurrent + 1;
					this.outer.SetNextState(fireRHCannon);
					return;
				}
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001AFE RID: 6910
		public static GameObject projectilePrefab;

		// Token: 0x04001AFF RID: 6911
		public static GameObject effectPrefab;

		// Token: 0x04001B00 RID: 6912
		public static float baseDuration = 2f;

		// Token: 0x04001B01 RID: 6913
		public static float baseDurationBetweenShots = 0.5f;

		// Token: 0x04001B02 RID: 6914
		public static float damageCoefficient = 1.2f;

		// Token: 0x04001B03 RID: 6915
		public static float force = 20f;

		// Token: 0x04001B04 RID: 6916
		public static int bulletCount;

		// Token: 0x04001B05 RID: 6917
		private float duration;

		// Token: 0x04001B06 RID: 6918
		private float durationBetweenShots;

		// Token: 0x04001B07 RID: 6919
		public int bulletCountCurrent = 1;
	}
}
