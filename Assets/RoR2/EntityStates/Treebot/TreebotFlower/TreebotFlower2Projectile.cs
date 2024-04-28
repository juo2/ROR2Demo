using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Orbs;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Treebot.TreebotFlower
{
	// Token: 0x0200018B RID: 395
	public class TreebotFlower2Projectile : BaseState
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001DDEC File Offset: 0x0001BFEC
		public override void OnEnter()
		{
			base.OnEnter();
			ProjectileController component = base.GetComponent<ProjectileController>();
			if (component)
			{
				this.owner = component.owner;
				this.procChainMask = component.procChainMask;
				this.procCoefficient = component.procCoefficient;
				this.teamIndex = component.teamFilter.teamIndex;
			}
			ProjectileDamage component2 = base.GetComponent<ProjectileDamage>();
			if (component2)
			{
				this.damage = component2.damage;
				this.damageType = component2.damageType;
				this.crit = component2.crit;
			}
			if (NetworkServer.active)
			{
				this.rootedBodies = new List<CharacterBody>();
			}
			this.PlayAnimation("Base", "SpawnToIdle");
			Util.PlaySound(TreebotFlower2Projectile.enterSoundString, base.gameObject);
			if (TreebotFlower2Projectile.enterEffectPrefab)
			{
				EffectManager.SimpleEffect(TreebotFlower2Projectile.enterEffectPrefab, base.transform.position, base.transform.rotation, false);
			}
			ChildLocator component3 = base.GetModelTransform().GetComponent<ChildLocator>();
			if (component3)
			{
				Transform transform = component3.FindChild("AreaIndicator");
				transform.localScale = new Vector3(TreebotFlower2Projectile.radius, TreebotFlower2Projectile.radius, TreebotFlower2Projectile.radius);
				transform.gameObject.SetActive(true);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001DF1C File Offset: 0x0001C11C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.rootPulseTimer -= Time.fixedDeltaTime;
				this.healTimer -= Time.fixedDeltaTime;
				if (this.rootPulseTimer <= 0f)
				{
					this.rootPulseTimer += TreebotFlower2Projectile.duration / TreebotFlower2Projectile.rootPulseCount;
					this.RootPulse();
				}
				if (this.healTimer <= 0f)
				{
					this.healTimer += TreebotFlower2Projectile.duration / TreebotFlower2Projectile.healPulseCount;
					this.HealPulse();
				}
				if (base.fixedAge >= TreebotFlower2Projectile.duration)
				{
					EntityState.Destroy(base.gameObject);
					return;
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		private void RootPulse()
		{
			Vector3 position = base.transform.position;
			foreach (HurtBox hurtBox in new SphereSearch
			{
				origin = position,
				radius = TreebotFlower2Projectile.radius,
				mask = LayerIndex.entityPrecise.mask
			}.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(this.teamIndex)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes())
			{
				CharacterBody body = hurtBox.healthComponent.body;
				if (!this.rootedBodies.Contains(body))
				{
					this.rootedBodies.Add(body);
					body.AddBuff(RoR2Content.Buffs.Entangle);
					body.RecalculateStats();
					Vector3 a = hurtBox.transform.position - position;
					float magnitude = a.magnitude;
					Vector3 a2 = a / magnitude;
					Rigidbody component = hurtBox.healthComponent.GetComponent<Rigidbody>();
					float num = component ? component.mass : 1f;
					float num2 = magnitude - TreebotFlower2Projectile.yankIdealDistance;
					float num3 = TreebotFlower2Projectile.yankSuitabilityCurve.Evaluate(num);
					Vector3 vector = component ? component.velocity : Vector3.zero;
					if (HGMath.IsVectorNaN(vector))
					{
						vector = Vector3.zero;
					}
					Vector3 a3 = -vector;
					if (num2 > 0f)
					{
						a3 = a2 * -Trajectory.CalculateInitialYSpeedForHeight(num2, -body.acceleration);
					}
					Vector3 force = a3 * (num * num3);
					DamageInfo damageInfo = new DamageInfo
					{
						attacker = this.owner,
						inflictor = base.gameObject,
						crit = this.crit,
						damage = this.damage,
						damageColorIndex = DamageColorIndex.Default,
						damageType = this.damageType,
						force = force,
						position = hurtBox.transform.position,
						procChainMask = this.procChainMask,
						procCoefficient = this.procCoefficient
					};
					hurtBox.healthComponent.TakeDamage(damageInfo);
					HurtBox hurtBoxReference = hurtBox;
					HurtBoxGroup hurtBoxGroup = hurtBox.hurtBoxGroup;
					int num4 = 0;
					while ((float)num4 < Mathf.Min(4f, body.radius * 2f))
					{
						EffectData effectData = new EffectData
						{
							scale = 1f,
							origin = position,
							genericFloat = Mathf.Max(0.2f, TreebotFlower2Projectile.duration - base.fixedAge)
						};
						effectData.SetHurtBoxReference(hurtBoxReference);
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/EntangleOrbEffect"), effectData, true);
						hurtBoxReference = hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, hurtBoxGroup.hurtBoxes.Length)];
						num4++;
					}
				}
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001E280 File Offset: 0x0001C480
		public override void OnExit()
		{
			if (this.rootedBodies != null)
			{
				foreach (CharacterBody characterBody in this.rootedBodies)
				{
					characterBody.RemoveBuff(RoR2Content.Buffs.Entangle);
				}
				this.rootedBodies = null;
			}
			Util.PlaySound(TreebotFlower2Projectile.exitSoundString, base.gameObject);
			if (TreebotFlower2Projectile.exitEffectPrefab)
			{
				EffectManager.SimpleEffect(TreebotFlower2Projectile.exitEffectPrefab, base.transform.position, base.transform.rotation, false);
			}
			base.OnExit();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001E328 File Offset: 0x0001C528
		private void HealPulse()
		{
			HealthComponent healthComponent = this.owner ? this.owner.GetComponent<HealthComponent>() : null;
			if (healthComponent && this.rootedBodies.Count > 0)
			{
				float num = 1f / TreebotFlower2Projectile.healPulseCount;
				HealOrb healOrb = new HealOrb();
				healOrb.origin = base.transform.position;
				healOrb.target = healthComponent.body.mainHurtBox;
				healOrb.healValue = num * TreebotFlower2Projectile.healthFractionYieldPerHit * healthComponent.fullHealth * (float)this.rootedBodies.Count;
				healOrb.overrideDuration = 0.3f;
				OrbManager.instance.AddOrb(healOrb);
			}
		}

		// Token: 0x04000883 RID: 2179
		public static float yankIdealDistance;

		// Token: 0x04000884 RID: 2180
		public static AnimationCurve yankSuitabilityCurve;

		// Token: 0x04000885 RID: 2181
		public static float healthFractionYieldPerHit;

		// Token: 0x04000886 RID: 2182
		public static float radius;

		// Token: 0x04000887 RID: 2183
		public static float healPulseCount;

		// Token: 0x04000888 RID: 2184
		public static float duration;

		// Token: 0x04000889 RID: 2185
		public static float rootPulseCount;

		// Token: 0x0400088A RID: 2186
		public static string enterSoundString;

		// Token: 0x0400088B RID: 2187
		public static string exitSoundString;

		// Token: 0x0400088C RID: 2188
		public static GameObject enterEffectPrefab;

		// Token: 0x0400088D RID: 2189
		public static GameObject exitEffectPrefab;

		// Token: 0x0400088E RID: 2190
		private List<CharacterBody> rootedBodies;

		// Token: 0x0400088F RID: 2191
		private float healTimer;

		// Token: 0x04000890 RID: 2192
		private float rootPulseTimer;

		// Token: 0x04000891 RID: 2193
		private GameObject owner;

		// Token: 0x04000892 RID: 2194
		private ProcChainMask procChainMask;

		// Token: 0x04000893 RID: 2195
		private float procCoefficient;

		// Token: 0x04000894 RID: 2196
		private TeamIndex teamIndex = TeamIndex.None;

		// Token: 0x04000895 RID: 2197
		private float damage;

		// Token: 0x04000896 RID: 2198
		private DamageType damageType;

		// Token: 0x04000897 RID: 2199
		private bool crit;

		// Token: 0x04000898 RID: 2200
		private float healPulseHealthFractionValue;
	}
}
