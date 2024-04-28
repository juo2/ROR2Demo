using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.LunarTeleporter
{
	// Token: 0x020002B1 RID: 689
	public class Idle : LunarTeleporterBaseState
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x0003387C File Offset: 0x00031A7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.preferredInteractability = Interactability.Available;
			this.PlayAnimation("Base", "Idle");
			this.outer.mainStateType = new SerializableEntityStateType(typeof(IdleToActive));
			if (NetworkServer.active)
			{
				this.teleporterInteraction.sceneExitController.useRunNextStageScene = true;
			}
		}
	}
}
