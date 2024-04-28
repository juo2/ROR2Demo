using System;
using System.Collections.Generic;
using System.Globalization;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Bell.BellWeapon
{
	// Token: 0x0200045C RID: 1116
	public class ChargeTrioBomb : BaseState
	{
		// Token: 0x060013F2 RID: 5106 RVA: 0x00058E54 File Offset: 0x00057054
		public override void OnEnter()
		{
			base.OnEnter();
			this.prepDuration = ChargeTrioBomb.basePrepDuration / this.attackSpeedStat;
			this.timeBetweenPreps = ChargeTrioBomb.baseTimeBetweenPreps / this.attackSpeedStat;
			this.barrageDuration = ChargeTrioBomb.baseBarrageDuration / this.attackSpeedStat;
			this.timeBetweenBarrages = ChargeTrioBomb.baseTimeBetweenBarrages / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00058ECA File Offset: 0x000570CA
		private string FindTargetChildStringFromBombIndex()
		{
			return string.Format(CultureInfo.InvariantCulture, "ProjectilePosition{0}", this.currentBombIndex);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00058EE8 File Offset: 0x000570E8
		private Transform FindTargetChildTransformFromBombIndex()
		{
			string childName = this.FindTargetChildStringFromBombIndex();
			return this.childLocator.FindChild(childName);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00058F08 File Offset: 0x00057108
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.perProjectileStopwatch += Time.fixedDeltaTime;
			if (base.fixedAge < this.prepDuration)
			{
				if (this.perProjectileStopwatch > this.timeBetweenPreps && this.currentBombIndex < 3)
				{
					this.currentBombIndex++;
					this.perProjectileStopwatch = 0f;
					Transform transform = this.FindTargetChildTransformFromBombIndex();
					if (transform)
					{
						GameObject item = UnityEngine.Object.Instantiate<GameObject>(ChargeTrioBomb.preppedBombPrefab, transform);
						this.preppedBombInstances.Add(item);
						return;
					}
				}
			}
			else if (base.fixedAge < this.prepDuration + this.barrageDuration)
			{
				if (this.perProjectileStopwatch > this.timeBetweenBarrages && this.currentBombIndex > 0)
				{
					this.perProjectileStopwatch = 0f;
					Ray aimRay = base.GetAimRay();
					Transform transform2 = this.FindTargetChildTransformFromBombIndex();
					if (transform2)
					{
						if (base.isAuthority)
						{
							ProjectileManager.instance.FireProjectile(ChargeTrioBomb.bombProjectilePrefab, transform2.position, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * ChargeTrioBomb.damageCoefficient, ChargeTrioBomb.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
							Rigidbody component = base.GetComponent<Rigidbody>();
							if (component)
							{
								component.AddForceAtPosition(-ChargeTrioBomb.selfForce * transform2.forward, transform2.position);
							}
						}
						EffectManager.SimpleMuzzleFlash(ChargeTrioBomb.muzzleflashPrefab, base.gameObject, this.FindTargetChildStringFromBombIndex(), false);
					}
					this.currentBombIndex--;
					EntityState.Destroy(this.preppedBombInstances[this.currentBombIndex]);
					return;
				}
			}
			else if (base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000590D4 File Offset: 0x000572D4
		public override void OnExit()
		{
			base.OnExit();
			foreach (GameObject obj in this.preppedBombInstances)
			{
				EntityState.Destroy(obj);
			}
		}

		// Token: 0x04001993 RID: 6547
		public static float basePrepDuration;

		// Token: 0x04001994 RID: 6548
		public static float baseTimeBetweenPreps;

		// Token: 0x04001995 RID: 6549
		public static GameObject preppedBombPrefab;

		// Token: 0x04001996 RID: 6550
		public static float baseBarrageDuration;

		// Token: 0x04001997 RID: 6551
		public static float baseTimeBetweenBarrages;

		// Token: 0x04001998 RID: 6552
		public static GameObject bombProjectilePrefab;

		// Token: 0x04001999 RID: 6553
		public static GameObject muzzleflashPrefab;

		// Token: 0x0400199A RID: 6554
		public static float damageCoefficient;

		// Token: 0x0400199B RID: 6555
		public static float force;

		// Token: 0x0400199C RID: 6556
		public static float selfForce;

		// Token: 0x0400199D RID: 6557
		private float prepDuration;

		// Token: 0x0400199E RID: 6558
		private float timeBetweenPreps;

		// Token: 0x0400199F RID: 6559
		private float barrageDuration;

		// Token: 0x040019A0 RID: 6560
		private float timeBetweenBarrages;

		// Token: 0x040019A1 RID: 6561
		private ChildLocator childLocator;

		// Token: 0x040019A2 RID: 6562
		private List<GameObject> preppedBombInstances = new List<GameObject>();

		// Token: 0x040019A3 RID: 6563
		private int currentBombIndex;

		// Token: 0x040019A4 RID: 6564
		private float perProjectileStopwatch;
	}
}
