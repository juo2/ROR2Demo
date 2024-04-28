using System;
using System.Collections.Generic;
using EntityStates;
using JetBrains.Annotations;
using RoR2.CharacterAI;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006C0 RID: 1728
	public class EntityStateMachine : MonoBehaviour
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x00090A33 File Offset: 0x0008EC33
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x00090A3B File Offset: 0x0008EC3B
		public EntityState state { get; private set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x00090A44 File Offset: 0x0008EC44
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x00090A4C File Offset: 0x0008EC4C
		public NetworkStateMachine networker { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x00090A55 File Offset: 0x0008EC55
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x00090A5D File Offset: 0x0008EC5D
		public NetworkIdentity networkIdentity { get; set; }

		// Token: 0x060021A6 RID: 8614 RVA: 0x00090A66 File Offset: 0x0008EC66
		public void SetNextState(EntityState newNextState)
		{
			EntityStateMachine.ModifyNextStateDelegate modifyNextStateDelegate = this.nextStateModifier;
			if (modifyNextStateDelegate != null)
			{
				modifyNextStateDelegate(this, ref newNextState);
			}
			this.nextState = newNextState;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00090A83 File Offset: 0x0008EC83
		public void SetNextStateToMain()
		{
			this.SetNextState(EntityStateCatalog.InstantiateState(this.mainStateType));
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00090A96 File Offset: 0x0008EC96
		public bool CanInterruptState(InterruptPriority interruptPriority)
		{
			return (this.nextState ?? this.state).GetMinimumInterruptPriority() <= interruptPriority;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00090AB3 File Offset: 0x0008ECB3
		public bool SetInterruptState(EntityState newNextState, InterruptPriority interruptPriority)
		{
			if (this.CanInterruptState(interruptPriority))
			{
				this.SetNextState(newNextState);
				return true;
			}
			return false;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x00090AC8 File Offset: 0x0008ECC8
		public bool HasPendingState()
		{
			return this.nextState != null;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x00090AD3 File Offset: 0x0008ECD3
		public bool IsInMainState()
		{
			return this.state != null && this.state.GetType() == this.mainStateType.stateType;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x00090AFA File Offset: 0x0008ECFA
		public bool IsInInitialState()
		{
			return this.state != null && this.state.GetType() == this.initialStateType.stateType;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x00090B24 File Offset: 0x0008ED24
		public void SetState([NotNull] EntityState newState)
		{
			this.nextState = null;
			newState.outer = this;
			if (this.state == null)
			{
				Debug.LogErrorFormat("State machine {0} on object {1} does not have a state!", new object[]
				{
					this.customName,
					base.gameObject
				});
			}
			this.state.ModifyNextState(newState);
			this.state.OnExit();
			this.state = newState;
			this.state.OnEnter();
			if (this.networkIndex != -1)
			{
				if (!this.networker)
				{
					Debug.LogErrorFormat("State machine {0} on object {1} does not have a networker assigned!", new object[]
					{
						this.customName,
						base.gameObject
					});
				}
				this.networker.SendSetEntityState(this.networkIndex);
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x00090BDC File Offset: 0x0008EDDC
		private void Awake()
		{
			if (!this.networker)
			{
				this.networker = base.GetComponent<NetworkStateMachine>();
			}
			if (!this.networkIdentity)
			{
				this.networkIdentity = base.GetComponent<NetworkIdentity>();
			}
			this.commonComponents = new EntityStateMachine.CommonComponentCache(base.gameObject);
			this.state = new Uninitialized();
			this.state.outer = this;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x00090C44 File Offset: 0x0008EE44
		private void Start()
		{
			if (this.nextState != null && this.networker && !this.networker.hasAuthority)
			{
				this.SetState(this.nextState);
				return;
			}
			Type stateType = this.initialStateType.stateType;
			if (this.state is Uninitialized && stateType != null && stateType.IsSubclassOf(typeof(EntityState)))
			{
				this.SetState(EntityStateCatalog.InstantiateState(stateType));
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00090CC0 File Offset: 0x0008EEC0
		public void Update()
		{
			this.state.Update();
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00090CCD File Offset: 0x0008EECD
		public void FixedUpdate()
		{
			if (this.nextState != null)
			{
				this.SetState(this.nextState);
			}
			this.state.FixedUpdate();
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x00090CEE File Offset: 0x0008EEEE
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x00090CF6 File Offset: 0x0008EEF6
		public bool destroying { get; private set; }

		// Token: 0x060021B4 RID: 8628 RVA: 0x00090CFF File Offset: 0x0008EEFF
		private void OnDestroy()
		{
			this.destroying = true;
			if (this.state != null)
			{
				this.state.OnExit();
				this.state = null;
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00090D24 File Offset: 0x0008EF24
		private void OnValidate()
		{
			if (this.mainStateType.stateType == null)
			{
				if (this.customName == "Body")
				{
					if (base.GetComponent<CharacterMotor>())
					{
						this.mainStateType = new SerializableEntityStateType(typeof(GenericCharacterMain));
						return;
					}
					if (base.GetComponent<RigidbodyMotor>())
					{
						this.mainStateType = new SerializableEntityStateType(typeof(FlyState));
						return;
					}
				}
				else
				{
					if (this.customName == "Weapon")
					{
						this.mainStateType = new SerializableEntityStateType(typeof(Idle));
						return;
					}
					if (this.customName == "AI")
					{
						BaseAI component = base.GetComponent<BaseAI>();
						if (component)
						{
							this.mainStateType = component.scanState;
						}
					}
				}
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00090DF4 File Offset: 0x0008EFF4
		public static EntityStateMachine FindByCustomName(GameObject gameObject, string customName)
		{
			List<EntityStateMachine> gameObjectComponents = GetComponentsCache<EntityStateMachine>.GetGameObjectComponents(gameObject);
			EntityStateMachine result = null;
			int i = 0;
			int count = gameObjectComponents.Count;
			while (i < count)
			{
				if (string.CompareOrdinal(customName, gameObjectComponents[i].customName) == 0)
				{
					result = gameObjectComponents[i];
					break;
				}
				i++;
			}
			GetComponentsCache<EntityStateMachine>.ReturnBuffer(gameObjectComponents);
			return result;
		}

		// Token: 0x04002703 RID: 9987
		private EntityState nextState;

		// Token: 0x04002704 RID: 9988
		[Tooltip("The name of this state machine.")]
		public string customName;

		// Token: 0x04002705 RID: 9989
		[Tooltip("The type of the state to enter when this component is first activated.")]
		public SerializableEntityStateType initialStateType = new SerializableEntityStateType(typeof(TestState1));

		// Token: 0x04002706 RID: 9990
		[Tooltip("The preferred main state of this state machine.")]
		public SerializableEntityStateType mainStateType;

		// Token: 0x04002709 RID: 9993
		public EntityStateMachine.CommonComponentCache commonComponents;

		// Token: 0x0400270A RID: 9994
		[NonSerialized]
		public int networkIndex = -1;

		// Token: 0x0400270B RID: 9995
		public EntityStateMachine.ModifyNextStateDelegate nextStateModifier;

		// Token: 0x020006C1 RID: 1729
		public struct CommonComponentCache
		{
			// Token: 0x060021B8 RID: 8632 RVA: 0x00090E68 File Offset: 0x0008F068
			public CommonComponentCache(GameObject gameObject)
			{
				this.transform = gameObject.transform;
				this.characterBody = gameObject.GetComponent<CharacterBody>();
				this.characterMotor = gameObject.GetComponent<CharacterMotor>();
				this.characterDirection = gameObject.GetComponent<CharacterDirection>();
				this.rigidbody = gameObject.GetComponent<Rigidbody>();
				this.rigidbodyMotor = gameObject.GetComponent<RigidbodyMotor>();
				this.rigidbodyDirection = gameObject.GetComponent<RigidbodyDirection>();
				this.railMotor = gameObject.GetComponent<RailMotor>();
				this.modelLocator = gameObject.GetComponent<ModelLocator>();
				this.inputBank = gameObject.GetComponent<InputBankTest>();
				this.teamComponent = gameObject.GetComponent<TeamComponent>();
				this.healthComponent = gameObject.GetComponent<HealthComponent>();
				this.skillLocator = gameObject.GetComponent<SkillLocator>();
				this.characterEmoteDefinitions = gameObject.GetComponent<CharacterEmoteDefinitions>();
				this.cameraTargetParams = gameObject.GetComponent<CameraTargetParams>();
				this.sfxLocator = gameObject.GetComponent<SfxLocator>();
				this.bodyAnimatorSmoothingParameters = gameObject.GetComponent<BodyAnimatorSmoothingParameters>();
				this.projectileController = gameObject.GetComponent<ProjectileController>();
			}

			// Token: 0x0400270D RID: 9997
			public readonly Transform transform;

			// Token: 0x0400270E RID: 9998
			public readonly CharacterBody characterBody;

			// Token: 0x0400270F RID: 9999
			public readonly CharacterMotor characterMotor;

			// Token: 0x04002710 RID: 10000
			public readonly CharacterDirection characterDirection;

			// Token: 0x04002711 RID: 10001
			public readonly Rigidbody rigidbody;

			// Token: 0x04002712 RID: 10002
			public readonly RigidbodyMotor rigidbodyMotor;

			// Token: 0x04002713 RID: 10003
			public readonly RigidbodyDirection rigidbodyDirection;

			// Token: 0x04002714 RID: 10004
			public readonly RailMotor railMotor;

			// Token: 0x04002715 RID: 10005
			public readonly ModelLocator modelLocator;

			// Token: 0x04002716 RID: 10006
			public readonly InputBankTest inputBank;

			// Token: 0x04002717 RID: 10007
			public readonly TeamComponent teamComponent;

			// Token: 0x04002718 RID: 10008
			public readonly HealthComponent healthComponent;

			// Token: 0x04002719 RID: 10009
			public readonly SkillLocator skillLocator;

			// Token: 0x0400271A RID: 10010
			public readonly CharacterEmoteDefinitions characterEmoteDefinitions;

			// Token: 0x0400271B RID: 10011
			public readonly CameraTargetParams cameraTargetParams;

			// Token: 0x0400271C RID: 10012
			public readonly SfxLocator sfxLocator;

			// Token: 0x0400271D RID: 10013
			public readonly BodyAnimatorSmoothingParameters bodyAnimatorSmoothingParameters;

			// Token: 0x0400271E RID: 10014
			public readonly ProjectileController projectileController;
		}

		// Token: 0x020006C2 RID: 1730
		// (Invoke) Token: 0x060021BA RID: 8634
		public delegate void ModifyNextStateDelegate(EntityStateMachine entityStateMachine, ref EntityState newNextState);
	}
}
