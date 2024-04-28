using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.HAND.Weapon
{
	// Token: 0x02000335 RID: 821
	public class FullSwing : BaseState
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003FB1C File Offset: 0x0003DD1C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FullSwing.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = FullSwing.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = FullSwing.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Hammer");
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.hammerChildTransform = component.FindChild("SwingCenter");
				}
			}
			if (this.modelAnimator)
			{
				int layerIndex = this.modelAnimator.GetLayerIndex("Gesture");
				if (this.modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("FullSwing3") || this.modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("FullSwing1"))
				{
					base.PlayCrossfade("Gesture", "FullSwing2", "FullSwing.playbackRate", this.duration / (1f - FullSwing.returnToIdlePercentage), 0.2f);
				}
				else if (this.modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).IsName("FullSwing2"))
				{
					base.PlayCrossfade("Gesture", "FullSwing3", "FullSwing.playbackRate", this.duration / (1f - FullSwing.returnToIdlePercentage), 0.2f);
				}
				else
				{
					base.PlayCrossfade("Gesture", "FullSwing1", "FullSwing.playbackRate", this.duration / (1f - FullSwing.returnToIdlePercentage), 0.2f);
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003FD58 File Offset: 0x0003DF58
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Hammer.hitBoxActive") > 0.5f)
			{
				if (!this.hasSwung)
				{
					EffectManager.SimpleMuzzleFlash(FullSwing.swingEffectPrefab, base.gameObject, "SwingCenter", true);
					this.hasSwung = true;
				}
				this.attack.forceVector = this.hammerChildTransform.right * -FullSwing.forceMagnitude;
				this.attack.Fire(null);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003FE0C File Offset: 0x0003E00C
		private static void PullEnemies(Vector3 position, Vector3 direction, float coneAngle, float maxDistance, float force, TeamIndex excludedTeam)
		{
			float num = Mathf.Cos(coneAngle * 0.5f * 0.017453292f);
			foreach (Collider collider in Physics.OverlapSphere(position, maxDistance))
			{
				Vector3 position2 = collider.transform.position;
				Vector3 normalized = (position - position2).normalized;
				if (Vector3.Dot(-normalized, direction) >= num)
				{
					TeamComponent component = collider.GetComponent<TeamComponent>();
					if (component)
					{
						TeamIndex teamIndex = component.teamIndex;
						if (teamIndex != excludedTeam)
						{
							CharacterMotor component2 = collider.GetComponent<CharacterMotor>();
							if (component2)
							{
								component2.ApplyForce(normalized * force, false, false);
							}
							Rigidbody component3 = collider.GetComponent<Rigidbody>();
							if (component3)
							{
								component3.AddForce(normalized * force, ForceMode.Impulse);
							}
						}
					}
				}
			}
		}

		// Token: 0x04001279 RID: 4729
		public static float baseDuration = 3.5f;

		// Token: 0x0400127A RID: 4730
		public static float returnToIdlePercentage;

		// Token: 0x0400127B RID: 4731
		public static float damageCoefficient = 4f;

		// Token: 0x0400127C RID: 4732
		public static float forceMagnitude = 16f;

		// Token: 0x0400127D RID: 4733
		public static float radius = 3f;

		// Token: 0x0400127E RID: 4734
		public static GameObject hitEffectPrefab;

		// Token: 0x0400127F RID: 4735
		public static GameObject swingEffectPrefab;

		// Token: 0x04001280 RID: 4736
		private Transform hammerChildTransform;

		// Token: 0x04001281 RID: 4737
		private OverlapAttack attack;

		// Token: 0x04001282 RID: 4738
		private Animator modelAnimator;

		// Token: 0x04001283 RID: 4739
		private float duration;

		// Token: 0x04001284 RID: 4740
		private bool hasSwung;
	}
}
