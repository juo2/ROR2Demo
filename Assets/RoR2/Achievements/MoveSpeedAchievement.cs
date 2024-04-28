using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EB8 RID: 3768
	[RegisterAchievement("MoveSpeed", "Items.JumpBoost", null, null)]
	public class MoveSpeedAchievement : BaseAchievement
	{
		// Token: 0x060055E0 RID: 21984 RVA: 0x0015ED9F File Offset: 0x0015CF9F
		public override void OnInstall()
		{
			base.OnInstall();
			RoR2Application.onUpdate += this.CheckMoveSpeed;
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0015EDB8 File Offset: 0x0015CFB8
		public override void OnUninstall()
		{
			RoR2Application.onUpdate -= this.CheckMoveSpeed;
			base.OnUninstall();
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0015EDD4 File Offset: 0x0015CFD4
		public void CheckMoveSpeed()
		{
			if (base.localUser != null && base.localUser.cachedBody && base.localUser.cachedBody.moveSpeed / base.localUser.cachedBody.baseMoveSpeed >= 4f)
			{
				base.Grant();
			}
		}

		// Token: 0x0400506A RID: 20586
		private const float requirement = 4f;
	}
}
