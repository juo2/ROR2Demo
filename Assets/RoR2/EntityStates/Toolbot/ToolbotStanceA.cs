using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x020001AB RID: 427
	public class ToolbotStanceA : ToolbotStanceBase
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x00020FAA File Offset: 0x0001F1AA
		public override void OnEnter()
		{
			base.OnEnter();
			this.swapStateType = typeof(ToolbotStanceB);
			if (NetworkServer.active)
			{
				base.SetEquipmentSlot(0);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		protected override GenericSkill GetCurrentPrimarySkill()
		{
			return base.GetPrimarySkill1();
		}
	}
}
