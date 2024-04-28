using System;
using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace EntityStates.SiphonItem
{
	// Token: 0x020001CC RID: 460
	public class DetonateState : BaseSiphonItemState
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00022EFC File Offset: 0x000210FC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = DetonateState.baseDuration;
			base.GetItemStack();
			Vector3 position = base.attachedBody.transform.position;
			SphereSearch sphereSearch = new SphereSearch();
			sphereSearch.origin = position;
			sphereSearch.radius = DetonateState.baseSiphonRange;
			sphereSearch.mask = LayerIndex.entityPrecise.mask;
			float num = base.attachedBody.healthComponent.fullCombinedHealth * DetonateState.damageFraction;
			TeamMask mask = default(TeamMask);
			mask.AddTeam(base.attachedBody.teamComponent.teamIndex);
			foreach (HurtBox hurtBox in sphereSearch.RefreshCandidates().FilterCandidatesByHurtBoxTeam(mask).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
			{
				if (hurtBox.healthComponent != base.attachedBody.healthComponent)
				{
					if (!this.burstPlayed)
					{
						this.burstPlayed = true;
						Util.PlaySound(DetonateState.explosionSound, base.gameObject);
						Util.PlaySound(DetonateState.siphonLoopSound, base.gameObject);
						EffectData effectData = new EffectData
						{
							scale = 1f,
							origin = position
						};
						effectData.SetHurtBoxReference(base.attachedBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Base").gameObject);
						EffectManager.SpawnEffect(DetonateState.burstEffectPrefab, effectData, false);
					}
					DamageInfo damageInfo = new DamageInfo
					{
						attacker = base.attachedBody.gameObject,
						inflictor = base.attachedBody.gameObject,
						crit = false,
						damage = num,
						damageColorIndex = DamageColorIndex.Default,
						damageType = DamageType.Generic,
						force = Vector3.zero,
						position = hurtBox.transform.position
					};
					hurtBox.healthComponent.TakeDamage(damageInfo);
					HurtBox hurtBoxReference = hurtBox;
					HurtBoxGroup hurtBoxGroup = hurtBox.hurtBoxGroup;
					int num2 = 0;
					while ((float)num2 < Mathf.Min(4f, base.attachedBody.radius * 2f))
					{
						EffectData effectData2 = new EffectData
						{
							scale = 1f,
							origin = position,
							genericFloat = 3f
						};
						effectData2.SetHurtBoxReference(hurtBoxReference);
						GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/SiphonOrbEffect");
						gameObject.GetComponent<OrbEffect>().parentObjectTransform = base.attachedBody.mainHurtBox.transform;
						EffectManager.SpawnEffect(gameObject, effectData2, true);
						hurtBoxReference = hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, hurtBoxGroup.hurtBoxes.Length)];
						num2++;
					}
					this.gainedHealth += num;
				}
			}
			this.gainedHealthFraction = this.gainedHealth * DetonateState.healPulseFraction;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000231AC File Offset: 0x000213AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.healTimer += Time.fixedDeltaTime;
			if (base.isAuthority && this.healTimer > this.duration * DetonateState.healPulseFraction && this.gainedHealth > 0f)
			{
				base.attachedBody.healthComponent.Heal(this.gainedHealthFraction, default(ProcChainMask), true);
				base.TurnOnHealingFX();
				this.healTimer = 0f;
				if (base.fixedAge >= this.duration)
				{
					Util.PlaySound(DetonateState.retractSound, base.gameObject);
					base.TurnOffHealingFX();
					this.outer.SetNextState(new RechargeState());
				}
			}
		}

		// Token: 0x040009AF RID: 2479
		public static float baseSiphonRange = 50f;

		// Token: 0x040009B0 RID: 2480
		public static float baseDuration;

		// Token: 0x040009B1 RID: 2481
		public static float healPulseFraction = 0.5f;

		// Token: 0x040009B2 RID: 2482
		public static float healMultiplier = 2f;

		// Token: 0x040009B3 RID: 2483
		public static float damageFraction = 0.1f;

		// Token: 0x040009B4 RID: 2484
		public static GameObject burstEffectPrefab;

		// Token: 0x040009B5 RID: 2485
		public static string explosionSound;

		// Token: 0x040009B6 RID: 2486
		public static string siphonLoopSound;

		// Token: 0x040009B7 RID: 2487
		public static string retractSound;

		// Token: 0x040009B8 RID: 2488
		private float duration;

		// Token: 0x040009B9 RID: 2489
		private float gainedHealth;

		// Token: 0x040009BA RID: 2490
		private float gainedHealthFraction;

		// Token: 0x040009BB RID: 2491
		private float healTimer;

		// Token: 0x040009BC RID: 2492
		private bool burstPlayed;
	}
}
