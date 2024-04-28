using System;
using System.Collections.Generic;
using System.Diagnostics;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000178 RID: 376
	public class Networking : IDisposable
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x000391FC File Offset: 0x000373FC
		internal Networking(BaseSteamworks steamworks, SteamNetworking networking)
		{
			this.networking = networking;
			steamworks.RegisterCallback<P2PSessionRequest_t>(new Action<P2PSessionRequest_t>(this.onP2PConnectionRequest));
			steamworks.RegisterCallback<P2PSessionConnectFail_t>(new Action<P2PSessionConnectFail_t>(this.onP2PConnectionFailed));
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00039250 File Offset: 0x00037450
		public void Dispose()
		{
			this.networking = null;
			this.OnIncomingConnection = null;
			this.OnConnectionFailed = null;
			this.OnP2PData = null;
			this.ListenChannels.Clear();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003927C File Offset: 0x0003747C
		public void Update()
		{
			if (this.OnP2PData == null)
			{
				return;
			}
			if (this.UpdateTimer.Elapsed.TotalSeconds < 0.016666666666666666)
			{
				return;
			}
			this.UpdateTimer.Reset();
			this.UpdateTimer.Start();
			foreach (int channel in this.ListenChannels)
			{
				while (this.ReadP2PPacket(channel))
				{
				}
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00039310 File Offset: 0x00037510
		public void SetListenChannel(int ChannelId, bool Listen)
		{
			this.ListenChannels.RemoveAll((int x) => x == ChannelId);
			if (Listen)
			{
				this.ListenChannels.Add(ChannelId);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00039358 File Offset: 0x00037558
		private void onP2PConnectionRequest(P2PSessionRequest_t o)
		{
			if (this.OnIncomingConnection == null)
			{
				this.networking.CloseP2PSessionWithUser(o.SteamIDRemote);
				return;
			}
			if (this.OnIncomingConnection(o.SteamIDRemote))
			{
				this.networking.AcceptP2PSessionWithUser(o.SteamIDRemote);
				return;
			}
			this.networking.CloseP2PSessionWithUser(o.SteamIDRemote);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000393C7 File Offset: 0x000375C7
		public void CloseP2PSessionWithUser(ulong steamId)
		{
			this.networking.CloseP2PSessionWithUser(steamId);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000393DB File Offset: 0x000375DB
		private void onP2PConnectionFailed(P2PSessionConnectFail_t o)
		{
			if (this.OnConnectionFailed != null)
			{
				this.OnConnectionFailed(o.SteamIDRemote, (Networking.SessionError)o.P2PSessionError);
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000393FC File Offset: 0x000375FC
		public unsafe bool SendP2PPacket(ulong steamid, byte[] data, int length, Networking.SendType eP2PSendType = Networking.SendType.Reliable, int nChannel = 0)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return this.networking.SendP2PPacket(steamid, (IntPtr)((void*)value), (uint)length, (P2PSend)eP2PSendType, nChannel);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00039440 File Offset: 0x00037640
		private unsafe bool ReadP2PPacket(int channel)
		{
			uint num = 0U;
			if (!this.networking.IsP2PPacketAvailable(out num, channel))
			{
				return false;
			}
			if ((long)Networking.ReceiveBuffer.Length < (long)((ulong)num))
			{
				Networking.ReceiveBuffer = new byte[num + 1024U];
			}
			byte[] receiveBuffer;
			byte* value;
			if ((receiveBuffer = Networking.ReceiveBuffer) == null || receiveBuffer.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &receiveBuffer[0];
			}
			CSteamID value2 = 1UL;
			if (!this.networking.ReadP2PPacket((IntPtr)((void*)value), num, out num, out value2, channel))
			{
				return false;
			}
			Networking.OnRecievedP2PData onP2PData = this.OnP2PData;
			if (onP2PData != null)
			{
				onP2PData(value2, Networking.ReceiveBuffer, (int)num, channel);
			}
			return true;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000394DC File Offset: 0x000376DC
		public bool CloseSession(ulong steamId)
		{
			return this.networking.CloseP2PSessionWithUser(steamId);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000394F0 File Offset: 0x000376F0
		public bool GetP2PSessionState(ulong steamid, ref Networking.P2PSessionState sessionState)
		{
			P2PSessionState_t p2PSessionState_t = default(P2PSessionState_t);
			if (!this.networking.GetP2PSessionState(steamid, ref p2PSessionState_t))
			{
				return false;
			}
			sessionState.ConnectionActive = p2PSessionState_t.ConnectionActive;
			sessionState.Connecting = p2PSessionState_t.Connecting;
			sessionState.P2PSessionError = p2PSessionState_t.P2PSessionError;
			sessionState.UsingRelay = p2PSessionState_t.UsingRelay;
			sessionState.BytesQueuedForSend = p2PSessionState_t.BytesQueuedForSend;
			sessionState.PacketsQueuedForSend = p2PSessionState_t.PacketsQueuedForSend;
			sessionState.RemoteIP = p2PSessionState_t.RemoteIP;
			sessionState.RemotePort = p2PSessionState_t.RemotePort;
			return true;
		}

		// Token: 0x0400085D RID: 2141
		private static byte[] ReceiveBuffer = new byte[65536];

		// Token: 0x0400085E RID: 2142
		public Networking.OnRecievedP2PData OnP2PData;

		// Token: 0x0400085F RID: 2143
		public Func<ulong, bool> OnIncomingConnection;

		// Token: 0x04000860 RID: 2144
		public Action<ulong, Networking.SessionError> OnConnectionFailed;

		// Token: 0x04000861 RID: 2145
		private List<int> ListenChannels = new List<int>();

		// Token: 0x04000862 RID: 2146
		private Stopwatch UpdateTimer = Stopwatch.StartNew();

		// Token: 0x04000863 RID: 2147
		internal SteamNetworking networking;

		// Token: 0x02000291 RID: 657
		// (Invoke) Token: 0x06001ED3 RID: 7891
		public delegate void OnRecievedP2PData(ulong steamid, byte[] data, int dataLength, int channel);

		// Token: 0x02000292 RID: 658
		public enum SessionError : byte
		{
			// Token: 0x04000C72 RID: 3186
			None,
			// Token: 0x04000C73 RID: 3187
			NotRunningApp,
			// Token: 0x04000C74 RID: 3188
			NoRightsToApp,
			// Token: 0x04000C75 RID: 3189
			DestinationNotLoggedIn,
			// Token: 0x04000C76 RID: 3190
			Timeout,
			// Token: 0x04000C77 RID: 3191
			Max
		}

		// Token: 0x02000293 RID: 659
		public enum SendType
		{
			// Token: 0x04000C79 RID: 3193
			Unreliable,
			// Token: 0x04000C7A RID: 3194
			UnreliableNoDelay,
			// Token: 0x04000C7B RID: 3195
			Reliable,
			// Token: 0x04000C7C RID: 3196
			ReliableWithBuffering
		}

		// Token: 0x02000294 RID: 660
		public struct P2PSessionState
		{
			// Token: 0x04000C7D RID: 3197
			public byte ConnectionActive;

			// Token: 0x04000C7E RID: 3198
			public byte Connecting;

			// Token: 0x04000C7F RID: 3199
			public byte P2PSessionError;

			// Token: 0x04000C80 RID: 3200
			public byte UsingRelay;

			// Token: 0x04000C81 RID: 3201
			public int BytesQueuedForSend;

			// Token: 0x04000C82 RID: 3202
			public int PacketsQueuedForSend;

			// Token: 0x04000C83 RID: 3203
			public uint RemoteIP;

			// Token: 0x04000C84 RID: 3204
			public ushort RemotePort;
		}
	}
}
