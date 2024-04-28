using System;
using RoR2;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200042F RID: 1071
	public class FireCaptainShotgun : GenericBulletBaseState
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x000556F8 File Offset: 0x000538F8
		public override void OnEnter()
		{
			this.fireSoundString = ((base.characterBody.spreadBloomAngle <= FireCaptainShotgun.tightSoundSwitchThreshold) ? FireCaptainShotgun.tightSoundString : FireCaptainShotgun.wideSoundString);
			base.OnEnter();
			this.PlayAnimation("Gesture, Additive", "FireCaptainShotgun");
			this.PlayAnimation("Gesture, Override", "FireCaptainShotgun");
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0005574F File Offset: 0x0005394F
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.falloffModel = BulletAttack.FalloffModel.DefaultBullet;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0005575F File Offset: 0x0005395F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400189F RID: 6303
		public static float tightSoundSwitchThreshold;

		// Token: 0x040018A0 RID: 6304
		public static string wideSoundString;

		// Token: 0x040018A1 RID: 6305
		public static string tightSoundString;
	}
}
