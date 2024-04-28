using System;
using RoR2;

namespace EntityStates.UrchinTurret.Weapon
{
	// Token: 0x02000170 RID: 368
	public class MinigunSpinDown : MinigunState
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0001B7DE File Offset: 0x000199DE
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = MinigunSpinDown.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(MinigunSpinDown.sound, base.gameObject, this.attackSpeedStat);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001B80F File Offset: 0x00019A0F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040007C6 RID: 1990
		public static float baseDuration;

		// Token: 0x040007C7 RID: 1991
		public static string sound;

		// Token: 0x040007C8 RID: 1992
		private float duration;
	}
}
