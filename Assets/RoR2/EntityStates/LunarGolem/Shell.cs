using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarGolem
{
	// Token: 0x020002B8 RID: 696
	public class Shell : BaseState
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x000340A4 File Offset: 0x000322A4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Shell.baseDuration / this.attackSpeedStat;
			Util.PlaySound(Shell.preShieldSoundString, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "PreShield", 0.2f);
			EffectManager.SimpleMuzzleFlash(Shell.preShieldEffect, base.gameObject, "Center", false);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00034108 File Offset: 0x00032308
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Shell.preShieldAnimDuration && !this.readyToActivate)
			{
				this.readyToActivate = true;
				Util.PlaySound(Shell.shieldActivateSoundString, base.gameObject);
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.LunarShell, Shell.buffDuration);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x04000F0E RID: 3854
		public static float baseDuration;

		// Token: 0x04000F0F RID: 3855
		public static float buffDuration;

		// Token: 0x04000F10 RID: 3856
		public static float preShieldAnimDuration;

		// Token: 0x04000F11 RID: 3857
		public static GameObject preShieldEffect;

		// Token: 0x04000F12 RID: 3858
		public static string preShieldSoundString;

		// Token: 0x04000F13 RID: 3859
		public static string shieldActivateSoundString;

		// Token: 0x04000F14 RID: 3860
		private bool readyToActivate;

		// Token: 0x04000F15 RID: 3861
		private float duration;
	}
}
