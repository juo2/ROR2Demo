using System;
using RoR2;

namespace EntityStates.VagrantNovaItem
{
	// Token: 0x02000169 RID: 361
	public class ReadyState : BaseVagrantNovaItemState
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x0001B1BD File Offset: 0x000193BD
		public override void OnEnter()
		{
			base.OnEnter();
			CharacterBody attachedBody = base.attachedBody;
			this.attachedHealthComponent = ((attachedBody != null) ? attachedBody.healthComponent : null);
			base.SetChargeSparkEmissionRateMultiplier(1f);
			GlobalEventManager.onServerDamageDealt += this.OnDamaged;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001B1F9 File Offset: 0x000193F9
		public override void OnExit()
		{
			base.OnExit();
			GlobalEventManager.onServerDamageDealt -= this.OnDamaged;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001B212 File Offset: 0x00019412
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.attachedHealthComponent.isHealthLow)
			{
				this.outer.SetNextState(new ChargeState());
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001B23F File Offset: 0x0001943F
		private void OnDamaged(DamageReport report)
		{
			if (report.hitLowHealth)
			{
				CharacterBody characterBody;
				if (report == null)
				{
					characterBody = null;
				}
				else
				{
					HealthComponent victim = report.victim;
					characterBody = ((victim != null) ? victim.body : null);
				}
				if (characterBody == base.attachedBody)
				{
					this.outer.SetNextState(new ChargeState());
				}
			}
		}

		// Token: 0x040007AF RID: 1967
		private HealthComponent attachedHealthComponent;
	}
}
