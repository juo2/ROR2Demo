using System;
using RoR2;

namespace EntityStates.ParentEgg
{
	// Token: 0x02000223 RID: 547
	public class BaseEggState : BaseState
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00027AF3 File Offset: 0x00025CF3
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00027AFB File Offset: 0x00025CFB
		private protected SpawnerPodsController controller { protected get; private set; }

		// Token: 0x060009A2 RID: 2466 RVA: 0x00027B04 File Offset: 0x00025D04
		public override void OnEnter()
		{
			base.OnEnter();
			this.controller = base.GetComponent<SpawnerPodsController>();
		}
	}
}
