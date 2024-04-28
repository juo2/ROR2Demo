using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000B2 RID: 178
	public class BaseState : EntityState
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterBody)
			{
				this.attackSpeedStat = base.characterBody.attackSpeed;
				this.damageStat = base.characterBody.damage;
				this.critStat = base.characterBody.crit;
				this.moveSpeedStat = base.characterBody.moveSpeed;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C130 File Offset: 0x0000A330
		protected Ray GetAimRay()
		{
			if (base.inputBank)
			{
				return new Ray(base.inputBank.aimOrigin, base.inputBank.aimDirection);
			}
			return new Ray(base.transform.position, base.transform.forward);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C181 File Offset: 0x0000A381
		protected void AddRecoil(float verticalMin, float verticalMax, float horizontalMin, float horizontalMax)
		{
			base.cameraTargetParams.AddRecoil(verticalMin, verticalMax, horizontalMin, horizontalMax);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C194 File Offset: 0x0000A394
		public OverlapAttack InitMeleeOverlap(float damageCoefficient, GameObject hitEffectPrefab, Transform modelTransform, string hitboxGroupName)
		{
			OverlapAttack overlapAttack = new OverlapAttack();
			overlapAttack.attacker = base.gameObject;
			overlapAttack.inflictor = base.gameObject;
			overlapAttack.teamIndex = TeamComponent.GetObjectTeam(overlapAttack.attacker);
			overlapAttack.damage = damageCoefficient * this.damageStat;
			overlapAttack.hitEffectPrefab = hitEffectPrefab;
			overlapAttack.isCrit = this.RollCrit();
			if (modelTransform)
			{
				overlapAttack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitboxGroupName);
			}
			return overlapAttack;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C228 File Offset: 0x0000A428
		public bool FireMeleeOverlap(OverlapAttack attack, Animator animator, string mecanimHitboxActiveParameter, float forceMagnitude, bool calculateForceVector = true)
		{
			bool result = false;
			if (animator && animator.GetFloat(mecanimHitboxActiveParameter) > 0.1f)
			{
				if (calculateForceVector)
				{
					attack.forceVector = base.transform.forward * forceMagnitude;
				}
				result = attack.Fire(null);
			}
			return result;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C274 File Offset: 0x0000A474
		public void SmallHop(CharacterMotor characterMotor, float smallHopVelocity)
		{
			if (characterMotor)
			{
				characterMotor.Motor.ForceUnground();
				characterMotor.velocity = new Vector3(characterMotor.velocity.x, Mathf.Max(characterMotor.velocity.y, smallHopVelocity), characterMotor.velocity.z);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		protected BaseState.HitStopCachedState CreateHitStopCachedState(CharacterMotor characterMotor, Animator animator, string playbackRateAnimationParameter)
		{
			BaseState.HitStopCachedState hitStopCachedState = default(BaseState.HitStopCachedState);
			hitStopCachedState.characterVelocity = new Vector3(characterMotor.velocity.x, Mathf.Max(0f, characterMotor.velocity.y), characterMotor.velocity.z);
			hitStopCachedState.playbackName = playbackRateAnimationParameter;
			hitStopCachedState.playbackRate = animator.GetFloat(hitStopCachedState.playbackName);
			return hitStopCachedState;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C330 File Offset: 0x0000A530
		protected void ConsumeHitStopCachedState(BaseState.HitStopCachedState hitStopCachedState, CharacterMotor characterMotor, Animator animator)
		{
			characterMotor.velocity = hitStopCachedState.characterVelocity;
			animator.SetFloat(hitStopCachedState.playbackName, hitStopCachedState.playbackRate);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C350 File Offset: 0x0000A550
		protected void StartAimMode(float duration = 2f, bool snap = false)
		{
			this.StartAimMode(this.GetAimRay(), duration, snap);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C360 File Offset: 0x0000A560
		protected void StartAimMode(Ray aimRay, float duration = 2f, bool snap = false)
		{
			if (base.characterDirection && aimRay.direction != Vector3.zero)
			{
				if (snap)
				{
					base.characterDirection.forward = aimRay.direction;
				}
				else
				{
					base.characterDirection.moveVector = aimRay.direction;
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(duration);
			}
			if (base.modelLocator)
			{
				Transform modelTransform = base.modelLocator.modelTransform;
				if (modelTransform)
				{
					AimAnimator component = modelTransform.GetComponent<AimAnimator>();
					if (component && snap)
					{
						component.AimImmediate();
					}
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C407 File Offset: 0x0000A607
		protected bool RollCrit()
		{
			return base.characterBody && base.characterBody.master && Util.CheckRoll(this.critStat, base.characterBody.master);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C440 File Offset: 0x0000A640
		protected Transform FindModelChild(string childName)
		{
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				return modelChildLocator.FindChild(childName);
			}
			return null;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C468 File Offset: 0x0000A668
		protected T FindModelChildComponent<T>(string childName)
		{
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				return modelChildLocator.FindChildComponent<T>(childName);
			}
			return default(T);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C498 File Offset: 0x0000A698
		protected GameObject FindModelChildGameObject(string childName)
		{
			ChildLocator modelChildLocator = base.GetModelChildLocator();
			if (modelChildLocator)
			{
				return modelChildLocator.FindChildGameObject(childName);
			}
			return null;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		protected bool isGrounded
		{
			get
			{
				return base.characterMotor && base.characterMotor.isGrounded;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C4D9 File Offset: 0x0000A6D9
		public TeamIndex GetTeam()
		{
			return TeamComponent.GetObjectTeam(base.gameObject);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C4E6 File Offset: 0x0000A6E6
		public bool HasBuff(BuffIndex buffType)
		{
			return base.characterBody && base.characterBody.HasBuff(buffType);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C503 File Offset: 0x0000A703
		public bool HasBuff(BuffDef buffType)
		{
			return base.characterBody && base.characterBody.HasBuff(buffType);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C520 File Offset: 0x0000A720
		public int GetBuffCount(BuffIndex buffType)
		{
			if (!base.characterBody)
			{
				return 0;
			}
			return base.characterBody.GetBuffCount(buffType);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C53D File Offset: 0x0000A73D
		public int GetBuffCount(BuffDef buffType)
		{
			if (!base.characterBody)
			{
				return 0;
			}
			return base.characterBody.GetBuffCount(buffType);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C55A File Offset: 0x0000A75A
		protected void AttemptToStartSprint()
		{
			if (base.inputBank)
			{
				base.inputBank.sprint.down = true;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C57C File Offset: 0x0000A77C
		protected HitBoxGroup FindHitBoxGroup(string groupName)
		{
			Transform modelTransform = base.GetModelTransform();
			if (!modelTransform)
			{
				return null;
			}
			HitBoxGroup result = null;
			List<HitBoxGroup> gameObjectComponents = GetComponentsCache<HitBoxGroup>.GetGameObjectComponents(modelTransform.gameObject);
			int i = 0;
			int count = gameObjectComponents.Count;
			while (i < count)
			{
				if (gameObjectComponents[i].groupName == groupName)
				{
					result = gameObjectComponents[i];
					break;
				}
				i++;
			}
			GetComponentsCache<HitBoxGroup>.ReturnBuffer(gameObjectComponents);
			return result;
		}

		// Token: 0x04000321 RID: 801
		protected float attackSpeedStat = 1f;

		// Token: 0x04000322 RID: 802
		protected float damageStat;

		// Token: 0x04000323 RID: 803
		protected float critStat;

		// Token: 0x04000324 RID: 804
		protected float moveSpeedStat;

		// Token: 0x04000325 RID: 805
		private const float defaultAimDuration = 2f;

		// Token: 0x020000B3 RID: 179
		protected struct HitStopCachedState
		{
			// Token: 0x04000326 RID: 806
			public Vector3 characterVelocity;

			// Token: 0x04000327 RID: 807
			public string playbackName;

			// Token: 0x04000328 RID: 808
			public float playbackRate;
		}
	}
}
