using System;
using EntityStates;
using JetBrains.Annotations;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000859 RID: 2137
	[RequireComponent(typeof(EntityStateMachine))]
	[RequireComponent(typeof(PurchaseInteraction))]
	public class RouletteChestController : NetworkBehaviour
	{
		// Token: 0x06002EC7 RID: 11975 RVA: 0x000C7CF4 File Offset: 0x000C5EF4
		private float CalcEntryDuration(int i)
		{
			float time = (float)i / (float)this.maxEntries;
			return this.bonusTimeDecay.Evaluate(time) * this.bonusTime + RouletteChestController.minTime;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000C7D25 File Offset: 0x000C5F25
		private bool isCycling
		{
			get
			{
				return !this.activationTime.isPositiveInfinity;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000C7D38 File Offset: 0x000C5F38
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint syncVarDirtyBits = base.syncVarDirtyBits;
			if (initialState)
			{
				syncVarDirtyBits = RouletteChestController.allDirtyBitsMask;
			}
			writer.WritePackedUInt32(syncVarDirtyBits);
			if ((syncVarDirtyBits & RouletteChestController.activationTimeDirtyBit) != 0U)
			{
				writer.Write(this.activationTime);
			}
			if ((syncVarDirtyBits & RouletteChestController.entriesDirtyBit) != 0U)
			{
				writer.WritePackedUInt32((uint)this.entries.Length);
				for (int i = 0; i < this.entries.Length; i++)
				{
					writer.Write(this.entries[i].pickupIndex);
				}
			}
			return syncVarDirtyBits > 0U;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000C7DB8 File Offset: 0x000C5FB8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			uint num = reader.ReadPackedUInt32();
			if ((num & RouletteChestController.activationTimeDirtyBit) != 0U)
			{
				this.activationTime = reader.ReadFixedTimeStamp();
			}
			if ((num & RouletteChestController.entriesDirtyBit) != 0U)
			{
				Array.Resize<RouletteChestController.Entry>(ref this.entries, (int)reader.ReadPackedUInt32());
				Run.FixedTimeStamp endTime = this.activationTime;
				for (int i = 0; i < this.entries.Length; i++)
				{
					RouletteChestController.Entry[] array = this.entries;
					int num2 = i;
					array[num2].pickupIndex = reader.ReadPickupIndex();
					array[num2].endTime = endTime + this.CalcEntryDuration(i);
					endTime = array[num2].endTime;
				}
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000C7E44 File Offset: 0x000C6044
		private void Awake()
		{
			this.stateMachine = base.GetComponent<EntityStateMachine>();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000C7E5E File Offset: 0x000C605E
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
			}
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000C7E81 File Offset: 0x000C6081
		private void OnEnable()
		{
			base.SetDirtyBit(RouletteChestController.enabledDirtyBit);
			if (this.pickupDisplay)
			{
				this.pickupDisplay.enabled = true;
			}
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000C7EA7 File Offset: 0x000C60A7
		private void OnDisable()
		{
			if (this.pickupDisplay)
			{
				this.pickupDisplay.SetPickupIndex(PickupIndex.none, false);
				this.pickupDisplay.enabled = false;
			}
			base.SetDirtyBit(RouletteChestController.enabledDirtyBit);
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000C7EE0 File Offset: 0x000C60E0
		[Server]
		private void GenerateEntriesServer(Run.FixedTimeStamp startTime)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RouletteChestController::GenerateEntriesServer(RoR2.Run/FixedTimeStamp)' called on client");
				return;
			}
			Array.Resize<RouletteChestController.Entry>(ref this.entries, this.maxEntries);
			for (int i = 0; i < this.entries.Length; i++)
			{
				RouletteChestController.Entry[] array = this.entries;
				int num = i;
				array[num].endTime = startTime + this.CalcEntryDuration(i);
				startTime = array[num].endTime;
			}
			PickupIndex b = PickupIndex.none;
			for (int j = 0; j < this.entries.Length; j++)
			{
				RouletteChestController.Entry[] array2 = this.entries;
				int num2 = j;
				PickupIndex pickupIndex = this.dropTable.GenerateDrop(this.rng);
				if (pickupIndex == b)
				{
					pickupIndex = this.dropTable.GenerateDrop(this.rng);
				}
				array2[num2].pickupIndex = pickupIndex;
				b = pickupIndex;
			}
			base.SetDirtyBit(RouletteChestController.entriesDirtyBit);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000C7FB1 File Offset: 0x000C61B1
		[Server]
		public void HandleInteractionServer(Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RouletteChestController::HandleInteractionServer(RoR2.Interactor)' called on client");
				return;
			}
			((RouletteChestController.RouletteChestControllerBaseState)this.stateMachine.state).HandleInteractionServer(activator);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000C7FE0 File Offset: 0x000C61E0
		[Server]
		private void BeginCycleServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RouletteChestController::BeginCycleServer()' called on client");
				return;
			}
			this.activationTime = Run.FixedTimeStamp.now;
			base.SetDirtyBit(RouletteChestController.activationTimeDirtyBit);
			this.GenerateEntriesServer(this.activationTime);
			UnityEvent unityEvent = this.onCycleBeginServer;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000C8034 File Offset: 0x000C6234
		[Server]
		private void EndCycleServer([CanBeNull] Interactor activator)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.RouletteChestController::EndCycleServer(RoR2.Interactor)' called on client");
				return;
			}
			float b = 0f;
			NetworkUser networkUser;
			if (activator && (networkUser = Util.LookUpBodyNetworkUser(activator.gameObject)) != null)
			{
				b = RttManager.GetConnectionRTT(networkUser.connectionToClient);
			}
			Run.FixedTimeStamp time = Run.FixedTimeStamp.now - b - RouletteChestController.rewindTime;
			PickupIndex pickupIndexForTime = this.GetPickupIndexForTime(time);
			this.EjectPickupServer(pickupIndexForTime);
			this.activationTime = Run.FixedTimeStamp.positiveInfinity;
			this.onCycleCompletedServer.Invoke();
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000C80BC File Offset: 0x000C62BC
		private void FixedUpdate()
		{
			if (this.pickupDisplay)
			{
				this.pickupDisplay.SetPickupIndex(this.isCycling ? this.GetPickupIndexForTime(Run.FixedTimeStamp.now) : PickupIndex.none, false);
			}
			if (NetworkClient.active)
			{
				int entryIndexForTime = this.GetEntryIndexForTime(Run.FixedTimeStamp.now);
				if (entryIndexForTime != this.previousEntryIndexClient)
				{
					this.previousEntryIndexClient = entryIndexForTime;
					this.onChangedEntryClient.Invoke();
				}
			}
			if (NetworkServer.active && this.isCycling && this.entries.Length != 0)
			{
				Run.FixedTimeStamp endTime = this.entries[this.entries.Length - 1].endTime;
				if (endTime.hasPassed)
				{
					this.EndCycleServer(null);
				}
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000C8170 File Offset: 0x000C6370
		private int GetEntryIndexForTime(Run.FixedTimeStamp time)
		{
			for (int i = 0; i < this.entries.Length; i++)
			{
				if (time < this.entries[i].endTime)
				{
					return i;
				}
			}
			if (this.entries.Length != 0)
			{
				return this.entries.Length - 1;
			}
			return -1;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000C81C0 File Offset: 0x000C63C0
		private PickupIndex GetPickupIndexForTime(Run.FixedTimeStamp time)
		{
			int entryIndexForTime = this.GetEntryIndexForTime(time);
			if (entryIndexForTime != -1)
			{
				return this.entries[entryIndexForTime].pickupIndex;
			}
			return PickupIndex.none;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000C81F0 File Offset: 0x000C63F0
		private void EjectPickupServer(PickupIndex pickupIndex)
		{
			if (pickupIndex == PickupIndex.none)
			{
				return;
			}
			PickupDropletController.CreatePickupDroplet(pickupIndex, this.ejectionTransform.position, this.ejectionTransform.rotation * this.localEjectionVelocity);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040030CE RID: 12494
		public int maxEntries = 1;

		// Token: 0x040030CF RID: 12495
		public float bonusTime;

		// Token: 0x040030D0 RID: 12496
		public AnimationCurve bonusTimeDecay;

		// Token: 0x040030D1 RID: 12497
		public PickupDropTable dropTable;

		// Token: 0x040030D2 RID: 12498
		public Transform ejectionTransform;

		// Token: 0x040030D3 RID: 12499
		public Vector3 localEjectionVelocity;

		// Token: 0x040030D4 RID: 12500
		public Animator modelAnimator;

		// Token: 0x040030D5 RID: 12501
		public PickupDisplay pickupDisplay;

		// Token: 0x040030D6 RID: 12502
		private EntityStateMachine stateMachine;

		// Token: 0x040030D7 RID: 12503
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040030D8 RID: 12504
		private static readonly float averageHgReactionTime = 0.20366667f;

		// Token: 0x040030D9 RID: 12505
		private static readonly float lowestHgReactionTime = 0.15f;

		// Token: 0x040030DA RID: 12506
		private static readonly float recognitionWindow = 0.15f;

		// Token: 0x040030DB RID: 12507
		private static readonly float minTime = RouletteChestController.lowestHgReactionTime + RouletteChestController.recognitionWindow;

		// Token: 0x040030DC RID: 12508
		private static readonly float rewindTime = 0.05f;

		// Token: 0x040030DD RID: 12509
		private Run.FixedTimeStamp activationTime = Run.FixedTimeStamp.positiveInfinity;

		// Token: 0x040030DE RID: 12510
		private RouletteChestController.Entry[] entries = Array.Empty<RouletteChestController.Entry>();

		// Token: 0x040030DF RID: 12511
		private Xoroshiro128Plus rng;

		// Token: 0x040030E0 RID: 12512
		private static readonly uint activationTimeDirtyBit = 1U;

		// Token: 0x040030E1 RID: 12513
		private static readonly uint entriesDirtyBit = 2U;

		// Token: 0x040030E2 RID: 12514
		private static readonly uint enabledDirtyBit = 4U;

		// Token: 0x040030E3 RID: 12515
		private static readonly uint allDirtyBitsMask = RouletteChestController.activationTimeDirtyBit | RouletteChestController.entriesDirtyBit;

		// Token: 0x040030E4 RID: 12516
		private int previousEntryIndexClient = -1;

		// Token: 0x040030E5 RID: 12517
		public UnityEvent onCycleBeginServer;

		// Token: 0x040030E6 RID: 12518
		public UnityEvent onCycleCompletedServer;

		// Token: 0x040030E7 RID: 12519
		public UnityEvent onChangedEntryClient;

		// Token: 0x0200085A RID: 2138
		public struct Entry
		{
			// Token: 0x040030E8 RID: 12520
			public PickupIndex pickupIndex;

			// Token: 0x040030E9 RID: 12521
			public Run.FixedTimeStamp endTime;
		}

		// Token: 0x0200085B RID: 2139
		private class RouletteChestControllerBaseState : EntityState
		{
			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000C82BB File Offset: 0x000C64BB
			// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000C82C3 File Offset: 0x000C64C3
			private protected RouletteChestController rouletteChestController { protected get; private set; }

			// Token: 0x06002EDD RID: 11997 RVA: 0x000C82CC File Offset: 0x000C64CC
			public override void OnEnter()
			{
				base.OnEnter();
				this.rouletteChestController = base.GetComponent<RouletteChestController>();
			}

			// Token: 0x06002EDE RID: 11998 RVA: 0x000026ED File Offset: 0x000008ED
			public virtual void HandleInteractionServer(Interactor activator)
			{
			}
		}

		// Token: 0x0200085C RID: 2140
		private class Idle : RouletteChestController.RouletteChestControllerBaseState
		{
			// Token: 0x06002EE0 RID: 12000 RVA: 0x000C82E0 File Offset: 0x000C64E0
			public override void OnEnter()
			{
				base.OnEnter();
				this.PlayAnimation("Body", "Idle");
				base.rouletteChestController.purchaseInteraction.Networkavailable = true;
			}

			// Token: 0x06002EE1 RID: 12001 RVA: 0x000C8309 File Offset: 0x000C6509
			public override void HandleInteractionServer(Interactor activator)
			{
				base.HandleInteractionServer(activator);
				this.outer.SetNextState(new RouletteChestController.Startup());
			}
		}

		// Token: 0x0200085D RID: 2141
		private class Startup : RouletteChestController.RouletteChestControllerBaseState
		{
			// Token: 0x06002EE3 RID: 12003 RVA: 0x000C832C File Offset: 0x000C652C
			public override void OnEnter()
			{
				base.OnEnter();
				this.PlayAnimation("Body", "IdleToActive");
				base.rouletteChestController.purchaseInteraction.Networkavailable = false;
				base.rouletteChestController.purchaseInteraction.costType = CostTypeIndex.None;
				Util.PlaySound(RouletteChestController.Startup.soundEntryEvent, base.gameObject);
			}

			// Token: 0x06002EE4 RID: 12004 RVA: 0x000C8382 File Offset: 0x000C6582
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge > RouletteChestController.Startup.baseDuration)
				{
					this.outer.SetNextState(new RouletteChestController.Cycling());
				}
			}

			// Token: 0x040030EB RID: 12523
			public static float baseDuration;

			// Token: 0x040030EC RID: 12524
			public static string soundEntryEvent;
		}

		// Token: 0x0200085E RID: 2142
		private class Cycling : RouletteChestController.RouletteChestControllerBaseState
		{
			// Token: 0x06002EE6 RID: 12006 RVA: 0x000C83B0 File Offset: 0x000C65B0
			public override void OnEnter()
			{
				base.OnEnter();
				base.rouletteChestController.onChangedEntryClient.AddListener(new UnityAction(this.OnChangedEntryClient));
				if (NetworkServer.active)
				{
					base.rouletteChestController.BeginCycleServer();
					base.rouletteChestController.onCycleCompletedServer.AddListener(new UnityAction(this.OnCycleCompleted));
				}
				base.rouletteChestController.purchaseInteraction.Networkavailable = true;
				base.rouletteChestController.purchaseInteraction.costType = CostTypeIndex.None;
			}

			// Token: 0x06002EE7 RID: 12007 RVA: 0x000C842F File Offset: 0x000C662F
			private void OnCycleCompleted()
			{
				this.outer.SetNextState(new RouletteChestController.Opening());
			}

			// Token: 0x06002EE8 RID: 12008 RVA: 0x000C8441 File Offset: 0x000C6641
			public override void OnExit()
			{
				base.rouletteChestController.onCycleCompletedServer.RemoveListener(new UnityAction(this.OnCycleCompleted));
				base.rouletteChestController.onChangedEntryClient.RemoveListener(new UnityAction(this.OnChangedEntryClient));
				base.OnExit();
			}

			// Token: 0x06002EE9 RID: 12009 RVA: 0x000C8484 File Offset: 0x000C6684
			private void OnChangedEntryClient()
			{
				int entryIndexForTime = base.rouletteChestController.GetEntryIndexForTime(Run.FixedTimeStamp.now);
				float num = base.rouletteChestController.CalcEntryDuration(entryIndexForTime);
				base.PlayAnimation("Body", "ActiveLoop", "ActiveLoop.playbackRate", num);
				float num2 = Util.Remap(num, RouletteChestController.minTime, RouletteChestController.minTime + base.rouletteChestController.bonusTime, 1f, 0f);
				Util.PlaySound(RouletteChestController.Cycling.soundCycleEvent, base.gameObject, RouletteChestController.Cycling.soundCycleSpeedRtpc, num2 * RouletteChestController.Cycling.soundCycleSpeedRtpcScale);
			}

			// Token: 0x06002EEA RID: 12010 RVA: 0x000C8509 File Offset: 0x000C6709
			public override void HandleInteractionServer(Interactor activator)
			{
				base.HandleInteractionServer(activator);
				base.rouletteChestController.EndCycleServer(activator);
			}

			// Token: 0x040030ED RID: 12525
			public static string soundCycleEvent;

			// Token: 0x040030EE RID: 12526
			public static string soundCycleSpeedRtpc;

			// Token: 0x040030EF RID: 12527
			public static float soundCycleSpeedRtpcScale;
		}

		// Token: 0x0200085F RID: 2143
		private class Opening : RouletteChestController.RouletteChestControllerBaseState
		{
			// Token: 0x06002EEC RID: 12012 RVA: 0x000C8520 File Offset: 0x000C6720
			public override void OnEnter()
			{
				base.OnEnter();
				this.PlayAnimation("Body", "ActiveToOpening");
				base.rouletteChestController.purchaseInteraction.Networkavailable = false;
				base.rouletteChestController.purchaseInteraction.costType = CostTypeIndex.None;
				Util.PlaySound(RouletteChestController.Opening.soundEntryEvent, base.gameObject);
			}

			// Token: 0x06002EED RID: 12013 RVA: 0x000C8576 File Offset: 0x000C6776
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge > RouletteChestController.Opening.baseDuration)
				{
					this.outer.SetNextState(new RouletteChestController.Opened());
				}
			}

			// Token: 0x040030F0 RID: 12528
			public static float baseDuration;

			// Token: 0x040030F1 RID: 12529
			public static string soundEntryEvent;
		}

		// Token: 0x02000860 RID: 2144
		private class Opened : RouletteChestController.RouletteChestControllerBaseState
		{
			// Token: 0x06002EEF RID: 12015 RVA: 0x000C85A2 File Offset: 0x000C67A2
			public override void OnEnter()
			{
				base.OnEnter();
				this.PlayAnimation("Body", "Opened");
				base.rouletteChestController.purchaseInteraction.Networkavailable = false;
				base.rouletteChestController.purchaseInteraction.costType = CostTypeIndex.None;
			}
		}
	}
}
