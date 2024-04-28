using System;
using RoR2;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Assassin2
{
	// Token: 0x02000486 RID: 1158
	public class DashStrike : BaseState
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x0005BD5C File Offset: 0x00059F5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = (DashStrike.dashDuration + DashStrike.slashDuration) / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			base.characterMotor.velocity = Vector3.zero;
			float sqrMagnitude = (base.characterBody.master.GetComponent<BaseAI>().currentEnemy.characterBody.corePosition - base.characterBody.corePosition).sqrMagnitude;
			this.calculatedDashSpeed = Util.Remap(sqrMagnitude, 0f, DashStrike.maxSlashDistance * DashStrike.maxSlashDistance, 0f, DashStrike.maxDashSpeedCoefficient);
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = DashStrike.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = DashStrike.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			this.attack.procCoefficient = DashStrike.procCoefficient;
			this.attack.damageType = DamageType.Generic;
			Util.PlayAttackSpeedSound(DashStrike.enterSoundString, base.gameObject, this.attackSpeedStat);
			if (this.modelAnimator)
			{
				this.PlayAnimation("Gesture", "Dash");
				this.handParamHash = Animator.StringToHash("HandStrike");
				this.swordParamHash = Animator.StringToHash("SwordStrike");
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0003BCF7 File Offset: 0x00039EF7
		public override void OnExit()
		{
			base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			base.OnExit();
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005BF10 File Offset: 0x0005A110
		private void HandleSlash(int animatorParamHash, string muzzleName, string hitBoxGroupName)
		{
			bool flag = false;
			if (this.modelAnimator.GetFloat(animatorParamHash) > 0.1f)
			{
				if (!this.startedSlash)
				{
					Util.PlaySound(DashStrike.slashSoundString, base.gameObject);
					EffectManager.SimpleMuzzleFlash(DashStrike.swipeEffectPrefab, base.gameObject, muzzleName, true);
					this.startedSlash = true;
				}
				if (this.modelTransform)
				{
					this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitBoxGroupName);
				}
				if (base.healthComponent)
				{
					base.healthComponent.TakeDamageForce(base.characterDirection.forward * DashStrike.selfForce, true, false);
				}
				this.attack.ResetIgnoredHealthComponents();
				if (base.characterDirection)
				{
					this.attack.forceVector = base.characterDirection.forward * DashStrike.forceMagnitude;
				}
				flag = this.attack.Fire(null);
				return;
			}
			if (this.startedSlash || flag)
			{
				this.slashCount++;
				this.startedSlash = false;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005C03C File Offset: 0x0005A23C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.dashComplete)
			{
				this.targetMoveVector = Vector3.ProjectOnPlane(Vector3.SmoothDamp(this.targetMoveVector, base.inputBank.aimDirection, ref this.targetMoveVectorVelocity, 0f, 0f), Vector3.up).normalized;
				base.characterDirection.moveVector = this.targetMoveVector;
				Vector3 forward = base.characterDirection.forward;
				float value = this.moveSpeedStat * this.calculatedDashSpeed;
				base.characterMotor.moveDirection = forward * this.calculatedDashSpeed;
				this.modelAnimator.SetFloat("forwardSpeed", value);
			}
			if (NetworkServer.active)
			{
				if (base.fixedAge > DashStrike.dashDuration && !this.dashComplete)
				{
					this.PlayAnimation("Gesture", "SwordStrike");
					this.dashComplete = true;
				}
				if (this.modelAnimator)
				{
					switch (this.slashCount)
					{
					case 0:
						this.HandleSlash(this.handParamHash, "ShurikenTag", "ShurikenHitbox");
						break;
					case 1:
						this.HandleSlash(this.swordParamHash, "Sword", "SwordHitbox");
						break;
					}
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A6D RID: 6765
		public static float dashDuration = 1f;

		// Token: 0x04001A6E RID: 6766
		public static float slashDuration = 1.667f;

		// Token: 0x04001A6F RID: 6767
		public static float damageCoefficient = 4f;

		// Token: 0x04001A70 RID: 6768
		public static float procCoefficient;

		// Token: 0x04001A71 RID: 6769
		public static float selfForce;

		// Token: 0x04001A72 RID: 6770
		public static float forceMagnitude = 16f;

		// Token: 0x04001A73 RID: 6771
		public static float maxDashSpeedCoefficient = 4f;

		// Token: 0x04001A74 RID: 6772
		public static float maxSlashDistance = 20f;

		// Token: 0x04001A75 RID: 6773
		public static GameObject hitEffectPrefab;

		// Token: 0x04001A76 RID: 6774
		public static GameObject swipeEffectPrefab;

		// Token: 0x04001A77 RID: 6775
		public static string enterSoundString;

		// Token: 0x04001A78 RID: 6776
		public static string slashSoundString;

		// Token: 0x04001A79 RID: 6777
		private Vector3 targetMoveVector;

		// Token: 0x04001A7A RID: 6778
		private Vector3 targetMoveVectorVelocity;

		// Token: 0x04001A7B RID: 6779
		private OverlapAttack attack;

		// Token: 0x04001A7C RID: 6780
		private Animator modelAnimator;

		// Token: 0x04001A7D RID: 6781
		private float duration;

		// Token: 0x04001A7E RID: 6782
		private int slashCount;

		// Token: 0x04001A7F RID: 6783
		private Transform modelTransform;

		// Token: 0x04001A80 RID: 6784
		private bool dashComplete;

		// Token: 0x04001A81 RID: 6785
		private int handParamHash;

		// Token: 0x04001A82 RID: 6786
		private int swordParamHash;

		// Token: 0x04001A83 RID: 6787
		private float calculatedDashSpeed;

		// Token: 0x04001A84 RID: 6788
		private bool startedSlash;
	}
}
