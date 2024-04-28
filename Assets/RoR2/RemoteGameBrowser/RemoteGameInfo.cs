using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using RoR2.Networking;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AE3 RID: 2787
	public struct RemoteGameInfo
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x00108DF6 File Offset: 0x00106FF6
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x00108DFE File Offset: 0x00106FFE
		[CanBeNull]
		public string[] tags { get; private set; }

		// Token: 0x0600400A RID: 16394 RVA: 0x00108E07 File Offset: 0x00107007
		public bool IsLobbyIdValid()
		{
			return (this.lobbyIdStr != null && this.lobbyIdStr != string.Empty) || this.lobbyId != null;
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x00108E30 File Offset: 0x00107030
		public bool IsServerIdValid()
		{
			return (this.serverIdStr != null && this.lobbyIdStr != string.Empty) || this.serverId != null;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x00108E59 File Offset: 0x00107059
		public void GetPlayers(List<RemotePlayerInfo> output)
		{
			RemoteGameInfo.GetPlayersDelegate getPlayersDelegate = this.getPlayersImplementation;
			if (getPlayersDelegate == null)
			{
				return;
			}
			getPlayersDelegate(this, output);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x00108E6D File Offset: 0x0010706D
		public bool GetRuleBook(RuleBook dest)
		{
			RemoteGameInfo.GetRuleBookDelegate getRuleBookDelegate = this.getRuleBookImplementation;
			return getRuleBookDelegate != null && getRuleBookDelegate(this, dest);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x00108E82 File Offset: 0x00107082
		public void RequestRefresh([CanBeNull] RemoteGameInfo.RequestRefreshSuccessCallback successCallback, [CanBeNull] Action failureCallback, bool fetchDetails)
		{
			if (this.requestRefreshImplementation == null)
			{
				if (failureCallback != null)
				{
					failureCallback();
				}
				return;
			}
			this.requestRefreshImplementation(this, successCallback, failureCallback, fetchDetails);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x00108EA8 File Offset: 0x001070A8
		public void SetTags(string[] newTags)
		{
			this.tags = newTags;
			if (this.tags != null)
			{
				for (int i = 0; i < this.tags.Length; i++)
				{
					string a = this.tags[i];
					if (!(a == "dz"))
					{
						if (!(a == "rs"))
						{
							if (a == "mn")
							{
								this.currentDifficultyIndex = new DifficultyIndex?(DifficultyIndex.Hard);
							}
						}
						else
						{
							this.currentDifficultyIndex = new DifficultyIndex?(DifficultyIndex.Normal);
						}
					}
					else
					{
						this.currentDifficultyIndex = new DifficultyIndex?(DifficultyIndex.Easy);
					}
				}
			}
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x00108F34 File Offset: 0x00107134
		public void CalcExtraFields()
		{
			this.lesserPlayerCount = int.MinValue;
			this.greaterPlayerCount = int.MaxValue;
			this.lesserMaxPlayers = int.MinValue;
			this.greaterMaxPlayers = int.MaxValue;
			RemoteGameInfo.<CalcExtraFields>g__Pick|51_0(this.lobbyPlayerCount, ref this.lesserPlayerCount, ref this.greaterPlayerCount);
			RemoteGameInfo.<CalcExtraFields>g__Pick|51_0(this.serverPlayerCount, ref this.lesserPlayerCount, ref this.greaterPlayerCount);
			RemoteGameInfo.<CalcExtraFields>g__Pick|51_0(this.lobbyMaxPlayers, ref this.lesserMaxPlayers, ref this.greaterMaxPlayers);
			RemoteGameInfo.<CalcExtraFields>g__Pick|51_0(this.serverMaxPlayers, ref this.lesserMaxPlayers, ref this.greaterMaxPlayers);
			if (this.lesserPlayerCount == -2147483648)
			{
				this.lesserPlayerCount = 0;
			}
			if (this.greaterPlayerCount == 2147483647)
			{
				this.greaterPlayerCount = 0;
			}
			if (this.lesserMaxPlayers == -2147483648)
			{
				this.lesserMaxPlayers = 0;
			}
			if (this.greaterMaxPlayers == 2147483647)
			{
				this.greaterMaxPlayers = 0;
			}
			this.availableSlots = int.MaxValue;
			int? num = this.lobbyMaxPlayers - this.lobbyPlayerCount;
			int? num2 = this.serverMaxPlayers - this.serverPlayerCount;
			if (num != null)
			{
				this.availableSlots = Math.Min(this.availableSlots, num.Value);
			}
			if (num2 != null)
			{
				this.availableSlots = Math.Min(this.availableSlots, num2.Value);
			}
			this.availableLobbySlots = ((this.lobbyMaxPlayers - this.lobbyPlayerCount) ?? 0);
			this.availableServerSlots = ((this.serverMaxPlayers - this.serverPlayerCount) ?? 0);
			if (this.availableSlots == 2147483647)
			{
				this.availableSlots = 0;
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x001091AE File Offset: 0x001073AE
		public bool HasTag(string tag)
		{
			return this.tags != null && Array.IndexOf<string>(this.tags, tag) != -1;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x001091CC File Offset: 0x001073CC
		public RemoteGameInfo.ArtifactEnumerable GetEnabledArtifacts()
		{
			RemoteGameInfo.ArtifactEnumerable result = new RemoteGameInfo.ArtifactEnumerable("");
			if (this.tags != null)
			{
				for (int i = 0; i < this.tags.Length; i++)
				{
					if (this.tags[i].StartsWith("a="))
					{
						result = new RemoteGameInfo.ArtifactEnumerable(this.tags[i]);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00109226 File Offset: 0x00107426
		[CompilerGenerated]
		internal static void <CalcExtraFields>g__Pick|51_0(int? inputValue, ref int lesserResult, ref int greaterResult)
		{
			if (inputValue != null)
			{
				lesserResult = Math.Max(lesserResult, inputValue.Value);
				greaterResult = Math.Min(greaterResult, inputValue.Value);
			}
		}

		// Token: 0x04003E5E RID: 15966
		public float retrievalTime;

		// Token: 0x04003E5F RID: 15967
		[CanBeNull]
		public string name;

		// Token: 0x04003E60 RID: 15968
		[CanBeNull]
		public string modHash;

		// Token: 0x04003E61 RID: 15969
		[CanBeNull]
		public string buildId;

		// Token: 0x04003E62 RID: 15970
		[CanBeNull]
		public string serverName;

		// Token: 0x04003E63 RID: 15971
		[CanBeNull]
		public string lobbyName;

		// Token: 0x04003E64 RID: 15972
		public string lobbyIdStr;

		// Token: 0x04003E65 RID: 15973
		public ulong? lobbyId;

		// Token: 0x04003E66 RID: 15974
		public string serverIdStr;

		// Token: 0x04003E67 RID: 15975
		public ulong? serverId;

		// Token: 0x04003E68 RID: 15976
		public AddressPortPair? serverAddress;

		// Token: 0x04003E69 RID: 15977
		public int? ping;

		// Token: 0x04003E6A RID: 15978
		public string currentSceneName;

		// Token: 0x04003E6B RID: 15979
		public SceneIndex? currentSceneIndex;

		// Token: 0x04003E6C RID: 15980
		public DifficultyIndex? currentDifficultyIndex;

		// Token: 0x04003E6D RID: 15981
		public int? lobbyPlayerCount;

		// Token: 0x04003E6E RID: 15982
		public int? lobbyMaxPlayers;

		// Token: 0x04003E6F RID: 15983
		public int? serverPlayerCount;

		// Token: 0x04003E70 RID: 15984
		public int? serverMaxPlayers;

		// Token: 0x04003E71 RID: 15985
		public bool? hasPassword;

		// Token: 0x04003E73 RID: 15987
		[CanBeNull]
		public string gameModeName;

		// Token: 0x04003E74 RID: 15988
		public bool? inGame;

		// Token: 0x04003E75 RID: 15989
		public bool? joinable;

		// Token: 0x04003E76 RID: 15990
		public bool? isBlacklisted;

		// Token: 0x04003E77 RID: 15991
		public bool? isFavorite;

		// Token: 0x04003E78 RID: 15992
		public bool? didRespond;

		// Token: 0x04003E79 RID: 15993
		[CanBeNull]
		public object userData;

		// Token: 0x04003E7A RID: 15994
		[CanBeNull]
		public RemoteGameInfo.GetPlayersDelegate getPlayersImplementation;

		// Token: 0x04003E7B RID: 15995
		[CanBeNull]
		public RemoteGameInfo.RequestRefreshDelegate requestRefreshImplementation;

		// Token: 0x04003E7C RID: 15996
		[CanBeNull]
		public RemoteGameInfo.GetRuleBookDelegate getRuleBookImplementation;

		// Token: 0x04003E7D RID: 15997
		public int availableSlots;

		// Token: 0x04003E7E RID: 15998
		public int availableLobbySlots;

		// Token: 0x04003E7F RID: 15999
		public int availableServerSlots;

		// Token: 0x04003E80 RID: 16000
		public int lesserPlayerCount;

		// Token: 0x04003E81 RID: 16001
		public int greaterPlayerCount;

		// Token: 0x04003E82 RID: 16002
		public int lesserMaxPlayers;

		// Token: 0x04003E83 RID: 16003
		public int greaterMaxPlayers;

		// Token: 0x02000AE4 RID: 2788
		// (Invoke) Token: 0x06004015 RID: 16405
		public delegate void GetPlayersDelegate(in RemoteGameInfo remoteGameInfo, [NotNull] List<RemotePlayerInfo> output);

		// Token: 0x02000AE5 RID: 2789
		// (Invoke) Token: 0x06004019 RID: 16409
		public delegate void RequestRefreshDelegate(in RemoteGameInfo remoteGameInfo, [CanBeNull] RemoteGameInfo.RequestRefreshSuccessCallback successCallback, [CanBeNull] Action failureCallback, bool fetchDetails);

		// Token: 0x02000AE6 RID: 2790
		// (Invoke) Token: 0x0600401D RID: 16413
		public delegate void RequestRefreshSuccessCallback(in RemoteGameInfo remoteGameInfo);

		// Token: 0x02000AE7 RID: 2791
		// (Invoke) Token: 0x06004021 RID: 16417
		public delegate bool GetRuleBookDelegate(in RemoteGameInfo remoteGameInfo, RuleBook dest);

		// Token: 0x02000AE8 RID: 2792
		public struct ArtifactEnumerator : IEnumerator<ArtifactDef>, IEnumerator, IDisposable
		{
			// Token: 0x06004024 RID: 16420 RVA: 0x00109251 File Offset: 0x00107451
			public ArtifactEnumerator(string tagSource)
			{
				this.tagSource = tagSource;
				this.readPos = 1;
			}

			// Token: 0x06004025 RID: 16421 RVA: 0x00109261 File Offset: 0x00107461
			public bool MoveNext()
			{
				this.readPos++;
				return this.readPos < this.tagSource.Length;
			}

			// Token: 0x06004026 RID: 16422 RVA: 0x00109284 File Offset: 0x00107484
			public void Reset()
			{
				this.readPos = 1;
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06004027 RID: 16423 RVA: 0x0010928D File Offset: 0x0010748D
			public ArtifactDef Current
			{
				get
				{
					return ArtifactCatalog.GetArtifactDef((ArtifactIndex)(this.tagSource[this.readPos] - '0'));
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06004028 RID: 16424 RVA: 0x001092A8 File Offset: 0x001074A8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06004029 RID: 16425 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x04003E84 RID: 16004
			public readonly string tagSource;

			// Token: 0x04003E85 RID: 16005
			private int readPos;
		}

		// Token: 0x02000AE9 RID: 2793
		public struct ArtifactEnumerable
		{
			// Token: 0x0600402A RID: 16426 RVA: 0x001092B0 File Offset: 0x001074B0
			public ArtifactEnumerable(string tagSource)
			{
				this.tagSource = tagSource;
			}

			// Token: 0x0600402B RID: 16427 RVA: 0x001092BC File Offset: 0x001074BC
			public RemoteGameInfo.ArtifactEnumerator GetEnumerator()
			{
				RemoteGameInfo.ArtifactEnumerator result = new RemoteGameInfo.ArtifactEnumerator(this.tagSource);
				result.Reset();
				return result;
			}

			// Token: 0x04003E86 RID: 16006
			public readonly string tagSource;
		}
	}
}
