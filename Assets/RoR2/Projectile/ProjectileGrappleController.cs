using System;
using EntityStates;
using EntityStates.Loader;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B95 RID: 2965
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileSimple))]
	[RequireComponent(typeof(EntityStateMachine))]
	[RequireComponent(typeof(ProjectileStickOnImpact))]
	public class ProjectileGrappleController : MonoBehaviour
	{
		// Token: 0x06004383 RID: 17283 RVA: 0x00118544 File Offset: 0x00116744
		private void Awake()
		{
			this.projectileStickOnImpactController = base.GetComponent<ProjectileStickOnImpact>();
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileSimple = base.GetComponent<ProjectileSimple>();
			this.resolvedOwnerHookStateType = this.ownerHookStateType.stateType;
			if (this.ropeEndTransform)
			{
				this.soundID = Util.PlaySound(this.enterSoundString, this.ropeEndTransform.gameObject);
			}
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x001185B0 File Offset: 0x001167B0
		private void FixedUpdate()
		{
			if (this.ropeEndTransform)
			{
				float in_value = Util.Remap((this.ropeEndTransform.transform.position - base.transform.position).magnitude, this.minHookDistancePitchModifier, this.maxHookDistancePitchModifier, 0f, 100f);
				AkSoundEngine.SetRTPCValueByPlayingID(this.hookDistanceRTPCstring, in_value, this.soundID);
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x00118624 File Offset: 0x00116824
		private void AssignHookReferenceToBodyStateMachine()
		{
			FireHook fireHook;
			if (this.owner.stateMachine && (fireHook = (this.owner.stateMachine.state as FireHook)) != null)
			{
				fireHook.SetHookReference(base.gameObject);
			}
			Transform modelTransform = this.owner.gameObject.GetComponent<ModelLocator>().modelTransform;
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(this.muzzleStringOnBody);
					if (transform)
					{
						this.ropeEndTransform.SetParent(transform, false);
					}
				}
			}
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x001186B7 File Offset: 0x001168B7
		private void Start()
		{
			this.owner = new ProjectileGrappleController.OwnerInfo(this.projectileController.owner);
			this.AssignHookReferenceToBodyStateMachine();
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x001186D8 File Offset: 0x001168D8
		private void OnDestroy()
		{
			if (this.ropeEndTransform)
			{
				Util.PlaySound(this.exitSoundString, this.ropeEndTransform.gameObject);
				UnityEngine.Object.Destroy(this.ropeEndTransform.gameObject);
				return;
			}
			AkSoundEngine.StopPlayingID(this.soundID);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x00118725 File Offset: 0x00116925
		private bool OwnerIsInFiringState()
		{
			return this.owner.stateMachine && this.owner.stateMachine.state.GetType() == this.resolvedOwnerHookStateType;
		}

		// Token: 0x040041D0 RID: 16848
		private ProjectileController projectileController;

		// Token: 0x040041D1 RID: 16849
		private ProjectileStickOnImpact projectileStickOnImpactController;

		// Token: 0x040041D2 RID: 16850
		private ProjectileSimple projectileSimple;

		// Token: 0x040041D3 RID: 16851
		public SerializableEntityStateType ownerHookStateType;

		// Token: 0x040041D4 RID: 16852
		public float acceleration;

		// Token: 0x040041D5 RID: 16853
		public float lookAcceleration = 4f;

		// Token: 0x040041D6 RID: 16854
		public float lookAccelerationRampUpDuration = 0.25f;

		// Token: 0x040041D7 RID: 16855
		public float initialLookImpulse = 5f;

		// Token: 0x040041D8 RID: 16856
		public float initiallMoveImpulse = 5f;

		// Token: 0x040041D9 RID: 16857
		public float moveAcceleration = 4f;

		// Token: 0x040041DA RID: 16858
		public string enterSoundString;

		// Token: 0x040041DB RID: 16859
		public string exitSoundString;

		// Token: 0x040041DC RID: 16860
		public string hookDistanceRTPCstring;

		// Token: 0x040041DD RID: 16861
		public float minHookDistancePitchModifier;

		// Token: 0x040041DE RID: 16862
		public float maxHookDistancePitchModifier;

		// Token: 0x040041DF RID: 16863
		public AnimationCurve lookAccelerationRampUpCurve;

		// Token: 0x040041E0 RID: 16864
		public Transform ropeEndTransform;

		// Token: 0x040041E1 RID: 16865
		public string muzzleStringOnBody = "MuzzleLeft";

		// Token: 0x040041E2 RID: 16866
		[Tooltip("The minimum distance the hook can be from the target before it detaches.")]
		public float nearBreakDistance;

		// Token: 0x040041E3 RID: 16867
		[Tooltip("The maximum distance this hook can travel.")]
		public float maxTravelDistance;

		// Token: 0x040041E4 RID: 16868
		public float escapeForceMultiplier = 2f;

		// Token: 0x040041E5 RID: 16869
		public float normalOffset = 1f;

		// Token: 0x040041E6 RID: 16870
		public float yankMassLimit;

		// Token: 0x040041E7 RID: 16871
		private Type resolvedOwnerHookStateType;

		// Token: 0x040041E8 RID: 16872
		private ProjectileGrappleController.OwnerInfo owner;

		// Token: 0x040041E9 RID: 16873
		private bool didStick;

		// Token: 0x040041EA RID: 16874
		private uint soundID;

		// Token: 0x02000B96 RID: 2966
		private struct OwnerInfo
		{
			// Token: 0x0600438A RID: 17290 RVA: 0x001187C8 File Offset: 0x001169C8
			public OwnerInfo(GameObject ownerGameObject)
			{
				this = default(ProjectileGrappleController.OwnerInfo);
				this.gameObject = ownerGameObject;
				if (this.gameObject)
				{
					this.characterBody = this.gameObject.GetComponent<CharacterBody>();
					this.characterMotor = this.gameObject.GetComponent<CharacterMotor>();
					this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
					this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.gameObject);
					EntityStateMachine[] components = this.gameObject.GetComponents<EntityStateMachine>();
					for (int i = 0; i < components.Length; i++)
					{
						if (components[i].customName == "Hook")
						{
							this.stateMachine = components[i];
							return;
						}
					}
				}
			}

			// Token: 0x040041EB RID: 16875
			public readonly GameObject gameObject;

			// Token: 0x040041EC RID: 16876
			public readonly CharacterBody characterBody;

			// Token: 0x040041ED RID: 16877
			public readonly CharacterMotor characterMotor;

			// Token: 0x040041EE RID: 16878
			public readonly Rigidbody rigidbody;

			// Token: 0x040041EF RID: 16879
			public readonly EntityStateMachine stateMachine;

			// Token: 0x040041F0 RID: 16880
			public readonly bool hasEffectiveAuthority;
		}

		// Token: 0x02000B97 RID: 2967
		private class BaseState : EntityStates.BaseState
		{
			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x0600438B RID: 17291 RVA: 0x0011886C File Offset: 0x00116A6C
			// (set) Token: 0x0600438C RID: 17292 RVA: 0x00118874 File Offset: 0x00116A74
			private protected bool ownerValid { protected get; private set; }

			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x0600438D RID: 17293 RVA: 0x0011887D File Offset: 0x00116A7D
			protected ref ProjectileGrappleController.OwnerInfo owner
			{
				get
				{
					return ref this.grappleController.owner;
				}
			}

			// Token: 0x0600438E RID: 17294 RVA: 0x0011888C File Offset: 0x00116A8C
			private void UpdatePositions()
			{
				this.aimOrigin = this.grappleController.owner.characterBody.aimOrigin;
				this.position = base.transform.position + base.transform.up * this.grappleController.normalOffset;
			}

			// Token: 0x0600438F RID: 17295 RVA: 0x001188E8 File Offset: 0x00116AE8
			public override void OnEnter()
			{
				base.OnEnter();
				this.grappleController = base.GetComponent<ProjectileGrappleController>();
				this.ownerValid = (this.grappleController && this.grappleController.owner.gameObject);
				if (this.ownerValid)
				{
					this.UpdatePositions();
				}
			}

			// Token: 0x06004390 RID: 17296 RVA: 0x00118940 File Offset: 0x00116B40
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (this.ownerValid)
				{
					this.ownerValid &= this.grappleController.owner.gameObject;
					if (this.ownerValid)
					{
						this.UpdatePositions();
						this.FixedUpdateBehavior();
					}
				}
				if (NetworkServer.active && !this.ownerValid)
				{
					this.ownerValid = false;
					EntityState.Destroy(base.gameObject);
					return;
				}
			}

			// Token: 0x06004391 RID: 17297 RVA: 0x001189B3 File Offset: 0x00116BB3
			protected virtual void FixedUpdateBehavior()
			{
				if (base.isAuthority && !this.grappleController.OwnerIsInFiringState())
				{
					this.outer.SetNextState(new ProjectileGrappleController.ReturnState());
					return;
				}
			}

			// Token: 0x06004392 RID: 17298 RVA: 0x001189DC File Offset: 0x00116BDC
			protected Ray GetOwnerAimRay()
			{
				if (!this.owner.characterBody)
				{
					return default(Ray);
				}
				return this.owner.characterBody.inputBank.GetAimRay();
			}

			// Token: 0x040041F1 RID: 16881
			protected ProjectileGrappleController grappleController;

			// Token: 0x040041F3 RID: 16883
			protected Vector3 aimOrigin;

			// Token: 0x040041F4 RID: 16884
			protected Vector3 position;
		}

		// Token: 0x02000B98 RID: 2968
		private class FlyState : ProjectileGrappleController.BaseState
		{
			// Token: 0x06004394 RID: 17300 RVA: 0x00118A1A File Offset: 0x00116C1A
			public override void OnEnter()
			{
				base.OnEnter();
				this.duration = this.grappleController.maxTravelDistance / this.grappleController.GetComponent<ProjectileSimple>().velocity;
			}

			// Token: 0x06004395 RID: 17301 RVA: 0x00118A44 File Offset: 0x00116C44
			protected override void FixedUpdateBehavior()
			{
				base.FixedUpdateBehavior();
				if (base.isAuthority)
				{
					if (this.grappleController.projectileStickOnImpactController.stuck)
					{
						EntityState entityState = null;
						if (this.grappleController.projectileStickOnImpactController.stuckBody)
						{
							Rigidbody component = this.grappleController.projectileStickOnImpactController.stuckBody.GetComponent<Rigidbody>();
							if (component && component.mass < this.grappleController.yankMassLimit)
							{
								CharacterBody component2 = component.GetComponent<CharacterBody>();
								if (!component2 || !component2.isPlayerControlled || component2.teamComponent.teamIndex != base.projectileController.teamFilter.teamIndex || FriendlyFireManager.ShouldDirectHitProceed(component2.healthComponent, base.projectileController.teamFilter.teamIndex))
								{
									entityState = new ProjectileGrappleController.YankState();
								}
							}
						}
						if (entityState == null)
						{
							entityState = new ProjectileGrappleController.GripState();
						}
						this.DeductOwnerStock();
						this.outer.SetNextState(entityState);
						return;
					}
					if (this.duration <= base.fixedAge)
					{
						this.outer.SetNextState(new ProjectileGrappleController.ReturnState());
						return;
					}
				}
			}

			// Token: 0x06004396 RID: 17302 RVA: 0x00118B5C File Offset: 0x00116D5C
			private void DeductOwnerStock()
			{
				if (base.ownerValid && base.owner.hasEffectiveAuthority)
				{
					SkillLocator component = base.owner.gameObject.GetComponent<SkillLocator>();
					if (component)
					{
						GenericSkill secondary = component.secondary;
						if (secondary)
						{
							secondary.DeductStock(1);
						}
					}
				}
			}

			// Token: 0x040041F5 RID: 16885
			private float duration;
		}

		// Token: 0x02000B99 RID: 2969
		private class BaseGripState : ProjectileGrappleController.BaseState
		{
			// Token: 0x06004398 RID: 17304 RVA: 0x00118BB5 File Offset: 0x00116DB5
			public override void OnEnter()
			{
				base.OnEnter();
				this.currentDistance = Vector3.Distance(this.aimOrigin, this.position);
			}

			// Token: 0x06004399 RID: 17305 RVA: 0x00118BD4 File Offset: 0x00116DD4
			protected override void FixedUpdateBehavior()
			{
				base.FixedUpdateBehavior();
				this.currentDistance = Vector3.Distance(this.aimOrigin, this.position);
				if (base.isAuthority)
				{
					bool flag = !this.grappleController.projectileStickOnImpactController.stuck;
					bool flag2 = this.currentDistance < this.grappleController.nearBreakDistance;
					bool flag3 = !this.grappleController.OwnerIsInFiringState();
					bool flag4;
					if (base.owner.stateMachine)
					{
						BaseSkillState baseSkillState = base.owner.stateMachine.state as BaseSkillState;
						flag4 = (baseSkillState == null || !baseSkillState.IsKeyDownAuthority());
					}
					else
					{
						flag4 = true;
					}
					if (flag4 || flag3 || flag2 || flag)
					{
						this.outer.SetNextState(new ProjectileGrappleController.ReturnState());
						return;
					}
				}
			}

			// Token: 0x040041F6 RID: 16886
			protected float currentDistance;
		}

		// Token: 0x02000B9A RID: 2970
		private class GripState : ProjectileGrappleController.BaseGripState
		{
			// Token: 0x0600439B RID: 17307 RVA: 0x00118C94 File Offset: 0x00116E94
			private void DeductStockIfStruckNonPylon()
			{
				GameObject victim = this.grappleController.projectileStickOnImpactController.victim;
				if (victim)
				{
					GameObject gameObject = victim;
					EntityLocator component = gameObject.GetComponent<EntityLocator>();
					if (component)
					{
						gameObject = component.entity;
					}
					gameObject.GetComponent<ProjectileController>();
				}
			}

			// Token: 0x0600439C RID: 17308 RVA: 0x00118CE0 File Offset: 0x00116EE0
			public override void OnEnter()
			{
				base.OnEnter();
				this.lastDistance = Vector3.Distance(this.aimOrigin, this.position);
				if (base.ownerValid)
				{
					this.grappleController.didStick = true;
					if (base.owner.characterMotor)
					{
						Vector3 direction = base.GetOwnerAimRay().direction;
						Vector3 vector = base.owner.characterMotor.velocity;
						vector = ((Vector3.Dot(vector, direction) < 0f) ? Vector3.zero : Vector3.Project(vector, direction));
						vector += direction * this.grappleController.initialLookImpulse;
						vector += base.owner.characterMotor.moveDirection * this.grappleController.initiallMoveImpulse;
						base.owner.characterMotor.velocity = vector;
					}
				}
			}

			// Token: 0x0600439D RID: 17309 RVA: 0x00118DC4 File Offset: 0x00116FC4
			protected override void FixedUpdateBehavior()
			{
				base.FixedUpdateBehavior();
				float num = this.grappleController.acceleration;
				if (this.currentDistance > this.lastDistance)
				{
					num *= this.grappleController.escapeForceMultiplier;
				}
				this.lastDistance = this.currentDistance;
				if (base.owner.hasEffectiveAuthority && base.owner.characterMotor && base.owner.characterBody)
				{
					Ray ownerAimRay = base.GetOwnerAimRay();
					Vector3 normalized = (base.transform.position - base.owner.characterBody.aimOrigin).normalized;
					Vector3 a = normalized * num;
					float time = Mathf.Clamp01(base.fixedAge / this.grappleController.lookAccelerationRampUpDuration);
					float num2 = this.grappleController.lookAccelerationRampUpCurve.Evaluate(time);
					float num3 = Util.Remap(Vector3.Dot(ownerAimRay.direction, normalized), -1f, 1f, 1f, 0f);
					a += ownerAimRay.direction * (this.grappleController.lookAcceleration * num2 * num3);
					a += base.owner.characterMotor.moveDirection * this.grappleController.moveAcceleration;
					base.owner.characterMotor.ApplyForce(a * (base.owner.characterMotor.mass * Time.fixedDeltaTime), true, true);
				}
			}

			// Token: 0x040041F7 RID: 16887
			private float lastDistance;
		}

		// Token: 0x02000B9B RID: 2971
		private class YankState : ProjectileGrappleController.BaseGripState
		{
			// Token: 0x0600439F RID: 17311 RVA: 0x00118F54 File Offset: 0x00117154
			public override void OnEnter()
			{
				base.OnEnter();
				this.stuckBody = this.grappleController.projectileStickOnImpactController.stuckBody;
			}

			// Token: 0x060043A0 RID: 17312 RVA: 0x00118F74 File Offset: 0x00117174
			protected override void FixedUpdateBehavior()
			{
				base.FixedUpdateBehavior();
				if (this.stuckBody)
				{
					if (Util.HasEffectiveAuthority(this.stuckBody.gameObject))
					{
						Vector3 a = this.aimOrigin - this.position;
						IDisplacementReceiver component = this.stuckBody.GetComponent<IDisplacementReceiver>();
						if ((Component)component && base.fixedAge >= ProjectileGrappleController.YankState.delayBeforeYanking)
						{
							component.AddDisplacement(a * (ProjectileGrappleController.YankState.yankSpeed * Time.fixedDeltaTime));
						}
					}
					if (base.owner.hasEffectiveAuthority && base.owner.characterMotor && base.fixedAge < ProjectileGrappleController.YankState.hoverTimeLimit)
					{
						Vector3 velocity = base.owner.characterMotor.velocity;
						if (velocity.y < 0f)
						{
							velocity.y = 0f;
							base.owner.characterMotor.velocity = velocity;
						}
					}
				}
			}

			// Token: 0x040041F8 RID: 16888
			public static float yankSpeed;

			// Token: 0x040041F9 RID: 16889
			public static float delayBeforeYanking;

			// Token: 0x040041FA RID: 16890
			public static float hoverTimeLimit = 0.5f;

			// Token: 0x040041FB RID: 16891
			private CharacterBody stuckBody;
		}

		// Token: 0x02000B9C RID: 2972
		private class ReturnState : ProjectileGrappleController.BaseState
		{
			// Token: 0x060043A3 RID: 17315 RVA: 0x0011906C File Offset: 0x0011726C
			public override void OnEnter()
			{
				base.OnEnter();
				if (base.ownerValid)
				{
					this.returnSpeed = this.grappleController.projectileSimple.velocity;
					this.returnSpeedAcceleration = this.returnSpeed * 2f;
				}
				if (NetworkServer.active && this.grappleController)
				{
					this.grappleController.projectileStickOnImpactController.Detach();
					this.grappleController.projectileStickOnImpactController.ignoreCharacters = true;
					this.grappleController.projectileStickOnImpactController.ignoreWorld = true;
				}
				Collider component = base.GetComponent<Collider>();
				if (component)
				{
					component.enabled = false;
				}
			}

			// Token: 0x060043A4 RID: 17316 RVA: 0x0011910C File Offset: 0x0011730C
			protected override void FixedUpdateBehavior()
			{
				base.FixedUpdateBehavior();
				if (base.rigidbody)
				{
					this.returnSpeed += this.returnSpeedAcceleration * Time.fixedDeltaTime;
					base.rigidbody.velocity = (this.aimOrigin - this.position).normalized * this.returnSpeed;
					if (NetworkServer.active)
					{
						Vector3 endPosition = this.position + base.rigidbody.velocity * Time.fixedDeltaTime;
						if (HGMath.Overshoots(this.position, endPosition, this.aimOrigin))
						{
							EntityState.Destroy(base.gameObject);
							return;
						}
					}
				}
			}

			// Token: 0x040041FC RID: 16892
			private float returnSpeedAcceleration = 240f;

			// Token: 0x040041FD RID: 16893
			private float returnSpeed;
		}
	}
}
