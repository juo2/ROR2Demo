using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HG;
using JetBrains.Annotations;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007D5 RID: 2005
	public class NetworkUIPromptController : NetworkBehaviour
	{
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06002AB7 RID: 10935 RVA: 0x000B7DFC File Offset: 0x000B5FFC
		// (remove) Token: 0x06002AB8 RID: 10936 RVA: 0x000B7E34 File Offset: 0x000B6034
		public event Action<NetworkUIPromptController, LocalUser, CameraRigController> onDisplayBegin;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06002AB9 RID: 10937 RVA: 0x000B7E6C File Offset: 0x000B606C
		// (remove) Token: 0x06002ABA RID: 10938 RVA: 0x000B7EA4 File Offset: 0x000B60A4
		public event Action<NetworkUIPromptController, LocalUser, CameraRigController> onDisplayEnd;

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x000B7ED9 File Offset: 0x000B60D9
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x000B7EE1 File Offset: 0x000B60E1
		private LocalUser currentLocalParticipant
		{
			get
			{
				return this._currentLocalParticipant;
			}
			set
			{
				if (this._currentLocalParticipant == value)
				{
					return;
				}
				if (this._currentLocalParticipant != null)
				{
					this.OnLocalParticipantLost(this._currentLocalParticipant);
				}
				this._currentLocalParticipant = value;
				if (this._currentLocalParticipant != null)
				{
					this.OnLocalParticipantDiscovered(this._currentLocalParticipant);
				}
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000B7F1C File Offset: 0x000B611C
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000B7F24 File Offset: 0x000B6124
		public CharacterMaster currentParticipantMaster
		{
			get
			{
				return this._currentParticipantMaster;
			}
			private set
			{
				if (this._currentParticipantMaster == value)
				{
					return;
				}
				if (this._currentParticipantMaster != null)
				{
					this.OnParticipantLost(this._currentParticipantMaster);
				}
				this._currentParticipantMaster = value;
				if (this._currentParticipantMaster != null)
				{
					this.OnParticipantDiscovered(this._currentParticipantMaster);
				}
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000B7F60 File Offset: 0x000B6160
		private void OnParticipantDiscovered([NotNull] CharacterMaster master)
		{
			LocalUser currentLocalParticipant = null;
			if (master.playerCharacterMasterController && master.playerCharacterMasterController.networkUser)
			{
				currentLocalParticipant = master.playerCharacterMasterController.networkUser.localUser;
			}
			this.currentLocalParticipant = currentLocalParticipant;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000B7FA6 File Offset: 0x000B61A6
		private void OnParticipantLost([NotNull] CharacterMaster master)
		{
			this.currentLocalParticipant = null;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000B7FAF File Offset: 0x000B61AF
		private void OnLocalParticipantDiscovered([NotNull] LocalUser localUser)
		{
			this.lastCurrentLocalParticipantUpdateTime = Time.unscaledTime;
			NetworkUIPromptController.UpdateBestControllerForLocalUser(localUser);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000B7FC4 File Offset: 0x000B61C4
		private void OnLocalParticipantLost([NotNull] LocalUser localUser)
		{
			ref NetworkUIPromptController.LocalUserInfo localUserInfo = ref NetworkUIPromptController.GetLocalUserInfo(localUser);
			if (localUserInfo.currentController == this)
			{
				localUserInfo.currentController.inControl = false;
				localUserInfo.currentController = null;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000B7FF9 File Offset: 0x000B61F9
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x000B8001 File Offset: 0x000B6201
		private bool inControl
		{
			get
			{
				return this._inControl;
			}
			set
			{
				if (this._inControl == value)
				{
					return;
				}
				this._inControl = value;
				if (this._inControl)
				{
					this.OnControlBegin();
					return;
				}
				this.OnControlEnd();
			}
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000B8029 File Offset: 0x000B6229
		private void HandleCameraDiscovered(CameraRigController cameraRigController)
		{
			this.currentCamera = cameraRigController;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000B8032 File Offset: 0x000B6232
		private void HandleCameraLost(CameraRigController cameraRigController)
		{
			this.currentCamera = null;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000B803C File Offset: 0x000B623C
		private void OnControlBegin()
		{
			this.currentCamera = this.currentLocalParticipant.cameraRigController;
			this.currentLocalParticipant.onCameraDiscovered += this.HandleCameraDiscovered;
			this.currentLocalParticipant.onCameraLost += this.HandleCameraLost;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000B8088 File Offset: 0x000B6288
		private void OnControlEnd()
		{
			this.currentLocalParticipant.onCameraLost -= this.HandleCameraLost;
			this.currentLocalParticipant.onCameraDiscovered -= this.HandleCameraDiscovered;
			this.currentCamera = null;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000B80C0 File Offset: 0x000B62C0
		[CanBeNull]
		private static NetworkUIPromptController FindBestControllerForLocalUser([NotNull] LocalUser localUser)
		{
			NetworkUIPromptController result = null;
			float positiveInfinity = float.PositiveInfinity;
			List<NetworkUIPromptController> instancesList = InstanceTracker.GetInstancesList<NetworkUIPromptController>();
			for (int i = 0; i < instancesList.Count; i++)
			{
				NetworkUIPromptController networkUIPromptController = instancesList[i];
				if (networkUIPromptController.currentLocalParticipant == localUser && networkUIPromptController.lastCurrentLocalParticipantUpdateTime < positiveInfinity)
				{
					positiveInfinity = networkUIPromptController.lastCurrentLocalParticipantUpdateTime;
					result = networkUIPromptController;
				}
			}
			return result;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000B8118 File Offset: 0x000B6318
		private static void UpdateBestControllerForLocalUser([NotNull] LocalUser localUser)
		{
			ref NetworkUIPromptController.LocalUserInfo localUserInfo = ref NetworkUIPromptController.GetLocalUserInfo(localUser);
			NetworkUIPromptController currentController = localUserInfo.currentController;
			NetworkUIPromptController networkUIPromptController = NetworkUIPromptController.FindBestControllerForLocalUser(localUser);
			if (currentController != networkUIPromptController)
			{
				if (currentController != null)
				{
					currentController.inControl = false;
				}
				if (networkUIPromptController != null)
				{
					networkUIPromptController.inControl = true;
				}
				localUserInfo.currentController = networkUIPromptController;
			}
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000B8159 File Offset: 0x000B6359
		private void OnEnable()
		{
			InstanceTracker.Add<NetworkUIPromptController>(this);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000B8161 File Offset: 0x000B6361
		private void OnDisable()
		{
			this.SetParticipantMasterId(NetworkInstanceId.Invalid);
			InstanceTracker.Remove<NetworkUIPromptController>(this);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000B8174 File Offset: 0x000B6374
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (!NetworkServer.active)
			{
				this.SetParticipantMasterId(this.masterObjectInstanceId);
			}
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000B8190 File Offset: 0x000B6390
		private void SetParticipantMasterId(NetworkInstanceId newMasterObjectInstanceId)
		{
			this.NetworkmasterObjectInstanceId = newMasterObjectInstanceId;
			GameObject gameObject = Util.FindNetworkObject(this.masterObjectInstanceId);
			CharacterMaster currentParticipantMaster = null;
			if (gameObject)
			{
				currentParticipantMaster = gameObject.GetComponent<CharacterMaster>();
			}
			this.currentParticipantMaster = currentParticipantMaster;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000B81C8 File Offset: 0x000B63C8
		[Server]
		public void ClearParticipant()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUIPromptController::ClearParticipant()' called on client");
				return;
			}
			this.SetParticipantMaster(null);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000B81E8 File Offset: 0x000B63E8
		[Server]
		public void SetParticipantMaster([CanBeNull] CharacterMaster newParticipantMaster)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUIPromptController::SetParticipantMaster(RoR2.CharacterMaster)' called on client");
				return;
			}
			NetworkIdentity networkIdentity = newParticipantMaster ? newParticipantMaster.networkIdentity : null;
			this.SetParticipantMasterId(networkIdentity ? networkIdentity.netId : NetworkInstanceId.Invalid);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000B8238 File Offset: 0x000B6438
		[Server]
		public void SetParticipantMasterFromInteractor([CanBeNull] Interactor newParticipantInteractor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUIPromptController::SetParticipantMasterFromInteractor(RoR2.Interactor)' called on client");
				return;
			}
			CharacterMaster characterMaster;
			if (!newParticipantInteractor)
			{
				characterMaster = null;
			}
			else
			{
				CharacterBody component = newParticipantInteractor.GetComponent<CharacterBody>();
				characterMaster = ((component != null) ? component.master : null);
			}
			CharacterMaster participantMaster = characterMaster;
			this.SetParticipantMaster(participantMaster);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000B827F File Offset: 0x000B647F
		[Server]
		public void SetParticipantMasterFromInteractorObject([CanBeNull] UnityEngine.Object newParticipantInteractor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUIPromptController::SetParticipantMasterFromInteractorObject(UnityEngine.Object)' called on client");
				return;
			}
			this.SetParticipantMasterFromInteractor(newParticipantInteractor as Interactor);
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000B82A2 File Offset: 0x000B64A2
		// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x000B82AC File Offset: 0x000B64AC
		private CameraRigController currentCamera
		{
			get
			{
				return this._currentCamera;
			}
			set
			{
				if (this._currentCamera == value)
				{
					return;
				}
				if (this._currentCamera != null)
				{
					Action<NetworkUIPromptController, LocalUser, CameraRigController> action = this.onDisplayEnd;
					if (action != null)
					{
						action(this, this.currentLocalParticipant, this._currentCamera);
					}
				}
				this._currentCamera = value;
				if (this._currentCamera != null)
				{
					Action<NetworkUIPromptController, LocalUser, CameraRigController> action2 = this.onDisplayBegin;
					if (action2 == null)
					{
						return;
					}
					action2(this, this.currentLocalParticipant, this._currentCamera);
				}
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000B8315 File Offset: 0x000B6515
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			LocalUserManager.onUserSignIn += NetworkUIPromptController.OnUserSignIn;
			LocalUserManager.onUserSignOut += NetworkUIPromptController.OnUserSignOut;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000B833C File Offset: 0x000B653C
		private static void OnUserSignIn(LocalUser localUser)
		{
			NetworkUIPromptController.LocalUserInfo localUserInfo = new NetworkUIPromptController.LocalUserInfo
			{
				localUser = localUser,
				currentController = null
			};
			ArrayUtils.ArrayAppend<NetworkUIPromptController.LocalUserInfo>(ref NetworkUIPromptController.allLocalUserInfo, ref NetworkUIPromptController.allLocalUserInfoCount, localUserInfo);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000B8374 File Offset: 0x000B6574
		private static void OnUserSignOut(LocalUser localUser)
		{
			for (int i = 0; i < NetworkUIPromptController.allLocalUserInfoCount; i++)
			{
				if (NetworkUIPromptController.allLocalUserInfo[i].localUser == localUser)
				{
					ArrayUtils.ArrayRemoveAt<NetworkUIPromptController.LocalUserInfo>(NetworkUIPromptController.allLocalUserInfo, ref NetworkUIPromptController.allLocalUserInfoCount, i, 1);
					return;
				}
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000B83B8 File Offset: 0x000B65B8
		private static ref NetworkUIPromptController.LocalUserInfo GetLocalUserInfo(LocalUser localUser)
		{
			for (int i = 0; i < NetworkUIPromptController.allLocalUserInfoCount; i++)
			{
				if (NetworkUIPromptController.allLocalUserInfo[i].localUser == localUser)
				{
					return ref NetworkUIPromptController.allLocalUserInfo[i];
				}
			}
			throw new ArgumentException("localUser must be signed in");
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000B83FE File Offset: 0x000B65FE
		public bool inUse
		{
			get
			{
				return this.currentParticipantMaster;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000B840B File Offset: 0x000B660B
		public bool isDisplaying
		{
			get
			{
				return this.currentCamera;
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000B8418 File Offset: 0x000B6618
		[Client]
		public NetworkWriter BeginMessageToServer()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'UnityEngine.Networking.NetworkWriter RoR2.NetworkUIPromptController::BeginMessageToServer()' called on server");
				return null;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(76);
			networkWriter.Write(base.gameObject);
			return networkWriter;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000B8460 File Offset: 0x000B6660
		[Client]
		public void FinishMessageToServer(NetworkWriter writer)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.NetworkUIPromptController::FinishMessageToServer(UnityEngine.Networking.NetworkWriter)' called on server");
				return;
			}
			writer.FinishMessage();
			NetworkUser networkUser = NetworkUIPromptController.FindParticipantNetworkUser(this);
			if (networkUser)
			{
				networkUser.connectionToServer.SendWriter(writer, this.GetNetworkChannel());
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000B84AC File Offset: 0x000B66AC
		private static NetworkUser FindParticipantNetworkUser(NetworkUIPromptController instance)
		{
			if (instance)
			{
				CharacterMaster currentParticipantMaster = instance.currentParticipantMaster;
				if (currentParticipantMaster)
				{
					PlayerCharacterMasterController playerCharacterMasterController = currentParticipantMaster.playerCharacterMasterController;
					if (playerCharacterMasterController)
					{
						return playerCharacterMasterController.networkUser;
					}
				}
			}
			return null;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000B84E8 File Offset: 0x000B66E8
		[NetworkMessageHandler(client = false, server = true, msgType = 76)]
		private static void HandleNetworkUIPromptMessage(NetworkMessage netMsg)
		{
			GameObject gameObject = netMsg.reader.ReadGameObject();
			if (!gameObject)
			{
				return;
			}
			NetworkUIPromptController component = gameObject.GetComponent<NetworkUIPromptController>();
			if (!component)
			{
				return;
			}
			NetworkUser networkUser = NetworkUIPromptController.FindParticipantNetworkUser(component);
			NetworkConnection networkConnection = networkUser ? networkUser.connectionToClient : null;
			if (netMsg.conn != networkConnection)
			{
				return;
			}
			Action<NetworkReader> action = component.messageFromClientHandler;
			if (action == null)
			{
				return;
			}
			action(netMsg.reader);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000B8578 File Offset: 0x000B6778
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x000B858B File Offset: 0x000B678B
		public NetworkInstanceId NetworkmasterObjectInstanceId
		{
			get
			{
				return this.masterObjectInstanceId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetParticipantMasterId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkInstanceId>(value, ref this.masterObjectInstanceId, 1U);
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000B85CC File Offset: 0x000B67CC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.masterObjectInstanceId);
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
				writer.Write(this.masterObjectInstanceId);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000B8638 File Offset: 0x000B6838
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.masterObjectInstanceId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetParticipantMasterId(reader.ReadNetworkId());
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002DAF RID: 11695
		private float lastCurrentLocalParticipantUpdateTime = float.NegativeInfinity;

		// Token: 0x04002DB0 RID: 11696
		private LocalUser _currentLocalParticipant;

		// Token: 0x04002DB1 RID: 11697
		private CharacterMaster _currentParticipantMaster;

		// Token: 0x04002DB2 RID: 11698
		private bool _inControl;

		// Token: 0x04002DB3 RID: 11699
		[SyncVar(hook = "SetParticipantMasterId")]
		private NetworkInstanceId masterObjectInstanceId;

		// Token: 0x04002DB4 RID: 11700
		private CameraRigController _currentCamera;

		// Token: 0x04002DB5 RID: 11701
		private static NetworkUIPromptController.LocalUserInfo[] allLocalUserInfo = Array.Empty<NetworkUIPromptController.LocalUserInfo>();

		// Token: 0x04002DB6 RID: 11702
		private static int allLocalUserInfoCount = 0;

		// Token: 0x04002DB7 RID: 11703
		public Action<NetworkReader> messageFromClientHandler;

		// Token: 0x020007D6 RID: 2006
		private struct LocalUserInfo
		{
			// Token: 0x04002DB8 RID: 11704
			public LocalUser localUser;

			// Token: 0x04002DB9 RID: 11705
			public NetworkUIPromptController currentController;
		}
	}
}
