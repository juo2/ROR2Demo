using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ImpMonster
{
	// Token: 0x0200030F RID: 783
	public class DoubleSlash : BaseState
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003BB7C File Offset: 0x00039D7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = DoubleSlash.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			base.characterMotor.walkSpeedPenaltyCoefficient = DoubleSlash.walkSpeedPenaltyCoefficient;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = DoubleSlash.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = DoubleSlash.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			this.attack.procCoefficient = DoubleSlash.procCoefficient;
			this.attack.damageType = DamageType.BleedOnHit;
			Util.PlayAttackSpeedSound(DoubleSlash.enterSoundString, base.gameObject, this.attackSpeedStat);
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture, Additive", "DoubleSlash", "DoubleSlash.playbackRate", this.duration);
				base.PlayAnimation("Gesture, Override", "DoubleSlash", "DoubleSlash.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration + 2f);
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003BCF7 File Offset: 0x00039EF7
		public override void OnExit()
		{
			base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			base.OnExit();
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003BD10 File Offset: 0x00039F10
		private void HandleSlash(string animatorParamName, string muzzleName, string hitBoxGroupName)
		{
			if (this.modelAnimator.GetFloat(animatorParamName) > 0.1f)
			{
				Util.PlaySound(DoubleSlash.slashSoundString, base.gameObject);
				EffectManager.SimpleMuzzleFlash(DoubleSlash.swipeEffectPrefab, base.gameObject, muzzleName, true);
				this.slashCount++;
				if (this.modelTransform)
				{
					this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitBoxGroupName);
				}
				if (base.healthComponent)
				{
					base.healthComponent.TakeDamageForce(base.characterDirection.forward * DoubleSlash.selfForce, true, false);
				}
				this.attack.ResetIgnoredHealthComponents();
				if (base.characterDirection)
				{
					this.attack.forceVector = base.characterDirection.forward * DoubleSlash.forceMagnitude;
				}
				this.attack.Fire(null);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003BE1C File Offset: 0x0003A01C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator)
			{
				switch (this.slashCount)
				{
				case 0:
					this.HandleSlash("HandR.hitBoxActive", "SwipeRight", "HandR");
					break;
				case 1:
					this.HandleSlash("HandL.hitBoxActive", "SwipeLeft", "HandL");
					break;
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400115B RID: 4443
		public static float baseDuration = 3.5f;

		// Token: 0x0400115C RID: 4444
		public static float damageCoefficient = 4f;

		// Token: 0x0400115D RID: 4445
		public static float procCoefficient;

		// Token: 0x0400115E RID: 4446
		public static float selfForce;

		// Token: 0x0400115F RID: 4447
		public static float forceMagnitude = 16f;

		// Token: 0x04001160 RID: 4448
		public static GameObject hitEffectPrefab;

		// Token: 0x04001161 RID: 4449
		public static GameObject swipeEffectPrefab;

		// Token: 0x04001162 RID: 4450
		public static string enterSoundString;

		// Token: 0x04001163 RID: 4451
		public static string slashSoundString;

		// Token: 0x04001164 RID: 4452
		public static float walkSpeedPenaltyCoefficient;

		// Token: 0x04001165 RID: 4453
		private OverlapAttack attack;

		// Token: 0x04001166 RID: 4454
		private Animator modelAnimator;

		// Token: 0x04001167 RID: 4455
		private float duration;

		// Token: 0x04001168 RID: 4456
		private int slashCount;

		// Token: 0x04001169 RID: 4457
		private Transform modelTransform;
	}
}
