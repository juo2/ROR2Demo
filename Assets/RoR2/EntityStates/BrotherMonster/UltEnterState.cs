using System;
using RoR2;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044C RID: 1100
	public class UltEnterState : BaseState
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00057653 File Offset: 0x00055853
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "UltEnter", "Ult.playbackRate", UltEnterState.duration, 0.1f);
			Util.PlaySound(UltEnterState.soundString, base.gameObject);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005768B File Offset: 0x0005588B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > UltEnterState.duration)
			{
				this.outer.SetNextState(new UltChannelState());
			}
		}

		// Token: 0x0400190D RID: 6413
		public static string soundString;

		// Token: 0x0400190E RID: 6414
		public static float duration;
	}
}
