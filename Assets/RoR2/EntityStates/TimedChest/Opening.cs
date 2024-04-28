using System;
using RoR2;

namespace EntityStates.TimedChest
{
	// Token: 0x020001B0 RID: 432
	public class Opening : BaseState
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060007C3 RID: 1987 RVA: 0x00021280 File Offset: 0x0001F480
		// (remove) Token: 0x060007C4 RID: 1988 RVA: 0x000212B4 File Offset: 0x0001F4B4
		public static event Action onOpened;

		// Token: 0x060007C5 RID: 1989 RVA: 0x000212E8 File Offset: 0x0001F4E8
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Opening");
			TimedChestController component = base.GetComponent<TimedChestController>();
			if (component)
			{
				component.purchased = true;
			}
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00021348 File Offset: 0x0001F548
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Opening.delayUntilUnlockAchievement && !this.hasGrantedAchievement)
			{
				Action action = Opening.onOpened;
				if (action != null)
				{
					action();
				}
				this.hasGrantedAchievement = true;
				GenericPickupController componentInChildren = base.gameObject.GetComponentInChildren<GenericPickupController>();
				if (componentInChildren)
				{
					componentInChildren.enabled = true;
				}
			}
		}

		// Token: 0x04000954 RID: 2388
		public static float delayUntilUnlockAchievement;

		// Token: 0x04000955 RID: 2389
		private bool hasGrantedAchievement;
	}
}
