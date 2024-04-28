using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x02000174 RID: 372
	public class BurrowDash : BaseCharacterMain
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x0001BCB0 File Offset: 0x00019EB0
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
			Util.PlaySound(BurrowDash.startSoundString, base.gameObject);
			base.PlayCrossfade("Body", "BurrowEnter", 0.1f);
			HitBoxGroup hitBoxGroup = null;
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "BurrowCharge");
				this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			}
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = BurrowDash.chargeDamageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = BurrowDash.impactEffectPrefab;
			this.attack.hitBoxGroup = hitBoxGroup;
			this.attack.damage = this.damageStat * BurrowDash.chargeDamageCoefficient;
			this.attack.damageType = DamageType.Freeze2s;
			this.attack.procCoefficient = 1f;
			base.gameObject.layer = LayerIndex.debris.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			HurtBoxGroup component = this.modelTransform.GetComponent<HurtBoxGroup>();
			int hurtBoxesDeactivatorCounter = component.hurtBoxesDeactivatorCounter;
			component.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter + 1;
			base.characterBody.hideCrosshair = true;
			base.characterBody.isSprinting = true;
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild("BurrowCenter");
				if (transform && BurrowDash.burrowLoopEffectPrefab)
				{
					this.burrowLoopEffectInstance = UnityEngine.Object.Instantiate<GameObject>(BurrowDash.burrowLoopEffectPrefab, transform.position, transform.rotation);
					this.burrowLoopEffectInstance.transform.parent = transform;
				}
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001BF68 File Offset: 0x0001A168
		public override void OnExit()
		{
			if (base.characterBody && !this.outer.destroying && this.endEffectPrefab)
			{
				EffectManager.SpawnEffect(this.endEffectPrefab, new EffectData
				{
					origin = base.characterBody.corePosition
				}, false);
			}
			Util.PlaySound(BurrowDash.endSoundString, base.gameObject);
			base.gameObject.layer = LayerIndex.defaultLayer.intVal;
			base.characterMotor.Motor.RebuildCollidableLayers();
			HurtBoxGroup component = this.modelTransform.GetComponent<HurtBoxGroup>();
			int hurtBoxesDeactivatorCounter = component.hurtBoxesDeactivatorCounter;
			component.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter - 1;
			base.characterBody.hideCrosshair = false;
			if (this.burrowLoopEffectInstance)
			{
				EntityState.Destroy(this.burrowLoopEffectInstance);
			}
			Animator modelAnimator = base.GetModelAnimator();
			int layerIndex = modelAnimator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				modelAnimator.SetLayerWeight(layerIndex, 2f);
				modelAnimator.PlayInFixedTime("LightImpact", layerIndex, 0f);
			}
			base.OnExit();
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001C06C File Offset: 0x0001A26C
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

		// Token: 0x0600067E RID: 1662 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void UpdateAnimationParameters()
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C0CF File Offset: 0x0001A2CF
		private Vector3 GetIdealVelocity()
		{
			return base.characterDirection.forward * base.characterBody.moveSpeed * BurrowDash.speedMultiplier.Evaluate(base.fixedAge / this.duration);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C108 File Offset: 0x0001A308
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
			if (base.fixedAge >= this.duration - BurrowDash.timeBeforeExitToPlayExitAnimation && !this.beginPlayingExitAnimation)
			{
				this.beginPlayingExitAnimation = true;
				base.PlayCrossfade("Body", "BurrowExit", 0.1f);
			}
			if (base.isAuthority)
			{
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
					if (this.attack.Fire(null))
					{
						Util.PlaySound(BurrowDash.impactSoundString, base.gameObject);
						this.inHitPause = true;
						this.hitPauseTimer = BurrowDash.hitPauseDuration;
						if (BurrowDash.healPercent > 0f)
						{
							base.healthComponent.HealFraction(BurrowDash.healPercent, default(ProcChainMask));
							Util.PlaySound("Play_item_use_fruit", base.gameObject);
							EffectData effectData = new EffectData();
							effectData.origin = base.transform.position;
							effectData.SetNetworkedObjectReference(base.gameObject);
							EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/FruitHealEffect"), effectData, true);
						}
						if (BurrowDash.resetDurationOnImpact)
						{
							base.fixedAge = 0f;
							return;
						}
						base.fixedAge -= BurrowDash.hitPauseDuration;
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

		// Token: 0x06000681 RID: 1665 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040007E4 RID: 2020
		[SerializeField]
		public float baseDuration;

		// Token: 0x040007E5 RID: 2021
		[SerializeField]
		public static AnimationCurve speedMultiplier;

		// Token: 0x040007E6 RID: 2022
		public static float chargeDamageCoefficient;

		// Token: 0x040007E7 RID: 2023
		public static GameObject impactEffectPrefab;

		// Token: 0x040007E8 RID: 2024
		public static GameObject burrowLoopEffectPrefab;

		// Token: 0x040007E9 RID: 2025
		public static float hitPauseDuration;

		// Token: 0x040007EA RID: 2026
		public static float timeBeforeExitToPlayExitAnimation;

		// Token: 0x040007EB RID: 2027
		public static string impactSoundString;

		// Token: 0x040007EC RID: 2028
		public static string startSoundString;

		// Token: 0x040007ED RID: 2029
		public static string endSoundString;

		// Token: 0x040007EE RID: 2030
		public static float healPercent;

		// Token: 0x040007EF RID: 2031
		public static bool resetDurationOnImpact;

		// Token: 0x040007F0 RID: 2032
		[SerializeField]
		public GameObject startEffectPrefab;

		// Token: 0x040007F1 RID: 2033
		[SerializeField]
		public GameObject endEffectPrefab;

		// Token: 0x040007F2 RID: 2034
		private float duration;

		// Token: 0x040007F3 RID: 2035
		private float hitPauseTimer;

		// Token: 0x040007F4 RID: 2036
		private Vector3 idealDirection;

		// Token: 0x040007F5 RID: 2037
		private OverlapAttack attack;

		// Token: 0x040007F6 RID: 2038
		private ChildLocator childLocator;

		// Token: 0x040007F7 RID: 2039
		private bool inHitPause;

		// Token: 0x040007F8 RID: 2040
		private bool beginPlayingExitAnimation;

		// Token: 0x040007F9 RID: 2041
		private Transform modelTransform;

		// Token: 0x040007FA RID: 2042
		private GameObject burrowLoopEffectInstance;
	}
}
