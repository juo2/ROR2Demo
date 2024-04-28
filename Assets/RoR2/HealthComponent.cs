using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EntityStates;
using JetBrains.Annotations;
using RoR2.Audio;
using RoR2.Networking;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000720 RID: 1824
	[RequireComponent(typeof(CharacterBody))]
	[DisallowMultipleComponent]
	public class HealthComponent : NetworkBehaviour
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000A2E5D File Offset: 0x000A105D
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x000A2E65 File Offset: 0x000A1065
		public DamageType killingDamageType
		{
			get
			{
				return (DamageType)this._killingDamageType;
			}
			private set
			{
				this.Network_killingDamageType = (uint)value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000A2E6E File Offset: 0x000A106E
		public bool alive
		{
			get
			{
				return this.health > 0f;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000A2E7D File Offset: 0x000A107D
		public float fullHealth
		{
			get
			{
				return this.body.maxHealth;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000A2E8A File Offset: 0x000A108A
		public float fullShield
		{
			get
			{
				return this.body.maxShield;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000A2E97 File Offset: 0x000A1097
		public float fullBarrier
		{
			get
			{
				return this.body.maxBarrier;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000A2EA4 File Offset: 0x000A10A4
		public float combinedHealth
		{
			get
			{
				return this.health + this.shield + this.barrier;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000A2EBA File Offset: 0x000A10BA
		public float fullCombinedHealth
		{
			get
			{
				return this.fullHealth + this.fullShield;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000A2EC9 File Offset: 0x000A10C9
		public float combinedHealthFraction
		{
			get
			{
				return this.combinedHealth / this.fullCombinedHealth;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000A2ED8 File Offset: 0x000A10D8
		public float missingCombinedHealth
		{
			get
			{
				return this.fullCombinedHealth - (this.combinedHealth - this.barrier);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000A2EEE File Offset: 0x000A10EE
		// (set) Token: 0x060025B9 RID: 9657 RVA: 0x000A2EF6 File Offset: 0x000A10F6
		public Run.FixedTimeStamp lastHitTime { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000A2EFF File Offset: 0x000A10FF
		// (set) Token: 0x060025BB RID: 9659 RVA: 0x000A2F07 File Offset: 0x000A1107
		public Run.FixedTimeStamp lastHealTime { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000A2F10 File Offset: 0x000A1110
		// (set) Token: 0x060025BD RID: 9661 RVA: 0x000A2F18 File Offset: 0x000A1118
		public GameObject lastHitAttacker { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000A2F24 File Offset: 0x000A1124
		public float timeSinceLastHit
		{
			get
			{
				return this.lastHitTime.timeSince;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000A2F40 File Offset: 0x000A1140
		public float timeSinceLastHeal
		{
			get
			{
				return this.lastHealTime.timeSince;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000A2F5B File Offset: 0x000A115B
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x000A2F63 File Offset: 0x000A1163
		public bool godMode { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000A2F6C File Offset: 0x000A116C
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x000A2F74 File Offset: 0x000A1174
		public float potionReserve { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000A2F7D File Offset: 0x000A117D
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x000A2F85 File Offset: 0x000A1185
		public bool isInFrozenState { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000A2F8E File Offset: 0x000A118E
		public bool isHealthLow
		{
			get
			{
				return (this.health + this.shield) / this.fullCombinedHealth <= HealthComponent.lowHealthFraction;
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060025C7 RID: 9671 RVA: 0x000A2FB0 File Offset: 0x000A11B0
		// (remove) Token: 0x060025C8 RID: 9672 RVA: 0x000A2FE4 File Offset: 0x000A11E4
		public static event Action<HealthComponent, float, ProcChainMask> onCharacterHealServer;

		// Token: 0x060025C9 RID: 9673 RVA: 0x000A3017 File Offset: 0x000A1217
		public void OnValidate()
		{
			if (base.gameObject.GetComponents<HealthComponent>().Length > 1)
			{
				Debug.LogErrorFormat(base.gameObject, "{0} has multiple health components!!", new object[]
				{
					base.gameObject
				});
			}
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000A3048 File Offset: 0x000A1248
		public float Heal(float amount, ProcChainMask procChainMask, bool nonRegen = true)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Single RoR2.HealthComponent::Heal(System.Single, RoR2.ProcChainMask, System.Boolean)' called on client");
				return 0f;
			}
			if (!this.alive || amount <= 0f || this.body.HasBuff(RoR2Content.Buffs.HealingDisabled))
			{
				return 0f;
			}
			float num = this.health;
			bool flag = false;
			if (this.currentEquipmentIndex == RoR2Content.Equipment.LunarPotion.equipmentIndex && !procChainMask.HasProc(ProcType.LunarPotionActivation))
			{
				this.potionReserve += amount;
				return amount;
			}
			if (nonRegen && !procChainMask.HasProc(ProcType.CritHeal) && Util.CheckRoll(this.body.critHeal, this.body.master))
			{
				procChainMask.AddProc(ProcType.CritHeal);
				flag = true;
			}
			if (flag)
			{
				amount *= 2f;
			}
			if (this.itemCounts.increaseHealing > 0)
			{
				amount *= 1f + (float)this.itemCounts.increaseHealing;
			}
			if (this.body.teamComponent.teamIndex == TeamIndex.Player && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse5)
			{
				amount /= 2f;
			}
			if (nonRegen && this.repeatHealComponent && !procChainMask.HasProc(ProcType.RepeatHeal))
			{
				this.repeatHealComponent.healthFractionToRestorePerSecond = 0.1f / (float)this.itemCounts.repeatHeal;
				this.repeatHealComponent.AddReserve(amount * (float)(1 + this.itemCounts.repeatHeal), this.fullHealth);
				return 0f;
			}
			float num2 = amount;
			if (this.health < this.fullHealth)
			{
				float num3 = Mathf.Max(Mathf.Min(amount, this.fullHealth - this.health), 0f);
				num2 = amount - num3;
				this.Networkhealth = this.health + num3;
			}
			if (num2 > 0f && nonRegen && this.itemCounts.barrierOnOverHeal > 0)
			{
				float value = num2 * ((float)this.itemCounts.barrierOnOverHeal * 0.5f);
				this.AddBarrier(value);
			}
			if (nonRegen)
			{
				this.lastHealTime = Run.FixedTimeStamp.now;
				HealthComponent.SendHeal(base.gameObject, amount, flag);
				if (this.itemCounts.novaOnHeal > 0 && !procChainMask.HasProc(ProcType.HealNova))
				{
					this.devilOrbHealPool = Mathf.Min(this.devilOrbHealPool + amount * (float)this.itemCounts.novaOnHeal, this.fullCombinedHealth);
				}
			}
			if (flag)
			{
				GlobalEventManager.instance.OnCrit(this.body, null, this.body.master, amount / this.fullHealth * 10f, procChainMask);
			}
			if (nonRegen)
			{
				Action<HealthComponent, float, ProcChainMask> action = HealthComponent.onCharacterHealServer;
				if (action != null)
				{
					action(this, amount, procChainMask);
				}
			}
			return this.health - num;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000A32D8 File Offset: 0x000A14D8
		public void UsePotion()
		{
			ProcChainMask procChainMask = default(ProcChainMask);
			procChainMask.AddProc(ProcType.LunarPotionActivation);
			this.Heal(this.potionReserve, procChainMask, true);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000A3305 File Offset: 0x000A1505
		public float HealFraction(float fraction, ProcChainMask procChainMask)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Single RoR2.HealthComponent::HealFraction(System.Single, RoR2.ProcChainMask)' called on client");
				return 0f;
			}
			return this.Heal(fraction * this.fullHealth, procChainMask, true);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000A3330 File Offset: 0x000A1530
		[Command]
		public void CmdHealFull()
		{
			this.HealFraction(1f, default(ProcChainMask));
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000A3352 File Offset: 0x000A1552
		[Server]
		public void RechargeShieldFull()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::RechargeShieldFull()' called on client");
				return;
			}
			if (this.shield < this.fullShield)
			{
				this.Networkshield = this.fullShield;
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000A3383 File Offset: 0x000A1583
		[Command]
		public void CmdRechargeShieldFull()
		{
			this.RechargeShieldFull();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000A338C File Offset: 0x000A158C
		[Server]
		public void RechargeShield(float value)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::RechargeShield(System.Single)' called on client");
				return;
			}
			if (this.shield < this.fullShield)
			{
				this.Networkshield = this.shield + value;
				if (this.shield > this.fullShield)
				{
					this.Networkshield = this.fullShield;
				}
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000A33E4 File Offset: 0x000A15E4
		[Server]
		public void AddBarrier(float value)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::AddBarrier(System.Single)' called on client");
				return;
			}
			if (!this.alive)
			{
				return;
			}
			if (this.barrier < this.fullBarrier)
			{
				this.Networkbarrier = Mathf.Min(this.barrier + value, this.fullBarrier);
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000A3438 File Offset: 0x000A1638
		[Server]
		public void AddCharge(float value)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::AddCharge(System.Single)' called on client");
				return;
			}
			if (!this.alive)
			{
				return;
			}
			if (this.magnetiCharge < this.fullHealth)
			{
				this.NetworkmagnetiCharge = Mathf.Min(this.barrier + value, this.fullBarrier);
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000A348A File Offset: 0x000A168A
		[Command]
		private void CmdAddBarrier(float value)
		{
			this.AddBarrier(value);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000A3493 File Offset: 0x000A1693
		public void AddBarrierAuthority(float value)
		{
			if (NetworkServer.active)
			{
				this.AddBarrier(value);
				return;
			}
			this.CallCmdAddBarrier(value);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000A34AB File Offset: 0x000A16AB
		[Command]
		private void CmdForceShieldRegen()
		{
			this.ForceShieldRegen();
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000A34B3 File Offset: 0x000A16B3
		public void ForceShieldRegen()
		{
			if (NetworkServer.active)
			{
				this.isShieldRegenForced = true;
				return;
			}
			this.CallCmdForceShieldRegen();
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000A34CC File Offset: 0x000A16CC
		[Server]
		public void TakeDamageForce(DamageInfo damageInfo, bool alwaysApply = false, bool disableAirControlUntilCollision = false)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TakeDamageForce(RoR2.DamageInfo,System.Boolean,System.Boolean)' called on client");
				return;
			}
			if (this.body.HasBuff(RoR2Content.Buffs.EngiShield) && this.shield > 0f)
			{
				return;
			}
			CharacterMotor component = base.GetComponent<CharacterMotor>();
			if (component)
			{
				component.ApplyForce(damageInfo.force, alwaysApply, disableAirControlUntilCollision);
			}
			Rigidbody component2 = base.GetComponent<Rigidbody>();
			if (component2)
			{
				component2.AddForce(damageInfo.force, ForceMode.Impulse);
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000A3548 File Offset: 0x000A1748
		[Server]
		public void TakeDamageForce(Vector3 force, bool alwaysApply = false, bool disableAirControlUntilCollision = false)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TakeDamageForce(UnityEngine.Vector3,System.Boolean,System.Boolean)' called on client");
				return;
			}
			if (this.body.HasBuff(RoR2Content.Buffs.EngiShield) && this.shield > 0f)
			{
				return;
			}
			CharacterMotor component = base.GetComponent<CharacterMotor>();
			if (component)
			{
				component.ApplyForce(force, alwaysApply, disableAirControlUntilCollision);
			}
			Rigidbody component2 = base.GetComponent<Rigidbody>();
			if (component2)
			{
				component2.AddForce(force, ForceMode.Impulse);
			}
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000A35BC File Offset: 0x000A17BC
		[Server]
		public void TakeDamage(DamageInfo damageInfo)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TakeDamage(RoR2.DamageInfo)' called on client");
				return;
			}
			if (!damageInfo.canRejectForce)
			{
				this.TakeDamageForce(damageInfo, false, false);
			}
			if (!this.alive || this.godMode)
			{
				return;
			}
			if (this.ospTimer > 0f)
			{
				return;
			}
			CharacterBody characterBody = null;
			TeamIndex teamIndex = TeamIndex.None;
			Vector3 vector = Vector3.zero;
			float combinedHealth = this.combinedHealth;
			if (damageInfo.attacker)
			{
				characterBody = damageInfo.attacker.GetComponent<CharacterBody>();
				if (characterBody)
				{
					teamIndex = characterBody.teamComponent.teamIndex;
					vector = characterBody.corePosition - damageInfo.position;
				}
			}
			bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
			bool flag2 = (damageInfo.damageType & DamageType.BypassBlock) > DamageType.Generic;
			if (!flag2 && this.itemCounts.bear > 0 && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(15f * (float)this.itemCounts.bear), 0f, null))
			{
				EffectData effectData = new EffectData
				{
					origin = damageInfo.position,
					rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
				};
				EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);
				damageInfo.rejected = true;
			}
			if (!flag2 && this.body.HasBuff(DLC1Content.Buffs.BearVoidReady) && damageInfo.damage > 0f)
			{
				EffectData effectData2 = new EffectData
				{
					origin = damageInfo.position,
					rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
				};
				EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearVoidEffectPrefab, effectData2, true);
				damageInfo.rejected = true;
				this.body.RemoveBuff(DLC1Content.Buffs.BearVoidReady);
				int itemCount = this.body.inventory.GetItemCount(DLC1Content.Items.BearVoid);
				this.body.AddTimedBuff(DLC1Content.Buffs.BearVoidCooldown, 15f * Mathf.Pow(0.9f, (float)itemCount));
			}
			if (this.body.HasBuff(RoR2Content.Buffs.HiddenInvincibility) && !flag)
			{
				damageInfo.rejected = true;
			}
			if (this.body.HasBuff(RoR2Content.Buffs.Immune) && (!characterBody || !characterBody.HasBuff(JunkContent.Buffs.GoldEmpowered)))
			{
				EffectManager.SpawnEffect(HealthComponent.AssetReferences.damageRejectedPrefab, new EffectData
				{
					origin = damageInfo.position
				}, true);
				damageInfo.rejected = true;
			}
			if (!damageInfo.rejected && this.body.HasBuff(JunkContent.Buffs.BodyArmor))
			{
				this.body.RemoveBuff(JunkContent.Buffs.BodyArmor);
				EffectData effectData3 = new EffectData
				{
					origin = damageInfo.position,
					rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
				};
				EffectManager.SpawnEffect(HealthComponent.AssetReferences.captainBodyArmorBlockEffectPrefab, effectData3, true);
				damageInfo.rejected = true;
			}
			IOnIncomingDamageServerReceiver[] array = this.onIncomingDamageReceivers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnIncomingDamageServer(damageInfo);
			}
			if (damageInfo.rejected)
			{
				return;
			}
			float num = damageInfo.damage;
			if (teamIndex == this.body.teamComponent.teamIndex)
			{
				TeamDef teamDef = TeamCatalog.GetTeamDef(teamIndex);
				if (teamDef != null)
				{
					num *= teamDef.friendlyFireScaling;
				}
			}
			if (num > 0f)
			{
				if (characterBody)
				{
					if (characterBody.canPerformBackstab && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && (damageInfo.procChainMask.HasProc(ProcType.Backstab) || BackstabManager.IsBackstab(-vector, this.body)))
					{
						damageInfo.crit = true;
						damageInfo.procChainMask.AddProc(ProcType.Backstab);
						if (BackstabManager.backstabImpactEffectPrefab)
						{
							EffectManager.SimpleImpactEffect(BackstabManager.backstabImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
						}
					}
					CharacterMaster master = characterBody.master;
					if (master && master.inventory)
					{
						if (combinedHealth >= this.fullCombinedHealth * 0.9f)
						{
							int itemCount2 = master.inventory.GetItemCount(RoR2Content.Items.Crowbar);
							if (itemCount2 > 0)
							{
								num *= 1f + 0.75f * (float)itemCount2;
								EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.crowbarImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
							}
						}
						if (combinedHealth >= this.fullCombinedHealth && !damageInfo.rejected)
						{
							int itemCount3 = master.inventory.GetItemCount(DLC1Content.Items.ExplodeOnDeathVoid);
							if (itemCount3 > 0)
							{
								Vector3 corePosition = Util.GetCorePosition(this.body);
								float damageCoefficient = 2.6f * (1f + (float)(itemCount3 - 1) * 0.6f);
								float baseDamage = Util.OnKillProcDamage(characterBody.damage, damageCoefficient);
								GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(HealthComponent.AssetReferences.explodeOnDeathVoidExplosionPrefab, corePosition, Quaternion.identity);
								DelayBlast component = gameObject.GetComponent<DelayBlast>();
								component.position = corePosition;
								component.baseDamage = baseDamage;
								component.baseForce = 1000f;
								component.radius = 12f + 2.4f * ((float)itemCount3 - 1f);
								component.attacker = damageInfo.attacker;
								component.inflictor = null;
								component.crit = Util.CheckRoll(characterBody.crit, master);
								component.maxTimer = 0.2f;
								component.damageColorIndex = DamageColorIndex.Void;
								component.falloffModel = BlastAttack.FalloffModel.SweetSpot;
								gameObject.GetComponent<TeamFilter>().teamIndex = teamIndex;
								NetworkServer.Spawn(gameObject);
							}
						}
						int itemCount4 = master.inventory.GetItemCount(RoR2Content.Items.NearbyDamageBonus);
						if (itemCount4 > 0 && vector.sqrMagnitude <= 169f)
						{
							damageInfo.damageColorIndex = DamageColorIndex.Nearby;
							num *= 1f + (float)itemCount4 * 0.2f;
							EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.diamondDamageBonusImpactEffectPrefab, damageInfo.position, vector, true);
						}
						int itemCount5 = master.inventory.GetItemCount(DLC1Content.Items.FragileDamageBonus);
						if (itemCount5 > 0)
						{
							num *= 1f + (float)itemCount5 * 0.2f;
						}
						if (damageInfo.procCoefficient > 0f)
						{
							int itemCount6 = master.inventory.GetItemCount(RoR2Content.Items.ArmorReductionOnHit);
							if (itemCount6 > 0 && !this.body.HasBuff(RoR2Content.Buffs.Pulverized))
							{
								this.body.AddTimedBuff(RoR2Content.Buffs.PulverizeBuildup, 2f * damageInfo.procCoefficient);
								if (this.body.GetBuffCount(RoR2Content.Buffs.PulverizeBuildup) >= 5)
								{
									this.body.ClearTimedBuffs(RoR2Content.Buffs.PulverizeBuildup);
									this.body.AddTimedBuff(RoR2Content.Buffs.Pulverized, 8f * (float)itemCount6);
									EffectManager.SpawnEffect(HealthComponent.AssetReferences.pulverizedEffectPrefab, new EffectData
									{
										origin = this.body.corePosition,
										scale = this.body.radius
									}, true);
								}
							}
							int itemCount7 = master.inventory.GetItemCount(DLC1Content.Items.PermanentDebuffOnHit);
							bool flag3 = false;
							for (int j = 0; j < itemCount7; j++)
							{
								if (Util.CheckRoll(100f * damageInfo.procCoefficient, master))
								{
									this.body.AddBuff(DLC1Content.Buffs.PermanentDebuff);
									flag3 = true;
								}
							}
							if (flag3)
							{
								EffectManager.SpawnEffect(HealthComponent.AssetReferences.permanentDebuffEffectPrefab, new EffectData
								{
									origin = damageInfo.position,
									scale = (float)itemCount7
								}, true);
							}
							if (this.body.HasBuff(RoR2Content.Buffs.MercExpose) && characterBody && characterBody.bodyIndex == BodyCatalog.FindBodyIndex("MercBody"))
							{
								this.body.RemoveBuff(RoR2Content.Buffs.MercExpose);
								float num2 = characterBody.damage * 3.5f;
								num += num2;
								damageInfo.damage += num2;
								SkillLocator skillLocator = characterBody.skillLocator;
								if (skillLocator)
								{
									skillLocator.DeductCooldownFromAllSkillsServer(1f);
								}
								EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab, damageInfo.position, Vector3.up, true);
							}
						}
						if (this.body.isBoss)
						{
							int itemCount8 = master.inventory.GetItemCount(RoR2Content.Items.BossDamageBonus);
							if (itemCount8 > 0)
							{
								num *= 1f + 0.2f * (float)itemCount8;
								damageInfo.damageColorIndex = DamageColorIndex.WeakPoint;
								EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.bossDamageBonusImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
							}
						}
					}
					if (damageInfo.crit)
					{
						num *= characterBody.critMultiplier;
					}
				}
				if ((damageInfo.damageType & DamageType.WeakPointHit) != DamageType.Generic)
				{
					num *= 1.5f;
					damageInfo.damageColorIndex = DamageColorIndex.WeakPoint;
				}
				if (this.body.HasBuff(RoR2Content.Buffs.DeathMark))
				{
					num *= 1.5f;
					damageInfo.damageColorIndex = DamageColorIndex.DeathMark;
				}
				if (!flag)
				{
					float num3 = this.body.armor;
					num3 += this.adaptiveArmorValue;
					bool flag4 = (damageInfo.damageType & DamageType.AOE) > DamageType.Generic;
					if ((this.body.bodyFlags & CharacterBody.BodyFlags.ResistantToAOE) > CharacterBody.BodyFlags.None && flag4)
					{
						num3 += 300f;
					}
					float num4 = (num3 >= 0f) ? (1f - num3 / (num3 + 100f)) : (2f - 100f / (100f - num3));
					num = Mathf.Max(1f, num * num4);
					if (this.itemCounts.armorPlate > 0)
					{
						num = Mathf.Max(1f, num - 5f * (float)this.itemCounts.armorPlate);
						EntitySoundManager.EmitSoundServer(LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseArmorPlateBlock").index, base.gameObject);
					}
					if (this.itemCounts.parentEgg > 0)
					{
						this.Heal((float)this.itemCounts.parentEgg * 15f, default(ProcChainMask), true);
						EntitySoundManager.EmitSoundServer(LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseParentEggHeal").index, base.gameObject);
					}
				}
				if (this.body.hasOneShotProtection && (damageInfo.damageType & DamageType.BypassOneShotProtection) != DamageType.BypassOneShotProtection)
				{
					float num5 = (this.fullCombinedHealth + this.barrier) * (1f - this.body.oneShotProtectionFraction);
					float b = Mathf.Max(0f, num5 - this.serverDamageTakenThisUpdate);
					float num6 = num;
					num = Mathf.Min(num, b);
					if (num != num6)
					{
						this.TriggerOneShotProtection();
					}
				}
				if ((damageInfo.damageType & DamageType.BonusToLowHealth) > DamageType.Generic)
				{
					float num7 = Mathf.Lerp(3f, 1f, this.combinedHealthFraction);
					num *= num7;
				}
				if (this.body.HasBuff(RoR2Content.Buffs.LunarShell) && num > this.fullHealth * 0.1f)
				{
					num = this.fullHealth * 0.1f;
				}
				if (this.itemCounts.minHealthPercentage > 0)
				{
					float num8 = this.fullCombinedHealth * ((float)this.itemCounts.minHealthPercentage / 100f);
					num = Mathf.Max(0f, Mathf.Min(num, this.combinedHealth - num8));
				}
			}
			if ((damageInfo.damageType & DamageType.SlowOnHit) != DamageType.Generic)
			{
				this.body.AddTimedBuff(RoR2Content.Buffs.Slow50, 2f);
			}
			if ((damageInfo.damageType & DamageType.ClayGoo) != DamageType.Generic && (this.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToGoo) == CharacterBody.BodyFlags.None)
			{
				this.body.AddTimedBuff(RoR2Content.Buffs.ClayGoo, 2f);
			}
			if ((damageInfo.damageType & DamageType.Nullify) != DamageType.Generic)
			{
				this.body.AddTimedBuff(RoR2Content.Buffs.NullifyStack, 8f);
			}
			if ((damageInfo.damageType & DamageType.CrippleOnHit) != DamageType.Generic || (characterBody && characterBody.HasBuff(RoR2Content.Buffs.AffixLunar)))
			{
				this.body.AddTimedBuff(RoR2Content.Buffs.Cripple, 3f);
			}
			if ((damageInfo.damageType & DamageType.ApplyMercExpose) != DamageType.Generic)
			{
				Debug.LogFormat("Adding expose", Array.Empty<object>());
				this.body.AddBuff(RoR2Content.Buffs.MercExpose);
			}
			CharacterMaster master2 = this.body.master;
			if (master2)
			{
				if (this.itemCounts.goldOnHit > 0)
				{
					uint num9 = (uint)(num / this.fullCombinedHealth * master2.money * (float)this.itemCounts.goldOnHit);
					uint money = master2.money;
					master2.money = (uint)Mathf.Max(0f, master2.money - num9);
					if (money - master2.money > 0U)
					{
						GoldOrb goldOrb = new GoldOrb();
						goldOrb.origin = damageInfo.position;
						goldOrb.target = (characterBody ? characterBody.mainHurtBox : this.body.mainHurtBox);
						goldOrb.goldAmount = 0U;
						OrbManager.instance.AddOrb(goldOrb);
						EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.loseCoinsImpactEffectPrefab, damageInfo.position, Vector3.up, true);
					}
				}
				if (this.itemCounts.goldOnHurt > 0 && characterBody != this.body && characterBody != null)
				{
					int num10 = 3;
					GoldOrb goldOrb2 = new GoldOrb();
					goldOrb2.origin = damageInfo.position;
					goldOrb2.target = this.body.mainHurtBox;
					goldOrb2.goldAmount = (uint)((float)(this.itemCounts.goldOnHurt * num10) * Run.instance.difficultyCoefficient);
					OrbManager.instance.AddOrb(goldOrb2);
					EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.gainCoinsImpactEffectPrefab, damageInfo.position, Vector3.up, true);
				}
			}
			if (this.itemCounts.adaptiveArmor > 0)
			{
				float num11 = num / this.fullCombinedHealth * 100f * 30f * (float)this.itemCounts.adaptiveArmor;
				this.adaptiveArmorValue = Mathf.Min(this.adaptiveArmorValue + num11, 400f);
			}
			float num12 = num;
			if (num12 > 0f)
			{
				this.isShieldRegenForced = false;
			}
			if (this.body.teamComponent.teamIndex == TeamIndex.Player && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse8)
			{
				float num13 = num12 / this.fullCombinedHealth * 100f;
				float num14 = 0.4f;
				int num15 = Mathf.FloorToInt(num13 * num14);
				for (int k = 0; k < num15; k++)
				{
					this.body.AddBuff(RoR2Content.Buffs.PermanentCurse);
				}
			}
			if (num12 > 0f && this.barrier > 0f)
			{
				if (num12 <= this.barrier)
				{
					this.Networkbarrier = this.barrier - num12;
					num12 = 0f;
				}
				else
				{
					num12 -= this.barrier;
					this.Networkbarrier = 0f;
				}
			}
			if (num12 > 0f && this.shield > 0f)
			{
				if (num12 <= this.shield)
				{
					this.Networkshield = this.shield - num12;
					num12 = 0f;
				}
				else
				{
					num12 -= this.shield;
					this.Networkshield = 0f;
					float scale = 1f;
					if (this.body)
					{
						scale = this.body.radius;
					}
					EffectManager.SpawnEffect(HealthComponent.AssetReferences.shieldBreakEffectPrefab, new EffectData
					{
						origin = base.transform.position,
						scale = scale
					}, true);
				}
			}
			bool flag5 = (damageInfo.damageType & DamageType.VoidDeath) != DamageType.Generic && (this.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToVoidDeath) == CharacterBody.BodyFlags.None;
			float executionHealthLost = 0f;
			GameObject gameObject2 = null;
			if (num12 > 0f)
			{
				float num16 = this.health - num12;
				if (num16 < 1f && (damageInfo.damageType & DamageType.NonLethal) != DamageType.Generic && this.health >= 1f)
				{
					num16 = 1f;
				}
				this.Networkhealth = num16;
			}
			float num17 = float.NegativeInfinity;
			bool flag6 = (this.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToExecutes) > CharacterBody.BodyFlags.None;
			if (!flag5 && !flag6)
			{
				if (this.isInFrozenState && num17 < 0.3f)
				{
					num17 = 0.3f;
					gameObject2 = FrozenState.executeEffectPrefab;
				}
				if (characterBody)
				{
					if (this.body.isElite)
					{
						float executeEliteHealthFraction = characterBody.executeEliteHealthFraction;
						if (num17 < executeEliteHealthFraction)
						{
							num17 = executeEliteHealthFraction;
							gameObject2 = HealthComponent.AssetReferences.executeEffectPrefab;
						}
					}
					if (!this.body.isBoss && characterBody.inventory && Util.CheckRoll((float)characterBody.inventory.GetItemCount(DLC1Content.Items.CritGlassesVoid) * 0.5f * damageInfo.procCoefficient, characterBody.master))
					{
						flag5 = true;
						gameObject2 = HealthComponent.AssetReferences.critGlassesVoidExecuteEffectPrefab;
						damageInfo.damageType |= DamageType.VoidDeath;
					}
				}
			}
			if (flag5 || (num17 > 0f && this.combinedHealthFraction <= num17))
			{
				flag5 = true;
				executionHealthLost = Mathf.Max(this.combinedHealth, 0f);
				if (this.health > 0f)
				{
					this.Networkhealth = 0f;
				}
				if (this.shield > 0f)
				{
					this.Networkshield = 0f;
				}
				if (this.barrier > 0f)
				{
					this.Networkbarrier = 0f;
				}
			}
			if (damageInfo.canRejectForce)
			{
				this.TakeDamageForce(damageInfo, false, false);
			}
			DamageReport damageReport = new DamageReport(damageInfo, this, num, combinedHealth);
			IOnTakeDamageServerReceiver[] array2 = this.onTakeDamageReceivers;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].OnTakeDamageServer(damageReport);
			}
			if (num > 0f)
			{
				HealthComponent.SendDamageDealt(damageReport);
			}
			this.UpdateLastHitTime(damageReport.damageDealt, damageInfo.position, (damageInfo.damageType & DamageType.Silent) > DamageType.Generic, damageInfo.attacker);
			if (damageInfo.attacker)
			{
				List<IOnDamageDealtServerReceiver> gameObjectComponents = GetComponentsCache<IOnDamageDealtServerReceiver>.GetGameObjectComponents(damageInfo.attacker);
				foreach (IOnDamageDealtServerReceiver onDamageDealtServerReceiver in gameObjectComponents)
				{
					onDamageDealtServerReceiver.OnDamageDealtServer(damageReport);
				}
				GetComponentsCache<IOnDamageDealtServerReceiver>.ReturnBuffer(gameObjectComponents);
			}
			if (damageInfo.inflictor)
			{
				List<IOnDamageInflictedServerReceiver> gameObjectComponents2 = GetComponentsCache<IOnDamageInflictedServerReceiver>.GetGameObjectComponents(damageInfo.inflictor);
				foreach (IOnDamageInflictedServerReceiver onDamageInflictedServerReceiver in gameObjectComponents2)
				{
					onDamageInflictedServerReceiver.OnDamageInflictedServer(damageReport);
				}
				GetComponentsCache<IOnDamageInflictedServerReceiver>.ReturnBuffer(gameObjectComponents2);
			}
			GlobalEventManager.ServerDamageDealt(damageReport);
			if (!this.alive)
			{
				this.killingDamageType = damageInfo.damageType;
				if (flag5)
				{
					GlobalEventManager.ServerCharacterExecuted(damageReport, executionHealthLost);
					if (gameObject2 != null)
					{
						EffectManager.SpawnEffect(gameObject2, new EffectData
						{
							origin = this.body.corePosition,
							scale = (this.body ? this.body.radius : 1f)
						}, true);
					}
				}
				IOnKilledServerReceiver[] components = base.GetComponents<IOnKilledServerReceiver>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].OnKilledServer(damageReport);
				}
				if (damageInfo.attacker)
				{
					IOnKilledOtherServerReceiver[] components2 = damageInfo.attacker.GetComponents<IOnKilledOtherServerReceiver>();
					for (int i = 0; i < components2.Length; i++)
					{
						components2[i].OnKilledOtherServer(damageReport);
					}
				}
				if (Util.CheckRoll(this.globalDeathEventChanceCoefficient * 100f, 0f, null))
				{
					GlobalEventManager.instance.OnCharacterDeath(damageReport);
					return;
				}
			}
			else if (num > 0f)
			{
				int a = 5 + 2 * (this.itemCounts.thorns - 1);
				if (this.itemCounts.thorns > 0 && !damageReport.damageInfo.procChainMask.HasProc(ProcType.Thorns))
				{
					bool flag7 = this.itemCounts.invadingDoppelganger > 0;
					float radius = 25f + 10f * (float)(this.itemCounts.thorns - 1);
					bool isCrit = this.body.RollCrit();
					float damageValue = 1.6f * this.body.damage;
					TeamIndex teamIndex2 = this.body.teamComponent.teamIndex;
					HurtBox[] hurtBoxes = new SphereSearch
					{
						origin = damageReport.damageInfo.position,
						radius = radius,
						mask = LayerIndex.entityPrecise.mask,
						queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
					}.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(teamIndex2)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();
					for (int l = 0; l < Mathf.Min(a, hurtBoxes.Length); l++)
					{
						LightningOrb lightningOrb = new LightningOrb();
						lightningOrb.attacker = base.gameObject;
						lightningOrb.bouncedObjects = null;
						lightningOrb.bouncesRemaining = 0;
						lightningOrb.damageCoefficientPerBounce = 1f;
						lightningOrb.damageColorIndex = DamageColorIndex.Item;
						lightningOrb.damageValue = damageValue;
						lightningOrb.isCrit = isCrit;
						lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
						lightningOrb.origin = damageReport.damageInfo.position;
						lightningOrb.procChainMask = default(ProcChainMask);
						lightningOrb.procChainMask.AddProc(ProcType.Thorns);
						lightningOrb.procCoefficient = (flag7 ? 0f : 0.5f);
						lightningOrb.range = 0f;
						lightningOrb.teamIndex = teamIndex2;
						lightningOrb.target = hurtBoxes[l];
						OrbManager.instance.AddOrb(lightningOrb);
					}
				}
			}
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000A4A34 File Offset: 0x000A2C34
		[Server]
		private void TriggerOneShotProtection()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TriggerOneShotProtection()' called on client");
				return;
			}
			this.ospTimer = 0.1f;
			Debug.Log("OSP Triggered.");
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000A4A60 File Offset: 0x000A2C60
		[Server]
		public void Suicide(GameObject killerOverride = null, GameObject inflictorOverride = null, DamageType damageType = DamageType.Generic)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::Suicide(UnityEngine.GameObject,UnityEngine.GameObject,RoR2.DamageType)' called on client");
				return;
			}
			if (this.alive && !this.godMode)
			{
				float combinedHealth = this.combinedHealth;
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = this.combinedHealth;
				damageInfo.position = base.transform.position;
				damageInfo.damageType = damageType;
				damageInfo.procCoefficient = 1f;
				if (killerOverride)
				{
					damageInfo.attacker = killerOverride;
				}
				if (inflictorOverride)
				{
					damageInfo.inflictor = inflictorOverride;
				}
				this.Networkhealth = 0f;
				DamageReport damageReport = new DamageReport(damageInfo, this, damageInfo.damage, combinedHealth);
				this.killingDamageType = damageInfo.damageType;
				IOnKilledServerReceiver[] components = base.GetComponents<IOnKilledServerReceiver>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].OnKilledServer(damageReport);
				}
				GlobalEventManager.instance.OnCharacterDeath(damageReport);
			}
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000A4B4C File Offset: 0x000A2D4C
		public void UpdateLastHitTime(float damageValue, Vector3 damagePosition, bool damageIsSilent, GameObject attacker)
		{
			if (NetworkServer.active && this.body && damageValue > 0f)
			{
				if (this.itemCounts.medkit > 0)
				{
					this.body.AddTimedBuff(RoR2Content.Buffs.MedkitHeal, 2f);
				}
				if (this.itemCounts.healingPotion > 0 && this.isHealthLow)
				{
					this.body.inventory.RemoveItem(DLC1Content.Items.HealingPotion, 1);
					this.body.inventory.GiveItem(DLC1Content.Items.HealingPotionConsumed, 1);
					CharacterMasterNotificationQueue.SendTransformNotification(this.body.master, DLC1Content.Items.HealingPotion.itemIndex, DLC1Content.Items.HealingPotionConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
					this.HealFraction(0.75f, default(ProcChainMask));
					EffectData effectData = new EffectData
					{
						origin = base.transform.position
					};
					effectData.SetNetworkedObjectReference(base.gameObject);
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/HealingPotionEffect"), effectData, true);
				}
				if (this.itemCounts.fragileDamageBonus > 0 && this.isHealthLow)
				{
					this.body.inventory.GiveItem(DLC1Content.Items.FragileDamageBonusConsumed, this.itemCounts.fragileDamageBonus);
					this.body.inventory.RemoveItem(DLC1Content.Items.FragileDamageBonus, this.itemCounts.fragileDamageBonus);
					CharacterMasterNotificationQueue.SendTransformNotification(this.body.master, DLC1Content.Items.FragileDamageBonus.itemIndex, DLC1Content.Items.FragileDamageBonusConsumed.itemIndex, CharacterMasterNotificationQueue.TransformationType.Default);
					EffectData effectData2 = new EffectData
					{
						origin = base.transform.position
					};
					effectData2.SetNetworkedObjectReference(base.gameObject);
					EffectManager.SpawnEffect(HealthComponent.AssetReferences.fragileDamageBonusBreakEffectPrefab, effectData2, true);
				}
			}
			if (damageIsSilent)
			{
				return;
			}
			this.lastHitTime = Run.FixedTimeStamp.now;
			this.lastHitAttacker = attacker;
			this.serverDamageTakenThisUpdate += damageValue;
			if (this.modelLocator)
			{
				Transform modelTransform = this.modelLocator.modelTransform;
				if (modelTransform)
				{
					Animator component = modelTransform.GetComponent<Animator>();
					if (component)
					{
						string layerName = "Flinch";
						int layerIndex = component.GetLayerIndex(layerName);
						if (layerIndex >= 0)
						{
							component.SetLayerWeight(layerIndex, 1f + Mathf.Clamp01(damageValue / this.fullCombinedHealth * 10f) * 3f);
							component.Play("FlinchStart", layerIndex);
						}
					}
				}
			}
			IPainAnimationHandler painAnimationHandler = this.painAnimationHandler;
			if (painAnimationHandler == null)
			{
				return;
			}
			painAnimationHandler.HandlePain(damageValue, damagePosition);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000A4DBD File Offset: 0x000A2FBD
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			HealthComponent.AssetReferences.Resolve();
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000A4DC4 File Offset: 0x000A2FC4
		private void Awake()
		{
			this.body = base.GetComponent<CharacterBody>();
			this.modelLocator = base.GetComponent<ModelLocator>();
			this.painAnimationHandler = base.GetComponent<IPainAnimationHandler>();
			this.onIncomingDamageReceivers = base.GetComponents<IOnIncomingDamageServerReceiver>();
			this.onTakeDamageReceivers = base.GetComponents<IOnTakeDamageServerReceiver>();
			this.lastHitTime = Run.FixedTimeStamp.negativeInfinity;
			this.lastHealTime = Run.FixedTimeStamp.negativeInfinity;
			this.body.onInventoryChanged += this.OnInventoryChanged;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000A4E3A File Offset: 0x000A303A
		private void OnDestroy()
		{
			this.body.onInventoryChanged -= this.OnInventoryChanged;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000A4E53 File Offset: 0x000A3053
		public void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.ServerFixedUpdate();
			}
			if (!this.alive && this.wasAlive)
			{
				this.wasAlive = false;
				CharacterDeathBehavior component = base.GetComponent<CharacterDeathBehavior>();
				if (component == null)
				{
					return;
				}
				component.OnDeath();
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000A4E8C File Offset: 0x000A308C
		private void ServerFixedUpdate()
		{
			if (this.alive)
			{
				this.regenAccumulator += this.body.regen * Time.fixedDeltaTime;
				if (this.barrier > 0f)
				{
					this.Networkbarrier = Mathf.Max(this.barrier - this.body.barrierDecayRate * Time.fixedDeltaTime, 0f);
				}
				if (this.regenAccumulator > 1f)
				{
					float num = Mathf.Floor(this.regenAccumulator);
					this.regenAccumulator -= num;
					this.Heal(num, default(ProcChainMask), false);
				}
				if (this.regenAccumulator < -1f)
				{
					float num2 = Mathf.Ceil(this.regenAccumulator);
					this.regenAccumulator -= num2;
					this.Networkhealth = this.health + num2;
					if (this.health <= 0f)
					{
						this.Suicide(null, null, DamageType.Generic);
					}
				}
				float num3 = this.shield;
				bool flag = num3 >= this.body.maxShield;
				if ((this.body.outOfDanger || this.isShieldRegenForced) && !flag)
				{
					num3 += this.body.maxShield * 0.5f * Time.fixedDeltaTime;
					if (num3 > this.body.maxShield)
					{
						num3 = this.body.maxShield;
					}
				}
				if (num3 >= this.body.maxShield && !flag)
				{
					Util.PlaySound("Play_item_proc_personal_shield_end", base.gameObject);
				}
				if (!num3.Equals(this.shield))
				{
					this.Networkshield = num3;
				}
				if (this.devilOrbHealPool > 0f)
				{
					this.devilOrbTimer -= Time.fixedDeltaTime;
					if (this.devilOrbTimer <= 0f)
					{
						this.devilOrbTimer += 0.1f;
						float scale = 1f;
						float num4 = this.fullCombinedHealth / 10f;
						float num5 = 2.5f;
						this.devilOrbHealPool -= num4;
						DevilOrb devilOrb = new DevilOrb();
						devilOrb.origin = this.body.aimOriginTransform.position;
						devilOrb.damageValue = num4 * num5;
						devilOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
						devilOrb.attacker = base.gameObject;
						devilOrb.damageColorIndex = DamageColorIndex.Poison;
						devilOrb.scale = scale;
						devilOrb.procChainMask.AddProc(ProcType.HealNova);
						devilOrb.effectType = DevilOrb.EffectType.Skull;
						HurtBox hurtBox = devilOrb.PickNextTarget(devilOrb.origin, 40f);
						if (hurtBox)
						{
							devilOrb.target = hurtBox;
							devilOrb.isCrit = Util.CheckRoll(this.body.crit, this.body.master);
							OrbManager.instance.AddOrb(devilOrb);
						}
					}
				}
				this.adaptiveArmorValue = Mathf.Max(0f, this.adaptiveArmorValue - 40f * Time.fixedDeltaTime);
				this.serverDamageTakenThisUpdate = 0f;
				this.ospTimer -= Time.fixedDeltaTime;
			}
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000A5194 File Offset: 0x000A3394
		private static void SendDamageDealt(DamageReport damageReport)
		{
			DamageInfo damageInfo = damageReport.damageInfo;
			NetworkServer.SendToAll(60, new DamageDealtMessage
			{
				victim = damageReport.victim.gameObject,
				damage = damageReport.damageDealt,
				attacker = damageInfo.attacker,
				position = damageInfo.position,
				crit = damageInfo.crit,
				damageType = damageInfo.damageType,
				damageColorIndex = damageInfo.damageColorIndex,
				hitLowHealth = damageReport.hitLowHealth
			});
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000A521C File Offset: 0x000A341C
		[NetworkMessageHandler(msgType = 60, client = true)]
		private static void HandleDamageDealt(NetworkMessage netMsg)
		{
			DamageDealtMessage damageDealtMessage = netMsg.ReadMessage<DamageDealtMessage>();
			if (damageDealtMessage.victim)
			{
				HealthComponent component = damageDealtMessage.victim.GetComponent<HealthComponent>();
				if (component && !NetworkServer.active)
				{
					component.UpdateLastHitTime(damageDealtMessage.damage, damageDealtMessage.position, damageDealtMessage.isSilent, damageDealtMessage.attacker);
				}
			}
			if (SettingsConVars.enableDamageNumbers.value && DamageNumberManager.instance)
			{
				TeamComponent teamComponent = null;
				if (damageDealtMessage.attacker)
				{
					teamComponent = damageDealtMessage.attacker.GetComponent<TeamComponent>();
				}
				DamageNumberManager.instance.SpawnDamageNumber(damageDealtMessage.damage, damageDealtMessage.position, damageDealtMessage.crit, teamComponent ? teamComponent.teamIndex : TeamIndex.None, damageDealtMessage.damageColorIndex);
			}
			GlobalEventManager.ClientDamageNotified(damageDealtMessage);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000A52E4 File Offset: 0x000A34E4
		private static void SendHeal(GameObject target, float amount, bool isCrit)
		{
			NetworkServer.SendToAll(61, new HealthComponent.HealMessage
			{
				target = target,
				amount = (isCrit ? (-amount) : amount)
			});
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000A5318 File Offset: 0x000A3518
		[NetworkMessageHandler(msgType = 61, client = true)]
		private static void HandleHeal(NetworkMessage netMsg)
		{
			HealthComponent.HealMessage healMessage = netMsg.ReadMessage<HealthComponent.HealMessage>();
			if (SettingsConVars.enableDamageNumbers.value && healMessage.target && DamageNumberManager.instance)
			{
				DamageNumberManager.instance.SpawnDamageNumber(healMessage.amount, Util.GetCorePosition(healMessage.target), healMessage.amount < 0f, TeamIndex.Player, DamageColorIndex.Heal);
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000A537C File Offset: 0x000A357C
		private void OnInventoryChanged()
		{
			this.itemCounts = default(HealthComponent.ItemCounts);
			Inventory inventory = this.body.inventory;
			this.itemCounts = (inventory ? new HealthComponent.ItemCounts(inventory) : default(HealthComponent.ItemCounts));
			this.currentEquipmentIndex = (inventory ? inventory.currentEquipmentIndex : EquipmentIndex.None);
			if (NetworkServer.active)
			{
				bool flag = this.itemCounts.repeatHeal != 0;
				if (flag != this.repeatHealComponent)
				{
					if (flag)
					{
						this.repeatHealComponent = base.gameObject.AddComponent<HealthComponent.RepeatHealComponent>();
						this.repeatHealComponent.healthComponent = this;
						return;
					}
					UnityEngine.Object.Destroy(this.repeatHealComponent);
					this.repeatHealComponent = null;
				}
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000A5430 File Offset: 0x000A3630
		public HealthComponent.HealthBarValues GetHealthBarValues()
		{
			float num = 1f - 1f / this.body.cursePenalty;
			float num2 = (1f - num) / this.fullCombinedHealth;
			float num3 = this.body.oneShotProtectionFraction * this.fullCombinedHealth - this.missingCombinedHealth;
			return new HealthComponent.HealthBarValues
			{
				hasInfusion = ((float)this.itemCounts.infusion > 0f),
				hasVoidShields = ((float)this.itemCounts.missileVoid > 0f),
				isElite = this.body.isElite,
				isBoss = this.body.isBoss,
				isVoid = ((this.body.bodyFlags & CharacterBody.BodyFlags.Void) > CharacterBody.BodyFlags.None),
				cullFraction = ((this.isInFrozenState && (this.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToExecutes) == CharacterBody.BodyFlags.None) ? Mathf.Clamp01(0.3f * this.fullCombinedHealth * num2) : 0f),
				healthFraction = Mathf.Clamp01(this.health * num2),
				shieldFraction = Mathf.Clamp01(this.shield * num2),
				barrierFraction = Mathf.Clamp01(this.barrier * num2),
				magneticFraction = Mathf.Clamp01(this.magnetiCharge * num2),
				curseFraction = num,
				ospFraction = num3 * num2,
				healthDisplayValue = (int)this.combinedHealth,
				maxHealthDisplayValue = (int)this.fullHealth
			};
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000A55D8 File Offset: 0x000A37D8
		static HealthComponent()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(HealthComponent), HealthComponent.kCmdCmdHealFull, new NetworkBehaviour.CmdDelegate(HealthComponent.InvokeCmdCmdHealFull));
			HealthComponent.kCmdCmdRechargeShieldFull = -833942624;
			NetworkBehaviour.RegisterCommandDelegate(typeof(HealthComponent), HealthComponent.kCmdCmdRechargeShieldFull, new NetworkBehaviour.CmdDelegate(HealthComponent.InvokeCmdCmdRechargeShieldFull));
			HealthComponent.kCmdCmdAddBarrier = -1976809257;
			NetworkBehaviour.RegisterCommandDelegate(typeof(HealthComponent), HealthComponent.kCmdCmdAddBarrier, new NetworkBehaviour.CmdDelegate(HealthComponent.InvokeCmdCmdAddBarrier));
			HealthComponent.kCmdCmdForceShieldRegen = -1029271894;
			NetworkBehaviour.RegisterCommandDelegate(typeof(HealthComponent), HealthComponent.kCmdCmdForceShieldRegen, new NetworkBehaviour.CmdDelegate(HealthComponent.InvokeCmdCmdForceShieldRegen));
			NetworkCRC.RegisterBehaviour("HealthComponent", 0);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000A56A8 File Offset: 0x000A38A8
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x000A56BB File Offset: 0x000A38BB
		public float Networkhealth
		{
			get
			{
				return this.health;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.health, 1U);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000A56D0 File Offset: 0x000A38D0
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x000A56E3 File Offset: 0x000A38E3
		public float Networkshield
		{
			get
			{
				return this.shield;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.shield, 2U);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000A56F8 File Offset: 0x000A38F8
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x000A570B File Offset: 0x000A390B
		public float Networkbarrier
		{
			get
			{
				return this.barrier;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.barrier, 4U);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000A5720 File Offset: 0x000A3920
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x000A5733 File Offset: 0x000A3933
		public float NetworkmagnetiCharge
		{
			get
			{
				return this.magnetiCharge;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.magnetiCharge, 8U);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000A5748 File Offset: 0x000A3948
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x000A575B File Offset: 0x000A395B
		public uint Network_killingDamageType
		{
			get
			{
				return this._killingDamageType;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this._killingDamageType, 16U);
			}
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000A576F File Offset: 0x000A396F
		protected static void InvokeCmdCmdHealFull(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdHealFull called on client.");
				return;
			}
			((HealthComponent)obj).CmdHealFull();
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000A5792 File Offset: 0x000A3992
		protected static void InvokeCmdCmdRechargeShieldFull(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdRechargeShieldFull called on client.");
				return;
			}
			((HealthComponent)obj).CmdRechargeShieldFull();
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000A57B5 File Offset: 0x000A39B5
		protected static void InvokeCmdCmdAddBarrier(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdAddBarrier called on client.");
				return;
			}
			((HealthComponent)obj).CmdAddBarrier(reader.ReadSingle());
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000A57DF File Offset: 0x000A39DF
		protected static void InvokeCmdCmdForceShieldRegen(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdForceShieldRegen called on client.");
				return;
			}
			((HealthComponent)obj).CmdForceShieldRegen();
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000A5804 File Offset: 0x000A3A04
		public void CallCmdHealFull()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdHealFull called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdHealFull();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)HealthComponent.kCmdCmdHealFull);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdHealFull");
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000A5880 File Offset: 0x000A3A80
		public void CallCmdRechargeShieldFull()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdRechargeShieldFull called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdRechargeShieldFull();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)HealthComponent.kCmdCmdRechargeShieldFull);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdRechargeShieldFull");
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000A58FC File Offset: 0x000A3AFC
		public void CallCmdAddBarrier(float value)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdAddBarrier called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdAddBarrier(value);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)HealthComponent.kCmdCmdAddBarrier);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(value);
			base.SendCommandInternal(networkWriter, 0, "CmdAddBarrier");
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000A5988 File Offset: 0x000A3B88
		public void CallCmdForceShieldRegen()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdForceShieldRegen called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdForceShieldRegen();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)HealthComponent.kCmdCmdForceShieldRegen);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdForceShieldRegen");
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000A5A04 File Offset: 0x000A3C04
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.health);
				writer.Write(this.shield);
				writer.Write(this.barrier);
				writer.Write(this.magnetiCharge);
				writer.WritePackedUInt32(this._killingDamageType);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.health);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.shield);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.barrier);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.magnetiCharge);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32(this._killingDamageType);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000A5B6C File Offset: 0x000A3D6C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.health = reader.ReadSingle();
				this.shield = reader.ReadSingle();
				this.barrier = reader.ReadSingle();
				this.magnetiCharge = reader.ReadSingle();
				this._killingDamageType = reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.health = reader.ReadSingle();
			}
			if ((num & 2) != 0)
			{
				this.shield = reader.ReadSingle();
			}
			if ((num & 4) != 0)
			{
				this.barrier = reader.ReadSingle();
			}
			if ((num & 8) != 0)
			{
				this.magnetiCharge = reader.ReadSingle();
			}
			if ((num & 16) != 0)
			{
				this._killingDamageType = reader.ReadPackedUInt32();
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002977 RID: 10615
		public static readonly float lowHealthFraction = 0.25f;

		// Token: 0x04002978 RID: 10616
		[Tooltip("How much health this object has.")]
		[HideInInspector]
		[SyncVar]
		public float health = 100f;

		// Token: 0x04002979 RID: 10617
		[Tooltip("How much shield this object has.")]
		[HideInInspector]
		[SyncVar]
		public float shield;

		// Token: 0x0400297A RID: 10618
		[Tooltip("How much barrier this object has.")]
		[SyncVar]
		[HideInInspector]
		public float barrier;

		// Token: 0x0400297B RID: 10619
		[SyncVar]
		[HideInInspector]
		public float magnetiCharge;

		// Token: 0x0400297C RID: 10620
		public bool dontShowHealthbar;

		// Token: 0x0400297D RID: 10621
		public float globalDeathEventChanceCoefficient = 1f;

		// Token: 0x0400297E RID: 10622
		[SyncVar]
		private uint _killingDamageType;

		// Token: 0x0400297F RID: 10623
		public CharacterBody body;

		// Token: 0x04002980 RID: 10624
		private ModelLocator modelLocator;

		// Token: 0x04002981 RID: 10625
		private IPainAnimationHandler painAnimationHandler;

		// Token: 0x04002982 RID: 10626
		private IOnIncomingDamageServerReceiver[] onIncomingDamageReceivers;

		// Token: 0x04002983 RID: 10627
		private IOnTakeDamageServerReceiver[] onTakeDamageReceivers;

		// Token: 0x0400298A RID: 10634
		public const float frozenExecuteThreshold = 0.3f;

		// Token: 0x0400298B RID: 10635
		private const float adaptiveArmorPerOnePercentTaken = 30f;

		// Token: 0x0400298C RID: 10636
		private const float adaptiveArmorDecayPerSecond = 40f;

		// Token: 0x0400298D RID: 10637
		private const float adaptiveArmorCap = 400f;

		// Token: 0x0400298E RID: 10638
		public const float medkitActivationDelay = 2f;

		// Token: 0x0400298F RID: 10639
		private const float devilOrbMaxTimer = 0.1f;

		// Token: 0x04002990 RID: 10640
		private float devilOrbHealPool;

		// Token: 0x04002991 RID: 10641
		private float devilOrbTimer;

		// Token: 0x04002992 RID: 10642
		private float regenAccumulator;

		// Token: 0x04002993 RID: 10643
		private bool wasAlive = true;

		// Token: 0x04002994 RID: 10644
		private float adaptiveArmorValue;

		// Token: 0x04002995 RID: 10645
		private bool isShieldRegenForced;

		// Token: 0x04002997 RID: 10647
		private float ospTimer;

		// Token: 0x04002998 RID: 10648
		private const float ospBufferDuration = 0.1f;

		// Token: 0x04002999 RID: 10649
		private float serverDamageTakenThisUpdate;

		// Token: 0x0400299A RID: 10650
		private HealthComponent.RepeatHealComponent repeatHealComponent;

		// Token: 0x0400299B RID: 10651
		private HealthComponent.ItemCounts itemCounts;

		// Token: 0x0400299C RID: 10652
		private EquipmentIndex currentEquipmentIndex;

		// Token: 0x0400299D RID: 10653
		private static int kCmdCmdHealFull = -290141736;

		// Token: 0x0400299E RID: 10654
		private static int kCmdCmdRechargeShieldFull;

		// Token: 0x0400299F RID: 10655
		private static int kCmdCmdAddBarrier;

		// Token: 0x040029A0 RID: 10656
		private static int kCmdCmdForceShieldRegen;

		// Token: 0x02000721 RID: 1825
		private static class AssetReferences
		{
			// Token: 0x06002600 RID: 9728 RVA: 0x000A5C44 File Offset: 0x000A3E44
			public static void Resolve()
			{
				HealthComponent.AssetReferences.bearEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc");
				HealthComponent.AssetReferences.bearVoidEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearVoidProc");
				HealthComponent.AssetReferences.executeEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OmniEffect/OmniImpactExecute");
				HealthComponent.AssetReferences.shieldBreakEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShieldBreakEffect");
				HealthComponent.AssetReferences.loseCoinsImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CoinImpact");
				HealthComponent.AssetReferences.gainCoinsImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/GainCoinsImpact");
				HealthComponent.AssetReferences.damageRejectedPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/DamageRejected");
				HealthComponent.AssetReferences.bossDamageBonusImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ImpactBossDamageBonus");
				HealthComponent.AssetReferences.pulverizedEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/PulverizedEffect");
				HealthComponent.AssetReferences.diamondDamageBonusImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/DiamondDamageBonusEffect");
				HealthComponent.AssetReferences.crowbarImpactEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ImpactCrowbar");
				HealthComponent.AssetReferences.captainBodyArmorBlockEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CaptainBodyArmorBlockEffect");
				HealthComponent.AssetReferences.permanentDebuffEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/PermanentDebuffEffect");
				HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/MercExposeConsumeEffect");
				HealthComponent.AssetReferences.critGlassesVoidExecuteEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/CritGlassesVoidExecuteEffect");
				HealthComponent.AssetReferences.explodeOnDeathVoidExplosionPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/ExplodeOnDeathVoidExplosion");
				HealthComponent.AssetReferences.fragileDamageBonusBreakEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/DelicateWatchProcEffect");
			}

			// Token: 0x040029A1 RID: 10657
			public static GameObject bearEffectPrefab;

			// Token: 0x040029A2 RID: 10658
			public static GameObject bearVoidEffectPrefab;

			// Token: 0x040029A3 RID: 10659
			public static GameObject executeEffectPrefab;

			// Token: 0x040029A4 RID: 10660
			public static GameObject critGlassesVoidExecuteEffectPrefab;

			// Token: 0x040029A5 RID: 10661
			public static GameObject shieldBreakEffectPrefab;

			// Token: 0x040029A6 RID: 10662
			public static GameObject loseCoinsImpactEffectPrefab;

			// Token: 0x040029A7 RID: 10663
			public static GameObject gainCoinsImpactEffectPrefab;

			// Token: 0x040029A8 RID: 10664
			public static GameObject damageRejectedPrefab;

			// Token: 0x040029A9 RID: 10665
			public static GameObject bossDamageBonusImpactEffectPrefab;

			// Token: 0x040029AA RID: 10666
			public static GameObject pulverizedEffectPrefab;

			// Token: 0x040029AB RID: 10667
			public static GameObject diamondDamageBonusImpactEffectPrefab;

			// Token: 0x040029AC RID: 10668
			public static GameObject crowbarImpactEffectPrefab;

			// Token: 0x040029AD RID: 10669
			public static GameObject captainBodyArmorBlockEffectPrefab;

			// Token: 0x040029AE RID: 10670
			public static GameObject mercExposeConsumeEffectPrefab;

			// Token: 0x040029AF RID: 10671
			public static GameObject explodeOnDeathVoidExplosionPrefab;

			// Token: 0x040029B0 RID: 10672
			public static GameObject fragileDamageBonusBreakEffectPrefab;

			// Token: 0x040029B1 RID: 10673
			public static GameObject permanentDebuffEffectPrefab;
		}

		// Token: 0x02000722 RID: 1826
		private class HealMessage : MessageBase
		{
			// Token: 0x06002602 RID: 9730 RVA: 0x000A5D50 File Offset: 0x000A3F50
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.target);
				writer.Write(this.amount);
			}

			// Token: 0x06002603 RID: 9731 RVA: 0x000A5D6A File Offset: 0x000A3F6A
			public override void Deserialize(NetworkReader reader)
			{
				this.target = reader.ReadGameObject();
				this.amount = reader.ReadSingle();
			}

			// Token: 0x040029B2 RID: 10674
			public GameObject target;

			// Token: 0x040029B3 RID: 10675
			public float amount;
		}

		// Token: 0x02000723 RID: 1827
		private struct ItemCounts
		{
			// Token: 0x06002604 RID: 9732 RVA: 0x000A5D84 File Offset: 0x000A3F84
			public ItemCounts([NotNull] Inventory src)
			{
				this.bear = src.GetItemCount(RoR2Content.Items.Bear);
				this.armorPlate = src.GetItemCount(RoR2Content.Items.ArmorPlate);
				this.goldOnHit = src.GetItemCount(RoR2Content.Items.GoldOnHit);
				this.goldOnHurt = src.GetItemCount(DLC1Content.Items.GoldOnHurt);
				this.phasing = src.GetItemCount(RoR2Content.Items.Phasing);
				this.thorns = src.GetItemCount(RoR2Content.Items.Thorns);
				this.invadingDoppelganger = src.GetItemCount(RoR2Content.Items.InvadingDoppelganger);
				this.medkit = src.GetItemCount(RoR2Content.Items.Medkit);
				this.fragileDamageBonus = src.GetItemCount(DLC1Content.Items.FragileDamageBonus);
				this.minHealthPercentage = src.GetItemCount(RoR2Content.Items.MinHealthPercentage);
				this.increaseHealing = src.GetItemCount(RoR2Content.Items.IncreaseHealing);
				this.barrierOnOverHeal = src.GetItemCount(RoR2Content.Items.BarrierOnOverHeal);
				this.repeatHeal = src.GetItemCount(RoR2Content.Items.RepeatHeal);
				this.novaOnHeal = src.GetItemCount(RoR2Content.Items.NovaOnHeal);
				this.adaptiveArmor = src.GetItemCount(RoR2Content.Items.AdaptiveArmor);
				this.healingPotion = src.GetItemCount(DLC1Content.Items.HealingPotion);
				this.infusion = src.GetItemCount(RoR2Content.Items.Infusion);
				this.parentEgg = src.GetItemCount(RoR2Content.Items.ParentEgg);
				this.missileVoid = src.GetItemCount(DLC1Content.Items.MissileVoid);
			}

			// Token: 0x040029B4 RID: 10676
			public int bear;

			// Token: 0x040029B5 RID: 10677
			public int armorPlate;

			// Token: 0x040029B6 RID: 10678
			public int goldOnHit;

			// Token: 0x040029B7 RID: 10679
			public int goldOnHurt;

			// Token: 0x040029B8 RID: 10680
			public int phasing;

			// Token: 0x040029B9 RID: 10681
			public int thorns;

			// Token: 0x040029BA RID: 10682
			public int invadingDoppelganger;

			// Token: 0x040029BB RID: 10683
			public int medkit;

			// Token: 0x040029BC RID: 10684
			public int parentEgg;

			// Token: 0x040029BD RID: 10685
			public int fragileDamageBonus;

			// Token: 0x040029BE RID: 10686
			public int minHealthPercentage;

			// Token: 0x040029BF RID: 10687
			public int increaseHealing;

			// Token: 0x040029C0 RID: 10688
			public int barrierOnOverHeal;

			// Token: 0x040029C1 RID: 10689
			public int repeatHeal;

			// Token: 0x040029C2 RID: 10690
			public int novaOnHeal;

			// Token: 0x040029C3 RID: 10691
			public int adaptiveArmor;

			// Token: 0x040029C4 RID: 10692
			public int healingPotion;

			// Token: 0x040029C5 RID: 10693
			public int infusion;

			// Token: 0x040029C6 RID: 10694
			public int missileVoid;
		}

		// Token: 0x02000724 RID: 1828
		public struct HealthBarValues
		{
			// Token: 0x040029C7 RID: 10695
			public bool hasInfusion;

			// Token: 0x040029C8 RID: 10696
			public bool hasVoidShields;

			// Token: 0x040029C9 RID: 10697
			public bool isVoid;

			// Token: 0x040029CA RID: 10698
			public bool isElite;

			// Token: 0x040029CB RID: 10699
			public bool isBoss;

			// Token: 0x040029CC RID: 10700
			public float cullFraction;

			// Token: 0x040029CD RID: 10701
			public float healthFraction;

			// Token: 0x040029CE RID: 10702
			public float shieldFraction;

			// Token: 0x040029CF RID: 10703
			public float barrierFraction;

			// Token: 0x040029D0 RID: 10704
			public float magneticFraction;

			// Token: 0x040029D1 RID: 10705
			public float curseFraction;

			// Token: 0x040029D2 RID: 10706
			public float ospFraction;

			// Token: 0x040029D3 RID: 10707
			public int healthDisplayValue;

			// Token: 0x040029D4 RID: 10708
			public int maxHealthDisplayValue;
		}

		// Token: 0x02000725 RID: 1829
		private class RepeatHealComponent : MonoBehaviour
		{
			// Token: 0x06002605 RID: 9733 RVA: 0x000A5ED4 File Offset: 0x000A40D4
			private void FixedUpdate()
			{
				this.timer -= Time.fixedDeltaTime;
				if (this.timer <= 0f)
				{
					this.timer = 0.2f;
					if (this.reserve > 0f)
					{
						float num = Mathf.Min(this.healthComponent.fullHealth * this.healthFractionToRestorePerSecond * 0.2f, this.reserve);
						this.reserve -= num;
						ProcChainMask procChainMask = default(ProcChainMask);
						procChainMask.AddProc(ProcType.RepeatHeal);
						this.healthComponent.Heal(num, procChainMask, true);
					}
				}
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x000A5F6A File Offset: 0x000A416A
			public void AddReserve(float amount, float max)
			{
				this.reserve = Mathf.Min(this.reserve + amount, max);
			}

			// Token: 0x040029D5 RID: 10709
			private float reserve;

			// Token: 0x040029D6 RID: 10710
			private float timer;

			// Token: 0x040029D7 RID: 10711
			private const float interval = 0.2f;

			// Token: 0x040029D8 RID: 10712
			public float healthFractionToRestorePerSecond = 0.1f;

			// Token: 0x040029D9 RID: 10713
			public HealthComponent healthComponent;
		}
	}
}
