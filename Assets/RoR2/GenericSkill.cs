using System;
using System.Collections.Generic;
using EntityStates;
using HG;
using JetBrains.Annotations;
using RoR2.Skills;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000702 RID: 1794
	[RequireComponent(typeof(CharacterBody))]
	public sealed class GenericSkill : MonoBehaviour, ILifeBehavior
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x0009CB9F File Offset: 0x0009AD9F
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x0009CBA7 File Offset: 0x0009ADA7
		public SkillDef skillDef { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0009CBB0 File Offset: 0x0009ADB0
		public SkillFamily skillFamily
		{
			get
			{
				return this._skillFamily;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x0009CBB8 File Offset: 0x0009ADB8
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x0009CBC0 File Offset: 0x0009ADC0
		public SkillDef baseSkill { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x0009CBC9 File Offset: 0x0009ADC9
		public string skillNameToken
		{
			get
			{
				return this.skillDef.GetCurrentNameToken(this);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0009CBD7 File Offset: 0x0009ADD7
		public string skillDescriptionToken
		{
			get
			{
				return this.skillDef.GetCurrentDescriptionToken(this);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x0009CBE5 File Offset: 0x0009ADE5
		public float baseRechargeInterval
		{
			get
			{
				return this.skillDef.GetRechargeInterval(this);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0009CBF3 File Offset: 0x0009ADF3
		public int rechargeStock
		{
			get
			{
				return this.skillDef.GetRechargeStock(this);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x0009CC01 File Offset: 0x0009AE01
		public bool beginSkillCooldownOnSkillEnd
		{
			get
			{
				return this.skillDef.beginSkillCooldownOnSkillEnd;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0009CC0E File Offset: 0x0009AE0E
		public SerializableEntityStateType activationState
		{
			get
			{
				return this.skillDef.activationState;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x0009CC1B File Offset: 0x0009AE1B
		public InterruptPriority interruptPriority
		{
			get
			{
				return this.skillDef.interruptPriority;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0009CC28 File Offset: 0x0009AE28
		public bool isCombatSkill
		{
			get
			{
				return this.skillDef.isCombatSkill;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x0009CC35 File Offset: 0x0009AE35
		public bool mustKeyPress
		{
			get
			{
				return this.skillDef.mustKeyPress;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0009CC42 File Offset: 0x0009AE42
		public Sprite icon
		{
			get
			{
				return this.skillDef.GetCurrentIcon(this);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x0009CC50 File Offset: 0x0009AE50
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x0009CC58 File Offset: 0x0009AE58
		[CanBeNull]
		public EntityStateMachine stateMachine { get; private set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x0009CC61 File Offset: 0x0009AE61
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x0009CC69 File Offset: 0x0009AE69
		[CanBeNull]
		public SkillDef.BaseSkillInstanceData skillInstanceData { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0009CC72 File Offset: 0x0009AE72
		// (set) Token: 0x060024D8 RID: 9432 RVA: 0x0009CC7A File Offset: 0x0009AE7A
		public CharacterBody characterBody { get; private set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0009CC83 File Offset: 0x0009AE83
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x0009CC8B File Offset: 0x0009AE8B
		public SkillDef defaultSkillDef { get; private set; }

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060024DB RID: 9435 RVA: 0x0009CC94 File Offset: 0x0009AE94
		// (remove) Token: 0x060024DC RID: 9436 RVA: 0x0009CCCC File Offset: 0x0009AECC
		public event Action<GenericSkill> onSkillChanged;

		// Token: 0x060024DD RID: 9437 RVA: 0x0009CD04 File Offset: 0x0009AF04
		private int FindSkillOverrideIndex(ref GenericSkill.SkillOverride skillOverride)
		{
			for (int i = 0; i < this.skillOverrides.Length; i++)
			{
				if (this.skillOverrides[i].Equals(skillOverride))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x0009CD38 File Offset: 0x0009AF38
		public void SetSkillOverride(object source, SkillDef skillDef, GenericSkill.SkillOverridePriority priority)
		{
			GenericSkill.SkillOverride skillOverride = new GenericSkill.SkillOverride(source, skillDef, priority);
			if (this.FindSkillOverrideIndex(ref skillOverride) == -1)
			{
				ArrayUtils.ArrayAppend<GenericSkill.SkillOverride>(ref this.skillOverrides, skillOverride);
				this.PickCurrentOverride();
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0009CD6C File Offset: 0x0009AF6C
		public void UnsetSkillOverride(object source, SkillDef skillDef, GenericSkill.SkillOverridePriority priority)
		{
			GenericSkill.SkillOverride skillOverride = new GenericSkill.SkillOverride(source, skillDef, priority);
			int num = this.FindSkillOverrideIndex(ref skillOverride);
			if (num != -1)
			{
				ArrayUtils.ArrayRemoveAtAndResize<GenericSkill.SkillOverride>(ref this.skillOverrides, num, 1);
				this.PickCurrentOverride();
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0009CDA4 File Offset: 0x0009AFA4
		public bool HasSkillOverrideOfPriority(GenericSkill.SkillOverridePriority priority)
		{
			for (int i = 0; i < this.skillOverrides.Length; i++)
			{
				if (priority == this.skillOverrides[i].priority)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0009CDD8 File Offset: 0x0009AFD8
		private void PickCurrentOverride()
		{
			this.currentSkillOverride = -1;
			GenericSkill.SkillOverridePriority skillOverridePriority = GenericSkill.SkillOverridePriority.Default;
			for (int i = 0; i < this.skillOverrides.Length; i++)
			{
				GenericSkill.SkillOverridePriority priority = this.skillOverrides[i].priority;
				if (skillOverridePriority <= priority)
				{
					this.currentSkillOverride = i;
					skillOverridePriority = priority;
				}
			}
			if (this.currentSkillOverride == -1)
			{
				this.SetSkillInternal(this.baseSkill);
				return;
			}
			this.SetSkillInternal(this.skillOverrides[this.currentSkillOverride].skillDef);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0009CE4A File Offset: 0x0009B04A
		private void SetSkillInternal(SkillDef newSkillDef)
		{
			if (this.skillDef == newSkillDef)
			{
				return;
			}
			this.UnassignSkill();
			this.AssignSkill(newSkillDef);
			Action<GenericSkill> action = this.onSkillChanged;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0009CE79 File Offset: 0x0009B079
		public void SetBaseSkill(SkillDef newSkillDef)
		{
			this.baseSkill = newSkillDef;
			this.PickCurrentOverride();
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0009CE88 File Offset: 0x0009B088
		private void UnassignSkill()
		{
			if (this.skillDef == null)
			{
				return;
			}
			this.skillDef.OnUnassigned(this);
			this.skillInstanceData = null;
			this.skillDef = null;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0009CEB0 File Offset: 0x0009B0B0
		private void AssignSkill(SkillDef newSkillDef)
		{
			this.skillDef = newSkillDef;
			if (this.skillDef == null)
			{
				return;
			}
			this.PickTargetStateMachine();
			this.RecalculateMaxStock();
			if (this.skillDef.fullRestockOnAssign && this.stock < this.maxStock)
			{
				this.stock = this.maxStock;
			}
			if (this.skillDef.dontAllowPastMaxStocks)
			{
				this.stock = Mathf.Min(this.maxStock, this.stock);
			}
			this.skillInstanceData = this.skillDef.OnAssigned(this);
			this.RecalculateFinalRechargeInterval();
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0009CF3C File Offset: 0x0009B13C
		public void SetBonusStockFromBody(int newBonusStockFromBody)
		{
			this.bonusStockFromBody = newBonusStockFromBody;
			this.RecalculateMaxStock();
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x0009CF4B File Offset: 0x0009B14B
		// (set) Token: 0x060024E8 RID: 9448 RVA: 0x0009CF53 File Offset: 0x0009B153
		public int maxStock { get; private set; } = 1;

		// Token: 0x060024E9 RID: 9449 RVA: 0x0009CF5C File Offset: 0x0009B15C
		private void RecalculateMaxStock()
		{
			this.maxStock = this.skillDef.GetMaxStock(this) + this.bonusStockFromBody;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x0009CF77 File Offset: 0x0009B177
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x0009CFAB File Offset: 0x0009B1AB
		public int stock
		{
			get
			{
				if (this.currentSkillOverride >= 0 && this.currentSkillOverride < this.skillOverrides.Length)
				{
					return this.skillOverrides[this.currentSkillOverride].stock;
				}
				return this.baseStock;
			}
			set
			{
				if (this.currentSkillOverride >= 0 && this.currentSkillOverride < this.skillOverrides.Length)
				{
					this.skillOverrides[this.currentSkillOverride].stock = value;
					return;
				}
				this.baseStock = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x0009CFE1 File Offset: 0x0009B1E1
		// (set) Token: 0x060024ED RID: 9453 RVA: 0x0009CFE9 File Offset: 0x0009B1E9
		public float cooldownScale
		{
			get
			{
				return this._cooldownScale;
			}
			set
			{
				if (this._cooldownScale == value)
				{
					return;
				}
				this._cooldownScale = value;
				this.RecalculateFinalRechargeInterval();
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x0009D002 File Offset: 0x0009B202
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x0009D00A File Offset: 0x0009B20A
		public float flatCooldownReduction
		{
			get
			{
				return this._flatCooldownReduction;
			}
			set
			{
				if (this._flatCooldownReduction == value)
				{
					return;
				}
				this._flatCooldownReduction = value;
				this.RecalculateFinalRechargeInterval();
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0009D023 File Offset: 0x0009B223
		// (set) Token: 0x060024F1 RID: 9457 RVA: 0x0009D057 File Offset: 0x0009B257
		public float rechargeStopwatch
		{
			get
			{
				if (this.currentSkillOverride >= 0 && this.currentSkillOverride < this.skillOverrides.Length)
				{
					return this.skillOverrides[this.currentSkillOverride].rechargeStopwatch;
				}
				return this.baseRechargeStopwatch;
			}
			set
			{
				if (this.currentSkillOverride >= 0 && this.currentSkillOverride < this.skillOverrides.Length)
				{
					this.skillOverrides[this.currentSkillOverride].rechargeStopwatch = value;
					return;
				}
				this.baseRechargeStopwatch = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0009D08D File Offset: 0x0009B28D
		public float cooldownRemaining
		{
			get
			{
				if (this.stock != this.maxStock && this.rechargeStock != 0)
				{
					return this.finalRechargeInterval - this.rechargeStopwatch;
				}
				return 0f;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0009D0B8 File Offset: 0x0009B2B8
		private void Awake()
		{
			this.defaultSkillDef = this.skillFamily.defaultSkillDef;
			this.baseSkill = this.defaultSkillDef;
			this.characterBody = base.GetComponent<CharacterBody>();
			this.AssignSkill(this.defaultSkillDef);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0009D0EF File Offset: 0x0009B2EF
		private void OnDestroy()
		{
			this.UnassignSkill();
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0009D0F7 File Offset: 0x0009B2F7
		private void Start()
		{
			this.RecalculateMaxStock();
			this.stock = this.maxStock;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0009D10B File Offset: 0x0009B30B
		private void FixedUpdate()
		{
			SkillDef skillDef = this.skillDef;
			if (skillDef == null)
			{
				return;
			}
			skillDef.OnFixedUpdate(this);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		public void OnDeathStart()
		{
			base.enabled = false;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0009D11E File Offset: 0x0009B31E
		public bool CanExecute()
		{
			SkillDef skillDef = this.skillDef;
			return skillDef != null && skillDef.CanExecute(this);
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x0009D132 File Offset: 0x0009B332
		public bool IsReady()
		{
			SkillDef skillDef = this.skillDef;
			return skillDef != null && skillDef.IsReady(this);
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0009D146 File Offset: 0x0009B346
		public bool ExecuteIfReady()
		{
			this.hasExecutedSuccessfully = this.CanExecute();
			if (this.hasExecutedSuccessfully)
			{
				this.OnExecute();
				return true;
			}
			return false;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x0009D168 File Offset: 0x0009B368
		public void RunRecharge(float dt)
		{
			if (this.stock < this.maxStock)
			{
				if (!this.beginSkillCooldownOnSkillEnd || !(this.stateMachine.state.GetType() == this.activationState.stateType))
				{
					this.rechargeStopwatch += dt;
				}
				if (this.rechargeStopwatch >= this.finalRechargeInterval)
				{
					this.RestockSteplike();
				}
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0009D1D2 File Offset: 0x0009B3D2
		public void Reset()
		{
			this.rechargeStopwatch = 0f;
			this.stock = this.maxStock;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0009D1EB File Offset: 0x0009B3EB
		public bool CanApplyAmmoPack()
		{
			return this.stock < this.maxStock;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0009D1FE File Offset: 0x0009B3FE
		public void ApplyAmmoPack()
		{
			if (this.stock < this.maxStock)
			{
				this.stock += this.rechargeStock;
				if (this.stock > this.maxStock)
				{
					this.stock = this.maxStock;
				}
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0009D23C File Offset: 0x0009B43C
		public void AddOneStock()
		{
			int stock = this.stock + 1;
			this.stock = stock;
			this.rechargeStopwatch = 0f;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0009D264 File Offset: 0x0009B464
		public void RemoveAllStocks()
		{
			this.stock = 0;
			this.rechargeStopwatch = 0f;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x0009D278 File Offset: 0x0009B478
		public void DeductStock(int count)
		{
			this.stock = Mathf.Max(0, this.stock - count);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0009D28E File Offset: 0x0009B48E
		private void OnExecute()
		{
			this.skillDef.OnExecute(this);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0009D29C File Offset: 0x0009B49C
		private void RestockContinuous()
		{
			if (this.finalRechargeInterval == 0f)
			{
				this.stock = this.maxStock;
				this.rechargeStopwatch = 0f;
				return;
			}
			int num = Mathf.FloorToInt(this.rechargeStopwatch / this.finalRechargeInterval * (float)this.rechargeStock);
			this.stock += num;
			if (this.stock >= this.maxStock)
			{
				this.stock = this.maxStock;
				this.rechargeStopwatch = 0f;
				return;
			}
			this.rechargeStopwatch -= (float)num * this.finalRechargeInterval;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0009D333 File Offset: 0x0009B533
		private void RestockSteplike()
		{
			this.stock += this.rechargeStock;
			if (this.stock >= this.maxStock)
			{
				this.stock = this.maxStock;
			}
			this.rechargeStopwatch = 0f;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0009D36D File Offset: 0x0009B56D
		public float CalculateFinalRechargeInterval()
		{
			return Mathf.Min(this.baseRechargeInterval, Mathf.Max(0.5f, this.baseRechargeInterval * this.cooldownScale - this.flatCooldownReduction));
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0009D398 File Offset: 0x0009B598
		private void RecalculateFinalRechargeInterval()
		{
			this.finalRechargeInterval = this.CalculateFinalRechargeInterval();
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0009D3A6 File Offset: 0x0009B5A6
		public void RecalculateValues()
		{
			this.RecalculateMaxStock();
			this.RecalculateFinalRechargeInterval();
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002508 RID: 9480 RVA: 0x0009D3B4 File Offset: 0x0009B5B4
		// (remove) Token: 0x06002509 RID: 9481 RVA: 0x0009D3EC File Offset: 0x0009B5EC
		private event GenericSkill.StateMachineResolver _customStateMachineResolver;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x0600250A RID: 9482 RVA: 0x0009D421 File Offset: 0x0009B621
		// (remove) Token: 0x0600250B RID: 9483 RVA: 0x0009D430 File Offset: 0x0009B630
		public event GenericSkill.StateMachineResolver customStateMachineResolver
		{
			add
			{
				this._customStateMachineResolver += value;
				this.PickTargetStateMachine();
			}
			remove
			{
				this._customStateMachineResolver -= value;
				this.PickTargetStateMachine();
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0009D440 File Offset: 0x0009B640
		private void PickTargetStateMachine()
		{
			EntityStateMachine stateMachine = this.stateMachine;
			EntityStateMachine stateMachine2 = this.stateMachine;
			if (!string.Equals((stateMachine2 != null) ? stateMachine2.customName : null, this.skillDef.activationStateMachineName, StringComparison.Ordinal))
			{
				stateMachine = EntityStateMachine.FindByCustomName(base.gameObject, this.skillDef.activationStateMachineName);
			}
			GenericSkill.StateMachineResolver customStateMachineResolver = this._customStateMachineResolver;
			if (customStateMachineResolver != null)
			{
				customStateMachineResolver(this, this.skillDef, ref stateMachine);
			}
			this.stateMachine = stateMachine;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0009D4B1 File Offset: 0x0009B6B1
		[AssetCheck(typeof(GenericSkill))]
		private static void CheckGenericSkillStateMachine(AssetCheckArgs args)
		{
			if (((GenericSkill)args.asset).stateMachine.customName == string.Empty)
			{
				args.LogError("Unnamed state machine.", ((GenericSkill)args.asset).gameObject);
			}
		}

		// Token: 0x040028C9 RID: 10441
		[SerializeField]
		private SkillFamily _skillFamily;

		// Token: 0x040028CB RID: 10443
		public string skillName;

		// Token: 0x040028CC RID: 10444
		public bool hideInCharacterSelect;

		// Token: 0x040028D2 RID: 10450
		private static readonly List<EntityStateMachine> stateMachineLookupBuffer = new List<EntityStateMachine>();

		// Token: 0x040028D3 RID: 10451
		private GenericSkill.SkillOverride[] skillOverrides = Array.Empty<GenericSkill.SkillOverride>();

		// Token: 0x040028D4 RID: 10452
		private int currentSkillOverride = -1;

		// Token: 0x040028D5 RID: 10453
		private int bonusStockFromBody;

		// Token: 0x040028D7 RID: 10455
		private int baseStock;

		// Token: 0x040028D8 RID: 10456
		private float finalRechargeInterval;

		// Token: 0x040028D9 RID: 10457
		private float _cooldownScale = 1f;

		// Token: 0x040028DA RID: 10458
		private float _flatCooldownReduction = 1f;

		// Token: 0x040028DB RID: 10459
		private float baseRechargeStopwatch;

		// Token: 0x040028DC RID: 10460
		[HideInInspector]
		public bool hasExecutedSuccessfully;

		// Token: 0x02000703 RID: 1795
		public class SkillOverride : IEquatable<GenericSkill.SkillOverride>
		{
			// Token: 0x06002510 RID: 9488 RVA: 0x0009D532 File Offset: 0x0009B732
			public SkillOverride(object source, SkillDef skillDef, GenericSkill.SkillOverridePriority priority)
			{
				this.source = source;
				this.skillDef = skillDef;
				this.priority = priority;
				this.stock = 0;
				this.rechargeStopwatch = 0f;
			}

			// Token: 0x06002511 RID: 9489 RVA: 0x0009D561 File Offset: 0x0009B761
			public bool Equals(GenericSkill.SkillOverride other)
			{
				return object.Equals(this.source, other.source) && object.Equals(this.skillDef, other.skillDef) && this.priority == other.priority;
			}

			// Token: 0x06002512 RID: 9490 RVA: 0x0009D59C File Offset: 0x0009B79C
			public override bool Equals(object obj)
			{
				GenericSkill.SkillOverride other;
				return (other = (obj as GenericSkill.SkillOverride)) != null && this.Equals(other);
			}

			// Token: 0x06002513 RID: 9491 RVA: 0x0009D5BC File Offset: 0x0009B7BC
			public override int GetHashCode()
			{
				return (((this.source != null) ? this.source.GetHashCode() : 0) * 397 ^ ((this.skillDef != null) ? this.skillDef.GetHashCode() : 0)) * 397 ^ (int)this.priority;
			}

			// Token: 0x040028DE RID: 10462
			public readonly object source;

			// Token: 0x040028DF RID: 10463
			public readonly SkillDef skillDef;

			// Token: 0x040028E0 RID: 10464
			public readonly GenericSkill.SkillOverridePriority priority;

			// Token: 0x040028E1 RID: 10465
			public int stock;

			// Token: 0x040028E2 RID: 10466
			public float rechargeStopwatch;
		}

		// Token: 0x02000704 RID: 1796
		public enum SkillOverridePriority
		{
			// Token: 0x040028E4 RID: 10468
			Default,
			// Token: 0x040028E5 RID: 10469
			Loadout,
			// Token: 0x040028E6 RID: 10470
			Upgrade,
			// Token: 0x040028E7 RID: 10471
			Replacement,
			// Token: 0x040028E8 RID: 10472
			Contextual,
			// Token: 0x040028E9 RID: 10473
			Network
		}

		// Token: 0x02000705 RID: 1797
		public class SkillOverrideHandle : IDisposable
		{
			// Token: 0x06002514 RID: 9492 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x040028EA RID: 10474
			public readonly object source;

			// Token: 0x040028EB RID: 10475
			public readonly SkillDef skill;

			// Token: 0x040028EC RID: 10476
			public readonly GenericSkill skillSlot;

			// Token: 0x040028ED RID: 10477
			public readonly GenericSkill.SkillOverridePriority priority;
		}

		// Token: 0x02000706 RID: 1798
		// (Invoke) Token: 0x06002517 RID: 9495
		public delegate void StateMachineResolver(GenericSkill genericSkill, SkillDef skillDef, ref EntityStateMachine targetStateMachine);
	}
}
