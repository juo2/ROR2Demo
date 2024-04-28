using System;
using RoR2;

namespace EntityStates.ParentEgg
{
	// Token: 0x02000224 RID: 548
	public class Death : GenericCharacterDeath
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00027B18 File Offset: 0x00025D18
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetComponent<SpawnerPodsController>().Dissolve();
		}

		// Token: 0x04000B30 RID: 2864
		private float duration;
	}
}
