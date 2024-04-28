using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F3 RID: 1011
	public class PrepBarrage : BaseState
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x00051020 File Offset: 0x0004F220
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepBarrage.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "PrepBarrage", "PrepBarrage.playbackRate", this.duration);
			}
			Util.PlaySound(PrepBarrage.prepBarrageSoundString, base.gameObject);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000510A8 File Offset: 0x0004F2A8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireBarrage nextState = new FireBarrage();
				this.outer.SetNextState(nextState);
				return;
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001753 RID: 5971
		public static float baseDuration = 3f;

		// Token: 0x04001754 RID: 5972
		public static string prepBarrageSoundString;

		// Token: 0x04001755 RID: 5973
		private float duration;

		// Token: 0x04001756 RID: 5974
		private Animator modelAnimator;
	}
}
