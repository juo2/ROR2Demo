using System;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x02000199 RID: 409
	public class NailgunSpinDown : BaseNailgunState
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x0001F366 File Offset: 0x0001D566
		protected override float GetBaseDuration()
		{
			return NailgunSpinDown.baseDuration;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001F36D File Offset: 0x0001D56D
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlayAttackSpeedSound(NailgunSpinDown.spinDownSound, base.gameObject, this.attackSpeedStat);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001F38C File Offset: 0x0001D58C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new NailgunFinalBurst
				{
					activatorSkillSlot = base.activatorSkillSlot
				});
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040008DE RID: 2270
		public static string spinDownSound;

		// Token: 0x040008DF RID: 2271
		public static float baseDuration;
	}
}
