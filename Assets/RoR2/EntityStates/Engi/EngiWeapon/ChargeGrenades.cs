using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A3 RID: 931
	public class ChargeGrenades : BaseState
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x00048CA8 File Offset: 0x00046EA8
		public override void OnEnter()
		{
			base.OnEnter();
			this.totalDuration = ChargeGrenades.baseTotalDuration / this.attackSpeedStat;
			this.maxChargeTime = ChargeGrenades.baseMaxChargeTime / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			this.PlayAnimation("Gesture, Additive", "ChargeGrenades");
			Util.PlaySound(ChargeGrenades.chargeLoopStartSoundString, base.gameObject);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("MuzzleLeft");
					if (transform && ChargeGrenades.chargeEffectPrefab)
					{
						this.chargeLeftInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeGrenades.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeLeftInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeLeftInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.totalDuration;
						}
					}
					Transform transform2 = component.FindChild("MuzzleRight");
					if (transform2 && ChargeGrenades.chargeEffectPrefab)
					{
						this.chargeRightInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeGrenades.chargeEffectPrefab, transform2.position, transform2.rotation);
						this.chargeRightInstance.transform.parent = transform2;
						ScaleParticleSystemDuration component3 = this.chargeRightInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component3)
						{
							component3.newDuration = this.totalDuration;
						}
					}
				}
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00048E04 File Offset: 0x00047004
		public override void OnExit()
		{
			base.OnExit();
			this.PlayAnimation("Gesture, Additive", "Empty");
			Util.PlaySound(ChargeGrenades.chargeLoopStopSoundString, base.gameObject);
			EntityState.Destroy(this.chargeLeftInstance);
			EntityState.Destroy(this.chargeRightInstance);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00048E44 File Offset: 0x00047044
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.lastCharge = this.charge;
			this.charge = Mathf.Min((int)(base.fixedAge / this.maxChargeTime * (float)ChargeGrenades.maxCharges), ChargeGrenades.maxCharges);
			float t = (float)this.charge / (float)ChargeGrenades.maxCharges;
			float value = Mathf.Lerp(ChargeGrenades.minBonusBloom, ChargeGrenades.maxBonusBloom, t);
			base.characterBody.SetSpreadBloom(value, true);
			int num = Mathf.FloorToInt(Mathf.Lerp((float)ChargeGrenades.minGrenadeCount, (float)ChargeGrenades.maxGrenadeCount, t));
			if (this.lastCharge < this.charge)
			{
				Util.PlaySound(ChargeGrenades.chargeStockSoundString, base.gameObject, "engiM1_chargePercent", 100f * ((float)(num - 1) / (float)ChargeGrenades.maxGrenadeCount));
			}
			if ((base.fixedAge >= this.totalDuration || !base.inputBank || !base.inputBank.skill1.down) && base.isAuthority)
			{
				FireGrenades fireGrenades = new FireGrenades();
				fireGrenades.grenadeCountMax = num;
				this.outer.SetNextState(fireGrenades);
				return;
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040014F6 RID: 5366
		public static float baseTotalDuration;

		// Token: 0x040014F7 RID: 5367
		public static float baseMaxChargeTime;

		// Token: 0x040014F8 RID: 5368
		public static int maxCharges;

		// Token: 0x040014F9 RID: 5369
		public static GameObject chargeEffectPrefab;

		// Token: 0x040014FA RID: 5370
		public static string chargeStockSoundString;

		// Token: 0x040014FB RID: 5371
		public static string chargeLoopStartSoundString;

		// Token: 0x040014FC RID: 5372
		public static string chargeLoopStopSoundString;

		// Token: 0x040014FD RID: 5373
		public static int minGrenadeCount;

		// Token: 0x040014FE RID: 5374
		public static int maxGrenadeCount;

		// Token: 0x040014FF RID: 5375
		public static float minBonusBloom;

		// Token: 0x04001500 RID: 5376
		public static float maxBonusBloom;

		// Token: 0x04001501 RID: 5377
		private GameObject chargeLeftInstance;

		// Token: 0x04001502 RID: 5378
		private GameObject chargeRightInstance;

		// Token: 0x04001503 RID: 5379
		private int charge;

		// Token: 0x04001504 RID: 5380
		private int lastCharge;

		// Token: 0x04001505 RID: 5381
		private float totalDuration;

		// Token: 0x04001506 RID: 5382
		private float maxChargeTime;
	}
}
