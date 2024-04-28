using System;
using RoR2;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047A RID: 1146
	public class EnterReload : BaseState
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0005B4E7 File Offset: 0x000596E7
		private float duration
		{
			get
			{
				return EnterReload.baseDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0005B4F5 File Offset: 0x000596F5
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Gesture, Additive", "EnterReload", "Reload.playbackRate", this.duration, 0.1f);
			Util.PlaySound(EnterReload.enterSoundString, base.gameObject);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0005B52E File Offset: 0x0005972E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new Reload());
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001A44 RID: 6724
		public static string enterSoundString;

		// Token: 0x04001A45 RID: 6725
		public static float baseDuration;
	}
}
