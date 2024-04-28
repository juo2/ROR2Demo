using System;
using RoR2;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000436 RID: 1078
	public class EnterSkyLeap : BaseState
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x0005634C File Offset: 0x0005454C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EnterSkyLeap.baseDuration / this.attackSpeedStat;
			Util.PlaySound(EnterSkyLeap.soundString, base.gameObject);
			base.PlayAnimation("Body", "EnterSkyLeap", "SkyLeap.playbackRate", this.duration);
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			base.characterDirection.moveVector = base.characterDirection.forward;
			base.characterBody.AddTimedBuff(RoR2Content.Buffs.ArmorBoost, EnterSkyLeap.baseDuration);
			AimAnimator aimAnimator = base.GetAimAnimator();
			if (aimAnimator)
			{
				aimAnimator.enabled = true;
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000563EE File Offset: 0x000545EE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.duration)
			{
				this.outer.SetNextState(new HoldSkyLeap());
			}
		}

		// Token: 0x040018D3 RID: 6355
		public static float baseDuration;

		// Token: 0x040018D4 RID: 6356
		public static string soundString;

		// Token: 0x040018D5 RID: 6357
		private float duration;
	}
}
