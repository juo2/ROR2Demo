using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.ConVar;
using RoR2.ExpansionManagement;
using RoR2.Navigation;
using RoR2.Networking;
using RoR2.Stats;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006EC RID: 1772
	[DisallowMultipleComponent]
	[RequireComponent(typeof(NetworkRuleBook))]
	[RequireComponent(typeof(RunArtifactManager))]
	public class Run : NetworkBehaviour
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x00097C86 File Offset: 0x00095E86
		// (set) Token: 0x06002329 RID: 9001 RVA: 0x00097C8D File Offset: 0x00095E8D
		public static Run instance { get; private set; }

		// Token: 0x0600232A RID: 9002 RVA: 0x00097C95 File Offset: 0x00095E95
		protected virtual void OnEnable()
		{
			Run.instance = SingletonHelper.Assign<Run>(Run.instance, this);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00097CA7 File Offset: 0x00095EA7
		protected virtual void OnDisable()
		{
			Run.instance = SingletonHelper.Unassign<Run>(Run.instance, this);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00097CBC File Offset: 0x00095EBC
		protected void Awake()
		{
			this.networkRuleBookComponent = base.GetComponent<NetworkRuleBook>();
			this.networkRuleBookComponent.onRuleBookUpdated += this.OnRuleBookUpdated;
			this.availableItems = ItemMask.Rent();
			this.expansionLockedItems = ItemMask.Rent();
			this.availableEquipment = EquipmentMask.Rent();
			this.expansionLockedEquipment = EquipmentMask.Rent();
			if (NetworkServer.active)
			{
				this.Network_uniqueId = (NetworkGuid)Guid.NewGuid();
				this.NetworkstartTimeUtc = (NetworkDateTime)DateTime.UtcNow;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00097D3F File Offset: 0x00095F3F
		public RuleBook ruleBook
		{
			get
			{
				return this.networkRuleBookComponent.ruleBook;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00097D4C File Offset: 0x00095F4C
		[Server]
		public void SetRuleBook(RuleBook newRuleBook)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::SetRuleBook(RoR2.RuleBook)' called on client");
				return;
			}
			this.networkRuleBookComponent.SetRuleBook(newRuleBook);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00097D70 File Offset: 0x00095F70
		private void OnRuleBookUpdated(NetworkRuleBook networkRuleBookComponent)
		{
			RuleBook ruleBook = networkRuleBookComponent.ruleBook;
			this.selectedDifficulty = ruleBook.FindDifficulty();
			ruleBook.GenerateItemMask(this.availableItems);
			ruleBook.GenerateEquipmentMask(this.availableEquipment);
			this.expansionLockedItems.Clear();
			foreach (ItemDef itemDef in ItemCatalog.allItemDefs)
			{
				if (itemDef && itemDef.requiredExpansion && !this.IsExpansionEnabled(itemDef.requiredExpansion))
				{
					this.expansionLockedItems.Add(itemDef.itemIndex);
				}
			}
			this.expansionLockedEquipment.Clear();
			foreach (EquipmentIndex equipmentIndex in EquipmentCatalog.allEquipment)
			{
				EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
				if (equipmentDef && equipmentDef.requiredExpansion && !this.IsExpansionEnabled(equipmentDef.requiredExpansion))
				{
					this.expansionLockedEquipment.Add(equipmentDef.equipmentIndex);
				}
			}
			if (NetworkServer.active)
			{
				Action<Run, RuleBook> action = Run.onServerRunSetRuleBookGlobal;
				if (action != null)
				{
					action(this, ruleBook);
				}
			}
			Action<Run, RuleBook> action2 = Run.onRunSetRuleBookGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this, ruleBook);
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06002330 RID: 9008 RVA: 0x00097EDC File Offset: 0x000960DC
		// (remove) Token: 0x06002331 RID: 9009 RVA: 0x00097F10 File Offset: 0x00096110
		public static event Action<Run, RuleBook> onServerRunSetRuleBookGlobal;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002332 RID: 9010 RVA: 0x00097F44 File Offset: 0x00096144
		// (remove) Token: 0x06002333 RID: 9011 RVA: 0x00097F78 File Offset: 0x00096178
		public static event Action<Run, RuleBook> onRunSetRuleBookGlobal;

		// Token: 0x06002334 RID: 9012 RVA: 0x00097FAB File Offset: 0x000961AB
		public Guid GetUniqueId()
		{
			return (Guid)this._uniqueId;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00097FB8 File Offset: 0x000961B8
		public DateTime GetStartTimeUtc()
		{
			return (DateTime)this.startTimeUtc;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00097FC8 File Offset: 0x000961C8
		[Server]
		private void SetRunStopwatchPaused(bool isPaused)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::SetRunStopwatchPaused(System.Boolean)' called on client");
				return;
			}
			if (isPaused != this.runStopwatch.isPaused)
			{
				Run.RunStopwatch networkrunStopwatch = this.runStopwatch;
				networkrunStopwatch.isPaused = isPaused;
				float num = this.GetRunStopwatch();
				if (isPaused)
				{
					networkrunStopwatch.offsetFromFixedTime = num;
				}
				else
				{
					networkrunStopwatch.offsetFromFixedTime = num - this.fixedTime;
				}
				this.NetworkrunStopwatch = networkrunStopwatch;
			}
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00098031 File Offset: 0x00096231
		public float GetRunStopwatch()
		{
			if (this.runStopwatch.isPaused)
			{
				return this.runStopwatch.offsetFromFixedTime;
			}
			return this.fixedTime + this.runStopwatch.offsetFromFixedTime;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00098060 File Offset: 0x00096260
		[Server]
		public void SetRunStopwatch(float t)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::SetRunStopwatch(System.Single)' called on client");
				return;
			}
			Run.RunStopwatch runStopwatch = this.runStopwatch;
			if (runStopwatch.isPaused)
			{
				runStopwatch.offsetFromFixedTime = t;
			}
			else
			{
				runStopwatch.offsetFromFixedTime = t - this.fixedTime;
			}
			this.NetworkrunStopwatch = runStopwatch;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000980B1 File Offset: 0x000962B1
		public bool isRunStopwatchPaused
		{
			get
			{
				return this.runStopwatch.isPaused;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x000980BE File Offset: 0x000962BE
		public virtual int loopClearCount
		{
			get
			{
				return this.stageClearCount / Run.stagesPerLoop;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x000980CC File Offset: 0x000962CC
		public virtual bool spawnWithPod
		{
			get
			{
				return Run.instance.stageClearCount == 0;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public virtual bool autoGenerateSpawnPoints
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public virtual bool canFamilyEventTrigger
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000980DB File Offset: 0x000962DB
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x000980E3 File Offset: 0x000962E3
		public GameObject uiInstance { get; protected set; }

		// Token: 0x06002340 RID: 9024 RVA: 0x000980EC File Offset: 0x000962EC
		public virtual GameObject InstantiateUi(Transform uiRoot)
		{
			if (uiRoot && this.uiPrefab)
			{
				this.uiInstance = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefab, uiRoot);
			}
			return this.uiInstance;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0009811B File Offset: 0x0009631B
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x00098123 File Offset: 0x00096323
		public ulong seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				this._seed = value;
				this.OnSeedSet();
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00098134 File Offset: 0x00096334
		private void GenerateStageRNG()
		{
			this.stageRng = new Xoroshiro128Plus(this.stageRngGenerator.nextUlong);
			this.bossRewardRng = new Xoroshiro128Plus(this.stageRng.nextUlong);
			this.treasureRng = new Xoroshiro128Plus(this.stageRng.nextUlong);
			this.spawnRng = new Xoroshiro128Plus(this.stageRng.nextUlong);
			this.randomSurvivorOnRespawnRng = new Xoroshiro128Plus(this.stageRng.nextUlong);
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000981AF File Offset: 0x000963AF
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x000981B7 File Offset: 0x000963B7
		public DifficultyIndex selectedDifficulty
		{
			get
			{
				return (DifficultyIndex)this.selectedDifficultyInternal;
			}
			set
			{
				this.NetworkselectedDifficultyInternal = (int)value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000981C0 File Offset: 0x000963C0
		public int livingPlayerCount
		{
			get
			{
				return PlayerCharacterMasterController.GetPlayersWithBodiesCount();
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000981C7 File Offset: 0x000963C7
		public int participatingPlayerCount
		{
			get
			{
				return Math.Max(PlayerCharacterMasterController.instances.Count, PlatformSystems.lobbyManager.calculatedTotalPlayerCount);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000981E2 File Offset: 0x000963E2
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x000981EA File Offset: 0x000963EA
		public float ambientLevel { get; protected set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000981F3 File Offset: 0x000963F3
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x000981FB File Offset: 0x000963FB
		public int ambientLevelFloor { get; protected set; }

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600234C RID: 9036 RVA: 0x00098204 File Offset: 0x00096404
		// (remove) Token: 0x0600234D RID: 9037 RVA: 0x00098238 File Offset: 0x00096438
		public static event Action<Run> onRunAmbientLevelUp;

		// Token: 0x0600234E RID: 9038 RVA: 0x0009826B File Offset: 0x0009646B
		protected void OnAmbientLevelUp()
		{
			Action<Run> action = Run.onRunAmbientLevelUp;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x0009827D File Offset: 0x0009647D
		public float teamlessDamageCoefficient
		{
			get
			{
				return this.difficultyCoefficient;
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x00098285 File Offset: 0x00096485
		protected virtual void FixedUpdate()
		{
			this.NetworkfixedTime = this.fixedTime + Time.fixedDeltaTime;
			Run.FixedTimeStamp.Update();
			if (NetworkServer.active)
			{
				this.SetRunStopwatchPaused(!this.ShouldUpdateRunStopwatch());
			}
			this.OnFixedUpdate();
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000982BA File Offset: 0x000964BA
		public void RecalculateDifficultyCoefficent()
		{
			this.RecalculateDifficultyCoefficentInternal();
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000982C4 File Offset: 0x000964C4
		protected virtual void RecalculateDifficultyCoefficentInternal()
		{
			float num = this.GetRunStopwatch();
			DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(this.selectedDifficulty);
			float num2 = Mathf.Floor(num * 0.016666668f);
			float num3 = (float)this.participatingPlayerCount * 0.3f;
			float num4 = 0.7f + num3;
			float num5 = 0.7f + num3;
			float num6 = Mathf.Pow((float)this.participatingPlayerCount, 0.2f);
			float num7 = 0.0506f * difficultyDef.scalingValue * num6;
			float num8 = 0.0506f * difficultyDef.scalingValue * num6;
			float num9 = Mathf.Pow(1.15f, (float)this.stageClearCount);
			this.compensatedDifficultyCoefficient = (num5 + num8 * num2) * num9;
			this.difficultyCoefficient = (num4 + num7 * num2) * num9;
			float num10 = (num4 + num7 * (num * 0.016666668f)) * Mathf.Pow(1.15f, (float)this.stageClearCount);
			this.ambientLevel = Mathf.Min((num10 - num4) / 0.33f + 1f, (float)Run.ambientLevelCap);
			int ambientLevelFloor = this.ambientLevelFloor;
			this.ambientLevelFloor = Mathf.FloorToInt(this.ambientLevel);
			if (ambientLevelFloor != this.ambientLevelFloor && ambientLevelFloor != 0 && this.ambientLevelFloor > ambientLevelFloor)
			{
				this.OnAmbientLevelUp();
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000983F4 File Offset: 0x000965F4
		protected virtual void OnFixedUpdate()
		{
			this.RecalculateDifficultyCoefficent();
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000983FC File Offset: 0x000965FC
		protected void Update()
		{
			this.time = Mathf.Clamp(this.time + Time.deltaTime, this.fixedTime, this.fixedTime + Time.fixedDeltaTime);
			Run.TimeStamp.Update();
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x0009842C File Offset: 0x0009662C
		protected virtual bool ShouldUpdateRunStopwatch()
		{
			return SceneCatalog.mostRecentSceneDef.sceneType == SceneType.Stage && this.livingPlayerCount > 0;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00098446 File Offset: 0x00096646
		[Server]
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		public bool CanUnlockableBeGrantedThisRun(string unlockableName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::CanUnlockableBeGrantedThisRun(System.String)' called on client");
				return false;
			}
			return this.CanUnlockableBeGrantedThisRun(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0009846A File Offset: 0x0009666A
		[Server]
		public virtual bool CanUnlockableBeGrantedThisRun(UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::CanUnlockableBeGrantedThisRun(RoR2.UnlockableDef)' called on client");
				return false;
			}
			return !this.unlockablesAlreadyFullyObtained.Contains(unlockableDef);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00098491 File Offset: 0x00096691
		[Server]
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		public void GrantUnlockToAllParticipatingPlayers(string unlockableName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::GrantUnlockToAllParticipatingPlayers(System.String)' called on client");
				return;
			}
			this.GrantUnlockToAllParticipatingPlayers(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000984B4 File Offset: 0x000966B4
		[Server]
		public void GrantUnlockToAllParticipatingPlayers(UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::GrantUnlockToAllParticipatingPlayers(RoR2.UnlockableDef)' called on client");
				return;
			}
			if (!unlockableDef || unlockableDef.index == UnlockableIndex.None)
			{
				return;
			}
			if (this.unlockablesAlreadyFullyObtained.Contains(unlockableDef))
			{
				return;
			}
			this.unlockablesAlreadyFullyObtained.Add(unlockableDef);
			foreach (NetworkUser networkUser in NetworkUser.readOnlyInstancesList)
			{
				if (networkUser.isParticipating)
				{
					networkUser.ServerHandleUnlock(unlockableDef);
				}
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0009854C File Offset: 0x0009674C
		[Server]
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		public void GrantUnlockToSinglePlayer(string unlockableName, CharacterBody body)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::GrantUnlockToSinglePlayer(System.String,RoR2.CharacterBody)' called on client");
				return;
			}
			this.GrantUnlockToSinglePlayer(UnlockableCatalog.GetUnlockableDef(unlockableName), body);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00098570 File Offset: 0x00096770
		[Server]
		public void GrantUnlockToSinglePlayer(UnlockableDef unlockableDef, CharacterBody body)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::GrantUnlockToSinglePlayer(RoR2.UnlockableDef,RoR2.CharacterBody)' called on client");
				return;
			}
			if (!unlockableDef || unlockableDef.index == UnlockableIndex.None)
			{
				return;
			}
			if (body)
			{
				NetworkUser networkUser = Util.LookUpBodyNetworkUser(body);
				if (networkUser)
				{
					networkUser.ServerHandleUnlock(unlockableDef);
				}
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000985C2 File Offset: 0x000967C2
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		[Server]
		public bool IsUnlockableUnlocked(string unlockableName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::IsUnlockableUnlocked(System.String)' called on client");
				return false;
			}
			return this.IsUnlockableUnlocked(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000985E6 File Offset: 0x000967E6
		[Server]
		public virtual bool IsUnlockableUnlocked(UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::IsUnlockableUnlocked(RoR2.UnlockableDef)' called on client");
				return false;
			}
			return this.unlockablesUnlockedByAnyUser.Contains(unlockableDef);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0009860A File Offset: 0x0009680A
		[Server]
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		public bool DoesEveryoneHaveThisUnlockableUnlocked(string unlockableName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::DoesEveryoneHaveThisUnlockableUnlocked(System.String)' called on client");
				return false;
			}
			return this.DoesEveryoneHaveThisUnlockableUnlocked(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0009862E File Offset: 0x0009682E
		[Server]
		public virtual bool DoesEveryoneHaveThisUnlockableUnlocked(UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::DoesEveryoneHaveThisUnlockableUnlocked(RoR2.UnlockableDef)' called on client");
				return false;
			}
			return this.unlockablesUnlockedByAllUsers.Contains(unlockableDef);
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00098652 File Offset: 0x00096852
		[Obsolete("Use the overload that accepts an UnlockableDef instead. This method may be removed from future releases.", false)]
		[Server]
		public void ForceUnlockImmediate(string unlockableName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::ForceUnlockImmediate(System.String)' called on client");
				return;
			}
			this.ForceUnlockImmediate(UnlockableCatalog.GetUnlockableDef(unlockableName));
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x00098675 File Offset: 0x00096875
		[Server]
		public void ForceUnlockImmediate(UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::ForceUnlockImmediate(RoR2.UnlockableDef)' called on client");
				return;
			}
			this.unlockablesUnlockedByAnyUser.Add(unlockableDef);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0009869C File Offset: 0x0009689C
		public void PickNextStageSceneFromCurrentSceneDestinations()
		{
			WeightedSelection<SceneDef> weightedSelection = new WeightedSelection<SceneDef>(8);
			SceneCatalog.mostRecentSceneDef.AddDestinationsToWeightedSelection(weightedSelection, new Func<SceneDef, bool>(this.CanPickStage));
			this.PickNextStageScene(weightedSelection);
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000986CE File Offset: 0x000968CE
		public bool CanPickStage(SceneDef sceneDef)
		{
			return !sceneDef.requiredExpansion || this.IsExpansionEnabled(sceneDef.requiredExpansion);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000986EC File Offset: 0x000968EC
		public unsafe void PickNextStageScene(WeightedSelection<SceneDef> choices)
		{
			if (choices.Count == 0)
			{
				return;
			}
			if (this.ruleBook.stageOrder == StageOrder.Normal)
			{
				this.nextStageScene = choices.Evaluate(this.nextStageRng.nextNormalizedFloat);
				return;
			}
			SceneDef[] array = SceneCatalog.allStageSceneDefs.Where(new Func<SceneDef, bool>(this.<PickNextStageScene>g__IsValidNextStage|123_0)).ToArray<SceneDef>();
			this.nextStageScene = *this.nextStageRng.NextElementUniform<SceneDef>(array);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0009875B File Offset: 0x0009695B
		public virtual ulong GenerateSeedForNewRun()
		{
			return RoR2Application.rng.nextUlong;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00098768 File Offset: 0x00096968
		protected virtual void BuildUnlockAvailability()
		{
			this.unlockablesUnlockedByAnyUser.Clear();
			this.unlockablesUnlockedByAllUsers.Clear();
			this.unlockablesAlreadyFullyObtained.Clear();
			int num = 0;
			Dictionary<UnlockableDef, int> dictionary = new Dictionary<UnlockableDef, int>();
			foreach (NetworkUser networkUser in NetworkUser.readOnlyInstancesList)
			{
				if (networkUser.isParticipating)
				{
					num++;
					foreach (UnlockableDef unlockableDef in networkUser.unlockables)
					{
						this.unlockablesUnlockedByAnyUser.Add(unlockableDef);
						if (!dictionary.ContainsKey(unlockableDef))
						{
							dictionary.Add(unlockableDef, 0);
						}
						Dictionary<UnlockableDef, int> dictionary2 = dictionary;
						UnlockableDef key = unlockableDef;
						int value = dictionary2[key] + 1;
						dictionary2[key] = value;
					}
				}
			}
			if (num > 0)
			{
				foreach (KeyValuePair<UnlockableDef, int> keyValuePair in dictionary)
				{
					if (keyValuePair.Value == num)
					{
						this.unlockablesUnlockedByAllUsers.Add(keyValuePair.Key);
						this.unlockablesAlreadyFullyObtained.Add(keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000988CC File Offset: 0x00096ACC
		protected virtual void Start()
		{
			this.OnRuleBookUpdated(this.networkRuleBookComponent);
			if (NetworkServer.active)
			{
				this.runRNG = new Xoroshiro128Plus(this.seed);
				this.nextStageRng = new Xoroshiro128Plus(this.runRNG.nextUlong);
				this.stageRngGenerator = new Xoroshiro128Plus(this.runRNG.nextUlong);
				this.GenerateStageRNG();
			}
			this.allowNewParticipants = true;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				this.OnUserAdded(readOnlyInstancesList[i]);
			}
			this.allowNewParticipants = false;
			if (NetworkServer.active)
			{
				WeightedSelection<SceneDef> weightedSelection = new WeightedSelection<SceneDef>(8);
				string @string = Run.cvRunSceneOverride.GetString();
				if (@string != "")
				{
					weightedSelection.AddChoice(SceneCatalog.GetSceneDefFromSceneName(@string), 1f);
				}
				else if (this.startingSceneGroup)
				{
					this.startingSceneGroup.AddToWeightedSelection(weightedSelection, new Func<SceneDef, bool>(this.CanPickStage));
				}
				else
				{
					for (int j = 0; j < this.startingScenes.Length; j++)
					{
						if (this.CanPickStage(this.startingScenes[j]))
						{
							weightedSelection.AddChoice(this.startingScenes[j], 1f);
						}
					}
				}
				this.PickNextStageScene(weightedSelection);
				if (!this.nextStageScene)
				{
					Debug.LogError("Cannot set next scene. nextStageScene is null!");
				}
				NetworkManager.singleton.ServerChangeScene(this.nextStageScene.cachedName);
			}
			this.BuildUnlockAvailability();
			this.BuildDropTable();
			Action<Run> action = Run.onRunStartGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x00098A5C File Offset: 0x00096C5C
		protected virtual void OnDestroy()
		{
			Action<Run> action = Run.onRunDestroyGlobal;
			if (action != null)
			{
				action(this);
			}
			if (GameOverController.instance)
			{
				UnityEngine.Object.Destroy(GameOverController.instance.gameObject);
			}
			ReadOnlyCollection<CharacterBody> readOnlyInstancesList = CharacterBody.readOnlyInstancesList;
			for (int i = readOnlyInstancesList.Count - 1; i >= 0; i--)
			{
				if (readOnlyInstancesList[i])
				{
					UnityEngine.Object.Destroy(readOnlyInstancesList[i].gameObject);
				}
			}
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList2 = CharacterMaster.readOnlyInstancesList;
			for (int j = readOnlyInstancesList2.Count - 1; j >= 0; j--)
			{
				if (readOnlyInstancesList2[j])
				{
					UnityEngine.Object.Destroy(readOnlyInstancesList2[j].gameObject);
				}
			}
			if (Stage.instance)
			{
				UnityEngine.Object.Destroy(Stage.instance.gameObject);
			}
			Chat.Clear();
			if (!this.shutdown && PlatformSystems.networkManager.isNetworkActive)
			{
				this.HandlePostRunDestination();
			}
			ItemMask.Return(this.availableItems);
			ItemMask.Return(this.expansionLockedItems);
			EquipmentMask.Return(this.availableEquipment);
			EquipmentMask.Return(this.expansionLockedEquipment);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x00098B6B File Offset: 0x00096D6B
		protected virtual void HandlePostRunDestination()
		{
			if (NetworkServer.active)
			{
				NetworkManager.singleton.ServerChangeScene("lobby");
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00098B83 File Offset: 0x00096D83
		protected void OnApplicationQuit()
		{
			this.shutdown = true;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00098B8C File Offset: 0x00096D8C
		[Server]
		public CharacterMaster GetUserMaster(NetworkUserId networkUserId)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.CharacterMaster RoR2.Run::GetUserMaster(RoR2.NetworkUserId)' called on client");
				return null;
			}
			CharacterMaster result;
			this.userMasters.TryGetValue(networkUserId, out result);
			return result;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00098BCA File Offset: 0x00096DCA
		[Server]
		public void OnServerSceneChanged(string sceneName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::OnServerSceneChanged(System.String)' called on client");
				return;
			}
			this.BeginStage();
			this.isGameOverServer = false;
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00098BEE File Offset: 0x00096DEE
		[Server]
		private void BeginStage()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::BeginStage()' called on client");
				return;
			}
			NetworkServer.Spawn(UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Stage")));
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00098C19 File Offset: 0x00096E19
		[Server]
		private void EndStage()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::EndStage()' called on client");
				return;
			}
			if (Stage.instance)
			{
				UnityEngine.Object.Destroy(Stage.instance);
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00098C46 File Offset: 0x00096E46
		public void OnUserAdded(NetworkUser user)
		{
			if (NetworkServer.active)
			{
				this.SetupUserCharacterMaster(user);
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnUserRemoved(NetworkUser user)
		{
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00098C56 File Offset: 0x00096E56
		public virtual bool ShouldAllowNewParticipant(NetworkUser user)
		{
			if (!this.allowNewParticipants)
			{
				RuleChoiceDef ruleChoice = this.ruleBook.GetRuleChoice(RuleCatalog.FindRuleDef("Misc.AllowDropIn"));
				return (bool)((ruleChoice != null) ? ruleChoice.extraData : null);
			}
			return true;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00098C88 File Offset: 0x00096E88
		[Server]
		private void SetupUserCharacterMaster(NetworkUser user)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::SetupUserCharacterMaster(RoR2.NetworkUser)' called on client");
				return;
			}
			if (user.masterObject)
			{
				return;
			}
			CharacterMaster characterMaster = this.GetUserMaster(user.id);
			bool flag = !characterMaster && this.ShouldAllowNewParticipant(user);
			if (flag)
			{
				characterMaster = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/CommandoMaster"), Vector3.zero, Quaternion.identity).GetComponent<CharacterMaster>();
				this.userMasters[user.id] = characterMaster;
				characterMaster.GiveMoney(this.ruleBook.startingMoney);
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(this.selectedDifficulty);
				if (this.selectedDifficulty == DifficultyIndex.Easy)
				{
					characterMaster.inventory.GiveItem(RoR2Content.Items.DrizzlePlayerHelper, 1);
				}
				else if (difficultyDef.countsAsHardMode)
				{
					characterMaster.inventory.GiveItem(RoR2Content.Items.MonsoonPlayerHelper, 1);
				}
				NetworkServer.Spawn(characterMaster.gameObject);
			}
			PlayerCharacterMasterController component = characterMaster.GetComponent<PlayerCharacterMasterController>();
			component.LinkToNetworkUserServer(user);
			if (flag)
			{
				Action<Run, PlayerCharacterMasterController> action = Run.onPlayerFirstCreatedServer;
				if (action == null)
				{
					return;
				}
				action(this, component);
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002373 RID: 9075 RVA: 0x00098D8C File Offset: 0x00096F8C
		// (remove) Token: 0x06002374 RID: 9076 RVA: 0x00098DC0 File Offset: 0x00096FC0
		public static event Action<Run, PlayerCharacterMasterController> onPlayerFirstCreatedServer;

		// Token: 0x06002375 RID: 9077 RVA: 0x00098DF4 File Offset: 0x00096FF4
		[Server]
		public virtual void HandlePlayerFirstEntryAnimation(CharacterBody body, Vector3 spawnPosition, Quaternion spawnRotation)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::HandlePlayerFirstEntryAnimation(RoR2.CharacterBody,UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
				return;
			}
			if (!body)
			{
				Debug.LogError("Can't handle player first entry animation for null body.");
				return;
			}
			if (body.preferredPodPrefab)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(body.preferredPodPrefab, body.transform.position, spawnRotation);
				VehicleSeat component = gameObject.GetComponent<VehicleSeat>();
				if (component)
				{
					component.AssignPassenger(body.gameObject);
				}
				else
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Body ",
						body.name,
						" has preferred pod ",
						body.preferredPodPrefab.name,
						", but that object has no VehicleSeat."
					}));
				}
				NetworkServer.Spawn(gameObject);
				return;
			}
			body.SetBodyStateToPreferredInitialState();
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnServerBossAdded(BossGroup bossGroup, CharacterMaster characterMaster)
		{
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnServerBossDefeated(BossGroup bossGroup)
		{
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnServerCharacterBodySpawned(CharacterBody characterBody)
		{
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnServerTeleporterPlaced(SceneDirector sceneDirector, GameObject teleporter)
		{
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnPlayerSpawnPointsPlaced(SceneDirector sceneDirector)
		{
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00098EBB File Offset: 0x000970BB
		public virtual GameObject GetTeleportEffectPrefab(GameObject objectToTeleport)
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TeleportOutBoom");
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00098EC7 File Offset: 0x000970C7
		public int GetDifficultyScaledCost(int baseCost, float difficultyCoefficient)
		{
			return (int)((float)baseCost * Mathf.Pow(difficultyCoefficient, 1.25f));
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x00098ED8 File Offset: 0x000970D8
		public int GetDifficultyScaledCost(int baseCost)
		{
			return this.GetDifficultyScaledCost(baseCost, Run.instance.difficultyCoefficient);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x00098EEC File Offset: 0x000970EC
		public void BuildDropTable()
		{
			this.availableTier1DropList.Clear();
			this.availableTier2DropList.Clear();
			this.availableTier3DropList.Clear();
			this.availableLunarItemDropList.Clear();
			this.availableEquipmentDropList.Clear();
			this.availableBossDropList.Clear();
			this.availableLunarEquipmentDropList.Clear();
			this.availableVoidTier1DropList.Clear();
			this.availableVoidTier2DropList.Clear();
			this.availableVoidTier3DropList.Clear();
			this.availableVoidBossDropList.Clear();
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				if (this.availableItems.Contains(itemIndex))
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
					List<PickupIndex> list = null;
					switch (itemDef.tier)
					{
					case ItemTier.Tier1:
						list = this.availableTier1DropList;
						break;
					case ItemTier.Tier2:
						list = this.availableTier2DropList;
						break;
					case ItemTier.Tier3:
						list = this.availableTier3DropList;
						break;
					case ItemTier.Lunar:
						list = this.availableLunarItemDropList;
						break;
					case ItemTier.Boss:
						list = this.availableBossDropList;
						break;
					case ItemTier.VoidTier1:
						list = this.availableVoidTier1DropList;
						break;
					case ItemTier.VoidTier2:
						list = this.availableVoidTier2DropList;
						break;
					case ItemTier.VoidTier3:
						list = this.availableVoidTier3DropList;
						break;
					case ItemTier.VoidBoss:
						list = this.availableVoidBossDropList;
						break;
					}
					if (list != null && itemDef.DoesNotContainTag(ItemTag.WorldUnique))
					{
						list.Add(PickupCatalog.FindPickupIndex(itemIndex));
					}
				}
				itemIndex++;
			}
			EquipmentIndex equipmentIndex = (EquipmentIndex)0;
			EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
			while (equipmentIndex < equipmentCount)
			{
				if (this.availableEquipment.Contains(equipmentIndex))
				{
					EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
					if (equipmentDef.canDrop)
					{
						if (!equipmentDef.isLunar)
						{
							this.availableEquipmentDropList.Add(PickupCatalog.FindPickupIndex(equipmentIndex));
						}
						else
						{
							this.availableLunarEquipmentDropList.Add(PickupCatalog.FindPickupIndex(equipmentIndex));
						}
					}
				}
				equipmentIndex++;
			}
			this.smallChestDropTierSelector.Clear();
			this.smallChestDropTierSelector.AddChoice(this.availableTier1DropList, 0.8f);
			this.smallChestDropTierSelector.AddChoice(this.availableTier2DropList, 0.2f);
			this.smallChestDropTierSelector.AddChoice(this.availableTier3DropList, 0.01f);
			this.mediumChestDropTierSelector.Clear();
			this.mediumChestDropTierSelector.AddChoice(this.availableTier2DropList, 0.8f);
			this.mediumChestDropTierSelector.AddChoice(this.availableTier3DropList, 0.2f);
			this.largeChestDropTierSelector.Clear();
			this.RefreshLunarCombinedDropList();
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x00099142 File Offset: 0x00097342
		public bool IsItemAvailable(ItemIndex itemIndex)
		{
			return this.availableItems.Contains(itemIndex);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00099150 File Offset: 0x00097350
		public bool IsEquipmentAvailable(EquipmentIndex equipmentIndex)
		{
			return this.availableEquipment.Contains(equipmentIndex);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0009915E File Offset: 0x0009735E
		public bool IsItemExpansionLocked(ItemIndex itemIndex)
		{
			return this.expansionLockedItems.Contains(itemIndex);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0009916C File Offset: 0x0009736C
		public bool IsEquipmentExpansionLocked(EquipmentIndex equipmentIndex)
		{
			return this.expansionLockedEquipment.Contains(equipmentIndex);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0009917C File Offset: 0x0009737C
		public bool IsPickupAvailable(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef.itemIndex != ItemIndex.None)
			{
				return this.IsItemAvailable(pickupDef.itemIndex);
			}
			return pickupDef.equipmentIndex == EquipmentIndex.None || this.IsEquipmentAvailable(pickupDef.equipmentIndex);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000991C0 File Offset: 0x000973C0
		[Server]
		public void DisablePickupDrop(PickupIndex pickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::DisablePickupDrop(RoR2.PickupIndex)' called on client");
				return;
			}
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef.itemIndex != ItemIndex.None)
			{
				this.DisableItemDrop(pickupDef.itemIndex);
			}
			if (pickupDef.equipmentIndex != EquipmentIndex.None)
			{
				this.DisableEquipmentDrop(pickupDef.equipmentIndex);
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00099214 File Offset: 0x00097414
		[Server]
		public void DisableItemDrop(ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::DisableItemDrop(RoR2.ItemIndex)' called on client");
				return;
			}
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			List<PickupIndex> list = null;
			bool flag = false;
			switch (itemDef.tier)
			{
			case ItemTier.Tier1:
				list = this.availableTier1DropList;
				break;
			case ItemTier.Tier2:
				list = this.availableTier2DropList;
				break;
			case ItemTier.Tier3:
				list = this.availableTier3DropList;
				break;
			case ItemTier.Lunar:
				list = this.availableLunarItemDropList;
				break;
			case ItemTier.Boss:
				list = this.availableBossDropList;
				break;
			case ItemTier.VoidTier1:
				list = this.availableVoidTier1DropList;
				break;
			case ItemTier.VoidTier2:
				list = this.availableVoidTier2DropList;
				break;
			case ItemTier.VoidTier3:
				list = this.availableVoidTier3DropList;
				break;
			case ItemTier.VoidBoss:
				list = this.availableVoidBossDropList;
				break;
			}
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
			if (list != null && pickupIndex != PickupIndex.none)
			{
				list.Remove(pickupIndex);
				if (flag)
				{
					this.RefreshLunarCombinedDropList();
				}
				Action<Run> action = Run.onAvailablePickupsModified;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00099300 File Offset: 0x00097500
		[Server]
		public void DisableEquipmentDrop(EquipmentIndex equipmentIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::DisableEquipmentDrop(RoR2.EquipmentIndex)' called on client");
				return;
			}
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(equipmentIndex);
			if (pickupIndex != PickupIndex.none)
			{
				bool flag = false;
				List<PickupIndex> list;
				if (PickupCatalog.GetPickupDef(pickupIndex).isLunar)
				{
					flag = true;
					list = this.availableLunarEquipmentDropList;
				}
				else
				{
					list = this.availableEquipmentDropList;
				}
				if (list.Contains(pickupIndex))
				{
					list.Remove(pickupIndex);
					if (flag)
					{
						this.RefreshLunarCombinedDropList();
					}
					Action<Run> action = Run.onAvailablePickupsModified;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00099384 File Offset: 0x00097584
		[Server]
		public void EnablePickupDrop(PickupIndex pickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::EnablePickupDrop(RoR2.PickupIndex)' called on client");
				return;
			}
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef.itemIndex != ItemIndex.None)
			{
				this.EnableItemDrop(pickupDef.itemIndex);
			}
			if (pickupDef.equipmentIndex != EquipmentIndex.None)
			{
				this.EnableEquipmentDrop(pickupDef.equipmentIndex);
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000993D8 File Offset: 0x000975D8
		[Server]
		public void EnableItemDrop(ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::EnableItemDrop(RoR2.ItemIndex)' called on client");
				return;
			}
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			List<PickupIndex> list = null;
			bool flag = false;
			switch (itemDef.tier)
			{
			case ItemTier.Tier1:
				list = this.availableTier1DropList;
				break;
			case ItemTier.Tier2:
				list = this.availableTier2DropList;
				break;
			case ItemTier.Tier3:
				list = this.availableTier3DropList;
				break;
			case ItemTier.Lunar:
				list = this.availableLunarItemDropList;
				break;
			case ItemTier.Boss:
				list = this.availableBossDropList;
				break;
			case ItemTier.VoidTier1:
				list = this.availableVoidTier1DropList;
				break;
			case ItemTier.VoidTier2:
				list = this.availableVoidTier2DropList;
				break;
			case ItemTier.VoidTier3:
				list = this.availableVoidTier3DropList;
				break;
			case ItemTier.VoidBoss:
				list = this.availableVoidBossDropList;
				break;
			}
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
			if (list != null && pickupIndex != PickupIndex.none && !list.Contains(pickupIndex))
			{
				list.Add(pickupIndex);
				if (flag)
				{
					this.RefreshLunarCombinedDropList();
				}
				Action<Run> action = Run.onAvailablePickupsModified;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000994CC File Offset: 0x000976CC
		[Server]
		public void EnableEquipmentDrop(EquipmentIndex equipmentIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::EnableEquipmentDrop(RoR2.EquipmentIndex)' called on client");
				return;
			}
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(equipmentIndex);
			if (pickupIndex != PickupIndex.none)
			{
				bool flag = false;
				List<PickupIndex> list;
				if (PickupCatalog.GetPickupDef(pickupIndex).isLunar)
				{
					list = this.availableLunarEquipmentDropList;
				}
				else
				{
					list = this.availableEquipmentDropList;
				}
				if (!list.Contains(pickupIndex))
				{
					list.Add(pickupIndex);
					if (flag)
					{
						this.RefreshLunarCombinedDropList();
					}
					Action<Run> action = Run.onAvailablePickupsModified;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0009954C File Offset: 0x0009774C
		[Server]
		private void RefreshLunarCombinedDropList()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::RefreshLunarCombinedDropList()' called on client");
				return;
			}
			this.availableLunarCombinedDropList.Clear();
			this.availableLunarCombinedDropList.AddRange(this.availableLunarEquipmentDropList);
			this.availableLunarCombinedDropList.AddRange(this.availableLunarItemDropList);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0009959B File Offset: 0x0009779B
		[ConCommand(commandName = "run_end", flags = ConVarFlags.SenderMustBeServer, helpText = "Ends the current run.")]
		private static void CCRunEnd(ConCommandArgs args)
		{
			if (Run.instance)
			{
				UnityEngine.Object.Destroy(Run.instance.gameObject);
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000995B8 File Offset: 0x000977B8
		[ConCommand(commandName = "run_print_unlockables", flags = ConVarFlags.SenderMustBeServer, helpText = "Prints all unlockables available in this run.")]
		private static void CCRunPrintUnlockables(ConCommandArgs args)
		{
			if (!Run.instance)
			{
				throw new ConCommandException("No run is currently in progress.");
			}
			List<string> list = new List<string>();
			foreach (UnlockableDef unlockableDef in Run.instance.unlockablesUnlockedByAnyUser)
			{
				list.Add(unlockableDef.cachedName);
			}
			Debug.Log(string.Join("\n", list.ToArray()));
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00099648 File Offset: 0x00097848
		[ConCommand(commandName = "run_print_seed", flags = ConVarFlags.None, helpText = "Prints the seed of the current run.")]
		private static void CCRunPrintSeed(ConCommandArgs args)
		{
			if (!Run.instance)
			{
				throw new ConCommandException("No run is currently in progress.");
			}
			Debug.LogFormat("Current run seed: {0}", new object[]
			{
				Run.instance.seed
			});
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00099683 File Offset: 0x00097883
		[ConCommand(commandName = "run_set_stages_cleared", flags = (ConVarFlags.ExecuteOnServer | ConVarFlags.Cheat), helpText = "Sets the current number of stages cleared in the run.")]
		private static void CCRunSetStagesCleared(ConCommandArgs args)
		{
			if (!Run.instance)
			{
				throw new ConCommandException("No run is currently in progress.");
			}
			Run.instance.NetworkstageClearCount = args.GetArgInt(0);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000996B0 File Offset: 0x000978B0
		public virtual void AdvanceStage(SceneDef nextScene)
		{
			if (Stage.instance)
			{
				Stage.instance.CompleteServer();
				if (SceneCatalog.GetSceneDefForCurrentScene().sceneType == SceneType.Stage)
				{
					this.NetworkstageClearCount = this.stageClearCount + 1;
				}
			}
			this.GenerateStageRNG();
			this.RecalculateDifficultyCoefficent();
			NetworkManager.singleton.ServerChangeScene(nextScene.cachedName);
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0009970A File Offset: 0x0009790A
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x00099712 File Offset: 0x00097912
		public bool isGameOverServer { get; private set; }

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002392 RID: 9106 RVA: 0x0009971C File Offset: 0x0009791C
		// (remove) Token: 0x06002393 RID: 9107 RVA: 0x00099750 File Offset: 0x00097950
		public static event Action<Run, GameEndingDef> onServerGameOver;

		// Token: 0x06002394 RID: 9108 RVA: 0x00099784 File Offset: 0x00097984
		public void BeginGameOver([NotNull] GameEndingDef gameEndingDef)
		{
			if (this.isGameOverServer)
			{
				return;
			}
			if (Stage.instance && gameEndingDef.isWin)
			{
				Stage.instance.CompleteServer();
			}
			this.isGameOverServer = true;
			if (gameEndingDef.lunarCoinReward > 0U)
			{
				for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
				{
					NetworkUser networkUser = NetworkUser.readOnlyInstancesList[i];
					if (networkUser && networkUser.isParticipating)
					{
						networkUser.AwardLunarCoins(gameEndingDef.lunarCoinReward);
					}
				}
			}
			StatManager.ForceUpdate();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.gameOverPrefab);
			GameOverController component = gameObject.GetComponent<GameOverController>();
			component.SetRunReport(RunReport.Generate(this, gameEndingDef));
			Action<Run, GameEndingDef> action = Run.onServerGameOver;
			if (action != null)
			{
				action(this, gameEndingDef);
			}
			NetworkServer.Spawn(gameObject);
			component.CallRpcClientGameOver();
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00099846 File Offset: 0x00097A46
		public virtual void OnClientGameOver(RunReport runReport)
		{
			Action<Run, RunReport> action = Run.onClientGameOverGlobal;
			if (action == null)
			{
				return;
			}
			action(this, runReport);
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002396 RID: 9110 RVA: 0x0009985C File Offset: 0x00097A5C
		// (remove) Token: 0x06002397 RID: 9111 RVA: 0x00099890 File Offset: 0x00097A90
		public static event Action<Run, RunReport> onClientGameOverGlobal;

		// Token: 0x06002398 RID: 9112 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OverrideRuleChoices(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000998C4 File Offset: 0x00097AC4
		protected void ForceChoice(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, RuleChoiceDef choiceDef)
		{
			foreach (RuleChoiceDef ruleChoiceDef in choiceDef.ruleDef.choices)
			{
				mustInclude[ruleChoiceDef.globalIndex] = false;
				mustExclude[ruleChoiceDef.globalIndex] = true;
			}
			mustInclude[choiceDef.globalIndex] = true;
			mustExclude[choiceDef.globalIndex] = false;
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x0009994C File Offset: 0x00097B4C
		protected void ForceChoice(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, string choiceDefGlobalName)
		{
			this.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindChoiceDef(choiceDefGlobalName));
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0009995C File Offset: 0x00097B5C
		public virtual Vector3 FindSafeTeleportPosition(CharacterBody characterBody, Transform targetDestination)
		{
			return this.FindSafeTeleportPosition(characterBody, targetDestination, float.NegativeInfinity, float.NegativeInfinity);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x00099970 File Offset: 0x00097B70
		public virtual Vector3 FindSafeTeleportPosition(CharacterBody characterBody, Transform targetDestination, float idealMinDistance, float idealMaxDistance)
		{
			Vector3 vector = targetDestination ? targetDestination.position : characterBody.transform.position;
			SpawnCard spawnCard = ScriptableObject.CreateInstance<SpawnCard>();
			spawnCard.hullSize = characterBody.hullClassification;
			spawnCard.nodeGraphType = MapNodeGroup.GraphType.Ground;
			spawnCard.prefab = LegacyResourcesAPI.Load<GameObject>("SpawnCards/HelperPrefab");
			Vector3 result = vector;
			GameObject gameObject = null;
			if (idealMaxDistance > 0f && idealMinDistance < idealMaxDistance)
			{
				gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Approximate,
					minDistance = idealMinDistance,
					maxDistance = idealMaxDistance,
					position = vector
				}, RoR2Application.rng));
			}
			if (!gameObject)
			{
				gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
					position = vector
				}, RoR2Application.rng));
				if (gameObject)
				{
					result = gameObject.transform.position;
				}
			}
			if (gameObject)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			UnityEngine.Object.Destroy(spawnCard);
			return result;
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600239D RID: 9117 RVA: 0x00099A68 File Offset: 0x00097C68
		// (remove) Token: 0x0600239E RID: 9118 RVA: 0x00099A9C File Offset: 0x00097C9C
		public static event Action<Run> onRunStartGlobal;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x0600239F RID: 9119 RVA: 0x00099AD0 File Offset: 0x00097CD0
		// (remove) Token: 0x060023A0 RID: 9120 RVA: 0x00099B04 File Offset: 0x00097D04
		public static event Action<Run> onRunDestroyGlobal;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060023A1 RID: 9121 RVA: 0x00099B38 File Offset: 0x00097D38
		// (remove) Token: 0x060023A2 RID: 9122 RVA: 0x00099B6C File Offset: 0x00097D6C
		public static event Action<Run> onAvailablePickupsModified;

		// Token: 0x060023A3 RID: 9123 RVA: 0x00099B9F File Offset: 0x00097D9F
		[Server]
		public void SetEventFlag(string name)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::SetEventFlag(System.String)' called on client");
				return;
			}
			this.eventFlags.Add(name);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x00099BC3 File Offset: 0x00097DC3
		[Server]
		public bool GetEventFlag(string name)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.Run::GetEventFlag(System.String)' called on client");
				return false;
			}
			return this.eventFlags.Contains(name);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00099BE7 File Offset: 0x00097DE7
		[Server]
		public void ResetEventFlag(string name)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Run::ResetEventFlag(System.String)' called on client");
				return;
			}
			this.eventFlags.Remove(name);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00099C0B File Offset: 0x00097E0B
		public virtual bool ShouldAllowNonChampionBossSpawn()
		{
			return this.stageClearCount > 0;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00099C16 File Offset: 0x00097E16
		public bool IsExpansionEnabled([NotNull] ExpansionDef expansionDef)
		{
			return expansionDef.enabledChoice != null && this.ruleBook.IsChoiceActive(expansionDef.enabledChoice);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00099C33 File Offset: 0x00097E33
		protected virtual void OnSeedSet()
		{
			Debug.Log(string.Format("Run seed:  {0}", this.seed));
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00099DB6 File Offset: 0x00097FB6
		[CompilerGenerated]
		private bool <PickNextStageScene>g__IsValidNextStage|123_0(SceneDef sceneDef)
		{
			return (!(this.nextStageScene != null) || !(this.nextStageScene.baseSceneName == sceneDef.baseSceneName)) && sceneDef.hasAnyDestinations && sceneDef.validForRandomSelection;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00099DF0 File Offset: 0x00097FF0
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x00099E03 File Offset: 0x00098003
		public NetworkGuid Network_uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<NetworkGuid>(value, ref this._uniqueId, 1U);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00099E18 File Offset: 0x00098018
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00099E2B File Offset: 0x0009802B
		public NetworkDateTime NetworkstartTimeUtc
		{
			get
			{
				return this.startTimeUtc;
			}
			[param: In]
			set
			{
				base.SetSyncVar<NetworkDateTime>(value, ref this.startTimeUtc, 2U);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00099E40 File Offset: 0x00098040
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x00099E53 File Offset: 0x00098053
		public float NetworkfixedTime
		{
			get
			{
				return this.fixedTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.fixedTime, 4U);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00099E68 File Offset: 0x00098068
		// (set) Token: 0x060023B4 RID: 9140 RVA: 0x00099E7B File Offset: 0x0009807B
		public Run.RunStopwatch NetworkrunStopwatch
		{
			get
			{
				return this.runStopwatch;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Run.RunStopwatch>(value, ref this.runStopwatch, 8U);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00099E90 File Offset: 0x00098090
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00099EA3 File Offset: 0x000980A3
		public int NetworkstageClearCount
		{
			get
			{
				return this.stageClearCount;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.stageClearCount, 16U);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x00099EB8 File Offset: 0x000980B8
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x00099ECB File Offset: 0x000980CB
		public int NetworkselectedDifficultyInternal
		{
			get
			{
				return this.selectedDifficultyInternal;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.selectedDifficultyInternal, 32U);
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x00099EE0 File Offset: 0x000980E0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteNetworkGuid_None(writer, this._uniqueId);
				GeneratedNetworkCode._WriteNetworkDateTime_None(writer, this.startTimeUtc);
				writer.Write(this.fixedTime);
				GeneratedNetworkCode._WriteRunStopwatch_Run(writer, this.runStopwatch);
				writer.WritePackedUInt32((uint)this.stageClearCount);
				writer.WritePackedUInt32((uint)this.selectedDifficultyInternal);
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
				GeneratedNetworkCode._WriteNetworkGuid_None(writer, this._uniqueId);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				GeneratedNetworkCode._WriteNetworkDateTime_None(writer, this.startTimeUtc);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.fixedTime);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				GeneratedNetworkCode._WriteRunStopwatch_Run(writer, this.runStopwatch);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.stageClearCount);
			}
			if ((base.syncVarDirtyBits & 32U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.selectedDifficultyInternal);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0009A088 File Offset: 0x00098288
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._uniqueId = GeneratedNetworkCode._ReadNetworkGuid_None(reader);
				this.startTimeUtc = GeneratedNetworkCode._ReadNetworkDateTime_None(reader);
				this.fixedTime = reader.ReadSingle();
				this.runStopwatch = GeneratedNetworkCode._ReadRunStopwatch_Run(reader);
				this.stageClearCount = (int)reader.ReadPackedUInt32();
				this.selectedDifficultyInternal = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._uniqueId = GeneratedNetworkCode._ReadNetworkGuid_None(reader);
			}
			if ((num & 2) != 0)
			{
				this.startTimeUtc = GeneratedNetworkCode._ReadNetworkDateTime_None(reader);
			}
			if ((num & 4) != 0)
			{
				this.fixedTime = reader.ReadSingle();
			}
			if ((num & 8) != 0)
			{
				this.runStopwatch = GeneratedNetworkCode._ReadRunStopwatch_Run(reader);
			}
			if ((num & 16) != 0)
			{
				this.stageClearCount = (int)reader.ReadPackedUInt32();
			}
			if ((num & 32) != 0)
			{
				this.selectedDifficultyInternal = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002820 RID: 10272
		private NetworkRuleBook networkRuleBookComponent;

		// Token: 0x04002821 RID: 10273
		[Tooltip("This is assigned to the prefab automatically by GameModeCatalog at runtime. Do not set this value manually.")]
		[HideInInspector]
		public GameModeIndex gameModeIndex = GameModeIndex.Invalid;

		// Token: 0x04002822 RID: 10274
		public string nameToken = "";

		// Token: 0x04002823 RID: 10275
		[Tooltip("Whether or not the user can select this game mode for play in the game mode selector UI.")]
		public bool userPickable = true;

		// Token: 0x04002824 RID: 10276
		public static int stagesPerLoop = 5;

		// Token: 0x04002825 RID: 10277
		public static float baseGravity = -30f;

		// Token: 0x04002828 RID: 10280
		[SyncVar]
		private NetworkGuid _uniqueId;

		// Token: 0x04002829 RID: 10281
		[SyncVar]
		private NetworkDateTime startTimeUtc;

		// Token: 0x0400282A RID: 10282
		[Tooltip("The pool of scenes to select the first scene of the run from.")]
		[ShowFieldObsolete]
		[Obsolete("Use startingSceneGroup instead.")]
		public SceneDef[] startingScenes = Array.Empty<SceneDef>();

		// Token: 0x0400282B RID: 10283
		[Tooltip("The pool of scenes to select the first scene of the run from.")]
		public SceneCollection startingSceneGroup;

		// Token: 0x0400282C RID: 10284
		public ItemMask availableItems;

		// Token: 0x0400282D RID: 10285
		public ItemMask expansionLockedItems;

		// Token: 0x0400282E RID: 10286
		public EquipmentMask availableEquipment;

		// Token: 0x0400282F RID: 10287
		public EquipmentMask expansionLockedEquipment;

		// Token: 0x04002830 RID: 10288
		[SyncVar]
		public float fixedTime;

		// Token: 0x04002831 RID: 10289
		public float time;

		// Token: 0x04002832 RID: 10290
		[SyncVar]
		private Run.RunStopwatch runStopwatch;

		// Token: 0x04002833 RID: 10291
		[SyncVar]
		public int stageClearCount;

		// Token: 0x04002834 RID: 10292
		public SceneDef nextStageScene;

		// Token: 0x04002835 RID: 10293
		public GameObject gameOverPrefab;

		// Token: 0x04002836 RID: 10294
		public GameObject lobbyBackgroundPrefab;

		// Token: 0x04002837 RID: 10295
		public GameObject uiPrefab;

		// Token: 0x04002839 RID: 10297
		private ulong _seed;

		// Token: 0x0400283A RID: 10298
		public Xoroshiro128Plus runRNG;

		// Token: 0x0400283B RID: 10299
		public Xoroshiro128Plus nextStageRng;

		// Token: 0x0400283C RID: 10300
		public Xoroshiro128Plus stageRngGenerator;

		// Token: 0x0400283D RID: 10301
		public Xoroshiro128Plus stageRng;

		// Token: 0x0400283E RID: 10302
		public Xoroshiro128Plus bossRewardRng;

		// Token: 0x0400283F RID: 10303
		public Xoroshiro128Plus treasureRng;

		// Token: 0x04002840 RID: 10304
		public Xoroshiro128Plus spawnRng;

		// Token: 0x04002841 RID: 10305
		public Xoroshiro128Plus randomSurvivorOnRespawnRng;

		// Token: 0x04002842 RID: 10306
		public float difficultyCoefficient = 1f;

		// Token: 0x04002843 RID: 10307
		public float compensatedDifficultyCoefficient = 1f;

		// Token: 0x04002844 RID: 10308
		[SyncVar]
		private int selectedDifficultyInternal = 1;

		// Token: 0x04002848 RID: 10312
		public int shopPortalCount;

		// Token: 0x04002849 RID: 10313
		private static int ambientLevelCap = 99;

		// Token: 0x0400284A RID: 10314
		private static readonly StringConVar cvRunSceneOverride = new StringConVar("run_scene_override", ConVarFlags.Cheat, "", "Overrides the first scene to enter in a run.");

		// Token: 0x0400284B RID: 10315
		private readonly HashSet<UnlockableDef> unlockablesUnlockedByAnyUser = new HashSet<UnlockableDef>();

		// Token: 0x0400284C RID: 10316
		private readonly HashSet<UnlockableDef> unlockablesUnlockedByAllUsers = new HashSet<UnlockableDef>();

		// Token: 0x0400284D RID: 10317
		private readonly HashSet<UnlockableDef> unlockablesAlreadyFullyObtained = new HashSet<UnlockableDef>();

		// Token: 0x0400284E RID: 10318
		private bool shutdown;

		// Token: 0x0400284F RID: 10319
		private Dictionary<NetworkUserId, CharacterMaster> userMasters = new Dictionary<NetworkUserId, CharacterMaster>();

		// Token: 0x04002850 RID: 10320
		private bool allowNewParticipants;

		// Token: 0x04002852 RID: 10322
		public readonly List<PickupIndex> availableTier1DropList = new List<PickupIndex>();

		// Token: 0x04002853 RID: 10323
		public readonly List<PickupIndex> availableTier2DropList = new List<PickupIndex>();

		// Token: 0x04002854 RID: 10324
		public readonly List<PickupIndex> availableTier3DropList = new List<PickupIndex>();

		// Token: 0x04002855 RID: 10325
		public readonly List<PickupIndex> availableEquipmentDropList = new List<PickupIndex>();

		// Token: 0x04002856 RID: 10326
		public readonly List<PickupIndex> availableLunarEquipmentDropList = new List<PickupIndex>();

		// Token: 0x04002857 RID: 10327
		public readonly List<PickupIndex> availableLunarItemDropList = new List<PickupIndex>();

		// Token: 0x04002858 RID: 10328
		public readonly List<PickupIndex> availableLunarCombinedDropList = new List<PickupIndex>();

		// Token: 0x04002859 RID: 10329
		public readonly List<PickupIndex> availableBossDropList = new List<PickupIndex>();

		// Token: 0x0400285A RID: 10330
		public readonly List<PickupIndex> availableVoidTier1DropList = new List<PickupIndex>();

		// Token: 0x0400285B RID: 10331
		public readonly List<PickupIndex> availableVoidTier2DropList = new List<PickupIndex>();

		// Token: 0x0400285C RID: 10332
		public readonly List<PickupIndex> availableVoidTier3DropList = new List<PickupIndex>();

		// Token: 0x0400285D RID: 10333
		public readonly List<PickupIndex> availableVoidBossDropList = new List<PickupIndex>();

		// Token: 0x0400285E RID: 10334
		public WeightedSelection<List<PickupIndex>> smallChestDropTierSelector = new WeightedSelection<List<PickupIndex>>(8);

		// Token: 0x0400285F RID: 10335
		public WeightedSelection<List<PickupIndex>> mediumChestDropTierSelector = new WeightedSelection<List<PickupIndex>>(8);

		// Token: 0x04002860 RID: 10336
		public WeightedSelection<List<PickupIndex>> largeChestDropTierSelector = new WeightedSelection<List<PickupIndex>>(8);

		// Token: 0x04002867 RID: 10343
		private readonly HashSet<string> eventFlags = new HashSet<string>();

		// Token: 0x020006ED RID: 1773
		[Serializable]
		public struct RunStopwatch : IEquatable<Run.RunStopwatch>
		{
			// Token: 0x060023BC RID: 9148 RVA: 0x0009A182 File Offset: 0x00098382
			public bool Equals(Run.RunStopwatch other)
			{
				return this.offsetFromFixedTime.Equals(other.offsetFromFixedTime) && this.isPaused == other.isPaused;
			}

			// Token: 0x060023BD RID: 9149 RVA: 0x0009A1A8 File Offset: 0x000983A8
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is Run.RunStopwatch)
				{
					Run.RunStopwatch other = (Run.RunStopwatch)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x060023BE RID: 9150 RVA: 0x0009A1D4 File Offset: 0x000983D4
			public override int GetHashCode()
			{
				return this.offsetFromFixedTime.GetHashCode() * 397 ^ this.isPaused.GetHashCode();
			}

			// Token: 0x04002868 RID: 10344
			public float offsetFromFixedTime;

			// Token: 0x04002869 RID: 10345
			public bool isPaused;
		}

		// Token: 0x020006EE RID: 1774
		[Serializable]
		public struct TimeStamp : IEquatable<Run.TimeStamp>, IComparable<Run.TimeStamp>
		{
			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x060023BF RID: 9151 RVA: 0x0009A1F3 File Offset: 0x000983F3
			public float timeUntil
			{
				get
				{
					return this.t - Run.TimeStamp.tNow;
				}
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x060023C0 RID: 9152 RVA: 0x0009A201 File Offset: 0x00098401
			public float timeSince
			{
				get
				{
					return Run.TimeStamp.tNow - this.t;
				}
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x060023C1 RID: 9153 RVA: 0x0009A20F File Offset: 0x0009840F
			public float timeUntilClamped
			{
				get
				{
					return Mathf.Max(this.timeUntil, 0f);
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x060023C2 RID: 9154 RVA: 0x0009A221 File Offset: 0x00098421
			public float timeSinceClamped
			{
				get
				{
					return Mathf.Max(this.timeSince, 0f);
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x060023C3 RID: 9155 RVA: 0x0009A233 File Offset: 0x00098433
			public bool hasPassed
			{
				get
				{
					return this.t <= Run.TimeStamp.tNow;
				}
			}

			// Token: 0x060023C4 RID: 9156 RVA: 0x0009A248 File Offset: 0x00098448
			public override int GetHashCode()
			{
				return this.t.GetHashCode();
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0009A263 File Offset: 0x00098463
			public bool isInfinity
			{
				get
				{
					return float.IsInfinity(this.t);
				}
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0009A270 File Offset: 0x00098470
			public bool isPositiveInfinity
			{
				get
				{
					return float.IsPositiveInfinity(this.t);
				}
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0009A27D File Offset: 0x0009847D
			public bool isNegativeInfinity
			{
				get
				{
					return float.IsNegativeInfinity(this.t);
				}
			}

			// Token: 0x060023C8 RID: 9160 RVA: 0x0009A28A File Offset: 0x0009848A
			public static void Update()
			{
				Run.TimeStamp.tNow = Run.instance.time;
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0009A29B File Offset: 0x0009849B
			public static Run.TimeStamp now
			{
				get
				{
					return new Run.TimeStamp(Run.TimeStamp.tNow);
				}
			}

			// Token: 0x060023CA RID: 9162 RVA: 0x0009A2A7 File Offset: 0x000984A7
			private TimeStamp(float t)
			{
				this.t = t;
			}

			// Token: 0x060023CB RID: 9163 RVA: 0x0009A2B0 File Offset: 0x000984B0
			public bool Equals(Run.TimeStamp other)
			{
				return this.t.Equals(other.t);
			}

			// Token: 0x060023CC RID: 9164 RVA: 0x0009A2D1 File Offset: 0x000984D1
			public override bool Equals(object obj)
			{
				return obj is Run.TimeStamp && this.Equals((Run.TimeStamp)obj);
			}

			// Token: 0x060023CD RID: 9165 RVA: 0x0009A2EC File Offset: 0x000984EC
			public int CompareTo(Run.TimeStamp other)
			{
				return this.t.CompareTo(other.t);
			}

			// Token: 0x060023CE RID: 9166 RVA: 0x0009A30D File Offset: 0x0009850D
			public static Run.TimeStamp operator +(Run.TimeStamp a, float b)
			{
				return new Run.TimeStamp(a.t + b);
			}

			// Token: 0x060023CF RID: 9167 RVA: 0x0009A31C File Offset: 0x0009851C
			public static Run.TimeStamp operator -(Run.TimeStamp a, float b)
			{
				return new Run.TimeStamp(a.t - b);
			}

			// Token: 0x060023D0 RID: 9168 RVA: 0x0009A32B File Offset: 0x0009852B
			public static float operator -(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.t - b.t;
			}

			// Token: 0x060023D1 RID: 9169 RVA: 0x0009A33A File Offset: 0x0009853A
			public static bool operator <(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.t < b.t;
			}

			// Token: 0x060023D2 RID: 9170 RVA: 0x0009A34A File Offset: 0x0009854A
			public static bool operator >(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.t > b.t;
			}

			// Token: 0x060023D3 RID: 9171 RVA: 0x0009A35A File Offset: 0x0009855A
			public static bool operator <=(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.t <= b.t;
			}

			// Token: 0x060023D4 RID: 9172 RVA: 0x0009A36D File Offset: 0x0009856D
			public static bool operator >=(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.t >= b.t;
			}

			// Token: 0x060023D5 RID: 9173 RVA: 0x0009A380 File Offset: 0x00098580
			public static bool operator ==(Run.TimeStamp a, Run.TimeStamp b)
			{
				return a.Equals(b);
			}

			// Token: 0x060023D6 RID: 9174 RVA: 0x0009A38A File Offset: 0x0009858A
			public static bool operator !=(Run.TimeStamp a, Run.TimeStamp b)
			{
				return !a.Equals(b);
			}

			// Token: 0x060023D7 RID: 9175 RVA: 0x0009A397 File Offset: 0x00098597
			public static float operator -(Run.TimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t - b.t;
			}

			// Token: 0x060023D8 RID: 9176 RVA: 0x0009A3A6 File Offset: 0x000985A6
			public static Run.TimeStamp Deserialize(NetworkReader reader)
			{
				return new Run.TimeStamp(reader.ReadSingle());
			}

			// Token: 0x060023D9 RID: 9177 RVA: 0x0009A3B3 File Offset: 0x000985B3
			public static void Serialize(NetworkWriter writer, Run.TimeStamp timeStamp)
			{
				writer.Write(timeStamp.t);
			}

			// Token: 0x060023DA RID: 9178 RVA: 0x0009A3C2 File Offset: 0x000985C2
			public static void ToXml(XElement element, Run.TimeStamp src)
			{
				element.Value = TextSerialization.ToStringInvariant(src.t);
			}

			// Token: 0x060023DB RID: 9179 RVA: 0x0009A3D8 File Offset: 0x000985D8
			public static bool FromXml(XElement element, ref Run.TimeStamp dest)
			{
				float num;
				if (TextSerialization.TryParseInvariant(element.Value, out num))
				{
					dest = new Run.TimeStamp(num);
					return true;
				}
				return false;
			}

			// Token: 0x060023DC RID: 9180 RVA: 0x0009A404 File Offset: 0x00098604
			static TimeStamp()
			{
				HGXml.Register<Run.TimeStamp>(new HGXml.Serializer<Run.TimeStamp>(Run.TimeStamp.ToXml), new HGXml.Deserializer<Run.TimeStamp>(Run.TimeStamp.FromXml));
			}

			// Token: 0x0400286A RID: 10346
			public readonly float t;

			// Token: 0x0400286B RID: 10347
			private static float tNow;

			// Token: 0x0400286C RID: 10348
			public static readonly Run.TimeStamp zero = new Run.TimeStamp(0f);

			// Token: 0x0400286D RID: 10349
			public static readonly Run.TimeStamp positiveInfinity = new Run.TimeStamp(float.PositiveInfinity);

			// Token: 0x0400286E RID: 10350
			public static readonly Run.TimeStamp negativeInfinity = new Run.TimeStamp(float.NegativeInfinity);
		}

		// Token: 0x020006EF RID: 1775
		[Serializable]
		public struct FixedTimeStamp : IEquatable<Run.FixedTimeStamp>, IComparable<Run.FixedTimeStamp>
		{
			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x060023DD RID: 9181 RVA: 0x0009A45B File Offset: 0x0009865B
			public float timeUntil
			{
				get
				{
					return this.t - Run.FixedTimeStamp.tNow;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x060023DE RID: 9182 RVA: 0x0009A469 File Offset: 0x00098669
			public float timeSince
			{
				get
				{
					return Run.FixedTimeStamp.tNow - this.t;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x060023DF RID: 9183 RVA: 0x0009A477 File Offset: 0x00098677
			public float timeUntilClamped
			{
				get
				{
					return Mathf.Max(this.timeUntil, 0f);
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x060023E0 RID: 9184 RVA: 0x0009A489 File Offset: 0x00098689
			public float timeSinceClamped
			{
				get
				{
					return Mathf.Max(this.timeSince, 0f);
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x060023E1 RID: 9185 RVA: 0x0009A49B File Offset: 0x0009869B
			public bool hasPassed
			{
				get
				{
					return this.t <= Run.FixedTimeStamp.tNow;
				}
			}

			// Token: 0x060023E2 RID: 9186 RVA: 0x0009A4B0 File Offset: 0x000986B0
			public override int GetHashCode()
			{
				return this.t.GetHashCode();
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0009A4CB File Offset: 0x000986CB
			public bool isInfinity
			{
				get
				{
					return float.IsInfinity(this.t);
				}
			}

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0009A4D8 File Offset: 0x000986D8
			public bool isPositiveInfinity
			{
				get
				{
					return float.IsPositiveInfinity(this.t);
				}
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0009A4E5 File Offset: 0x000986E5
			public bool isNegativeInfinity
			{
				get
				{
					return float.IsNegativeInfinity(this.t);
				}
			}

			// Token: 0x060023E6 RID: 9190 RVA: 0x0009A4F2 File Offset: 0x000986F2
			public static void Update()
			{
				Run.FixedTimeStamp.tNow = Run.instance.fixedTime;
			}

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0009A503 File Offset: 0x00098703
			public static Run.FixedTimeStamp now
			{
				get
				{
					return new Run.FixedTimeStamp(Run.FixedTimeStamp.tNow);
				}
			}

			// Token: 0x060023E8 RID: 9192 RVA: 0x0009A50F File Offset: 0x0009870F
			private FixedTimeStamp(float t)
			{
				this.t = t;
			}

			// Token: 0x060023E9 RID: 9193 RVA: 0x0009A518 File Offset: 0x00098718
			public bool Equals(Run.FixedTimeStamp other)
			{
				return this.t.Equals(other.t);
			}

			// Token: 0x060023EA RID: 9194 RVA: 0x0009A539 File Offset: 0x00098739
			public override bool Equals(object obj)
			{
				return obj is Run.FixedTimeStamp && this.Equals((Run.FixedTimeStamp)obj);
			}

			// Token: 0x060023EB RID: 9195 RVA: 0x0009A554 File Offset: 0x00098754
			public int CompareTo(Run.FixedTimeStamp other)
			{
				return this.t.CompareTo(other.t);
			}

			// Token: 0x060023EC RID: 9196 RVA: 0x0009A575 File Offset: 0x00098775
			public static Run.FixedTimeStamp operator +(Run.FixedTimeStamp a, float b)
			{
				return new Run.FixedTimeStamp(a.t + b);
			}

			// Token: 0x060023ED RID: 9197 RVA: 0x0009A584 File Offset: 0x00098784
			public static Run.FixedTimeStamp operator -(Run.FixedTimeStamp a, float b)
			{
				return new Run.FixedTimeStamp(a.t - b);
			}

			// Token: 0x060023EE RID: 9198 RVA: 0x0009A593 File Offset: 0x00098793
			public static float operator -(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t - b.t;
			}

			// Token: 0x060023EF RID: 9199 RVA: 0x0009A5A2 File Offset: 0x000987A2
			public static bool operator <(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t < b.t;
			}

			// Token: 0x060023F0 RID: 9200 RVA: 0x0009A5B2 File Offset: 0x000987B2
			public static bool operator >(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t > b.t;
			}

			// Token: 0x060023F1 RID: 9201 RVA: 0x0009A5C2 File Offset: 0x000987C2
			public static bool operator <=(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t <= b.t;
			}

			// Token: 0x060023F2 RID: 9202 RVA: 0x0009A5D5 File Offset: 0x000987D5
			public static bool operator >=(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.t >= b.t;
			}

			// Token: 0x060023F3 RID: 9203 RVA: 0x0009A5E8 File Offset: 0x000987E8
			public static bool operator ==(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return a.Equals(b);
			}

			// Token: 0x060023F4 RID: 9204 RVA: 0x0009A5F2 File Offset: 0x000987F2
			public static bool operator !=(Run.FixedTimeStamp a, Run.FixedTimeStamp b)
			{
				return !a.Equals(b);
			}

			// Token: 0x060023F5 RID: 9205 RVA: 0x0009A5FF File Offset: 0x000987FF
			public static Run.FixedTimeStamp Deserialize(NetworkReader reader)
			{
				return new Run.FixedTimeStamp(reader.ReadSingle());
			}

			// Token: 0x060023F6 RID: 9206 RVA: 0x0009A60C File Offset: 0x0009880C
			public static void Serialize(NetworkWriter writer, Run.FixedTimeStamp timeStamp)
			{
				writer.Write(timeStamp.t);
			}

			// Token: 0x060023F7 RID: 9207 RVA: 0x0009A61B File Offset: 0x0009881B
			public static void ToXml(XElement element, Run.FixedTimeStamp src)
			{
				element.Value = TextSerialization.ToStringInvariant(src.t);
			}

			// Token: 0x060023F8 RID: 9208 RVA: 0x0009A630 File Offset: 0x00098830
			public static bool FromXml(XElement element, ref Run.FixedTimeStamp dest)
			{
				float num;
				if (TextSerialization.TryParseInvariant(element.Value, out num))
				{
					dest = new Run.FixedTimeStamp(num);
					return true;
				}
				return false;
			}

			// Token: 0x060023F9 RID: 9209 RVA: 0x0009A65C File Offset: 0x0009885C
			static FixedTimeStamp()
			{
				HGXml.Register<Run.FixedTimeStamp>(new HGXml.Serializer<Run.FixedTimeStamp>(Run.FixedTimeStamp.ToXml), new HGXml.Deserializer<Run.FixedTimeStamp>(Run.FixedTimeStamp.FromXml));
			}

			// Token: 0x0400286F RID: 10351
			public readonly float t;

			// Token: 0x04002870 RID: 10352
			private static float tNow;

			// Token: 0x04002871 RID: 10353
			public static readonly Run.FixedTimeStamp zero = new Run.FixedTimeStamp(0f);

			// Token: 0x04002872 RID: 10354
			public static readonly Run.FixedTimeStamp positiveInfinity = new Run.FixedTimeStamp(float.PositiveInfinity);

			// Token: 0x04002873 RID: 10355
			public static readonly Run.FixedTimeStamp negativeInfinity = new Run.FixedTimeStamp(float.NegativeInfinity);
		}
	}
}
