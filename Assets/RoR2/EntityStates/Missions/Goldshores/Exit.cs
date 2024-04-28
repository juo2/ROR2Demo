using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Goldshores
{
	// Token: 0x0200024C RID: 588
	public class Exit : EntityState
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x0002B134 File Offset: 0x00029334
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscGoldshoresPortal"), new DirectorPlacementRule
				{
					maxDistance = float.PositiveInfinity,
					minDistance = 10f,
					placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
					position = base.transform.position,
					spawnOnTarget = GoldshoresMissionController.instance.bossSpawnPosition
				}, Run.instance.stageRng));
				if (gameObject)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "PORTAL_GOLDSHORES_OPEN"
					});
					gameObject.GetComponent<SceneExitController>().useRunNextStageScene = true;
				}
				for (int i = CombatDirector.instancesList.Count - 1; i >= 0; i--)
				{
					CombatDirector.instancesList[i].enabled = false;
				}
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}
	}
}
