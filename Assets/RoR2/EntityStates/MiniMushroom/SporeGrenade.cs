using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.MiniMushroom
{
	// Token: 0x02000272 RID: 626
	public class SporeGrenade : BaseState
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002C918 File Offset: 0x0002AB18
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = SporeGrenade.baseDuration / this.attackSpeedStat;
			this.chargeTime = SporeGrenade.baseChargeTime / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetBool("isCharged", false);
				this.PlayAnimation("Gesture, Additive", "Charge");
				this.chargeupSoundID = Util.PlaySound(SporeGrenade.chargeUpSoundString, base.characterBody.modelLocator.modelTransform.gameObject);
			}
			Transform transform = base.FindModelChild("ChargeSpot");
			if (transform)
			{
				this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SporeGrenade.chargeEffectPrefab, transform);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.chargeTime)
			{
				if (!this.hasFired)
				{
					this.hasFired = true;
					Animator animator = this.modelAnimator;
					if (animator != null)
					{
						animator.SetBool("isCharged", true);
					}
					if (base.isAuthority)
					{
						this.FireGrenade(SporeGrenade.muzzleString);
					}
				}
				if (base.isAuthority && base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002CA50 File Offset: 0x0002AC50
		public override void OnExit()
		{
			this.PlayAnimation("Gesture, Additive", "Empty");
			AkSoundEngine.StopPlayingID(this.chargeupSoundID);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002CA8C File Offset: 0x0002AC8C
		private void FireGrenade(string targetMuzzle)
		{
			Ray aimRay = base.GetAimRay();
			Ray ray = new Ray(aimRay.origin, Vector3.up);
			Transform transform = base.FindModelChild(targetMuzzle);
			if (transform)
			{
				ray.origin = transform.position;
			}
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = aimRay.origin;
			bullseyeSearch.searchDirection = aimRay.direction;
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			if (base.teamComponent)
			{
				bullseyeSearch.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
			}
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
			bullseyeSearch.RefreshCandidates();
			HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
			bool flag = false;
			Vector3 a = Vector3.zero;
			RaycastHit raycastHit;
			if (hurtBox)
			{
				a = hurtBox.transform.position;
				flag = true;
			}
			else if (Physics.Raycast(aimRay, out raycastHit, 1000f, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
			{
				a = raycastHit.point;
				flag = true;
			}
			float magnitude = SporeGrenade.projectileVelocity;
			if (flag)
			{
				Vector3 vector = a - ray.origin;
				Vector2 a2 = new Vector2(vector.x, vector.z);
				float magnitude2 = a2.magnitude;
				Vector2 vector2 = a2 / magnitude2;
				if (magnitude2 < SporeGrenade.minimumDistance)
				{
					magnitude2 = SporeGrenade.minimumDistance;
				}
				if (magnitude2 > SporeGrenade.maximumDistance)
				{
					magnitude2 = SporeGrenade.maximumDistance;
				}
				float y = Trajectory.CalculateInitialYSpeed(SporeGrenade.timeToTarget, vector.y);
				float num = magnitude2 / SporeGrenade.timeToTarget;
				Vector3 direction = new Vector3(vector2.x * num, y, vector2.y * num);
				magnitude = direction.magnitude;
				ray.direction = direction;
			}
			Quaternion rotation = Util.QuaternionSafeLookRotation(ray.direction + UnityEngine.Random.insideUnitSphere * 0.05f);
			ProjectileManager.instance.FireProjectile(SporeGrenade.projectilePrefab, ray.origin, rotation, base.gameObject, this.damageStat * SporeGrenade.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, magnitude);
		}

		// Token: 0x04000C78 RID: 3192
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000C79 RID: 3193
		public static string attackSoundString;

		// Token: 0x04000C7A RID: 3194
		public static string chargeUpSoundString;

		// Token: 0x04000C7B RID: 3195
		public static float recoilAmplitude = 1f;

		// Token: 0x04000C7C RID: 3196
		public static GameObject projectilePrefab;

		// Token: 0x04000C7D RID: 3197
		public static float baseDuration = 2f;

		// Token: 0x04000C7E RID: 3198
		public static string muzzleString;

		// Token: 0x04000C7F RID: 3199
		public static float damageCoefficient;

		// Token: 0x04000C80 RID: 3200
		public static float timeToTarget = 3f;

		// Token: 0x04000C81 RID: 3201
		public static float projectileVelocity = 55f;

		// Token: 0x04000C82 RID: 3202
		public static float minimumDistance;

		// Token: 0x04000C83 RID: 3203
		public static float maximumDistance;

		// Token: 0x04000C84 RID: 3204
		public static float baseChargeTime = 2f;

		// Token: 0x04000C85 RID: 3205
		private uint chargeupSoundID;

		// Token: 0x04000C86 RID: 3206
		private Ray projectileRay;

		// Token: 0x04000C87 RID: 3207
		private Transform modelTransform;

		// Token: 0x04000C88 RID: 3208
		private float duration;

		// Token: 0x04000C89 RID: 3209
		private float chargeTime;

		// Token: 0x04000C8A RID: 3210
		private bool hasFired;

		// Token: 0x04000C8B RID: 3211
		private Animator modelAnimator;

		// Token: 0x04000C8C RID: 3212
		private GameObject chargeEffectInstance;
	}
}
