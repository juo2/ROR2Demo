using System;
using RoR2;

namespace EntityStates.Barrel
{
	// Token: 0x02000472 RID: 1138
	public class Opening : EntityState
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x0005AC94 File Offset: 0x00058E94
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Opening", "Opening.playbackRate", Opening.duration);
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005ACE5 File Offset: 0x00058EE5
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Opening.duration)
			{
				this.outer.SetNextState(new Opened());
				return;
			}
		}

		// Token: 0x04001A25 RID: 6693
		public static float duration = 1f;
	}
}
