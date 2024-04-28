using System;
using HG;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C37 RID: 3127
	public static class RttManager
	{
		// Token: 0x060046B6 RID: 18102 RVA: 0x00124898 File Offset: 0x00122A98
		public static float GetConnectionRTT(NetworkConnection connection)
		{
			int num;
			if (RttManager.FindConnectionIndex(connection, out num))
			{
				return RttManager.entries[num].newestRttInSeconds;
			}
			return 0f;
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x001248C8 File Offset: 0x00122AC8
		public static uint GetConnectionRTTInMilliseconds(NetworkConnection connection)
		{
			int num;
			if (RttManager.FindConnectionIndex(connection, out num))
			{
				return RttManager.entries[num].newestRttInMilliseconds;
			}
			return 0U;
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x001248F4 File Offset: 0x00122AF4
		public static float GetConnectionFrameSmoothedRtt(NetworkConnection connection)
		{
			int num;
			if (RttManager.FindConnectionIndex(connection, out num))
			{
				return RttManager.entries[num].frameSmoothedRtt;
			}
			return 0f;
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x00124924 File Offset: 0x00122B24
		public static float GetConnectionFixedSmoothedRtt(NetworkConnection connection)
		{
			int num;
			if (RttManager.FindConnectionIndex(connection, out num))
			{
				return RttManager.entries[num].fixedSmoothedRtt;
			}
			return 0f;
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00124954 File Offset: 0x00122B54
		private static void OnConnectionDiscovered(NetworkConnection connection)
		{
			RttManager.ConnectionRttInfo connectionRttInfo = new RttManager.ConnectionRttInfo(connection);
			ArrayUtils.ArrayAppend<RttManager.ConnectionRttInfo>(ref RttManager.entries, connectionRttInfo);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00124978 File Offset: 0x00122B78
		private static void OnConnectionLost(NetworkConnection connection)
		{
			int position;
			if (RttManager.FindConnectionIndex(connection, out position))
			{
				ArrayUtils.ArrayRemoveAtAndResize<RttManager.ConnectionRttInfo>(ref RttManager.entries, position, 1);
			}
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0012499C File Offset: 0x00122B9C
		private static bool FindConnectionIndex(NetworkConnection connection, out int entryIndex)
		{
			RttManager.ConnectionRttInfo[] array = RttManager.entries;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].connection == connection)
				{
					entryIndex = i;
					return true;
				}
			}
			entryIndex = -1;
			return false;
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x001249D5 File Offset: 0x00122BD5
		private static void UpdateFilteredRtt(float deltaTime, float targetValue, ref float currentValue, ref float velocity)
		{
			if (currentValue == 0f)
			{
				currentValue = targetValue;
				velocity = 0f;
				return;
			}
			currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref velocity, RttManager.cvRttSmoothDuration.value, 100f, deltaTime);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00124A08 File Offset: 0x00122C08
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			NetworkManagerSystem.onClientConnectGlobal += RttManager.OnConnectionDiscovered;
			NetworkManagerSystem.onClientDisconnectGlobal += RttManager.OnConnectionLost;
			NetworkManagerSystem.onServerConnectGlobal += RttManager.OnConnectionDiscovered;
			NetworkManagerSystem.onServerDisconnectGlobal += RttManager.OnConnectionLost;
			RoR2Application.onUpdate += RttManager.Update;
			RoR2Application.onFixedUpdate += RttManager.FixedUpdate;
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00124A7C File Offset: 0x00122C7C
		private static void Update()
		{
			float deltaTime = Time.deltaTime;
			RttManager.ConnectionRttInfo[] array = RttManager.entries;
			for (int i = 0; i < array.Length; i++)
			{
				ref RttManager.ConnectionRttInfo ptr = ref array[i];
				RttManager.UpdateFilteredRtt(deltaTime, ptr.newestRttInSeconds, ref ptr.frameSmoothedRtt, ref ptr.frameVelocity);
			}
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00124AC4 File Offset: 0x00122CC4
		private static void FixedUpdate()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			RttManager.ConnectionRttInfo[] array = RttManager.entries;
			for (int i = 0; i < array.Length; i++)
			{
				ref RttManager.ConnectionRttInfo ptr = ref array[i];
				RttManager.UpdateFilteredRtt(fixedDeltaTime, ptr.newestRttInSeconds, ref ptr.fixedSmoothedRtt, ref ptr.fixedVelocity);
			}
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00124B0C File Offset: 0x00122D0C
		[NetworkMessageHandler(msgType = 65, client = true, server = true)]
		private static void HandlePing(NetworkMessage netMsg)
		{
			NetworkReader reader = netMsg.reader;
			netMsg.conn.SendByChannel(66, reader.ReadMessage<RttManager.PingMessage>(), netMsg.channelId);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00124B3C File Offset: 0x00122D3C
		[NetworkMessageHandler(msgType = 66, client = true, server = true)]
		private static void HandlePingResponse(NetworkMessage netMsg)
		{
			uint timeStampMs = netMsg.reader.ReadMessage<RttManager.PingMessage>().timeStampMs;
			uint num = (uint)RoR2Application.instance.stopwatch.ElapsedMilliseconds - timeStampMs;
			int num2;
			if (RttManager.FindConnectionIndex(netMsg.conn, out num2))
			{
				RttManager.ConnectionRttInfo[] array = RttManager.entries;
				int num3 = num2;
				array[num3].newestRttInMilliseconds = num;
				array[num3].newestRttInSeconds = num * 0.001f;
			}
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00124B9C File Offset: 0x00122D9C
		public static void Ping(NetworkConnection conn, int channelId)
		{
			conn.SendByChannel(65, new RttManager.PingMessage
			{
				timeStampMs = (uint)RoR2Application.instance.stopwatch.ElapsedMilliseconds
			}, channelId);
		}

		// Token: 0x04004489 RID: 17545
		private static RttManager.ConnectionRttInfo[] entries = Array.Empty<RttManager.ConnectionRttInfo>();

		// Token: 0x0400448A RID: 17546
		private static readonly FloatConVar cvRttSmoothDuration = new FloatConVar("net_rtt_smooth_duration", ConVarFlags.None, "0.1", "The smoothing duration for round-trip time values.");

		// Token: 0x02000C38 RID: 3128
		private struct ConnectionRttInfo
		{
			// Token: 0x060046C5 RID: 18117 RVA: 0x00124BEC File Offset: 0x00122DEC
			public ConnectionRttInfo(NetworkConnection connection)
			{
				this.connection = connection;
				this.newestRttInMilliseconds = 0U;
				this.newestRttInSeconds = 0f;
				this.frameSmoothedRtt = 0f;
				this.frameVelocity = 0f;
				this.fixedSmoothedRtt = 0f;
				this.fixedVelocity = 0f;
			}

			// Token: 0x0400448B RID: 17547
			public readonly NetworkConnection connection;

			// Token: 0x0400448C RID: 17548
			public float newestRttInSeconds;

			// Token: 0x0400448D RID: 17549
			public uint newestRttInMilliseconds;

			// Token: 0x0400448E RID: 17550
			public float frameSmoothedRtt;

			// Token: 0x0400448F RID: 17551
			public float frameVelocity;

			// Token: 0x04004490 RID: 17552
			public float fixedSmoothedRtt;

			// Token: 0x04004491 RID: 17553
			public float fixedVelocity;
		}

		// Token: 0x02000C39 RID: 3129
		private class PingMessage : MessageBase
		{
			// Token: 0x060046C7 RID: 18119 RVA: 0x00124C3E File Offset: 0x00122E3E
			public override void Serialize(NetworkWriter writer)
			{
				writer.WritePackedUInt32(this.timeStampMs);
			}

			// Token: 0x060046C8 RID: 18120 RVA: 0x00124C4C File Offset: 0x00122E4C
			public override void Deserialize(NetworkReader reader)
			{
				this.timeStampMs = reader.ReadPackedUInt32();
			}

			// Token: 0x04004492 RID: 17554
			public uint timeStampMs;
		}
	}
}
