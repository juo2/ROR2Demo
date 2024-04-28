using System;
using RoR2;
using UnityEngine;

namespace EntityStates.JellyfishMonster
{
	// Token: 0x020002EB RID: 747
	public class JellyNova : BaseState
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x000383C4 File Offset: 0x000365C4
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = JellyNova.baseDuration / this.attackSpeedStat;
			Transform modelTransform = base.GetModelTransform();
			base.PlayCrossfade("Body", "Nova", "Nova.playbackRate", this.duration, 0.1f);
			this.soundID = Util.PlaySound(JellyNova.chargingSoundString, base.gameObject);
			if (JellyNova.chargingEffectPrefab)
			{
				this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(JellyNova.chargingEffectPrefab, base.transform.position, base.transform.rotation);
				this.chargeEffect.transform.parent = base.transform;
				this.chargeEffect.transform.localScale = new Vector3(JellyNova.novaRadius, JellyNova.novaRadius, JellyNova.novaRadius);
				this.chargeEffect.GetComponent<ScaleParticleSystemDuration>().newDuration = this.duration;
			}
			if (modelTransform)
			{
				this.printController = modelTransform.GetComponent<PrintController>();
				if (this.printController)
				{
					this.printController.enabled = true;
					this.printController.printTime = this.duration;
				}
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000384F4 File Offset: 0x000366F4
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.soundID);
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			if (this.printController)
			{
				this.printController.enabled = false;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00038543 File Offset: 0x00036743
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority && !this.hasExploded)
			{
				this.Detonate();
				return;
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00038584 File Offset: 0x00036784
		private void Detonate()
		{
			this.hasExploded = true;
			Util.PlaySound(JellyNova.novaSoundString, base.gameObject);
			if (base.modelLocator)
			{
				if (base.modelLocator.modelBaseTransform)
				{
					EntityState.Destroy(base.modelLocator.modelBaseTransform.gameObject);
				}
				if (base.modelLocator.modelTransform)
				{
					EntityState.Destroy(base.modelLocator.modelTransform.gameObject);
				}
			}
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			if (JellyNova.novaEffectPrefab)
			{
				EffectManager.SpawnEffect(JellyNova.novaEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = JellyNova.novaRadius
				}, true);
			}
			new BlastAttack
			{
				attacker = base.gameObject,
				inflictor = base.gameObject,
				teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
				baseDamage = this.damageStat * JellyNova.novaDamageCoefficient,
				baseForce = JellyNova.novaForce,
				position = base.transform.position,
				radius = JellyNova.novaRadius,
				procCoefficient = 2f,
				attackerFiltering = AttackerFiltering.NeverHitSelf
			}.Fire();
			if (base.healthComponent)
			{
				base.healthComponent.Suicide(null, null, DamageType.Generic);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x04001054 RID: 4180
		public static float baseDuration = 3f;

		// Token: 0x04001055 RID: 4181
		public static GameObject chargingEffectPrefab;

		// Token: 0x04001056 RID: 4182
		public static GameObject novaEffectPrefab;

		// Token: 0x04001057 RID: 4183
		public static string chargingSoundString;

		// Token: 0x04001058 RID: 4184
		public static string novaSoundString;

		// Token: 0x04001059 RID: 4185
		public static float novaDamageCoefficient;

		// Token: 0x0400105A RID: 4186
		public static float novaRadius;

		// Token: 0x0400105B RID: 4187
		public static float novaForce;

		// Token: 0x0400105C RID: 4188
		private bool hasExploded;

		// Token: 0x0400105D RID: 4189
		private float duration;

		// Token: 0x0400105E RID: 4190
		private float stopwatch;

		// Token: 0x0400105F RID: 4191
		private GameObject chargeEffect;

		// Token: 0x04001060 RID: 4192
		private PrintController printController;

		// Token: 0x04001061 RID: 4193
		private uint soundID;
	}
}
