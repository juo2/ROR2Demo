using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.HermitCrab
{
	// Token: 0x02000329 RID: 809
	public class FireMortar : BaseState
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x0003EAA8 File Offset: 0x0003CCA8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireMortar.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Gesture, Additive", "FireMortar", 0f);
			Util.PlaySound(FireMortar.mortarSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(FireMortar.mortarMuzzleflashEffect, base.gameObject, FireMortar.mortarMuzzleName, false);
			if (base.isAuthority)
			{
				this.Fire();
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003EB18 File Offset: 0x0003CD18
		private void Fire()
		{
			Ray aimRay = base.GetAimRay();
			Ray ray = new Ray(aimRay.origin, Vector3.up);
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
			float magnitude = FireMortar.projectileVelocity;
			if (flag)
			{
				Vector3 vector = a - ray.origin;
				Vector2 a2 = new Vector2(vector.x, vector.z);
				float magnitude2 = a2.magnitude;
				Vector2 vector2 = a2 / magnitude2;
				if (magnitude2 < FireMortar.minimumDistance)
				{
					magnitude2 = FireMortar.minimumDistance;
				}
				float y = Trajectory.CalculateInitialYSpeed(FireMortar.timeToTarget, vector.y);
				float num = magnitude2 / FireMortar.timeToTarget;
				Vector3 direction = new Vector3(vector2.x * num, y, vector2.y * num);
				magnitude = direction.magnitude;
				ray.direction = direction;
			}
			for (int i = 0; i < FireMortar.mortarCount; i++)
			{
				Quaternion rotation = Util.QuaternionSafeLookRotation(ray.direction + ((i != 0) ? (UnityEngine.Random.insideUnitSphere * 0.05f) : Vector3.zero));
				ProjectileManager.instance.FireProjectile(FireMortar.mortarProjectilePrefab, ray.origin, rotation, base.gameObject, this.damageStat * FireMortar.mortarDamageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, magnitude);
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003ED44 File Offset: 0x0003CF44
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > this.duration)
			{
				Burrowed burrowed = new Burrowed();
				burrowed.duration = Burrowed.mortarCooldown;
				this.outer.SetNextState(burrowed);
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001237 RID: 4663
		public static GameObject mortarProjectilePrefab;

		// Token: 0x04001238 RID: 4664
		public static GameObject mortarMuzzleflashEffect;

		// Token: 0x04001239 RID: 4665
		public static int mortarCount;

		// Token: 0x0400123A RID: 4666
		public static string mortarMuzzleName;

		// Token: 0x0400123B RID: 4667
		public static string mortarSoundString;

		// Token: 0x0400123C RID: 4668
		public static float mortarDamageCoefficient;

		// Token: 0x0400123D RID: 4669
		public static float baseDuration;

		// Token: 0x0400123E RID: 4670
		public static float timeToTarget = 3f;

		// Token: 0x0400123F RID: 4671
		public static float projectileVelocity = 55f;

		// Token: 0x04001240 RID: 4672
		public static float minimumDistance;

		// Token: 0x04001241 RID: 4673
		private float stopwatch;

		// Token: 0x04001242 RID: 4674
		private float duration;
	}
}
