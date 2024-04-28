using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200078D RID: 1933
	[RequireComponent(typeof(NetworkedBodyAttachment))]
	public class JetpackController : NetworkBehaviour
	{
		// Token: 0x060028C3 RID: 10435 RVA: 0x000B0CE4 File Offset: 0x000AEEE4
		public static JetpackController FindJetpackController(GameObject targetObject)
		{
			if (!targetObject)
			{
				return null;
			}
			for (int i = 0; i < JetpackController.instancesList.Count; i++)
			{
				if (JetpackController.instancesList[i].networkedBodyAttachment.attachedBodyObject == targetObject)
				{
					return JetpackController.instancesList[i];
				}
			}
			return null;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000B0D3A File Offset: 0x000AEF3A
		private CharacterBody targetBody
		{
			get
			{
				return this.networkedBodyAttachment.attachedBody;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000B0D47 File Offset: 0x000AEF47
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x000B0D50 File Offset: 0x000AEF50
		private bool providingAntiGravity
		{
			get
			{
				return this._providingAntiGravity;
			}
			set
			{
				if (this._providingAntiGravity == value)
				{
					return;
				}
				this._providingAntiGravity = value;
				if (this.targetCharacterGravityParameterProvider != null)
				{
					CharacterGravityParameters gravityParameters = this.targetCharacterGravityParameterProvider.gravityParameters;
					gravityParameters.channeledAntiGravityGranterCount += (this._providingAntiGravity ? 1 : -1);
					this.targetCharacterGravityParameterProvider.gravityParameters = gravityParameters;
				}
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000B0DA5 File Offset: 0x000AEFA5
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x000B0DB0 File Offset: 0x000AEFB0
		private bool providingFlight
		{
			get
			{
				return this._providingFlight;
			}
			set
			{
				if (this._providingFlight == value)
				{
					return;
				}
				this._providingFlight = value;
				if (this.targetCharacterFlightParameterProvider != null)
				{
					CharacterFlightParameters flightParameters = this.targetCharacterFlightParameterProvider.flightParameters;
					flightParameters.channeledFlightGranterCount += (this._providingFlight ? 1 : -1);
					this.targetCharacterFlightParameterProvider.flightParameters = flightParameters;
				}
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000B0E05 File Offset: 0x000AF005
		private void Awake()
		{
			this.networkedBodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000B0E13 File Offset: 0x000AF013
		private void Start()
		{
			this.SetupWings();
			if (this.targetBody)
			{
				this.targetCharacterGravityParameterProvider = this.targetBody.GetComponent<ICharacterGravityParameterProvider>();
				this.targetCharacterFlightParameterProvider = this.targetBody.GetComponent<ICharacterFlightParameterProvider>();
				this.StartFlight();
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000B0E50 File Offset: 0x000AF050
		private void StartFlight()
		{
			Debug.Log("Starting flight");
			this.providingAntiGravity = true;
			this.providingFlight = true;
			if (this.targetBody.hasEffectiveAuthority && this.targetBody.characterMotor && this.targetBody.characterMotor.isGrounded)
			{
				Vector3 velocity = this.targetBody.characterMotor.velocity;
				velocity.y = 15f;
				this.targetBody.characterMotor.velocity = velocity;
				this.targetBody.characterMotor.Motor.ForceUnground();
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000B0EE9 File Offset: 0x000AF0E9
		private void OnDestroy()
		{
			this.ShowMotionLines(false);
			if (this.targetBody)
			{
				this.providingFlight = false;
				this.targetCharacterFlightParameterProvider = null;
				this.providingAntiGravity = false;
				this.targetCharacterGravityParameterProvider = null;
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000B0F1B File Offset: 0x000AF11B
		private void OnEnable()
		{
			JetpackController.instancesList.Add(this);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000B0F28 File Offset: 0x000AF128
		private void OnDisable()
		{
			JetpackController.instancesList.Remove(this);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000B0F38 File Offset: 0x000AF138
		private void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			this.boostCooldownTimer -= Time.fixedDeltaTime;
			if (this.targetBody)
			{
				base.transform.position = this.targetBody.transform.position;
				if (this.targetBody.hasEffectiveAuthority && this.targetBody.characterMotor)
				{
					if (this.stopwatch < this.duration)
					{
						if (this.boostCooldownTimer <= 0f && this.targetBody.inputBank.jump.justPressed && this.targetBody.inputBank.moveVector != Vector3.zero)
						{
							this.boostCooldownTimer = this.boostCooldown;
							this.targetBody.characterMotor.velocity = this.targetBody.inputBank.moveVector * (this.targetBody.moveSpeed * this.boostSpeedMultiplier);
							this.targetBody.characterMotor.disableAirControlUntilCollision = false;
						}
					}
					else
					{
						Vector3 velocity = this.targetBody.characterMotor.velocity;
						velocity.y = Mathf.Max(velocity.y, -5f);
						this.targetBody.characterMotor.velocity = velocity;
						this.providingAntiGravity = false;
						this.providingFlight = false;
					}
				}
			}
			if (this.stopwatch >= this.duration)
			{
				bool flag = !this.targetBody.characterMotor || !this.targetBody.characterMotor.isGrounded;
				if (this.wingAnimator && !flag)
				{
					this.wingAnimator.SetBool("wingsReady", false);
				}
				if (NetworkServer.active && !flag)
				{
					UnityEngine.Object.Destroy(base.gameObject);
					return;
				}
			}
			else
			{
				float num = 4f;
				if (this.targetBody.characterMotor)
				{
					float magnitude = this.targetBody.characterMotor.velocity.magnitude;
					float moveSpeed = this.targetBody.moveSpeed;
					if (magnitude != 0f && moveSpeed != 0f)
					{
						num += magnitude / moveSpeed * 6f;
					}
				}
				if (this.wingAnimator)
				{
					this.wingAnimator.SetBool("wingsReady", true);
					this.wingAnimator.SetFloat("fly.playbackRate", num, 0.1f, Time.fixedDeltaTime);
					this.ShowMotionLines(true);
				}
			}
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000B11BD File Offset: 0x000AF3BD
		public void ResetTimer()
		{
			this.stopwatch = 0f;
			this.StartFlight();
			if (NetworkServer.active)
			{
				this.CallRpcResetTimer();
			}
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000B11DD File Offset: 0x000AF3DD
		[ClientRpc]
		private void RpcResetTimer()
		{
			if (NetworkServer.active)
			{
				return;
			}
			this.ResetTimer();
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000B11F0 File Offset: 0x000AF3F0
		private Transform FindWings()
		{
			ModelLocator modelLocator = this.targetBody.modelLocator;
			if (modelLocator)
			{
				Transform modelTransform = modelLocator.modelTransform;
				if (modelTransform)
				{
					CharacterModel component = modelTransform.GetComponent<CharacterModel>();
					if (component)
					{
						List<GameObject> equipmentDisplayObjects = component.GetEquipmentDisplayObjects(RoR2Content.Equipment.Jetpack.equipmentIndex);
						if (equipmentDisplayObjects.Count > 0)
						{
							return equipmentDisplayObjects[0].transform;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000B1258 File Offset: 0x000AF458
		private void ShowMotionLines(bool showWings)
		{
			if (this.wingMotions == null)
			{
				return;
			}
			for (int i = 0; i < this.wingMotions.Length; i++)
			{
				if (this.wingMotions[i])
				{
					this.wingMotions[i].SetActive(showWings);
				}
			}
			if (this.wingMeshObject)
			{
				this.wingMeshObject.SetActive(!showWings);
			}
			if (this.hasBegunSoundLoop != showWings)
			{
				if (showWings)
				{
					if (showWings)
					{
						Util.PlaySound("Play_item_use_bugWingFlapLoop", base.gameObject);
					}
				}
				else
				{
					Util.PlaySound("Stop_item_use_bugWingFlapLoop", base.gameObject);
				}
				this.hasBegunSoundLoop = showWings;
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000B12F8 File Offset: 0x000AF4F8
		public void SetupWings()
		{
			this.wingTransform = this.FindWings();
			if (this.wingTransform)
			{
				this.wingAnimator = this.wingTransform.GetComponentInChildren<Animator>();
				ChildLocator component = this.wingTransform.GetComponent<ChildLocator>();
				if (this.wingAnimator)
				{
					this.wingAnimator.SetBool("wingsReady", true);
				}
				if (component)
				{
					this.wingMotions = new GameObject[4];
					this.wingMotions[0] = component.FindChild("WingMotionLargeL").gameObject;
					this.wingMotions[1] = component.FindChild("WingMotionLargeR").gameObject;
					this.wingMotions[2] = component.FindChild("WingMotionSmallL").gameObject;
					this.wingMotions[3] = component.FindChild("WingMotionSmallR").gameObject;
					this.wingMeshObject = component.FindChild("WingMesh").gameObject;
				}
			}
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000B1408 File Offset: 0x000AF608
		static JetpackController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(JetpackController), JetpackController.kRpcRpcResetTimer, new NetworkBehaviour.CmdDelegate(JetpackController.InvokeRpcRpcResetTimer));
			NetworkCRC.RegisterBehaviour("JetpackController", 0);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000B1458 File Offset: 0x000AF658
		protected static void InvokeRpcRpcResetTimer(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcResetTimer called on server.");
				return;
			}
			((JetpackController)obj).RpcResetTimer();
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000B147C File Offset: 0x000AF67C
		public void CallRpcResetTimer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcResetTimer called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)JetpackController.kRpcRpcResetTimer);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcResetTimer");
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000B14E8 File Offset: 0x000AF6E8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002C43 RID: 11331
		private static readonly List<JetpackController> instancesList = new List<JetpackController>();

		// Token: 0x04002C44 RID: 11332
		private NetworkedBodyAttachment networkedBodyAttachment;

		// Token: 0x04002C45 RID: 11333
		public float duration;

		// Token: 0x04002C46 RID: 11334
		public float acceleration;

		// Token: 0x04002C47 RID: 11335
		public float boostSpeedMultiplier = 3f;

		// Token: 0x04002C48 RID: 11336
		public float boostCooldown = 0.5f;

		// Token: 0x04002C49 RID: 11337
		private float stopwatch;

		// Token: 0x04002C4A RID: 11338
		private Transform wingTransform;

		// Token: 0x04002C4B RID: 11339
		private Animator wingAnimator;

		// Token: 0x04002C4C RID: 11340
		private GameObject[] wingMotions;

		// Token: 0x04002C4D RID: 11341
		private GameObject wingMeshObject;

		// Token: 0x04002C4E RID: 11342
		private float boostCooldownTimer;

		// Token: 0x04002C4F RID: 11343
		private bool hasBegunSoundLoop;

		// Token: 0x04002C50 RID: 11344
		private ICharacterGravityParameterProvider targetCharacterGravityParameterProvider;

		// Token: 0x04002C51 RID: 11345
		private ICharacterFlightParameterProvider targetCharacterFlightParameterProvider;

		// Token: 0x04002C52 RID: 11346
		private bool _providingAntiGravity;

		// Token: 0x04002C53 RID: 11347
		private bool _providingFlight;

		// Token: 0x04002C54 RID: 11348
		private static int kRpcRpcResetTimer = 1278379706;
	}
}
