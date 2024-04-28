using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x0200019C RID: 412
	public class ToolbotDash : BaseCharacterMain
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0001F710 File Offset: 0x0001D910
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration;
			if (base.isAuthority)
			{
				if (base.inputBank)
				{
					this.idealDirection = base.inputBank.aimDirection;
					this.idealDirection.y = 0f;
				}
				this.UpdateDirection();
			}
			if (base.modelLocator)
			{
				base.modelLocator.normalizeToFloor = true;
			}
			if (this.startEffectPrefab && base.characterBody)
			{
				EffectManager.SpawnEffect(this.startEffectPrefab, new EffectData
				{
					origin = base.characterBody.corePosition
				}, false);
			}
			if (base.characterDirection)
			{
				base.characterDirection.forward = this.idealDirection;
			}
			this.soundID = Util.PlaySound(ToolbotDash.startSoundString, base.gameObject);
			base.PlayCrossfade("Body", "BoxModeEnter", 0.1f);
			base.PlayCrossfade("Stance, Override", "PutAwayGun", 0.1f);
			base.modelAnimator.SetFloat("aimWeight", 0f);
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.ArmorBoost);
			}
			HitBoxGroup hitBoxGroup = null;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Charge");
			}
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = base.GetTeam();
			this.attack.damage = ToolbotDash.chargeDamageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = ToolbotDash.impactEffectPrefab;
			this.attack.forceVector = Vector3.up * ToolbotDash.upwardForceMagnitude;
			this.attack.pushAwayForce = ToolbotDash.awayForceMagnitude;
			this.attack.hitBoxGroup = hitBoxGroup;
			this.attack.isCrit = base.RollCrit();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001F934 File Offset: 0x0001DB34
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.soundID);
			Util.PlaySound(ToolbotDash.endSoundString, base.gameObject);
			if (!this.outer.destroying && base.characterBody)
			{
				if (this.endEffectPrefab)
				{
					EffectManager.SpawnEffect(this.endEffectPrefab, new EffectData
					{
						origin = base.characterBody.corePosition
					}, false);
				}
				this.PlayAnimation("Body", "BoxModeExit");
				base.PlayCrossfade("Stance, Override", "Empty", 0.1f);
				base.characterBody.isSprinting = false;
				if (NetworkServer.active)
				{
					base.characterBody.RemoveBuff(RoR2Content.Buffs.ArmorBoost);
				}
			}
			if (base.characterMotor && !base.characterMotor.disableAirControlUntilCollision)
			{
				base.characterMotor.velocity += this.GetIdealVelocity();
			}
			if (base.modelLocator)
			{
				base.modelLocator.normalizeToFloor = false;
			}
			base.modelAnimator.SetFloat("aimWeight", 1f);
			base.OnExit();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001FA5B File Offset: 0x0001DC5B
		private float GetDamageBoostFromSpeed()
		{
			return Mathf.Max(1f, base.characterBody.moveSpeed / base.characterBody.baseMoveSpeed);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001FA80 File Offset: 0x0001DC80
		private void UpdateDirection()
		{
			if (base.inputBank)
			{
				Vector2 vector = Util.Vector3XZToVector2XY(base.inputBank.moveVector);
				if (vector != Vector2.zero)
				{
					vector.Normalize();
					this.idealDirection = new Vector3(vector.x, 0f, vector.y).normalized;
				}
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001FAE3 File Offset: 0x0001DCE3
		private Vector3 GetIdealVelocity()
		{
			return base.characterDirection.forward * base.characterBody.moveSpeed * this.speedMultiplier;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001FB0C File Offset: 0x0001DD0C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
			if (base.isAuthority)
			{
				if (base.characterBody)
				{
					base.characterBody.isSprinting = true;
				}
				if (base.skillLocator.special && base.inputBank.skill4.down)
				{
					base.skillLocator.special.ExecuteIfReady();
				}
				this.UpdateDirection();
				if (!this.inHitPause)
				{
					if (base.characterDirection)
					{
						base.characterDirection.moveVector = this.idealDirection;
						if (base.characterMotor && !base.characterMotor.disableAirControlUntilCollision)
						{
							base.characterMotor.rootMotion += this.GetIdealVelocity() * Time.fixedDeltaTime;
						}
					}
					this.attack.damage = this.damageStat * (ToolbotDash.chargeDamageCoefficient * this.GetDamageBoostFromSpeed());
					if (this.attack.Fire(this.victimsStruck))
					{
						Util.PlaySound(ToolbotDash.impactSoundString, base.gameObject);
						this.inHitPause = true;
						this.hitPauseTimer = ToolbotDash.hitPauseDuration;
						base.AddRecoil(-0.5f * ToolbotDash.recoilAmplitude, -0.5f * ToolbotDash.recoilAmplitude, -0.5f * ToolbotDash.recoilAmplitude, 0.5f * ToolbotDash.recoilAmplitude);
						base.PlayAnimation("Gesture, Additive", "BoxModeImpact", "BoxModeImpact.playbackRate", ToolbotDash.hitPauseDuration);
						for (int i = 0; i < this.victimsStruck.Count; i++)
						{
							float num = 0f;
							HurtBox hurtBox = this.victimsStruck[i];
							if (hurtBox.healthComponent)
							{
								CharacterMotor component = hurtBox.healthComponent.GetComponent<CharacterMotor>();
								if (component)
								{
									num = component.mass;
								}
								else
								{
									Rigidbody component2 = hurtBox.healthComponent.GetComponent<Rigidbody>();
									if (component2)
									{
										num = component2.mass;
									}
								}
								if (num >= ToolbotDash.massThresholdForKnockback)
								{
									this.outer.SetNextState(new ToolbotDashImpact
									{
										victimHealthComponent = hurtBox.healthComponent,
										idealDirection = this.idealDirection,
										damageBoostFromSpeed = this.GetDamageBoostFromSpeed(),
										isCrit = this.attack.isCrit
									});
									return;
								}
							}
						}
						return;
					}
				}
				else
				{
					base.characterMotor.velocity = Vector3.zero;
					this.hitPauseTimer -= Time.fixedDeltaTime;
					if (this.hitPauseTimer < 0f)
					{
						this.inHitPause = false;
					}
				}
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x040008EC RID: 2284
		[SerializeField]
		public float baseDuration;

		// Token: 0x040008ED RID: 2285
		[SerializeField]
		public float speedMultiplier;

		// Token: 0x040008EE RID: 2286
		public static float chargeDamageCoefficient;

		// Token: 0x040008EF RID: 2287
		public static float awayForceMagnitude;

		// Token: 0x040008F0 RID: 2288
		public static float upwardForceMagnitude;

		// Token: 0x040008F1 RID: 2289
		public static GameObject impactEffectPrefab;

		// Token: 0x040008F2 RID: 2290
		public static float hitPauseDuration;

		// Token: 0x040008F3 RID: 2291
		public static string impactSoundString;

		// Token: 0x040008F4 RID: 2292
		public static float recoilAmplitude;

		// Token: 0x040008F5 RID: 2293
		public static string startSoundString;

		// Token: 0x040008F6 RID: 2294
		public static string endSoundString;

		// Token: 0x040008F7 RID: 2295
		public static GameObject knockbackEffectPrefab;

		// Token: 0x040008F8 RID: 2296
		public static float knockbackDamageCoefficient;

		// Token: 0x040008F9 RID: 2297
		public static float massThresholdForKnockback;

		// Token: 0x040008FA RID: 2298
		public static float knockbackForce;

		// Token: 0x040008FB RID: 2299
		[SerializeField]
		public GameObject startEffectPrefab;

		// Token: 0x040008FC RID: 2300
		[SerializeField]
		public GameObject endEffectPrefab;

		// Token: 0x040008FD RID: 2301
		private uint soundID;

		// Token: 0x040008FE RID: 2302
		private float duration;

		// Token: 0x040008FF RID: 2303
		private float hitPauseTimer;

		// Token: 0x04000900 RID: 2304
		private Vector3 idealDirection;

		// Token: 0x04000901 RID: 2305
		private OverlapAttack attack;

		// Token: 0x04000902 RID: 2306
		private bool inHitPause;

		// Token: 0x04000903 RID: 2307
		private List<HurtBox> victimsStruck = new List<HurtBox>();
	}
}
