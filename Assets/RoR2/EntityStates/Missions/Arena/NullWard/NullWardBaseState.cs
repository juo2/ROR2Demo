using System;
using RoR2;

namespace EntityStates.Missions.Arena.NullWard
{
	// Token: 0x02000262 RID: 610
	public class NullWardBaseState : EntityState
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002BE84 File Offset: 0x0002A084
		protected ArenaMissionController arenaMissionController
		{
			get
			{
				return ArenaMissionController.instance;
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002BE8C File Offset: 0x0002A08C
		public override void OnEnter()
		{
			base.OnEnter();
			this.sphereZone = base.GetComponent<SphereZone>();
			this.sphereZone.enabled = true;
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			this.childLocator = base.GetComponent<ChildLocator>();
			base.gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
		}

		// Token: 0x04000C41 RID: 3137
		public static float wardRadiusOff;

		// Token: 0x04000C42 RID: 3138
		public static float wardRadiusOn;

		// Token: 0x04000C43 RID: 3139
		public static float wardWaitingRadius;

		// Token: 0x04000C44 RID: 3140
		protected SphereZone sphereZone;

		// Token: 0x04000C45 RID: 3141
		protected PurchaseInteraction purchaseInteraction;

		// Token: 0x04000C46 RID: 3142
		protected ChildLocator childLocator;
	}
}
