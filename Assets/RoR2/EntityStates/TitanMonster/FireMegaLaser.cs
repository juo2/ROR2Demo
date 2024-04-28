using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000366 RID: 870
	public class FireMegaLaser : BaseState
	{
		// Token: 0x06000F9E RID: 3998 RVA: 0x00044BBC File Offset: 0x00042DBC
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterBody.SetAimTimer(FireMegaLaser.maximumDuration);
			Util.PlaySound(FireMegaLaser.playAttackSoundString, base.gameObject);
			Util.PlaySound(FireMegaLaser.playLoopSoundString, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "FireLaserLoop", 0.25f);
			this.enemyFinder = new BullseyeSearch();
			this.enemyFinder.viewer = base.characterBody;
			this.enemyFinder.maxDistanceFilter = FireMegaLaser.maxDistance;
			this.enemyFinder.maxAngleFilter = FireMegaLaser.lockOnAngle;
			this.enemyFinder.searchOrigin = this.aimRay.origin;
			this.enemyFinder.searchDirection = this.aimRay.direction;
			this.enemyFinder.filterByLoS = false;
			this.enemyFinder.sortMode = BullseyeSearch.SortMode.Angle;
			this.enemyFinder.teamMaskFilter = TeamMask.allButNeutral;
			if (base.teamComponent)
			{
				this.enemyFinder.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
			}
			this.aimRay = base.GetAimRay();
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.muzzleTransform = component.FindChild("MuzzleLaser");
					if (this.muzzleTransform && this.laserPrefab)
					{
						this.laserEffect = UnityEngine.Object.Instantiate<GameObject>(this.laserPrefab, this.muzzleTransform.position, this.muzzleTransform.rotation);
						this.laserEffect.transform.parent = this.muzzleTransform;
						this.laserChildLocator = this.laserEffect.GetComponent<ChildLocator>();
						this.laserEffectEnd = this.laserChildLocator.FindChild("LaserEnd");
					}
				}
			}
			this.UpdateLockOn();
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00044DA4 File Offset: 0x00042FA4
		public override void OnExit()
		{
			if (this.laserEffect)
			{
				EntityState.Destroy(this.laserEffect);
			}
			base.characterBody.SetAimTimer(2f);
			Util.PlaySound(FireMegaLaser.stopLoopSoundString, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "FireLaserEnd", 0.25f);
			base.OnExit();
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00044E08 File Offset: 0x00043008
		private void UpdateLockOn()
		{
			if (base.isAuthority)
			{
				this.enemyFinder.searchOrigin = this.aimRay.origin;
				this.enemyFinder.searchDirection = this.aimRay.direction;
				this.enemyFinder.RefreshCandidates();
				HurtBox exists = this.enemyFinder.GetResults().FirstOrDefault<HurtBox>();
				this.lockedOnHurtBox = exists;
				this.foundAnyTarget = exists;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00044E78 File Offset: 0x00043078
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireStopwatch += Time.fixedDeltaTime;
			this.stopwatch += Time.fixedDeltaTime;
			this.aimRay = base.GetAimRay();
			Vector3 vector = this.aimRay.origin;
			if (this.muzzleTransform)
			{
				vector = this.muzzleTransform.position;
			}
			Vector3 vector2;
			RaycastHit raycastHit;
			if (this.lockedOnHurtBox)
			{
				vector2 = this.lockedOnHurtBox.transform.position;
			}
			else if (Util.CharacterRaycast(base.gameObject, this.aimRay, out raycastHit, FireMegaLaser.maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
			{
				vector2 = raycastHit.point;
			}
			else
			{
				vector2 = this.aimRay.GetPoint(FireMegaLaser.maxDistance);
			}
			Ray ray = new Ray(vector, vector2 - vector);
			bool flag = false;
			if (this.laserEffect && this.laserChildLocator)
			{
				RaycastHit raycastHit2;
				if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit2, (vector2 - vector).magnitude, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
				{
					vector2 = raycastHit2.point;
					RaycastHit raycastHit3;
					if (Util.CharacterRaycast(base.gameObject, new Ray(vector2 - ray.direction * 0.1f, -ray.direction), out raycastHit3, raycastHit2.distance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
					{
						vector2 = ray.GetPoint(0.1f);
						flag = true;
					}
				}
				this.laserEffect.transform.rotation = Util.QuaternionSafeLookRotation(vector2 - vector);
				this.laserEffectEnd.transform.position = vector2;
			}
			if (this.fireStopwatch > 1f / FireMegaLaser.fireFrequency)
			{
				string targetMuzzle = "MuzzleLaser";
				if (!flag)
				{
					this.FireBullet(this.modelTransform, ray, targetMuzzle, (vector2 - ray.origin).magnitude + 0.1f);
				}
				this.UpdateLockOn();
				this.fireStopwatch -= 1f / FireMegaLaser.fireFrequency;
			}
			if (base.isAuthority && (((!base.inputBank || !base.inputBank.skill4.down) && this.stopwatch > FireMegaLaser.minimumDuration) || this.stopwatch > FireMegaLaser.maximumDuration))
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00045148 File Offset: 0x00043348
		private void FireBullet(Transform modelTransform, Ray aimRay, string targetMuzzle, float maxDistance)
		{
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireMegaLaser.minSpread,
					maxSpread = FireMegaLaser.maxSpread,
					bulletCount = 1U,
					damage = FireMegaLaser.damageCoefficient * this.damageStat / FireMegaLaser.fireFrequency,
					force = FireMegaLaser.force,
					muzzleName = targetMuzzle,
					hitEffectPrefab = this.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					procCoefficient = FireMegaLaser.procCoefficientPerTick,
					HitEffectNormal = false,
					radius = 0f,
					maxDistance = maxDistance
				}.Fire();
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00045250 File Offset: 0x00043450
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(HurtBoxReference.FromHurtBox(this.lockedOnHurtBox));
			writer.Write(this.stopwatch);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00045278 File Offset: 0x00043478
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			HurtBoxReference hurtBoxReference = reader.ReadHurtBoxReference();
			this.stopwatch = reader.ReadSingle();
			GameObject gameObject = hurtBoxReference.ResolveGameObject();
			this.lockedOnHurtBox = ((gameObject != null) ? gameObject.GetComponent<HurtBox>() : null);
		}

		// Token: 0x040013DE RID: 5086
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x040013DF RID: 5087
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x040013E0 RID: 5088
		[SerializeField]
		public GameObject laserPrefab;

		// Token: 0x040013E1 RID: 5089
		public static string playAttackSoundString;

		// Token: 0x040013E2 RID: 5090
		public static string playLoopSoundString;

		// Token: 0x040013E3 RID: 5091
		public static string stopLoopSoundString;

		// Token: 0x040013E4 RID: 5092
		public static float damageCoefficient;

		// Token: 0x040013E5 RID: 5093
		public static float force;

		// Token: 0x040013E6 RID: 5094
		public static float minSpread;

		// Token: 0x040013E7 RID: 5095
		public static float maxSpread;

		// Token: 0x040013E8 RID: 5096
		public static int bulletCount;

		// Token: 0x040013E9 RID: 5097
		public static float fireFrequency;

		// Token: 0x040013EA RID: 5098
		public static float maxDistance;

		// Token: 0x040013EB RID: 5099
		public static float minimumDuration;

		// Token: 0x040013EC RID: 5100
		public static float maximumDuration;

		// Token: 0x040013ED RID: 5101
		public static float lockOnAngle;

		// Token: 0x040013EE RID: 5102
		public static float procCoefficientPerTick;

		// Token: 0x040013EF RID: 5103
		private HurtBox lockedOnHurtBox;

		// Token: 0x040013F0 RID: 5104
		private float fireStopwatch;

		// Token: 0x040013F1 RID: 5105
		private float stopwatch;

		// Token: 0x040013F2 RID: 5106
		private Ray aimRay;

		// Token: 0x040013F3 RID: 5107
		private Transform modelTransform;

		// Token: 0x040013F4 RID: 5108
		private GameObject laserEffect;

		// Token: 0x040013F5 RID: 5109
		private ChildLocator laserChildLocator;

		// Token: 0x040013F6 RID: 5110
		private Transform laserEffectEnd;

		// Token: 0x040013F7 RID: 5111
		protected Transform muzzleTransform;

		// Token: 0x040013F8 RID: 5112
		private BullseyeSearch enemyFinder;

		// Token: 0x040013F9 RID: 5113
		private bool foundAnyTarget;
	}
}
