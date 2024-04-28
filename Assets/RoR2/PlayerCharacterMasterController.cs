using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Rewired;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200080B RID: 2059
	[RequireComponent(typeof(CharacterMaster))]
	[RequireComponent(typeof(PingerController))]
	public class PlayerCharacterMasterController : NetworkBehaviour
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000BE28D File Offset: 0x000BC48D
		public static ReadOnlyCollection<PlayerCharacterMasterController> instances
		{
			get
			{
				return PlayerCharacterMasterController._instancesReadOnly;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000BE294 File Offset: 0x000BC494
		private bool bodyIsFlier
		{
			get
			{
				return !this.bodyMotor || this.bodyMotor.isFlying;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000BE2B0 File Offset: 0x000BC4B0
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000BE2B8 File Offset: 0x000BC4B8
		public CharacterMaster master { get; private set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000BE2C1 File Offset: 0x000BC4C1
		private NetworkIdentity networkIdentity
		{
			get
			{
				return this.master.networkIdentity;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000BE2D0 File Offset: 0x000BC4D0
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x000BE304 File Offset: 0x000BC504
		public string finalMessageTokenServer
		{
			[Server]
			[CompilerGenerated]
			get
			{
				if (!NetworkServer.active)
				{
					Debug.LogWarning("[Server] function 'System.String RoR2.PlayerCharacterMasterController::get_finalMessageTokenServer()' called on client");
					return null;
				}
				return this.<finalMessageTokenServer>k__BackingField;
			}
			[CompilerGenerated]
			[Server]
			set
			{
				if (!NetworkServer.active)
				{
					Debug.LogWarning("[Server] function 'System.Void RoR2.PlayerCharacterMasterController::set_finalMessageTokenServer(System.String)' called on client");
					return;
				}
				this.<finalMessageTokenServer>k__BackingField = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000BE322 File Offset: 0x000BC522
		public bool hasEffectiveAuthority
		{
			get
			{
				return this.master.hasEffectiveAuthority;
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000BE32F File Offset: 0x000BC52F
		private void OnSyncNetworkUserInstanceId(NetworkInstanceId value)
		{
			this.resolvedNetworkUserGameObjectInstance = null;
			this.resolvedNetworkUserInstance = null;
			this.networkUserResolved = (value == NetworkInstanceId.Invalid);
			this.NetworknetworkUserInstanceId = value;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000BE358 File Offset: 0x000BC558
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x000BE3B8 File Offset: 0x000BC5B8
		public GameObject networkUserObject
		{
			get
			{
				if (!this.networkUserResolved)
				{
					this.resolvedNetworkUserGameObjectInstance = Util.FindNetworkObject(this.networkUserInstanceId);
					this.resolvedNetworkUserInstance = null;
					if (this.resolvedNetworkUserGameObjectInstance)
					{
						this.resolvedNetworkUserInstance = this.resolvedNetworkUserGameObjectInstance.GetComponent<NetworkUser>();
						this.networkUserResolved = true;
						this.SetBodyPrefabToPreference();
					}
				}
				return this.resolvedNetworkUserGameObjectInstance;
			}
			private set
			{
				NetworkInstanceId networknetworkUserInstanceId = NetworkInstanceId.Invalid;
				this.resolvedNetworkUserGameObjectInstance = null;
				this.resolvedNetworkUserInstance = null;
				this.networkUserResolved = true;
				if (value)
				{
					NetworkIdentity component = value.GetComponent<NetworkIdentity>();
					if (component)
					{
						networknetworkUserInstanceId = component.netId;
						this.resolvedNetworkUserGameObjectInstance = value;
						this.resolvedNetworkUserInstance = value.GetComponent<NetworkUser>();
						this.SetBodyPrefabToPreference();
					}
				}
				this.NetworknetworkUserInstanceId = networknetworkUserInstanceId;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000BE41E File Offset: 0x000BC61E
		public NetworkUser networkUser
		{
			get
			{
				if (!this.networkUserObject)
				{
					return null;
				}
				return this.resolvedNetworkUserInstance;
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000BE438 File Offset: 0x000BC638
		[Server]
		public void LinkToNetworkUserServer(NetworkUser networkUser)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PlayerCharacterMasterController::LinkToNetworkUserServer(RoR2.NetworkUser)' called on client");
				return;
			}
			networkUser.masterObject = base.gameObject;
			this.networkUserObject = networkUser.gameObject;
			this.networkIdentity.AssignClientAuthority(networkUser.connectionToClient);
			if (!this.alreadyLinkedToNetworkUserOnce)
			{
				networkUser.CopyLoadoutToMaster();
				this.alreadyLinkedToNetworkUserOnce = true;
			}
			else
			{
				networkUser.CopyLoadoutFromMaster();
			}
			this.SetBodyPrefabToPreference();
			Action action = this.onLinkedToNetworkUserServer;
			if (action != null)
			{
				action();
			}
			Action<PlayerCharacterMasterController> action2 = PlayerCharacterMasterController.onLinkedToNetworkUserServerGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this);
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
		public bool isConnected
		{
			get
			{
				return this.networkUserObject;
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000BE4D5 File Offset: 0x000BC6D5
		private void Awake()
		{
			this.master = base.GetComponent<CharacterMaster>();
			this.netid = base.GetComponent<NetworkIdentity>();
			this.pingerController = base.GetComponent<PingerController>();
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000BE4FB File Offset: 0x000BC6FB
		private void OnEnable()
		{
			PlayerCharacterMasterController._instances.Add(this);
			if (PlayerCharacterMasterController.onPlayerAdded != null)
			{
				PlayerCharacterMasterController.onPlayerAdded(this);
			}
			NetworkUser.onNetworkUserBodyPreferenceChanged += this.OnNetworkUserBodyPreferenceChanged;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000BE52B File Offset: 0x000BC72B
		private void OnDisable()
		{
			PlayerCharacterMasterController._instances.Remove(this);
			if (PlayerCharacterMasterController.onPlayerRemoved != null)
			{
				PlayerCharacterMasterController.onPlayerRemoved(this);
			}
			NetworkUser.onNetworkUserBodyPreferenceChanged -= this.OnNetworkUserBodyPreferenceChanged;
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000BE55C File Offset: 0x000BC75C
		private void Start()
		{
			if (NetworkServer.active && this.networkUser)
			{
				this.CallRpcIncrementRunCount();
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000BE578 File Offset: 0x000BC778
		[ClientRpc]
		private void RpcIncrementRunCount()
		{
			if (this.networkUser)
			{
				LocalUser localUser = this.networkUser.localUser;
				if (localUser != null)
				{
					localUser.userProfile.totalRunCount += 1U;
				}
			}
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000BE5B4 File Offset: 0x000BC7B4
		private static bool CanSendBodyInput(NetworkUser networkUser, out LocalUser localUser, out Player inputPlayer, out CameraRigController cameraRigController)
		{
			if (!networkUser)
			{
				localUser = null;
				inputPlayer = null;
				cameraRigController = null;
				return false;
			}
			localUser = networkUser.localUser;
			inputPlayer = networkUser.inputPlayer;
			cameraRigController = networkUser.cameraRigController;
			return localUser != null && inputPlayer != null && cameraRigController && !localUser.isUIFocused && cameraRigController.isControlAllowed;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000BE618 File Offset: 0x000BC818
		private void Update()
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetBody(this.master.GetBodyObject());
				if (this.bodyInputs)
				{
					Vector3 moveVector = Vector3.zero;
					Vector3 aimDirection = this.bodyInputs.aimDirection;
					LocalUser localUser;
					Player player;
					CameraRigController cameraRigController;
					if (PlayerCharacterMasterController.CanSendBodyInput(this.networkUser, out localUser, out player, out cameraRigController))
					{
						Transform transform = cameraRigController.transform;
						this.sprintInputPressReceived |= player.GetButtonDown(18);
						Vector2 vector = new Vector2(player.GetAxis(0), player.GetAxis(1));
						float sqrMagnitude = vector.sqrMagnitude;
						if (sqrMagnitude > 1f)
						{
							vector /= Mathf.Sqrt(sqrMagnitude);
						}
						if (this.bodyIsFlier)
						{
							moveVector = transform.right * vector.x + transform.forward * vector.y;
						}
						else
						{
							float y = transform.eulerAngles.y;
							moveVector = Quaternion.Euler(0f, y, 0f) * new Vector3(vector.x, 0f, vector.y);
						}
						aimDirection = (cameraRigController.crosshairWorldPosition - this.bodyInputs.aimOrigin).normalized;
					}
					this.bodyInputs.moveVector = moveVector;
					this.bodyInputs.aimDirection = aimDirection;
				}
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000BE77C File Offset: 0x000BC97C
		private void FixedUpdate()
		{
			if (this.hasEffectiveAuthority && this.bodyInputs)
			{
				bool newState = false;
				bool newState2 = false;
				bool newState3 = false;
				bool newState4 = false;
				bool newState5 = false;
				bool newState6 = false;
				bool newState7 = false;
				bool newState8 = false;
				bool newState9 = false;
				LocalUser localUser;
				Player player;
				CameraRigController cameraRigController;
				if (PlayerCharacterMasterController.CanSendBodyInput(this.networkUser, out localUser, out player, out cameraRigController))
				{
					bool flag = this.body.isSprinting;
					if (this.sprintInputPressReceived)
					{
						this.sprintInputPressReceived = false;
						flag = !flag;
					}
					if (flag)
					{
						Vector3 aimDirection = this.bodyInputs.aimDirection;
						aimDirection.y = 0f;
						aimDirection.Normalize();
						Vector3 moveVector = this.bodyInputs.moveVector;
						moveVector.y = 0f;
						moveVector.Normalize();
						if ((this.body.bodyFlags & CharacterBody.BodyFlags.SprintAnyDirection) == CharacterBody.BodyFlags.None && Vector3.Dot(aimDirection, moveVector) < PlayerCharacterMasterController.sprintMinAimMoveDot)
						{
							flag = false;
						}
					}
					newState = player.GetButton(7);
					newState2 = player.GetButton(8);
					newState3 = player.GetButton(9);
					newState4 = player.GetButton(10);
					newState5 = player.GetButton(5);
					newState6 = player.GetButton(4);
					newState7 = flag;
					newState8 = player.GetButton(6);
					newState9 = player.GetButton(28);
				}
				this.bodyInputs.skill1.PushState(newState);
				this.bodyInputs.skill2.PushState(newState2);
				this.bodyInputs.skill3.PushState(newState3);
				this.bodyInputs.skill4.PushState(newState4);
				this.bodyInputs.interact.PushState(newState5);
				this.bodyInputs.jump.PushState(newState6);
				this.bodyInputs.sprint.PushState(newState7);
				this.bodyInputs.activateEquipment.PushState(newState8);
				this.bodyInputs.ping.PushState(newState9);
				this.CheckPinging();
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000BE958 File Offset: 0x000BCB58
		private void CheckPinging()
		{
			if (this.hasEffectiveAuthority && this.body && this.bodyInputs && this.bodyInputs.ping.justPressed)
			{
				this.pingerController.AttemptPing(new Ray(this.bodyInputs.aimOrigin, this.bodyInputs.aimDirection), this.body.gameObject);
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000BE9CC File Offset: 0x000BCBCC
		public string GetDisplayName()
		{
			string result = "";
			if (this.networkUserObject)
			{
				NetworkUser component = this.networkUserObject.GetComponent<NetworkUser>();
				if (component)
				{
					result = component.userName;
				}
			}
			return result;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000BEA08 File Offset: 0x000BCC08
		private void SetBody(GameObject newBody)
		{
			if (newBody)
			{
				this.body = newBody.GetComponent<CharacterBody>();
			}
			else
			{
				this.body = null;
			}
			if (this.body)
			{
				this.bodyInputs = this.body.inputBank;
				this.bodyMotor = this.body.characterMotor;
				return;
			}
			this.bodyInputs = null;
			this.bodyMotor = null;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000BEA70 File Offset: 0x000BCC70
		public bool preventGameOver
		{
			get
			{
				return this.master.preventGameOver;
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000BEA7D File Offset: 0x000BCC7D
		[Server]
		public void OnBodyDeath()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PlayerCharacterMasterController::OnBodyDeath()' called on client");
				return;
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000BEA94 File Offset: 0x000BCC94
		public void OnBodyStart()
		{
			if (NetworkServer.active)
			{
				this.finalMessageTokenServer = string.Empty;
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000BEAA8 File Offset: 0x000BCCA8
		public static int GetPlayersWithBodiesCount()
		{
			int num = 0;
			using (List<PlayerCharacterMasterController>.Enumerator enumerator = PlayerCharacterMasterController._instances.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.master.hasBody)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06002CA2 RID: 11426 RVA: 0x000BEB08 File Offset: 0x000BCD08
		// (remove) Token: 0x06002CA3 RID: 11427 RVA: 0x000BEB40 File Offset: 0x000BCD40
		public event Action onLinkedToNetworkUserServer;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06002CA4 RID: 11428 RVA: 0x000BEB78 File Offset: 0x000BCD78
		// (remove) Token: 0x06002CA5 RID: 11429 RVA: 0x000BEBAC File Offset: 0x000BCDAC
		public static event Action<PlayerCharacterMasterController> onPlayerAdded;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06002CA6 RID: 11430 RVA: 0x000BEBE0 File Offset: 0x000BCDE0
		// (remove) Token: 0x06002CA7 RID: 11431 RVA: 0x000BEC14 File Offset: 0x000BCE14
		public static event Action<PlayerCharacterMasterController> onPlayerRemoved;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06002CA8 RID: 11432 RVA: 0x000BEC48 File Offset: 0x000BCE48
		// (remove) Token: 0x06002CA9 RID: 11433 RVA: 0x000BEC7C File Offset: 0x000BCE7C
		public static event Action<PlayerCharacterMasterController> onLinkedToNetworkUserServerGlobal;

		// Token: 0x06002CAA RID: 11434 RVA: 0x000BECAF File Offset: 0x000BCEAF
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
			GlobalEventManager.onCharacterDeathGlobal += delegate(DamageReport damageReport)
			{
				CharacterMaster characterMaster = damageReport.attackerMaster;
				if (characterMaster)
				{
					if (characterMaster.minionOwnership.ownerMaster)
					{
						characterMaster = characterMaster.minionOwnership.ownerMaster;
					}
					PlayerCharacterMasterController component = characterMaster.GetComponent<PlayerCharacterMasterController>();
					if (component && Util.CheckRoll(1f * component.lunarCoinChanceMultiplier, 0f, null))
					{
						PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.MiscPickups.LunarCoin.miscPickupIndex), damageReport.victim.transform.position, Vector3.up * 10f);
						component.lunarCoinChanceMultiplier *= 0.5f;
					}
				}
			};
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000BECD8 File Offset: 0x000BCED8
		private void SetBodyPrefabToPreference()
		{
			this.master.bodyPrefab = BodyCatalog.GetBodyPrefab(this.networkUser.bodyIndexPreference);
			if (!this.master.bodyPrefab)
			{
				Debug.LogError(string.Format("SetBodyPrefabToPreference failed to find a body prefab for index '{0}'.  Reverting to backup: {1}", this.networkUser.bodyIndexPreference, this.master.backupBodyIndex));
				this.master.bodyPrefab = BodyCatalog.GetBodyPrefab(this.master.backupBodyIndex);
				if (!this.master.bodyPrefab)
				{
					Debug.LogError(string.Format("SetBodyPrefabToPreference backup ({0}) failed.", this.master.backupBodyIndex));
				}
			}
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000BED90 File Offset: 0x000BCF90
		private void OnNetworkUserBodyPreferenceChanged(NetworkUser networkUser)
		{
			if (this.networkUser && networkUser.netId.Value == this.networkUser.netId.Value)
			{
				this.SetBodyPrefabToPreference();
			}
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000BEDD3 File Offset: 0x000BCFD3
		public PlayerCharacterMasterController()
		{
			this.<finalMessageTokenServer>k__BackingField = string.Empty;
			this.cameraMinPitch = -70f;
			this.cameraMaxPitch = 70f;
			this.lunarCoinChanceMultiplier = 0.5f;
			base..ctor();
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000BEE08 File Offset: 0x000BD008
		static PlayerCharacterMasterController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(PlayerCharacterMasterController), PlayerCharacterMasterController.kRpcRpcIncrementRunCount, new NetworkBehaviour.CmdDelegate(PlayerCharacterMasterController.InvokeRpcRpcIncrementRunCount));
			NetworkCRC.RegisterBehaviour("PlayerCharacterMasterController", 0);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000BEE78 File Offset: 0x000BD078
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000BEE8B File Offset: 0x000BD08B
		public NetworkInstanceId NetworknetworkUserInstanceId
		{
			get
			{
				return this.networkUserInstanceId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncNetworkUserInstanceId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkInstanceId>(value, ref this.networkUserInstanceId, 1U);
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000BEECA File Offset: 0x000BD0CA
		protected static void InvokeRpcRpcIncrementRunCount(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcIncrementRunCount called on server.");
				return;
			}
			((PlayerCharacterMasterController)obj).RpcIncrementRunCount();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000BEEF0 File Offset: 0x000BD0F0
		public void CallRpcIncrementRunCount()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcIncrementRunCount called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)PlayerCharacterMasterController.kRpcRpcIncrementRunCount);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcIncrementRunCount");
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000BEF5C File Offset: 0x000BD15C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.networkUserInstanceId);
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
				writer.Write(this.networkUserInstanceId);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000BEFC8 File Offset: 0x000BD1C8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.networkUserInstanceId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncNetworkUserInstanceId(reader.ReadNetworkId());
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002ED4 RID: 11988
		private static List<PlayerCharacterMasterController> _instances = new List<PlayerCharacterMasterController>();

		// Token: 0x04002ED5 RID: 11989
		private static ReadOnlyCollection<PlayerCharacterMasterController> _instancesReadOnly = new ReadOnlyCollection<PlayerCharacterMasterController>(PlayerCharacterMasterController._instances);

		// Token: 0x04002ED6 RID: 11990
		private CharacterBody body;

		// Token: 0x04002ED7 RID: 11991
		private InputBankTest bodyInputs;

		// Token: 0x04002ED8 RID: 11992
		private CharacterMotor bodyMotor;

		// Token: 0x04002EDB RID: 11995
		private PingerController pingerController;

		// Token: 0x04002EDC RID: 11996
		[SyncVar(hook = "OnSyncNetworkUserInstanceId")]
		private NetworkInstanceId networkUserInstanceId;

		// Token: 0x04002EDD RID: 11997
		private GameObject resolvedNetworkUserGameObjectInstance;

		// Token: 0x04002EDE RID: 11998
		private bool networkUserResolved;

		// Token: 0x04002EDF RID: 11999
		private NetworkUser resolvedNetworkUserInstance;

		// Token: 0x04002EE0 RID: 12000
		private bool alreadyLinkedToNetworkUserOnce;

		// Token: 0x04002EE1 RID: 12001
		public float cameraMinPitch;

		// Token: 0x04002EE2 RID: 12002
		public float cameraMaxPitch;

		// Token: 0x04002EE3 RID: 12003
		public GameObject crosshair;

		// Token: 0x04002EE4 RID: 12004
		public Vector3 crosshairPosition;

		// Token: 0x04002EE5 RID: 12005
		private NetworkIdentity netid;

		// Token: 0x04002EE6 RID: 12006
		private static readonly float sprintMinAimMoveDot = Mathf.Cos(1.0471976f);

		// Token: 0x04002EE7 RID: 12007
		private bool sprintInputPressReceived;

		// Token: 0x04002EEC RID: 12012
		private float lunarCoinChanceMultiplier;

		// Token: 0x04002EED RID: 12013
		private static int kRpcRpcIncrementRunCount = 1915650359;
	}
}
