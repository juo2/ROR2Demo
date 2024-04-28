using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002C8 RID: 712
	public class LoaderMeleeAttack : BasicMeleeAttack
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x000353B4 File Offset: 0x000335B4
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			if (base.HasBuff(JunkContent.Buffs.LoaderOvercharged))
			{
				overlapAttack.damage *= 2f;
				overlapAttack.hitEffectPrefab = LoaderMeleeAttack.overchargeImpactEffectPrefab;
				overlapAttack.damageType |= DamageType.Stun1s;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00035401 File Offset: 0x00033601
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			base.healthComponent.AddBarrierAuthority(LoaderMeleeAttack.barrierPercentagePerHit * base.healthComponent.fullBarrier);
		}

		// Token: 0x04000F70 RID: 3952
		public static GameObject overchargeImpactEffectPrefab;

		// Token: 0x04000F71 RID: 3953
		public static float barrierPercentagePerHit;
	}
}
