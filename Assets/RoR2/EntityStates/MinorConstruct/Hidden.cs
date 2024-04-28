using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MinorConstruct
{
	// Token: 0x0200026B RID: 619
	public class Hidden : BaseHideState
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002C3E4 File Offset: 0x0002A5E4
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.buffDef)
			{
				base.characterBody.AddBuff(this.buffDef);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002C40A File Offset: 0x0002A60A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!base.characterBody.outOfCombat || !base.characterBody.outOfDanger)
			{
				this.outer.SetNextState(new Revealed());
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002C43C File Offset: 0x0002A63C
		public override void OnExit()
		{
			if (this.buffDef)
			{
				base.characterBody.RemoveBuff(this.buffDef);
			}
			base.OnExit();
		}

		// Token: 0x04000C5A RID: 3162
		[SerializeField]
		public BuffDef buffDef;
	}
}
