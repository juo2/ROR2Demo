using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000321 RID: 801
	public class FireGlaive : BaseState
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0003DC90 File Offset: 0x0003BE90
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			Transform modelTransform = base.GetModelTransform();
			this.duration = FireGlaive.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture", "FireGlaive", "FireGlaive.playbackRate", this.duration);
			Vector3 position = aimRay.origin;
			Quaternion rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("RightHand");
					if (transform)
					{
						position = transform.position;
					}
				}
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireGlaive.projectilePrefab, position, rotation, base.gameObject, this.damageStat * FireGlaive.damageCoefficient, FireGlaive.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003DD85 File Offset: 0x0003BF85
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040011EC RID: 4588
		public static GameObject projectilePrefab;

		// Token: 0x040011ED RID: 4589
		public static float baseDuration = 2f;

		// Token: 0x040011EE RID: 4590
		public static float damageCoefficient = 1.2f;

		// Token: 0x040011EF RID: 4591
		public static float force = 20f;

		// Token: 0x040011F0 RID: 4592
		private float duration;
	}
}
