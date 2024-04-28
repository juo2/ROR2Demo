using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001E0 RID: 480
	public class ThrowSack : SackBaseState
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x000241E4 File Offset: 0x000223E4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ThrowSack.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(ThrowSack.sound, base.gameObject, this.attackSpeedStat);
			base.PlayAnimation("Body", "ThrowSack", "ThrowSack.playbackRate", this.duration);
			if (ThrowSack.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(ThrowSack.effectPrefab, base.gameObject, SackBaseState.muzzleName, false);
			}
			this.Fire();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00024264 File Offset: 0x00022464
		private void Fire()
		{
			Ray aimRay = base.GetAimRay();
			Ray ray = aimRay;
			Ray ray2 = aimRay;
			Vector3 point = aimRay.GetPoint(ThrowSack.minimumDistance);
			bool flag = false;
			RaycastHit raycastHit;
			if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, 500f, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
			{
				point = raycastHit.point;
				flag = true;
			}
			float magnitude = ThrowSack.projectileVelocity;
			if (flag)
			{
				Vector3 vector = point - ray2.origin;
				Vector2 a = new Vector2(vector.x, vector.z);
				float magnitude2 = a.magnitude;
				Vector2 vector2 = a / magnitude2;
				if (magnitude2 < ThrowSack.minimumDistance)
				{
					magnitude2 = ThrowSack.minimumDistance;
				}
				float y = Trajectory.CalculateInitialYSpeed(ThrowSack.timeToTarget, vector.y);
				float num = magnitude2 / ThrowSack.timeToTarget;
				Vector3 direction = new Vector3(vector2.x * num, y, vector2.y * num);
				magnitude = direction.magnitude;
				ray2.direction = direction;
			}
			for (int i = 0; i < ThrowSack.projectileCount; i++)
			{
				Quaternion rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(ray2.direction, ThrowSack.minSpread, ThrowSack.maxSpread, 1f, 1f, 0f, 0f));
				ProjectileManager.instance.FireProjectile(ThrowSack.projectilePrefab, ray2.origin, rotation, base.gameObject, this.damageStat * ThrowSack.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, magnitude);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002440F File Offset: 0x0002260F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000A09 RID: 2569
		public static float baseDuration;

		// Token: 0x04000A0A RID: 2570
		public static string sound;

		// Token: 0x04000A0B RID: 2571
		public static GameObject effectPrefab;

		// Token: 0x04000A0C RID: 2572
		public static GameObject projectilePrefab;

		// Token: 0x04000A0D RID: 2573
		public static float damageCoefficient;

		// Token: 0x04000A0E RID: 2574
		public static float force;

		// Token: 0x04000A0F RID: 2575
		public static float minSpread;

		// Token: 0x04000A10 RID: 2576
		public static float maxSpread;

		// Token: 0x04000A11 RID: 2577
		public static string attackSoundString;

		// Token: 0x04000A12 RID: 2578
		public static float projectileVelocity;

		// Token: 0x04000A13 RID: 2579
		public static float minimumDistance;

		// Token: 0x04000A14 RID: 2580
		public static float timeToTarget = 3f;

		// Token: 0x04000A15 RID: 2581
		public static int projectileCount;

		// Token: 0x04000A16 RID: 2582
		private float duration;
	}
}
