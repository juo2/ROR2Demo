using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000276 RID: 630
	public class Assaulter2 : BasicMeleeAttack
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002D4EA File Offset: 0x0002B6EA
		private Vector3 dashVelocity
		{
			get
			{
				return this.dashVector * this.moveSpeedStat * Assaulter2.speedCoefficient;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002D508 File Offset: 0x0002B708
		public override void OnEnter()
		{
			base.OnEnter();
			this.dashVector = base.inputBank.aimDirection;
			base.gameObject.layer = LayerIndex.fakeActor.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			base.characterMotor.Motor.ForceUnground();
			base.characterMotor.velocity = Vector3.zero;
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 0.7f;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEnergized");
				temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
			}
			base.PlayCrossfade("FullBody, Override", "AssaulterLoop", 0.1f);
			base.characterDirection.forward = base.characterMotor.velocity.normalized;
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002D63C File Offset: 0x0002B83C
		public override void OnExit()
		{
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			base.characterMotor.velocity *= Assaulter2.speedCoefficientOnExit;
			base.SmallHop(base.characterMotor, Assaulter2.exitSmallHop);
			Util.PlaySound(Assaulter2.endSoundString, base.gameObject);
			this.PlayAnimation("FullBody, Override", "EvisLoopExit");
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			base.OnExit();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002D6D8 File Offset: 0x0002B8D8
		protected override void PlayAnimation()
		{
			base.PlayAnimation();
			base.PlayCrossfade("FullBody, Override", "AssaulterLoop", 0.1f);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002D6F8 File Offset: 0x0002B8F8
		protected override void AuthorityFixedUpdate()
		{
			base.AuthorityFixedUpdate();
			if (!base.authorityInHitPause)
			{
				base.characterMotor.rootMotion += this.dashVelocity * Time.fixedDeltaTime;
				base.characterDirection.forward = this.dashVelocity;
				base.characterDirection.moveVector = this.dashVelocity;
				base.characterBody.isSprinting = true;
				if (this.bufferedSkill2)
				{
					base.skillLocator.secondary.ExecuteIfReady();
					this.bufferedSkill2 = false;
				}
			}
			if (base.skillLocator && base.skillLocator.secondary.IsReady() && base.inputBank.skill2.down)
			{
				this.bufferedSkill2 = true;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002D7BF File Offset: 0x0002B9BF
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			overlapAttack.damageType = DamageType.Stun1s;
			overlapAttack.damage = this.damageCoefficient * this.damageStat;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			this.grantAnotherDash = true;
			float num = this.hitPauseDuration / this.attackSpeedStat;
			if (Assaulter2.selfOnHitOverlayEffectPrefab && num > 0.033333335f)
			{
				EffectData effectData = new EffectData
				{
					origin = base.transform.position,
					genericFloat = this.hitPauseDuration / this.attackSpeedStat
				};
				effectData.SetNetworkedObjectReference(base.gameObject);
				EffectManager.SpawnEffect(Assaulter2.selfOnHitOverlayEffectPrefab, effectData, true);
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000CB2 RID: 3250
		public static float speedCoefficientOnExit;

		// Token: 0x04000CB3 RID: 3251
		public static float speedCoefficient;

		// Token: 0x04000CB4 RID: 3252
		public static string endSoundString;

		// Token: 0x04000CB5 RID: 3253
		public static float exitSmallHop;

		// Token: 0x04000CB6 RID: 3254
		public static GameObject selfOnHitOverlayEffectPrefab;

		// Token: 0x04000CB7 RID: 3255
		public bool grantAnotherDash;

		// Token: 0x04000CB8 RID: 3256
		private Transform modelTransform;

		// Token: 0x04000CB9 RID: 3257
		private Vector3 dashVector;

		// Token: 0x04000CBA RID: 3258
		private bool bufferedSkill2;
	}
}
