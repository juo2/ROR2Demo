using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000476 RID: 1142
	public abstract class Bandit2FirePrimaryBase : GenericBulletBaseState
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0005B0EC File Offset: 0x000592EC
		public override void OnEnter()
		{
			this.minimumDuration = this.minimumBaseDuration / this.attackSpeedStat;
			base.OnEnter();
			base.PlayAnimation("Gesture, Additive", "FireMainWeapon", "FireMainWeapon.playbackRate", this.duration);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005574F File Offset: 0x0005394F
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.falloffModel = BulletAttack.FalloffModel.DefaultBullet;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005B122 File Offset: 0x00059322
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge <= this.minimumDuration)
			{
				return InterruptPriority.Skill;
			}
			return InterruptPriority.Any;
		}

		// Token: 0x04001A33 RID: 6707
		[SerializeField]
		public float minimumBaseDuration;

		// Token: 0x04001A34 RID: 6708
		protected float minimumDuration;
	}
}
