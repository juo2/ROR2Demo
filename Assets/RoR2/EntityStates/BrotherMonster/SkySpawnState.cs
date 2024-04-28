using System;
using RoR2;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000440 RID: 1088
	public class SkySpawnState : BaseState
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x00056CDC File Offset: 0x00054EDC
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "ExitSkyLeap", "SkyLeap.playbackRate", SkySpawnState.duration);
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			Util.PlaySound(SkySpawnState.soundString, base.gameObject);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00056D2A File Offset: 0x00054F2A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > SkySpawnState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040018F1 RID: 6385
		public static float duration;

		// Token: 0x040018F2 RID: 6386
		public static string soundString;
	}
}
