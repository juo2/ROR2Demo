using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBoss.ClayBossWeapon
{
	// Token: 0x0200040C RID: 1036
	public class ChargeBombardment : BaseState
	{
		// Token: 0x060012A0 RID: 4768 RVA: 0x0005348C File Offset: 0x0005168C
		public override void OnEnter()
		{
			base.OnEnter();
			this.totalDuration = ChargeBombardment.baseTotalDuration / this.attackSpeedStat;
			this.maxChargeTime = ChargeBombardment.baseMaxChargeTime / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			this.PlayAnimation("Gesture, Additive", "ChargeBombardment");
			Util.PlaySound(ChargeBombardment.chargeLoopStartSoundString, base.gameObject);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("Muzzle");
					if (transform && ChargeBombardment.chargeEffectPrefab)
					{
						this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeBombardment.chargeEffectPrefab, transform.position, transform.rotation);
						this.chargeInstance.transform.parent = transform;
						ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component2)
						{
							component2.newDuration = this.totalDuration;
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.totalDuration);
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0005358D File Offset: 0x0005178D
		public override void OnExit()
		{
			base.OnExit();
			this.PlayAnimation("Gesture, Additive", "Empty");
			Util.PlaySound(ChargeBombardment.chargeLoopStopSoundString, base.gameObject);
			EntityState.Destroy(this.chargeInstance);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000535C4 File Offset: 0x000517C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.charge = Mathf.Min((int)(this.stopwatch / this.maxChargeTime * (float)ChargeBombardment.maxCharges), ChargeBombardment.maxCharges);
			float t = (float)this.charge / (float)ChargeBombardment.maxCharges;
			float value = Mathf.Lerp(ChargeBombardment.minBonusBloom, ChargeBombardment.maxBonusBloom, t);
			base.characterBody.SetSpreadBloom(value, true);
			int grenadeCountMax = Mathf.FloorToInt(Mathf.Lerp((float)ChargeBombardment.minGrenadeCount, (float)ChargeBombardment.maxGrenadeCount, t));
			if ((this.stopwatch >= this.totalDuration || !base.inputBank || !base.inputBank.skill1.down) && base.isAuthority)
			{
				FireBombardment fireBombardment = new FireBombardment();
				fireBombardment.grenadeCountMax = grenadeCountMax;
				this.outer.SetNextState(fireBombardment);
				return;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001808 RID: 6152
		public static float baseTotalDuration;

		// Token: 0x04001809 RID: 6153
		public static float baseMaxChargeTime;

		// Token: 0x0400180A RID: 6154
		public static int maxCharges;

		// Token: 0x0400180B RID: 6155
		public static GameObject chargeEffectPrefab;

		// Token: 0x0400180C RID: 6156
		public static string chargeLoopStartSoundString;

		// Token: 0x0400180D RID: 6157
		public static string chargeLoopStopSoundString;

		// Token: 0x0400180E RID: 6158
		public static int minGrenadeCount;

		// Token: 0x0400180F RID: 6159
		public static int maxGrenadeCount;

		// Token: 0x04001810 RID: 6160
		public static float minBonusBloom;

		// Token: 0x04001811 RID: 6161
		public static float maxBonusBloom;

		// Token: 0x04001812 RID: 6162
		private float stopwatch;

		// Token: 0x04001813 RID: 6163
		private GameObject chargeInstance;

		// Token: 0x04001814 RID: 6164
		private int charge;

		// Token: 0x04001815 RID: 6165
		private float totalDuration;

		// Token: 0x04001816 RID: 6166
		private float maxChargeTime;
	}
}
