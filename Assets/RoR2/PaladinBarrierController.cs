using System;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007F9 RID: 2041
	public class PaladinBarrierController : MonoBehaviour, IBarrier
	{
		// Token: 0x06002C02 RID: 11266 RVA: 0x000BC4F8 File Offset: 0x000BA6F8
		public void BlockedDamage(DamageInfo damageInfo, float actualDamageBlocked)
		{
			this.totalDamageBlocked += actualDamageBlocked;
			LightningOrb lightningOrb = new LightningOrb();
			lightningOrb.teamIndex = this.teamComponent.teamIndex;
			lightningOrb.origin = damageInfo.position;
			lightningOrb.damageValue = actualDamageBlocked * this.blockLaserDamageCoefficient;
			lightningOrb.bouncesRemaining = 0;
			lightningOrb.attacker = damageInfo.attacker;
			lightningOrb.procCoefficient = this.blockLaserProcCoefficient;
			lightningOrb.lightningType = LightningOrb.LightningType.TreePoisonDart;
			HurtBox hurtBox = lightningOrb.PickNextTarget(lightningOrb.origin);
			if (hurtBox)
			{
				lightningOrb.target = hurtBox;
				lightningOrb.isCrit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master);
				OrbManager.instance.AddOrb(lightningOrb);
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000BC5B2 File Offset: 0x000BA7B2
		public void EnableBarrier()
		{
			this.barrierPivotTransform.gameObject.SetActive(true);
			this.barrierIsOn = true;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000BC5CC File Offset: 0x000BA7CC
		public void DisableBarrier()
		{
			this.barrierPivotTransform.gameObject.SetActive(false);
			this.barrierIsOn = false;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000BC5E6 File Offset: 0x000BA7E6
		private void Start()
		{
			this.inputBank = base.GetComponent<InputBankTest>();
			this.characterBody = base.GetComponent<CharacterBody>();
			this.teamComponent = base.GetComponent<TeamComponent>();
			this.DisableBarrier();
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000BC612 File Offset: 0x000BA812
		private void Update()
		{
			if (this.barrierIsOn)
			{
				this.barrierPivotTransform.rotation = Util.QuaternionSafeLookRotation(this.inputBank.aimDirection);
			}
		}

		// Token: 0x04002E77 RID: 11895
		public float blockLaserDamageCoefficient;

		// Token: 0x04002E78 RID: 11896
		public float blockLaserProcCoefficient;

		// Token: 0x04002E79 RID: 11897
		public float blockLaserDistance;

		// Token: 0x04002E7A RID: 11898
		private float totalDamageBlocked;

		// Token: 0x04002E7B RID: 11899
		private CharacterBody characterBody;

		// Token: 0x04002E7C RID: 11900
		private InputBankTest inputBank;

		// Token: 0x04002E7D RID: 11901
		private TeamComponent teamComponent;

		// Token: 0x04002E7E RID: 11902
		private bool barrierIsOn;

		// Token: 0x04002E7F RID: 11903
		public Transform barrierPivotTransform;
	}
}
