using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.ClayGrenadier
{
	// Token: 0x020003FE RID: 1022
	public class ThrowBarrel : GenericProjectileBaseState
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x00051FA8 File Offset: 0x000501A8
		protected override void PlayAnimation(float duration)
		{
			base.PlayAnimation(duration);
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.playbackRateParam, duration, 0.2f);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00051FD0 File Offset: 0x000501D0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlayAttackSpeedSound(this.enterSoundString, base.gameObject, this.attackSpeedStat);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			base.characterBody.SetAimTimer(0f);
			Transform transform = base.FindModelChild(this.chargeEffectMuzzleString);
			if (transform && this.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeInstance.transform.parent = transform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.delayBeforeFiringProjectile;
				}
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000520A4 File Offset: 0x000502A4
		protected override Ray ModifyProjectileAimRay(Ray aimRay)
		{
			RaycastHit raycastHit = default(RaycastHit);
			Ray ray = aimRay;
			float num = this.projectilePrefab.GetComponent<ProjectileSimple>().desiredForwardSpeed;
			ray.origin = aimRay.origin;
			if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
			{
				float num2 = num;
				Vector3 vector = raycastHit.point - aimRay.origin;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				float magnitude = vector2.magnitude;
				float y = Trajectory.CalculateInitialYSpeed(magnitude / num2, vector.y);
				Vector3 a = new Vector3(vector2.x / magnitude * num2, y, vector2.y / magnitude * num2);
				num = a.magnitude;
				aimRay.direction = a / num;
			}
			aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, this.projectileYawBonusPerProjectile * (float)this.currentProjectileCount, this.projectilePitchBonusPerProjectile * (float)this.currentProjectileCount);
			return aimRay;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000521E4 File Offset: 0x000503E4
		protected override void FireProjectile()
		{
			for (int i = 0; i < this.projectileCount; i++)
			{
				base.FireProjectile();
				this.currentProjectileCount++;
			}
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0005222E File Offset: 0x0005042E
		public override void OnExit()
		{
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			base.OnExit();
		}

		// Token: 0x0400179F RID: 6047
		[SerializeField]
		public float aimCalculationRaycastDistance;

		// Token: 0x040017A0 RID: 6048
		[SerializeField]
		public string animationLayerName = "Body";

		// Token: 0x040017A1 RID: 6049
		[SerializeField]
		public string animationStateName = "FaceSlam";

		// Token: 0x040017A2 RID: 6050
		[SerializeField]
		public string playbackRateParam = "FaceSlam.playbackRate";

		// Token: 0x040017A3 RID: 6051
		[SerializeField]
		public int projectileCount;

		// Token: 0x040017A4 RID: 6052
		[SerializeField]
		public float projectilePitchBonusPerProjectile;

		// Token: 0x040017A5 RID: 6053
		[SerializeField]
		public float projectileYawBonusPerProjectile;

		// Token: 0x040017A6 RID: 6054
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x040017A7 RID: 6055
		[SerializeField]
		public string chargeEffectMuzzleString;

		// Token: 0x040017A8 RID: 6056
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040017A9 RID: 6057
		private GameObject chargeInstance;

		// Token: 0x040017AA RID: 6058
		private int currentProjectileCount;
	}
}
