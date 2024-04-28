using System;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003EF RID: 1007
	public class FireShotgunBlast : GenericBulletBaseState
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0005075C File Offset: 0x0004E95C
		public override void OnEnter()
		{
			this.muzzleName = "MuzzleLeft";
			base.OnEnter();
			this.PlayAnimation("Gesture Additive, Left", "FirePistol, Left");
			this.PlayAnimation("Gesture Override, Left", "FirePistol, Left");
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000507B8 File Offset: 0x0004E9B8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.hasFiredSecondBlast && FireShotgunBlast.delayBetweenShotgunBlasts / this.attackSpeedStat < base.fixedAge)
			{
				this.hasFiredSecondBlast = true;
				this.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
				this.PlayAnimation("Gesture Override, Right", "FirePistol, Right");
				this.muzzleName = "MuzzleRight";
				this.FireBullet(base.GetAimRay());
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001722 RID: 5922
		public static float delayBetweenShotgunBlasts;

		// Token: 0x04001723 RID: 5923
		private bool hasFiredSecondBlast;
	}
}
