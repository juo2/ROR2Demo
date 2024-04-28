using System;
using RoR2;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001BB RID: 443
	public abstract class SurvivorPodBaseState : EntityState
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00021D74 File Offset: 0x0001FF74
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00021D7C File Offset: 0x0001FF7C
		private protected SurvivorPodController survivorPodController { protected get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00021D85 File Offset: 0x0001FF85
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00021D8D File Offset: 0x0001FF8D
		private protected VehicleSeat vehicleSeat { protected get; private set; }

		// Token: 0x060007F6 RID: 2038 RVA: 0x00021D98 File Offset: 0x0001FF98
		public override void OnEnter()
		{
			base.OnEnter();
			this.survivorPodController = base.GetComponent<SurvivorPodController>();
			SurvivorPodController survivorPodController = this.survivorPodController;
			this.vehicleSeat = ((survivorPodController != null) ? survivorPodController.vehicleSeat : null);
			if (!this.survivorPodController && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}
	}
}
