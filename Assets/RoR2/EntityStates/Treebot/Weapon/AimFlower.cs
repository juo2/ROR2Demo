using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x0200017C RID: 380
	public class AimFlower : AimThrowableBase
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001CB69 File Offset: 0x0001AD69
		public override void Update()
		{
			base.Update();
			this.keyDown &= !base.inputBank.skill1.down;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001CB91 File Offset: 0x0001AD91
		protected override bool KeyIsDown()
		{
			return this.keyDown;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001CB9C File Offset: 0x0001AD9C
		protected override void FireProjectile()
		{
			if (base.healthComponent)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = base.healthComponent.combinedHealth * AimFlower.healthCostFraction;
				damageInfo.position = base.characterBody.corePosition;
				damageInfo.force = Vector3.zero;
				damageInfo.damageColorIndex = DamageColorIndex.Default;
				damageInfo.crit = false;
				damageInfo.attacker = null;
				damageInfo.inflictor = null;
				damageInfo.damageType = DamageType.NonLethal;
				damageInfo.procCoefficient = 0f;
				damageInfo.procChainMask = default(ProcChainMask);
				base.healthComponent.TakeDamage(damageInfo);
			}
			base.FireProjectile();
		}

		// Token: 0x04000830 RID: 2096
		public static float healthCostFraction;

		// Token: 0x04000831 RID: 2097
		private bool keyDown = true;
	}
}
