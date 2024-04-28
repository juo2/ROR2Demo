using System;
using RoR2;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000481 RID: 1153
	public class FireSidearmResetRevolver : BaseFireSidearmRevolverState
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x0005BA66 File Offset: 0x00059C66
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.damageType |= DamageType.ResetCooldownsOnKill;
		}
	}
}
