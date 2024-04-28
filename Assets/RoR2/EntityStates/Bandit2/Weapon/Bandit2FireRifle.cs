using System;
using RoR2;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000477 RID: 1143
	public class Bandit2FireRifle : Bandit2FirePrimaryBase
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0005B135 File Offset: 0x00059335
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
		}
	}
}
