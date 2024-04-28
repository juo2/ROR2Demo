using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.GreaterWispMonster
{
	// Token: 0x02000345 RID: 837
	public class FireCannons : BaseState
	{
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00040968 File Offset: 0x0003EB68
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			string text = "MuzzleLeft";
			string text2 = "MuzzleRight";
			this.duration = FireCannons.baseDuration / this.attackSpeedStat;
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, text, false);
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, text2, false);
			}
			base.PlayAnimation("Gesture", "FireCannons", "FireCannons.playbackRate", this.duration);
			if (base.isAuthority && base.modelLocator && base.modelLocator.modelTransform)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					int childIndex = component.FindChildIndex(text);
					int childIndex2 = component.FindChildIndex(text2);
					Transform transform = component.FindChild(childIndex);
					Transform transform2 = component.FindChild(childIndex2);
					if (transform)
					{
						ProjectileManager.instance.FireProjectile(this.projectilePrefab, transform.position, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
					}
					if (transform2)
					{
						ProjectileManager.instance.FireProjectile(this.projectilePrefab, transform2.position, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
					}
				}
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00040B24 File Offset: 0x0003ED24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040012B9 RID: 4793
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040012BA RID: 4794
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x040012BB RID: 4795
		public static float baseDuration = 2f;

		// Token: 0x040012BC RID: 4796
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x040012BD RID: 4797
		[SerializeField]
		public float force = 20f;

		// Token: 0x040012BE RID: 4798
		private float duration;
	}
}
