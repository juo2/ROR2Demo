using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.Orbs;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200070E RID: 1806
	public class GlobalEventManager : MonoBehaviour
	{
		// Token: 0x0600252E RID: 9518 RVA: 0x0009DF5B File Offset: 0x0009C15B
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			GlobalEventManager.CommonAssets.Load();
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0009DF62 File Offset: 0x0009C162
		private void OnEnable()
		{
			if (GlobalEventManager.instance)
			{
				Debug.LogError("Only one GlobalEventManager can exist at a time.");
				return;
			}
			GlobalEventManager.instance = this;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0009DF81 File Offset: 0x0009C181
		private void OnDisable()
		{
			if (GlobalEventManager.instance == this)
			{
				GlobalEventManager.instance = null;
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0009DF98 File Offset: 0x0009C198
		public void OnHitEnemy([NotNull] DamageInfo damageInfo, [NotNull] GameObject victim)
		{
			if (damageInfo.procCoefficient == 0f || damageInfo.rejected)
			{
				return;
			}
			if (!NetworkServer.active)
			{
				return;
			}
			if (damageInfo.attacker && damageInfo.procCoefficient > 0f)
			{
				uint? maxStacksFromAttacker = null;
				if ((damageInfo != null) ? damageInfo.inflictor : null)
				{
					ProjectileDamage component = damageInfo.inflictor.GetComponent<ProjectileDamage>();
					if (component && component.useDotMaxStacksFromAttacker)
					{
						maxStacksFromAttacker = new uint?(component.dotMaxStacksFromAttacker);
					}
				}
				CharacterBody component2 = damageInfo.attacker.GetComponent<CharacterBody>();
				CharacterBody characterBody = victim ? victim.GetComponent<CharacterBody>() : null;
				if (component2)
				{
					if ((damageInfo.damageType & DamageType.PoisonOnHit) > DamageType.Generic)
					{
						DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Poison, 10f * damageInfo.procCoefficient, 1f, maxStacksFromAttacker);
					}
					CharacterMaster master = component2.master;
					if (master)
					{
						Inventory inventory = master.inventory;
						TeamComponent component3 = component2.GetComponent<TeamComponent>();
						TeamIndex teamIndex = component3 ? component3.teamIndex : TeamIndex.Neutral;
						Vector3 aimOrigin = component2.aimOrigin;
						if (damageInfo.crit)
						{
							GlobalEventManager.instance.OnCrit(component2, damageInfo, master, damageInfo.procCoefficient, damageInfo.procChainMask);
						}
						if (!damageInfo.procChainMask.HasProc(ProcType.HealOnHit))
						{
							int itemCount = inventory.GetItemCount(RoR2Content.Items.Seed);
							if (itemCount > 0)
							{
								HealthComponent component4 = component2.GetComponent<HealthComponent>();
								if (component4)
								{
									ProcChainMask procChainMask = damageInfo.procChainMask;
									procChainMask.AddProc(ProcType.HealOnHit);
									component4.Heal((float)itemCount * damageInfo.procCoefficient, procChainMask, true);
								}
							}
						}
						if (!damageInfo.procChainMask.HasProc(ProcType.BleedOnHit))
						{
							bool flag = (damageInfo.damageType & DamageType.BleedOnHit) > DamageType.Generic || (inventory.GetItemCount(RoR2Content.Items.BleedOnHitAndExplode) > 0 && damageInfo.crit);
							if ((component2.bleedChance > 0f || flag) && (flag || Util.CheckRoll(damageInfo.procCoefficient * component2.bleedChance, master)))
							{
								ProcChainMask procChainMask2 = damageInfo.procChainMask;
								procChainMask2.AddProc(ProcType.BleedOnHit);
								DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Bleed, 3f * damageInfo.procCoefficient, 1f, maxStacksFromAttacker);
							}
						}
						if (!damageInfo.procChainMask.HasProc(ProcType.FractureOnHit))
						{
							int num = inventory.GetItemCount(DLC1Content.Items.BleedOnHitVoid);
							num += (component2.HasBuff(DLC1Content.Buffs.EliteVoid) ? 10 : 0);
							if (num > 0 && Util.CheckRoll(damageInfo.procCoefficient * (float)num * 10f, master))
							{
								ProcChainMask procChainMask3 = damageInfo.procChainMask;
								procChainMask3.AddProc(ProcType.FractureOnHit);
								DotController.DotDef dotDef = DotController.GetDotDef(DotController.DotIndex.Fracture);
								DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Fracture, dotDef.interval, 1f, maxStacksFromAttacker);
							}
						}
						bool flag2 = (damageInfo.damageType & DamageType.BlightOnHit) > DamageType.Generic;
						if (flag2 && flag2)
						{
							ProcChainMask procChainMask4 = damageInfo.procChainMask;
							DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Blight, 5f * damageInfo.procCoefficient, 1f, maxStacksFromAttacker);
						}
						if ((damageInfo.damageType & DamageType.WeakOnHit) > DamageType.Generic)
						{
							characterBody.AddTimedBuff(RoR2Content.Buffs.Weak, 6f * damageInfo.procCoefficient);
						}
						if ((damageInfo.damageType & DamageType.IgniteOnHit) > DamageType.Generic || component2.HasBuff(RoR2Content.Buffs.AffixRed))
						{
							float num2 = 0.5f;
							InflictDotInfo inflictDotInfo = new InflictDotInfo
							{
								attackerObject = damageInfo.attacker,
								victimObject = victim,
								totalDamage = new float?(damageInfo.damage * num2),
								damageMultiplier = 1f,
								dotIndex = DotController.DotIndex.Burn,
								maxStacksFromAttacker = maxStacksFromAttacker
							};
							StrengthenBurnUtils.CheckDotForUpgrade(inventory, ref inflictDotInfo);
							DotController.InflictDot(ref inflictDotInfo);
						}
						int num3 = component2.HasBuff(RoR2Content.Buffs.AffixWhite) ? 1 : 0;
						num3 += (component2.HasBuff(RoR2Content.Buffs.AffixHaunted) ? 2 : 0);
						if (num3 > 0 && characterBody)
						{
							EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/AffixWhiteImpactEffect"), damageInfo.position, Vector3.up, true);
							characterBody.AddTimedBuff(RoR2Content.Buffs.Slow80, 1.5f * damageInfo.procCoefficient * (float)num3);
						}
						int itemCount2 = master.inventory.GetItemCount(RoR2Content.Items.SlowOnHit);
						if (itemCount2 > 0 && characterBody)
						{
							characterBody.AddTimedBuff(RoR2Content.Buffs.Slow60, 2f * (float)itemCount2);
						}
						int itemCount3 = master.inventory.GetItemCount(DLC1Content.Items.SlowOnHitVoid);
						if (itemCount3 > 0 && characterBody && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(5f * (float)itemCount3 * damageInfo.procCoefficient), master))
						{
							characterBody.AddTimedBuff(RoR2Content.Buffs.Nullified, 1f * (float)itemCount3);
						}
						if ((component2.HasBuff(RoR2Content.Buffs.AffixPoison) ? 1 : 0) > 0 && characterBody)
						{
							characterBody.AddTimedBuff(RoR2Content.Buffs.HealingDisabled, 8f * damageInfo.procCoefficient);
						}
						int itemCount4 = inventory.GetItemCount(RoR2Content.Items.GoldOnHit);
						if (itemCount4 > 0 && Util.CheckRoll(30f * damageInfo.procCoefficient, master))
						{
							GoldOrb goldOrb = new GoldOrb();
							goldOrb.origin = damageInfo.position;
							goldOrb.target = component2.mainHurtBox;
							goldOrb.goldAmount = (uint)((float)itemCount4 * 2f * Run.instance.difficultyCoefficient);
							OrbManager.instance.AddOrb(goldOrb);
							EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CoinImpact"), damageInfo.position, Vector3.up, true);
						}
						if (!damageInfo.procChainMask.HasProc(ProcType.Missile))
						{
							int itemCount5 = inventory.GetItemCount(RoR2Content.Items.Missile);
							if (itemCount5 > 0)
							{
								if (Util.CheckRoll(10f * damageInfo.procCoefficient, master))
								{
									float damageCoefficient = 3f * (float)itemCount5;
									float missileDamage = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient);
									MissileUtils.FireMissile(component2.corePosition, component2, damageInfo.procChainMask, victim, missileDamage, damageInfo.crit, GlobalEventManager.CommonAssets.missilePrefab, DamageColorIndex.Item, true);
								}
							}
							else
							{
								itemCount5 = inventory.GetItemCount(DLC1Content.Items.MissileVoid);
								if (itemCount5 > 0 && component2.healthComponent.shield > 0f)
								{
									int? num4;
									if (component2 == null)
									{
										num4 = null;
									}
									else
									{
										Inventory inventory2 = component2.inventory;
										num4 = ((inventory2 != null) ? new int?(inventory2.GetItemCount(DLC1Content.Items.MoreMissile)) : null);
									}
									int num5 = num4 ?? 0;
									float num6 = Mathf.Max(1f, 1f + 0.5f * (float)(num5 - 1));
									float damageCoefficient2 = 0.4f * (float)itemCount5;
									float damageValue = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient2) * num6;
									int num7 = (num5 > 0) ? 3 : 1;
									for (int i = 0; i < num7; i++)
									{
										MissileVoidOrb missileVoidOrb = new MissileVoidOrb();
										missileVoidOrb.origin = aimOrigin;
										missileVoidOrb.damageValue = damageValue;
										missileVoidOrb.isCrit = damageInfo.crit;
										missileVoidOrb.teamIndex = teamIndex;
										missileVoidOrb.attacker = damageInfo.attacker;
										missileVoidOrb.procChainMask = damageInfo.procChainMask;
										missileVoidOrb.procChainMask.AddProc(ProcType.Missile);
										missileVoidOrb.procCoefficient = 0.2f;
										missileVoidOrb.damageColorIndex = DamageColorIndex.Void;
										HurtBox mainHurtBox = characterBody.mainHurtBox;
										if (mainHurtBox)
										{
											missileVoidOrb.target = mainHurtBox;
											OrbManager.instance.AddOrb(missileVoidOrb);
										}
									}
								}
							}
						}
						if (component2.HasBuff(JunkContent.Buffs.LoaderPylonPowered) && !damageInfo.procChainMask.HasProc(ProcType.LoaderLightning))
						{
							float damageCoefficient3 = 0.3f;
							float damageValue2 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient3);
							LightningOrb lightningOrb = new LightningOrb();
							lightningOrb.origin = damageInfo.position;
							lightningOrb.damageValue = damageValue2;
							lightningOrb.isCrit = damageInfo.crit;
							lightningOrb.bouncesRemaining = 3;
							lightningOrb.teamIndex = teamIndex;
							lightningOrb.attacker = damageInfo.attacker;
							lightningOrb.bouncedObjects = new List<HealthComponent>
							{
								victim.GetComponent<HealthComponent>()
							};
							lightningOrb.procChainMask = damageInfo.procChainMask;
							lightningOrb.procChainMask.AddProc(ProcType.LoaderLightning);
							lightningOrb.procCoefficient = 0f;
							lightningOrb.lightningType = LightningOrb.LightningType.Loader;
							lightningOrb.damageColorIndex = DamageColorIndex.Item;
							lightningOrb.range = 20f;
							HurtBox hurtBox = lightningOrb.PickNextTarget(damageInfo.position);
							if (hurtBox)
							{
								lightningOrb.target = hurtBox;
								OrbManager.instance.AddOrb(lightningOrb);
							}
						}
						int itemCount6 = inventory.GetItemCount(RoR2Content.Items.ChainLightning);
						float num8 = 25f;
						if (itemCount6 > 0 && !damageInfo.procChainMask.HasProc(ProcType.ChainLightning) && Util.CheckRoll(num8 * damageInfo.procCoefficient, master))
						{
							float damageCoefficient4 = 0.8f;
							float damageValue3 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient4);
							LightningOrb lightningOrb2 = new LightningOrb();
							lightningOrb2.origin = damageInfo.position;
							lightningOrb2.damageValue = damageValue3;
							lightningOrb2.isCrit = damageInfo.crit;
							lightningOrb2.bouncesRemaining = 2 * itemCount6;
							lightningOrb2.teamIndex = teamIndex;
							lightningOrb2.attacker = damageInfo.attacker;
							lightningOrb2.bouncedObjects = new List<HealthComponent>
							{
								victim.GetComponent<HealthComponent>()
							};
							lightningOrb2.procChainMask = damageInfo.procChainMask;
							lightningOrb2.procChainMask.AddProc(ProcType.ChainLightning);
							lightningOrb2.procCoefficient = 0.2f;
							lightningOrb2.lightningType = LightningOrb.LightningType.Ukulele;
							lightningOrb2.damageColorIndex = DamageColorIndex.Item;
							lightningOrb2.range += (float)(2 * itemCount6);
							HurtBox hurtBox2 = lightningOrb2.PickNextTarget(damageInfo.position);
							if (hurtBox2)
							{
								lightningOrb2.target = hurtBox2;
								OrbManager.instance.AddOrb(lightningOrb2);
							}
						}
						int itemCount7 = inventory.GetItemCount(DLC1Content.Items.ChainLightningVoid);
						float num9 = 25f;
						if (itemCount7 > 0 && !damageInfo.procChainMask.HasProc(ProcType.ChainLightning) && Util.CheckRoll(num9 * damageInfo.procCoefficient, master))
						{
							float damageCoefficient5 = 0.6f;
							float damageValue4 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient5);
							VoidLightningOrb voidLightningOrb = new VoidLightningOrb();
							voidLightningOrb.origin = damageInfo.position;
							voidLightningOrb.damageValue = damageValue4;
							voidLightningOrb.isCrit = damageInfo.crit;
							voidLightningOrb.totalStrikes = 3 * itemCount7;
							voidLightningOrb.teamIndex = teamIndex;
							voidLightningOrb.attacker = damageInfo.attacker;
							voidLightningOrb.procChainMask = damageInfo.procChainMask;
							voidLightningOrb.procChainMask.AddProc(ProcType.ChainLightning);
							voidLightningOrb.procCoefficient = 0.2f;
							voidLightningOrb.damageColorIndex = DamageColorIndex.Void;
							voidLightningOrb.secondsPerStrike = 0.1f;
							HurtBox mainHurtBox2 = characterBody.mainHurtBox;
							if (mainHurtBox2)
							{
								voidLightningOrb.target = mainHurtBox2;
								OrbManager.instance.AddOrb(voidLightningOrb);
							}
						}
						int itemCount8 = inventory.GetItemCount(RoR2Content.Items.BounceNearby);
						if (itemCount8 > 0)
						{
							float num10 = (1f - 100f / (100f + 20f * (float)itemCount8)) * 100f;
							if (!damageInfo.procChainMask.HasProc(ProcType.BounceNearby) && Util.CheckRoll(num10 * damageInfo.procCoefficient, master))
							{
								List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
								int maxTargets = 5 + itemCount8 * 5;
								BullseyeSearch search = new BullseyeSearch();
								List<HealthComponent> list2 = CollectionPool<HealthComponent, List<HealthComponent>>.RentCollection();
								if (component2 && component2.healthComponent)
								{
									list2.Add(component2.healthComponent);
								}
								if (characterBody && characterBody.healthComponent)
								{
									list2.Add(characterBody.healthComponent);
								}
								BounceOrb.SearchForTargets(search, teamIndex, damageInfo.position, 30f, maxTargets, list, list2);
								CollectionPool<HealthComponent, List<HealthComponent>>.ReturnCollection(list2);
								List<HealthComponent> bouncedObjects = new List<HealthComponent>
								{
									victim.GetComponent<HealthComponent>()
								};
								float damageCoefficient6 = 1f;
								float damageValue5 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient6);
								int j = 0;
								int count = list.Count;
								while (j < count)
								{
									HurtBox hurtBox3 = list[j];
									if (hurtBox3)
									{
										BounceOrb bounceOrb = new BounceOrb();
										bounceOrb.origin = damageInfo.position;
										bounceOrb.damageValue = damageValue5;
										bounceOrb.isCrit = damageInfo.crit;
										bounceOrb.teamIndex = teamIndex;
										bounceOrb.attacker = damageInfo.attacker;
										bounceOrb.procChainMask = damageInfo.procChainMask;
										bounceOrb.procChainMask.AddProc(ProcType.BounceNearby);
										bounceOrb.procCoefficient = 0.33f;
										bounceOrb.damageColorIndex = DamageColorIndex.Item;
										bounceOrb.bouncedObjects = bouncedObjects;
										bounceOrb.target = hurtBox3;
										OrbManager.instance.AddOrb(bounceOrb);
									}
									j++;
								}
								CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
							}
						}
						int itemCount9 = inventory.GetItemCount(RoR2Content.Items.StickyBomb);
						if (itemCount9 > 0 && Util.CheckRoll(5f * (float)itemCount9 * damageInfo.procCoefficient, master) && characterBody)
						{
							bool alive = characterBody.healthComponent.alive;
							float num11 = 5f;
							Vector3 position = damageInfo.position;
							Vector3 forward = characterBody.corePosition - position;
							float magnitude = forward.magnitude;
							Quaternion rotation = (magnitude != 0f) ? Util.QuaternionSafeLookRotation(forward) : UnityEngine.Random.rotationUniform;
							float damageCoefficient7 = 1.8f;
							float damage = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient7);
							ProjectileManager.instance.FireProjectile(LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/StickyBomb"), position, rotation, damageInfo.attacker, damage, 100f, damageInfo.crit, DamageColorIndex.Item, null, alive ? (magnitude * num11) : -1f);
						}
						if (!damageInfo.procChainMask.HasProc(ProcType.Rings) && damageInfo.damage / component2.damage >= 4f)
						{
							if (component2.HasBuff(RoR2Content.Buffs.ElementalRingsReady))
							{
								int itemCount10 = inventory.GetItemCount(RoR2Content.Items.IceRing);
								int itemCount11 = inventory.GetItemCount(RoR2Content.Items.FireRing);
								component2.RemoveBuff(RoR2Content.Buffs.ElementalRingsReady);
								int num12 = 1;
								while ((float)num12 <= 10f)
								{
									component2.AddTimedBuff(RoR2Content.Buffs.ElementalRingsCooldown, (float)num12);
									num12++;
								}
								ProcChainMask procChainMask5 = damageInfo.procChainMask;
								procChainMask5.AddProc(ProcType.Rings);
								Vector3 position2 = damageInfo.position;
								if (itemCount10 > 0)
								{
									float damageCoefficient8 = 2.5f * (float)itemCount10;
									float damage2 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient8);
									DamageInfo damageInfo2 = new DamageInfo
									{
										damage = damage2,
										damageColorIndex = DamageColorIndex.Item,
										damageType = DamageType.Generic,
										attacker = damageInfo.attacker,
										crit = damageInfo.crit,
										force = Vector3.zero,
										inflictor = null,
										position = position2,
										procChainMask = procChainMask5,
										procCoefficient = 1f
									};
									EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/IceRingExplosion"), position2, Vector3.up, true);
									characterBody.AddTimedBuff(RoR2Content.Buffs.Slow80, 3f * (float)itemCount10);
									characterBody.healthComponent.TakeDamage(damageInfo2);
								}
								if (itemCount11 > 0)
								{
									GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/FireTornado");
									float resetInterval = gameObject.GetComponent<ProjectileOverlapAttack>().resetInterval;
									float lifetime = gameObject.GetComponent<ProjectileSimple>().lifetime;
									float damageCoefficient9 = 3f * (float)itemCount11;
									float damage3 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient9) / lifetime * resetInterval;
									float speedOverride = 0f;
									Quaternion rotation2 = Quaternion.identity;
									Vector3 vector = position2 - aimOrigin;
									vector.y = 0f;
									if (vector != Vector3.zero)
									{
										speedOverride = -1f;
										rotation2 = Util.QuaternionSafeLookRotation(vector, Vector3.up);
									}
									ProjectileManager.instance.FireProjectile(new FireProjectileInfo
									{
										damage = damage3,
										crit = damageInfo.crit,
										damageColorIndex = DamageColorIndex.Item,
										position = position2,
										procChainMask = procChainMask5,
										force = 0f,
										owner = damageInfo.attacker,
										projectilePrefab = gameObject,
										rotation = rotation2,
										speedOverride = speedOverride,
										target = null
									});
								}
							}
							else if (component2.HasBuff(DLC1Content.Buffs.ElementalRingVoidReady))
							{
								int itemCount12 = inventory.GetItemCount(DLC1Content.Items.ElementalRingVoid);
								component2.RemoveBuff(DLC1Content.Buffs.ElementalRingVoidReady);
								int num13 = 1;
								while ((float)num13 <= 20f)
								{
									component2.AddTimedBuff(DLC1Content.Buffs.ElementalRingVoidCooldown, (float)num13);
									num13++;
								}
								ProcChainMask procChainMask6 = damageInfo.procChainMask;
								procChainMask6.AddProc(ProcType.Rings);
								if (itemCount12 > 0)
								{
									GameObject projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/ElementalRingVoidBlackHole");
									float damageCoefficient10 = 1f * (float)itemCount12;
									float damage4 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient10);
									ProjectileManager.instance.FireProjectile(new FireProjectileInfo
									{
										damage = damage4,
										crit = damageInfo.crit,
										damageColorIndex = DamageColorIndex.Void,
										position = damageInfo.position,
										procChainMask = procChainMask6,
										force = 6000f,
										owner = damageInfo.attacker,
										projectilePrefab = projectilePrefab,
										rotation = Quaternion.identity,
										target = null
									});
								}
							}
						}
						int itemCount13 = master.inventory.GetItemCount(RoR2Content.Items.DeathMark);
						int num14 = 0;
						if (itemCount13 >= 1 && !characterBody.HasBuff(RoR2Content.Buffs.DeathMark))
						{
							foreach (BuffIndex buffType in BuffCatalog.debuffBuffIndices)
							{
								if (characterBody.HasBuff(buffType))
								{
									num14++;
								}
							}
							DotController dotController = DotController.FindDotController(victim.gameObject);
							if (dotController)
							{
								for (DotController.DotIndex dotIndex = DotController.DotIndex.Bleed; dotIndex < DotController.DotIndex.Count; dotIndex++)
								{
									if (dotController.HasDotActive(dotIndex))
									{
										num14++;
									}
								}
							}
							if (num14 >= 4)
							{
								characterBody.AddTimedBuff(RoR2Content.Buffs.DeathMark, 7f * (float)itemCount13);
							}
						}
						if (damageInfo != null && damageInfo.inflictor != null && damageInfo.inflictor.GetComponent<BoomerangProjectile>() != null && !damageInfo.procChainMask.HasProc(ProcType.BleedOnHit))
						{
							int num15 = 0;
							if (inventory.GetEquipmentIndex() == RoR2Content.Equipment.Saw.equipmentIndex)
							{
								num15 = 1;
							}
							bool flag3 = (damageInfo.damageType & DamageType.BleedOnHit) > DamageType.Generic;
							if ((num15 > 0 || flag3) && (flag3 || Util.CheckRoll(100f, master)))
							{
								ProcChainMask procChainMask7 = damageInfo.procChainMask;
								procChainMask7.AddProc(ProcType.BleedOnHit);
								DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.Bleed, 4f * damageInfo.procCoefficient, 1f, maxStacksFromAttacker);
							}
						}
						if (damageInfo.crit && (damageInfo.damageType & DamageType.SuperBleedOnCrit) != DamageType.Generic)
						{
							DotController.InflictDot(victim, damageInfo.attacker, DotController.DotIndex.SuperBleed, 15f * damageInfo.procCoefficient, 1f, maxStacksFromAttacker);
						}
						if (component2.HasBuff(RoR2Content.Buffs.LifeSteal))
						{
							float amount = damageInfo.damage * 0.2f;
							component2.healthComponent.Heal(amount, damageInfo.procChainMask, true);
						}
						int itemCount14 = inventory.GetItemCount(RoR2Content.Items.FireballsOnHit);
						if (itemCount14 > 0 && !damageInfo.procChainMask.HasProc(ProcType.Meatball))
						{
							InputBankTest component5 = component2.GetComponent<InputBankTest>();
							Vector3 vector2 = characterBody.characterMotor ? (victim.transform.position + Vector3.up * (characterBody.characterMotor.capsuleHeight * 0.5f + 2f)) : (victim.transform.position + Vector3.up * 2f);
							Vector3 forward2 = component5 ? component5.aimDirection : victim.transform.forward;
							forward2 = Vector3.up;
							float num16 = 20f;
							if (Util.CheckRoll(10f * damageInfo.procCoefficient, master))
							{
								EffectData effectData = new EffectData
								{
									scale = 1f,
									origin = vector2
								};
								EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MuzzleFlashes/MuzzleflashFireMeatBall"), effectData, true);
								int num17 = 3;
								float damageCoefficient11 = 3f * (float)itemCount14;
								float damage5 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, damageCoefficient11);
								float min = 15f;
								float max = 30f;
								ProcChainMask procChainMask8 = damageInfo.procChainMask;
								procChainMask8.AddProc(ProcType.Meatball);
								float speedOverride2 = UnityEngine.Random.Range(min, max);
								float num18 = (float)(360 / num17);
								float num19 = num18 / 360f;
								float num20 = 1f;
								float num21 = num18;
								for (int l = 0; l < num17; l++)
								{
									float num22 = (float)l * 3.1415927f * 2f / (float)num17;
									FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
									{
										projectilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/FireMeatBall"),
										position = vector2 + new Vector3(num20 * Mathf.Sin(num22), 0f, num20 * Mathf.Cos(num22)),
										rotation = Util.QuaternionSafeLookRotation(forward2),
										procChainMask = procChainMask8,
										target = victim,
										owner = component2.gameObject,
										damage = damage5,
										crit = damageInfo.crit,
										force = 200f,
										damageColorIndex = DamageColorIndex.Item,
										speedOverride = speedOverride2,
										useSpeedOverride = true
									};
									num21 += num18;
									ProjectileManager.instance.FireProjectile(fireProjectileInfo);
									forward2.x += Mathf.Sin(num22 + UnityEngine.Random.Range(-num16, num16));
									forward2.z += Mathf.Cos(num22 + UnityEngine.Random.Range(-num16, num16));
								}
							}
						}
						int itemCount15 = inventory.GetItemCount(RoR2Content.Items.LightningStrikeOnHit);
						if (itemCount15 > 0 && !damageInfo.procChainMask.HasProc(ProcType.LightningStrikeOnHit) && Util.CheckRoll(10f * damageInfo.procCoefficient, master))
						{
							float damageValue6 = Util.OnHitProcDamage(damageInfo.damage, component2.damage, 5f * (float)itemCount15);
							ProcChainMask procChainMask9 = damageInfo.procChainMask;
							procChainMask9.AddProc(ProcType.LightningStrikeOnHit);
							HurtBox target = characterBody.mainHurtBox;
							if (characterBody.hurtBoxGroup)
							{
								target = characterBody.hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, characterBody.hurtBoxGroup.hurtBoxes.Length)];
							}
							OrbManager.instance.AddOrb(new SimpleLightningStrikeOrb
							{
								attacker = component2.gameObject,
								damageColorIndex = DamageColorIndex.Item,
								damageValue = damageValue6,
								isCrit = Util.CheckRoll(component2.crit, master),
								procChainMask = procChainMask9,
								procCoefficient = 1f,
								target = target
							});
						}
						if ((damageInfo.damageType & DamageType.LunarSecondaryRootOnHit) != DamageType.Generic && characterBody)
						{
							int itemCount16 = master.inventory.GetItemCount(RoR2Content.Items.LunarSecondaryReplacement);
							characterBody.AddTimedBuff(RoR2Content.Buffs.LunarSecondaryRoot, 3f * (float)itemCount16);
						}
						if ((damageInfo.damageType & DamageType.FruitOnHit) != DamageType.Generic && characterBody)
						{
							characterBody.AddTimedBuff(RoR2Content.Buffs.Fruiting, 10f);
						}
						if (inventory.GetItemCount(DLC1Content.Items.DroneWeaponsBoost) > 0)
						{
							DroneWeaponsBoostBehavior component6 = component2.GetComponent<DroneWeaponsBoostBehavior>();
							if (component6)
							{
								component6.OnEnemyHit(damageInfo, characterBody);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0009F668 File Offset: 0x0009D868
		public void OnCharacterHitGroundServer(CharacterBody characterBody, Vector3 impactVelocity)
		{
			bool flag = RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.weakAssKneesArtifactDef);
			float num = Mathf.Abs(impactVelocity.y);
			Inventory inventory = characterBody.inventory;
			CharacterMaster master = characterBody.master;
			CharacterMotor characterMotor = characterBody.characterMotor;
			bool flag2 = false;
			if ((inventory ? inventory.GetItemCount(RoR2Content.Items.FallBoots) : 0) <= 0 && (characterBody.bodyFlags & CharacterBody.BodyFlags.IgnoreFallDamage) == CharacterBody.BodyFlags.None)
			{
				float num2 = Mathf.Max(num - (characterBody.jumpPower + 20f), 0f);
				if (num2 > 0f)
				{
					HealthComponent component = characterBody.GetComponent<HealthComponent>();
					if (component)
					{
						flag2 = true;
						float num3 = num2 / 60f;
						DamageInfo damageInfo = new DamageInfo();
						damageInfo.attacker = null;
						damageInfo.inflictor = null;
						damageInfo.crit = false;
						damageInfo.damage = num3 * characterBody.maxHealth;
						damageInfo.damageType = (DamageType.NonLethal | DamageType.FallDamage);
						damageInfo.force = Vector3.zero;
						damageInfo.position = characterBody.footPosition;
						damageInfo.procCoefficient = 0f;
						if (flag || (characterBody.teamComponent.teamIndex == TeamIndex.Player && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse3))
						{
							damageInfo.damage *= 2f;
							damageInfo.damageType &= ~DamageType.NonLethal;
							damageInfo.damageType |= DamageType.BypassOneShotProtection;
						}
						component.TakeDamage(damageInfo);
					}
				}
			}
			if (characterMotor && Run.FixedTimeStamp.now - characterMotor.lastGroundedTime > 0.2f)
			{
				Vector3 footPosition = characterBody.footPosition;
				float radius = characterBody.radius;
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(footPosition + Vector3.up * 1.5f, Vector3.down), out raycastHit, 4f, LayerIndex.world.mask | LayerIndex.water.mask, QueryTriggerInteraction.Collide))
				{
					SurfaceDef objectSurfaceDef = SurfaceDefProvider.GetObjectSurfaceDef(raycastHit.collider, raycastHit.point);
					if (objectSurfaceDef)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CharacterLandImpact"), new EffectData
						{
							origin = footPosition,
							scale = radius,
							color = objectSurfaceDef.approximateColor
						}, true);
						if (objectSurfaceDef.footstepEffectPrefab)
						{
							EffectManager.SpawnEffect(objectSurfaceDef.footstepEffectPrefab, new EffectData
							{
								origin = raycastHit.point,
								scale = radius * 3f
							}, false);
						}
						SfxLocator component2 = characterBody.GetComponent<SfxLocator>();
						if (component2)
						{
							if (objectSurfaceDef.materialSwitchString != null && objectSurfaceDef.materialSwitchString.Length > 0)
							{
								AkSoundEngine.SetSwitch("material", objectSurfaceDef.materialSwitchString, characterBody.gameObject);
							}
							else
							{
								AkSoundEngine.SetSwitch("material", "dirt", characterBody.gameObject);
							}
							Util.PlaySound(component2.landingSound, characterBody.gameObject);
							if (flag2)
							{
								Util.PlaySound(component2.fallDamageSound, characterBody.gameObject);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0009F97A File Offset: 0x0009DB7A
		[Obsolete("Use OnCharacterHitGroundServer instead, which this is just a backwards-compatibility wrapper for.", false)]
		public void OnCharacterHitGround(CharacterBody characterBody, Vector3 impactVelocity)
		{
			this.OnCharacterHitGroundServer(characterBody, impactVelocity);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0009F984 File Offset: 0x0009DB84
		private void OnPlayerCharacterDeath(DamageReport damageReport, NetworkUser victimNetworkUser)
		{
			if (!victimNetworkUser)
			{
				return;
			}
			CharacterBody victimBody = damageReport.victimBody;
			string text;
			if ((damageReport.damageInfo.damageType & DamageType.VoidDeath) != DamageType.Generic)
			{
				text = "PLAYER_DEATH_QUOTE_VOIDDEATH";
			}
			else if (damageReport.isFallDamage)
			{
				text = GlobalEventManager.fallDamageDeathQuoteTokens[UnityEngine.Random.Range(0, GlobalEventManager.fallDamageDeathQuoteTokens.Length)];
			}
			else if (victimBody && victimBody.inventory && victimBody.inventory.GetItemCount(RoR2Content.Items.LunarDagger) > 0)
			{
				text = "PLAYER_DEATH_QUOTE_BRITTLEDEATH";
			}
			else
			{
				text = GlobalEventManager.standardDeathQuoteTokens[UnityEngine.Random.Range(0, GlobalEventManager.standardDeathQuoteTokens.Length)];
			}
			if (victimNetworkUser.masterController)
			{
				victimNetworkUser.masterController.finalMessageTokenServer = text;
			}
			Chat.SendBroadcastChat(new Chat.PlayerDeathChatMessage
			{
				subjectAsNetworkUser = victimNetworkUser,
				baseToken = text
			});
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002535 RID: 9525 RVA: 0x0009FA50 File Offset: 0x0009DC50
		// (remove) Token: 0x06002536 RID: 9526 RVA: 0x0009FA84 File Offset: 0x0009DC84
		public static event Action<DamageReport> onCharacterDeathGlobal;

		// Token: 0x06002537 RID: 9527 RVA: 0x0009FAB8 File Offset: 0x0009DCB8
		private static void ProcIgniteOnKill(DamageReport damageReport, int igniteOnKillCount, CharacterBody victimBody, TeamIndex attackerTeamIndex)
		{
			float num = 8f + 4f * (float)igniteOnKillCount;
			float radius = victimBody.radius;
			float num2 = num + radius;
			float num3 = 1.5f;
			float baseDamage = damageReport.attackerBody.damage * num3;
			Vector3 corePosition = victimBody.corePosition;
			GlobalEventManager.igniteOnKillSphereSearch.origin = corePosition;
			GlobalEventManager.igniteOnKillSphereSearch.mask = LayerIndex.entityPrecise.mask;
			GlobalEventManager.igniteOnKillSphereSearch.radius = num2;
			GlobalEventManager.igniteOnKillSphereSearch.RefreshCandidates();
			GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(attackerTeamIndex));
			GlobalEventManager.igniteOnKillSphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
			GlobalEventManager.igniteOnKillSphereSearch.OrderCandidatesByDistance();
			GlobalEventManager.igniteOnKillSphereSearch.GetHurtBoxes(GlobalEventManager.igniteOnKillHurtBoxBuffer);
			GlobalEventManager.igniteOnKillSphereSearch.ClearCandidates();
			float value = (float)(1 + igniteOnKillCount) * 0.75f * damageReport.attackerBody.damage;
			for (int i = 0; i < GlobalEventManager.igniteOnKillHurtBoxBuffer.Count; i++)
			{
				HurtBox hurtBox = GlobalEventManager.igniteOnKillHurtBoxBuffer[i];
				if (hurtBox.healthComponent)
				{
					InflictDotInfo inflictDotInfo = new InflictDotInfo
					{
						victimObject = hurtBox.healthComponent.gameObject,
						attackerObject = damageReport.attacker,
						totalDamage = new float?(value),
						dotIndex = DotController.DotIndex.Burn,
						damageMultiplier = 1f
					};
					UnityEngine.Object exists;
					if (damageReport == null)
					{
						exists = null;
					}
					else
					{
						CharacterMaster attackerMaster = damageReport.attackerMaster;
						exists = ((attackerMaster != null) ? attackerMaster.inventory : null);
					}
					if (exists)
					{
						StrengthenBurnUtils.CheckDotForUpgrade(damageReport.attackerMaster.inventory, ref inflictDotInfo);
					}
					DotController.InflictDot(ref inflictDotInfo);
				}
			}
			GlobalEventManager.igniteOnKillHurtBoxBuffer.Clear();
			new BlastAttack
			{
				radius = num2,
				baseDamage = baseDamage,
				procCoefficient = 0f,
				crit = Util.CheckRoll(damageReport.attackerBody.crit, damageReport.attackerMaster),
				damageColorIndex = DamageColorIndex.Item,
				attackerFiltering = AttackerFiltering.Default,
				falloffModel = BlastAttack.FalloffModel.None,
				attacker = damageReport.attacker,
				teamIndex = attackerTeamIndex,
				position = corePosition
			}.Fire();
			EffectManager.SpawnEffect(GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab, new EffectData
			{
				origin = corePosition,
				scale = num2,
				rotation = Util.QuaternionSafeLookRotation(damageReport.damageInfo.force)
			}, true);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0009FD00 File Offset: 0x0009DF00
		public void OnCharacterDeath(DamageReport damageReport)
		{
			if (!NetworkServer.active || damageReport == null)
			{
				return;
			}
			DamageInfo damageInfo = damageReport.damageInfo;
			GameObject gameObject = null;
			if (damageReport.victim)
			{
				gameObject = damageReport.victim.gameObject;
			}
			CharacterBody victimBody = damageReport.victimBody;
			TeamComponent teamComponent = null;
			CharacterMaster victimMaster = damageReport.victimMaster;
			TeamIndex teamIndex = damageReport.victimTeamIndex;
			Vector3 vector = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Vector3 vector2 = Vector3.zero;
			Transform transform = gameObject.transform;
			if (transform)
			{
				vector = transform.position;
				rotation = transform.rotation;
				vector2 = vector;
			}
			InputBankTest inputBankTest = null;
			EquipmentIndex equipmentIndex = EquipmentIndex.None;
			EquipmentDef equipmentDef = null;
			if (victimBody)
			{
				teamComponent = victimBody.teamComponent;
				inputBankTest = victimBody.inputBank;
				vector2 = victimBody.corePosition;
				if (victimBody.equipmentSlot)
				{
					equipmentIndex = victimBody.equipmentSlot.equipmentIndex;
					equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
				}
			}
			Ray ray = inputBankTest ? inputBankTest.GetAimRay() : new Ray(vector, rotation * Vector3.forward);
			GameObject attacker = damageReport.attacker;
			CharacterBody attackerBody = damageReport.attackerBody;
			CharacterMaster attackerMaster = damageReport.attackerMaster;
			Inventory inventory = attackerMaster ? attackerMaster.inventory : null;
			TeamIndex attackerTeamIndex = damageReport.attackerTeamIndex;
			if (teamComponent)
			{
				teamIndex = teamComponent.teamIndex;
			}
			if (victimBody && victimMaster)
			{
				PlayerCharacterMasterController playerCharacterMasterController = victimMaster.playerCharacterMasterController;
				if (playerCharacterMasterController)
				{
					NetworkUser networkUser = playerCharacterMasterController.networkUser;
					if (networkUser)
					{
						this.OnPlayerCharacterDeath(damageReport, networkUser);
					}
				}
				if (victimBody.HasBuff(RoR2Content.Buffs.AffixWhite))
				{
					Vector3 position = vector2;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
					float num = 12f + victimBody.radius;
					gameObject2.transform.localScale = new Vector3(num, num, num);
					DelayBlast component = gameObject2.GetComponent<DelayBlast>();
					if (component)
					{
						component.position = position;
						component.baseDamage = victimBody.damage * 1.5f;
						component.baseForce = 2300f;
						component.attacker = gameObject;
						component.radius = num;
						component.crit = Util.CheckRoll(victimBody.crit, victimMaster);
						component.procCoefficient = 0.75f;
						component.maxTimer = 2f;
						component.falloffModel = BlastAttack.FalloffModel.None;
						component.explosionEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/AffixWhiteExplosion");
						component.delayEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/AffixWhiteDelayEffect");
						component.damageType = DamageType.Freeze2s;
						TeamFilter component2 = gameObject2.GetComponent<TeamFilter>();
						if (component2)
						{
							component2.teamIndex = TeamComponent.GetObjectTeam(component.attacker);
						}
					}
				}
				if (victimBody.HasBuff(RoR2Content.Buffs.AffixPoison))
				{
					Vector3 position2 = vector2;
					Quaternion rotation2 = Quaternion.LookRotation(ray.direction);
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/UrchinTurretMaster"), position2, rotation2);
					CharacterMaster component3 = gameObject3.GetComponent<CharacterMaster>();
					if (component3)
					{
						component3.teamIndex = teamIndex;
						NetworkServer.Spawn(gameObject3);
						component3.SpawnBodyHere();
					}
				}
				if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.wispOnDeath) && teamIndex == TeamIndex.Monster && victimMaster.masterIndex != GlobalEventManager.CommonAssets.wispSoulMasterPrefabMasterComponent.masterIndex)
				{
					new MasterSummon
					{
						position = vector2,
						ignoreTeamMemberLimit = true,
						masterPrefab = GlobalEventManager.CommonAssets.wispSoulMasterPrefabMasterComponent.gameObject,
						summonerBodyObject = gameObject,
						rotation = Quaternion.LookRotation(ray.direction)
					}.Perform();
				}
				if (victimBody.HasBuff(RoR2Content.Buffs.Fruiting) || (damageReport.damageInfo != null && (damageReport.damageInfo.damageType & DamageType.FruitOnHit) > DamageType.Generic))
				{
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TreebotFruitDeathEffect.prefab"), new EffectData
					{
						origin = vector,
						rotation = UnityEngine.Random.rotation
					}, true);
					int num2 = Mathf.Min(Math.Max(1, (int)(victimBody.bestFitRadius * 2f)), 8);
					GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/TreebotFruitPack");
					for (int i = 0; i < num2; i++)
					{
						GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(original, vector + UnityEngine.Random.insideUnitSphere * victimBody.radius * 0.5f, UnityEngine.Random.rotation);
						TeamFilter component4 = gameObject4.GetComponent<TeamFilter>();
						if (component4)
						{
							component4.teamIndex = attackerTeamIndex;
						}
						gameObject4.GetComponentInChildren<HealthPickup>();
						gameObject4.transform.localScale = new Vector3(1f, 1f, 1f);
						NetworkServer.Spawn(gameObject4);
					}
				}
				if (victimBody.HasBuff(DLC1Content.Buffs.EliteEarth))
				{
					new MasterSummon
					{
						position = vector2,
						ignoreTeamMemberLimit = true,
						masterPrefab = GlobalEventManager.CommonAssets.eliteEarthHealerMaster,
						summonerBodyObject = gameObject,
						rotation = Quaternion.LookRotation(ray.direction)
					}.Perform();
				}
				if (victimBody.HasBuff(DLC1Content.Buffs.EliteVoid) && (!victimMaster || victimMaster.IsDeadAndOutOfLivesServer()))
				{
					Vector3 position3 = vector2;
					GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteVoid/VoidInfestorMaster.prefab").WaitForCompletion(), position3, Quaternion.identity);
					CharacterMaster component5 = gameObject5.GetComponent<CharacterMaster>();
					if (component5)
					{
						component5.teamIndex = TeamIndex.Void;
						NetworkServer.Spawn(gameObject5);
						component5.SpawnBodyHere();
					}
				}
			}
			if (attackerBody)
			{
				attackerBody.HandleOnKillEffectsServer(damageReport);
				if (attackerMaster && inventory)
				{
					int itemCount = inventory.GetItemCount(RoR2Content.Items.IgniteOnKill);
					if (itemCount > 0)
					{
						GlobalEventManager.ProcIgniteOnKill(damageReport, itemCount, victimBody, attackerTeamIndex);
					}
					int itemCount2 = inventory.GetItemCount(RoR2Content.Items.ExplodeOnDeath);
					if (itemCount2 > 0)
					{
						Vector3 position4 = vector2;
						float damageCoefficient = 3.5f * (1f + (float)(itemCount2 - 1) * 0.8f);
						float baseDamage = Util.OnKillProcDamage(attackerBody.damage, damageCoefficient);
						GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(GlobalEventManager.CommonAssets.explodeOnDeathPrefab, position4, Quaternion.identity);
						DelayBlast component6 = gameObject6.GetComponent<DelayBlast>();
						if (component6)
						{
							component6.position = position4;
							component6.baseDamage = baseDamage;
							component6.baseForce = 2000f;
							component6.bonusForce = Vector3.up * 1000f;
							component6.radius = 12f + 2.4f * ((float)itemCount2 - 1f);
							component6.attacker = damageInfo.attacker;
							component6.inflictor = null;
							component6.crit = Util.CheckRoll(attackerBody.crit, attackerMaster);
							component6.maxTimer = 0.5f;
							component6.damageColorIndex = DamageColorIndex.Item;
							component6.falloffModel = BlastAttack.FalloffModel.SweetSpot;
						}
						TeamFilter component7 = gameObject6.GetComponent<TeamFilter>();
						if (component7)
						{
							component7.teamIndex = attackerTeamIndex;
						}
						NetworkServer.Spawn(gameObject6);
					}
					int itemCount3 = inventory.GetItemCount(RoR2Content.Items.Dagger);
					if (itemCount3 > 0)
					{
						float damageCoefficient2 = 1.5f * (float)itemCount3;
						Vector3 a = vector + Vector3.up * 1.8f;
						for (int j = 0; j < 3; j++)
						{
							ProjectileManager.instance.FireProjectile(GlobalEventManager.CommonAssets.daggerPrefab, a + UnityEngine.Random.insideUnitSphere * 0.5f, Util.QuaternionSafeLookRotation(Vector3.up + UnityEngine.Random.insideUnitSphere * 0.1f), attackerBody.gameObject, Util.OnKillProcDamage(attackerBody.damage, damageCoefficient2), 200f, Util.CheckRoll(attackerBody.crit, attackerMaster), DamageColorIndex.Item, null, -1f);
						}
					}
					int itemCount4 = inventory.GetItemCount(RoR2Content.Items.Tooth);
					if (itemCount4 > 0)
					{
						float num3 = Mathf.Pow((float)itemCount4, 0.25f);
						GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HealPack"), vector, UnityEngine.Random.rotation);
						TeamFilter component8 = gameObject7.GetComponent<TeamFilter>();
						if (component8)
						{
							component8.teamIndex = attackerTeamIndex;
						}
						HealthPickup componentInChildren = gameObject7.GetComponentInChildren<HealthPickup>();
						if (componentInChildren)
						{
							componentInChildren.flatHealing = 8f;
							componentInChildren.fractionalHealing = 0.02f * (float)itemCount4;
						}
						gameObject7.transform.localScale = new Vector3(num3, num3, num3);
						NetworkServer.Spawn(gameObject7);
					}
					int itemCount5 = inventory.GetItemCount(RoR2Content.Items.Infusion);
					if (itemCount5 > 0)
					{
						int num4 = itemCount5 * 100;
						if ((ulong)inventory.infusionBonus < (ulong)((long)num4))
						{
							InfusionOrb infusionOrb = new InfusionOrb();
							infusionOrb.origin = vector;
							infusionOrb.target = Util.FindBodyMainHurtBox(attackerBody);
							infusionOrb.maxHpValue = itemCount5;
							OrbManager.instance.AddOrb(infusionOrb);
						}
					}
					if ((damageInfo.damageType & DamageType.ResetCooldownsOnKill) == DamageType.ResetCooldownsOnKill)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/Bandit2ResetEffect"), new EffectData
						{
							origin = damageInfo.position
						}, true);
						SkillLocator skillLocator = attackerBody.skillLocator;
						if (skillLocator)
						{
							skillLocator.ResetSkills();
						}
					}
					if ((damageInfo.damageType & DamageType.GiveSkullOnKill) == DamageType.GiveSkullOnKill && victimMaster)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/Bandit2KillEffect"), new EffectData
						{
							origin = damageInfo.position
						}, true);
						attackerBody.AddBuff(RoR2Content.Buffs.BanditSkull);
					}
					int itemCount6 = inventory.GetItemCount(RoR2Content.Items.Talisman);
					if (itemCount6 > 0 && attackerBody.equipmentSlot)
					{
						inventory.DeductActiveEquipmentCooldown(2f + (float)itemCount6 * 2f);
					}
					int itemCount7 = inventory.GetItemCount(JunkContent.Items.TempestOnKill);
					if (itemCount7 > 0 && Util.CheckRoll(25f, attackerMaster))
					{
						GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/TempestWard"), victimBody.footPosition, Quaternion.identity);
						TeamFilter component9 = gameObject8.GetComponent<TeamFilter>();
						if (component9)
						{
							component9.teamIndex = attackerTeamIndex;
						}
						BuffWard component10 = gameObject8.GetComponent<BuffWard>();
						if (component10)
						{
							component10.expireDuration = 2f + 6f * (float)itemCount7;
						}
						NetworkServer.Spawn(gameObject8);
					}
					int itemCount8 = inventory.GetItemCount(RoR2Content.Items.Bandolier);
					if (itemCount8 > 0 && Util.CheckRoll((1f - 1f / Mathf.Pow((float)(itemCount8 + 1), 0.33f)) * 100f, attackerMaster))
					{
						GameObject gameObject9 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/AmmoPack"), vector, UnityEngine.Random.rotation);
						TeamFilter component11 = gameObject9.GetComponent<TeamFilter>();
						if (component11)
						{
							component11.teamIndex = attackerTeamIndex;
						}
						NetworkServer.Spawn(gameObject9);
					}
					if (victimBody && damageReport.victimIsElite)
					{
						int itemCount9 = inventory.GetItemCount(RoR2Content.Items.HeadHunter);
						int itemCount10 = inventory.GetItemCount(RoR2Content.Items.KillEliteFrenzy);
						if (itemCount9 > 0)
						{
							float duration = 3f + 5f * (float)itemCount9;
							for (int k = 0; k < BuffCatalog.eliteBuffIndices.Length; k++)
							{
								BuffIndex buffIndex = BuffCatalog.eliteBuffIndices[k];
								if (victimBody.HasBuff(buffIndex))
								{
									attackerBody.AddTimedBuff(buffIndex, duration);
								}
							}
						}
						if (itemCount10 > 0)
						{
							attackerBody.AddTimedBuff(RoR2Content.Buffs.NoCooldowns, (float)itemCount10 * 4f);
						}
					}
					int itemCount11 = inventory.GetItemCount(RoR2Content.Items.GhostOnKill);
					if (itemCount11 > 0 && victimBody && Util.CheckRoll(7f, attackerMaster))
					{
						Util.TryToCreateGhost(victimBody, attackerBody, itemCount11 * 30);
					}
					if (inventory.GetItemCount(DLC1Content.Items.MinorConstructOnKill) > 0 && victimBody && victimBody.isElite && !attackerMaster.IsDeployableLimited(DeployableSlot.MinorConstructOnKill))
					{
						Vector3 forward = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * Quaternion.AngleAxis(-80f, Vector3.right) * Vector3.forward;
						FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
						{
							projectilePrefab = GlobalEventManager.CommonAssets.minorConstructOnKillProjectile,
							position = vector,
							rotation = Util.QuaternionSafeLookRotation(forward),
							procChainMask = default(ProcChainMask),
							target = gameObject,
							owner = attackerBody.gameObject,
							damage = 0f,
							crit = false,
							force = 0f,
							damageColorIndex = DamageColorIndex.Item
						};
						ProjectileManager.instance.FireProjectile(fireProjectileInfo);
					}
					int itemCount12 = inventory.GetItemCount(DLC1Content.Items.MoveSpeedOnKill);
					if (itemCount12 > 0)
					{
						int num5 = itemCount12 - 1;
						int num6 = 5;
						float num7 = 1f + (float)num5 * 0.5f;
						attackerBody.ClearTimedBuffs(DLC1Content.Buffs.KillMoveSpeed);
						for (int l = 0; l < num6; l++)
						{
							attackerBody.AddTimedBuff(DLC1Content.Buffs.KillMoveSpeed, num7 * (float)(l + 1) / (float)num6);
						}
						EffectData effectData = new EffectData();
						effectData.origin = attackerBody.corePosition;
						CharacterMotor characterMotor = attackerBody.characterMotor;
						bool flag = false;
						if (characterMotor)
						{
							Vector3 moveDirection = characterMotor.moveDirection;
							if (moveDirection != Vector3.zero)
							{
								effectData.rotation = Util.QuaternionSafeLookRotation(moveDirection);
								flag = true;
							}
						}
						if (!flag)
						{
							effectData.rotation = attackerBody.transform.rotation;
						}
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MoveSpeedOnKillActivate"), effectData, true);
					}
					if (equipmentDef && Util.CheckRoll(equipmentDef.dropOnDeathChance * 100f, attackerMaster) && victimBody)
					{
						PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(equipmentIndex), vector + Vector3.up * 1.5f, Vector3.up * 20f + ray.direction * 2f);
					}
					int itemCount13 = inventory.GetItemCount(RoR2Content.Items.BarrierOnKill);
					if (itemCount13 > 0 && attackerBody.healthComponent)
					{
						attackerBody.healthComponent.AddBarrier(15f * (float)itemCount13);
					}
					int itemCount14 = inventory.GetItemCount(RoR2Content.Items.BonusGoldPackOnKill);
					if (itemCount14 > 0 && Util.CheckRoll(4f * (float)itemCount14, attackerMaster))
					{
						GameObject gameObject10 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BonusMoneyPack"), vector, UnityEngine.Random.rotation);
						TeamFilter component12 = gameObject10.GetComponent<TeamFilter>();
						if (component12)
						{
							component12.teamIndex = attackerTeamIndex;
						}
						NetworkServer.Spawn(gameObject10);
					}
					int itemCount15 = inventory.GetItemCount(RoR2Content.Items.Plant);
					if (itemCount15 > 0)
					{
						GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/InterstellarDeskPlant"), victimBody.footPosition, Quaternion.identity);
						DeskPlantController component13 = gameObject11.GetComponent<DeskPlantController>();
						if (component13)
						{
							if (component13.teamFilter)
							{
								component13.teamFilter.teamIndex = attackerTeamIndex;
							}
							component13.itemCount = itemCount15;
						}
						NetworkServer.Spawn(gameObject11);
					}
					int incubatorOnKillCount = attackerMaster.inventory.GetItemCount(JunkContent.Items.Incubator);
					if (incubatorOnKillCount > 0 && attackerMaster.GetDeployableCount(DeployableSlot.ParentPodAlly) + attackerMaster.GetDeployableCount(DeployableSlot.ParentAlly) < incubatorOnKillCount && Util.CheckRoll(7f + 1f * (float)incubatorOnKillCount, attackerMaster))
					{
						DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscParentPod"), new DirectorPlacementRule
						{
							placementMode = DirectorPlacementRule.PlacementMode.Approximate,
							minDistance = 3f,
							maxDistance = 20f,
							spawnOnTarget = transform
						}, RoR2Application.rng);
						directorSpawnRequest.summonerBodyObject = attacker;
						DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
						directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult spawnResult)
						{
							if (spawnResult.success)
							{
								Inventory inventory2 = spawnResult.spawnedInstance.GetComponent<CharacterMaster>().inventory;
								if (inventory2)
								{
									inventory2.GiveItem(RoR2Content.Items.BoostDamage, 30);
									inventory2.GiveItem(RoR2Content.Items.BoostHp, 10 * incubatorOnKillCount);
								}
							}
						}));
						DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
					}
					int itemCount16 = inventory.GetItemCount(RoR2Content.Items.BleedOnHitAndExplode);
					if (itemCount16 > 0 && victimBody && (victimBody.HasBuff(RoR2Content.Buffs.Bleeding) || victimBody.HasBuff(RoR2Content.Buffs.SuperBleed)))
					{
						Util.PlaySound("Play_bleedOnCritAndExplode_explode", gameObject);
						Vector3 position5 = vector2;
						float damageCoefficient3 = 4f * (float)(1 + (itemCount16 - 1));
						float num8 = 0.15f * (float)(1 + (itemCount16 - 1));
						float baseDamage2 = Util.OnKillProcDamage(attackerBody.damage, damageCoefficient3) + victimBody.maxHealth * num8;
						GameObject gameObject12 = UnityEngine.Object.Instantiate<GameObject>(GlobalEventManager.CommonAssets.bleedOnHitAndExplodeBlastEffect, position5, Quaternion.identity);
						DelayBlast component14 = gameObject12.GetComponent<DelayBlast>();
						component14.position = position5;
						component14.baseDamage = baseDamage2;
						component14.baseForce = 0f;
						component14.radius = 16f;
						component14.attacker = damageInfo.attacker;
						component14.inflictor = null;
						component14.crit = Util.CheckRoll(attackerBody.crit, attackerMaster);
						component14.maxTimer = 0f;
						component14.damageColorIndex = DamageColorIndex.Item;
						component14.falloffModel = BlastAttack.FalloffModel.SweetSpot;
						gameObject12.GetComponent<TeamFilter>().teamIndex = attackerTeamIndex;
						NetworkServer.Spawn(gameObject12);
					}
				}
			}
			Action<DamageReport> action = GlobalEventManager.onCharacterDeathGlobal;
			if (action == null)
			{
				return;
			}
			action(damageReport);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000A0D18 File Offset: 0x0009EF18
		public void OnHitAll(DamageInfo damageInfo, GameObject hitObject)
		{
			if (damageInfo.procCoefficient == 0f || damageInfo.rejected)
			{
				return;
			}
			bool active = NetworkServer.active;
			if (damageInfo.attacker)
			{
				CharacterBody component = damageInfo.attacker.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						Inventory inventory = master.inventory;
						if (master.inventory)
						{
							if (!damageInfo.procChainMask.HasProc(ProcType.Behemoth))
							{
								int itemCount = inventory.GetItemCount(RoR2Content.Items.Behemoth);
								if (itemCount > 0 && damageInfo.procCoefficient != 0f)
								{
									float num = (1.5f + 2.5f * (float)itemCount) * damageInfo.procCoefficient;
									float damageCoefficient = 0.6f;
									float baseDamage = Util.OnHitProcDamage(damageInfo.damage, component.damage, damageCoefficient);
									EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OmniEffect/OmniExplosionVFXQuick"), new EffectData
									{
										origin = damageInfo.position,
										scale = num,
										rotation = Util.QuaternionSafeLookRotation(damageInfo.force)
									}, true);
									BlastAttack blastAttack = new BlastAttack();
									blastAttack.position = damageInfo.position;
									blastAttack.baseDamage = baseDamage;
									blastAttack.baseForce = 0f;
									blastAttack.radius = num;
									blastAttack.attacker = damageInfo.attacker;
									blastAttack.inflictor = null;
									blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
									blastAttack.crit = damageInfo.crit;
									blastAttack.procChainMask = damageInfo.procChainMask;
									blastAttack.procCoefficient = 0f;
									blastAttack.damageColorIndex = DamageColorIndex.Item;
									blastAttack.falloffModel = BlastAttack.FalloffModel.None;
									blastAttack.damageType = damageInfo.damageType;
									blastAttack.Fire();
								}
							}
							if ((component.HasBuff(RoR2Content.Buffs.AffixBlue) ? 1 : 0) > 0)
							{
								float damageCoefficient2 = 0.5f;
								float damage = Util.OnHitProcDamage(damageInfo.damage, component.damage, damageCoefficient2);
								float force = 0f;
								Vector3 position = damageInfo.position;
								ProjectileManager.instance.FireProjectile(LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LightningStake"), position, Quaternion.identity, damageInfo.attacker, damage, force, damageInfo.crit, DamageColorIndex.Item, null, -1f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000A0F38 File Offset: 0x0009F138
		public void OnCrit(CharacterBody body, DamageInfo damageInfo, CharacterMaster master, float procCoefficient, ProcChainMask procChainMask)
		{
			Vector3 hitPos = body.corePosition;
			GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/Critspark");
			if (body)
			{
				if (body.critMultiplier > 2f)
				{
					gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CritsparkHeavy");
				}
				if (body && procCoefficient > 0f && master && master.inventory)
				{
					Inventory inventory = master.inventory;
					if (!procChainMask.HasProc(ProcType.HealOnCrit))
					{
						procChainMask.AddProc(ProcType.HealOnCrit);
						int itemCount = inventory.GetItemCount(RoR2Content.Items.HealOnCrit);
						if (itemCount > 0 && body.healthComponent)
						{
							Util.PlaySound("Play_item_proc_crit_heal", body.gameObject);
							if (NetworkServer.active)
							{
								body.healthComponent.Heal((4f + (float)itemCount * 4f) * procCoefficient, procChainMask, true);
							}
						}
					}
					if (inventory.GetItemCount(RoR2Content.Items.AttackSpeedOnCrit) > 0)
					{
						body.AddTimedBuff(RoR2Content.Buffs.AttackSpeedOnCrit, 3f * procCoefficient);
					}
					int itemCount2 = inventory.GetItemCount(JunkContent.Items.CooldownOnCrit);
					if (itemCount2 > 0)
					{
						Util.PlaySound("Play_item_proc_crit_cooldown", body.gameObject);
						SkillLocator component = body.GetComponent<SkillLocator>();
						if (component)
						{
							float dt = (float)itemCount2 * procCoefficient;
							if (component.primary)
							{
								component.primary.RunRecharge(dt);
							}
							if (component.secondary)
							{
								component.secondary.RunRecharge(dt);
							}
							if (component.utility)
							{
								component.utility.RunRecharge(dt);
							}
							if (component.special)
							{
								component.special.RunRecharge(dt);
							}
						}
					}
				}
			}
			if (damageInfo != null)
			{
				hitPos = damageInfo.position;
			}
			if (gameObject)
			{
				EffectManager.SimpleImpactEffect(gameObject, hitPos, Vector3.up, true);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000A1109 File Offset: 0x0009F309
		public static void OnTeamLevelUp(TeamIndex teamIndex)
		{
			Action<TeamIndex> action = GlobalEventManager.onTeamLevelUp;
			if (action == null)
			{
				return;
			}
			action(teamIndex);
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x0600253C RID: 9532 RVA: 0x000A111C File Offset: 0x0009F31C
		// (remove) Token: 0x0600253D RID: 9533 RVA: 0x000A1150 File Offset: 0x0009F350
		public static event Action<TeamIndex> onTeamLevelUp;

		// Token: 0x0600253E RID: 9534 RVA: 0x000A1183 File Offset: 0x0009F383
		public static void OnCharacterLevelUp(CharacterBody characterBody)
		{
			Action<CharacterBody> action = GlobalEventManager.onCharacterLevelUp;
			if (action == null)
			{
				return;
			}
			action(characterBody);
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600253F RID: 9535 RVA: 0x000A1198 File Offset: 0x0009F398
		// (remove) Token: 0x06002540 RID: 9536 RVA: 0x000A11CC File Offset: 0x0009F3CC
		public static event Action<CharacterBody> onCharacterLevelUp;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002541 RID: 9537 RVA: 0x000A1200 File Offset: 0x0009F400
		// (remove) Token: 0x06002542 RID: 9538 RVA: 0x000A1234 File Offset: 0x0009F434
		public static event Action<Interactor, IInteractable, GameObject> OnInteractionsGlobal;

		// Token: 0x06002543 RID: 9539 RVA: 0x000A1268 File Offset: 0x0009F468
		public void OnInteractionBegin(Interactor interactor, IInteractable interactable, GameObject interactableObject)
		{
			if (!interactor)
			{
				Debug.LogError("OnInteractionBegin invalid interactor!");
				return;
			}
			if (interactable == null)
			{
				Debug.LogError("OnInteractionBegin invalid interactable!");
				return;
			}
			if (!interactableObject)
			{
				Debug.LogError("OnInteractionBegin invalid interactableObject!");
				return;
			}
			Action<Interactor, IInteractable, GameObject> onInteractionsGlobal = GlobalEventManager.OnInteractionsGlobal;
			if (onInteractionsGlobal != null)
			{
				onInteractionsGlobal(interactor, interactable, interactableObject);
			}
			CharacterBody component = interactor.GetComponent<CharacterBody>();
			Vector3 vector = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Transform transform = interactableObject.transform;
			if (transform)
			{
				vector = transform.position;
				rotation = transform.rotation;
			}
			if (component)
			{
				Inventory inventory = component.inventory;
				if (inventory)
				{
					GlobalEventManager.<>c__DisplayClass33_0 CS$<>8__locals1 = new GlobalEventManager.<>c__DisplayClass33_0();
					CS$<>8__locals1.interactionProcFilter = interactableObject.GetComponent<InteractionProcFilter>();
					int itemCount = inventory.GetItemCount(RoR2Content.Items.Firework);
					if (itemCount > 0 && CS$<>8__locals1.<OnInteractionBegin>g__InteractableIsPermittedForSpawn|0((MonoBehaviour)interactable))
					{
						ModelLocator component2 = interactableObject.GetComponent<ModelLocator>();
						Transform transform2;
						if (component2 == null)
						{
							transform2 = null;
						}
						else
						{
							Transform modelTransform = component2.modelTransform;
							if (modelTransform == null)
							{
								transform2 = null;
							}
							else
							{
								ChildLocator component3 = modelTransform.GetComponent<ChildLocator>();
								transform2 = ((component3 != null) ? component3.FindChild("FireworkOrigin") : null);
							}
						}
						Transform transform3 = transform2;
						Vector3 position = transform3 ? transform3.position : (interactableObject.transform.position + Vector3.up * 2f);
						int remaining = 4 + itemCount * 4;
						FireworkLauncher component4 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/FireworkLauncher"), position, Quaternion.identity).GetComponent<FireworkLauncher>();
						component4.owner = interactor.gameObject;
						component4.crit = Util.CheckRoll(component.crit, component.master);
						component4.remaining = remaining;
					}
					CS$<>8__locals1.squidStacks = inventory.GetItemCount(RoR2Content.Items.Squid);
					if (CS$<>8__locals1.squidStacks > 0 && CS$<>8__locals1.<OnInteractionBegin>g__InteractableIsPermittedForSpawn|0((MonoBehaviour)interactable))
					{
						SpawnCard spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscSquidTurret");
						DirectorPlacementRule placementRule = new DirectorPlacementRule
						{
							placementMode = DirectorPlacementRule.PlacementMode.Approximate,
							minDistance = 5f,
							maxDistance = 25f,
							position = interactableObject.transform.position
						};
						DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(spawnCard, placementRule, RoR2Application.rng);
						directorSpawnRequest.teamIndexOverride = new TeamIndex?(TeamIndex.Player);
						directorSpawnRequest.summonerBodyObject = interactor.gameObject;
						DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
						directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult result)
						{
							if (!result.success || !result.spawnedInstance)
							{
								return;
							}
							CharacterMaster component7 = result.spawnedInstance.GetComponent<CharacterMaster>();
							if (component7 && component7.inventory)
							{
								component7.inventory.GiveItem(RoR2Content.Items.HealthDecay, 30);
								component7.inventory.GiveItem(RoR2Content.Items.BoostAttackSpeed, 10 * (CS$<>8__locals1.squidStacks - 1));
							}
						}));
						DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
					}
					int itemCount2 = inventory.GetItemCount(RoR2Content.Items.MonstersOnShrineUse);
					if (itemCount2 > 0)
					{
						PurchaseInteraction component5 = interactableObject.GetComponent<PurchaseInteraction>();
						if (component5 && component5.isShrine && !interactableObject.GetComponent<ShrineCombatBehavior>())
						{
							GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Encounters/MonstersOnShrineUseEncounter");
							if (gameObject)
							{
								GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, vector, Quaternion.identity);
								NetworkServer.Spawn(gameObject2);
								CombatDirector component6 = gameObject2.GetComponent<CombatDirector>();
								if (component6 && Stage.instance)
								{
									float monsterCredit = 40f * Stage.instance.entryDifficultyCoefficient * (float)itemCount2;
									DirectorCard directorCard = component6.SelectMonsterCardForCombatShrine(monsterCredit);
									if (directorCard != null)
									{
										component6.CombatShrineActivation(interactor, monsterCredit, directorCard);
										EffectData effectData = new EffectData
										{
											origin = vector,
											rotation = rotation
										};
										EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MonstersOnShrineUse"), effectData, true);
										return;
									}
									NetworkServer.Destroy(gameObject2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06002544 RID: 9540 RVA: 0x000A15B4 File Offset: 0x0009F7B4
		// (remove) Token: 0x06002545 RID: 9541 RVA: 0x000A15E8 File Offset: 0x0009F7E8
		public static event Action<DamageDealtMessage> onClientDamageNotified;

		// Token: 0x06002546 RID: 9542 RVA: 0x000A161B File Offset: 0x0009F81B
		public static void ClientDamageNotified(DamageDealtMessage damageDealtMessage)
		{
			Action<DamageDealtMessage> action = GlobalEventManager.onClientDamageNotified;
			if (action == null)
			{
				return;
			}
			action(damageDealtMessage);
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06002547 RID: 9543 RVA: 0x000A1630 File Offset: 0x0009F830
		// (remove) Token: 0x06002548 RID: 9544 RVA: 0x000A1664 File Offset: 0x0009F864
		public static event Action<DamageReport> onServerDamageDealt;

		// Token: 0x06002549 RID: 9545 RVA: 0x000A1697 File Offset: 0x0009F897
		public static void ServerDamageDealt(DamageReport damageReport)
		{
			Action<DamageReport> action = GlobalEventManager.onServerDamageDealt;
			if (action == null)
			{
				return;
			}
			action(damageReport);
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600254A RID: 9546 RVA: 0x000A16AC File Offset: 0x0009F8AC
		// (remove) Token: 0x0600254B RID: 9547 RVA: 0x000A16E0 File Offset: 0x0009F8E0
		public static event Action<DamageReport, float> onServerCharacterExecuted;

		// Token: 0x0600254C RID: 9548 RVA: 0x000A1713 File Offset: 0x0009F913
		public static void ServerCharacterExecuted(DamageReport damageReport, float executionHealthLost)
		{
			Action<DamageReport, float> action = GlobalEventManager.onServerCharacterExecuted;
			if (action == null)
			{
				return;
			}
			action(damageReport, executionHealthLost);
		}

		// Token: 0x04002912 RID: 10514
		public static GlobalEventManager instance;

		// Token: 0x04002913 RID: 10515
		[Obsolete("Transform of the global event manager should not be used! You probably meant something else instead.", true)]
		private new Transform transform;

		// Token: 0x04002914 RID: 10516
		[Obsolete("GameObject of the global event manager should not be used! You probably meant something else instead.", true)]
		private new GameObject gameObject;

		// Token: 0x04002915 RID: 10517
		private static readonly string[] standardDeathQuoteTokens = (from i in Enumerable.Range(0, 37)
		select "PLAYER_DEATH_QUOTE_" + TextSerialization.ToStringInvariant(i)).ToArray<string>();

		// Token: 0x04002916 RID: 10518
		private static readonly string[] fallDamageDeathQuoteTokens = (from i in Enumerable.Range(0, 5)
		select "PLAYER_DEATH_QUOTE_FALLDAMAGE_" + TextSerialization.ToStringInvariant(i)).ToArray<string>();

		// Token: 0x04002918 RID: 10520
		private static readonly SphereSearch igniteOnKillSphereSearch = new SphereSearch();

		// Token: 0x04002919 RID: 10521
		private static readonly List<HurtBox> igniteOnKillHurtBoxBuffer = new List<HurtBox>();

		// Token: 0x0200070F RID: 1807
		private static class CommonAssets
		{
			// Token: 0x0600254F RID: 9551 RVA: 0x000A1798 File Offset: 0x0009F998
			public static void Load()
			{
				GlobalEventManager.CommonAssets.wispSoulMasterPrefabMasterComponent = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/WispSoulMaster").GetComponent<CharacterMaster>();
				GlobalEventManager.CommonAssets.igniteOnKillExplosionEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/IgniteExplosionVFX");
				GlobalEventManager.CommonAssets.missilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/MissileProjectile");
				GlobalEventManager.CommonAssets.explodeOnDeathPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ExplodeOnDeath/WilloWispDelay.prefab").WaitForCompletion();
				GlobalEventManager.CommonAssets.daggerPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/DaggerProjectile");
				GlobalEventManager.CommonAssets.bleedOnHitAndExplodeImpactEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/BleedOnHitAndExplode_Impact");
				GlobalEventManager.CommonAssets.bleedOnHitAndExplodeBlastEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BleedOnHitAndExplodeDelay");
				GlobalEventManager.CommonAssets.missileVoidPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/MissileVoidProjectile");
				GlobalEventManager.CommonAssets.eliteEarthHealerMaster = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteEarth/AffixEarthHealerMaster.prefab").WaitForCompletion();
				GlobalEventManager.CommonAssets.minorConstructOnKillProjectile = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MinorConstructOnKill/MinorConstructOnKillProjectile.prefab").WaitForCompletion();
			}

			// Token: 0x04002920 RID: 10528
			public static CharacterMaster wispSoulMasterPrefabMasterComponent;

			// Token: 0x04002921 RID: 10529
			public static GameObject igniteOnKillExplosionEffectPrefab;

			// Token: 0x04002922 RID: 10530
			public static GameObject missilePrefab;

			// Token: 0x04002923 RID: 10531
			public static GameObject explodeOnDeathPrefab;

			// Token: 0x04002924 RID: 10532
			public static GameObject daggerPrefab;

			// Token: 0x04002925 RID: 10533
			public static GameObject bleedOnHitAndExplodeImpactEffect;

			// Token: 0x04002926 RID: 10534
			public static GameObject bleedOnHitAndExplodeBlastEffect;

			// Token: 0x04002927 RID: 10535
			public static GameObject minorConstructOnKillProjectile;

			// Token: 0x04002928 RID: 10536
			public static GameObject missileVoidPrefab;

			// Token: 0x04002929 RID: 10537
			public static GameObject eliteEarthHealerMaster;
		}
	}
}
