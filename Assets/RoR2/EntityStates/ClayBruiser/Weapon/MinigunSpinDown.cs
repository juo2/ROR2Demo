using System;
using RoR2;

namespace EntityStates.ClayBruiser.Weapon
{
	// Token: 0x02000401 RID: 1025
	public class MinigunSpinDown : MinigunState
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x00052400 File Offset: 0x00050600
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = MinigunSpinDown.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(MinigunSpinDown.sound, base.gameObject, this.attackSpeedStat);
			base.GetModelAnimator().SetBool("WeaponIsReady", false);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0005244D File Offset: 0x0005064D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040017B3 RID: 6067
		public static float baseDuration;

		// Token: 0x040017B4 RID: 6068
		public static string sound;

		// Token: 0x040017B5 RID: 6069
		private float duration;
	}
}
