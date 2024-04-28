using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.LunarTeleporter
{
	// Token: 0x020002B2 RID: 690
	public class Active : LunarTeleporterBaseState
	{
		// Token: 0x06000C39 RID: 3129 RVA: 0x000338E0 File Offset: 0x00031AE0
		public override void OnEnter()
		{
			base.OnEnter();
			this.preferredInteractability = Interactability.Available;
			this.PlayAnimation("Base", "Active");
			this.outer.mainStateType = new SerializableEntityStateType(typeof(ActiveToIdle));
			if (NetworkServer.active)
			{
				this.teleporterInteraction.sceneExitController.useRunNextStageScene = false;
			}
		}
	}
}
