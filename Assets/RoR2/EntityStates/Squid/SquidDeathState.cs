using System;

namespace EntityStates.Squid
{
	// Token: 0x020001C1 RID: 449
	public class SquidDeathState : GenericCharacterDeath
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00022060 File Offset: 0x00020260
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Death");
		}

		// Token: 0x04000977 RID: 2423
		public static float duration;
	}
}
