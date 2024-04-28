using System;
using RoR2;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000483 RID: 1155
	public class FireSidearmSkullRevolver : BaseFireSidearmRevolverState
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x0005BA8C File Offset: 0x00059C8C
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			int num = 0;
			if (base.characterBody)
			{
				num = base.characterBody.GetBuffCount(RoR2Content.Buffs.BanditSkull);
			}
			bulletAttack.damage *= 1f + 0.1f * (float)num;
			bulletAttack.damageType |= DamageType.GiveSkullOnKill;
		}
	}
}
