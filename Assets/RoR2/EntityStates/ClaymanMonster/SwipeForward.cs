using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ClaymanMonster
{
	// Token: 0x020003FA RID: 1018
	public class SwipeForward : BaseState
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x000516A8 File Offset: 0x0004F8A8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = SwipeForward.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = SwipeForward.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = SwipeForward.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			Util.PlaySound(SwipeForward.attackString, base.gameObject);
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Sword");
			}
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture, Override", "SwipeForward", "SwipeForward.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Additive", "SwipeForward", "SwipeForward.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00051820 File Offset: 0x0004FA20
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("SwipeForward.hitBoxActive") > 0.1f)
			{
				if (!this.hasSlashed)
				{
					EffectManager.SimpleMuzzleFlash(SwipeForward.swingEffectPrefab, base.gameObject, "SwingCenter", true);
					HealthComponent healthComponent = base.characterBody.healthComponent;
					CharacterDirection component = base.characterBody.GetComponent<CharacterDirection>();
					if (healthComponent)
					{
						healthComponent.TakeDamageForce(SwipeForward.selfForceMagnitude * component.forward, true, false);
					}
					this.hasSlashed = true;
				}
				this.attack.forceVector = base.transform.forward * SwipeForward.forceMagnitude;
				this.attack.Fire(null);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400176B RID: 5995
		public static float baseDuration = 3.5f;

		// Token: 0x0400176C RID: 5996
		public static float damageCoefficient = 4f;

		// Token: 0x0400176D RID: 5997
		public static float forceMagnitude = 16f;

		// Token: 0x0400176E RID: 5998
		public static float selfForceMagnitude;

		// Token: 0x0400176F RID: 5999
		public static float radius = 3f;

		// Token: 0x04001770 RID: 6000
		public static GameObject hitEffectPrefab;

		// Token: 0x04001771 RID: 6001
		public static GameObject swingEffectPrefab;

		// Token: 0x04001772 RID: 6002
		public static string attackString;

		// Token: 0x04001773 RID: 6003
		private OverlapAttack attack;

		// Token: 0x04001774 RID: 6004
		private Animator modelAnimator;

		// Token: 0x04001775 RID: 6005
		private float duration;

		// Token: 0x04001776 RID: 6006
		private bool hasSlashed;
	}
}
