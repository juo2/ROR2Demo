using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using EntityStates;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x020008E6 RID: 2278
	[DisallowMultipleComponent]
	public class VehicleSeat : NetworkBehaviour, IInteractable
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000D7063 File Offset: 0x000D5263
		public CharacterBody currentPassengerBody
		{
			get
			{
				return this.passengerInfo.body;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000D7070 File Offset: 0x000D5270
		public InputBankTest currentPassengerInputBank
		{
			get
			{
				return this.passengerInfo.inputBank;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000D707D File Offset: 0x000D527D
		public string GetContextString(Interactor activator)
		{
			if (!this.passengerBodyObject)
			{
				return Language.GetString(this.enterVehicleContextString);
			}
			if (this.passengerBodyObject == activator.gameObject)
			{
				return Language.GetString(this.exitVehicleContextString);
			}
			return null;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000D70B4 File Offset: 0x000D52B4
		public Interactability GetInteractability(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (!this.passengerBodyObject)
			{
				Interactability? interactability = this.enterVehicleAllowedCheck.Evaluate(component);
				if (interactability == null)
				{
					return Interactability.Available;
				}
				return interactability.GetValueOrDefault();
			}
			else if (this.passengerBodyObject == activator.gameObject && this.passengerAssignmentTime.timeSince >= this.passengerAssignmentCooldown)
			{
				Interactability? interactability = this.exitVehicleAllowedCheck.Evaluate(component);
				if (interactability == null)
				{
					return Interactability.Available;
				}
				return interactability.GetValueOrDefault();
			}
			else
			{
				if (component && component.currentVehicle && component.currentVehicle != this)
				{
					return Interactability.ConditionsNotMet;
				}
				return Interactability.Disabled;
			}
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000D7158 File Offset: 0x000D5358
		public void OnInteractionBegin(Interactor activator)
		{
			if (!this.passengerBodyObject)
			{
				if (this.handleVehicleEnterRequestServer.Evaluate(activator.gameObject) == null)
				{
					this.SetPassenger(activator.gameObject);
					return;
				}
			}
			else if (activator.gameObject == this.passengerBodyObject && this.handleVehicleExitRequestServer.Evaluate(activator.gameObject) == null)
			{
				this.SetPassenger(null);
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldShowOnScanner()
		{
			return true;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000D71CA File Offset: 0x000D53CA
		private static bool shouldLog
		{
			get
			{
				return VehicleSeat.cvVehicleSeatDebug.value;
			}
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000D71D6 File Offset: 0x000D53D6
		private void Awake()
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.collider = base.GetComponent<Collider>();
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000D71F0 File Offset: 0x000D53F0
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (!NetworkServer.active && this.passengerBodyObject)
			{
				this.OnPassengerEnter(this.passengerBodyObject);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000D7218 File Offset: 0x000D5418
		public bool hasPassenger
		{
			get
			{
				return this.passengerBodyObject;
			}
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000D7228 File Offset: 0x000D5428
		private void SetPassengerInternal(GameObject newPassengerBodyObject)
		{
			if (this.passengerBodyObject)
			{
				this.OnPassengerExit(this.passengerBodyObject);
			}
			this.NetworkpassengerBodyObject = newPassengerBodyObject;
			this.passengerInfo = default(VehicleSeat.PassengerInfo);
			this.passengerAssignmentTime = Run.FixedTimeStamp.now;
			if (this.passengerBodyObject)
			{
				this.OnPassengerEnter(this.passengerBodyObject);
			}
			if (VehicleSeat.shouldLog)
			{
				Debug.Log("End SetPassenger.");
			}
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000D7298 File Offset: 0x000D5498
		private void SetPassenger(GameObject newPassengerBodyObject)
		{
			string text = newPassengerBodyObject ? Util.GetBestBodyName(newPassengerBodyObject) : "null";
			if (VehicleSeat.shouldLog)
			{
				Debug.LogFormat("SetPassenger passenger={0}", new object[]
				{
					text
				});
			}
			if (base.syncVarHookGuard)
			{
				if (VehicleSeat.shouldLog)
				{
					Debug.Log("syncVarHookGuard==true Setting passengerBodyObject=newPassengerBodyObject");
				}
				this.NetworkpassengerBodyObject = newPassengerBodyObject;
				return;
			}
			if (VehicleSeat.shouldLog)
			{
				Debug.Log("syncVarHookGuard==false");
			}
			if (this.passengerBodyObject == newPassengerBodyObject)
			{
				if (VehicleSeat.shouldLog)
				{
					Debug.Log("ReferenceEquals(passengerBodyObject, newPassengerBodyObject)==true");
				}
				return;
			}
			if (VehicleSeat.shouldLog)
			{
				Debug.Log("ReferenceEquals(passengerBodyObject, newPassengerBodyObject)==false");
			}
			this.SetPassengerInternal(newPassengerBodyObject);
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000D733B File Offset: 0x000D553B
		private void OnPassengerMovementHit(ref CharacterMotor.MovementHitInfo movementHitInfo)
		{
			if (NetworkServer.active && this.ejectOnCollision && this.passengerAssignmentTime.timeSince > Time.fixedDeltaTime)
			{
				this.SetPassenger(null);
			}
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000D7368 File Offset: 0x000D5568
		private void ForcePassengerState()
		{
			if (this.passengerInfo.bodyStateMachine && this.passengerInfo.hasEffectiveAuthority)
			{
				Type type = this.passengerState.GetType();
				if (this.passengerInfo.bodyStateMachine.state.GetType() != type)
				{
					this.passengerInfo.bodyStateMachine.SetInterruptState(EntityStateCatalog.InstantiateState(this.passengerState), InterruptPriority.Vehicle);
				}
			}
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000D73DF File Offset: 0x000D55DF
		private void Update()
		{
			this.UpdatePassengerPosition();
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000D73E7 File Offset: 0x000D55E7
		private void FixedUpdate()
		{
			this.ForcePassengerState();
			this.UpdatePassengerPosition();
			if (this.passengerInfo.characterMotor)
			{
				this.passengerInfo.characterMotor.velocity = Vector3.zero;
			}
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000D741C File Offset: 0x000D561C
		private void UpdatePassengerPosition()
		{
			Vector3 position = this.seatPosition.position;
			if (this.passengerInfo.characterMotor)
			{
				this.passengerInfo.characterMotor.velocity = Vector3.zero;
				this.passengerInfo.characterMotor.Motor.BaseVelocity = Vector3.zero;
				this.passengerInfo.characterMotor.Motor.SetPosition(position, true);
				if (!this.disablePassengerMotor && Time.inFixedTimeStep)
				{
					this.passengerInfo.characterMotor.rootMotion = position - this.passengerInfo.transform.position;
					return;
				}
			}
			else if (this.passengerInfo.transform)
			{
				this.passengerInfo.transform.position = position;
			}
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000D74E8 File Offset: 0x000D56E8
		[Server]
		public bool AssignPassenger(GameObject bodyObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.VehicleSeat::AssignPassenger(UnityEngine.GameObject)' called on client");
				return false;
			}
			if (this.passengerBodyObject)
			{
				return false;
			}
			if (bodyObject)
			{
				CharacterBody component = bodyObject.GetComponent<CharacterBody>();
				if (component && component.currentVehicle)
				{
					component.currentVehicle.EjectPassenger(bodyObject);
				}
			}
			this.SetPassenger(bodyObject);
			return true;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000D7552 File Offset: 0x000D5752
		[Server]
		public void EjectPassenger(GameObject bodyObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VehicleSeat::EjectPassenger(UnityEngine.GameObject)' called on client");
				return;
			}
			if (bodyObject == this.passengerBodyObject)
			{
				this.SetPassenger(null);
			}
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000D7579 File Offset: 0x000D5779
		[Server]
		public void EjectPassenger()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VehicleSeat::EjectPassenger()' called on client");
				return;
			}
			this.SetPassenger(null);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000D7597 File Offset: 0x000D5797
		private void OnDestroy()
		{
			this.SetPassenger(null);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000D75A0 File Offset: 0x000D57A0
		private void OnPassengerEnter(GameObject passenger)
		{
			this.passengerInfo = new VehicleSeat.PassengerInfo(this.passengerBodyObject);
			if (this.passengerInfo.body)
			{
				this.passengerInfo.body.currentVehicle = this;
			}
			if (this.hidePassenger && this.passengerInfo.characterModel)
			{
				this.passengerInfo.characterModel.invisibilityCount++;
			}
			this.ForcePassengerState();
			if (this.passengerInfo.characterMotor)
			{
				if (this.disablePassengerMotor)
				{
					this.passengerInfo.characterMotor.enabled = false;
				}
				else
				{
					this.passengerInfo.characterMotor.onMovementHit += this.OnPassengerMovementHit;
				}
			}
			if (this.passengerInfo.collider && this.collider)
			{
				Physics.IgnoreCollision(this.collider, this.passengerInfo.collider, true);
			}
			if (this.passengerInfo.interactionDriver)
			{
				this.passengerInfo.interactionDriver.interactableOverride = base.gameObject;
			}
			if (VehicleSeat.shouldLog)
			{
				Debug.Log("Taking control of passengerBodyObject.");
				Debug.Log(this.passengerInfo.GetString());
			}
			Action<GameObject> action = this.onPassengerEnter;
			if (action != null)
			{
				action(this.passengerBodyObject);
			}
			UnityEvent unityEvent = this.onPassengerEnterUnityEvent;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			Action<VehicleSeat, GameObject> action2 = VehicleSeat.onPassengerEnterGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this, this.passengerBodyObject);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000D7720 File Offset: 0x000D5920
		private void OnPassengerExit(GameObject passenger)
		{
			if (VehicleSeat.shouldLog)
			{
				Debug.Log("Releasing passenger.");
			}
			if (this.hidePassenger && this.passengerInfo.characterModel)
			{
				this.passengerInfo.characterModel.invisibilityCount--;
			}
			if (this.passengerInfo.body)
			{
				this.passengerInfo.body.currentVehicle = null;
			}
			if (this.passengerInfo.characterMotor)
			{
				if (this.disablePassengerMotor)
				{
					this.passengerInfo.characterMotor.enabled = true;
				}
				else
				{
					this.passengerInfo.characterMotor.onMovementHit -= this.OnPassengerMovementHit;
				}
				this.passengerInfo.characterMotor.velocity = Vector3.zero;
				this.passengerInfo.characterMotor.rootMotion = Vector3.zero;
				this.passengerInfo.characterMotor.Motor.BaseVelocity = Vector3.zero;
			}
			if (this.passengerInfo.collider && this.collider)
			{
				Physics.IgnoreCollision(this.collider, this.passengerInfo.collider, false);
			}
			if (this.passengerInfo.hasEffectiveAuthority)
			{
				if (this.passengerInfo.bodyStateMachine && this.passengerInfo.bodyStateMachine.CanInterruptState(InterruptPriority.Vehicle))
				{
					this.passengerInfo.bodyStateMachine.SetNextStateToMain();
				}
				Vector3 newPosition = this.exitPosition ? this.exitPosition.position : this.seatPosition.position;
				TeleportHelper.TeleportGameObject(this.passengerInfo.transform.gameObject, newPosition);
			}
			if (this.passengerInfo.interactionDriver && this.passengerInfo.interactionDriver.interactableOverride == base.gameObject)
			{
				this.passengerInfo.interactionDriver.interactableOverride = null;
			}
			if (this.rigidbody && this.passengerInfo.characterMotor)
			{
				this.passengerInfo.characterMotor.velocity = this.rigidbody.velocity * this.exitVelocityFraction;
			}
			Action<GameObject> action = this.onPassengerExit;
			if (action != null)
			{
				action(this.passengerBodyObject);
			}
			UnityEvent unityEvent = this.onPassengerExitUnityEvent;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			Action<VehicleSeat, GameObject> action2 = VehicleSeat.onPassengerExitGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this, this.passengerBodyObject);
		}

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06003321 RID: 13089 RVA: 0x000D7994 File Offset: 0x000D5B94
		// (remove) Token: 0x06003322 RID: 13090 RVA: 0x000D79CC File Offset: 0x000D5BCC
		public event Action<GameObject> onPassengerEnter;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06003323 RID: 13091 RVA: 0x000D7A04 File Offset: 0x000D5C04
		// (remove) Token: 0x06003324 RID: 13092 RVA: 0x000D7A3C File Offset: 0x000D5C3C
		public event Action<GameObject> onPassengerExit;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06003325 RID: 13093 RVA: 0x000D7A74 File Offset: 0x000D5C74
		// (remove) Token: 0x06003326 RID: 13094 RVA: 0x000D7AA8 File Offset: 0x000D5CA8
		public static event Action<VehicleSeat, GameObject> onPassengerEnterGlobal;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06003327 RID: 13095 RVA: 0x000D7ADC File Offset: 0x000D5CDC
		// (remove) Token: 0x06003328 RID: 13096 RVA: 0x000D7B10 File Offset: 0x000D5D10
		public static event Action<VehicleSeat, GameObject> onPassengerExitGlobal;

		// Token: 0x0600332B RID: 13099 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000D7BC8 File Offset: 0x000D5DC8
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x000D7BDC File Offset: 0x000D5DDC
		public GameObject NetworkpassengerBodyObject
		{
			get
			{
				return this.passengerBodyObject;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetPassenger(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVarGameObject(value, ref this.passengerBodyObject, 1U, ref this.___passengerBodyObjectNetId);
			}
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000D7C2C File Offset: 0x000D5E2C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.passengerBodyObject);
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
				writer.Write(this.passengerBodyObject);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000D7C98 File Offset: 0x000D5E98
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.___passengerBodyObjectNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SetPassenger(reader.ReadGameObject());
			}
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000D7CD9 File Offset: 0x000D5ED9
		public override void PreStartClient()
		{
			if (!this.___passengerBodyObjectNetId.IsEmpty())
			{
				this.NetworkpassengerBodyObject = ClientScene.FindLocalObject(this.___passengerBodyObjectNetId);
			}
		}

		// Token: 0x04003421 RID: 13345
		public SerializableEntityStateType passengerState;

		// Token: 0x04003422 RID: 13346
		public Transform seatPosition;

		// Token: 0x04003423 RID: 13347
		public Transform exitPosition;

		// Token: 0x04003424 RID: 13348
		public bool ejectOnCollision;

		// Token: 0x04003425 RID: 13349
		public bool hidePassenger = true;

		// Token: 0x04003426 RID: 13350
		public float exitVelocityFraction = 1f;

		// Token: 0x04003427 RID: 13351
		public UnityEvent onPassengerEnterUnityEvent;

		// Token: 0x04003428 RID: 13352
		[FormerlySerializedAs("OnPassengerExitUnityEvent")]
		public UnityEvent onPassengerExitUnityEvent;

		// Token: 0x04003429 RID: 13353
		public string enterVehicleContextString;

		// Token: 0x0400342A RID: 13354
		public string exitVehicleContextString;

		// Token: 0x0400342B RID: 13355
		public bool disablePassengerMotor;

		// Token: 0x0400342C RID: 13356
		public bool isEquipmentActivationAllowed;

		// Token: 0x0400342D RID: 13357
		[SyncVar(hook = "SetPassenger")]
		private GameObject passengerBodyObject;

		// Token: 0x0400342E RID: 13358
		private VehicleSeat.PassengerInfo passengerInfo;

		// Token: 0x0400342F RID: 13359
		private Rigidbody rigidbody;

		// Token: 0x04003430 RID: 13360
		private Collider collider;

		// Token: 0x04003431 RID: 13361
		public CallbackCheck<Interactability, CharacterBody> enterVehicleAllowedCheck = new CallbackCheck<Interactability, CharacterBody>();

		// Token: 0x04003432 RID: 13362
		public CallbackCheck<Interactability, CharacterBody> exitVehicleAllowedCheck = new CallbackCheck<Interactability, CharacterBody>();

		// Token: 0x04003433 RID: 13363
		public CallbackCheck<bool, GameObject> handleVehicleEnterRequestServer = new CallbackCheck<bool, GameObject>();

		// Token: 0x04003434 RID: 13364
		public CallbackCheck<bool, GameObject> handleVehicleExitRequestServer = new CallbackCheck<bool, GameObject>();

		// Token: 0x04003435 RID: 13365
		private static readonly BoolConVar cvVehicleSeatDebug = new BoolConVar("vehicle_seat_debug", ConVarFlags.None, "0", "Enables debug logging for VehicleSeat.");

		// Token: 0x04003436 RID: 13366
		private Run.FixedTimeStamp passengerAssignmentTime = Run.FixedTimeStamp.negativeInfinity;

		// Token: 0x04003437 RID: 13367
		private readonly float passengerAssignmentCooldown = 0.2f;

		// Token: 0x0400343C RID: 13372
		private NetworkInstanceId ___passengerBodyObjectNetId;

		// Token: 0x020008E7 RID: 2279
		private struct PassengerInfo
		{
			// Token: 0x06003331 RID: 13105 RVA: 0x000D7D00 File Offset: 0x000D5F00
			public PassengerInfo(GameObject passengerBodyObject)
			{
				this.transform = passengerBodyObject.transform;
				this.body = passengerBodyObject.GetComponent<CharacterBody>();
				this.inputBank = passengerBodyObject.GetComponent<InputBankTest>();
				this.interactionDriver = passengerBodyObject.GetComponent<InteractionDriver>();
				this.characterMotor = passengerBodyObject.GetComponent<CharacterMotor>();
				this.networkIdentity = passengerBodyObject.GetComponent<NetworkIdentity>();
				this.collider = passengerBodyObject.GetComponent<Collider>();
				this.bodyStateMachine = null;
				passengerBodyObject.GetComponents<EntityStateMachine>(VehicleSeat.PassengerInfo.sharedBuffer);
				for (int i = 0; i < VehicleSeat.PassengerInfo.sharedBuffer.Count; i++)
				{
					EntityStateMachine entityStateMachine = VehicleSeat.PassengerInfo.sharedBuffer[i];
					if (string.CompareOrdinal(entityStateMachine.customName, "Body") == 0)
					{
						this.bodyStateMachine = entityStateMachine;
						break;
					}
				}
				VehicleSeat.PassengerInfo.sharedBuffer.Clear();
				this.characterModel = null;
				if (this.body.modelLocator && this.body.modelLocator.modelTransform)
				{
					this.characterModel = this.body.modelLocator.modelTransform.GetComponent<CharacterModel>();
				}
			}

			// Token: 0x170004C0 RID: 1216
			// (get) Token: 0x06003332 RID: 13106 RVA: 0x000D7E04 File Offset: 0x000D6004
			public bool hasEffectiveAuthority
			{
				get
				{
					return Util.HasEffectiveAuthority(this.networkIdentity);
				}
			}

			// Token: 0x06003333 RID: 13107 RVA: 0x000D7E14 File Offset: 0x000D6014
			public string GetString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("transform=").Append(this.transform).AppendLine();
				stringBuilder.Append("body=").Append(this.body).AppendLine();
				stringBuilder.Append("inputBank=").Append(this.inputBank).AppendLine();
				stringBuilder.Append("interactionDriver=").Append(this.interactionDriver).AppendLine();
				stringBuilder.Append("characterMotor=").Append(this.characterMotor).AppendLine();
				stringBuilder.Append("bodyStateMachine=").Append(this.bodyStateMachine).AppendLine();
				stringBuilder.Append("characterModel=").Append(this.characterModel).AppendLine();
				stringBuilder.Append("networkIdentity=").Append(this.networkIdentity).AppendLine();
				stringBuilder.Append("hasEffectiveAuthority=").Append(this.hasEffectiveAuthority).AppendLine();
				return stringBuilder.ToString();
			}

			// Token: 0x0400343D RID: 13373
			private static readonly List<EntityStateMachine> sharedBuffer = new List<EntityStateMachine>();

			// Token: 0x0400343E RID: 13374
			public readonly Transform transform;

			// Token: 0x0400343F RID: 13375
			public readonly CharacterBody body;

			// Token: 0x04003440 RID: 13376
			public readonly InputBankTest inputBank;

			// Token: 0x04003441 RID: 13377
			public readonly InteractionDriver interactionDriver;

			// Token: 0x04003442 RID: 13378
			public readonly CharacterMotor characterMotor;

			// Token: 0x04003443 RID: 13379
			public readonly EntityStateMachine bodyStateMachine;

			// Token: 0x04003444 RID: 13380
			public readonly CharacterModel characterModel;

			// Token: 0x04003445 RID: 13381
			public readonly NetworkIdentity networkIdentity;

			// Token: 0x04003446 RID: 13382
			public readonly Collider collider;
		}

		// Token: 0x020008E8 RID: 2280
		// (Invoke) Token: 0x06003336 RID: 13110
		public delegate void InteractabilityCheckDelegate(CharacterBody activator, ref Interactability? resultOverride);
	}
}
