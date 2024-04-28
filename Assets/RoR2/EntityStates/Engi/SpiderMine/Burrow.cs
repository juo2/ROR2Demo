using System;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x0200038E RID: 910
	public class Burrow : BaseSpiderMineState
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x00047C02 File Offset: 0x00045E02
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Burrow.baseDuration;
			base.PlayAnimation("Base", "IdleToArmed", "IdleToArmed.playbackRate", this.duration);
			base.EmitDustEffect();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00047C38 File Offset: 0x00045E38
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				EntityState entityState = null;
				if (!base.projectileStickOnImpact.stuck)
				{
					entityState = new WaitForStick();
				}
				else if (this.duration <= base.fixedAge)
				{
					entityState = new WaitForTarget();
				}
				if (entityState != null)
				{
					this.outer.SetNextState(entityState);
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040014C6 RID: 5318
		public static float baseDuration;

		// Token: 0x040014C7 RID: 5319
		private float duration;
	}
}
