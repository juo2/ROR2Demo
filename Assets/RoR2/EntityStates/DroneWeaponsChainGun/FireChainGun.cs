using System;
using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace EntityStates.DroneWeaponsChainGun
{
	// Token: 0x020003C0 RID: 960
	public class FireChainGun : BaseDroneWeaponChainGunState
	{
		// Token: 0x06001124 RID: 4388 RVA: 0x0004B3C4 File Offset: 0x000495C4
		public FireChainGun()
		{
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0004B75C File Offset: 0x0004995C
		public FireChainGun(HurtBox targetHurtBox)
		{
			this.targetHurtBox = targetHurtBox;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0004B76C File Offset: 0x0004996C
		public override void OnEnter()
		{
			base.OnEnter();
			Transform transform = base.FindChild(this.muzzleName);
			if (!transform)
			{
				transform = this.body.coreTransform;
			}
			if (base.isAuthority)
			{
				this.duration = this.baseDuration / this.body.attackSpeed;
				ChainGunOrb chainGunOrb = new ChainGunOrb(this.orbEffectObject);
				chainGunOrb.damageValue = this.body.damage * this.damageCoefficient;
				chainGunOrb.isCrit = Util.CheckRoll(this.body.crit, this.body.master);
				chainGunOrb.teamIndex = TeamComponent.GetObjectTeam(this.body.gameObject);
				chainGunOrb.attacker = this.body.gameObject;
				chainGunOrb.procCoefficient = this.procCoefficient;
				chainGunOrb.procChainMask = default(ProcChainMask);
				chainGunOrb.origin = transform.position;
				chainGunOrb.target = this.targetHurtBox;
				chainGunOrb.speed = this.orbSpeed;
				chainGunOrb.bouncesRemaining = this.additionalBounces;
				chainGunOrb.bounceRange = this.bounceRange;
				chainGunOrb.damageCoefficientPerBounce = this.damageCoefficientPerBounce;
				chainGunOrb.targetsToFindPerBounce = this.targetsToFindPerBounce;
				chainGunOrb.canBounceOnSameTarget = this.canBounceOnSameTarget;
				chainGunOrb.damageColorIndex = DamageColorIndex.Item;
				OrbManager.instance.AddOrb(chainGunOrb);
			}
			if (transform)
			{
				EffectData effectData = new EffectData
				{
					origin = transform.position
				};
				EffectManager.SpawnEffect(this.muzzleFlashPrefab, effectData, true);
			}
			Util.PlaySound(this.fireSoundString, base.gameObject);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004B8F4 File Offset: 0x00049AF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				BaseDroneWeaponChainGunState baseDroneWeaponChainGunState;
				if (this.stepIndex < this.shotCount)
				{
					baseDroneWeaponChainGunState = new FireChainGun(this.targetHurtBox);
					(baseDroneWeaponChainGunState as FireChainGun).stepIndex = this.stepIndex + 1;
				}
				else
				{
					baseDroneWeaponChainGunState = new AimChainGun();
				}
				baseDroneWeaponChainGunState.PassDisplayLinks(this.gunChildLocators, this.gunAnimators);
				this.outer.SetNextState(baseDroneWeaponChainGunState);
			}
		}

		// Token: 0x040015A9 RID: 5545
		[SerializeField]
		public float baseDuration;

		// Token: 0x040015AA RID: 5546
		[SerializeField]
		public GameObject orbEffectObject;

		// Token: 0x040015AB RID: 5547
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x040015AC RID: 5548
		[SerializeField]
		public float orbSpeed;

		// Token: 0x040015AD RID: 5549
		[SerializeField]
		public int shotCount;

		// Token: 0x040015AE RID: 5550
		[SerializeField]
		public float procCoefficient;

		// Token: 0x040015AF RID: 5551
		[SerializeField]
		public int additionalBounces;

		// Token: 0x040015B0 RID: 5552
		[SerializeField]
		public float bounceRange;

		// Token: 0x040015B1 RID: 5553
		[SerializeField]
		public float damageCoefficientPerBounce;

		// Token: 0x040015B2 RID: 5554
		[SerializeField]
		public int targetsToFindPerBounce;

		// Token: 0x040015B3 RID: 5555
		[SerializeField]
		public bool canBounceOnSameTarget;

		// Token: 0x040015B4 RID: 5556
		[SerializeField]
		public string muzzleName;

		// Token: 0x040015B5 RID: 5557
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x040015B6 RID: 5558
		[SerializeField]
		public string fireSoundString;

		// Token: 0x040015B7 RID: 5559
		private HurtBox targetHurtBox;

		// Token: 0x040015B8 RID: 5560
		private float duration;

		// Token: 0x040015B9 RID: 5561
		private int stepIndex;
	}
}
