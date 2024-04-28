using System;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000943 RID: 2371
	public static class MissileUtils
	{
		// Token: 0x0600359C RID: 13724 RVA: 0x000E2648 File Offset: 0x000E0848
		public static void FireMissile(Vector3 position, CharacterBody attackerBody, ProcChainMask procChainMask, GameObject victim, float missileDamage, bool isCrit, GameObject projectilePrefab, DamageColorIndex damageColorIndex, bool addMissileProc)
		{
			MissileUtils.FireMissile(position, attackerBody, procChainMask, victim, missileDamage, isCrit, projectilePrefab, damageColorIndex, Vector3.up + UnityEngine.Random.insideUnitSphere * 0.1f, 200f, addMissileProc);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000E2688 File Offset: 0x000E0888
		public static void FireMissile(Vector3 position, CharacterBody attackerBody, ProcChainMask procChainMask, GameObject victim, float missileDamage, bool isCrit, GameObject projectilePrefab, DamageColorIndex damageColorIndex, Vector3 initialDirection, float force, bool addMissileProc)
		{
			int? num;
			if (attackerBody == null)
			{
				num = null;
			}
			else
			{
				Inventory inventory = attackerBody.inventory;
				num = ((inventory != null) ? new int?(inventory.GetItemCount(DLC1Content.Items.MoreMissile)) : null);
			}
			int num2 = num ?? 0;
			float num3 = Mathf.Max(1f, 1f + 0.5f * (float)(num2 - 1));
			InputBankTest component = attackerBody.GetComponent<InputBankTest>();
			ProcChainMask procChainMask2 = procChainMask;
			if (addMissileProc)
			{
				procChainMask2.AddProc(ProcType.Missile);
			}
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = projectilePrefab,
				position = position,
				rotation = Util.QuaternionSafeLookRotation(initialDirection),
				procChainMask = procChainMask2,
				target = victim,
				owner = attackerBody.gameObject,
				damage = missileDamage * num3,
				crit = isCrit,
				force = force,
				damageColorIndex = damageColorIndex
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			if (num2 > 0)
			{
				Vector3 axis = component ? component.aimDirection : attackerBody.transform.position;
				FireProjectileInfo fireProjectileInfo2 = fireProjectileInfo;
				fireProjectileInfo2.rotation = Util.QuaternionSafeLookRotation(Quaternion.AngleAxis(45f, axis) * initialDirection);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo2);
				FireProjectileInfo fireProjectileInfo3 = fireProjectileInfo;
				fireProjectileInfo3.rotation = Util.QuaternionSafeLookRotation(Quaternion.AngleAxis(-45f, axis) * initialDirection);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo3);
			}
		}
	}
}
