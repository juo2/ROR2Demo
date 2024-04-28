using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x020001AC RID: 428
	public class ToolbotStanceB : ToolbotStanceBase
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		public override void OnEnter()
		{
			base.OnEnter();
			this.swapStateType = typeof(ToolbotStanceA);
			if (NetworkServer.active)
			{
				base.SetEquipmentSlot(1);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00021006 File Offset: 0x0001F206
		protected override GenericSkill GetCurrentPrimarySkill()
		{
			return base.GetPrimarySkill2();
		}
	}
}
