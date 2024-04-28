using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Missions.Goldshores
{
	// Token: 0x0200024B RID: 587
	public class ActivateBeacons : EntityState
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0002B09D File Offset: 0x0002929D
		public override void OnEnter()
		{
			base.OnEnter();
			if (GoldshoresMissionController.instance)
			{
				GoldshoresMissionController.instance.SpawnBeacons();
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002B0BB File Offset: 0x000292BB
		public override void OnExit()
		{
			base.OnExit();
			if (!this.outer.destroying && GoldshoresMissionController.instance)
			{
				GoldshoresMissionController.instance.BeginTransitionIntoBossfight();
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002B0E8 File Offset: 0x000292E8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && GoldshoresMissionController.instance && GoldshoresMissionController.instance.beaconsActive >= GoldshoresMissionController.instance.beaconsRequiredToSpawnBoss)
			{
				this.outer.SetNextState(new GoldshoresBossfight());
			}
		}
	}
}
