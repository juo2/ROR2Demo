using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007DC RID: 2012
	public class NetworkedBodySpawnSlot : MonoBehaviour, MasterSpawnSlotController.ISlot
	{
		// Token: 0x06002B82 RID: 11138 RVA: 0x000BA986 File Offset: 0x000B8B86
		public bool IsOpen()
		{
			return !this.isSpawnPending && !this.spawnedMaster;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000BA9A0 File Offset: 0x000B8BA0
		public void Spawn(GameObject summonerBodyObject, Xoroshiro128Plus rng, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback = null)
		{
			if (NetworkServer.active && this.ownerBody)
			{
				Transform transform = this.ownerBody.transform;
				if (!string.IsNullOrEmpty(this.ownerAttachChildName) && this.ownerChildLocator)
				{
					Transform transform2 = this.ownerChildLocator.FindChild(this.ownerAttachChildName);
					if (transform2)
					{
						transform = transform2.transform;
					}
				}
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Direct,
					spawnOnTarget = transform
				};
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.spawnCard, placementRule, rng);
				directorSpawnRequest.summonerBodyObject = summonerBodyObject;
				DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
				directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult spawnResult)
				{
					this.OnSpawnedServer(this.ownerBody.gameObject, spawnResult, callback);
				}));
				this.isSpawnPending = true;
				DirectorCore instance = DirectorCore.instance;
				if (instance == null)
				{
					return;
				}
				instance.TrySpawnObject(directorSpawnRequest);
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000BAA88 File Offset: 0x000B8C88
		public void Kill()
		{
			if (NetworkServer.active && this.spawnedMaster)
			{
				GameObject bodyObject = this.spawnedMaster.GetBodyObject();
				if (this.killEffectPrefab && bodyObject)
				{
					EffectData effectData = new EffectData
					{
						origin = bodyObject.transform.position,
						rotation = bodyObject.transform.rotation
					};
					EffectManager.SpawnEffect(this.killEffectPrefab, effectData, true);
				}
				this.spawnedMaster.TrueKill();
				this.spawnedMaster = null;
			}
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000BAB14 File Offset: 0x000B8D14
		private void OnSpawnedServer(GameObject ownerBodyObject, SpawnCard.SpawnResult spawnResult, Action<MasterSpawnSlotController.ISlot, SpawnCard.SpawnResult> callback)
		{
			this.isSpawnPending = false;
			if (spawnResult.success)
			{
				this.spawnedMaster = spawnResult.spawnedInstance.GetComponent<CharacterMaster>();
				this.spawnedMaster.onBodyDestroyed += this.OnBodyDestroyed;
				GameObject bodyObject = this.spawnedMaster.GetBodyObject();
				if (bodyObject)
				{
					if (this.spawnEffectPrefab)
					{
						EffectData effectData = new EffectData
						{
							origin = bodyObject.transform.position,
							rotation = bodyObject.transform.rotation
						};
						EffectManager.SpawnEffect(this.spawnEffectPrefab, effectData, true);
					}
					NetworkedBodyAttachment component = bodyObject.GetComponent<NetworkedBodyAttachment>();
					if (component)
					{
						component.AttachToGameObjectAndSpawn(ownerBodyObject, this.ownerAttachChildName);
					}
				}
			}
			if (callback != null)
			{
				callback(this, spawnResult);
			}
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000BABD8 File Offset: 0x000B8DD8
		private void OnBodyDestroyed(CharacterBody body)
		{
			if (body.master.IsDeadAndOutOfLivesServer() && this.spawnedMaster)
			{
				this.spawnedMaster.onBodyDestroyed -= this.OnBodyDestroyed;
				this.spawnedMaster = null;
			}
		}

		// Token: 0x04002DF3 RID: 11763
		[SerializeField]
		private SpawnCard spawnCard;

		// Token: 0x04002DF4 RID: 11764
		[SerializeField]
		private CharacterBody ownerBody;

		// Token: 0x04002DF5 RID: 11765
		[SerializeField]
		private ChildLocator ownerChildLocator;

		// Token: 0x04002DF6 RID: 11766
		[SerializeField]
		private string ownerAttachChildName;

		// Token: 0x04002DF7 RID: 11767
		[SerializeField]
		private GameObject spawnEffectPrefab;

		// Token: 0x04002DF8 RID: 11768
		[SerializeField]
		private GameObject killEffectPrefab;

		// Token: 0x04002DF9 RID: 11769
		private CharacterMaster spawnedMaster;

		// Token: 0x04002DFA RID: 11770
		private bool isSpawnPending;
	}
}
