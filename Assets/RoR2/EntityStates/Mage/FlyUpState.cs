using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Mage
{
	// Token: 0x02000295 RID: 661
	public class FlyUpState : MageCharacterMain
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00030798 File Offset: 0x0002E998
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(FlyUpState.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			this.flyVector = Vector3.up;
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			base.PlayCrossfade("Body", "FlyUp", "FlyUp.playbackRate", FlyUpState.duration, 0.1f);
			base.characterMotor.Motor.ForceUnground();
			base.characterMotor.velocity = Vector3.zero;
			EffectManager.SimpleMuzzleFlash(FlyUpState.muzzleflashEffect, base.gameObject, "MuzzleLeft", false);
			EffectManager.SimpleMuzzleFlash(FlyUpState.muzzleflashEffect, base.gameObject, "MuzzleRight", false);
			if (base.isAuthority)
			{
				this.blastPosition = base.characterBody.corePosition;
			}
			if (NetworkServer.active)
			{
				BlastAttack blastAttack = new BlastAttack();
				blastAttack.radius = FlyUpState.blastAttackRadius;
				blastAttack.procCoefficient = FlyUpState.blastAttackProcCoefficient;
				blastAttack.position = this.blastPosition;
				blastAttack.attacker = base.gameObject;
				blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
				blastAttack.baseDamage = base.characterBody.damage * FlyUpState.blastAttackDamageCoefficient;
				blastAttack.falloffModel = BlastAttack.FalloffModel.None;
				blastAttack.baseForce = FlyUpState.blastAttackForce;
				blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
				blastAttack.damageType = DamageType.Stun1s;
				blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
				blastAttack.Fire();
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00030915 File Offset: 0x0002EB15
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.blastPosition);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0003092A File Offset: 0x0002EB2A
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.blastPosition = reader.ReadVector3();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00030940 File Offset: 0x0002EB40
		public override void HandleMovements()
		{
			base.HandleMovements();
			base.characterMotor.rootMotion += this.flyVector * (this.moveSpeedStat * FlyUpState.speedCoefficientCurve.Evaluate(base.fixedAge / FlyUpState.duration) * Time.fixedDeltaTime);
			base.characterMotor.velocity.y = 0f;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000309AC File Offset: 0x0002EBAC
		protected override void UpdateAnimationParameters()
		{
			base.UpdateAnimationParameters();
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000309B4 File Offset: 0x0002EBB4
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.flyVector);
			effectData.origin = origin;
			EffectManager.SpawnEffect(FlyUpState.blinkPrefab, effectData, false);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000309EB File Offset: 0x0002EBEB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= FlyUpState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00030A13 File Offset: 0x0002EC13
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				Util.PlaySound(FlyUpState.endSoundString, base.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000DD3 RID: 3539
		public static GameObject blinkPrefab;

		// Token: 0x04000DD4 RID: 3540
		public static float duration = 0.3f;

		// Token: 0x04000DD5 RID: 3541
		public static string beginSoundString;

		// Token: 0x04000DD6 RID: 3542
		public static string endSoundString;

		// Token: 0x04000DD7 RID: 3543
		public static AnimationCurve speedCoefficientCurve;

		// Token: 0x04000DD8 RID: 3544
		public static GameObject muzzleflashEffect;

		// Token: 0x04000DD9 RID: 3545
		public static float blastAttackRadius;

		// Token: 0x04000DDA RID: 3546
		public static float blastAttackDamageCoefficient;

		// Token: 0x04000DDB RID: 3547
		public static float blastAttackProcCoefficient;

		// Token: 0x04000DDC RID: 3548
		public static float blastAttackForce;

		// Token: 0x04000DDD RID: 3549
		private Vector3 flyVector = Vector3.zero;

		// Token: 0x04000DDE RID: 3550
		private Transform modelTransform;

		// Token: 0x04000DDF RID: 3551
		private CharacterModel characterModel;

		// Token: 0x04000DE0 RID: 3552
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04000DE1 RID: 3553
		private Vector3 blastPosition;
	}
}
