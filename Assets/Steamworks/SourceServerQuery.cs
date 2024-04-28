using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Facepunch.Steamworks
{
	// Token: 0x02000182 RID: 386
	internal class SourceServerQuery : IDisposable
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0003AB6C File Offset: 0x00038D6C
		public static void Cycle()
		{
			if (SourceServerQuery.Current.Count == 0)
			{
				return;
			}
			for (int i = SourceServerQuery.Current.Count; i > 0; i--)
			{
				SourceServerQuery.Current[i - 1].Update();
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		public SourceServerQuery(ServerList.Server server, IPAddress address, int queryPort, SourceServerQuery.QueryType queryType)
		{
			this.Server = server;
			this.endPoint = new IPEndPoint(address, queryPort);
			this.queryType = queryType;
			if (queryType != SourceServerQuery.QueryType.Rules)
			{
				if (queryType != SourceServerQuery.QueryType.Players)
				{
					throw new ArgumentOutOfRangeException("queryType", queryType, null);
				}
				this.players = new List<ServerList.PlayerInfo>();
			}
			else
			{
				this.rules = new Dictionary<string, string>();
			}
			SourceServerQuery.Current.Add(this);
			this.IsRunning = true;
			this.IsSuccessful = false;
			this.thread = new Thread(new ParameterizedThreadStart(this.ThreadedStart));
			this.thread.Start();
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003AC8C File Offset: 0x00038E8C
		private void Update()
		{
			if (this.IsRunning)
			{
				return;
			}
			SourceServerQuery.Current.Remove(this);
			SourceServerQuery.QueryType queryType = this.queryType;
			if (queryType == SourceServerQuery.QueryType.Rules)
			{
				this.Server.OnServerRulesReceiveFinished(this.rules, this.IsSuccessful);
				return;
			}
			if (queryType != SourceServerQuery.QueryType.Players)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.Server.OnServerPlayerInfosReceiveFinished(this.players, this.IsSuccessful);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003ACFC File Offset: 0x00038EFC
		private void ThreadedStart(object obj)
		{
			try
			{
				using (this.udpClient = new UdpClient())
				{
					this.udpClient.Client.SendTimeout = 3000;
					this.udpClient.Client.ReceiveTimeout = 10000;
					this.udpClient.Connect(this.endPoint);
					SourceServerQuery.QueryType queryType = this.queryType;
					if (queryType != SourceServerQuery.QueryType.Rules)
					{
						if (queryType != SourceServerQuery.QueryType.Players)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.GetPlayers();
					}
					else
					{
						this.GetRules();
					}
					this.IsSuccessful = true;
				}
			}
			catch (Exception value)
			{
				this.IsSuccessful = false;
				Console.WriteLine(value);
			}
			this.udpClient = null;
			this.IsRunning = false;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003ADCC File Offset: 0x00038FCC
		private void GetRules()
		{
			this.GetChallengeData(SourceServerQuery.A2S_RULES);
			this.Send(this._challengeBytes);
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.Receive())))
			{
				if (binaryReader.ReadByte() != 69)
				{
					throw new Exception("Invalid data received in response to A2S_RULES request");
				}
				ushort num = binaryReader.ReadUInt16();
				for (int i = 0; i < (int)num; i++)
				{
					this.rules.Add(binaryReader.ReadNullTerminatedUTF8String(this.readBuffer), binaryReader.ReadNullTerminatedUTF8String(this.readBuffer));
				}
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003AE68 File Offset: 0x00039068
		private void GetPlayers()
		{
			this.GetChallengeData(SourceServerQuery.A2S_PLAYER);
			this.Send(this._challengeBytes);
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.Receive())))
			{
				if (binaryReader.ReadByte() != 68)
				{
					throw new Exception("Invalid data received in response to A2S_PLAYERS request");
				}
				byte b = binaryReader.ReadByte();
				for (int i = 0; i < (int)b; i++)
				{
					ServerList.PlayerInfo item = default(ServerList.PlayerInfo);
					item.index = binaryReader.ReadByte();
					item.name = binaryReader.ReadNullTerminatedUTF8String(this.readBuffer);
					item.score = binaryReader.ReadInt64();
					item.duration = binaryReader.ReadSingle();
					this.players.Add(item);
				}
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003AF30 File Offset: 0x00039130
		private byte[] Receive()
		{
			byte[][] array = null;
			do
			{
				byte[] array2 = this.udpClient.Receive(ref this.endPoint);
				using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(array2)))
				{
					int num = binaryReader.ReadInt32();
					if (num == -1)
					{
						byte[] array3 = new byte[(long)array2.Length - binaryReader.BaseStream.Position];
						Buffer.BlockCopy(array2, (int)binaryReader.BaseStream.Position, array3, 0, array3.Length);
						return array3;
					}
					if (num != -2)
					{
						throw new Exception("Invalid Header");
					}
					binaryReader.ReadInt32();
					byte b = binaryReader.ReadByte();
					byte b2 = binaryReader.ReadByte();
					binaryReader.ReadInt32();
					if (array == null)
					{
						array = new byte[(int)b2][];
					}
					byte[] array4 = new byte[(long)array2.Length - binaryReader.BaseStream.Position];
					Buffer.BlockCopy(array2, (int)binaryReader.BaseStream.Position, array4, 0, array4.Length);
					array[(int)b] = array4;
				}
			}
			while (array.Any((byte[] p) => p == null));
			return this.Combine(array);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003B074 File Offset: 0x00039274
		private void GetChallengeData(byte requestType)
		{
			if (this._challengeBytes != null)
			{
				return;
			}
			this._challengeBytes = new byte[5];
			this._challengeBytes[0] = requestType;
			this._challengeBytes[1] = byte.MaxValue;
			this._challengeBytes[2] = byte.MaxValue;
			this._challengeBytes[3] = byte.MaxValue;
			this._challengeBytes[4] = byte.MaxValue;
			this.Send(this._challengeBytes);
			byte[] array = this.Receive();
			if (array[0] != 65)
			{
				throw new Exception("Invalid Challenge");
			}
			this._challengeBytes = array;
			this._challengeBytes[0] = requestType;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0003B108 File Offset: 0x00039308
		private void Send(byte[] message)
		{
			this.sendBuffer[0] = byte.MaxValue;
			this.sendBuffer[1] = byte.MaxValue;
			this.sendBuffer[2] = byte.MaxValue;
			this.sendBuffer[3] = byte.MaxValue;
			Buffer.BlockCopy(message, 0, this.sendBuffer, 4, message.Length);
			this.udpClient.Send(this.sendBuffer, message.Length + 4);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003B174 File Offset: 0x00039374
		private byte[] Combine(byte[][] arrays)
		{
			byte[] array = new byte[arrays.Sum((byte[] a) => a.Length)];
			int num = 0;
			foreach (byte[] array2 in arrays)
			{
				Buffer.BlockCopy(array2, 0, array, num, array2.Length);
				num += array2.Length;
			}
			return array;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003B1D9 File Offset: 0x000393D9
		public void Dispose()
		{
			if (this.thread != null && this.thread.IsAlive)
			{
				this.thread.Abort();
			}
			this.thread = null;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0003B204 File Offset: 0x00039404
		private void LogBytes(string prefix, byte[] bytes, int length = -1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(prefix);
			stringBuilder.Append(": { ");
			if (length == -1)
			{
				length = bytes.Length;
			}
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(string.Format("0x{0:x2} ", bytes[i]));
			}
			stringBuilder.Append("}");
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x04000894 RID: 2196
		public static List<SourceServerQuery> Current = new List<SourceServerQuery>();

		// Token: 0x04000895 RID: 2197
		private static readonly byte[] A2S_SERVERQUERY_GETCHALLENGE = new byte[]
		{
			85,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x04000896 RID: 2198
		private static readonly byte A2S_PLAYER = 85;

		// Token: 0x04000897 RID: 2199
		private static readonly byte A2S_RULES = 86;

		// Token: 0x04000898 RID: 2200
		public volatile bool IsRunning;

		// Token: 0x04000899 RID: 2201
		public volatile bool IsSuccessful;

		// Token: 0x0400089A RID: 2202
		private ServerList.Server Server;

		// Token: 0x0400089B RID: 2203
		private UdpClient udpClient;

		// Token: 0x0400089C RID: 2204
		private IPEndPoint endPoint;

		// Token: 0x0400089D RID: 2205
		private Thread thread;

		// Token: 0x0400089E RID: 2206
		private byte[] _challengeBytes;

		// Token: 0x0400089F RID: 2207
		private readonly SourceServerQuery.QueryType queryType;

		// Token: 0x040008A0 RID: 2208
		private Dictionary<string, string> rules = new Dictionary<string, string>();

		// Token: 0x040008A1 RID: 2209
		private List<ServerList.PlayerInfo> players = new List<ServerList.PlayerInfo>();

		// Token: 0x040008A2 RID: 2210
		private byte[] readBuffer = new byte[4096];

		// Token: 0x040008A3 RID: 2211
		private byte[] sendBuffer = new byte[1024];

		// Token: 0x020002A5 RID: 677
		public enum QueryType
		{
			// Token: 0x04000D2F RID: 3375
			Rules,
			// Token: 0x04000D30 RID: 3376
			Players
		}
	}
}
