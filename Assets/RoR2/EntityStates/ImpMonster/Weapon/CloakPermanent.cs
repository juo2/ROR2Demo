using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.ImpMonster.Weapon
{
	// Token: 0x02000313 RID: 787
	public class CloakPermanent : BaseState
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003C10A File Offset: 0x0003A30A
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Cloak);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003C136 File Offset: 0x0003A336
		public override void OnExit()
		{
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.Cloak);
			}
			base.OnExit();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}
	}
}
