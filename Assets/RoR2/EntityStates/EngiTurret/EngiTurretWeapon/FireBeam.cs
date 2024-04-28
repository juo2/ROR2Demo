using System;
using RoR2;
using UnityEngine;

namespace EntityStates.EngiTurret.EngiTurretWeapon
{
	// Token: 0x0200038A RID: 906
	public class FireBeam : BaseState
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x0004754C File Offset: 0x0004574C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.attackSoundString, base.gameObject);
			this.fireTimer = 0f;
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(this.muzzleString);
					if (transform && this.laserPrefab)
					{
						this.laserEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.laserPrefab, transform.position, transform.rotation);
						this.laserEffectInstance.transform.parent = transform;
						this.laserEffectInstanceEndTransform = this.laserEffectInstance.GetComponent<ChildLocator>().FindChild("LaserEnd");
					}
				}
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00047614 File Offset: 0x00045814
		public override void OnExit()
		{
			if (this.laserEffectInstance)
			{
				EntityState.Destroy(this.laserEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00047634 File Offset: 0x00045834
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.laserRay = this.GetLaserRay();
			this.fireTimer += Time.fixedDeltaTime;
			float num = this.fireFrequency * base.characterBody.attackSpeed;
			float num2 = 1f / num;
			if (this.fireTimer > num2)
			{
				this.FireBullet(this.modelTransform, this.laserRay, this.muzzleString);
				this.fireTimer = 0f;
			}
			if (this.laserEffectInstance && this.laserEffectInstanceEndTransform)
			{
				this.laserEffectInstanceEndTransform.position = this.GetBeamEndPoint();
			}
			if (base.isAuthority && !this.ShouldFireLaser())
			{
				this.outer.SetNextState(this.GetNextState());
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000476F8 File Offset: 0x000458F8
		protected Vector3 GetBeamEndPoint()
		{
			Vector3 point = this.laserRay.GetPoint(this.maxDistance);
			RaycastHit raycastHit;
			if (Util.CharacterRaycast(base.gameObject, this.laserRay, out raycastHit, this.maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
			{
				point = raycastHit.point;
			}
			return point;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0000DE8C File Offset: 0x0000C08C
		protected virtual EntityState GetNextState()
		{
			return EntityStateCatalog.InstantiateState(this.outer.mainStateType);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00047766 File Offset: 0x00045966
		public virtual void ModifyBullet(BulletAttack bulletAttack)
		{
			bulletAttack.damageType |= DamageType.SlowOnHit;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00047776 File Offset: 0x00045976
		public virtual bool ShouldFireLaser()
		{
			return base.inputBank && base.inputBank.skill1.down;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00047797 File Offset: 0x00045997
		public virtual Ray GetLaserRay()
		{
			return base.GetAimRay();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000477A0 File Offset: 0x000459A0
		private void FireBullet(Transform modelTransform, Ray laserRay, string targetMuzzle)
		{
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = new BulletAttack();
				bulletAttack.owner = base.gameObject;
				bulletAttack.weapon = base.gameObject;
				bulletAttack.origin = laserRay.origin;
				bulletAttack.aimVector = laserRay.direction;
				bulletAttack.minSpread = this.minSpread;
				bulletAttack.maxSpread = this.maxSpread;
				bulletAttack.bulletCount = 1U;
				bulletAttack.damage = this.damageCoefficient * this.damageStat / this.fireFrequency;
				bulletAttack.procCoefficient = this.procCoefficient / this.fireFrequency;
				bulletAttack.force = this.force;
				bulletAttack.muzzleName = targetMuzzle;
				bulletAttack.hitEffectPrefab = this.hitEffectPrefab;
				bulletAttack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
				bulletAttack.HitEffectNormal = false;
				bulletAttack.radius = 0f;
				bulletAttack.maxDistance = this.maxDistance;
				this.ModifyBullet(bulletAttack);
				bulletAttack.Fire();
			}
		}

		// Token: 0x040014A2 RID: 5282
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x040014A3 RID: 5283
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x040014A4 RID: 5284
		[SerializeField]
		public GameObject laserPrefab;

		// Token: 0x040014A5 RID: 5285
		[SerializeField]
		public string muzzleString;

		// Token: 0x040014A6 RID: 5286
		[SerializeField]
		public string attackSoundString;

		// Token: 0x040014A7 RID: 5287
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x040014A8 RID: 5288
		[SerializeField]
		public float procCoefficient;

		// Token: 0x040014A9 RID: 5289
		[SerializeField]
		public float force;

		// Token: 0x040014AA RID: 5290
		[SerializeField]
		public float minSpread;

		// Token: 0x040014AB RID: 5291
		[SerializeField]
		public float maxSpread;

		// Token: 0x040014AC RID: 5292
		[SerializeField]
		public int bulletCount;

		// Token: 0x040014AD RID: 5293
		[SerializeField]
		public float fireFrequency;

		// Token: 0x040014AE RID: 5294
		[SerializeField]
		public float maxDistance;

		// Token: 0x040014AF RID: 5295
		private float fireTimer;

		// Token: 0x040014B0 RID: 5296
		private Ray laserRay;

		// Token: 0x040014B1 RID: 5297
		private Transform modelTransform;

		// Token: 0x040014B2 RID: 5298
		private GameObject laserEffectInstance;

		// Token: 0x040014B3 RID: 5299
		private Transform laserEffectInstanceEndTransform;

		// Token: 0x040014B4 RID: 5300
		public int bulletCountCurrent = 1;
	}
}
