using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ParentEgg
{
	// Token: 0x02000225 RID: 549
	public class Hatch : GenericCharacterDeath
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00027B2B File Offset: 0x00025D2B
		public override void OnEnter()
		{
			this.controller = base.GetComponent<SpawnerPodsController>();
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.DoHatch();
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00027B4C File Offset: 0x00025D4C
		protected override void PlayDeathAnimation(float crossfadeDuration)
		{
			this.PlayAnimation("Body", "Hatch");
			EffectManager.SimpleEffect(this.controller.hatchEffect, base.gameObject.transform.position, base.transform.rotation, false);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00027B8A File Offset: 0x00025D8A
		protected override void PlayDeathSound()
		{
			Util.PlaySound(this.controller.podHatchSound, base.gameObject);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00027BA4 File Offset: 0x00025DA4
		private void DoHatch()
		{
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscParent"), new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Direct,
				minDistance = 0f,
				maxDistance = 1f,
				spawnOnTarget = base.transform
			}, RoR2Application.rng);
			directorSpawnRequest.summonerBodyObject = base.gameObject;
			DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
			directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(this.OnHatchlingSpawned));
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00027C30 File Offset: 0x00025E30
		private void OnHatchlingSpawned(SpawnCard.SpawnResult spawnResult)
		{
			CharacterMaster component = spawnResult.spawnedInstance.GetComponent<CharacterMaster>();
			component.teamIndex = base.characterBody.teamComponent.teamIndex;
			CharacterMaster master = base.characterBody.master;
			CharacterMaster characterMaster = master ? master.minionOwnership.ownerMaster : null;
			if (component)
			{
				Inventory inventory = base.characterBody.master.inventory;
				Inventory inventory2 = component.inventory;
				inventory2.CopyItemsFrom(inventory);
				inventory2.CopyEquipmentFrom(inventory);
				GameObject bodyObject = component.GetBodyObject();
				if (bodyObject && characterMaster)
				{
					Deployable component2 = bodyObject.GetComponent<Deployable>();
					characterMaster.AddDeployable(component2, DeployableSlot.ParentAlly);
				}
			}
		}

		// Token: 0x04000B31 RID: 2865
		private SpawnerPodsController controller;
	}
}
