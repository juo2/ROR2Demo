using System;
using EntityStates.Treebot.Weapon;
using RoR2;

namespace EntityStates.ClayBruiser.Weapon
{
	// Token: 0x02000403 RID: 1027
	public class FireSonicBoom : FireSonicBoom
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x000527C5 File Offset: 0x000509C5
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetModelAnimator().SetBool("WeaponIsReady", true);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000527DE File Offset: 0x000509DE
		protected override void AddDebuff(CharacterBody body)
		{
			body.AddTimedBuff(RoR2Content.Buffs.ClayGoo, this.slowDuration);
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000527F1 File Offset: 0x000509F1
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				base.GetModelAnimator().SetBool("WeaponIsReady", false);
			}
			base.OnExit();
		}
	}
}
