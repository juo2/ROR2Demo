using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Assassin.Weapon
{
	// Token: 0x0200048C RID: 1164
	public class SlashCombo : BaseState
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0005C784 File Offset: 0x0005A984
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = SlashCombo.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = SlashCombo.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = SlashCombo.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			Util.PlaySound(SlashCombo.attackString, base.gameObject);
			string hitboxGroupName = "";
			string animationStateName = "";
			switch (this.slashComboPermutation)
			{
			case SlashCombo.SlashComboPermutation.Slash1:
				hitboxGroupName = "DaggerLeft";
				animationStateName = "SlashP1";
				break;
			case SlashCombo.SlashComboPermutation.Slash2:
				hitboxGroupName = "DaggerLeft";
				animationStateName = "SlashP2";
				break;
			case SlashCombo.SlashComboPermutation.Final:
				hitboxGroupName = "DaggerLeft";
				animationStateName = "SlashP3";
				break;
			}
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitboxGroupName);
			}
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture, Override", animationStateName, "SlashCombo.playbackRate", this.duration * SlashCombo.mecanimDurationCoefficient);
				base.PlayAnimation("Gesture, Additive", animationStateName, "SlashCombo.playbackRate", this.duration * SlashCombo.mecanimDurationCoefficient);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0005C954 File Offset: 0x0005AB54
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("SlashCombo.hitBoxActive") > 0.1f)
			{
				if (!this.hasSlashed)
				{
					EffectManager.SimpleMuzzleFlash(SlashCombo.swingEffectPrefab, base.gameObject, "SwingCenter", true);
					HealthComponent healthComponent = base.characterBody.healthComponent;
					CharacterDirection component = base.characterBody.GetComponent<CharacterDirection>();
					if (healthComponent)
					{
						healthComponent.TakeDamageForce(SlashCombo.selfForceMagnitude * component.forward, true, false);
					}
					this.hasSlashed = true;
				}
				this.attack.forceVector = base.transform.forward * SlashCombo.forceMagnitude;
				this.attack.Fire(null);
			}
			if (base.fixedAge < this.duration || !base.isAuthority)
			{
				return;
			}
			if (base.inputBank && base.inputBank.skill1.down)
			{
				SlashCombo slashCombo = new SlashCombo();
				switch (this.slashComboPermutation)
				{
				case SlashCombo.SlashComboPermutation.Slash1:
					slashCombo.slashComboPermutation = SlashCombo.SlashComboPermutation.Slash2;
					break;
				case SlashCombo.SlashComboPermutation.Slash2:
					slashCombo.slashComboPermutation = SlashCombo.SlashComboPermutation.Slash1;
					break;
				}
				this.outer.SetNextState(slashCombo);
				return;
			}
			this.outer.SetNextStateToMain();
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001AA2 RID: 6818
		public static float baseDuration = 3.5f;

		// Token: 0x04001AA3 RID: 6819
		public static float mecanimDurationCoefficient;

		// Token: 0x04001AA4 RID: 6820
		public static float damageCoefficient = 4f;

		// Token: 0x04001AA5 RID: 6821
		public static float forceMagnitude = 16f;

		// Token: 0x04001AA6 RID: 6822
		public static float selfForceMagnitude;

		// Token: 0x04001AA7 RID: 6823
		public static float radius = 3f;

		// Token: 0x04001AA8 RID: 6824
		public static GameObject hitEffectPrefab;

		// Token: 0x04001AA9 RID: 6825
		public static GameObject swingEffectPrefab;

		// Token: 0x04001AAA RID: 6826
		public static string attackString;

		// Token: 0x04001AAB RID: 6827
		private OverlapAttack attack;

		// Token: 0x04001AAC RID: 6828
		private Animator modelAnimator;

		// Token: 0x04001AAD RID: 6829
		private float duration;

		// Token: 0x04001AAE RID: 6830
		private bool hasSlashed;

		// Token: 0x04001AAF RID: 6831
		public SlashCombo.SlashComboPermutation slashComboPermutation;

		// Token: 0x0200048D RID: 1165
		public enum SlashComboPermutation
		{
			// Token: 0x04001AB1 RID: 6833
			Slash1,
			// Token: 0x04001AB2 RID: 6834
			Slash2,
			// Token: 0x04001AB3 RID: 6835
			Final
		}
	}
}
