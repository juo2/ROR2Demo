using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LemurianMonster
{
	// Token: 0x020002D2 RID: 722
	public class Bite : BaseState
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x000362B0 File Offset: 0x000344B0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Bite.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = Bite.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = Bite.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			Util.PlayAttackSpeedSound(Bite.attackString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Bite");
			}
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "Bite", "Bite.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00036414 File Offset: 0x00034614
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Bite.hitBoxActive") > 0.1f)
			{
				if (!this.hasBit)
				{
					EffectManager.SimpleMuzzleFlash(Bite.biteEffectPrefab, base.gameObject, "MuzzleMouth", true);
					this.hasBit = true;
				}
				this.attack.forceVector = base.transform.forward * Bite.forceMagnitude;
				this.attack.Fire(null);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000FBB RID: 4027
		public static float baseDuration = 3.5f;

		// Token: 0x04000FBC RID: 4028
		public static float damageCoefficient = 4f;

		// Token: 0x04000FBD RID: 4029
		public static float forceMagnitude = 16f;

		// Token: 0x04000FBE RID: 4030
		public static float radius = 3f;

		// Token: 0x04000FBF RID: 4031
		public static GameObject hitEffectPrefab;

		// Token: 0x04000FC0 RID: 4032
		public static GameObject biteEffectPrefab;

		// Token: 0x04000FC1 RID: 4033
		public static string attackString;

		// Token: 0x04000FC2 RID: 4034
		private OverlapAttack attack;

		// Token: 0x04000FC3 RID: 4035
		private Animator modelAnimator;

		// Token: 0x04000FC4 RID: 4036
		private float duration;

		// Token: 0x04000FC5 RID: 4037
		private bool hasBit;
	}
}
