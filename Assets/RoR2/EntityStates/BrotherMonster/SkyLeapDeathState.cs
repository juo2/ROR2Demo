using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000432 RID: 1074
	public class SkyLeapDeathState : GenericCharacterDeath
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00055CCA File Offset: 0x00053ECA
		public override void OnEnter()
		{
			this.duration = SkyLeapDeathState.baseDuration / this.attackSpeedStat;
			base.OnEnter();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00055CE4 File Offset: 0x00053EE4
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			base.PlayAnimation("Body", "EnterSkyLeap", "SkyLeap.playbackRate", this.duration);
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			base.characterDirection.moveVector = base.characterDirection.forward;
			AimAnimator aimAnimator = base.GetAimAnimator();
			if (aimAnimator)
			{
				aimAnimator.enabled = true;
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00055D48 File Offset: 0x00053F48
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x040018B8 RID: 6328
		public static float baseDuration;

		// Token: 0x040018B9 RID: 6329
		private float duration;
	}
}
