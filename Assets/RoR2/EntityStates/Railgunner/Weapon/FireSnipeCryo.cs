using System;
using EntityStates.Railgunner.Backpack;
using RoR2;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F8 RID: 504
	public class FireSnipeCryo : BaseFireSnipe
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x000259ED File Offset: 0x00023BED
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.damageType |= DamageType.Freeze2s;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00025A08 File Offset: 0x00023C08
		protected override EntityState InstantiateBackpackState()
		{
			return new UseCryo();
		}
	}
}
