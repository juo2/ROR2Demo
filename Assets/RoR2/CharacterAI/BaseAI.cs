using System;
using System.Linq;
using EntityStates;
using EntityStates.AI;
using JetBrains.Annotations;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.CharacterAI
{
	// Token: 0x02000C77 RID: 3191
	[RequireComponent(typeof(CharacterMaster))]
	public class BaseAI : MonoBehaviour
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0012C199 File Offset: 0x0012A399
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x0012C1A1 File Offset: 0x0012A3A1
		public CharacterMaster master { get; protected set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0012C1AA File Offset: 0x0012A3AA
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x0012C1B2 File Offset: 0x0012A3B2
		public CharacterBody body { get; protected set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x0012C1BB File Offset: 0x0012A3BB
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x0012C1C3 File Offset: 0x0012A3C3
		public Transform bodyTransform { get; protected set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x0012C1CC File Offset: 0x0012A3CC
		// (set) Token: 0x060048EA RID: 18666 RVA: 0x0012C1D4 File Offset: 0x0012A3D4
		public CharacterDirection bodyCharacterDirection { get; protected set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x0012C1DD File Offset: 0x0012A3DD
		// (set) Token: 0x060048EC RID: 18668 RVA: 0x0012C1E5 File Offset: 0x0012A3E5
		public CharacterMotor bodyCharacterMotor { get; protected set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x0012C1EE File Offset: 0x0012A3EE
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x0012C1F6 File Offset: 0x0012A3F6
		public InputBankTest bodyInputBank { get; protected set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x0012C1FF File Offset: 0x0012A3FF
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x0012C207 File Offset: 0x0012A407
		public HealthComponent bodyHealthComponent { get; protected set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x0012C210 File Offset: 0x0012A410
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x0012C218 File Offset: 0x0012A418
		public SkillLocator bodySkillLocator { get; protected set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x0012C221 File Offset: 0x0012A421
		// (set) Token: 0x060048F4 RID: 18676 RVA: 0x0012C229 File Offset: 0x0012A429
		public NetworkIdentity networkIdentity { get; protected set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060048F5 RID: 18677 RVA: 0x0012C232 File Offset: 0x0012A432
		// (set) Token: 0x060048F6 RID: 18678 RVA: 0x0012C23A File Offset: 0x0012A43A
		public AISkillDriver[] skillDrivers { get; protected set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x0012C243 File Offset: 0x0012A443
		// (set) Token: 0x060048F8 RID: 18680 RVA: 0x0012C24B File Offset: 0x0012A44B
		public bool hasAimConfirmation { get; private set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0012C254 File Offset: 0x0012A454
		public bool hasAimTarget
		{
			get
			{
				return this.skillDriverEvaluation.aimTarget != null && this.skillDriverEvaluation.aimTarget.gameObject;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0012C27A File Offset: 0x0012A47A
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x0012C282 File Offset: 0x0012A482
		private BroadNavigationSystem broadNavigationSystem
		{
			get
			{
				return this._broadNavigationSystem;
			}
			set
			{
				if (this._broadNavigationSystem == value)
				{
					return;
				}
				BroadNavigationSystem broadNavigationSystem = this._broadNavigationSystem;
				if (broadNavigationSystem != null)
				{
					broadNavigationSystem.ReturnAgent(ref this._broadNavigationAgent);
				}
				this._broadNavigationSystem = value;
				BroadNavigationSystem broadNavigationSystem2 = this._broadNavigationSystem;
				if (broadNavigationSystem2 == null)
				{
					return;
				}
				broadNavigationSystem2.RequestAgent(ref this._broadNavigationAgent);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0012C2C2 File Offset: 0x0012A4C2
		public BroadNavigationSystem.Agent broadNavigationAgent
		{
			get
			{
				return this._broadNavigationAgent;
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x0012C2CC File Offset: 0x0012A4CC
		[ContextMenu("Toggle broad navigation debug drawing")]
		private void ToggleBroadNavigationDebugDraw()
		{
			if (this.broadNavigationSystem is NodeGraphNavigationSystem)
			{
				NodeGraphNavigationSystem.Agent agent = (NodeGraphNavigationSystem.Agent)this.broadNavigationAgent;
				agent.drawPath = !agent.drawPath;
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0012C304 File Offset: 0x0012A504
		public void SetBroadNavigationDebugDrawEnabled(bool newBroadNavigationDebugDrawEnabled)
		{
			if (this.broadNavigationSystem is NodeGraphNavigationSystem)
			{
				((NodeGraphNavigationSystem.Agent)this.broadNavigationAgent).drawPath = newBroadNavigationDebugDrawEnabled;
			}
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x0012C334 File Offset: 0x0012A534
		protected void Awake()
		{
			this.targetRefreshTimer = 0.5f;
			this.master = base.GetComponent<CharacterMaster>();
			this.stateMachine = base.GetComponent<EntityStateMachine>();
			this.stateMachine.enabled = false;
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.skillDrivers = base.GetComponents<AISkillDriver>();
			this.currentEnemy = new BaseAI.Target(this);
			this.leader = new BaseAI.Target(this);
			this.buddy = new BaseAI.Target(this);
			this.customTarget = new BaseAI.Target(this);
			this.broadNavigationSystem = BaseAI.nodeGraphNavigationSystem;
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x0012C3C3 File Offset: 0x0012A5C3
		protected void OnDestroy()
		{
			this.broadNavigationSystem = null;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x0012C3CC File Offset: 0x0012A5CC
		protected void Start()
		{
			if (!Util.HasEffectiveAuthority(this.networkIdentity))
			{
				base.enabled = false;
				if (this.stateMachine)
				{
					this.stateMachine.enabled = false;
				}
			}
			if (NetworkServer.active)
			{
				this.skillDriverUpdateTimer = UnityEngine.Random.value;
			}
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x0012C418 File Offset: 0x0012A618
		protected void FixedUpdate()
		{
			this.enemyAttention -= Time.fixedDeltaTime;
			this.targetRefreshTimer -= Time.fixedDeltaTime;
			this.skillDriverUpdateTimer -= Time.fixedDeltaTime;
			if (this.body)
			{
				this.broadNavigationAgent.ConfigureFromBody(this.body);
				if (this.skillDriverUpdateTimer <= 0f)
				{
					if (this.skillDriverEvaluation.dominantSkillDriver && this.skillDriverEvaluation.dominantSkillDriver.resetCurrentEnemyOnNextDriverSelection)
					{
						this.currentEnemy.Reset();
						this.targetRefreshTimer = 0f;
					}
					if (!this.currentEnemy.gameObject && this.targetRefreshTimer <= 0f)
					{
						this.targetRefreshTimer = 0.5f;
						HurtBox hurtBox = this.FindEnemyHurtBox(float.PositiveInfinity, this.fullVision, true);
						if (hurtBox && hurtBox.healthComponent)
						{
							this.currentEnemy.gameObject = hurtBox.healthComponent.gameObject;
							this.currentEnemy.bestHurtBox = hurtBox;
						}
						if (this.currentEnemy.gameObject)
						{
							this.enemyAttention = this.enemyAttentionDuration;
						}
					}
					this.BeginSkillDriver(this.EvaluateSkillDrivers());
				}
			}
			this._broadNavigationAgent.currentPosition = this.GetNavigationStartPos();
			this.UpdateBodyInputs();
			this.UpdateBodyAim(Time.fixedDeltaTime);
			this.debugEnemyHurtBox = this.currentEnemy.bestHurtBox;
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0012C5A0 File Offset: 0x0012A7A0
		private void BeginSkillDriver(BaseAI.SkillDriverEvaluation newSkillDriverEvaluation)
		{
			this.skillDriverEvaluation = newSkillDriverEvaluation;
			this.skillDriverUpdateTimer = UnityEngine.Random.Range(0.16666667f, 0.2f);
			if (this.skillDriverEvaluation.dominantSkillDriver)
			{
				this.selectedSkilldriverName = this.skillDriverEvaluation.dominantSkillDriver.customName;
				if (this.skillDriverEvaluation.dominantSkillDriver.driverUpdateTimerOverride >= 0f)
				{
					this.skillDriverUpdateTimer = this.skillDriverEvaluation.dominantSkillDriver.driverUpdateTimerOverride;
				}
				this.skillDriverEvaluation.dominantSkillDriver.OnSelected();
				return;
			}
			this.selectedSkilldriverName = "";
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x0012C63C File Offset: 0x0012A83C
		public virtual void OnBodyStart(CharacterBody newBody)
		{
			this.body = newBody;
			this.bodyTransform = newBody.transform;
			this.bodyCharacterDirection = newBody.GetComponent<CharacterDirection>();
			this.bodyCharacterMotor = newBody.GetComponent<CharacterMotor>();
			this.bodyInputBank = newBody.GetComponent<InputBankTest>();
			this.bodyHealthComponent = newBody.GetComponent<HealthComponent>();
			this.bodySkillLocator = newBody.GetComponent<SkillLocator>();
			this.localNavigator.SetBody(newBody);
			this._broadNavigationAgent.enabled = true;
			if (this.stateMachine && Util.HasEffectiveAuthority(this.networkIdentity))
			{
				this.stateMachine.enabled = true;
				this.stateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.scanState));
			}
			base.enabled = true;
			if (this.bodyCharacterDirection)
			{
				this.bodyInputs.desiredAimDirection = this.bodyCharacterDirection.forward;
			}
			else
			{
				this.bodyInputs.desiredAimDirection = this.bodyTransform.forward;
			}
			if (this.bodyInputBank)
			{
				this.bodyInputBank.aimDirection = this.bodyInputs.desiredAimDirection;
			}
			Action<CharacterBody> action = this.onBodyDiscovered;
			if (action == null)
			{
				return;
			}
			action(newBody);
		}

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06004905 RID: 18693 RVA: 0x0012C764 File Offset: 0x0012A964
		// (remove) Token: 0x06004906 RID: 18694 RVA: 0x0012C79C File Offset: 0x0012A99C
		public event Action<CharacterBody> onBodyDiscovered;

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06004907 RID: 18695 RVA: 0x0012C7D4 File Offset: 0x0012A9D4
		// (remove) Token: 0x06004908 RID: 18696 RVA: 0x0012C80C File Offset: 0x0012AA0C
		public event Action<CharacterBody> onBodyLost;

		// Token: 0x06004909 RID: 18697 RVA: 0x0012C841 File Offset: 0x0012AA41
		public virtual void OnBodyDeath(CharacterBody characterBody)
		{
			this.OnBodyLost(characterBody);
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x0012C841 File Offset: 0x0012AA41
		public virtual void OnBodyDestroyed(CharacterBody characterBody)
		{
			this.OnBodyLost(characterBody);
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x0012C84C File Offset: 0x0012AA4C
		public virtual void OnBodyLost(CharacterBody characterBody)
		{
			if (this.body != null)
			{
				base.enabled = false;
				this.body = null;
				this.bodyTransform = null;
				this.bodyCharacterDirection = null;
				this.bodyCharacterMotor = null;
				this.bodyInputBank = null;
				this.bodyHealthComponent = null;
				this.bodySkillLocator = null;
				this.localNavigator.SetBody(null);
				this._broadNavigationAgent.enabled = false;
				if (this.stateMachine)
				{
					this.stateMachine.enabled = false;
					this.stateMachine.SetNextState(new Idle());
				}
				Action<CharacterBody> action = this.onBodyLost;
				if (action == null)
				{
					return;
				}
				action(characterBody);
			}
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0012C8F0 File Offset: 0x0012AAF0
		public virtual void OnBodyDamaged(DamageReport damageReport)
		{
			DamageInfo damageInfo = damageReport.damageInfo;
			if (!damageInfo.attacker)
			{
				return;
			}
			if (!this.body)
			{
				return;
			}
			if ((!this.currentEnemy.gameObject || this.enemyAttention <= 0f) && damageInfo.attacker != this.body.gameObject && (!this.neverRetaliateFriendlies || !damageReport.isFriendlyFire))
			{
				this.currentEnemy.gameObject = damageInfo.attacker;
				this.enemyAttention = this.enemyAttentionDuration;
			}
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x0012C984 File Offset: 0x0012AB84
		[Obsolete]
		public virtual bool HasLOS(Vector3 start, Vector3 end)
		{
			if (!this.body)
			{
				return false;
			}
			Vector3 direction = end - start;
			float magnitude = direction.magnitude;
			RaycastHit raycastHit;
			return this.body.visionDistance >= magnitude && !Physics.Raycast(new Ray
			{
				origin = start,
				direction = direction
			}, out raycastHit, magnitude, LayerIndex.world.mask);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x0012C9F8 File Offset: 0x0012ABF8
		public virtual bool HasLOS(Vector3 end)
		{
			if (!this.body || !this.bodyInputBank)
			{
				return false;
			}
			Vector3 aimOrigin = this.bodyInputBank.aimOrigin;
			Vector3 direction = end - aimOrigin;
			float magnitude = direction.magnitude;
			RaycastHit raycastHit;
			return this.body.visionDistance >= magnitude && !Physics.Raycast(new Ray
			{
				origin = aimOrigin,
				direction = direction
			}, out raycastHit, magnitude, LayerIndex.world.mask);
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x0012CA84 File Offset: 0x0012AC84
		private Vector3? GetNavigationStartPos()
		{
			if (!this.body)
			{
				return null;
			}
			return new Vector3?(this.body.footPosition);
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x0012CAB8 File Offset: 0x0012ACB8
		public NodeGraph GetDesiredSpawnNodeGraph()
		{
			return SceneInfo.instance.GetNodeGraph(this.desiredSpawnNodeGraphType);
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x0012CACC File Offset: 0x0012ACCC
		public void SetGoalPosition(Vector3? goalPos)
		{
			this.broadNavigationAgent.goalPosition = goalPos;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x0012CAE8 File Offset: 0x0012ACE8
		public void SetGoalPosition(BaseAI.Target goalTarget)
		{
			Vector3? goalPosition = null;
			Vector3 vector;
			if (goalTarget.GetBullseyePosition(out vector))
			{
				Vector3 value = vector;
				goalPosition = new Vector3?(value);
			}
			this.SetGoalPosition(goalPosition);
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0012CB18 File Offset: 0x0012AD18
		public void DebugDrawPath(Color color, float duration)
		{
			if (this.broadNavigationSystem is NodeGraphNavigationSystem)
			{
				((NodeGraphNavigationSystem.Agent)this.broadNavigationAgent).DebugDrawPath(color, duration);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0012CB47 File Offset: 0x0012AD47
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0012CB4F File Offset: 0x0012AD4F
		public BaseAI.Target currentEnemy { get; private set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x0012CB58 File Offset: 0x0012AD58
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x0012CB60 File Offset: 0x0012AD60
		public BaseAI.Target leader { get; private set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x0012CB69 File Offset: 0x0012AD69
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x0012CB71 File Offset: 0x0012AD71
		private BaseAI.Target buddy { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0012CB7A File Offset: 0x0012AD7A
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x0012CB82 File Offset: 0x0012AD82
		public BaseAI.Target customTarget { get; private set; }

		// Token: 0x0600491C RID: 18716 RVA: 0x0012CB8C File Offset: 0x0012AD8C
		private static bool CheckLoS(Vector3 start, Vector3 end)
		{
			Vector3 direction = end - start;
			RaycastHit raycastHit;
			return !Physics.Raycast(start, direction, out raycastHit, direction.magnitude, LayerIndex.world.mask, QueryTriggerInteraction.Ignore);
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x0012CBC8 File Offset: 0x0012ADC8
		public HurtBox GetBestHurtBox(GameObject target)
		{
			CharacterBody component = target.GetComponent<CharacterBody>();
			HurtBoxGroup hurtBoxGroup = (component != null) ? component.hurtBoxGroup : null;
			if (hurtBoxGroup && hurtBoxGroup.bullseyeCount > 1 && this.bodyInputBank)
			{
				Vector3 aimOrigin = this.bodyInputBank.aimOrigin;
				HurtBox hurtBox = null;
				float num = float.PositiveInfinity;
				foreach (HurtBox hurtBox2 in hurtBoxGroup.hurtBoxes)
				{
					if (hurtBox2.isBullseye)
					{
						Vector3 position = hurtBox2.transform.position;
						if (BaseAI.CheckLoS(aimOrigin, hurtBox2.transform.position))
						{
							float sqrMagnitude = (position - aimOrigin).sqrMagnitude;
							if (sqrMagnitude < num)
							{
								num = sqrMagnitude;
								hurtBox = hurtBox2;
							}
						}
					}
				}
				if (hurtBox)
				{
					return hurtBox;
				}
			}
			return Util.FindBodyMainHurtBox(target);
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x0012CCA0 File Offset: 0x0012AEA0
		public void ForceAcquireNearestEnemyIfNoCurrentEnemy()
		{
			if (this.currentEnemy.gameObject)
			{
				return;
			}
			if (!this.body)
			{
				Debug.LogErrorFormat("BaseAI.ForceAcquireNearestEnemyIfNoCurrentEnemy for CharacterMaster '{0}' failed: AI has no body to search from.", new object[]
				{
					base.gameObject.name
				});
				return;
			}
			HurtBox hurtBox = this.FindEnemyHurtBox(float.PositiveInfinity, true, false);
			if (hurtBox && hurtBox.healthComponent)
			{
				this.currentEnemy.gameObject = hurtBox.healthComponent.gameObject;
				this.currentEnemy.bestHurtBox = hurtBox;
			}
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x0012CD34 File Offset: 0x0012AF34
		private HurtBox FindEnemyHurtBox(float maxDistance, bool full360Vision, bool filterByLoS)
		{
			if (!this.body)
			{
				return null;
			}
			this.enemySearch.viewer = this.body;
			this.enemySearch.teamMaskFilter = TeamMask.allButNeutral;
			this.enemySearch.teamMaskFilter.RemoveTeam(this.master.teamIndex);
			this.enemySearch.sortMode = BullseyeSearch.SortMode.Distance;
			this.enemySearch.minDistanceFilter = 0f;
			this.enemySearch.maxDistanceFilter = maxDistance;
			this.enemySearch.searchOrigin = this.bodyInputBank.aimOrigin;
			this.enemySearch.searchDirection = this.bodyInputBank.aimDirection;
			this.enemySearch.maxAngleFilter = (full360Vision ? 180f : 90f);
			this.enemySearch.filterByLoS = filterByLoS;
			this.enemySearch.RefreshCandidates();
			return this.enemySearch.GetResults().FirstOrDefault<HurtBox>();
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x0012CE24 File Offset: 0x0012B024
		public bool GameObjectPassesSkillDriverFilters(BaseAI.Target target, AISkillDriver skillDriver, out float separationSqrMagnitude)
		{
			separationSqrMagnitude = 0f;
			if (!target.gameObject)
			{
				return false;
			}
			float num = 1f;
			if (target.healthComponent)
			{
				num = target.healthComponent.combinedHealthFraction;
			}
			if (num < skillDriver.minTargetHealthFraction || num > skillDriver.maxTargetHealthFraction)
			{
				return false;
			}
			float num2 = 0f;
			if (this.body)
			{
				num2 = this.body.radius;
			}
			float num3 = 0f;
			if (target.characterBody)
			{
				num3 = target.characterBody.radius;
			}
			Vector3 b = this.bodyInputBank ? this.bodyInputBank.aimOrigin : this.bodyTransform.position;
			Vector3 a;
			target.GetBullseyePosition(out a);
			float sqrMagnitude = (a - b).sqrMagnitude;
			separationSqrMagnitude = sqrMagnitude - num3 * num3 - num2 * num2;
			return separationSqrMagnitude >= skillDriver.minDistanceSqr && separationSqrMagnitude <= skillDriver.maxDistanceSqr && (!skillDriver.selectionRequiresTargetLoS || target.hasLoS);
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0012CF2F File Offset: 0x0012B12F
		private void UpdateTargets()
		{
			this.currentEnemy.Update();
			this.leader.Update();
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x0012CF48 File Offset: 0x0012B148
		protected BaseAI.SkillDriverEvaluation? EvaluateSingleSkillDriver(in BaseAI.SkillDriverEvaluation currentSkillDriverEvaluation, AISkillDriver aiSkillDriver, float myHealthFraction)
		{
			if (!this.body || !this.bodySkillLocator)
			{
				return null;
			}
			float positiveInfinity = float.PositiveInfinity;
			if (aiSkillDriver.noRepeat && currentSkillDriverEvaluation.dominantSkillDriver == aiSkillDriver)
			{
				return null;
			}
			if (aiSkillDriver.maxTimesSelected >= 0 && aiSkillDriver.timesSelected >= aiSkillDriver.maxTimesSelected)
			{
				return null;
			}
			BaseAI.Target target = null;
			if (aiSkillDriver.requireEquipmentReady && this.body.equipmentSlot.stock <= 0)
			{
				return null;
			}
			if (aiSkillDriver.skillSlot != SkillSlot.None)
			{
				GenericSkill skill = this.bodySkillLocator.GetSkill(aiSkillDriver.skillSlot);
				if (aiSkillDriver.requireSkillReady && (!skill || !skill.IsReady()))
				{
					return null;
				}
				if (aiSkillDriver.requiredSkill && (!skill || !(skill.skillDef == aiSkillDriver.requiredSkill)))
				{
					return null;
				}
			}
			if (aiSkillDriver.minUserHealthFraction > myHealthFraction || aiSkillDriver.maxUserHealthFraction < myHealthFraction)
			{
				return null;
			}
			if (this.bodyCharacterMotor && !this.bodyCharacterMotor.isGrounded && aiSkillDriver.selectionRequiresOnGround)
			{
				return null;
			}
			switch (aiSkillDriver.moveTargetType)
			{
			case AISkillDriver.TargetType.CurrentEnemy:
				if (this.GameObjectPassesSkillDriverFilters(this.currentEnemy, aiSkillDriver, out positiveInfinity))
				{
					target = this.currentEnemy;
				}
				break;
			case AISkillDriver.TargetType.NearestFriendlyInSkillRange:
				if (this.bodyInputBank)
				{
					this.buddySearch.teamMaskFilter = TeamMask.none;
					this.buddySearch.teamMaskFilter.AddTeam(this.master.teamIndex);
					this.buddySearch.sortMode = BullseyeSearch.SortMode.Distance;
					this.buddySearch.minDistanceFilter = aiSkillDriver.minDistanceSqr;
					this.buddySearch.maxDistanceFilter = aiSkillDriver.maxDistance;
					this.buddySearch.searchOrigin = this.bodyInputBank.aimOrigin;
					this.buddySearch.searchDirection = this.bodyInputBank.aimDirection;
					this.buddySearch.maxAngleFilter = 180f;
					this.buddySearch.filterByLoS = aiSkillDriver.activationRequiresTargetLoS;
					this.buddySearch.RefreshCandidates();
					if (this.body)
					{
						this.buddySearch.FilterOutGameObject(this.body.gameObject);
					}
					this.buddySearch.FilterCandidatesByHealthFraction(aiSkillDriver.minTargetHealthFraction, aiSkillDriver.maxTargetHealthFraction);
					HurtBox hurtBox = this.buddySearch.GetResults().FirstOrDefault<HurtBox>();
					if (hurtBox && hurtBox.healthComponent)
					{
						this.buddy.gameObject = hurtBox.healthComponent.gameObject;
						this.buddy.bestHurtBox = hurtBox;
					}
					if (this.GameObjectPassesSkillDriverFilters(this.buddy, aiSkillDriver, out positiveInfinity))
					{
						target = this.buddy;
					}
				}
				break;
			case AISkillDriver.TargetType.CurrentLeader:
				if (this.GameObjectPassesSkillDriverFilters(this.leader, aiSkillDriver, out positiveInfinity))
				{
					target = this.leader;
				}
				break;
			case AISkillDriver.TargetType.Custom:
				if (this.GameObjectPassesSkillDriverFilters(this.customTarget, aiSkillDriver, out positiveInfinity))
				{
					target = this.customTarget;
				}
				break;
			}
			if (target == null)
			{
				return null;
			}
			BaseAI.Target target2 = null;
			if (aiSkillDriver.aimType != AISkillDriver.AimType.None)
			{
				bool flag = aiSkillDriver.selectionRequiresAimTarget;
				switch (aiSkillDriver.aimType)
				{
				case AISkillDriver.AimType.AtMoveTarget:
					target2 = target;
					break;
				case AISkillDriver.AimType.AtCurrentEnemy:
					target2 = this.currentEnemy;
					break;
				case AISkillDriver.AimType.AtCurrentLeader:
					target2 = this.leader;
					break;
				default:
					flag = false;
					break;
				}
				if (flag && (target2 == null || !target2.gameObject))
				{
					return null;
				}
			}
			return new BaseAI.SkillDriverEvaluation?(new BaseAI.SkillDriverEvaluation
			{
				dominantSkillDriver = aiSkillDriver,
				target = target,
				aimTarget = target2,
				separationSqrMagnitude = positiveInfinity
			});
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0012D320 File Offset: 0x0012B520
		public BaseAI.SkillDriverEvaluation EvaluateSkillDrivers()
		{
			this.UpdateTargets();
			float myHealthFraction = 1f;
			if (this.bodyHealthComponent)
			{
				myHealthFraction = this.bodyHealthComponent.combinedHealthFraction;
			}
			if (this.bodySkillLocator)
			{
				if (this.skillDriverEvaluation.dominantSkillDriver && this.skillDriverEvaluation.dominantSkillDriver.nextHighPriorityOverride)
				{
					BaseAI.SkillDriverEvaluation? skillDriverEvaluation = this.EvaluateSingleSkillDriver(this.skillDriverEvaluation, this.skillDriverEvaluation.dominantSkillDriver.nextHighPriorityOverride, myHealthFraction);
					if (skillDriverEvaluation != null)
					{
						return skillDriverEvaluation.Value;
					}
				}
				for (int i = 0; i < this.skillDrivers.Length; i++)
				{
					AISkillDriver aiskillDriver = this.skillDrivers[i];
					if (aiskillDriver.enabled)
					{
						BaseAI.SkillDriverEvaluation? skillDriverEvaluation2 = this.EvaluateSingleSkillDriver(this.skillDriverEvaluation, aiskillDriver, myHealthFraction);
						if (skillDriverEvaluation2 != null)
						{
							return skillDriverEvaluation2.Value;
						}
					}
				}
			}
			return default(BaseAI.SkillDriverEvaluation);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0012D40C File Offset: 0x0012B60C
		protected void UpdateBodyInputs()
		{
			BaseAIState baseAIState;
			if ((baseAIState = (this.stateMachine.state as BaseAIState)) != null)
			{
				this.bodyInputs = baseAIState.GenerateBodyInputs(this.bodyInputs);
			}
			if (this.bodyInputBank)
			{
				this.bodyInputBank.skill1.PushState(this.bodyInputs.pressSkill1);
				this.bodyInputBank.skill2.PushState(this.bodyInputs.pressSkill2);
				this.bodyInputBank.skill3.PushState(this.bodyInputs.pressSkill3);
				this.bodyInputBank.skill4.PushState(this.bodyInputs.pressSkill4);
				this.bodyInputBank.jump.PushState(this.bodyInputs.pressJump);
				this.bodyInputBank.sprint.PushState(this.bodyInputs.pressSprint);
				this.bodyInputBank.activateEquipment.PushState(this.bodyInputs.pressActivateEquipment);
				this.bodyInputBank.moveVector = this.bodyInputs.moveVector;
			}
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0012D524 File Offset: 0x0012B724
		protected void UpdateBodyAim(float deltaTime)
		{
			this.hasAimConfirmation = false;
			if (this.bodyInputBank)
			{
				Vector3 aimDirection = this.bodyInputBank.aimDirection;
				Vector3 desiredAimDirection = this.bodyInputs.desiredAimDirection;
				if (desiredAimDirection != Vector3.zero)
				{
					Quaternion target = Util.QuaternionSafeLookRotation(desiredAimDirection);
					Vector3 vector = Util.SmoothDampQuaternion(Util.QuaternionSafeLookRotation(aimDirection), target, ref this.aimVelocity, this.aimVectorDampTime, this.aimVectorMaxSpeed, deltaTime) * Vector3.forward;
					this.bodyInputBank.aimDirection = vector;
					this.hasAimConfirmation = (Vector3.Dot(vector, desiredAimDirection) >= 0.95f);
				}
			}
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x0012D5C0 File Offset: 0x0012B7C0
		[ConCommand(commandName = "ai_draw_path", flags = ConVarFlags.Cheat, helpText = "Enables or disables the drawing of the specified AI's broad navigation path. Format: ai_draw_path <CharacterMaster selector> <0/1>")]
		private static void CCAiDrawPath(ConCommandArgs args)
		{
			CharacterMaster argCharacterMasterInstance = args.GetArgCharacterMasterInstance(0);
			args.GetArgBool(1);
			if (!argCharacterMasterInstance)
			{
				throw new ConCommandException("Could not find target.");
			}
			BaseAI component = argCharacterMasterInstance.GetComponent<BaseAI>();
			if (!component)
			{
				throw new ConCommandException("Target has no AI.");
			}
			component.ToggleBroadNavigationDebugDraw();
		}

		// Token: 0x040045C9 RID: 17865
		[Tooltip("If true, this character can spot enemies behind itself.")]
		public bool fullVision;

		// Token: 0x040045CA RID: 17866
		[Tooltip("If true, this AI will not be allowed to retaliate against damage received from a source on its own team.")]
		public bool neverRetaliateFriendlies;

		// Token: 0x040045CB RID: 17867
		public float enemyAttentionDuration = 5f;

		// Token: 0x040045CC RID: 17868
		public MapNodeGroup.GraphType desiredSpawnNodeGraphType;

		// Token: 0x040045CD RID: 17869
		[Tooltip("The state machine to run while the body exists.")]
		public EntityStateMachine stateMachine;

		// Token: 0x040045CE RID: 17870
		public SerializableEntityStateType scanState;

		// Token: 0x040045CF RID: 17871
		public bool isHealer;

		// Token: 0x040045D0 RID: 17872
		public float enemyAttention;

		// Token: 0x040045D1 RID: 17873
		public float aimVectorDampTime = 0.2f;

		// Token: 0x040045D2 RID: 17874
		public float aimVectorMaxSpeed = 6f;

		// Token: 0x040045D3 RID: 17875
		private float aimVelocity;

		// Token: 0x040045D4 RID: 17876
		private float targetRefreshTimer;

		// Token: 0x040045D5 RID: 17877
		private const float targetRefreshInterval = 0.5f;

		// Token: 0x040045D6 RID: 17878
		public LocalNavigator localNavigator = new LocalNavigator();

		// Token: 0x040045D7 RID: 17879
		public string selectedSkilldriverName;

		// Token: 0x040045D8 RID: 17880
		private const float maxVisionDistance = float.PositiveInfinity;

		// Token: 0x040045DA RID: 17882
		public static readonly NodeGraphNavigationSystem nodeGraphNavigationSystem = new NodeGraphNavigationSystem();

		// Token: 0x040045DB RID: 17883
		private BroadNavigationSystem _broadNavigationSystem;

		// Token: 0x040045DC RID: 17884
		private BroadNavigationSystem.Agent _broadNavigationAgent = BroadNavigationSystem.Agent.invalid;

		// Token: 0x040045DD RID: 17885
		public HurtBox debugEnemyHurtBox;

		// Token: 0x040045E4 RID: 17892
		private BullseyeSearch enemySearch = new BullseyeSearch();

		// Token: 0x040045E5 RID: 17893
		private BullseyeSearch buddySearch = new BullseyeSearch();

		// Token: 0x040045E6 RID: 17894
		private float skillDriverUpdateTimer;

		// Token: 0x040045E7 RID: 17895
		private const float skillDriverMinUpdateInterval = 0.16666667f;

		// Token: 0x040045E8 RID: 17896
		private const float skillDriverMaxUpdateInterval = 0.2f;

		// Token: 0x040045E9 RID: 17897
		public BaseAI.SkillDriverEvaluation skillDriverEvaluation;

		// Token: 0x040045EA RID: 17898
		protected BaseAI.BodyInputs bodyInputs;

		// Token: 0x02000C78 RID: 3192
		[Serializable]
		public class Target
		{
			// Token: 0x06004929 RID: 18729 RVA: 0x0012D67C File Offset: 0x0012B87C
			public Target([NotNull] BaseAI owner)
			{
				this.owner = owner;
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x0600492A RID: 18730 RVA: 0x0012D69D File Offset: 0x0012B89D
			// (set) Token: 0x0600492B RID: 18731 RVA: 0x0012D6A8 File Offset: 0x0012B8A8
			public GameObject gameObject
			{
				get
				{
					return this._gameObject;
				}
				set
				{
					if (value == this._gameObject)
					{
						return;
					}
					this._gameObject = value;
					GameObject gameObject = this.gameObject;
					this.characterBody = ((gameObject != null) ? gameObject.GetComponent<CharacterBody>() : null);
					CharacterBody characterBody = this.characterBody;
					this.healthComponent = ((characterBody != null) ? characterBody.healthComponent : null);
					CharacterBody characterBody2 = this.characterBody;
					this.hurtBoxGroup = ((characterBody2 != null) ? characterBody2.hurtBoxGroup : null);
					this.bullseyeCount = (this.hurtBoxGroup ? this.hurtBoxGroup.bullseyeCount : 0);
					this.mainHurtBox = (this.hurtBoxGroup ? this.hurtBoxGroup.mainHurtBox : null);
					this.bestHurtBox = this.mainHurtBox;
					this.hasLoS = false;
					this.unset = !this._gameObject;
				}
			}

			// Token: 0x0600492C RID: 18732 RVA: 0x0012D778 File Offset: 0x0012B978
			public void Update()
			{
				if (!this.gameObject)
				{
					return;
				}
				this.hasLoS = (this.bestHurtBox && this.owner.HasLOS(this.bestHurtBox.transform.position));
				if (this.bullseyeCount > 1 && !this.hasLoS)
				{
					this.bestHurtBox = this.GetBestHurtBox(out this.hasLoS);
				}
			}

			// Token: 0x0600492D RID: 18733 RVA: 0x0012D7E7 File Offset: 0x0012B9E7
			public bool TestLOSNow()
			{
				return this.bestHurtBox && this.owner.HasLOS(this.bestHurtBox.transform.position);
			}

			// Token: 0x0600492E RID: 18734 RVA: 0x0012D814 File Offset: 0x0012BA14
			public bool GetBullseyePosition(out Vector3 position)
			{
				if (this.characterBody && this.owner.body && (this.characterBody.GetVisibilityLevel(this.owner.body) >= VisibilityLevel.Revealed || this.lastKnownBullseyePositionTime.timeSince >= 2f))
				{
					if (this.bestHurtBox)
					{
						this.lastKnownBullseyePosition = new Vector3?(this.bestHurtBox.transform.position);
					}
					else
					{
						this.lastKnownBullseyePosition = null;
					}
					this.lastKnownBullseyePositionTime = Run.FixedTimeStamp.now;
				}
				if (this.lastKnownBullseyePosition != null)
				{
					position = this.lastKnownBullseyePosition.Value;
					return true;
				}
				position = (this.gameObject ? this.gameObject.transform.position : Vector3.zero);
				return false;
			}

			// Token: 0x0600492F RID: 18735 RVA: 0x0012D900 File Offset: 0x0012BB00
			private HurtBox GetBestHurtBox(out bool hadLoS)
			{
				if (this.owner && this.owner.bodyInputBank && this.hurtBoxGroup)
				{
					Vector3 aimOrigin = this.owner.bodyInputBank.aimOrigin;
					HurtBox hurtBox = null;
					float num = float.PositiveInfinity;
					foreach (HurtBox hurtBox2 in this.hurtBoxGroup.hurtBoxes)
					{
						if (hurtBox2 && hurtBox2.transform && hurtBox2.isBullseye)
						{
							Vector3 position = hurtBox2.transform.position;
							if (BaseAI.CheckLoS(aimOrigin, hurtBox2.transform.position))
							{
								float sqrMagnitude = (position - aimOrigin).sqrMagnitude;
								if (sqrMagnitude < num)
								{
									num = sqrMagnitude;
									hurtBox = hurtBox2;
								}
							}
						}
					}
					if (hurtBox)
					{
						hadLoS = true;
						return hurtBox;
					}
				}
				hadLoS = false;
				return this.mainHurtBox;
			}

			// Token: 0x170006AC RID: 1708
			// (get) Token: 0x06004930 RID: 18736 RVA: 0x0012D9F5 File Offset: 0x0012BBF5
			// (set) Token: 0x06004931 RID: 18737 RVA: 0x0012D9FD File Offset: 0x0012BBFD
			public CharacterBody characterBody { get; private set; }

			// Token: 0x170006AD RID: 1709
			// (get) Token: 0x06004932 RID: 18738 RVA: 0x0012DA06 File Offset: 0x0012BC06
			// (set) Token: 0x06004933 RID: 18739 RVA: 0x0012DA0E File Offset: 0x0012BC0E
			public HealthComponent healthComponent { get; private set; }

			// Token: 0x06004934 RID: 18740 RVA: 0x0012DA18 File Offset: 0x0012BC18
			public void Reset()
			{
				if (this.unset)
				{
					return;
				}
				this._gameObject = null;
				this.characterBody = null;
				this.healthComponent = null;
				this.hurtBoxGroup = null;
				this.bullseyeCount = 0;
				this.mainHurtBox = null;
				this.bestHurtBox = this.mainHurtBox;
				this.hasLoS = false;
				this.unset = true;
			}

			// Token: 0x040045EB RID: 17899
			private readonly BaseAI owner;

			// Token: 0x040045EC RID: 17900
			private bool unset = true;

			// Token: 0x040045ED RID: 17901
			private GameObject _gameObject;

			// Token: 0x040045F0 RID: 17904
			public HurtBox bestHurtBox;

			// Token: 0x040045F1 RID: 17905
			private HurtBoxGroup hurtBoxGroup;

			// Token: 0x040045F2 RID: 17906
			private HurtBox mainHurtBox;

			// Token: 0x040045F3 RID: 17907
			private int bullseyeCount;

			// Token: 0x040045F4 RID: 17908
			private Vector3? lastKnownBullseyePosition;

			// Token: 0x040045F5 RID: 17909
			private Run.FixedTimeStamp lastKnownBullseyePositionTime = Run.FixedTimeStamp.negativeInfinity;

			// Token: 0x040045F6 RID: 17910
			public bool hasLoS;
		}

		// Token: 0x02000C79 RID: 3193
		public struct SkillDriverEvaluation
		{
			// Token: 0x040045F7 RID: 17911
			public AISkillDriver dominantSkillDriver;

			// Token: 0x040045F8 RID: 17912
			public BaseAI.Target target;

			// Token: 0x040045F9 RID: 17913
			public BaseAI.Target aimTarget;

			// Token: 0x040045FA RID: 17914
			public float separationSqrMagnitude;
		}

		// Token: 0x02000C7A RID: 3194
		public struct BodyInputs
		{
			// Token: 0x040045FB RID: 17915
			public Vector3 desiredAimDirection;

			// Token: 0x040045FC RID: 17916
			public Vector3 moveVector;

			// Token: 0x040045FD RID: 17917
			public bool pressSprint;

			// Token: 0x040045FE RID: 17918
			public bool pressJump;

			// Token: 0x040045FF RID: 17919
			public bool pressSkill1;

			// Token: 0x04004600 RID: 17920
			public bool pressSkill2;

			// Token: 0x04004601 RID: 17921
			public bool pressSkill3;

			// Token: 0x04004602 RID: 17922
			public bool pressSkill4;

			// Token: 0x04004603 RID: 17923
			public bool pressActivateEquipment;
		}
	}
}
