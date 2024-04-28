using System;

namespace EntityStates.HermitCrab
{
	// Token: 0x02000328 RID: 808
	public class Burrowed : BaseState
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x0003EA15 File Offset: 0x0003CC15
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "Burrowed", 0.1f);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003EA34 File Offset: 0x0003CC34
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (base.inputBank.moveVector.sqrMagnitude > 0.1f)
				{
					this.outer.SetNextState(new BurrowOut());
				}
				if (base.fixedAge >= this.duration && base.inputBank.skill1.down)
				{
					this.outer.SetNextState(new FireMortar());
				}
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001235 RID: 4661
		public static float mortarCooldown;

		// Token: 0x04001236 RID: 4662
		public float duration;
	}
}
