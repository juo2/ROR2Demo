using System;
using RoR2;

namespace EntityStates.Croco
{
	// Token: 0x020003DD RID: 989
	public class Leap : BaseLeap
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004E20E File Offset: 0x0004C40E
		protected override DamageType GetBlastDamageType()
		{
			return (this.crocoDamageTypeController ? this.crocoDamageTypeController.GetDamageType() : DamageType.Generic) | DamageType.Stun1s;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004E22E File Offset: 0x0004C42E
		protected override void DoImpactAuthority()
		{
			base.DoImpactAuthority();
			base.DetonateAuthority();
			base.DropAcidPoolAuthority();
		}
	}
}
