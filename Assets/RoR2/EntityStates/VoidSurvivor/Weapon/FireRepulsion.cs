using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000104 RID: 260
	public class FireRepulsion : BaseState
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00013374 File Offset: 0x00011574
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.aimRay = base.GetAimRay();
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.duration, this.animationCrossfadeDuration);
			Util.PlaySound(this.sound, base.gameObject);
			Vector3 origin = this.aimRay.origin;
			Transform transform = base.FindModelChild(this.muzzle);
			if (transform)
			{
				origin = transform.position;
			}
			EffectManager.SpawnEffect(this.fireEffectPrefab, new EffectData
			{
				origin = origin,
				rotation = Quaternion.LookRotation(this.aimRay.direction)
			}, false);
			this.aimRay.origin = this.aimRay.origin - this.aimRay.direction * this.backupDistance;
			if (NetworkServer.active)
			{
				this.PushEnemies();
				this.ReflectProjectiles();
				VoidSurvivorController component = base.GetComponent<VoidSurvivorController>();
				if (component)
				{
					component.AddCorruption(this.corruption);
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001348C File Offset: 0x0001168C
		private void ReflectProjectiles()
		{
			Vector3 vector = base.characterBody ? base.characterBody.corePosition : Vector3.zero;
			TeamIndex teamIndex = base.characterBody ? base.characterBody.teamComponent.teamIndex : TeamIndex.None;
			float num = this.maxProjectileReflectDistance * this.maxProjectileReflectDistance;
			List<ProjectileController> instancesList = InstanceTracker.GetInstancesList<ProjectileController>();
			List<ProjectileController> list = new List<ProjectileController>();
			int i = 0;
			int count = instancesList.Count;
			while (i < count)
			{
				ProjectileController projectileController = instancesList[i];
				if (projectileController.teamFilter.teamIndex != teamIndex && (projectileController.transform.position - vector).sqrMagnitude < num)
				{
					list.Add(projectileController);
				}
				i++;
			}
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				ProjectileController projectileController2 = list[j];
				if (projectileController2)
				{
					Vector3 position = projectileController2.transform.position;
					Vector3 start = vector;
					if (this.tracerEffectPrefab)
					{
						EffectData effectData = new EffectData
						{
							origin = position,
							start = start
						};
						EffectManager.SpawnEffect(this.tracerEffectPrefab, effectData, true);
					}
					GameObject owner = projectileController2.owner;
					CharacterBody component = projectileController2.owner.GetComponent<CharacterBody>();
					projectileController2.IgnoreCollisionsWithOwner(false);
					projectileController2.Networkowner = base.gameObject;
					projectileController2.teamFilter.teamIndex = base.characterBody.teamComponent.teamIndex;
					ProjectileDamage component2 = projectileController2.GetComponent<ProjectileDamage>();
					if (component2)
					{
						component2.damage *= this.damageMultiplier;
					}
					Rigidbody component3 = projectileController2.GetComponent<Rigidbody>();
					if (component3)
					{
						Vector3 vector2 = component3.velocity * -1f;
						if (component)
						{
							vector2 = component.corePosition - component3.transform.position;
						}
						component3.transform.forward = vector2;
						component3.velocity = Vector3.RotateTowards(component3.velocity, vector2, float.PositiveInfinity, 0f);
					}
				}
				j++;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000136AC File Offset: 0x000118AC
		private void PushEnemies()
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.teamMaskFilter = TeamMask.all;
			bullseyeSearch.maxAngleFilter = this.fieldOfView * 0.5f;
			bullseyeSearch.maxDistanceFilter = this.maxKnockbackDistance;
			bullseyeSearch.searchOrigin = this.aimRay.origin;
			bullseyeSearch.searchDirection = this.aimRay.direction;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.RefreshCandidates();
			bullseyeSearch.FilterOutGameObject(base.gameObject);
			IEnumerable<HurtBox> enumerable = bullseyeSearch.GetResults().Where(new Func<HurtBox, bool>(Util.IsValid)).Distinct(default(HurtBox.EntityEqualityComparer));
			TeamIndex team = base.GetTeam();
			foreach (HurtBox hurtBox in enumerable)
			{
				if (FriendlyFireManager.ShouldSplashHitProceed(hurtBox.healthComponent, team))
				{
					CharacterBody body = hurtBox.healthComponent.body;
					this.AddDebuff(body);
					body.RecalculateStats();
					float acceleration = body.acceleration;
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000137C0 File Offset: 0x000119C0
		protected virtual void AddDebuff(CharacterBody body)
		{
			body.AddTimedBuff(this.buffDef, this.buffDuration);
			SetStateOnHurt component = body.healthComponent.GetComponent<SetStateOnHurt>();
			if (component == null)
			{
				return;
			}
			component.SetStun(-1f);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float CalculateDamage()
		{
			return 0f;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float CalculateProcCoefficient()
		{
			return 0f;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000137F5 File Offset: 0x000119F5
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040004F9 RID: 1273
		[SerializeField]
		public string sound;

		// Token: 0x040004FA RID: 1274
		[SerializeField]
		public string muzzle;

		// Token: 0x040004FB RID: 1275
		[SerializeField]
		public GameObject fireEffectPrefab;

		// Token: 0x040004FC RID: 1276
		[SerializeField]
		public float baseDuration;

		// Token: 0x040004FD RID: 1277
		[SerializeField]
		public float fieldOfView;

		// Token: 0x040004FE RID: 1278
		[SerializeField]
		public float backupDistance;

		// Token: 0x040004FF RID: 1279
		[SerializeField]
		public float maxKnockbackDistance;

		// Token: 0x04000500 RID: 1280
		[SerializeField]
		public float idealDistanceToPlaceTargets;

		// Token: 0x04000501 RID: 1281
		[SerializeField]
		public float liftVelocity;

		// Token: 0x04000502 RID: 1282
		[SerializeField]
		public float animationCrossfadeDuration;

		// Token: 0x04000503 RID: 1283
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000504 RID: 1284
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000505 RID: 1285
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x04000506 RID: 1286
		[SerializeField]
		public float damageMultiplier;

		// Token: 0x04000507 RID: 1287
		[SerializeField]
		public float maxProjectileReflectDistance;

		// Token: 0x04000508 RID: 1288
		[SerializeField]
		public GameObject tracerEffectPrefab;

		// Token: 0x04000509 RID: 1289
		[SerializeField]
		public float corruption;

		// Token: 0x0400050A RID: 1290
		[SerializeField]
		public BuffDef buffDef;

		// Token: 0x0400050B RID: 1291
		[SerializeField]
		public float buffDuration;

		// Token: 0x0400050C RID: 1292
		public static AnimationCurve shoveSuitabilityCurve;

		// Token: 0x0400050D RID: 1293
		private float duration;

		// Token: 0x0400050E RID: 1294
		private Ray aimRay;
	}
}
