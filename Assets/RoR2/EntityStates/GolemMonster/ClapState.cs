using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GolemMonster
{
	// Token: 0x0200036A RID: 874
	public class ClapState : BaseState
	{
		// Token: 0x06000FB9 RID: 4025 RVA: 0x00045834 File Offset: 0x00043A34
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(ClapState.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Body", "Clap", "Clap.playbackRate", ClapState.duration, 0.1f);
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/GolemClapCharge");
					Transform transform = component.FindChild("HandL");
					Transform transform2 = component.FindChild("HandR");
					if (transform)
					{
						this.leftHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform);
					}
					if (transform2)
					{
						this.rightHandChargeEffect = UnityEngine.Object.Instantiate<GameObject>(original, transform2);
					}
				}
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00045903 File Offset: 0x00043B03
		public override void OnExit()
		{
			EntityState.Destroy(this.leftHandChargeEffect);
			EntityState.Destroy(this.rightHandChargeEffect);
			base.OnExit();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00045924 File Offset: 0x00043B24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("Clap.hitBoxActive") > 0.5f && !this.hasAttacked)
			{
				if (base.isAuthority && this.modelTransform)
				{
					ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						Transform transform = component.FindChild("ClapZone");
						if (transform)
						{
							this.attack = new BlastAttack();
							this.attack.attacker = base.gameObject;
							this.attack.inflictor = base.gameObject;
							this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
							this.attack.baseDamage = this.damageStat * ClapState.damageCoefficient;
							this.attack.baseForce = ClapState.forceMagnitude;
							this.attack.position = transform.position;
							this.attack.radius = ClapState.radius;
							this.attack.attackerFiltering = AttackerFiltering.NeverHitSelf;
							this.attack.Fire();
						}
					}
				}
				this.hasAttacked = true;
				EntityState.Destroy(this.leftHandChargeEffect);
				EntityState.Destroy(this.rightHandChargeEffect);
			}
			if (base.fixedAge >= ClapState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001415 RID: 5141
		public static float duration = 3.5f;

		// Token: 0x04001416 RID: 5142
		public static float damageCoefficient = 4f;

		// Token: 0x04001417 RID: 5143
		public static float forceMagnitude = 16f;

		// Token: 0x04001418 RID: 5144
		public static float radius = 3f;

		// Token: 0x04001419 RID: 5145
		private BlastAttack attack;

		// Token: 0x0400141A RID: 5146
		public static string attackSoundString;

		// Token: 0x0400141B RID: 5147
		private Animator modelAnimator;

		// Token: 0x0400141C RID: 5148
		private Transform modelTransform;

		// Token: 0x0400141D RID: 5149
		private bool hasAttacked;

		// Token: 0x0400141E RID: 5150
		private GameObject leftHandChargeEffect;

		// Token: 0x0400141F RID: 5151
		private GameObject rightHandChargeEffect;
	}
}
