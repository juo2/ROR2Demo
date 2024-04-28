using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using HG;
using RoR2.Items;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006AE RID: 1710
	public class DotController : NetworkBehaviour
	{
		// Token: 0x06002145 RID: 8517 RVA: 0x0008EDFB File Offset: 0x0008CFFB
		public static DotController.DotDef GetDotDef(DotController.DotIndex dotIndex)
		{
			if (dotIndex < DotController.DotIndex.Bleed || dotIndex >= DotController.DotIndex.Count)
			{
				return null;
			}
			return DotController.dotDefs[(int)dotIndex];
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0008EE10 File Offset: 0x0008D010
		[SystemInitializer(new Type[]
		{
			typeof(BuffCatalog)
		})]
		private static void InitDotCatalog()
		{
			DotController.dotDefs = new DotController.DotDef[9];
			DotController.dotDefs[0] = new DotController.DotDef
			{
				interval = 0.25f,
				damageCoefficient = 0.2f,
				damageColorIndex = DamageColorIndex.Bleed,
				associatedBuff = RoR2Content.Buffs.Bleeding
			};
			DotController.dotDefs[1] = new DotController.DotDef
			{
				interval = 0.2f,
				damageCoefficient = 0.1f,
				damageColorIndex = DamageColorIndex.Item,
				associatedBuff = RoR2Content.Buffs.OnFire,
				terminalTimedBuff = RoR2Content.Buffs.OnFire,
				terminalTimedBuffDuration = 1f
			};
			DotController.dotDefs[7] = new DotController.DotDef
			{
				interval = 0.2f,
				damageCoefficient = 0.1f,
				damageColorIndex = DamageColorIndex.Item,
				associatedBuff = DLC1Content.Buffs.StrongerBurn,
				terminalTimedBuff = DLC1Content.Buffs.StrongerBurn,
				terminalTimedBuffDuration = 1f
			};
			DotController.dotDefs[3] = new DotController.DotDef
			{
				interval = 0.2f,
				damageCoefficient = 0.1f,
				damageColorIndex = DamageColorIndex.Item,
				associatedBuff = RoR2Content.Buffs.OnFire
			};
			DotController.dotDefs[2] = new DotController.DotDef
			{
				interval = 0.2f,
				damageCoefficient = 0.02f,
				damageColorIndex = DamageColorIndex.Item
			};
			DotController.dotDefs[4] = new DotController.DotDef
			{
				interval = 0.333f,
				damageCoefficient = 0.333f,
				damageColorIndex = DamageColorIndex.Poison,
				associatedBuff = RoR2Content.Buffs.Poisoned
			};
			DotController.dotDefs[5] = new DotController.DotDef
			{
				interval = 0.333f,
				damageCoefficient = 0.2f,
				damageColorIndex = DamageColorIndex.Poison,
				associatedBuff = RoR2Content.Buffs.Blight
			};
			DotController.dotDefs[6] = new DotController.DotDef
			{
				interval = 0.25f,
				damageCoefficient = 0.333f,
				damageColorIndex = DamageColorIndex.SuperBleed,
				associatedBuff = RoR2Content.Buffs.SuperBleed
			};
			DotController.dotDefs[8] = new DotController.DotDef
			{
				interval = 3f,
				damageCoefficient = 4f,
				damageColorIndex = DamageColorIndex.Void,
				associatedBuff = DLC1Content.Buffs.Fracture,
				resetTimerOnAdd = false
			};
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x0008F028 File Offset: 0x0008D228
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x0008F07A File Offset: 0x0008D27A
		public GameObject victimObject
		{
			get
			{
				if (!this._victimObject)
				{
					if (NetworkServer.active)
					{
						this._victimObject = NetworkServer.FindLocalObject(this.victimObjectId);
					}
					else if (NetworkClient.active)
					{
						this._victimObject = ClientScene.FindLocalObject(this.victimObjectId);
					}
				}
				return this._victimObject;
			}
			set
			{
				this.NetworkvictimObjectId = value.GetComponent<NetworkIdentity>().netId;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x0008F08D File Offset: 0x0008D28D
		private CharacterBody victimBody
		{
			get
			{
				if (!this._victimBody && this.victimObject)
				{
					this._victimBody = this.victimObject.GetComponent<CharacterBody>();
				}
				return this._victimBody;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0008F0C0 File Offset: 0x0008D2C0
		private HealthComponent victimHealthComponent
		{
			get
			{
				CharacterBody victimBody = this.victimBody;
				if (victimBody == null)
				{
					return null;
				}
				return victimBody.healthComponent;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x0008F0D3 File Offset: 0x0008D2D3
		private TeamIndex victimTeam
		{
			get
			{
				if (!this.victimBody)
				{
					return TeamIndex.None;
				}
				return this.victimBody.teamComponent.teamIndex;
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0008F0F4 File Offset: 0x0008D2F4
		public bool HasDotActive(DotController.DotIndex dotIndex)
		{
			return ((ulong)this.activeDotFlags & (ulong)(1L << (int)(dotIndex & (DotController.DotIndex)31))) > 0UL;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0008F109 File Offset: 0x0008D309
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.dotStackList = new List<DotController.DotStack>();
				this.dotTimers = new float[9];
			}
			DotController.instancesList.Add(this);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0008F138 File Offset: 0x0008D338
		private void OnDestroy()
		{
			if (NetworkServer.active)
			{
				for (int i = this.dotStackList.Count - 1; i >= 0; i--)
				{
					this.RemoveDotStackAtServer(i);
				}
			}
			DotController.instancesList.Remove(this);
			if (this.recordedVictimInstanceId != -1)
			{
				DotController.dotControllerLocator.Remove(this.recordedVictimInstanceId);
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0008F194 File Offset: 0x0008D394
		private void FixedUpdate()
		{
			if (!this.victimObject)
			{
				if (NetworkServer.active)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				return;
			}
			this.UpdateDotVisuals();
			if (NetworkServer.active)
			{
				for (DotController.DotIndex dotIndex = DotController.DotIndex.Bleed; dotIndex < DotController.DotIndex.Count; dotIndex++)
				{
					uint num = 1U << (int)dotIndex;
					if ((this.activeDotFlags & num) > 0U)
					{
						DotController.DotDef dotDef = DotController.GetDotDef(dotIndex);
						float num2 = this.dotTimers[(int)dotIndex] - Time.fixedDeltaTime;
						if (num2 <= 0f)
						{
							num2 += dotDef.interval;
							int num3 = 0;
							this.EvaluateDotStacksForType(dotIndex, dotDef.interval, out num3);
							this.NetworkactiveDotFlags = (this.activeDotFlags & ~num);
							if (num3 != 0)
							{
								this.NetworkactiveDotFlags = (this.activeDotFlags | num);
							}
						}
						this.dotTimers[(int)dotIndex] = num2;
					}
				}
				if (this.dotStackList.Count == 0)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0008F26C File Offset: 0x0008D46C
		private void UpdateDotVisuals()
		{
			if (!this.victimBody)
			{
				return;
			}
			ModelLocator modelLocator = null;
			base.transform.position = this.victimBody.corePosition;
			if ((this.activeDotFlags & 1U) != 0U)
			{
				if (!this.bleedEffect)
				{
					this.bleedEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/BleedEffect"), base.transform);
				}
			}
			else if (this.bleedEffect)
			{
				UnityEngine.Object.Destroy(this.bleedEffect);
				this.bleedEffect = null;
			}
			if ((this.activeDotFlags & 2U) != 0U || (this.activeDotFlags & 8U) != 0U)
			{
				if (!this.burnEffectController)
				{
					modelLocator = (modelLocator ? modelLocator : this.victimObject.GetComponent<ModelLocator>());
					if (modelLocator && modelLocator.modelTransform)
					{
						this.burnEffectController = base.gameObject.AddComponent<BurnEffectController>();
						this.burnEffectController.effectType = BurnEffectController.normalEffect;
						this.burnEffectController.target = modelLocator.modelTransform.gameObject;
					}
				}
			}
			else if (this.burnEffectController)
			{
				UnityEngine.Object.Destroy(this.burnEffectController);
				this.burnEffectController = null;
			}
			if ((this.activeDotFlags & 128U) != 0U)
			{
				if (!this.strongerBurnEffectController)
				{
					modelLocator = (modelLocator ? modelLocator : this.victimObject.GetComponent<ModelLocator>());
					if (modelLocator && modelLocator.modelTransform)
					{
						this.strongerBurnEffectController = base.gameObject.AddComponent<BurnEffectController>();
						this.strongerBurnEffectController.effectType = BurnEffectController.strongerBurnEffect;
						this.strongerBurnEffectController.target = modelLocator.modelTransform.gameObject;
					}
				}
			}
			else if (this.strongerBurnEffectController)
			{
				UnityEngine.Object.Destroy(this.strongerBurnEffectController);
				this.strongerBurnEffectController = null;
			}
			if ((this.activeDotFlags & 4U) != 0U)
			{
				if (!this.helfireEffectController)
				{
					modelLocator = (modelLocator ? modelLocator : this.victimObject.GetComponent<ModelLocator>());
					if (modelLocator && modelLocator.modelTransform)
					{
						this.helfireEffectController = base.gameObject.AddComponent<BurnEffectController>();
						this.helfireEffectController.effectType = BurnEffectController.helfireEffect;
						this.helfireEffectController.target = modelLocator.modelTransform.gameObject;
					}
				}
			}
			else if (this.helfireEffectController)
			{
				UnityEngine.Object.Destroy(this.helfireEffectController);
				this.helfireEffectController = null;
			}
			if ((this.activeDotFlags & 16U) != 0U)
			{
				if (!this.poisonEffectController)
				{
					modelLocator = (modelLocator ? modelLocator : this.victimObject.GetComponent<ModelLocator>());
					if (modelLocator && modelLocator.modelTransform)
					{
						this.poisonEffectController = base.gameObject.AddComponent<BurnEffectController>();
						this.poisonEffectController.effectType = BurnEffectController.poisonEffect;
						this.poisonEffectController.target = modelLocator.modelTransform.gameObject;
					}
				}
			}
			else if (this.poisonEffectController)
			{
				UnityEngine.Object.Destroy(this.poisonEffectController);
				this.poisonEffectController = null;
			}
			if ((this.activeDotFlags & 32U) != 0U)
			{
				if (!this.blightEffectController)
				{
					modelLocator = (modelLocator ? modelLocator : this.victimObject.GetComponent<ModelLocator>());
					if (modelLocator && modelLocator.modelTransform)
					{
						this.blightEffectController = base.gameObject.AddComponent<BurnEffectController>();
						this.blightEffectController.effectType = BurnEffectController.blightEffect;
						this.blightEffectController.target = modelLocator.modelTransform.gameObject;
					}
				}
			}
			else if (this.blightEffectController)
			{
				UnityEngine.Object.Destroy(this.blightEffectController);
				this.blightEffectController = null;
			}
			if ((this.activeDotFlags & 64U) != 0U)
			{
				if (!this.superBleedEffect)
				{
					this.superBleedEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/SuperBleedEffect"), base.transform);
				}
			}
			else if (this.superBleedEffect)
			{
				UnityEngine.Object.Destroy(this.superBleedEffect);
				this.superBleedEffect = null;
			}
			if ((this.activeDotFlags & 256U) != 0U)
			{
				if (!this.preFractureEffect)
				{
					this.preFractureEffect = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PreFractureEffect"), base.transform);
					return;
				}
			}
			else if (this.preFractureEffect)
			{
				UnityEngine.Object.Destroy(this.preFractureEffect);
				this.preFractureEffect = null;
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0008F6C2 File Offset: 0x0008D8C2
		private void LateUpdate()
		{
			if (this.victimObject)
			{
				base.transform.position = this.victimObject.transform.position;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0008F6EC File Offset: 0x0008D8EC
		private static void AddPendingDamageEntry(List<DotController.PendingDamage> pendingDamages, GameObject attackerObject, float damage, DamageType damageType)
		{
			for (int i = 0; i < pendingDamages.Count; i++)
			{
				if (pendingDamages[i].attackerObject == attackerObject)
				{
					pendingDamages[i].totalDamage += damage;
					return;
				}
			}
			DotController.PendingDamage pendingDamage = DotController.pendingDamagePool.Request();
			pendingDamage.attackerObject = attackerObject;
			pendingDamage.totalDamage = damage;
			pendingDamage.damageType = damageType;
			pendingDamages.Add(pendingDamage);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0008F75C File Offset: 0x0008D95C
		private void OnDotStackAddedServer(DotController.DotStack dotStack)
		{
			DotController.DotDef dotDef = dotStack.dotDef;
			if (dotDef.associatedBuff != null && this.victimBody)
			{
				this.victimBody.AddBuff(dotDef.associatedBuff.buffIndex);
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0008F7A4 File Offset: 0x0008D9A4
		private void OnDotStackRemovedServer(DotController.DotStack dotStack)
		{
			DotController.DotDef dotDef = dotStack.dotDef;
			if (this.victimBody)
			{
				if (dotDef.associatedBuff != null)
				{
					this.victimBody.RemoveBuff(dotDef.associatedBuff.buffIndex);
				}
				if (dotDef.terminalTimedBuff != null)
				{
					this.victimBody.AddTimedBuff(dotDef.terminalTimedBuff, dotDef.terminalTimedBuffDuration);
				}
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0008F810 File Offset: 0x0008DA10
		private void RemoveDotStackAtServer(int i)
		{
			DotController.DotStack dotStack = this.dotStackList[i];
			this.dotStackList.RemoveAt(i);
			this.OnDotStackRemovedServer(dotStack);
			DotController.dotStackPool.Return(dotStack);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0008F84C File Offset: 0x0008DA4C
		private void EvaluateDotStacksForType(DotController.DotIndex dotIndex, float dt, out int remainingActive)
		{
			List<DotController.PendingDamage> list = CollectionPool<DotController.PendingDamage, List<DotController.PendingDamage>>.RentCollection();
			remainingActive = 0;
			DotController.DotDef dotDef = DotController.GetDotDef(dotIndex);
			for (int i = this.dotStackList.Count - 1; i >= 0; i--)
			{
				DotController.DotStack dotStack = this.dotStackList[i];
				if (dotStack.dotIndex == dotIndex)
				{
					dotStack.timer -= dt;
					DotController.AddPendingDamageEntry(list, dotStack.attackerObject, dotStack.damage, dotStack.damageType);
					if (dotStack.timer <= 0f)
					{
						this.RemoveDotStackAtServer(i);
					}
					else
					{
						remainingActive++;
					}
				}
			}
			if (this.victimObject)
			{
				if (this.victimBody && dotIndex == DotController.DotIndex.Fracture && list.Count > 0)
				{
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/FractureImpactEffect"), new EffectData
					{
						origin = this.victimBody.corePosition
					}, true);
				}
				if (this.victimHealthComponent)
				{
					Vector3 corePosition = this.victimBody.corePosition;
					for (int j = 0; j < list.Count; j++)
					{
						DamageInfo damageInfo = new DamageInfo();
						damageInfo.attacker = list[j].attackerObject;
						damageInfo.crit = false;
						damageInfo.damage = list[j].totalDamage;
						damageInfo.force = Vector3.zero;
						damageInfo.inflictor = base.gameObject;
						damageInfo.position = corePosition;
						damageInfo.procCoefficient = 0f;
						damageInfo.damageColorIndex = dotDef.damageColorIndex;
						damageInfo.damageType = (list[j].damageType | DamageType.DoT);
						damageInfo.dotIndex = dotIndex;
						this.victimHealthComponent.TakeDamage(damageInfo);
					}
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				DotController.pendingDamagePool.Return(list[k]);
			}
			CollectionPool<DotController.PendingDamage, List<DotController.PendingDamage>>.ReturnCollection(list);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0008FA34 File Offset: 0x0008DC34
		[Server]
		private void AddDot(GameObject attackerObject, float duration, DotController.DotIndex dotIndex, float damageMultiplier, uint? maxStacksFromAttacker, float? totalDamage, DotController.DotIndex? preUpgradeDotIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.DotController::AddDot(UnityEngine.GameObject,System.Single,RoR2.DotController/DotIndex,System.Single,System.Nullable`1<System.UInt32>,System.Nullable`1<System.Single>,System.Nullable`1<RoR2.DotController/DotIndex>)' called on client");
				return;
			}
			TeamIndex attackerTeam = TeamIndex.Neutral;
			float num = 0f;
			TeamComponent component = attackerObject.GetComponent<TeamComponent>();
			if (component)
			{
				attackerTeam = component.teamIndex;
			}
			CharacterBody component2 = attackerObject.GetComponent<CharacterBody>();
			if (component2)
			{
				num = component2.damage;
			}
			DotController.DotDef dotDef = DotController.dotDefs[(int)dotIndex];
			DotController.DotStack dotStack = DotController.dotStackPool.Request();
			dotStack.dotIndex = dotIndex;
			dotStack.dotDef = dotDef;
			dotStack.attackerObject = attackerObject;
			dotStack.attackerTeam = attackerTeam;
			dotStack.timer = duration;
			dotStack.damage = dotDef.damageCoefficient * num * damageMultiplier;
			dotStack.damageType = DamageType.Generic;
			int num2 = 0;
			int i = 0;
			int count = this.dotStackList.Count;
			while (i < count)
			{
				if (this.dotStackList[i].dotIndex == dotIndex)
				{
					num2++;
				}
				i++;
			}
			switch (dotIndex)
			{
			case DotController.DotIndex.Bleed:
			case DotController.DotIndex.SuperBleed:
			case DotController.DotIndex.Fracture:
			{
				int j = 0;
				int count2 = this.dotStackList.Count;
				while (j < count2)
				{
					if (this.dotStackList[j].dotIndex == dotIndex)
					{
						this.dotStackList[j].timer = Mathf.Max(this.dotStackList[j].timer, duration);
					}
					j++;
				}
				break;
			}
			case DotController.DotIndex.Burn:
			case DotController.DotIndex.StrongerBurn:
				dotStack.damage = Mathf.Min(dotStack.damage, this.victimBody.healthComponent.fullCombinedHealth * 0.01f * damageMultiplier);
				break;
			case DotController.DotIndex.Helfire:
			{
				if (!component2)
				{
					return;
				}
				HealthComponent healthComponent = component2.healthComponent;
				if (!healthComponent)
				{
					return;
				}
				dotStack.damage = Mathf.Min(healthComponent.fullCombinedHealth * 0.01f * damageMultiplier, this.victimBody.healthComponent.fullCombinedHealth * 0.01f * damageMultiplier);
				if (this.victimBody)
				{
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/HelfireIgniteEffect"), new EffectData
					{
						origin = this.victimBody.corePosition
					}, true);
				}
				break;
			}
			case DotController.DotIndex.PercentBurn:
				dotStack.damage = Mathf.Min(dotStack.damage, this.victimBody.healthComponent.fullCombinedHealth * 0.01f);
				break;
			case DotController.DotIndex.Poison:
			{
				float a = this.victimHealthComponent.fullCombinedHealth / 100f * 1f * dotDef.interval;
				dotStack.damage = Mathf.Min(Mathf.Max(a, dotStack.damage), dotStack.damage * 50f);
				dotStack.damageType = DamageType.NonLethal;
				int k = 0;
				int count3 = this.dotStackList.Count;
				while (k < count3)
				{
					if (this.dotStackList[k].dotIndex == DotController.DotIndex.Poison)
					{
						this.dotStackList[k].timer = Mathf.Max(this.dotStackList[k].timer, duration);
						this.dotStackList[k].damage = dotStack.damage;
						return;
					}
					k++;
				}
				if (num2 == 0 && component2 != null)
				{
					CharacterMaster master = component2.master;
					if (master != null)
					{
						PlayerStatsComponent playerStatsComponent = master.playerStatsComponent;
						if (playerStatsComponent != null)
						{
							playerStatsComponent.currentStats.PushStatValue(StatDef.totalCrocoInfectionsInflicted, 1UL);
						}
					}
				}
				break;
			}
			}
			if ((dotIndex == DotController.DotIndex.Helfire || (preUpgradeDotIndex != null && preUpgradeDotIndex.Value == DotController.DotIndex.Helfire)) && this.victimObject == attackerObject)
			{
				dotStack.damageType |= (DamageType.NonLethal | DamageType.Silent);
			}
			dotStack.damage = Mathf.Max(1f, dotStack.damage);
			if (totalDamage != null && dotStack.damage != 0f)
			{
				duration = totalDamage.Value * dotDef.interval / dotStack.damage;
				dotStack.timer = duration;
			}
			if (maxStacksFromAttacker != null)
			{
				DotController.DotStack dotStack2 = null;
				int num3 = 0;
				int l = 0;
				int count4 = this.dotStackList.Count;
				while (l < count4)
				{
					DotController.DotStack dotStack3 = this.dotStackList[l];
					if (dotStack3.dotIndex == dotIndex && dotStack3.attackerObject == attackerObject)
					{
						num3++;
						if (dotStack2 == null || dotStack3.timer < dotStack2.timer)
						{
							dotStack2 = dotStack3;
						}
					}
					l++;
				}
				if ((long)num3 >= (long)((ulong)maxStacksFromAttacker.Value) && dotStack2 != null)
				{
					if (dotStack2.timer < duration)
					{
						dotStack2.timer = duration;
						dotStack2.damage = dotStack.damage;
						dotStack2.damageType = dotStack.damageType;
					}
					return;
				}
			}
			if (num2 == 0 || dotDef.resetTimerOnAdd)
			{
				this.NetworkactiveDotFlags = (this.activeDotFlags | 1U << (int)dotIndex);
				this.dotTimers[(int)dotIndex] = dotDef.interval;
			}
			this.dotStackList.Add(dotStack);
			this.OnDotStackAddedServer(dotStack);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0008FF14 File Offset: 0x0008E114
		[Server]
		public static void InflictDot(ref InflictDotInfo inflictDotInfo)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.DotController::InflictDot(RoR2.InflictDotInfo&)' called on client");
				return;
			}
			if (inflictDotInfo.dotIndex < DotController.DotIndex.Bleed || inflictDotInfo.dotIndex >= DotController.DotIndex.Count || ImmuneToDebuffBehavior.OverrideDot(inflictDotInfo))
			{
				return;
			}
			if (inflictDotInfo.victimObject && inflictDotInfo.attackerObject)
			{
				DotController component;
				if (!DotController.dotControllerLocator.TryGetValue(inflictDotInfo.victimObject.GetInstanceID(), out component))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/DotController"));
					component = gameObject.GetComponent<DotController>();
					component.victimObject = inflictDotInfo.victimObject;
					component.recordedVictimInstanceId = inflictDotInfo.victimObject.GetInstanceID();
					DotController.dotControllerLocator.Add(component.recordedVictimInstanceId, component);
					NetworkServer.Spawn(gameObject);
				}
				component.AddDot(inflictDotInfo.attackerObject, inflictDotInfo.duration, inflictDotInfo.dotIndex, inflictDotInfo.damageMultiplier, inflictDotInfo.maxStacksFromAttacker, inflictDotInfo.totalDamage, inflictDotInfo.preUpgradeDotIndex);
				DotController.OnDotInflictedServerGlobalDelegate onDotInflictedServerGlobalDelegate = DotController.onDotInflictedServerGlobal;
				if (onDotInflictedServerGlobalDelegate == null)
				{
					return;
				}
				onDotInflictedServerGlobalDelegate(component, ref inflictDotInfo);
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0009001C File Offset: 0x0008E21C
		[Server]
		public static void InflictDot(GameObject victimObject, GameObject attackerObject, DotController.DotIndex dotIndex, float duration = 8f, float damageMultiplier = 1f, uint? maxStacksFromAttacker = null)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.DotController::InflictDot(UnityEngine.GameObject,UnityEngine.GameObject,RoR2.DotController/DotIndex,System.Single,System.Single,System.Nullable`1<System.UInt32>)' called on client");
				return;
			}
			InflictDotInfo inflictDotInfo = new InflictDotInfo
			{
				victimObject = victimObject,
				attackerObject = attackerObject,
				dotIndex = dotIndex,
				duration = duration,
				damageMultiplier = damageMultiplier,
				maxStacksFromAttacker = maxStacksFromAttacker
			};
			DotController.InflictDot(ref inflictDotInfo);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00090084 File Offset: 0x0008E284
		[Server]
		public static void RemoveAllDots(GameObject victimObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.DotController::RemoveAllDots(UnityEngine.GameObject)' called on client");
				return;
			}
			DotController dotController;
			if (DotController.dotControllerLocator.TryGetValue(victimObject.GetInstanceID(), out dotController))
			{
				UnityEngine.Object.Destroy(dotController.gameObject);
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000900C8 File Offset: 0x0008E2C8
		[Server]
		public static DotController FindDotController(GameObject victimObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'RoR2.DotController RoR2.DotController::FindDotController(UnityEngine.GameObject)' called on client");
				return null;
			}
			int i = 0;
			int count = DotController.instancesList.Count;
			while (i < count)
			{
				if (victimObject == DotController.instancesList[i]._victimObject)
				{
					return DotController.instancesList[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600215C RID: 8540 RVA: 0x00090130 File Offset: 0x0008E330
		// (remove) Token: 0x0600215D RID: 8541 RVA: 0x00090164 File Offset: 0x0008E364
		public static event DotController.OnDotInflictedServerGlobalDelegate onDotInflictedServerGlobal;

		// Token: 0x06002160 RID: 8544 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x000901E0 File Offset: 0x0008E3E0
		// (set) Token: 0x06002162 RID: 8546 RVA: 0x000901F3 File Offset: 0x0008E3F3
		public NetworkInstanceId NetworkvictimObjectId
		{
			get
			{
				return this.victimObjectId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<NetworkInstanceId>(value, ref this.victimObjectId, 1U);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00090208 File Offset: 0x0008E408
		// (set) Token: 0x06002164 RID: 8548 RVA: 0x0009021B File Offset: 0x0008E41B
		public uint NetworkactiveDotFlags
		{
			get
			{
				return this.activeDotFlags;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this.activeDotFlags, 2U);
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00090230 File Offset: 0x0008E430
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.victimObjectId);
				writer.WritePackedUInt32(this.activeDotFlags);
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
				writer.Write(this.victimObjectId);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32(this.activeDotFlags);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000902DC File Offset: 0x0008E4DC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.victimObjectId = reader.ReadNetworkId();
				this.activeDotFlags = reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.victimObjectId = reader.ReadNetworkId();
			}
			if ((num & 2) != 0)
			{
				this.activeDotFlags = reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040026A1 RID: 9889
		public const float minDamagePerTick = 1f;

		// Token: 0x040026A2 RID: 9890
		private static DotController.DotDef[] dotDefs;

		// Token: 0x040026A3 RID: 9891
		private static readonly Dictionary<int, DotController> dotControllerLocator = new Dictionary<int, DotController>();

		// Token: 0x040026A4 RID: 9892
		private static readonly List<DotController> instancesList = new List<DotController>();

		// Token: 0x040026A5 RID: 9893
		public static readonly ReadOnlyCollection<DotController> readOnlyInstancesList = DotController.instancesList.AsReadOnly();

		// Token: 0x040026A6 RID: 9894
		[SyncVar]
		private NetworkInstanceId victimObjectId;

		// Token: 0x040026A7 RID: 9895
		private GameObject _victimObject;

		// Token: 0x040026A8 RID: 9896
		private CharacterBody _victimBody;

		// Token: 0x040026A9 RID: 9897
		private BurnEffectController burnEffectController;

		// Token: 0x040026AA RID: 9898
		private BurnEffectController strongerBurnEffectController;

		// Token: 0x040026AB RID: 9899
		private BurnEffectController helfireEffectController;

		// Token: 0x040026AC RID: 9900
		private BurnEffectController poisonEffectController;

		// Token: 0x040026AD RID: 9901
		private BurnEffectController blightEffectController;

		// Token: 0x040026AE RID: 9902
		private GameObject bleedEffect;

		// Token: 0x040026AF RID: 9903
		private GameObject superBleedEffect;

		// Token: 0x040026B0 RID: 9904
		private GameObject preFractureEffect;

		// Token: 0x040026B1 RID: 9905
		[SyncVar]
		private uint activeDotFlags;

		// Token: 0x040026B2 RID: 9906
		private static readonly DotController.DotStackPool dotStackPool = new DotController.DotStackPool();

		// Token: 0x040026B3 RID: 9907
		private List<DotController.DotStack> dotStackList;

		// Token: 0x040026B4 RID: 9908
		private float[] dotTimers;

		// Token: 0x040026B5 RID: 9909
		private static readonly DotController.PendingDamagePool pendingDamagePool = new DotController.PendingDamagePool();

		// Token: 0x040026B6 RID: 9910
		private int recordedVictimInstanceId = -1;

		// Token: 0x020006AF RID: 1711
		public enum DotIndex
		{
			// Token: 0x040026B9 RID: 9913
			None = -1,
			// Token: 0x040026BA RID: 9914
			Bleed,
			// Token: 0x040026BB RID: 9915
			Burn,
			// Token: 0x040026BC RID: 9916
			Helfire,
			// Token: 0x040026BD RID: 9917
			PercentBurn,
			// Token: 0x040026BE RID: 9918
			Poison,
			// Token: 0x040026BF RID: 9919
			Blight,
			// Token: 0x040026C0 RID: 9920
			SuperBleed,
			// Token: 0x040026C1 RID: 9921
			StrongerBurn,
			// Token: 0x040026C2 RID: 9922
			Fracture,
			// Token: 0x040026C3 RID: 9923
			Count
		}

		// Token: 0x020006B0 RID: 1712
		public class DotDef
		{
			// Token: 0x040026C4 RID: 9924
			public float interval;

			// Token: 0x040026C5 RID: 9925
			public float damageCoefficient;

			// Token: 0x040026C6 RID: 9926
			public DamageColorIndex damageColorIndex;

			// Token: 0x040026C7 RID: 9927
			public BuffDef associatedBuff;

			// Token: 0x040026C8 RID: 9928
			public BuffDef terminalTimedBuff;

			// Token: 0x040026C9 RID: 9929
			public float terminalTimedBuffDuration;

			// Token: 0x040026CA RID: 9930
			public bool resetTimerOnAdd;
		}

		// Token: 0x020006B1 RID: 1713
		private class DotStack
		{
			// Token: 0x06002169 RID: 8553 RVA: 0x00090342 File Offset: 0x0008E542
			public void Reset()
			{
				this.dotIndex = DotController.DotIndex.Bleed;
				this.dotDef = null;
				this.attackerObject = null;
				this.attackerTeam = TeamIndex.Neutral;
				this.timer = 0f;
				this.damage = 0f;
				this.damageType = DamageType.Generic;
			}

			// Token: 0x040026CB RID: 9931
			public DotController.DotIndex dotIndex;

			// Token: 0x040026CC RID: 9932
			public DotController.DotDef dotDef;

			// Token: 0x040026CD RID: 9933
			public GameObject attackerObject;

			// Token: 0x040026CE RID: 9934
			public TeamIndex attackerTeam;

			// Token: 0x040026CF RID: 9935
			public float timer;

			// Token: 0x040026D0 RID: 9936
			public float damage;

			// Token: 0x040026D1 RID: 9937
			public DamageType damageType;
		}

		// Token: 0x020006B2 RID: 1714
		private sealed class DotStackPool : BasePool<DotController.DotStack>
		{
			// Token: 0x0600216B RID: 8555 RVA: 0x0009037D File Offset: 0x0008E57D
			protected override void ResetItem(DotController.DotStack item)
			{
				item.Reset();
			}
		}

		// Token: 0x020006B3 RID: 1715
		private class PendingDamage
		{
			// Token: 0x0600216D RID: 8557 RVA: 0x0009038D File Offset: 0x0008E58D
			public void Reset()
			{
				this.attackerObject = null;
				this.totalDamage = 0f;
				this.damageType = DamageType.Generic;
			}

			// Token: 0x040026D2 RID: 9938
			public GameObject attackerObject;

			// Token: 0x040026D3 RID: 9939
			public float totalDamage;

			// Token: 0x040026D4 RID: 9940
			public DamageType damageType;
		}

		// Token: 0x020006B4 RID: 1716
		private sealed class PendingDamagePool : BasePool<DotController.PendingDamage>
		{
			// Token: 0x0600216F RID: 8559 RVA: 0x000903A8 File Offset: 0x0008E5A8
			protected override void ResetItem(DotController.PendingDamage item)
			{
				item.Reset();
			}
		}

		// Token: 0x020006B5 RID: 1717
		// (Invoke) Token: 0x06002172 RID: 8562
		public delegate void OnDotInflictedServerGlobalDelegate(DotController dotController, ref InflictDotInfo inflictDotInfo);
	}
}
