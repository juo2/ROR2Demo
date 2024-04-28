using System;
using RoR2;

namespace EntityStates.Treebot.TreebotFlower
{
	// Token: 0x0200018A RID: 394
	public class SpawnState : BaseState
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0001DD93 File Offset: 0x0001BF93
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.enterSoundString, base.gameObject);
			base.PlayAnimation("Base", "Spawn", "Spawn.playbackRate", SpawnState.duration);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001DDC6 File Offset: 0x0001BFC6
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration)
			{
				this.outer.SetNextState(new TreebotFlower2Projectile());
			}
		}

		// Token: 0x04000881 RID: 2177
		public static float duration;

		// Token: 0x04000882 RID: 2178
		public static string enterSoundString;
	}
}
