using System;
using System.Collections.Generic;
using System.Linq;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000826 RID: 2086
	public class PreGameRuleVoteController : NetworkBehaviour
	{
		// Token: 0x06002D4E RID: 11598 RVA: 0x000C129C File Offset: 0x000BF49C
		public static PreGameRuleVoteController FindForUser(NetworkUser networkUser)
		{
			GameObject gameObject = networkUser.gameObject;
			foreach (PreGameRuleVoteController preGameRuleVoteController in PreGameRuleVoteController.instancesList)
			{
				if (preGameRuleVoteController.networkUserNetworkIdentity && preGameRuleVoteController.networkUserNetworkIdentity.gameObject == gameObject)
				{
					return preGameRuleVoteController;
				}
			}
			return null;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000C1318 File Offset: 0x000BF518
		public static void CreateForNetworkUserServer(NetworkUser networkUser)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/PreGameRuleVoteController"));
			PreGameRuleVoteController component = gameObject.GetComponent<PreGameRuleVoteController>();
			component.networkUserNetworkIdentity = networkUser.GetComponent<NetworkIdentity>();
			component.networkUser = networkUser;
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06002D50 RID: 11600 RVA: 0x000C1348 File Offset: 0x000BF548
		// (remove) Token: 0x06002D51 RID: 11601 RVA: 0x000C137C File Offset: 0x000BF57C
		public static event Action onVotesUpdated;

		// Token: 0x06002D52 RID: 11602 RVA: 0x000C13AF File Offset: 0x000BF5AF
		private static PreGameRuleVoteController.Vote[] CreateBallot()
		{
			return new PreGameRuleVoteController.Vote[RuleCatalog.ruleCount];
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000C13BB File Offset: 0x000BF5BB
		[SystemInitializer(new Type[]
		{
			typeof(RuleCatalog)
		})]
		private static void Init()
		{
			PreGameRuleVoteController.votesForEachChoice = new int[RuleCatalog.choiceCount];
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000C13CC File Offset: 0x000BF5CC
		private void Start()
		{
			if (this.localUser != null)
			{
				PreGameRuleVoteController.LocalUserBallotPersistenceManager.ApplyPersistentBallotIfPresent(this.localUser, this.votes);
				this.ClientTransmitVotesToServer();
			}
			if (NetworkServer.active)
			{
				PreGameRuleVoteController.UpdateGameVotes();
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000C13FC File Offset: 0x000BF5FC
		private void Update()
		{
			if (NetworkServer.active && !this.networkUserNetworkIdentity)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			if (this.clientShouldTransmit)
			{
				this.clientShouldTransmit = false;
				this.ClientTransmitVotesToServer();
			}
			if (PreGameRuleVoteController.shouldUpdateGameVotes)
			{
				PreGameRuleVoteController.shouldUpdateGameVotes = false;
				PreGameRuleVoteController.UpdateGameVotes();
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000C1450 File Offset: 0x000BF650
		[Client]
		private void ClientTransmitVotesToServer()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.PreGameRuleVoteController::ClientTransmitVotesToServer()' called on server");
				return;
			}
			Debug.Log("PreGameRuleVoteController.ClientTransmitVotesToServer()");
			if (!this.networkUserNetworkIdentity)
			{
				Debug.Log("Can't transmit votes: No network user object.");
				return;
			}
			NetworkUser component = this.networkUserNetworkIdentity.GetComponent<NetworkUser>();
			if (!component)
			{
				Debug.Log("Can't transmit votes: No network user component.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(70);
			networkWriter.Write(base.gameObject);
			this.WriteVotes(networkWriter);
			networkWriter.FinishMessage();
			component.connectionToServer.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000C14F4 File Offset: 0x000BF6F4
		[NetworkMessageHandler(msgType = 70, client = false, server = true)]
		public static void ServerHandleClientVoteUpdate(NetworkMessage netMsg)
		{
			string format = "Received vote from {0}";
			object[] array = new object[1];
			int num = 0;
			NetworkUser networkUser = NetworkUser.readOnlyInstancesList.FirstOrDefault((NetworkUser v) => v.connectionToClient == netMsg.conn);
			array[num] = ((networkUser != null) ? networkUser.userName : null);
			Debug.LogFormat(format, array);
			GameObject gameObject = netMsg.reader.ReadGameObject();
			if (!gameObject)
			{
				Debug.Log("PreGameRuleVoteController.ServerHandleClientVoteUpdate() failed: preGameRuleVoteControllerObject=null");
				return;
			}
			PreGameRuleVoteController component = gameObject.GetComponent<PreGameRuleVoteController>();
			if (!component)
			{
				Debug.Log("PreGameRuleVoteController.ServerHandleClientVoteUpdate() failed: preGameRuleVoteController=null");
				return;
			}
			NetworkIdentity networkUserNetworkIdentity = component.networkUserNetworkIdentity;
			if (!networkUserNetworkIdentity)
			{
				Debug.Log("PreGameRuleVoteController.ServerHandleClientVoteUpdate() failed: No NetworkIdentity");
				return;
			}
			NetworkUser component2 = networkUserNetworkIdentity.GetComponent<NetworkUser>();
			if (!component2)
			{
				Debug.Log("PreGameRuleVoteController.ServerHandleClientVoteUpdate() failed: No NetworkUser");
				return;
			}
			if (component2.connectionToClient != netMsg.conn)
			{
				Debug.LogFormat("PreGameRuleVoteController.ServerHandleClientVoteUpdate() failed: {0}!={1}", new object[]
				{
					component.connectionToClient,
					netMsg.conn
				});
				return;
			}
			Debug.LogFormat("Accepting vote from {0}", new object[]
			{
				component2.userName
			});
			component.ReadVotes(netMsg.reader);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000C1620 File Offset: 0x000BF820
		public void SetVote(int ruleIndex, int choiceValue)
		{
			PreGameRuleVoteController.Vote vote = this.votes[ruleIndex];
			if (vote.choiceValue == choiceValue)
			{
				return;
			}
			this.votes[ruleIndex].choiceValue = choiceValue;
			if (!NetworkServer.active && this.networkUserNetworkIdentity && this.networkUserNetworkIdentity.isLocalPlayer)
			{
				this.clientShouldTransmit = true;
			}
			else
			{
				base.SetDirtyBit(2U);
			}
			PreGameRuleVoteController.shouldUpdateGameVotes = true;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000C168E File Offset: 0x000BF88E
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x000C1696 File Offset: 0x000BF896
		public NetworkIdentity networkUserNetworkIdentity { get; private set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000C169F File Offset: 0x000BF89F
		private LocalUser localUser
		{
			get
			{
				NetworkUser networkUser = this.networkUser;
				if (networkUser == null)
				{
					return null;
				}
				return networkUser.localUser;
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000C16B4 File Offset: 0x000BF8B4
		private static void UpdateGameVotes()
		{
			int i = 0;
			int choiceCount = RuleCatalog.choiceCount;
			while (i < choiceCount)
			{
				PreGameRuleVoteController.votesForEachChoice[i] = 0;
				i++;
			}
			int j = 0;
			int ruleCount = RuleCatalog.ruleCount;
			while (j < ruleCount)
			{
				RuleDef ruleDef = RuleCatalog.GetRuleDef(j);
				int count = ruleDef.choices.Count;
				foreach (PreGameRuleVoteController preGameRuleVoteController in PreGameRuleVoteController.instancesList)
				{
					PreGameRuleVoteController.Vote vote = preGameRuleVoteController.votes[j];
					if (vote.hasVoted && vote.choiceValue < count)
					{
						RuleChoiceDef ruleChoiceDef = ruleDef.choices[vote.choiceValue];
						PreGameRuleVoteController.votesForEachChoice[ruleChoiceDef.globalIndex]++;
					}
				}
				j++;
			}
			if (NetworkServer.active)
			{
				bool flag = false;
				int k = 0;
				int ruleCount2 = RuleCatalog.ruleCount;
				while (k < ruleCount2)
				{
					RuleDef ruleDef2 = RuleCatalog.GetRuleDef(k);
					int count2 = ruleDef2.choices.Count;
					PreGameController.instance.readOnlyRuleBook.GetRuleChoiceIndex(ruleDef2);
					int ruleChoiceIndex = -1;
					int num = 0;
					bool flag2 = false;
					for (int l = 0; l < count2; l++)
					{
						RuleChoiceDef ruleChoiceDef2 = ruleDef2.choices[l];
						int num2 = PreGameRuleVoteController.votesForEachChoice[ruleChoiceDef2.globalIndex];
						if (num2 == num)
						{
							flag2 = true;
						}
						else if (num2 > num)
						{
							ruleChoiceIndex = ruleChoiceDef2.globalIndex;
							num = num2;
							flag2 = false;
						}
					}
					if (num == 0)
					{
						ruleChoiceIndex = ruleDef2.choices[ruleDef2.defaultChoiceIndex].globalIndex;
					}
					if (!flag2 || num == 0)
					{
						flag = (PreGameController.instance.ApplyChoice(ruleChoiceIndex) || flag);
					}
					k++;
				}
				if (flag)
				{
					PreGameController.instance.RecalculateModifierAvailability();
				}
			}
			Action action = PreGameRuleVoteController.onVotesUpdated;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000C1898 File Offset: 0x000BFA98
		private void Awake()
		{
			PreGameRuleVoteController.instancesList.Add(this);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000C18A5 File Offset: 0x000BFAA5
		private void OnDestroy()
		{
			PreGameRuleVoteController.shouldUpdateGameVotes = true;
			PreGameRuleVoteController.instancesList.Remove(this);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000C18BC File Offset: 0x000BFABC
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 3U;
			}
			writer.Write((byte)num);
			bool flag = (num & 1U) > 0U;
			bool flag2 = (num & 2U) > 0U;
			if (flag)
			{
				writer.Write(this.networkUserNetworkIdentity);
			}
			if (flag2)
			{
				this.WriteVotes(writer);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000C190C File Offset: 0x000BFB0C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			byte b = reader.ReadByte();
			bool flag = (b & 1) > 0;
			bool flag2 = (b & 2) > 0;
			if (flag)
			{
				this.networkUserNetworkIdentity = reader.ReadNetworkIdentity();
				this.networkUser = (this.networkUserNetworkIdentity ? this.networkUserNetworkIdentity.GetComponent<NetworkUser>() : null);
			}
			if (flag2)
			{
				this.ReadVotes(reader);
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000C1964 File Offset: 0x000BFB64
		private RuleChoiceDef GetDefaultChoice(RuleDef ruleDef)
		{
			return ruleDef.choices[PreGameController.instance.readOnlyRuleBook.GetRuleChoiceIndex(ruleDef)];
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000C1984 File Offset: 0x000BFB84
		private void SetVotesFromRuleBookForSinglePlayer()
		{
			for (int i = 0; i < this.votes.Length; i++)
			{
				RuleDef ruleDef = RuleCatalog.GetRuleDef(i);
				this.votes[i].choiceValue = this.GetDefaultChoice(ruleDef).localIndex;
			}
			base.SetDirtyBit(2U);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000C19D0 File Offset: 0x000BFBD0
		private void WriteVotes(NetworkWriter writer)
		{
			int i = 0;
			int ruleCount = RuleCatalog.ruleCount;
			while (i < ruleCount)
			{
				this.ruleMaskBuffer[i] = this.votes[i].hasVoted;
				i++;
			}
			writer.Write(this.ruleMaskBuffer);
			int j = 0;
			int ruleCount2 = RuleCatalog.ruleCount;
			while (j < ruleCount2)
			{
				if (this.votes[j].hasVoted)
				{
					PreGameRuleVoteController.Vote.Serialize(writer, this.votes[j]);
				}
				j++;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000C1A50 File Offset: 0x000BFC50
		private void ReadVotes(NetworkReader reader)
		{
			reader.ReadRuleMask(this.ruleMaskBuffer);
			bool flag = !this.networkUserNetworkIdentity || !this.networkUserNetworkIdentity.isLocalPlayer;
			int i = 0;
			int ruleCount = RuleCatalog.ruleCount;
			while (i < ruleCount)
			{
				PreGameRuleVoteController.Vote vote;
				if (this.ruleMaskBuffer[i])
				{
					vote = PreGameRuleVoteController.Vote.Deserialize(reader);
				}
				else
				{
					vote = default(PreGameRuleVoteController.Vote);
				}
				if (flag)
				{
					this.votes[i] = vote;
				}
				i++;
			}
			PreGameRuleVoteController.shouldUpdateGameVotes = (PreGameRuleVoteController.shouldUpdateGameVotes || flag);
			if (NetworkServer.active)
			{
				base.SetDirtyBit(2U);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000C1AE3 File Offset: 0x000BFCE3
		public bool IsChoiceVoted(RuleChoiceDef ruleChoiceDef)
		{
			return this.votes[ruleChoiceDef.ruleDef.globalIndex].choiceValue == ruleChoiceDef.localIndex;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000C1B08 File Offset: 0x000BFD08
		static PreGameRuleVoteController()
		{
			PreGameController.onServerRecalculatedModifierAvailability += delegate(PreGameController controller)
			{
				PreGameRuleVoteController.UpdateGameVotes();
			};
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002F60 RID: 12128
		private static readonly List<PreGameRuleVoteController> instancesList = new List<PreGameRuleVoteController>();

		// Token: 0x04002F62 RID: 12130
		private const byte networkUserIdentityDirtyBit = 1;

		// Token: 0x04002F63 RID: 12131
		private const byte votesDirtyBit = 2;

		// Token: 0x04002F64 RID: 12132
		private const byte allDirtyBits = 3;

		// Token: 0x04002F65 RID: 12133
		private PreGameRuleVoteController.Vote[] votes = PreGameRuleVoteController.CreateBallot();

		// Token: 0x04002F66 RID: 12134
		public static int[] votesForEachChoice;

		// Token: 0x04002F67 RID: 12135
		private bool clientShouldTransmit;

		// Token: 0x04002F69 RID: 12137
		private NetworkUser networkUser;

		// Token: 0x04002F6A RID: 12138
		private static bool shouldUpdateGameVotes;

		// Token: 0x04002F6B RID: 12139
		private readonly RuleMask ruleMaskBuffer = new RuleMask();

		// Token: 0x02000827 RID: 2087
		private static class LocalUserBallotPersistenceManager
		{
			// Token: 0x06002D6A RID: 11626 RVA: 0x000C1B47 File Offset: 0x000BFD47
			static LocalUserBallotPersistenceManager()
			{
				LocalUserManager.onUserSignIn += PreGameRuleVoteController.LocalUserBallotPersistenceManager.OnLocalUserSignIn;
				LocalUserManager.onUserSignOut += PreGameRuleVoteController.LocalUserBallotPersistenceManager.OnLocalUserSignOut;
				PreGameRuleVoteController.onVotesUpdated += PreGameRuleVoteController.LocalUserBallotPersistenceManager.OnVotesUpdated;
			}

			// Token: 0x06002D6B RID: 11627 RVA: 0x000C1B86 File Offset: 0x000BFD86
			private static void OnLocalUserSignIn(LocalUser localUser)
			{
				PreGameRuleVoteController.LocalUserBallotPersistenceManager.votesCache.Add(localUser, null);
			}

			// Token: 0x06002D6C RID: 11628 RVA: 0x000C1B94 File Offset: 0x000BFD94
			private static void OnLocalUserSignOut(LocalUser localUser)
			{
				PreGameRuleVoteController.LocalUserBallotPersistenceManager.votesCache.Remove(localUser);
			}

			// Token: 0x06002D6D RID: 11629 RVA: 0x000C1BA4 File Offset: 0x000BFDA4
			private static void OnVotesUpdated()
			{
				foreach (PreGameRuleVoteController preGameRuleVoteController in PreGameRuleVoteController.instancesList)
				{
					if (preGameRuleVoteController.localUser != null)
					{
						PreGameRuleVoteController.LocalUserBallotPersistenceManager.votesCache[preGameRuleVoteController.localUser] = preGameRuleVoteController.votes;
					}
				}
			}

			// Token: 0x06002D6E RID: 11630 RVA: 0x000C1C10 File Offset: 0x000BFE10
			public static void ApplyPersistentBallotIfPresent(LocalUser localUser, PreGameRuleVoteController.Vote[] dest)
			{
				PreGameRuleVoteController.Vote[] array;
				if (PreGameRuleVoteController.LocalUserBallotPersistenceManager.votesCache.TryGetValue(localUser, out array) && array != null)
				{
					Debug.LogFormat("Applying persistent ballot of votes for LocalUser {0}.", new object[]
					{
						localUser.userProfile.name
					});
					Array.Copy(array, dest, array.Length);
				}
			}

			// Token: 0x04002F6C RID: 12140
			private static readonly Dictionary<LocalUser, PreGameRuleVoteController.Vote[]> votesCache = new Dictionary<LocalUser, PreGameRuleVoteController.Vote[]>();
		}

		// Token: 0x02000828 RID: 2088
		[Serializable]
		private struct Vote
		{
			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000C1C57 File Offset: 0x000BFE57
			public bool hasVoted
			{
				get
				{
					return this.internalValue > 0;
				}
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000C1C62 File Offset: 0x000BFE62
			// (set) Token: 0x06002D71 RID: 11633 RVA: 0x000C1C6C File Offset: 0x000BFE6C
			public int choiceValue
			{
				get
				{
					return (int)(this.internalValue - 1);
				}
				set
				{
					this.internalValue = (byte)(value + 1);
				}
			}

			// Token: 0x06002D72 RID: 11634 RVA: 0x000C1C78 File Offset: 0x000BFE78
			public static void Serialize(NetworkWriter writer, PreGameRuleVoteController.Vote vote)
			{
				writer.Write(vote.internalValue);
			}

			// Token: 0x06002D73 RID: 11635 RVA: 0x000C1C88 File Offset: 0x000BFE88
			public static PreGameRuleVoteController.Vote Deserialize(NetworkReader reader)
			{
				return new PreGameRuleVoteController.Vote
				{
					internalValue = reader.ReadByte()
				};
			}

			// Token: 0x04002F6D RID: 12141
			[SerializeField]
			private byte internalValue;
		}
	}
}
