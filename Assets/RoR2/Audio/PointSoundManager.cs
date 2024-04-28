using System;
using System.Collections.Generic;
using RoR2.ConVar;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RoR2.Audio
{
	// Token: 0x02000E5E RID: 3678
	public static class PointSoundManager
	{
		// Token: 0x06005436 RID: 21558 RVA: 0x0015B6BF File Offset: 0x001598BF
		private static AkGameObj RequestEmitter()
		{
			if (PointSoundManager.emitterPool.Count > 0)
			{
				return PointSoundManager.emitterPool.Pop();
			}
			GameObject gameObject = new GameObject("SoundEmitter");
			gameObject.AddComponent<Rigidbody>().isKinematic = true;
			return gameObject.AddComponent<AkGameObj>();
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0015B6F4 File Offset: 0x001598F4
		private static void FreeEmitter(AkGameObj emitter)
		{
			if (emitter)
			{
				PointSoundManager.emitterPool.Push(emitter);
			}
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0015B70C File Offset: 0x0015990C
		public static uint EmitSoundLocal(AkEventIdArg akEventId, Vector3 position)
		{
			if (WwiseIntegrationManager.noAudio || akEventId.id == 0U || akEventId.id == 2166136261U)
			{
				return 0U;
			}
			AkGameObj akGameObj = PointSoundManager.RequestEmitter();
			akGameObj.transform.position = position;
			uint result;
			if (PointSoundManager.cvPointSoundManagerTimeout.value < 0f)
			{
				result = AkSoundEngine.PostEvent(akEventId, akGameObj.gameObject, 1U, new AkCallbackManager.EventCallback(PointSoundManager.Callback), akGameObj);
			}
			else
			{
				result = AkSoundEngine.PostEvent(akEventId, akGameObj.gameObject);
				PointSoundManager.timeouts.Enqueue(new PointSoundManager.TimeoutInfo(akGameObj, Time.unscaledTime));
			}
			return result;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0015B7A4 File Offset: 0x001599A4
		private static void Callback(object cookie, AkCallbackType in_type, AkCallbackInfo in_info)
		{
			if (in_type == AkCallbackType.AK_EndOfEvent)
			{
				PointSoundManager.FreeEmitter((AkGameObj)cookie);
			}
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0015B7B5 File Offset: 0x001599B5
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			SceneManager.sceneUnloaded += PointSoundManager.OnSceneUnloaded;
			RoR2Application.onUpdate += PointSoundManager.StaticUpdate;
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0015B7D9 File Offset: 0x001599D9
		private static void StaticUpdate()
		{
			PointSoundManager.ProcessTimeoutQueue();
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0015B7E0 File Offset: 0x001599E0
		private static void OnSceneUnloaded(Scene scene)
		{
			PointSoundManager.ClearEmitterPool();
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0015B7E8 File Offset: 0x001599E8
		private static void ClearEmitterPool()
		{
			foreach (AkGameObj akGameObj in PointSoundManager.emitterPool)
			{
				if (akGameObj)
				{
					UnityEngine.Object.Destroy(akGameObj.gameObject);
				}
			}
			PointSoundManager.emitterPool.Clear();
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0015B850 File Offset: 0x00159A50
		public static void EmitSoundServer(NetworkSoundEventIndex networkSoundEventIndex, Vector3 position)
		{
			PointSoundManager.sharedMessage.soundEventIndex = networkSoundEventIndex;
			PointSoundManager.sharedMessage.position = position;
			NetworkServer.SendByChannelToAll(72, PointSoundManager.sharedMessage, PointSoundManager.channel.intVal);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0015B87F File Offset: 0x00159A7F
		[NetworkMessageHandler(client = true, server = false, msgType = 72)]
		private static void HandleMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<PointSoundManager.NetworkSoundEventMessage>(PointSoundManager.sharedMessage);
			PointSoundManager.EmitSoundLocal(NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(PointSoundManager.sharedMessage.soundEventIndex), PointSoundManager.sharedMessage.position);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0015B8B0 File Offset: 0x00159AB0
		private static void ProcessTimeoutQueue()
		{
			float num = Time.unscaledTime - PointSoundManager.cvPointSoundManagerTimeout.value;
			while (PointSoundManager.timeouts.Count > 0)
			{
				PointSoundManager.TimeoutInfo timeoutInfo = PointSoundManager.timeouts.Peek();
				if (num <= timeoutInfo.startTime)
				{
					break;
				}
				PointSoundManager.timeouts.Dequeue();
				PointSoundManager.FreeEmitter(timeoutInfo.emitter);
			}
		}

		// Token: 0x04004FFE RID: 20478
		private static readonly Stack<AkGameObj> emitterPool = new Stack<AkGameObj>();

		// Token: 0x04004FFF RID: 20479
		private const uint EMPTY_AK_EVENT_SOUND = 2166136261U;

		// Token: 0x04005000 RID: 20480
		private static readonly PointSoundManager.NetworkSoundEventMessage sharedMessage = new PointSoundManager.NetworkSoundEventMessage();

		// Token: 0x04005001 RID: 20481
		private static readonly QosChannelIndex channel = QosChannelIndex.effects;

		// Token: 0x04005002 RID: 20482
		private const short messageType = 72;

		// Token: 0x04005003 RID: 20483
		private static readonly FloatConVar cvPointSoundManagerTimeout = new FloatConVar("pointsoundmanager_timeout", ConVarFlags.None, "3", "Timeout value in seconds to use for sound emitters dispatched by PointSoundManager. -1 for end-of-playback callbacks instead, which we suspect may have thread-safety issues.");

		// Token: 0x04005004 RID: 20484
		private static readonly Queue<PointSoundManager.TimeoutInfo> timeouts = new Queue<PointSoundManager.TimeoutInfo>();

		// Token: 0x02000E5F RID: 3679
		private class NetworkSoundEventMessage : MessageBase
		{
			// Token: 0x06005442 RID: 21570 RVA: 0x0015B957 File Offset: 0x00159B57
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.WriteNetworkSoundEventIndex(this.soundEventIndex);
				writer.Write(this.position);
			}

			// Token: 0x06005443 RID: 21571 RVA: 0x0015B978 File Offset: 0x00159B78
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.soundEventIndex = reader.ReadNetworkSoundEventIndex();
				this.position = reader.ReadVector3();
			}

			// Token: 0x04005005 RID: 20485
			public NetworkSoundEventIndex soundEventIndex;

			// Token: 0x04005006 RID: 20486
			public Vector3 position;
		}

		// Token: 0x02000E60 RID: 3680
		private struct TimeoutInfo
		{
			// Token: 0x06005445 RID: 21573 RVA: 0x0015B999 File Offset: 0x00159B99
			public TimeoutInfo(AkGameObj emitter, float startTime)
			{
				this.emitter = emitter;
				this.startTime = startTime;
			}

			// Token: 0x04005007 RID: 20487
			public readonly AkGameObj emitter;

			// Token: 0x04005008 RID: 20488
			public readonly float startTime;
		}
	}
}
