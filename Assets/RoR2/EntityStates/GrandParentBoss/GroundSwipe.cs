using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000352 RID: 850
	public class GroundSwipe : BasicMeleeAttack, SteppedSkillDef.IStepSetter
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000417FA File Offset: 0x0003F9FA
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x00041802 File Offset: 0x0003FA02
		private protected ChildLocator childLocator { protected get; private set; }

		// Token: 0x06000F3A RID: 3898 RVA: 0x0004180B File Offset: 0x0003FA0B
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.step = i;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00041814 File Offset: 0x0003FA14
		public override void OnEnter()
		{
			base.OnEnter();
			this.hasFired = false;
			this.chargeStartDelay = this.baseChargeStartDelay / this.attackSpeedStat;
			this.fireProjectileDelay = this.baseFireProjectileDelay / this.attackSpeedStat;
			this.childLocator = base.GetModelChildLocator();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00041860 File Offset: 0x0003FA60
		public override void OnExit()
		{
			EntityState.Destroy(this.chargeEffectInstance);
			base.OnExit();
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00041873 File Offset: 0x0003FA73
		protected override void PlayAnimation()
		{
			base.PlayCrossfade(this.animationLayerName, this.GetAnimationStateName(), this.playbackRateParam, this.duration, 1f);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00041898 File Offset: 0x0003FA98
		public string GetMuzzleName()
		{
			if (this.step % 2 == 0)
			{
				return this.muzzleNameLeft;
			}
			return this.muzzleNameRight;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000418B1 File Offset: 0x0003FAB1
		public string GetAnimationStateName()
		{
			if (this.step % 2 == 0)
			{
				return this.animationStateNameLeft;
			}
			return this.animationStateNameRight;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000418CA File Offset: 0x0003FACA
		public override string GetHitBoxGroupName()
		{
			if (this.step % 2 == 0)
			{
				return this.hitBoxGroupNameLeft;
			}
			return this.hitBoxGroupName;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000418E4 File Offset: 0x0003FAE4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.chargeEffectInstance && this.childLocator && base.fixedAge >= this.chargeStartDelay)
			{
				Transform transform = this.childLocator.FindChild(this.GetMuzzleName()) ?? base.characterBody.coreTransform;
				if (transform)
				{
					if (this.chargeEffectPrefab)
					{
						this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeEffectInstance.transform.parent = transform;
						ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
						ObjectScaleCurve component2 = this.chargeEffectInstance.GetComponent<ObjectScaleCurve>();
						if (component)
						{
							component.newDuration = this.duration;
						}
						if (component2)
						{
							component2.timeMax = this.duration;
						}
					}
					if (this.chargeSoundName != null)
					{
						Util.PlaySound(this.chargeSoundName, base.gameObject);
					}
				}
			}
			if (base.isAuthority && !this.hasFired && this.projectilePrefab && base.fixedAge >= this.fireProjectileDelay)
			{
				this.hasFired = true;
				Ray aimRay = base.GetAimRay();
				Ray ray = aimRay;
				Transform transform2 = this.childLocator.FindChild(this.GetMuzzleName());
				if (transform2)
				{
					ray.origin = transform2.position;
				}
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.viewer = base.characterBody;
				bullseyeSearch.searchOrigin = base.characterBody.corePosition;
				bullseyeSearch.searchDirection = base.characterBody.corePosition;
				bullseyeSearch.maxDistanceFilter = this.maxBullseyeDistance;
				bullseyeSearch.maxAngleFilter = this.maxBullseyeAngle;
				bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
				bullseyeSearch.RefreshCandidates();
				HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				Vector3? vector = null;
				RaycastHit raycastHit;
				if (hurtBox)
				{
					vector = new Vector3?(hurtBox.transform.position);
				}
				else if (Physics.Raycast(aimRay, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					vector = new Vector3?(raycastHit.point);
				}
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					projectilePrefab = this.projectilePrefab,
					position = ray.origin,
					owner = base.gameObject,
					damage = this.damageStat * this.projectileDamageCoefficient,
					force = this.projectileForce,
					crit = base.RollCrit()
				};
				if (vector != null)
				{
					Vector3 vector2 = vector.Value - ray.origin;
					Vector2 a = new Vector2(vector2.x, vector2.z);
					float num = a.magnitude;
					Vector2 vector3 = a / num;
					num = Mathf.Clamp(num, this.minProjectileDistance, this.maxProjectileDistance);
					float y = Trajectory.CalculateInitialYSpeed(num / this.projectileHorizontalSpeed, vector2.y);
					Vector3 direction = new Vector3(vector3.x * this.projectileHorizontalSpeed, y, vector3.y * this.projectileHorizontalSpeed);
					fireProjectileInfo.speedOverride = direction.magnitude;
					ray.direction = direction;
				}
				else
				{
					fireProjectileInfo.speedOverride = this.projectileHorizontalSpeed;
				}
				fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(ray.direction);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x04001300 RID: 4864
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x04001301 RID: 4865
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04001302 RID: 4866
		[SerializeField]
		public float baseChargeStartDelay;

		// Token: 0x04001303 RID: 4867
		[SerializeField]
		public float baseFireProjectileDelay;

		// Token: 0x04001304 RID: 4868
		[SerializeField]
		public float projectileDamageCoefficient;

		// Token: 0x04001305 RID: 4869
		[SerializeField]
		public float projectileForce;

		// Token: 0x04001306 RID: 4870
		[SerializeField]
		public float maxBullseyeDistance;

		// Token: 0x04001307 RID: 4871
		[SerializeField]
		public float maxBullseyeAngle;

		// Token: 0x04001308 RID: 4872
		[SerializeField]
		public float minProjectileDistance;

		// Token: 0x04001309 RID: 4873
		[SerializeField]
		public float maxProjectileDistance;

		// Token: 0x0400130A RID: 4874
		[SerializeField]
		public float projectileHorizontalSpeed;

		// Token: 0x0400130B RID: 4875
		[SerializeField]
		public string chargeSoundName;

		// Token: 0x0400130C RID: 4876
		[SerializeField]
		public string hitBoxGroupNameLeft;

		// Token: 0x0400130D RID: 4877
		[SerializeField]
		public string animationLayerName = "Body";

		// Token: 0x0400130E RID: 4878
		[SerializeField]
		public string muzzleNameLeft = "SecondaryProjectileMuzzle";

		// Token: 0x0400130F RID: 4879
		[SerializeField]
		public string muzzleNameRight = "SecondaryProjectileMuzzle";

		// Token: 0x04001310 RID: 4880
		[SerializeField]
		public string animationStateNameLeft = "FireSecondaryProjectile";

		// Token: 0x04001311 RID: 4881
		[SerializeField]
		public string animationStateNameRight = "FireSecondaryProjectile";

		// Token: 0x04001312 RID: 4882
		[SerializeField]
		public string playbackRateParam = "GroundSwipe.playbackRate";

		// Token: 0x04001313 RID: 4883
		private GameObject chargeEffectInstance;

		// Token: 0x04001315 RID: 4885
		private bool hasFired;

		// Token: 0x04001316 RID: 4886
		private float chargeStartDelay;

		// Token: 0x04001317 RID: 4887
		private float fireProjectileDelay;

		// Token: 0x04001318 RID: 4888
		private int step;
	}
}
