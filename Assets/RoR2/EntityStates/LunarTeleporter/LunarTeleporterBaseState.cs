using System;
using RoR2;

namespace EntityStates.LunarTeleporter
{
	// Token: 0x020002B0 RID: 688
	public class LunarTeleporterBaseState : BaseState
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x00033802 File Offset: 0x00031A02
		public override void OnEnter()
		{
			base.OnEnter();
			this.genericInteraction = base.GetComponent<GenericInteraction>();
			this.teleporterInteraction = base.transform.root.GetComponent<TeleporterInteraction>();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003382C File Offset: 0x00031A2C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (TeleporterInteraction.instance)
			{
				if (TeleporterInteraction.instance.isIdle)
				{
					this.genericInteraction.Networkinteractability = this.preferredInteractability;
					return;
				}
				this.genericInteraction.Networkinteractability = Interactability.Disabled;
			}
		}

		// Token: 0x04000EE7 RID: 3815
		protected GenericInteraction genericInteraction;

		// Token: 0x04000EE8 RID: 3816
		protected Interactability preferredInteractability = Interactability.Available;

		// Token: 0x04000EE9 RID: 3817
		protected TeleporterInteraction teleporterInteraction;
	}
}
