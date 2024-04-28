using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using RoR2.HudOverlay;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200076C RID: 1900
	[DisallowMultipleComponent]
	public class InfiniteTowerWaveController : NetworkBehaviour
	{
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06002747 RID: 10055 RVA: 0x000AA9EC File Offset: 0x000A8BEC
		// (remove) Token: 0x06002748 RID: 10056 RVA: 0x000AAA24 File Offset: 0x000A8C24
		public event Action<InfiniteTowerWaveController> onAllEnemiesDefeatedServer;

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000AAA59 File Offset: 0x000A8C59
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000AAA61 File Offset: 0x000A8C61
		public GameObject defaultEnemyIndicatorPrefab { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000AAA6A File Offset: 0x000A8C6A
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x000AAA72 File Offset: 0x000A8C72
		public bool isFinished
		{
			get
			{
				return this._isFinished;
			}
			private set
			{
				this.Network_isFinished = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000AAA7B File Offset: 0x000A8C7B
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x000AAA83 File Offset: 0x000A8C83
		public bool isTimerActive
		{
			get
			{
				return this._isTimerActive;
			}
			private set
			{
				this.Network_isTimerActive = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000AAA8C File Offset: 0x000A8C8C
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x000AAA94 File Offset: 0x000A8C94
		public float totalWaveCredits
		{
			get
			{
				return this._totalWaveCredits;
			}
			protected set
			{
				this.Network_totalWaveCredits = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x000AAA9D File Offset: 0x000A8C9D
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x000AAAA5 File Offset: 0x000A8CA5
		public float totalCreditsSpent
		{
			get
			{
				return this._totalCreditsSpent;
			}
			protected set
			{
				this.Network_totalCreditsSpent = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000AAAAE File Offset: 0x000A8CAE
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x000AAAB6 File Offset: 0x000A8CB6
		public float zoneRadiusPercentage
		{
			get
			{
				return this._zoneRadiusPercentage;
			}
			protected set
			{
				this.Network_zoneRadiusPercentage = value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000AAAC0 File Offset: 0x000A8CC0
		public float secondsRemaining
		{
			get
			{
				if (Run.instance && this._timerStart > 0f)
				{
					return Mathf.Max(0f, (float)this.secondsAfterWave - (Run.instance.GetRunStopwatch() - this._timerStart));
				}
				return (float)this.secondsAfterWave;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x000AAB14 File Offset: 0x000A8D14
		public bool isInSuddenDeath
		{
			get
			{
				return Run.instance && this._suddenDeathTimerStart > 0f && (!this.haveAllEnemiesBeenDefeated && this.HasFullProgress()) && this.secondsBeforeSuddenDeath - (Run.instance.GetRunStopwatch() - this._suddenDeathTimerStart) < 0f;
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000AAB70 File Offset: 0x000A8D70
		[Server]
		public virtual void Initialize(int waveIndex, Inventory enemyInventory, GameObject spawnTarget)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::Initialize(System.Int32,RoR2.Inventory,UnityEngine.GameObject)' called on client");
				return;
			}
			this.totalWaveCredits = (this.baseCredits + this.linearCreditsPerWave * (float)(waveIndex - 1)) * Run.instance.difficultyCoefficient;
			this.creditsPerSecond = Mathf.Max(0.1f, 1f - this.immediateCreditsFraction) * this.totalWaveCredits / this.wavePeriodSeconds;
			if (this.combatDirector)
			{
				this.combatDirector.monsterCredit += this.immediateCreditsFraction * this.totalWaveCredits;
				this.combatDirector.currentSpawnTarget = spawnTarget;
			}
			this.NetworkwaveIndex = waveIndex;
			this.enemyInventory = enemyInventory;
			this.spawnTarget = spawnTarget;
			this.rng = new Xoroshiro128Plus((ulong)((long)waveIndex ^ (long)Run.instance.seed));
			if (!string.IsNullOrEmpty(this.beginChatToken))
			{
				Chat.SendBroadcastChat(new Chat.SimpleChatMessage
				{
					baseToken = this.beginChatToken
				});
			}
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000AAC68 File Offset: 0x000A8E68
		public void InstantiateUi(Transform uiRoot)
		{
			if (uiRoot && this.uiPrefab)
			{
				this.uiInstance = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefab, uiRoot);
			}
			foreach (InfiniteTowerWaveController.OverlayEntry overlayEntry in this.overlayEntries)
			{
				OverlayController item = HudOverlayManager.AddGlobalOverlay(new OverlayCreationParams
				{
					prefab = overlayEntry.prefab,
					childLocatorEntry = overlayEntry.hudChildName
				});
				this.overlayControllerList.Add(item);
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000AACEF File Offset: 0x000A8EEF
		public void PlayBeginSound()
		{
			Util.PlaySound(this.beginSoundString, RoR2Application.instance.gameObject);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000AAD07 File Offset: 0x000A8F07
		public void PlayAllEnemiesDefeatedSound()
		{
			this.hasPlayedEnemiesDefeatedSound = true;
			Util.PlaySound(this.onAllEnemiesDefeatedSoundString, RoR2Application.instance.gameObject);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000AAD26 File Offset: 0x000A8F26
		public int GetSquadCount()
		{
			if (!NetworkServer.active)
			{
				return this.squadCount;
			}
			if (this.combatSquad)
			{
				return this.combatSquad.memberCount;
			}
			return 0;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000AAD50 File Offset: 0x000A8F50
		public virtual float GetNormalizedProgress()
		{
			if (this.totalWaveCredits != 0f)
			{
				return this.totalCreditsSpent / this.totalWaveCredits;
			}
			return 1f;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000AAD72 File Offset: 0x000A8F72
		public virtual bool HasFullProgress()
		{
			return this.totalCreditsSpent >= this.totalWaveCredits;
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000AAD85 File Offset: 0x000A8F85
		[Server]
		public virtual void ForceFinish()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::ForceFinish()' called on client");
				return;
			}
			this.KillSquad();
			if (this.combatDirector)
			{
				this.combatDirector.monsterCredit = 0f;
			}
			this.MarkAsFinished();
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000AADC8 File Offset: 0x000A8FC8
		[Server]
		private void KillSquad()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::KillSquad()' called on client");
				return;
			}
			if (this.combatSquad)
			{
				foreach (CharacterMaster characterMaster in new List<CharacterMaster>(this.combatSquad.readOnlyMembersList))
				{
					characterMaster.TrueKill();
				}
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000AAE44 File Offset: 0x000A9044
		[Server]
		protected void MarkAsFinished()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::MarkAsFinished()' called on client");
				return;
			}
			this.isFinished = true;
			this.OnFinishedServer();
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000AAE68 File Offset: 0x000A9068
		[Server]
		protected void StartTimer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::StartTimer()' called on client");
				return;
			}
			this.Network_isTimerActive = true;
			this.Network_timerStart = Run.instance.GetRunStopwatch();
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000AAE96 File Offset: 0x000A9096
		[Server]
		protected virtual void OnAllEnemiesDefeatedServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnAllEnemiesDefeatedServer()' called on client");
				return;
			}
			this.DropRewards();
			Action<InfiniteTowerWaveController> action = this.onAllEnemiesDefeatedServer;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000AAEC4 File Offset: 0x000A90C4
		[Server]
		protected virtual void OnTimerExpire()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnTimerExpire()' called on client");
				return;
			}
			this.MarkAsFinished();
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000AAEE4 File Offset: 0x000A90E4
		[Server]
		protected virtual void OnFinishedServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnFinishedServer()' called on client");
				return;
			}
			if (this.convertGoldOnWaveFinish)
			{
				ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
				for (int i = 0; i < instances.Count; i++)
				{
					CharacterMaster component = instances[i].gameObject.GetComponent<CharacterMaster>();
					uint num = Math.Max(component.money, (uint)Mathf.CeilToInt(component.money));
					ulong num2 = (ulong)(num * this.goldToExpConversionRatio / (float)instances.Count);
					component.money -= num;
					GameObject bodyObject = component.GetBodyObject();
					if (bodyObject)
					{
						ExperienceManager.instance.AwardExperience(base.transform.position, bodyObject.GetComponent<CharacterBody>(), num2);
					}
					else
					{
						TeamManager.instance.GiveTeamExperience(component.teamIndex, num2);
					}
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000AAFC0 File Offset: 0x000A91C0
		private void FixedUpdate()
		{
			if (this.haveAllEnemiesBeenDefeated && !this.hasPlayedEnemiesDefeatedSound)
			{
				this.PlayAllEnemiesDefeatedSound();
			}
			if (NetworkServer.active)
			{
				if (this.combatDirector)
				{
					this.NetworksquadCount = this.GetSquadCount();
					this.Network_totalCreditsSpent = this.combatDirector.totalCreditsSpent;
				}
				if (!this.isFinished)
				{
					if (this.combatSquad && this.combatSquad.memberCount == 0 && !this.haveAllEnemiesBeenDefeated)
					{
						this.Network_failsafeTimer = this._failsafeTimer + Time.fixedDeltaTime;
						if (this._failsafeTimer > this.secondsBeforeFailsafe)
						{
							Debug.LogError(string.Format("Failsafe detected!  Ending wave {0}", this.waveIndex));
							this.totalWaveCredits = 0f;
							if (this.combatDirector)
							{
								this.combatDirector.monsterCredit = 0f;
							}
						}
					}
					else
					{
						this.Network_failsafeTimer = 0f;
					}
					if (this.isInSuddenDeath)
					{
						if (!this.hasNotifiedSuddenDeath)
						{
							if (!string.IsNullOrEmpty(this.suddenDeathChatToken))
							{
								Chat.SendBroadcastChat(new Chat.SimpleChatMessage
								{
									baseToken = this.suddenDeathChatToken
								});
							}
							this.hasNotifiedSuddenDeath = true;
						}
						this.Network_zoneRadiusPercentage = Math.Max(0f, this._zoneRadiusPercentage - this.suddenDeathRadiusConstrictingPerSecond * Time.fixedDeltaTime);
					}
					else
					{
						this.Network_zoneRadiusPercentage = 1f;
					}
					if (!this.isTimerActive)
					{
						if (this.combatDirector)
						{
							if (this.combatDirector.totalCreditsSpent < this.totalWaveCredits)
							{
								if (this.combatSquad.memberCount < this.maxSquadSize)
								{
									float num = Time.fixedDeltaTime * this.creditsPerSecond;
									this.combatDirector.monsterCredit += num;
								}
							}
							else
							{
								if (this._suddenDeathTimerStart == 0f)
								{
									this.Network_suddenDeathTimerStart = Run.instance.GetRunStopwatch();
								}
								this.combatDirector.monsterCredit = 0f;
								if (this.combatSquad.memberCount == 0)
								{
									if (this.squadDefeatTimer <= 0f)
									{
										this.NetworkhaveAllEnemiesBeenDefeated = true;
										this.StartTimer();
										this.OnAllEnemiesDefeatedServer();
									}
									else
									{
										this.squadDefeatTimer -= Time.fixedDeltaTime;
									}
								}
							}
						}
					}
					else
					{
						this.Network_zoneRadiusPercentage = 1f;
						if (!this.hasTimerExpired && this.secondsRemaining <= 0f)
						{
							this.hasTimerExpired = true;
							this.OnTimerExpire();
						}
					}
				}
				else
				{
					this.Network_zoneRadiusPercentage = 1f;
				}
			}
			if (!this.hasEnabledEnemyIndicators && this.combatSquad && this.HasFullProgress())
			{
				this.hasEnabledEnemyIndicators = true;
				foreach (CharacterMaster master in this.combatSquad.readOnlyMembersList)
				{
					this.RequestIndicatorForMaster(master);
				}
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000AB29C File Offset: 0x000A949C
		private void OnDestroy()
		{
			if (this.uiInstance)
			{
				UnityEngine.Object.Destroy(this.uiInstance);
			}
			foreach (OverlayController overlayController in this.overlayControllerList)
			{
				HudOverlayManager.RemoveGlobalOverlay(overlayController);
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000AB304 File Offset: 0x000A9504
		private void OnEnable()
		{
			if (this.combatSquad)
			{
				this.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
			}
			this.squadDefeatTimer = this.squadDefeatGracePeriod;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000AB337 File Offset: 0x000A9537
		private void OnDisable()
		{
			if (this.combatSquad)
			{
				this.combatSquad.onMemberDiscovered -= this.OnCombatSquadMemberDiscovered;
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000AB360 File Offset: 0x000A9560
		protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
		{
			if (this.hasEnabledEnemyIndicators)
			{
				this.RequestIndicatorForMaster(master);
			}
			this.squadDefeatTimer = this.squadDefeatGracePeriod;
			if (NetworkServer.active)
			{
				master.inventory.AddItemsFrom(this.enemyInventory);
			}
			master.isBoss = this.isBossWave;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000AB3AC File Offset: 0x000A95AC
		protected virtual void RequestIndicatorForMaster(CharacterMaster master)
		{
			GameObject bodyObject = master.GetBodyObject();
			if (bodyObject)
			{
				TeamComponent component = bodyObject.GetComponent<TeamComponent>();
				if (component)
				{
					component.RequestDefaultIndicator(this.defaultEnemyIndicatorPrefab);
				}
			}
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000AB3E4 File Offset: 0x000A95E4
		[Server]
		private void DropRewards()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::DropRewards()' called on client");
				return;
			}
			int participatingPlayerCount = Run.instance.participatingPlayerCount;
			if (participatingPlayerCount > 0 && this.spawnTarget && this.rewardDropTable)
			{
				int num = participatingPlayerCount;
				float angle = 360f / (float)num;
				Vector3 vector = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 40f + Vector3.forward * 5f);
				Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
				Vector3 position = this.spawnTarget.transform.position + this.rewardOffset;
				int i = 0;
				while (i < num)
				{
					PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
					{
						pickupIndex = PickupCatalog.FindPickupIndex(this.rewardDisplayTier),
						pickerOptions = PickupPickerController.GenerateOptionsFromDropTable(this.rewardOptionCount, this.rewardDropTable, this.rng),
						rotation = Quaternion.identity,
						prefabOverride = this.rewardPickupPrefab
					}, position, vector);
					i++;
					vector = rotation * vector;
				}
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000AB588 File Offset: 0x000A9788
		// (set) Token: 0x0600276F RID: 10095 RVA: 0x000AB59B File Offset: 0x000A979B
		public float Network_totalWaveCredits
		{
			get
			{
				return this._totalWaveCredits;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._totalWaveCredits, 1U);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000AB5B0 File Offset: 0x000A97B0
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x000AB5C3 File Offset: 0x000A97C3
		public float Network_totalCreditsSpent
		{
			get
			{
				return this._totalCreditsSpent;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._totalCreditsSpent, 2U);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000AB5D8 File Offset: 0x000A97D8
		// (set) Token: 0x06002773 RID: 10099 RVA: 0x000AB5EB File Offset: 0x000A97EB
		public float Network_timerStart
		{
			get
			{
				return this._timerStart;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._timerStart, 4U);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000AB600 File Offset: 0x000A9800
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x000AB613 File Offset: 0x000A9813
		public float Network_failsafeTimer
		{
			get
			{
				return this._failsafeTimer;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._failsafeTimer, 8U);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000AB628 File Offset: 0x000A9828
		// (set) Token: 0x06002777 RID: 10103 RVA: 0x000AB63B File Offset: 0x000A983B
		public float Network_suddenDeathTimerStart
		{
			get
			{
				return this._suddenDeathTimerStart;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._suddenDeathTimerStart, 16U);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000AB650 File Offset: 0x000A9850
		// (set) Token: 0x06002779 RID: 10105 RVA: 0x000AB663 File Offset: 0x000A9863
		public bool Network_isFinished
		{
			get
			{
				return this._isFinished;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this._isFinished, 32U);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000AB678 File Offset: 0x000A9878
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x000AB68B File Offset: 0x000A988B
		public bool Network_isTimerActive
		{
			get
			{
				return this._isTimerActive;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this._isTimerActive, 64U);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000AB6A0 File Offset: 0x000A98A0
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x000AB6B3 File Offset: 0x000A98B3
		public float Network_zoneRadiusPercentage
		{
			get
			{
				return this._zoneRadiusPercentage;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._zoneRadiusPercentage, 128U);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x000AB6C8 File Offset: 0x000A98C8
		// (set) Token: 0x0600277F RID: 10111 RVA: 0x000AB6DB File Offset: 0x000A98DB
		public int NetworkwaveIndex
		{
			get
			{
				return this.waveIndex;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.waveIndex, 256U);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000AB6F0 File Offset: 0x000A98F0
		// (set) Token: 0x06002781 RID: 10113 RVA: 0x000AB703 File Offset: 0x000A9903
		public int NetworksquadCount
		{
			get
			{
				return this.squadCount;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.squadCount, 512U);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000AB718 File Offset: 0x000A9918
		// (set) Token: 0x06002783 RID: 10115 RVA: 0x000AB72B File Offset: 0x000A992B
		public bool NetworkhaveAllEnemiesBeenDefeated
		{
			get
			{
				return this.haveAllEnemiesBeenDefeated;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.haveAllEnemiesBeenDefeated, 1024U);
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000AB740 File Offset: 0x000A9940
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._totalWaveCredits);
				writer.Write(this._totalCreditsSpent);
				writer.Write(this._timerStart);
				writer.Write(this._failsafeTimer);
				writer.Write(this._suddenDeathTimerStart);
				writer.Write(this._isFinished);
				writer.Write(this._isTimerActive);
				writer.Write(this._zoneRadiusPercentage);
				writer.WritePackedUInt32((uint)this.waveIndex);
				writer.WritePackedUInt32((uint)this.squadCount);
				writer.Write(this.haveAllEnemiesBeenDefeated);
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
				writer.Write(this._totalWaveCredits);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._totalCreditsSpent);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._timerStart);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._failsafeTimer);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._suddenDeathTimerStart);
			}
			if ((base.syncVarDirtyBits & 32U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._isFinished);
			}
			if ((base.syncVarDirtyBits & 64U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._isTimerActive);
			}
			if ((base.syncVarDirtyBits & 128U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._zoneRadiusPercentage);
			}
			if ((base.syncVarDirtyBits & 256U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.waveIndex);
			}
			if ((base.syncVarDirtyBits & 512U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.squadCount);
			}
			if ((base.syncVarDirtyBits & 1024U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.haveAllEnemiesBeenDefeated);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000ABA24 File Offset: 0x000A9C24
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._totalWaveCredits = reader.ReadSingle();
				this._totalCreditsSpent = reader.ReadSingle();
				this._timerStart = reader.ReadSingle();
				this._failsafeTimer = reader.ReadSingle();
				this._suddenDeathTimerStart = reader.ReadSingle();
				this._isFinished = reader.ReadBoolean();
				this._isTimerActive = reader.ReadBoolean();
				this._zoneRadiusPercentage = reader.ReadSingle();
				this.waveIndex = (int)reader.ReadPackedUInt32();
				this.squadCount = (int)reader.ReadPackedUInt32();
				this.haveAllEnemiesBeenDefeated = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._totalWaveCredits = reader.ReadSingle();
			}
			if ((num & 2) != 0)
			{
				this._totalCreditsSpent = reader.ReadSingle();
			}
			if ((num & 4) != 0)
			{
				this._timerStart = reader.ReadSingle();
			}
			if ((num & 8) != 0)
			{
				this._failsafeTimer = reader.ReadSingle();
			}
			if ((num & 16) != 0)
			{
				this._suddenDeathTimerStart = reader.ReadSingle();
			}
			if ((num & 32) != 0)
			{
				this._isFinished = reader.ReadBoolean();
			}
			if ((num & 64) != 0)
			{
				this._isTimerActive = reader.ReadBoolean();
			}
			if ((num & 128) != 0)
			{
				this._zoneRadiusPercentage = reader.ReadSingle();
			}
			if ((num & 256) != 0)
			{
				this.waveIndex = (int)reader.ReadPackedUInt32();
			}
			if ((num & 512) != 0)
			{
				this.squadCount = (int)reader.ReadPackedUInt32();
			}
			if ((num & 1024) != 0)
			{
				this.haveAllEnemiesBeenDefeated = reader.ReadBoolean();
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002B37 RID: 11063
		private const float minimumDistributedCreditsFraction = 0.1f;

		// Token: 0x04002B38 RID: 11064
		[Tooltip("Should the enemies in this wave count as bosses?")]
		[SerializeField]
		private bool isBossWave;

		// Token: 0x04002B39 RID: 11065
		[Tooltip("The combat director for this wave")]
		[SerializeField]
		protected CombatDirector combatDirector;

		// Token: 0x04002B3A RID: 11066
		[Tooltip("The combat squad for this wave")]
		[SerializeField]
		protected CombatSquad combatSquad;

		// Token: 0x04002B3B RID: 11067
		[Tooltip("The base total number of credits to give to the CombatDirector for this wave.  Wave 1 gets this many credits.")]
		[SerializeField]
		protected float baseCredits;

		// Token: 0x04002B3C RID: 11068
		[Tooltip("The number of additional credits to give to the CombatDirector for each wave you've completed.  This doesn't affect Wave 1.")]
		[SerializeField]
		protected float linearCreditsPerWave;

		// Token: 0x04002B3D RID: 11069
		[Tooltip("The period (in seconds) over which we give the CombatDirector its credits.")]
		[SerializeField]
		protected float wavePeriodSeconds;

		// Token: 0x04002B3E RID: 11070
		[SerializeField]
		[Tooltip("The number of seconds (after the last enemy is spawned) before the radius begins constricting")]
		protected float secondsBeforeSuddenDeath = 60f;

		// Token: 0x04002B3F RID: 11071
		[SerializeField]
		[Tooltip("If there's ever a period of this many seconds with no enemies, end the wave")]
		protected float secondsBeforeFailsafe = 60f;

		// Token: 0x04002B40 RID: 11072
		[Range(0f, 1f)]
		[Tooltip("The zone radius percentage is reduced by this amount each second")]
		[SerializeField]
		protected float suddenDeathRadiusConstrictingPerSecond = 0.05f;

		// Token: 0x04002B41 RID: 11073
		[Tooltip("Broadcast this message at the beginning of sudden death")]
		[SerializeField]
		protected string suddenDeathChatToken = "INFINITETOWER_SUDDEN_DEATH";

		// Token: 0x04002B42 RID: 11074
		[Range(0f, 1f)]
		[Tooltip("The normalized fraction of the total credits to give to the CombatDirector immediately")]
		[SerializeField]
		protected float immediateCreditsFraction;

		// Token: 0x04002B43 RID: 11075
		[Tooltip("The maximum number of members in the combat squad before we stop giving the CombatDirector credits.  If the squad size is reduced below this number, we resume giving the director credits.")]
		[SerializeField]
		protected int maxSquadSize;

		// Token: 0x04002B44 RID: 11076
		[Tooltip("The time (in seconds) after completing the wave before the next wave begins")]
		[SerializeField]
		public int secondsAfterWave;

		// Token: 0x04002B45 RID: 11077
		[SerializeField]
		[Tooltip("The time (in seconds) after completing the wave before the next wave begins")]
		protected float squadDefeatGracePeriod = 1f;

		// Token: 0x04002B46 RID: 11078
		[Tooltip("The prefab to instantiate on the UI.")]
		[SerializeField]
		protected GameObject uiPrefab;

		// Token: 0x04002B47 RID: 11079
		[SerializeField]
		[Tooltip("The overlays to add for this wave")]
		protected InfiniteTowerWaveController.OverlayEntry[] overlayEntries;

		// Token: 0x04002B48 RID: 11080
		[SerializeField]
		[Tooltip("If true, convert all player gold to experience at the end of the wave")]
		protected bool convertGoldOnWaveFinish;

		// Token: 0x04002B49 RID: 11081
		[Tooltip("The multiplier to use when converting gold to experience (only used if convertGoldOnWaveFinish is true).")]
		[SerializeField]
		protected float goldToExpConversionRatio;

		// Token: 0x04002B4A RID: 11082
		[Tooltip("The drop table to use for the end of wave rewards")]
		[SerializeField]
		protected PickupDropTable rewardDropTable;

		// Token: 0x04002B4B RID: 11083
		[Tooltip("Use this tier to get a pickup index for the reward.  The droplet's visuals will correspond to this.")]
		[SerializeField]
		protected ItemTier rewardDisplayTier;

		// Token: 0x04002B4C RID: 11084
		[Tooltip("The number of options to display when the player interacts with the reward pickup.")]
		[SerializeField]
		protected int rewardOptionCount;

		// Token: 0x04002B4D RID: 11085
		[SerializeField]
		[Tooltip("The prefab to use for the reward pickup.")]
		protected GameObject rewardPickupPrefab;

		// Token: 0x04002B4E RID: 11086
		[Tooltip("Where to spawn the reward droplets relative to the spawn target (the center of the safe ward).")]
		[SerializeField]
		protected Vector3 rewardOffset;

		// Token: 0x04002B4F RID: 11087
		[Tooltip("Broadcast this message at the beginning of the wave.")]
		[SerializeField]
		protected string beginChatToken = "INFINITETOWER_WAVE_BEGIN";

		// Token: 0x04002B50 RID: 11088
		[Tooltip("Play this sound at the beginning of the wave.")]
		[SerializeField]
		protected string beginSoundString;

		// Token: 0x04002B51 RID: 11089
		[SerializeField]
		[Tooltip("Play this sound when all enemies are defeated.")]
		protected string onAllEnemiesDefeatedSoundString;

		// Token: 0x04002B54 RID: 11092
		[SyncVar]
		private float _totalWaveCredits;

		// Token: 0x04002B55 RID: 11093
		[SyncVar]
		private float _totalCreditsSpent;

		// Token: 0x04002B56 RID: 11094
		[SyncVar]
		private float _timerStart;

		// Token: 0x04002B57 RID: 11095
		[SyncVar]
		private float _failsafeTimer;

		// Token: 0x04002B58 RID: 11096
		[SyncVar]
		protected float _suddenDeathTimerStart;

		// Token: 0x04002B59 RID: 11097
		[SyncVar]
		private bool _isFinished;

		// Token: 0x04002B5A RID: 11098
		[SyncVar]
		private bool _isTimerActive;

		// Token: 0x04002B5B RID: 11099
		[SyncVar]
		private float _zoneRadiusPercentage = 1f;

		// Token: 0x04002B5C RID: 11100
		[SyncVar]
		protected int waveIndex;

		// Token: 0x04002B5D RID: 11101
		[SyncVar]
		private int squadCount;

		// Token: 0x04002B5E RID: 11102
		[SyncVar]
		private bool haveAllEnemiesBeenDefeated;

		// Token: 0x04002B5F RID: 11103
		protected float creditsPerSecond;

		// Token: 0x04002B60 RID: 11104
		protected GameObject uiInstance;

		// Token: 0x04002B61 RID: 11105
		protected bool hasEnabledEnemyIndicators;

		// Token: 0x04002B62 RID: 11106
		protected float squadDefeatTimer;

		// Token: 0x04002B63 RID: 11107
		private Inventory enemyInventory;

		// Token: 0x04002B64 RID: 11108
		private bool hasTimerExpired;

		// Token: 0x04002B65 RID: 11109
		protected GameObject spawnTarget;

		// Token: 0x04002B66 RID: 11110
		private Xoroshiro128Plus rng;

		// Token: 0x04002B67 RID: 11111
		private List<OverlayController> overlayControllerList = new List<OverlayController>();

		// Token: 0x04002B68 RID: 11112
		private bool hasNotifiedSuddenDeath;

		// Token: 0x04002B69 RID: 11113
		private bool hasPlayedEnemiesDefeatedSound;

		// Token: 0x0200076D RID: 1901
		[Serializable]
		public struct OverlayEntry
		{
			// Token: 0x04002B6A RID: 11114
			[Tooltip("The overlay prefab to instantiate")]
			[SerializeField]
			public GameObject prefab;

			// Token: 0x04002B6B RID: 11115
			[Tooltip("The overaly prefab to instantiate")]
			[SerializeField]
			public string hudChildName;
		}
	}
}
