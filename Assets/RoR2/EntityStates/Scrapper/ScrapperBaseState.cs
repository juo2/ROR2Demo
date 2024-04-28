using System;
using RoR2;

namespace EntityStates.Scrapper
{
	// Token: 0x020001CD RID: 461
	public class ScrapperBaseState : BaseState
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool enableInteraction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002328E File Offset: 0x0002148E
		public override void OnEnter()
		{
			base.OnEnter();
			this.pickupPickerController = base.GetComponent<PickupPickerController>();
			this.scrapperController = base.GetComponent<ScrapperController>();
			this.pickupPickerController.SetAvailable(this.enableInteraction);
		}

		// Token: 0x040009BD RID: 2493
		protected PickupPickerController pickupPickerController;

		// Token: 0x040009BE RID: 2494
		protected ScrapperController scrapperController;
	}
}
