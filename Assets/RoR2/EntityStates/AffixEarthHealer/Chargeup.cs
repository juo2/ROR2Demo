using System;
using RoR2;

namespace EntityStates.AffixEarthHealer
{
	// Token: 0x020004A0 RID: 1184
	public class Chargeup : BaseState
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0005E588 File Offset: 0x0005C788
		public override void OnEnter()
		{
			base.OnEnter();
			base.FindModelChild("ChargeUpFX").gameObject.SetActive(true);
			base.PlayAnimation("Base", "ChargeUp", "ChargeUp.playbackRate", Chargeup.duration);
			Util.PlaySound(Chargeup.enterSoundString, base.gameObject);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0005E5DC File Offset: 0x0005C7DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Chargeup.duration)
			{
				this.outer.SetNextState(new Heal());
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005E601 File Offset: 0x0005C801
		public override void OnExit()
		{
			base.FindModelChild("ChargeUpFX").gameObject.SetActive(false);
			base.OnExit();
		}

		// Token: 0x04001B17 RID: 6935
		public static float duration;

		// Token: 0x04001B18 RID: 6936
		public static string enterSoundString;
	}
}
