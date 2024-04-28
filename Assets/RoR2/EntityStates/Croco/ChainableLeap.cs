using System;
using RoR2;

namespace EntityStates.Croco
{
	// Token: 0x020003DE RID: 990
	public class ChainableLeap : BaseLeap
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x0004E24B File Offset: 0x0004C44B
		protected override DamageType GetBlastDamageType()
		{
			return DamageType.Stun1s;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0004E250 File Offset: 0x0004C450
		protected override void DoImpactAuthority()
		{
			base.DoImpactAuthority();
			BlastAttack.Result result = base.DetonateAuthority();
			base.skillLocator.utility.RunRecharge((float)result.hitCount * ChainableLeap.refundPerHit);
		}

		// Token: 0x0400167E RID: 5758
		public static float refundPerHit;
	}
}
