using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DA RID: 474
	public class BaseSitState : BaseState
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00023EBE File Offset: 0x000220BE
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.ArmorBoost);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00023EEA File Offset: 0x000220EA
		public override void OnExit()
		{
			if (NetworkServer.active && base.characterBody)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.ArmorBoost);
			}
			base.OnExit();
		}
	}
}
