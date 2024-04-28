using System;
using RoR2;

namespace EntityStates
{
	// Token: 0x020000D7 RID: 215
	public class PrepFlower2 : BaseState
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0001011C File Offset: 0x0000E31C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PrepFlower2.baseDuration / this.attackSpeedStat;
			Util.PlaySound(PrepFlower2.enterSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "PrepFlower", "PrepFlower.playbackRate", this.duration);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001016D File Offset: 0x0000E36D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FireFlower2());
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040003E3 RID: 995
		public static float baseDuration;

		// Token: 0x040003E4 RID: 996
		public static string enterSoundString;

		// Token: 0x040003E5 RID: 997
		private float duration;
	}
}
