using System;
using System.Collections.Generic;
using System.Diagnostics;
using RoR2.Audio;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200059C RID: 1436
	public static class EffectManager
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x00070323 File Offset: 0x0006E523
		[NetworkMessageHandler(msgType = 52, server = true)]
		private static void HandleEffectServer(NetworkMessage netMsg)
		{
			EffectManager.HandleEffectServerInternal(netMsg);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0007032B File Offset: 0x0006E52B
		[NetworkMessageHandler(msgType = 52, client = true)]
		private static void HandleEffectClient(NetworkMessage netMsg)
		{
			EffectManager.HandleEffectClientInternal(netMsg);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00070334 File Offset: 0x0006E534
		public static void SpawnEffect(GameObject effectPrefab, EffectData effectData, bool transmit)
		{
			EffectIndex effectIndex = EffectCatalog.FindEffectIndexFromPrefab(effectPrefab);
			if (effectIndex != EffectIndex.Invalid)
			{
				EffectManager.SpawnEffect(effectIndex, effectData, transmit);
				return;
			}
			if (effectPrefab && !string.IsNullOrEmpty(effectPrefab.name))
			{
				Debug.LogError("Unable to SpawnEffect from prefab named '" + ((effectPrefab != null) ? effectPrefab.name : null) + "'");
				return;
			}
			Debug.LogError(string.Format("Unable to SpawnEffect.  Is null? {0}.  Name = '{1}'.\n{2}", effectPrefab == null, (effectPrefab != null) ? effectPrefab.name : null, new StackTrace()));
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000703B8 File Offset: 0x0006E5B8
		public static void SpawnEffect(EffectIndex effectIndex, EffectData effectData, bool transmit)
		{
			if (transmit)
			{
				EffectManager.TransmitEffect(effectIndex, effectData, null);
				if (NetworkServer.active)
				{
					return;
				}
			}
			if (NetworkClient.active)
			{
				if (effectData.networkSoundEventIndex != NetworkSoundEventIndex.Invalid)
				{
					PointSoundManager.EmitSoundLocal(NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(effectData.networkSoundEventIndex), effectData.origin);
				}
				EffectDef effectDef = EffectCatalog.GetEffectDef(effectIndex);
				if (effectDef == null)
				{
					return;
				}
				string spawnSoundEventName = effectDef.spawnSoundEventName;
				if (!string.IsNullOrEmpty(spawnSoundEventName))
				{
					PointSoundManager.EmitSoundLocal((AkEventIdArg)spawnSoundEventName, effectData.origin);
				}
				SurfaceDef surfaceDef = SurfaceDefCatalog.GetSurfaceDef(effectData.surfaceDefIndex);
				if (surfaceDef != null)
				{
					string impactSoundString = surfaceDef.impactSoundString;
					if (!string.IsNullOrEmpty(impactSoundString))
					{
						PointSoundManager.EmitSoundLocal((AkEventIdArg)impactSoundString, effectData.origin);
					}
				}
				if (!VFXBudget.CanAffordSpawn(effectDef.prefabVfxAttributes))
				{
					return;
				}
				if (effectDef.cullMethod != null && !effectDef.cullMethod(effectData))
				{
					return;
				}
				EffectData effectData2 = effectData.Clone();
				EffectComponent component = UnityEngine.Object.Instantiate<GameObject>(effectDef.prefab, effectData2.origin, effectData2.rotation).GetComponent<EffectComponent>();
				if (component)
				{
					component.effectData = effectData2.Clone();
				}
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000704C8 File Offset: 0x0006E6C8
		private static void TransmitEffect(EffectIndex effectIndex, EffectData effectData, NetworkConnection netOrigin = null)
		{
			EffectManager.outgoingEffectMessage.effectIndex = effectIndex;
			EffectData.Copy(effectData, EffectManager.outgoingEffectMessage.effectData);
			if (NetworkServer.active)
			{
				using (IEnumerator<NetworkConnection> enumerator = NetworkServer.connections.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NetworkConnection networkConnection = enumerator.Current;
						if (networkConnection != null && networkConnection != netOrigin)
						{
							networkConnection.SendByChannel(52, EffectManager.outgoingEffectMessage, EffectManager.qosChannel.intVal);
						}
					}
					return;
				}
			}
			if (ClientScene.readyConnection != null)
			{
				ClientScene.readyConnection.SendByChannel(52, EffectManager.outgoingEffectMessage, EffectManager.qosChannel.intVal);
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00070574 File Offset: 0x0006E774
		private static void HandleEffectClientInternal(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<EffectManager.EffectMessage>(EffectManager.incomingEffectMessage);
			EffectManager.SpawnEffect(EffectManager.incomingEffectMessage.effectIndex, EffectManager.incomingEffectMessage.effectData, false);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0007059B File Offset: 0x0006E79B
		private static void HandleEffectServerInternal(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<EffectManager.EffectMessage>(EffectManager.incomingEffectMessage);
			EffectManager.TransmitEffect(EffectManager.incomingEffectMessage.effectIndex, EffectManager.incomingEffectMessage.effectData, netMsg.conn);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000705C8 File Offset: 0x0006E7C8
		public static void SimpleMuzzleFlash(GameObject effectPrefab, GameObject obj, string muzzleName, bool transmit)
		{
			if (!obj)
			{
				return;
			}
			ModelLocator component = obj.GetComponent<ModelLocator>();
			if (component && component.modelTransform)
			{
				ChildLocator component2 = component.modelTransform.GetComponent<ChildLocator>();
				if (component2)
				{
					int childIndex = component2.FindChildIndex(muzzleName);
					Transform transform = component2.FindChild(childIndex);
					if (transform)
					{
						EffectData effectData = new EffectData
						{
							origin = transform.position
						};
						effectData.SetChildLocatorTransformReference(obj, childIndex);
						EffectManager.SpawnEffect(effectPrefab, effectData, transmit);
					}
				}
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0007064B File Offset: 0x0006E84B
		public static void SimpleImpactEffect(GameObject effectPrefab, Vector3 hitPos, Vector3 normal, bool transmit)
		{
			EffectManager.SpawnEffect(effectPrefab, new EffectData
			{
				origin = hitPos,
				rotation = ((normal == Vector3.zero) ? Quaternion.identity : Util.QuaternionSafeLookRotation(normal))
			}, transmit);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00070680 File Offset: 0x0006E880
		public static void SimpleImpactEffect(GameObject effectPrefab, Vector3 hitPos, Vector3 normal, Color color, bool transmit)
		{
			EffectManager.SpawnEffect(effectPrefab, new EffectData
			{
				origin = hitPos,
				rotation = Util.QuaternionSafeLookRotation(normal),
				color = color
			}, transmit);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000706AE File Offset: 0x0006E8AE
		public static void SimpleEffect(GameObject effectPrefab, Vector3 position, Quaternion rotation, bool transmit)
		{
			EffectManager.SpawnEffect(effectPrefab, new EffectData
			{
				origin = position,
				rotation = rotation
			}, transmit);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000706CA File Offset: 0x0006E8CA
		public static void SimpleSoundEffect(NetworkSoundEventIndex soundEventIndex, Vector3 position, bool transmit)
		{
			EffectManager.SpawnEffect(EffectIndex.Invalid, new EffectData
			{
				origin = position,
				networkSoundEventIndex = soundEventIndex
			}, transmit);
		}

		// Token: 0x04002042 RID: 8258
		private static readonly QosChannelIndex qosChannel = QosChannelIndex.effects;

		// Token: 0x04002043 RID: 8259
		private static readonly EffectManager.EffectMessage outgoingEffectMessage = new EffectManager.EffectMessage();

		// Token: 0x04002044 RID: 8260
		private static readonly EffectManager.EffectMessage incomingEffectMessage = new EffectManager.EffectMessage();

		// Token: 0x0200059D RID: 1437
		private class EffectMessage : MessageBase
		{
			// Token: 0x060019F7 RID: 6647 RVA: 0x00070706 File Offset: 0x0006E906
			public override void Serialize(NetworkWriter writer)
			{
				writer.WriteEffectIndex(this.effectIndex);
				writer.Write(this.effectData);
			}

			// Token: 0x060019F8 RID: 6648 RVA: 0x00070720 File Offset: 0x0006E920
			public override void Deserialize(NetworkReader reader)
			{
				this.effectIndex = reader.ReadEffectIndex();
				reader.ReadEffectData(this.effectData);
			}

			// Token: 0x04002045 RID: 8261
			public EffectIndex effectIndex;

			// Token: 0x04002046 RID: 8262
			public readonly EffectData effectData = new EffectData();
		}
	}
}
