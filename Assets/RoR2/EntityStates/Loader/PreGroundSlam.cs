using System;
using RoR2;

namespace EntityStates.Loader
{
	// Token: 0x020002C6 RID: 710
	public class PreGroundSlam : BaseCharacterMain
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x00034FF8 File Offset: 0x000331F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PreGroundSlam.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Body", "PreGroundSlam", "GroundSlam.playbackRate", this.duration);
			Util.PlaySound(PreGroundSlam.enterSoundString, base.gameObject);
			base.characterMotor.Motor.ForceUnground();
			base.characterMotor.disableAirControlUntilCollision = false;
			base.characterMotor.velocity.y = PreGroundSlam.upwardVelocity;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003507A File Offset: 0x0003327A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.characterMotor.moveDirection = base.inputBank.moveVector;
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new GroundSlam());
			}
		}

		// Token: 0x04000F59 RID: 3929
		public static float baseDuration;

		// Token: 0x04000F5A RID: 3930
		public static string enterSoundString;

		// Token: 0x04000F5B RID: 3931
		public static float upwardVelocity;

		// Token: 0x04000F5C RID: 3932
		private float duration;
	}
}
