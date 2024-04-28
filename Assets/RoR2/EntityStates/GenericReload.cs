using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C6 RID: 198
	public class GenericReload : BaseState
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000EE34 File Offset: 0x0000D034
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.skillLocator && base.skillLocator.primary)
			{
				this.duration = base.skillLocator.primary.CalculateFinalRechargeInterval();
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x040003A9 RID: 937
		[SerializeField]
		public string enterSoundString;

		// Token: 0x040003AA RID: 938
		protected float duration;
	}
}
