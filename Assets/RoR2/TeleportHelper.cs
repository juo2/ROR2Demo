using System;
using RoR2.Navigation;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A88 RID: 2696
	public static class TeleportHelper
	{
		// Token: 0x06003DDD RID: 15837 RVA: 0x000FF68C File Offset: 0x000FD88C
		public static void TeleportGameObject(GameObject gameObject, Vector3 newPosition)
		{
			bool hasEffectiveAuthority = Util.HasEffectiveAuthority(gameObject);
			TeleportHelper.TeleportGameObject(gameObject, newPosition, newPosition - gameObject.transform.position, hasEffectiveAuthority);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000FF6BC File Offset: 0x000FD8BC
		private static void TeleportGameObject(GameObject gameObject, Vector3 newPosition, Vector3 delta, bool hasEffectiveAuthority)
		{
			TeleportHelper.OnTeleport(gameObject, newPosition, delta);
			if (NetworkServer.active || hasEffectiveAuthority)
			{
				TeleportHelper.TeleportMessage msg = new TeleportHelper.TeleportMessage
				{
					gameObject = gameObject,
					newPosition = newPosition,
					delta = delta
				};
				QosChannelIndex defaultReliable = QosChannelIndex.defaultReliable;
				if (NetworkServer.active)
				{
					NetworkServer.SendByChannelToAll(68, msg, defaultReliable.intVal);
					return;
				}
				NetworkManager.singleton.client.connection.SendByChannel(68, msg, defaultReliable.intVal);
			}
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000FF730 File Offset: 0x000FD930
		private static void OnTeleport(GameObject gameObject, Vector3 newPosition, Vector3 delta)
		{
			CharacterMotor component = gameObject.GetComponent<CharacterMotor>();
			if (component)
			{
				component.Motor.SetPosition(newPosition, true);
				component.velocity = new Vector3(0f, Mathf.Min(component.velocity.y, 0f), 0f);
			}
			else
			{
				gameObject.transform.position = newPosition;
			}
			ITeleportHandler[] componentsInChildren = gameObject.GetComponentsInChildren<ITeleportHandler>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].OnTeleport(newPosition - delta, newPosition);
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000FF7B8 File Offset: 0x000FD9B8
		public static void TeleportBody(CharacterBody body, Vector3 targetFootPosition)
		{
			if (body)
			{
				Vector3 b = new Vector3(0f, 0.1f, 0f);
				Vector3 b2 = body.footPosition - body.transform.position;
				TeleportHelper.TeleportGameObject(body.gameObject, targetFootPosition - b2 + b);
				if (body.characterMotor)
				{
					body.characterMotor.disableAirControlUntilCollision = false;
				}
			}
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000FF82C File Offset: 0x000FDA2C
		[NetworkMessageHandler(client = true, server = true, msgType = 68)]
		private static void HandleTeleport(NetworkMessage netMsg)
		{
			if (Util.ConnectionIsLocal(netMsg.conn))
			{
				return;
			}
			netMsg.ReadMessage<TeleportHelper.TeleportMessage>(TeleportHelper.messageBuffer);
			if (!TeleportHelper.messageBuffer.gameObject)
			{
				return;
			}
			bool flag = Util.HasEffectiveAuthority(TeleportHelper.messageBuffer.gameObject);
			if (flag)
			{
				return;
			}
			TeleportHelper.TeleportGameObject(TeleportHelper.messageBuffer.gameObject, TeleportHelper.messageBuffer.newPosition, TeleportHelper.messageBuffer.delta, flag);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000FF89C File Offset: 0x000FDA9C
		public static Vector3? FindSafeTeleportDestination(Vector3 characterFootPosition, CharacterBody characterBodyOrPrefabComponent, Xoroshiro128Plus rng)
		{
			Vector3? result = null;
			SpawnCard spawnCard = ScriptableObject.CreateInstance<SpawnCard>();
			spawnCard.hullSize = characterBodyOrPrefabComponent.hullClassification;
			spawnCard.nodeGraphType = MapNodeGroup.GraphType.Ground;
			spawnCard.prefab = LegacyResourcesAPI.Load<GameObject>("SpawnCards/HelperPrefab");
			GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
				position = characterFootPosition
			}, rng));
			if (!gameObject)
			{
				gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.RandomNormalized,
					position = characterFootPosition
				}, rng));
			}
			if (gameObject)
			{
				result = new Vector3?(gameObject.transform.position);
				UnityEngine.Object.Destroy(gameObject);
			}
			UnityEngine.Object.Destroy(spawnCard);
			return result;
		}

		// Token: 0x04003CAD RID: 15533
		private static readonly TeleportHelper.TeleportMessage messageBuffer = new TeleportHelper.TeleportMessage();

		// Token: 0x02000A89 RID: 2697
		private class TeleportMessage : MessageBase
		{
			// Token: 0x06003DE5 RID: 15845 RVA: 0x000FF95F File Offset: 0x000FDB5F
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.gameObject);
				writer.Write(this.newPosition);
				writer.Write(this.delta);
			}

			// Token: 0x06003DE6 RID: 15846 RVA: 0x000FF985 File Offset: 0x000FDB85
			public override void Deserialize(NetworkReader reader)
			{
				this.gameObject = reader.ReadGameObject();
				this.newPosition = reader.ReadVector3();
				this.delta = reader.ReadVector3();
			}

			// Token: 0x04003CAE RID: 15534
			public GameObject gameObject;

			// Token: 0x04003CAF RID: 15535
			public Vector3 newPosition;

			// Token: 0x04003CB0 RID: 15536
			public Vector3 delta;
		}
	}
}
