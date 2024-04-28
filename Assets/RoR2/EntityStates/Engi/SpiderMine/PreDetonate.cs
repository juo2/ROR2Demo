using System;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x02000393 RID: 915
	public class PreDetonate : BaseSpiderMineState
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x00048100 File Offset: 0x00046300
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = PreDetonate.baseDuration;
			base.rigidbody.isKinematic = true;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004811F File Offset: 0x0004631F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new Detonate());
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014D2 RID: 5330
		public static float baseDuration;

		// Token: 0x040014D3 RID: 5331
		private float duration;
	}
}
