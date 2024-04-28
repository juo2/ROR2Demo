using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008FC RID: 2300
	public class VoteController : NetworkBehaviour
	{
		// Token: 0x060033EC RID: 13292 RVA: 0x000DABDA File Offset: 0x000D8DDA
		[Server]
		private void StartTimer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::StartTimer()' called on client");
				return;
			}
			if (this.timerIsActive)
			{
				return;
			}
			this.NetworktimerIsActive = true;
			this.Networktimer = this.timeoutDuration;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000DAC0D File Offset: 0x000D8E0D
		[Server]
		private void StopTimer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::StopTimer()' called on client");
				return;
			}
			this.NetworktimerIsActive = false;
			this.Networktimer = this.timeoutDuration;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000DAC38 File Offset: 0x000D8E38
		[Server]
		private void InitializeVoters()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::InitializeVoters()' called on client");
				return;
			}
			this.StopTimer();
			this.votes.Clear();
			IEnumerable<NetworkUser> source = NetworkUser.readOnlyInstancesList;
			if (this.onlyAllowParticipatingPlayers)
			{
				source = from v in source
				where v.isParticipating
				select v;
			}
			foreach (GameObject networkUserObject in from v in source
			select v.gameObject)
			{
				this.votes.Add(new UserVote
				{
					networkUserObject = networkUserObject,
					voteChoiceIndex = -1
				});
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000DAD1C File Offset: 0x000D8F1C
		[Server]
		private void AddUserToVoters(NetworkUser networkUser)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::AddUserToVoters(RoR2.NetworkUser)' called on client");
				return;
			}
			if (this.onlyAllowParticipatingPlayers && !networkUser.isParticipating)
			{
				return;
			}
			if (this.votes.Any((UserVote v) => v.networkUserObject == networkUser.gameObject))
			{
				return;
			}
			this.votes.Add(new UserVote
			{
				networkUserObject = networkUser.gameObject,
				voteChoiceIndex = -1
			});
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000DADAC File Offset: 0x000D8FAC
		private void Awake()
		{
			if (NetworkServer.active)
			{
				if (this.timerStartCondition == VoteController.TimerStartCondition.Immediate)
				{
					this.StartTimer();
				}
				if (this.addNewPlayers)
				{
					NetworkUser.OnPostNetworkUserStart += this.AddUserToVoters;
				}
				NetworkManagerSystem.onServerConnectGlobal += this.OnServerConnectGlobal;
				NetworkManagerSystem.onServerDisconnectGlobal += this.OnServerDisconnectGlobal;
			}
			this.votes.InitializeBehaviour(this, VoteController.kListvotes);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000DAE1A File Offset: 0x000D901A
		private void OnServerConnectGlobal(NetworkConnection conn)
		{
			if (this.resetOnConnectionsChanged)
			{
				this.InitializeVoters();
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000DAE1A File Offset: 0x000D901A
		private void OnServerDisconnectGlobal(NetworkConnection conn)
		{
			if (this.resetOnConnectionsChanged)
			{
				this.InitializeVoters();
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000DAE2A File Offset: 0x000D902A
		private void OnDestroy()
		{
			NetworkUser.OnPostNetworkUserStart -= this.AddUserToVoters;
			NetworkManagerSystem.onServerConnectGlobal -= this.OnServerConnectGlobal;
			NetworkManagerSystem.onServerDisconnectGlobal -= this.OnServerDisconnectGlobal;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000DAE5F File Offset: 0x000D905F
		public override void OnStartServer()
		{
			base.OnStartServer();
			this.InitializeVoters();
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000DAE70 File Offset: 0x000D9070
		[Server]
		public void ReceiveUserVote(NetworkUser networkUser, int voteChoiceIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::ReceiveUserVote(RoR2.NetworkUser,System.Int32)' called on client");
				return;
			}
			if (this.resetOnConnectionsChanged)
			{
				int connectingClientCount = PlatformSystems.networkManager.GetConnectingClientCount();
				if (connectingClientCount > 0)
				{
					Debug.LogFormat("Vote from user \"{0}\" rejected: {1} clients are currently still in the process of connecting.", new object[]
					{
						networkUser.userName,
						connectingClientCount
					});
					return;
				}
			}
			if (voteChoiceIndex < 0 && !this.canRevokeVote)
			{
				return;
			}
			if (voteChoiceIndex >= this.choices.Length)
			{
				return;
			}
			GameObject gameObject = networkUser.gameObject;
			for (int i = 0; i < (int)this.votes.Count; i++)
			{
				if (gameObject == this.votes[i].networkUserObject)
				{
					if (this.votes[i].receivedVote && !this.canChangeVote)
					{
						return;
					}
					this.votes[i] = new UserVote
					{
						networkUserObject = gameObject,
						voteChoiceIndex = voteChoiceIndex
					};
				}
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000DAF5F File Offset: 0x000D915F
		private void Update()
		{
			if (NetworkServer.active)
			{
				this.ServerUpdate();
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000DAF70 File Offset: 0x000D9170
		[Server]
		private void ServerUpdate()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::ServerUpdate()' called on client");
				return;
			}
			if (this.timerIsActive)
			{
				this.Networktimer = this.timer - Time.deltaTime;
				if (this.timer < 0f)
				{
					this.Networktimer = 0f;
				}
			}
			int num = 0;
			for (int i = (int)(this.votes.Count - 1); i >= 0; i--)
			{
				if (!this.votes[i].networkUserObject)
				{
					this.votes.RemoveAt(i);
				}
				else if (this.votes[i].receivedVote)
				{
					num++;
				}
			}
			bool flag = num > 0;
			bool flag2 = num == (int)this.votes.Count;
			if (flag)
			{
				if (this.timerStartCondition == VoteController.TimerStartCondition.OnAnyVoteReceived || this.timerStartCondition == VoteController.TimerStartCondition.WhileAnyVoteReceived)
				{
					this.StartTimer();
				}
			}
			else if (this.timerStartCondition == VoteController.TimerStartCondition.WhileAnyVoteReceived)
			{
				this.StopTimer();
			}
			if (flag2)
			{
				if (this.timerStartCondition == VoteController.TimerStartCondition.WhileAllVotesReceived)
				{
					this.StartTimer();
				}
				else if (RoR2Application.isInSinglePlayer)
				{
					this.Networktimer = 0f;
				}
				else
				{
					this.Networktimer = Mathf.Min(this.timer, this.minimumTimeBeforeProcessing);
				}
			}
			else if (this.timerStartCondition == VoteController.TimerStartCondition.WhileAllVotesReceived)
			{
				this.StopTimer();
			}
			if ((flag2 && !this.mustTimeOut) || (this.timerIsActive && this.timer <= 0f))
			{
				this.FinishVote();
			}
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000DB0E0 File Offset: 0x000D92E0
		[Server]
		private void FinishVote()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::FinishVote()' called on client");
				return;
			}
			IGrouping<int, UserVote> grouping = (from v in this.votes
			where v.receivedVote
			group v by v.voteChoiceIndex into v
			orderby v.Count<UserVote>() descending
			select v).FirstOrDefault<IGrouping<int, UserVote>>();
			int num = (grouping == null) ? this.defaultChoiceIndex : grouping.Key;
			if (num >= this.choices.Length)
			{
				num = this.defaultChoiceIndex;
			}
			if (num < this.choices.Length)
			{
				this.choices[num].Invoke();
			}
			base.enabled = false;
			this.NetworktimerIsActive = false;
			this.Networktimer = 0f;
			if (this.destroyGameObjectOnComplete)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000DB1E2 File Offset: 0x000D93E2
		public int GetVoteCount()
		{
			return (int)this.votes.Count;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000DB1EF File Offset: 0x000D93EF
		public UserVote GetVote(int i)
		{
			return this.votes[i];
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000DB200 File Offset: 0x000D9400
		public void SubmitVoteForAllLocalUsers(int choiceIndex)
		{
			foreach (NetworkUser networkUser in NetworkUser.readOnlyLocalPlayersList)
			{
				networkUser.CallCmdSubmitVote(base.gameObject, choiceIndex);
			}
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000DB288 File Offset: 0x000D9488
		// (set) Token: 0x060033FF RID: 13311 RVA: 0x000DB29B File Offset: 0x000D949B
		public bool NetworktimerIsActive
		{
			get
			{
				return this.timerIsActive;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.timerIsActive, 2U);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000DB2B0 File Offset: 0x000D94B0
		// (set) Token: 0x06003401 RID: 13313 RVA: 0x000DB2C3 File Offset: 0x000D94C3
		public float Networktimer
		{
			get
			{
				return this.timer;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.timer, 4U);
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000DB2D7 File Offset: 0x000D94D7
		protected static void InvokeSyncListvotes(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("SyncList votes called on server.");
				return;
			}
			((VoteController)obj).votes.HandleMsg(reader);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000DB300 File Offset: 0x000D9500
		static VoteController()
		{
			NetworkBehaviour.RegisterSyncListDelegate(typeof(VoteController), VoteController.kListvotes, new NetworkBehaviour.CmdDelegate(VoteController.InvokeSyncListvotes));
			NetworkCRC.RegisterBehaviour("VoteController", 0);
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000DB33C File Offset: 0x000D953C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteStructSyncListUserVote_None(writer, this.votes);
				writer.Write(this.timerIsActive);
				writer.Write(this.timer);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				GeneratedNetworkCode._WriteStructSyncListUserVote_None(writer, this.votes);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.timerIsActive);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.timer);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000DB428 File Offset: 0x000D9628
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				GeneratedNetworkCode._ReadStructSyncListUserVote_None(reader, this.votes);
				this.timerIsActive = reader.ReadBoolean();
				this.timer = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				GeneratedNetworkCode._ReadStructSyncListUserVote_None(reader, this.votes);
			}
			if ((num & 2) != 0)
			{
				this.timerIsActive = reader.ReadBoolean();
			}
			if ((num & 4) != 0)
			{
				this.timer = reader.ReadSingle();
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040034DB RID: 13531
		[Tooltip("Custom name for this component to help describe its role.")]
		public string customName;

		// Token: 0x040034DC RID: 13532
		[Tooltip("Whether or not users must be participating in the run to be allowed to vote.")]
		public bool onlyAllowParticipatingPlayers = true;

		// Token: 0x040034DD RID: 13533
		[Tooltip("Whether or not to add new players to the voting pool when they connect.")]
		public bool addNewPlayers;

		// Token: 0x040034DE RID: 13534
		[Tooltip("Whether or not users are allowed to change their choice after submitting it.")]
		public bool canChangeVote;

		// Token: 0x040034DF RID: 13535
		[Tooltip("Whether or not users are allowed to revoke their vote entirely after submitting it.")]
		public bool canRevokeVote;

		// Token: 0x040034E0 RID: 13536
		[Tooltip("If set, the vote cannot be completed early by all users submitting, and the timeout must occur.")]
		public bool mustTimeOut;

		// Token: 0x040034E1 RID: 13537
		[Tooltip("Whether or not this vote must reset and be unvotable while someone is connecting or disconnecting.")]
		public bool resetOnConnectionsChanged;

		// Token: 0x040034E2 RID: 13538
		[Tooltip("How long it takes for the vote to forcibly complete once the timer begins.")]
		public float timeoutDuration = 15f;

		// Token: 0x040034E3 RID: 13539
		[Tooltip("How long it takes for action to be taken after the vote is complete.")]
		public float minimumTimeBeforeProcessing = 3f;

		// Token: 0x040034E4 RID: 13540
		[Tooltip("What causes the timer to start counting down.")]
		public VoteController.TimerStartCondition timerStartCondition;

		// Token: 0x040034E5 RID: 13541
		[Tooltip("An array of functions to be called based on the user vote.")]
		public UnityEvent[] choices;

		// Token: 0x040034E6 RID: 13542
		[Tooltip("The choice to use when nobody votes or everybody who can vote quits.")]
		public int defaultChoiceIndex;

		// Token: 0x040034E7 RID: 13543
		[Tooltip("Whether or not to destroy the attached GameObject when the vote completes.")]
		public bool destroyGameObjectOnComplete = true;

		// Token: 0x040034E8 RID: 13544
		private SyncListUserVote votes = new SyncListUserVote();

		// Token: 0x040034E9 RID: 13545
		[SyncVar]
		public bool timerIsActive;

		// Token: 0x040034EA RID: 13546
		[SyncVar]
		public float timer;

		// Token: 0x040034EB RID: 13547
		private static int kListvotes = 458257089;

		// Token: 0x020008FD RID: 2301
		public enum TimerStartCondition
		{
			// Token: 0x040034ED RID: 13549
			Immediate,
			// Token: 0x040034EE RID: 13550
			OnAnyVoteReceived,
			// Token: 0x040034EF RID: 13551
			WhileAnyVoteReceived,
			// Token: 0x040034F0 RID: 13552
			WhileAllVotesReceived,
			// Token: 0x040034F1 RID: 13553
			Never
		}
	}
}
