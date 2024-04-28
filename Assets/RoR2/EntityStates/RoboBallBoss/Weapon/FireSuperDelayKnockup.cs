using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001EA RID: 490
	public class FireSuperDelayKnockup : FireDelayKnockup
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x00024FD7 File Offset: 0x000231D7
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.EngiShield, FireSuperDelayKnockup.shieldDuration);
			}
		}

		// Token: 0x04000A4F RID: 2639
		public static float shieldDuration;
	}
}
