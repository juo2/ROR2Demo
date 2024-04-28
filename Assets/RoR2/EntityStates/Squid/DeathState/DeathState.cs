using System;

namespace EntityStates.Squid.DeathState
{
	// Token: 0x020001C3 RID: 451
	public class DeathState : BaseState
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0002235D File Offset: 0x0002055D
		public override void OnEnter()
		{
			this.duration = DeathState.baseDuration;
			base.OnEnter();
			this.PlayAnimation("Body", "Death");
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00022380 File Offset: 0x00020580
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000223A9 File Offset: 0x000205A9
		public override void OnExit()
		{
			if (!this.outer.destroying && base.gameObject)
			{
				EntityState.Destroy(base.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000984 RID: 2436
		public static float baseDuration;

		// Token: 0x04000985 RID: 2437
		private float duration;
	}
}
