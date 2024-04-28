using System;
using System.Collections.Generic;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Stats
{
	// Token: 0x02000AB1 RID: 2737
	[RequireComponent(typeof(PlayerCharacterMasterController))]
	[RequireComponent(typeof(CharacterMaster))]
	public class PlayerStatsComponent : NetworkBehaviour
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x00103374 File Offset: 0x00101574
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x0010337C File Offset: 0x0010157C
		public CharacterMaster characterMaster { get; private set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x00103385 File Offset: 0x00101585
		// (set) Token: 0x06003EDE RID: 16094 RVA: 0x0010338D File Offset: 0x0010158D
		public PlayerCharacterMasterController playerCharacterMasterController { get; private set; }

		// Token: 0x06003EDF RID: 16095 RVA: 0x00103398 File Offset: 0x00101598
		private void Awake()
		{
			this.playerCharacterMasterController = base.GetComponent<PlayerCharacterMasterController>();
			this.characterMaster = base.GetComponent<CharacterMaster>();
			PlayerStatsComponent.instancesList.Add(this);
			this.currentStats = StatSheet.New();
			if (NetworkClient.active)
			{
				this.recordedStats = StatSheet.New();
				this.clientDeltaStatsBuffer = StatSheet.New();
			}
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x001033F0 File Offset: 0x001015F0
		private void OnDestroy()
		{
			if (NetworkServer.active)
			{
				this.SendUpdateToClient();
			}
			PlayerStatsComponent.instancesList.Remove(this);
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x0010340B File Offset: 0x0010160B
		public static StatSheet FindBodyStatSheet(GameObject bodyObject)
		{
			if (!bodyObject)
			{
				return null;
			}
			return PlayerStatsComponent.FindBodyStatSheet(bodyObject.GetComponent<CharacterBody>());
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00103422 File Offset: 0x00101622
		public static StatSheet FindBodyStatSheet(CharacterBody characterBody)
		{
			if (characterBody == null)
			{
				return null;
			}
			CharacterMaster master = characterBody.master;
			if (master == null)
			{
				return null;
			}
			PlayerStatsComponent component = master.GetComponent<PlayerStatsComponent>();
			if (component == null)
			{
				return null;
			}
			return component.currentStats;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00103445 File Offset: 0x00101645
		public static StatSheet FindMasterStatSheet(CharacterMaster master)
		{
			PlayerStatsComponent playerStatsComponent = PlayerStatsComponent.FindMasterStatsComponent(master);
			if (playerStatsComponent == null)
			{
				return null;
			}
			return playerStatsComponent.currentStats;
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00103458 File Offset: 0x00101658
		public static PlayerStatsComponent FindBodyStatsComponent(GameObject bodyObject)
		{
			if (!bodyObject)
			{
				return null;
			}
			return PlayerStatsComponent.FindBodyStatsComponent(bodyObject.GetComponent<CharacterBody>());
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x0010346F File Offset: 0x0010166F
		public static PlayerStatsComponent FindBodyStatsComponent(CharacterBody characterBody)
		{
			if (characterBody == null)
			{
				return null;
			}
			CharacterMaster master = characterBody.master;
			if (master == null)
			{
				return null;
			}
			return master.GetComponent<PlayerStatsComponent>();
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x00103487 File Offset: 0x00101687
		public static PlayerStatsComponent FindMasterStatsComponent(CharacterMaster master)
		{
			if (master == null)
			{
				return null;
			}
			return master.playerStatsComponent;
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x00103494 File Offset: 0x00101694
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			GlobalEventManager.onCharacterDeathGlobal += delegate(DamageReport damageReport)
			{
				if (NetworkServer.active)
				{
					PlayerStatsComponent playerStatsComponent = PlayerStatsComponent.FindBodyStatsComponent(damageReport.victim.gameObject);
					if (playerStatsComponent)
					{
						playerStatsComponent.serverTransmitTimer = 0f;
					}
				}
			};
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x001034BA File Offset: 0x001016BA
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.ServerFixedUpdate();
			}
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x001034C9 File Offset: 0x001016C9
		[Server]
		public void ForceNextTransmit()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stats.PlayerStatsComponent::ForceNextTransmit()' called on client");
				return;
			}
			this.serverTransmitTimer = 0f;
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x001034EC File Offset: 0x001016EC
		[Server]
		private void ServerFixedUpdate()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stats.PlayerStatsComponent::ServerFixedUpdate()' called on client");
				return;
			}
			float num = 0f;
			float runTime = 0f;
			if (Run.instance && !Run.instance.isRunStopwatchPaused)
			{
				num = Time.fixedDeltaTime;
				runTime = Run.instance.GetRunStopwatch();
			}
			StatManager.CharacterUpdateEvent e = default(StatManager.CharacterUpdateEvent);
			e.statsComponent = this;
			e.runTime = runTime;
			GameObject bodyObject = this.characterMaster.GetBodyObject();
			if (bodyObject != this.cachedBodyObject)
			{
				this.cachedBodyObject = bodyObject;
				this.cachedBodyObject = bodyObject;
				this.cachedBodyTransform = ((bodyObject != null) ? bodyObject.transform : null);
				if (this.cachedBodyTransform)
				{
					this.previousBodyPosition = this.cachedBodyTransform.position;
				}
				this.cachedCharacterBody = ((bodyObject != null) ? bodyObject.GetComponent<CharacterBody>() : null);
				this.cachedBodyCharacterMotor = ((bodyObject != null) ? bodyObject.GetComponent<CharacterMotor>() : null);
			}
			if (this.cachedBodyTransform)
			{
				Vector3 position = this.cachedBodyTransform.position;
				e.additionalDistanceTraveled = Vector3.Distance(position, this.previousBodyPosition);
				this.previousBodyPosition = position;
			}
			if (this.characterMaster.hasBody)
			{
				e.additionalTimeAlive += num;
			}
			if (this.cachedCharacterBody)
			{
				e.level = (int)this.cachedCharacterBody.level;
			}
			StatManager.PushCharacterUpdateEvent(e);
			this.serverTransmitTimer -= Time.fixedDeltaTime;
			if (this.serverTransmitTimer <= 0f)
			{
				this.serverTransmitTimer = this.serverTransmitInterval;
				this.SendUpdateToClient();
			}
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x00103680 File Offset: 0x00101880
		[Server]
		private void SendUpdateToClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stats.PlayerStatsComponent::SendUpdateToClient()' called on client");
				return;
			}
			NetworkUser networkUser = this.playerCharacterMasterController.networkUser;
			if (networkUser)
			{
				NetworkWriter networkWriter = new NetworkWriter();
				networkWriter.StartMessage(58);
				networkWriter.Write(base.gameObject);
				this.currentStats.Write(networkWriter);
				networkWriter.FinishMessage();
				networkUser.connectionToClient.SendWriter(networkWriter, this.GetNetworkChannel());
			}
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x001036F8 File Offset: 0x001018F8
		[NetworkMessageHandler(client = true, msgType = 58)]
		private static void HandleStatsUpdate(NetworkMessage netMsg)
		{
			GameObject gameObject = netMsg.reader.ReadGameObject();
			if (gameObject)
			{
				PlayerStatsComponent component = gameObject.GetComponent<PlayerStatsComponent>();
				if (component)
				{
					component.InstanceHandleStatsUpdate(netMsg.reader);
				}
			}
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x00103734 File Offset: 0x00101934
		[Client]
		private void InstanceHandleStatsUpdate(NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.Stats.PlayerStatsComponent::InstanceHandleStatsUpdate(UnityEngine.Networking.NetworkReader)' called on server");
				return;
			}
			if (!NetworkServer.active)
			{
				this.currentStats.Read(reader);
			}
			this.FlushStatsToUserProfile();
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x00103764 File Offset: 0x00101964
		[Client]
		private void FlushStatsToUserProfile()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.Stats.PlayerStatsComponent::FlushStatsToUserProfile()' called on server");
				return;
			}
			StatSheet.GetDelta(this.clientDeltaStatsBuffer, this.currentStats, this.recordedStats);
			StatSheet.Copy(this.currentStats, this.recordedStats);
			NetworkUser networkUser = this.playerCharacterMasterController.networkUser;
			LocalUser localUser = (networkUser != null) ? networkUser.localUser : null;
			if (localUser == null)
			{
				return;
			}
			UserProfile userProfile = localUser.userProfile;
			if (userProfile == null)
			{
				return;
			}
			userProfile.ApplyDeltaStatSheet(this.clientDeltaStatsBuffer);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x001037E0 File Offset: 0x001019E0
		[ConCommand(commandName = "print_stats", flags = ConVarFlags.None, helpText = "Prints all current stats of the sender.")]
		private static void CCPrintStats(ConCommandArgs args)
		{
			GameObject senderMasterObject = args.senderMasterObject;
			StatSheet statSheet;
			if (senderMasterObject == null)
			{
				statSheet = null;
			}
			else
			{
				PlayerStatsComponent component = senderMasterObject.GetComponent<PlayerStatsComponent>();
				statSheet = ((component != null) ? component.currentStats : null);
			}
			StatSheet statSheet2 = statSheet;
			if (statSheet2 == null)
			{
				return;
			}
			string[] array = new string[statSheet2.fields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Format("[\"{0}\"]={1}", statSheet2.fields[i].name, statSheet2.fields[i].ToString());
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00103894 File Offset: 0x00101A94
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003D29 RID: 15657
		public static readonly List<PlayerStatsComponent> instancesList = new List<PlayerStatsComponent>();

		// Token: 0x04003D2C RID: 15660
		private float serverTransmitTimer;

		// Token: 0x04003D2D RID: 15661
		private float serverTransmitInterval = 10f;

		// Token: 0x04003D2E RID: 15662
		private Vector3 previousBodyPosition;

		// Token: 0x04003D2F RID: 15663
		private GameObject cachedBodyObject;

		// Token: 0x04003D30 RID: 15664
		private CharacterBody cachedCharacterBody;

		// Token: 0x04003D31 RID: 15665
		private CharacterMotor cachedBodyCharacterMotor;

		// Token: 0x04003D32 RID: 15666
		private Transform cachedBodyTransform;

		// Token: 0x04003D33 RID: 15667
		public StatSheet currentStats;

		// Token: 0x04003D34 RID: 15668
		private StatSheet clientDeltaStatsBuffer;

		// Token: 0x04003D35 RID: 15669
		private StatSheet recordedStats;
	}
}
