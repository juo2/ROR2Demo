using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000410 RID: 1040
	public class HitGroundState : BaseCaptainSupplyDropState
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x00053D08 File Offset: 0x00051F08
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = HitGroundState.baseDuration;
			if (NetworkServer.active)
			{
				GameObject ownerObject = base.GetComponent<GenericOwnership>().ownerObject;
				ProjectileDamage component = base.GetComponent<ProjectileDamage>();
				Vector3 position = base.transform.position;
				Vector3 vector = -base.transform.up;
				new BulletAttack
				{
					origin = position - vector * HitGroundState.impactBulletDistance,
					aimVector = vector,
					maxDistance = HitGroundState.impactBulletDistance + 1f,
					stopperMask = default(LayerMask),
					hitMask = LayerIndex.CommonMasks.bullet,
					damage = component.damage,
					damageColorIndex = component.damageColorIndex,
					damageType = component.damageType,
					bulletCount = 1U,
					minSpread = 0f,
					maxSpread = 0f,
					owner = ownerObject,
					weapon = base.gameObject,
					procCoefficient = 0f,
					falloffModel = BulletAttack.FalloffModel.None,
					isCrit = base.RollCrit(),
					smartCollision = false,
					sniper = false,
					force = component.force,
					radius = HitGroundState.impactBulletRadius,
					hitEffectPrefab = HitGroundState.effectPrefab
				}.Fire();
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00053E52 File Offset: 0x00052052
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new DeployState());
			}
		}

		// Token: 0x0400182E RID: 6190
		public static float baseDuration;

		// Token: 0x0400182F RID: 6191
		public static GameObject effectPrefab;

		// Token: 0x04001830 RID: 6192
		public static float impactBulletDistance;

		// Token: 0x04001831 RID: 6193
		public static float impactBulletRadius;

		// Token: 0x04001832 RID: 6194
		private float duration;
	}
}
