using System;
using EntityStates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.Skills
{
	// Token: 0x02000C17 RID: 3095
	[CreateAssetMenu(menuName = "RoR2/SkillDef/Generic")]
	public class SkillDef : ScriptableObject
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x00003BE8 File Offset: 0x00001DE8
		[Obsolete("Accessing UnityEngine.Object.Name causes allocations on read. Look up the name from the catalog instead. If absolutely necessary to perform direct access, cast to ScriptableObject first.")]
		public new string name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x00122460 File Offset: 0x00120660
		// (set) Token: 0x06004601 RID: 17921 RVA: 0x00122468 File Offset: 0x00120668
		public int skillIndex { get; set; }

		// Token: 0x06004602 RID: 17922 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public virtual SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return null;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnUnassigned([NotNull] GenericSkill skillSlot)
		{
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00122471 File Offset: 0x00120671
		public virtual Sprite GetCurrentIcon([NotNull] GenericSkill skillSlot)
		{
			return this.icon;
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00122479 File Offset: 0x00120679
		public virtual string GetCurrentNameToken([NotNull] GenericSkill skillSlot)
		{
			return this.skillNameToken;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x00122481 File Offset: 0x00120681
		public virtual string GetCurrentDescriptionToken([NotNull] GenericSkill skillSlot)
		{
			return this.skillDescriptionToken;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x00122489 File Offset: 0x00120689
		protected bool HasRequiredStockAndDelay([NotNull] GenericSkill skillSlot)
		{
			return skillSlot.stock >= this.requiredStock;
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x0012249C File Offset: 0x0012069C
		public virtual bool CanExecute([NotNull] GenericSkill skillSlot)
		{
			return this.HasRequiredStockAndDelay(skillSlot) && this.IsReady(skillSlot) && skillSlot.stateMachine && !skillSlot.stateMachine.HasPendingState() && skillSlot.stateMachine.CanInterruptState(this.interruptPriority);
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x001224E8 File Offset: 0x001206E8
		public virtual bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return this.HasRequiredStockAndDelay(skillSlot);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x001224F4 File Offset: 0x001206F4
		protected virtual EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
		{
			EntityState entityState = EntityStateCatalog.InstantiateState(this.activationState);
			ISkillState skillState;
			if ((skillState = (entityState as ISkillState)) != null)
			{
				skillState.activatorSkillSlot = skillSlot;
			}
			return entityState;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00122520 File Offset: 0x00120720
		public virtual void OnExecute([NotNull] GenericSkill skillSlot)
		{
			skillSlot.stateMachine.SetInterruptState(this.InstantiateNextState(skillSlot), this.interruptPriority);
			if (this.cancelSprintingOnActivation)
			{
				skillSlot.characterBody.isSprinting = false;
			}
			skillSlot.stock -= this.stockToConsume;
			if (this.resetCooldownTimerOnUse)
			{
				skillSlot.rechargeStopwatch = 0f;
			}
			if (skillSlot.characterBody)
			{
				skillSlot.characterBody.OnSkillActivated(skillSlot);
			}
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x0012259C File Offset: 0x0012079C
		public virtual void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			skillSlot.RunRecharge(Time.fixedDeltaTime);
			if (((this.canceledFromSprinting && skillSlot.characterBody.isSprinting) || this.forceSprintDuringState) && skillSlot.stateMachine.state.GetType() == this.activationState.stateType)
			{
				if (this.canceledFromSprinting && skillSlot.characterBody.isSprinting)
				{
					skillSlot.stateMachine.SetNextStateToMain();
				}
				if (this.forceSprintDuringState)
				{
					skillSlot.characterBody.isSprinting = true;
				}
			}
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x0012262A File Offset: 0x0012082A
		public bool IsAlreadyInState([NotNull] GenericSkill skillSlot)
		{
			return ((skillSlot != null) ? skillSlot.stateMachine.state.GetType() : null) == this.activationState.stateType;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00122652 File Offset: 0x00120852
		public virtual int GetMaxStock([NotNull] GenericSkill skillSlot)
		{
			return this.baseMaxStock;
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x0012265A File Offset: 0x0012085A
		public virtual int GetRechargeStock([NotNull] GenericSkill skillSlot)
		{
			return this.rechargeStock;
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00122662 File Offset: 0x00120862
		public virtual float GetRechargeInterval([NotNull] GenericSkill skillSlot)
		{
			return this.baseRechargeInterval;
		}

		// Token: 0x040043F2 RID: 17394
		[Header("Skill Identifier")]
		[Tooltip("The name of the skill. This is mainly for purposes of identification in the inspector and currently has no direct effect.")]
		public string skillName = "";

		// Token: 0x040043F3 RID: 17395
		[Tooltip("The language token with the name of this skill.")]
		[Header("User-Facing Info")]
		public string skillNameToken = "";

		// Token: 0x040043F4 RID: 17396
		[Tooltip("The language token with the description of this skill.")]
		public string skillDescriptionToken = "";

		// Token: 0x040043F5 RID: 17397
		[Tooltip("Extra tooltips when hovered over in character select. Currently only used in that area!")]
		public string[] keywordTokens;

		// Token: 0x040043F6 RID: 17398
		[Tooltip("The icon to display for this skill.")]
		[ShowThumbnail]
		public Sprite icon;

		// Token: 0x040043F7 RID: 17399
		[Tooltip("The state machine this skill operates upon.")]
		[Header("State Machine Parameters")]
		public string activationStateMachineName;

		// Token: 0x040043F8 RID: 17400
		[Tooltip("The state to enter when this skill is activated.")]
		public SerializableEntityStateType activationState;

		// Token: 0x040043F9 RID: 17401
		[Tooltip("The priority of this skill.")]
		public InterruptPriority interruptPriority = InterruptPriority.Skill;

		// Token: 0x040043FA RID: 17402
		[Header("Stock and Cooldown")]
		[Tooltip("How long it takes for this skill to recharge after being used.")]
		public float baseRechargeInterval = 1f;

		// Token: 0x040043FB RID: 17403
		[Tooltip("Maximum number of charges this skill can carry.")]
		public int baseMaxStock = 1;

		// Token: 0x040043FC RID: 17404
		[Tooltip("How much stock to restore on a recharge.")]
		public int rechargeStock = 1;

		// Token: 0x040043FD RID: 17405
		[Tooltip("How much stock is required to activate this skill.")]
		public int requiredStock = 1;

		// Token: 0x040043FE RID: 17406
		[Tooltip("How much stock to deduct when the skill is activated.")]
		public int stockToConsume = 1;

		// Token: 0x040043FF RID: 17407
		[Header("Optional Parameters, Stock")]
		[Tooltip("Whether or not it resets any progress on cooldowns.")]
		[FormerlySerializedAs("isBullets")]
		public bool resetCooldownTimerOnUse;

		// Token: 0x04004400 RID: 17408
		[Tooltip("Whether or not to fully restock this skill when it's assigned.")]
		public bool fullRestockOnAssign = true;

		// Token: 0x04004401 RID: 17409
		[Tooltip("Whether or not this skill can hold past it's maximum stock.")]
		public bool dontAllowPastMaxStocks;

		// Token: 0x04004402 RID: 17410
		[Tooltip("Whether or not the cooldown waits until it leaves the set state")]
		public bool beginSkillCooldownOnSkillEnd;

		// Token: 0x04004403 RID: 17411
		[Tooltip("Whether or not activating the skill forces off sprinting.")]
		[FormerlySerializedAs("noSprint")]
		[Header("Optional Parameters, Sprinting")]
		public bool cancelSprintingOnActivation = true;

		// Token: 0x04004404 RID: 17412
		[Tooltip("Whether or not this skill is considered 'mobility'. Currently just forces sprint.")]
		[FormerlySerializedAs("mobilitySkill")]
		public bool forceSprintDuringState;

		// Token: 0x04004405 RID: 17413
		[Tooltip("Whether or not sprinting sets the skill's state to be reset.")]
		public bool canceledFromSprinting;

		// Token: 0x04004406 RID: 17414
		[Tooltip("Whether or not this is considered a combat skill.")]
		[Header("Optional Parameters, Misc")]
		public bool isCombatSkill = true;

		// Token: 0x04004407 RID: 17415
		[Tooltip("The skill can't be activated if the key is held.")]
		public bool mustKeyPress;

		// Token: 0x02000C18 RID: 3096
		public class BaseSkillInstanceData
		{
		}
	}
}
