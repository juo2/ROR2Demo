using System;

namespace EntityStates.LunarExploderMonster.Weapon
{
	// Token: 0x020002BC RID: 700
	public class FireExploderShards : GenericProjectileBaseState
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x000344D9 File Offset: 0x000326D9
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Gesture, Additive", "FireExploderShards");
			base.characterBody.SetAimTimer(0f);
		}
	}
}
