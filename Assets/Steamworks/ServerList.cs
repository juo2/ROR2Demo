using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000170 RID: 368
	public class ServerList : IDisposable
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x0003755B File Offset: 0x0003575B
		internal ServerList(Client client)
		{
			this.client = client;
			this.UpdateFavouriteList();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00037594 File Offset: 0x00035794
		internal void UpdateFavouriteList()
		{
			this.FavouriteHash.Clear();
			this.HistoryHash.Clear();
			for (int i = 0; i < this.client.native.matchmaking.GetFavoriteGameCount(); i++)
			{
				AppId_t appId_t = 0U;
				uint num;
				ushort num2;
				ushort num3;
				uint num4;
				uint num5;
				this.client.native.matchmaking.GetFavoriteGame(i, ref appId_t, out num, out num2, out num3, out num4, out num5);
				ulong num6 = (ulong)num;
				num6 <<= 32;
				num6 |= (ulong)num2;
				if ((num4 & 1U) == 1U)
				{
					this.FavouriteHash.Add(num6);
				}
				if ((num4 & 1U) == 1U)
				{
					this.HistoryHash.Add(num6);
				}
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00037640 File Offset: 0x00035840
		public void Dispose()
		{
			this.DisposeRuleRequests();
			this.client = null;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00037650 File Offset: 0x00035850
		public ServerList.Request Internet(ServerList.Filter filter = null)
		{
			if (filter == null)
			{
				filter = new ServerList.Filter();
				filter.Add("appid", this.client.AppId.ToString());
			}
			filter.Start();
			ServerList.Request request = new ServerList.Request(this.client);
			request.Filter = filter;
			request.AddRequest(this.client.native.servers.RequestInternetServerList(this.client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero));
			filter.Free();
			return request;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000376E4 File Offset: 0x000358E4
		public ServerList.Request Custom(IEnumerable<string> serverList)
		{
			ServerList.Request request = new ServerList.Request(this.client);
			request.ServerList = serverList;
			request.StartCustomQuery();
			return request;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00037700 File Offset: 0x00035900
		public ServerList.Request History(ServerList.Filter filter = null)
		{
			if (filter == null)
			{
				filter = new ServerList.Filter();
				filter.Add("appid", this.client.AppId.ToString());
			}
			filter.Start();
			ServerList.Request request = new ServerList.Request(this.client);
			request.Filter = filter;
			request.AddRequest(this.client.native.servers.RequestHistoryServerList(this.client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero));
			filter.Free();
			return request;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00037794 File Offset: 0x00035994
		public ServerList.Request Favourites(ServerList.Filter filter = null)
		{
			if (filter == null)
			{
				filter = new ServerList.Filter();
				filter.Add("appid", this.client.AppId.ToString());
			}
			filter.Start();
			ServerList.Request request = new ServerList.Request(this.client);
			request.Filter = filter;
			request.AddRequest(this.client.native.servers.RequestFavoritesServerList(this.client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero));
			filter.Free();
			return request;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00037828 File Offset: 0x00035A28
		public ServerList.Request Friends(ServerList.Filter filter = null)
		{
			if (filter == null)
			{
				filter = new ServerList.Filter();
				filter.Add("appid", this.client.AppId.ToString());
			}
			filter.Start();
			ServerList.Request request = new ServerList.Request(this.client);
			request.Filter = filter;
			request.AddRequest(this.client.native.servers.RequestFriendsServerList(this.client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero));
			filter.Free();
			return request;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000378BC File Offset: 0x00035ABC
		public ServerList.Request Local(ServerList.Filter filter = null)
		{
			if (filter == null)
			{
				filter = new ServerList.Filter();
				filter.Add("appid", this.client.AppId.ToString());
			}
			filter.Start();
			ServerList.Request request = new ServerList.Request(this.client);
			request.Filter = filter;
			request.AddRequest(this.client.native.servers.RequestLANServerList(this.client.AppId, IntPtr.Zero));
			filter.Free();
			return request;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00037944 File Offset: 0x00035B44
		internal bool IsFavourite(ServerList.Server server)
		{
			ulong num = (ulong)server.Address.IpToInt32();
			num <<= 32;
			num |= (ulong)server.ConnectionPort;
			return this.FavouriteHash.Contains(num);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0003797C File Offset: 0x00035B7C
		internal bool IsHistory(ServerList.Server server)
		{
			ulong num = (ulong)server.Address.IpToInt32();
			num <<= 32;
			num |= (ulong)server.ConnectionPort;
			return this.HistoryHash.Contains(num);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000379B4 File Offset: 0x00035BB4
		private void DisposeRuleRequests()
		{
			for (int i = this.liveRequests.Count - 1; i >= 0; i--)
			{
				this.DisposeRuleRequestAt(i);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000379E0 File Offset: 0x00035BE0
		private void DisposeRuleRequestAt(int i)
		{
			this.client.native.servers.CancelServerQuery(this.liveRequests[i].queryHandle);
			this.liveRequests[i].steamMatchmakingRulesResponse.Dispose();
			this.liveRequests.RemoveAt(i);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00037A38 File Offset: 0x00035C38
		public void RequestServerRules(IPAddress address, ushort port, Action<List<KeyValuePair<string, string>>> onSuccess, Action onFailure)
		{
			ServerList.<>c__DisplayClass23_0 CS$<>8__locals1 = new ServerList.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.onSuccess = onSuccess;
			CS$<>8__locals1.onFailure = onFailure;
			CS$<>8__locals1.response = null;
			CS$<>8__locals1.handle = default(HServerQuery);
			CS$<>8__locals1.response = new ServerList.SteamMatchmakingRulesResponse(new Action<List<KeyValuePair<string, string>>>(CS$<>8__locals1.<RequestServerRules>g__OnSuccess|1), new Action(CS$<>8__locals1.<RequestServerRules>g__OnFailure|2));
			CS$<>8__locals1.handle = this.client.native.servers.ServerRules(address.IpToInt32(), port, CS$<>8__locals1.response.nativePtr);
			this.liveRequests.Add(new ServerList.SteamMatchmakingServersResponseRequest
			{
				steamMatchmakingRulesResponse = CS$<>8__locals1.response,
				queryHandle = CS$<>8__locals1.handle
			});
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00037AF4 File Offset: 0x00035CF4
		public void RequestServerPlayers(IPAddress address, ushort port, Action<List<ServerList.PlayerInfo>> onSuccess, Action onFailure)
		{
			ServerList.<>c__DisplayClass24_0 CS$<>8__locals1 = new ServerList.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.onSuccess = onSuccess;
			CS$<>8__locals1.onFailure = onFailure;
			CS$<>8__locals1.response = null;
			CS$<>8__locals1.handle = default(HServerQuery);
			CS$<>8__locals1.response = new ServerList.SteamMatchmakingPlayersResponse(new Action<List<ServerList.PlayerInfo>>(CS$<>8__locals1.<RequestServerPlayers>g__OnSuccess|1), new Action(CS$<>8__locals1.<RequestServerPlayers>g__OnFailure|2));
			CS$<>8__locals1.handle = this.client.native.servers.PlayerDetails(address.IpToInt32(), port, CS$<>8__locals1.response.nativePtr);
			this.liveRequests.Add(new ServerList.SteamMatchmakingServersResponseRequest
			{
				steamMatchmakingRulesResponse = CS$<>8__locals1.response,
				queryHandle = CS$<>8__locals1.handle
			});
		}

		// Token: 0x0400082F RID: 2095
		internal Client client;

		// Token: 0x04000830 RID: 2096
		private HashSet<ulong> FavouriteHash = new HashSet<ulong>();

		// Token: 0x04000831 RID: 2097
		private HashSet<ulong> HistoryHash = new HashSet<ulong>();

		// Token: 0x04000832 RID: 2098
		private List<ServerList.SteamMatchmakingServersResponseRequest> liveRequests = new List<ServerList.SteamMatchmakingServersResponseRequest>();

		// Token: 0x0200027C RID: 636
		public class Filter : List<KeyValuePair<string, string>>
		{
			// Token: 0x06001E0E RID: 7694 RVA: 0x00064C4A File Offset: 0x00062E4A
			public void Add(string k, string v)
			{
				base.Add(new KeyValuePair<string, string>(k, v));
			}

			// Token: 0x06001E0F RID: 7695 RVA: 0x00064C5C File Offset: 0x00062E5C
			internal void Start()
			{
				MatchMakingKeyValuePair_t[] array = this.Select(delegate(KeyValuePair<string, string> x)
				{
					if (x.Key == "appid")
					{
						this.AppId = int.Parse(x.Value);
					}
					return new MatchMakingKeyValuePair_t
					{
						Key = x.Key,
						Value = x.Value
					};
				}).ToArray<MatchMakingKeyValuePair_t>();
				int num = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
				this.NativeArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * array.Length);
				this.m_pArrayEntries = Marshal.AllocHGlobal(num * array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					Marshal.StructureToPtr<MatchMakingKeyValuePair_t>(array[i], new IntPtr(this.m_pArrayEntries.ToInt64() + (long)(i * num)), false);
				}
				Marshal.WriteIntPtr(this.NativeArray, this.m_pArrayEntries);
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x00064D00 File Offset: 0x00062F00
			internal void Free()
			{
				if (this.m_pArrayEntries != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.m_pArrayEntries);
				}
				if (this.NativeArray != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.NativeArray);
				}
			}

			// Token: 0x06001E11 RID: 7697 RVA: 0x00064D3C File Offset: 0x00062F3C
			internal bool Test(gameserveritem_t info)
			{
				return this.AppId == 0 || (long)this.AppId == (long)((ulong)info.AppID);
			}

			// Token: 0x04000BF7 RID: 3063
			internal IntPtr NativeArray;

			// Token: 0x04000BF8 RID: 3064
			private IntPtr m_pArrayEntries;

			// Token: 0x04000BF9 RID: 3065
			private int AppId;
		}

		// Token: 0x0200027D RID: 637
		private struct MatchPair
		{
			// Token: 0x04000BFA RID: 3066
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string key;

			// Token: 0x04000BFB RID: 3067
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string value;
		}

		// Token: 0x0200027E RID: 638
		public struct PlayerInfo
		{
			// Token: 0x04000BFC RID: 3068
			public byte index;

			// Token: 0x04000BFD RID: 3069
			public string name;

			// Token: 0x04000BFE RID: 3070
			public long score;

			// Token: 0x04000BFF RID: 3071
			public float duration;
		}

		// Token: 0x0200027F RID: 639
		public class Request : IDisposable
		{
			// Token: 0x06001E14 RID: 7700 RVA: 0x00064DBC File Offset: 0x00062FBC
			internal Request(Client c)
			{
				this.client = c;
				this.client.OnUpdate += this.Update;
			}

			// Token: 0x06001E15 RID: 7701 RVA: 0x00064E10 File Offset: 0x00063010
			~Request()
			{
				this.Dispose();
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00064E3C File Offset: 0x0006303C
			// (set) Token: 0x06001E17 RID: 7703 RVA: 0x00064E44 File Offset: 0x00063044
			internal IEnumerable<string> ServerList { get; set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00064E4D File Offset: 0x0006304D
			// (set) Token: 0x06001E19 RID: 7705 RVA: 0x00064E55 File Offset: 0x00063055
			internal ServerList.Filter Filter { get; set; }

			// Token: 0x06001E1A RID: 7706 RVA: 0x00064E60 File Offset: 0x00063060
			internal void StartCustomQuery()
			{
				if (this.ServerList == null)
				{
					return;
				}
				int count = 16;
				int num = 0;
				for (;;)
				{
					IEnumerable<string> enumerable = this.ServerList.Skip(num).Take(count);
					if (enumerable.Count<string>() == 0)
					{
						break;
					}
					num += enumerable.Count<string>();
					ServerList.Filter filter = new ServerList.Filter();
					filter.Add("or", enumerable.Count<string>().ToString());
					foreach (string v in enumerable)
					{
						filter.Add("gameaddr", v);
					}
					filter.Start();
					HServerListRequest value = this.client.native.servers.RequestInternetServerList(this.client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero);
					filter.Free();
					this.AddRequest(value);
				}
				this.ServerList = null;
			}

			// Token: 0x06001E1B RID: 7707 RVA: 0x00064F68 File Offset: 0x00063168
			internal void AddRequest(IntPtr id)
			{
				this.Requests.Add(new ServerList.Request.SubRequest
				{
					Request = id
				});
			}

			// Token: 0x06001E1C RID: 7708 RVA: 0x00064F84 File Offset: 0x00063184
			private void Update()
			{
				if (this.Requests.Count == 0)
				{
					return;
				}
				for (int i = 0; i < this.Requests.Count<ServerList.Request.SubRequest>(); i++)
				{
					if (this.Requests[i].Update(this.client.native.servers, new Action<gameserveritem_t>(this.OnServer), this.OnUpdate))
					{
						this.Requests.RemoveAt(i);
						i--;
					}
				}
				if (this.Requests.Count == 0)
				{
					this.Finished = true;
					this.client.OnUpdate -= this.Update;
					Action onFinished = this.OnFinished;
					if (onFinished == null)
					{
						return;
					}
					onFinished();
				}
			}

			// Token: 0x06001E1D RID: 7709 RVA: 0x00065038 File Offset: 0x00063238
			private void OnServer(gameserveritem_t info)
			{
				if (!info.HadSuccessfulResponse)
				{
					this.Unresponsive.Add(Facepunch.Steamworks.ServerList.Server.FromSteam(this.client, info));
					return;
				}
				if (this.Filter != null && !this.Filter.Test(info))
				{
					return;
				}
				ServerList.Server server = Facepunch.Steamworks.ServerList.Server.FromSteam(this.client, info);
				this.Responded.Add(server);
				Action<ServerList.Server> onServerResponded = this.OnServerResponded;
				if (onServerResponded == null)
				{
					return;
				}
				onServerResponded(server);
			}

			// Token: 0x06001E1E RID: 7710 RVA: 0x000650A8 File Offset: 0x000632A8
			public void Dispose()
			{
				if (this.client.IsValid)
				{
					this.client.OnUpdate -= this.Update;
				}
				foreach (ServerList.Request.SubRequest subRequest in this.Requests)
				{
					if (this.client.IsValid)
					{
						this.client.native.servers.CancelQuery(subRequest.Request);
					}
				}
				this.Requests.Clear();
			}

			// Token: 0x04000C00 RID: 3072
			internal Client client;

			// Token: 0x04000C01 RID: 3073
			internal List<ServerList.Request.SubRequest> Requests = new List<ServerList.Request.SubRequest>();

			// Token: 0x04000C02 RID: 3074
			public Action OnUpdate;

			// Token: 0x04000C03 RID: 3075
			public Action<ServerList.Server> OnServerResponded;

			// Token: 0x04000C04 RID: 3076
			public Action OnFinished;

			// Token: 0x04000C05 RID: 3077
			public List<ServerList.Server> Responded = new List<ServerList.Server>();

			// Token: 0x04000C06 RID: 3078
			public List<ServerList.Server> Unresponsive = new List<ServerList.Server>();

			// Token: 0x04000C07 RID: 3079
			public bool Finished;

			// Token: 0x020002BB RID: 699
			internal class SubRequest
			{
				// Token: 0x06002D65 RID: 11621 RVA: 0x000680F0 File Offset: 0x000662F0
				internal bool Update(SteamMatchmakingServers servers, Action<gameserveritem_t> OnServer, Action OnUpdate)
				{
					if (this.Request == IntPtr.Zero)
					{
						return true;
					}
					if (this.Timer.Elapsed.TotalSeconds < 0.5)
					{
						return false;
					}
					this.Timer.Reset();
					this.Timer.Start();
					bool changes = false;
					int serverCount = servers.GetServerCount(this.Request);
					if (serverCount != this.Pointer)
					{
						for (int i = this.Pointer; i < serverCount; i++)
						{
							this.WatchList.Add(i);
						}
					}
					this.Pointer = serverCount;
					this.WatchList.RemoveAll(delegate(int x)
					{
						gameserveritem_t serverDetails = servers.GetServerDetails(this.Request, x);
						if (serverDetails.HadSuccessfulResponse)
						{
							OnServer(serverDetails);
							changes = true;
							return true;
						}
						return false;
					});
					if (!servers.IsRefreshing(this.Request))
					{
						this.WatchList.RemoveAll(delegate(int x)
						{
							gameserveritem_t serverDetails = servers.GetServerDetails(this.Request, x);
							OnServer(serverDetails);
							return true;
						});
						servers.CancelQuery(this.Request);
						this.Request = IntPtr.Zero;
						changes = true;
					}
					if (changes && OnUpdate != null)
					{
						OnUpdate();
					}
					return this.Request == IntPtr.Zero;
				}

				// Token: 0x04000D40 RID: 3392
				internal IntPtr Request;

				// Token: 0x04000D41 RID: 3393
				internal int Pointer;

				// Token: 0x04000D42 RID: 3394
				internal List<int> WatchList = new List<int>();

				// Token: 0x04000D43 RID: 3395
				internal Stopwatch Timer = Stopwatch.StartNew();
			}
		}

		// Token: 0x02000280 RID: 640
		public class Server
		{
			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00065150 File Offset: 0x00063350
			// (set) Token: 0x06001E20 RID: 7712 RVA: 0x00065158 File Offset: 0x00063358
			public string Name { get; set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00065161 File Offset: 0x00063361
			// (set) Token: 0x06001E22 RID: 7714 RVA: 0x00065169 File Offset: 0x00063369
			public int Ping { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00065172 File Offset: 0x00063372
			// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0006517A File Offset: 0x0006337A
			public string GameDir { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00065183 File Offset: 0x00063383
			// (set) Token: 0x06001E26 RID: 7718 RVA: 0x0006518B File Offset: 0x0006338B
			public string Map { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00065194 File Offset: 0x00063394
			// (set) Token: 0x06001E28 RID: 7720 RVA: 0x0006519C File Offset: 0x0006339C
			public string Description { get; set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000651A5 File Offset: 0x000633A5
			// (set) Token: 0x06001E2A RID: 7722 RVA: 0x000651AD File Offset: 0x000633AD
			public uint AppId { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000651B6 File Offset: 0x000633B6
			// (set) Token: 0x06001E2C RID: 7724 RVA: 0x000651BE File Offset: 0x000633BE
			public int Players { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000651C7 File Offset: 0x000633C7
			// (set) Token: 0x06001E2E RID: 7726 RVA: 0x000651CF File Offset: 0x000633CF
			public int MaxPlayers { get; set; }

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06001E2F RID: 7727 RVA: 0x000651D8 File Offset: 0x000633D8
			// (set) Token: 0x06001E30 RID: 7728 RVA: 0x000651E0 File Offset: 0x000633E0
			public int BotPlayers { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000651E9 File Offset: 0x000633E9
			// (set) Token: 0x06001E32 RID: 7730 RVA: 0x000651F1 File Offset: 0x000633F1
			public bool Passworded { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06001E33 RID: 7731 RVA: 0x000651FA File Offset: 0x000633FA
			// (set) Token: 0x06001E34 RID: 7732 RVA: 0x00065202 File Offset: 0x00063402
			public bool Secure { get; set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0006520B File Offset: 0x0006340B
			// (set) Token: 0x06001E36 RID: 7734 RVA: 0x00065213 File Offset: 0x00063413
			public uint LastTimePlayed { get; set; }

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0006521C File Offset: 0x0006341C
			// (set) Token: 0x06001E38 RID: 7736 RVA: 0x00065224 File Offset: 0x00063424
			public int Version { get; set; }

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0006522D File Offset: 0x0006342D
			// (set) Token: 0x06001E3A RID: 7738 RVA: 0x00065235 File Offset: 0x00063435
			public string[] Tags { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0006523E File Offset: 0x0006343E
			// (set) Token: 0x06001E3C RID: 7740 RVA: 0x00065246 File Offset: 0x00063446
			public ulong SteamId { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0006524F File Offset: 0x0006344F
			// (set) Token: 0x06001E3E RID: 7742 RVA: 0x00065257 File Offset: 0x00063457
			public IPAddress Address { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00065260 File Offset: 0x00063460
			// (set) Token: 0x06001E40 RID: 7744 RVA: 0x00065268 File Offset: 0x00063468
			public int ConnectionPort { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00065271 File Offset: 0x00063471
			// (set) Token: 0x06001E42 RID: 7746 RVA: 0x00065279 File Offset: 0x00063479
			public int QueryPort { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06001E43 RID: 7747 RVA: 0x00065282 File Offset: 0x00063482
			public bool Favourite
			{
				get
				{
					return this.Client.ServerList.IsFavourite(this);
				}
			}

			// Token: 0x06001E44 RID: 7748 RVA: 0x00065298 File Offset: 0x00063498
			internal static ServerList.Server FromSteam(Client client, gameserveritem_t item)
			{
				return new ServerList.Server
				{
					Client = client,
					Address = Utility.Int32ToIp(item.NetAdr.IP),
					ConnectionPort = (int)item.NetAdr.ConnectionPort,
					QueryPort = (int)item.NetAdr.QueryPort,
					Name = item.ServerName,
					Ping = item.Ping,
					GameDir = item.GameDir,
					Map = item.Map,
					Description = item.GameDescription,
					AppId = item.AppID,
					Players = item.Players,
					MaxPlayers = item.MaxPlayers,
					BotPlayers = item.BotPlayers,
					Passworded = item.Password,
					Secure = item.Secure,
					LastTimePlayed = item.TimeLastPlayed,
					Version = item.ServerVersion,
					Tags = ((item.GameTags == null) ? null : item.GameTags.Split(new char[]
					{
						','
					})),
					SteamId = item.SteamID
				};
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000653BA File Offset: 0x000635BA
			public bool HasRules
			{
				get
				{
					return this.Rules != null && this.Rules.Count > 0;
				}
			}

			// Token: 0x06001E46 RID: 7750 RVA: 0x000653D4 File Offset: 0x000635D4
			public void FetchRules()
			{
				if (this.fetchingRules)
				{
					return;
				}
				this.fetchingRules = true;
				this.Rules = null;
				this.Client.ServerList.RequestServerRules(this.Address, (ushort)this.QueryPort, delegate(List<KeyValuePair<string, string>> keyValues)
				{
					this.Rules = new Dictionary<string, string>();
					foreach (KeyValuePair<string, string> keyValuePair in keyValues)
					{
						this.Rules.Add(keyValuePair.Key, keyValuePair.Value);
					}
					this.OnReceivedRules(true);
					this.fetchingRules = false;
				}, delegate
				{
					this.OnReceivedRules(false);
					this.fetchingRules = false;
				});
			}

			// Token: 0x06001E47 RID: 7751 RVA: 0x0006542D File Offset: 0x0006362D
			internal void OnServerRulesReceiveFinished(Dictionary<string, string> rules, bool Success)
			{
				this.RulesRequest = null;
				if (Success)
				{
					this.Rules = rules;
				}
				if (this.OnReceivedRules != null)
				{
					this.OnReceivedRules(Success);
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00065454 File Offset: 0x00063654
			public bool HasPlayerInfos
			{
				get
				{
					return this.PlayerInfos != null && this.PlayerInfos.Count > 0;
				}
			}

			// Token: 0x06001E49 RID: 7753 RVA: 0x00065470 File Offset: 0x00063670
			public void FetchPlayerInfos()
			{
				if (this.fetchingPlayerInfos)
				{
					return;
				}
				this.fetchingPlayerInfos = true;
				this.PlayerInfos = null;
				this.Client.ServerList.RequestServerPlayers(this.Address, (ushort)this.QueryPort, delegate(List<ServerList.PlayerInfo> playerInfos)
				{
					this.PlayerInfos = playerInfos;
					this.fetchingPlayerInfos = false;
					this.OnReceivedPlayerInfos(true);
				}, delegate
				{
					this.fetchingPlayerInfos = false;
					this.OnReceivedPlayerInfos(false);
				});
			}

			// Token: 0x06001E4A RID: 7754 RVA: 0x000654C9 File Offset: 0x000636C9
			public void OnServerPlayerInfosReceiveFinished(List<ServerList.PlayerInfo> players, bool Success)
			{
				this.PlayerInfosRequest = null;
				if (Success)
				{
					this.PlayerInfos = players;
				}
				if (this.OnReceivedPlayerInfos != null)
				{
					this.OnReceivedPlayerInfos(Success);
				}
			}

			// Token: 0x06001E4B RID: 7755 RVA: 0x000654F0 File Offset: 0x000636F0
			public void AddToHistory()
			{
				this.Client.native.matchmaking.AddFavoriteGame(this.AppId, this.Address.IpToInt32(), (ushort)this.ConnectionPort, (ushort)this.QueryPort, 2U, (uint)Utility.Epoch.Current);
				this.Client.ServerList.UpdateFavouriteList();
			}

			// Token: 0x06001E4C RID: 7756 RVA: 0x00065550 File Offset: 0x00063750
			public void RemoveFromHistory()
			{
				this.Client.native.matchmaking.RemoveFavoriteGame(this.AppId, this.Address.IpToInt32(), (ushort)this.ConnectionPort, (ushort)this.QueryPort, 2U);
				this.Client.ServerList.UpdateFavouriteList();
			}

			// Token: 0x06001E4D RID: 7757 RVA: 0x000655A8 File Offset: 0x000637A8
			public void AddToFavourites()
			{
				this.Client.native.matchmaking.AddFavoriteGame(this.AppId, this.Address.IpToInt32(), (ushort)this.ConnectionPort, (ushort)this.QueryPort, 1U, (uint)Utility.Epoch.Current);
				this.Client.ServerList.UpdateFavouriteList();
			}

			// Token: 0x06001E4E RID: 7758 RVA: 0x00065608 File Offset: 0x00063808
			public void RemoveFromFavourites()
			{
				this.Client.native.matchmaking.RemoveFavoriteGame(this.AppId, this.Address.IpToInt32(), (ushort)this.ConnectionPort, (ushort)this.QueryPort, 1U);
				this.Client.ServerList.UpdateFavouriteList();
			}

			// Token: 0x04000C1C RID: 3100
			internal Client Client;

			// Token: 0x04000C1D RID: 3101
			public Action<bool> OnReceivedRules;

			// Token: 0x04000C1E RID: 3102
			public Dictionary<string, string> Rules;

			// Token: 0x04000C1F RID: 3103
			internal SourceServerQuery RulesRequest;

			// Token: 0x04000C20 RID: 3104
			private bool fetchingRules;

			// Token: 0x04000C21 RID: 3105
			public Action<bool> OnReceivedPlayerInfos;

			// Token: 0x04000C22 RID: 3106
			public List<ServerList.PlayerInfo> PlayerInfos;

			// Token: 0x04000C23 RID: 3107
			internal SourceServerQuery PlayerInfosRequest;

			// Token: 0x04000C24 RID: 3108
			private bool fetchingPlayerInfos;

			// Token: 0x04000C25 RID: 3109
			internal const uint k_unFavoriteFlagNone = 0U;

			// Token: 0x04000C26 RID: 3110
			internal const uint k_unFavoriteFlagFavorite = 1U;

			// Token: 0x04000C27 RID: 3111
			internal const uint k_unFavoriteFlagHistory = 2U;
		}

		// Token: 0x02000281 RID: 641
		private struct SteamMatchmakingServersResponseRequest
		{
			// Token: 0x04000C28 RID: 3112
			public ServerList.BaseSteamMatchmakingServersResponse steamMatchmakingRulesResponse;

			// Token: 0x04000C29 RID: 3113
			public HServerQuery queryHandle;
		}

		// Token: 0x02000282 RID: 642
		internal abstract class BaseSteamMatchmakingServersResponse : IDisposable
		{
			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06001E54 RID: 7764 RVA: 0x0006572E File Offset: 0x0006392E
			// (set) Token: 0x06001E55 RID: 7765 RVA: 0x00065736 File Offset: 0x00063936
			public IntPtr nativePtr { get; private set; }

			// Token: 0x06001E56 RID: 7766 RVA: 0x0006573F File Offset: 0x0006393F
			public virtual void Dispose()
			{
				if (this.nativePtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.nativePtr);
					this.nativePtr = IntPtr.Zero;
				}
			}

			// Token: 0x06001E57 RID: 7767 RVA: 0x00065769 File Offset: 0x00063969
			protected void AddVirtualMethod(Delegate d)
			{
				this.virtualMethods.Add(d);
			}

			// Token: 0x06001E58 RID: 7768 RVA: 0x00065778 File Offset: 0x00063978
			protected void FinalizeVirtualMethodTable()
			{
				this.nativePtr = Marshal.AllocHGlobal(IntPtr.Size * (1 + this.virtualMethods.Count));
				IntPtr intPtr = ServerList.BaseSteamMatchmakingServersResponse.<FinalizeVirtualMethodTable>g__AddIntPtr|7_0(this.nativePtr, IntPtr.Size);
				Marshal.WriteIntPtr(this.nativePtr, intPtr);
				for (int i = 0; i < this.virtualMethods.Count; i++)
				{
					Marshal.WriteIntPtr(ServerList.BaseSteamMatchmakingServersResponse.<FinalizeVirtualMethodTable>g__AddIntPtr|7_0(intPtr, IntPtr.Size * i), Marshal.GetFunctionPointerForDelegate(this.virtualMethods[i]));
				}
			}

			// Token: 0x06001E59 RID: 7769 RVA: 0x000657FC File Offset: 0x000639FC
			protected unsafe static string IntPtrToString(IntPtr ptr)
			{
				byte* ptr2 = (byte*)((void*)ptr);
				int num = 0;
				while (ptr2[num] != 0)
				{
					num++;
				}
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ptr2[i];
				}
				return Encoding.UTF8.GetString(array);
			}

			// Token: 0x06001E5B RID: 7771 RVA: 0x00065856 File Offset: 0x00063A56
			[CompilerGenerated]
			internal static IntPtr <FinalizeVirtualMethodTable>g__AddIntPtr|7_0(IntPtr intPtr, int offset)
			{
				return new IntPtr(intPtr.ToInt64() + (long)offset);
			}

			// Token: 0x04000C2B RID: 3115
			private List<Delegate> virtualMethods = new List<Delegate>();
		}

		// Token: 0x02000283 RID: 643
		internal class SteamMatchmakingRulesResponse : ServerList.BaseSteamMatchmakingServersResponse
		{
			// Token: 0x06001E5C RID: 7772 RVA: 0x00065868 File Offset: 0x00063A68
			public SteamMatchmakingRulesResponse(Action<List<KeyValuePair<string, string>>> onComplete, Action onServerFailedToRespond)
			{
				ServerList.SteamMatchmakingRulesResponse <>4__this = this;
				if (onServerFailedToRespond == null || onComplete == null)
				{
					throw new ArgumentNullException();
				}
				base.AddVirtualMethod(new ServerList.SteamMatchmakingRulesResponse.InternalRulesResponded(delegate(IntPtr thisptr, IntPtr rule, IntPtr value)
				{
					<>4__this.receivedKeyValues.Add(new KeyValuePair<string, string>(ServerList.BaseSteamMatchmakingServersResponse.IntPtrToString(rule), ServerList.BaseSteamMatchmakingServersResponse.IntPtrToString(value)));
				}));
				base.AddVirtualMethod(new ServerList.SteamMatchmakingRulesResponse.InternalRulesFailedToRespond(delegate(IntPtr thisptr)
				{
					onServerFailedToRespond();
				}));
				base.AddVirtualMethod(new ServerList.SteamMatchmakingRulesResponse.InternalRulesRefreshComplete(delegate(IntPtr thisptr)
				{
					onComplete(<>4__this.receivedKeyValues);
				}));
				base.FinalizeVirtualMethodTable();
			}

			// Token: 0x04000C2C RID: 3116
			private readonly List<KeyValuePair<string, string>> receivedKeyValues = new List<KeyValuePair<string, string>>();

			// Token: 0x020002BC RID: 700
			// (Invoke) Token: 0x06002D68 RID: 11624
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalRulesResponded(IntPtr thisptr, IntPtr pchRule, IntPtr pchValue);

			// Token: 0x020002BD RID: 701
			// (Invoke) Token: 0x06002D6C RID: 11628
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalRulesFailedToRespond(IntPtr thisptr);

			// Token: 0x020002BE RID: 702
			// (Invoke) Token: 0x06002D70 RID: 11632
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalRulesRefreshComplete(IntPtr thisptr);
		}

		// Token: 0x02000284 RID: 644
		internal class SteamMatchmakingPlayersResponse : ServerList.BaseSteamMatchmakingServersResponse
		{
			// Token: 0x06001E5D RID: 7773 RVA: 0x000658F4 File Offset: 0x00063AF4
			public SteamMatchmakingPlayersResponse(Action<List<ServerList.PlayerInfo>> onSuccess, Action onFailure)
			{
				ServerList.SteamMatchmakingPlayersResponse <>4__this = this;
				if (onSuccess == null || onFailure == null)
				{
					throw new ArgumentNullException();
				}
				base.AddVirtualMethod(new ServerList.SteamMatchmakingPlayersResponse.InternalAddPlayerToList(delegate(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed)
				{
					<>4__this.receivedPlayerDetails.Add(new ServerList.PlayerInfo
					{
						name = ServerList.BaseSteamMatchmakingServersResponse.IntPtrToString(pchName),
						score = (long)nScore,
						duration = flTimePlayed
					});
				}));
				base.AddVirtualMethod(new ServerList.SteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond(delegate(IntPtr thisptr)
				{
					onFailure();
				}));
				base.AddVirtualMethod(new ServerList.SteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete(delegate(IntPtr thisptr)
				{
					onSuccess(<>4__this.receivedPlayerDetails);
				}));
				base.FinalizeVirtualMethodTable();
			}

			// Token: 0x04000C2D RID: 3117
			private List<ServerList.PlayerInfo> receivedPlayerDetails = new List<ServerList.PlayerInfo>();

			// Token: 0x020002C0 RID: 704
			// (Invoke) Token: 0x06002D78 RID: 11640
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalAddPlayerToList(IntPtr thisptr, IntPtr pchName, int nScore, float flTimePlayed);

			// Token: 0x020002C1 RID: 705
			// (Invoke) Token: 0x06002D7C RID: 11644
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalPlayersFailedToRespond(IntPtr thisptr);

			// Token: 0x020002C2 RID: 706
			// (Invoke) Token: 0x06002D80 RID: 11648
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			private delegate void InternalPlayersRefreshComplete(IntPtr thisptr);
		}
	}
}
