using System;
using System.Collections.ObjectModel;
using RoR2;

namespace EntityStates.Interactables.MSObelisk
{
	// Token: 0x020002F0 RID: 752
	public class ReadyToEndGame : BaseState
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public override void OnEnter()
		{
			base.OnEnter();
			this.childLocator = base.GetComponent<ChildLocator>();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			this.purchaseInteraction.NetworkcontextToken = "MSOBELISK_CONTEXT_CONFIRMATION";
			this.purchaseInteraction.Networkavailable = false;
			this.childLocator.FindChild(ReadyToEndGame.chargeupChildString).gameObject.SetActive(true);
			Util.PlaySound(ReadyToEndGame.chargeupSoundString, base.gameObject);
			ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
			for (int i = 0; i < instances.Count; i++)
			{
				instances[i].master.preventGameOver = true;
			}
			for (int j = 0; j < CameraRigController.readOnlyInstancesList.Count; j++)
			{
				CameraRigController.readOnlyInstancesList[j].disableSpectating = true;
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00038CBC File Offset: 0x00036EBC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= ReadyToEndGame.chargeupDuration && !this.ready)
			{
				this.ready = true;
				this.purchaseInteraction.Networkavailable = true;
				base.gameObject.GetComponent<EntityStateMachine>().mainStateType = new SerializableEntityStateType(typeof(EndingGame));
			}
		}

		// Token: 0x04001079 RID: 4217
		public static string chargeupChildString;

		// Token: 0x0400107A RID: 4218
		public static string chargeupSoundString;

		// Token: 0x0400107B RID: 4219
		public static float chargeupDuration;

		// Token: 0x0400107C RID: 4220
		private ChildLocator childLocator;

		// Token: 0x0400107D RID: 4221
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x0400107E RID: 4222
		private bool ready;
	}
}
