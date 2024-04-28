using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.MineDeployer
{
	// Token: 0x02000396 RID: 918
	public class FireMine : BaseMineDeployerState
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x000482CC File Offset: 0x000464CC
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				FireMine.ResolveVelocities();
				Transform transform = base.transform.Find("FirePoint");
				ProjectileDamage component = base.GetComponent<ProjectileDamage>();
				Vector3 forward = transform.TransformVector(FireMine.velocities[this.fireIndex]);
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					crit = component.crit,
					damage = component.damage,
					damageColorIndex = component.damageColorIndex,
					force = component.force,
					owner = base.owner,
					position = transform.position,
					procChainMask = base.projectileController.procChainMask,
					projectilePrefab = FireMine.projectilePrefab,
					rotation = Quaternion.LookRotation(forward),
					fuseOverride = -1f,
					useFuseOverride = false,
					speedOverride = forward.magnitude,
					useSpeedOverride = true
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x000483D8 File Offset: 0x000465D8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && FireMine.duration <= base.fixedAge)
			{
				int num = this.fireIndex + 1;
				if (num < FireMine.velocities.Length)
				{
					this.outer.SetNextState(new FireMine
					{
						fireIndex = num
					});
					return;
				}
				this.outer.SetNextState(new WaitForDeath());
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0004843C File Offset: 0x0004663C
		private static Vector3[] GeneratePoints(float radius)
		{
			Vector3[] array = new Vector3[9];
			Quaternion rotation = Quaternion.AngleAxis(60f, Vector3.up);
			Quaternion rotation2 = Quaternion.AngleAxis(120f, Vector3.up);
			Vector3 forward = Vector3.forward;
			array[0] = forward;
			array[1] = rotation2 * array[0];
			array[2] = rotation2 * array[1];
			float num = 1f;
			float num2 = Vector3.Distance(array[0], array[1]);
			float d = Mathf.Sqrt(num * num + num2 * num2) / num;
			array[3] = rotation * (array[2] * d);
			array[4] = rotation2 * array[3];
			array[5] = rotation2 * array[4];
			d = 1f;
			array[6] = rotation * (array[5] * d);
			array[7] = rotation2 * array[6];
			array[8] = rotation2 * array[7];
			float d2 = radius / array[8].magnitude;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] *= d2;
			}
			return array;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000485A4 File Offset: 0x000467A4
		private static Vector3[] GenerateHexPoints(float radius)
		{
			Vector3[] array = new Vector3[6];
			Quaternion rotation = Quaternion.AngleAxis(60f, Vector3.up);
			ref Vector3 ptr = ref array[0];
			ptr = Vector3.forward * radius;
			for (int i = 1; i < array.Length; i++)
			{
				Vector3[] array2 = array;
				int num = i;
				array2[num] = rotation * ptr;
				ptr = ref array2[num];
			}
			return array;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004860C File Offset: 0x0004680C
		private static Vector3[] GeneratePointsFromPattern(GameObject patternObject)
		{
			Transform transform = patternObject.transform;
			Vector3 position = transform.position;
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child.gameObject.activeInHierarchy)
				{
					list.Add(child.position - position);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00048670 File Offset: 0x00046870
		private static Vector3[] GenerateVelocitiesFromPoints(Vector3[] points, float apex)
		{
			Vector3[] array = new Vector3[points.Length];
			float num = Trajectory.CalculateInitialYSpeedForHeight(apex);
			for (int i = 0; i < points.Length; i++)
			{
				Vector3 normalized = points[i].normalized;
				float d = Trajectory.CalculateGroundSpeedToClearDistance(num, points[i].magnitude);
				Vector3 vector = normalized * d;
				vector.y = num;
				array[i] = vector;
			}
			return array;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000486D4 File Offset: 0x000468D4
		private static void ResolveVelocities()
		{
			if (FireMine.velocitiesResolved)
			{
				return;
			}
			FireMine.velocities = FireMine.GenerateVelocitiesFromPoints(FireMine.GeneratePoints(FireMine.patternRadius), FireMine.launchApex);
			if (!Application.isEditor)
			{
				FireMine.velocitiesResolved = true;
			}
		}

		// Token: 0x040014D8 RID: 5336
		public static float duration;

		// Token: 0x040014D9 RID: 5337
		public static float launchApex;

		// Token: 0x040014DA RID: 5338
		public static float patternRadius;

		// Token: 0x040014DB RID: 5339
		public static GameObject projectilePrefab;

		// Token: 0x040014DC RID: 5340
		private int fireIndex;

		// Token: 0x040014DD RID: 5341
		private static Vector3[] velocities;

		// Token: 0x040014DE RID: 5342
		private static bool velocitiesResolved;
	}
}
