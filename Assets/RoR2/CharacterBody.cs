using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EntityStates;
using HG;
using RoR2.Audio;
using RoR2.Items;
using RoR2.Navigation;
using RoR2.Networking;
using RoR2.Projectile;
using RoR2.Skills;
using Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200061B RID: 1563
	[DisallowMultipleComponent]
	[RequireComponent(typeof(SkillLocator))]
	[RequireComponent(typeof(TeamComponent))]
	public class CharacterBody : NetworkBehaviour, ILifeBehavior, IDisplayNameProvider, IOnTakeDamageServerReceiver, IOnKilledOtherServerReceiver
	{
		// Token: 0x06001CD0 RID: 7376 RVA: 0x0007A955 File Offset: 0x00078B55
		[RuntimeInitializeOnLoadMethod]
		private static void LoadCommonAssets()
		{
			CharacterBody.CommonAssets.Load();
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0007A95C File Offset: 0x00078B5C
		public string GetDisplayName()
		{
			return Language.GetString(this.baseNameToken);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0007A969 File Offset: 0x00078B69
		public string GetSubtitle()
		{
			return Language.GetString(this.subtitleNameToken);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0007A978 File Offset: 0x00078B78
		public string GetUserName()
		{
			string text = "";
			if (this.master)
			{
				PlayerCharacterMasterController component = this.master.GetComponent<PlayerCharacterMasterController>();
				if (component)
				{
					text = component.GetDisplayName();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetDisplayName();
			}
			return text;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0007A9C4 File Offset: 0x00078BC4
		public string GetColoredUserName()
		{
			Color32 userColor = new Color32(127, 127, 127, byte.MaxValue);
			string text = null;
			if (this.master)
			{
				PlayerCharacterMasterController component = this.master.GetComponent<PlayerCharacterMasterController>();
				if (component)
				{
					GameObject networkUserObject = component.networkUserObject;
					if (networkUserObject)
					{
						NetworkUser component2 = networkUserObject.GetComponent<NetworkUser>();
						if (component2)
						{
							userColor = component2.userColor;
							text = component2.userName;
						}
					}
				}
			}
			if (text == null)
			{
				text = this.GetDisplayName();
			}
			return Util.GenerateColoredString(text, userColor);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0007AA48 File Offset: 0x00078C48
		[Server]
		private void WriteBuffs(NetworkWriter writer)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::WriteBuffs(UnityEngine.Networking.NetworkWriter)' called on client");
				return;
			}
			writer.Write((byte)this.activeBuffsListCount);
			for (int i = 0; i < this.activeBuffsListCount; i++)
			{
				BuffIndex buffIndex = this.activeBuffsList[i];
				BuffDef buffDef = BuffCatalog.GetBuffDef(buffIndex);
				writer.WriteBuffIndex(buffIndex);
				if (buffDef.canStack)
				{
					writer.WritePackedUInt32((uint)this.buffs[(int)buffIndex]);
				}
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		[Client]
		private void ReadBuffs(NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.CharacterBody::ReadBuffs(UnityEngine.Networking.NetworkReader)' called on server");
				return;
			}
			CharacterBody.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			if (this.activeBuffsList == null)
			{
				Debug.LogError("Trying to ReadBuffs, but our activeBuffsList is null");
				return;
			}
			CS$<>8__locals1.activeBuffsIndexToCheck = 0;
			int num = (int)reader.ReadByte();
			BuffIndex buffIndex = BuffIndex.None;
			for (int i = 0; i < num; i++)
			{
				BuffIndex buffIndex2 = reader.ReadBuffIndex();
				BuffDef buffDef = BuffCatalog.GetBuffDef(buffIndex2);
				if (buffDef != null)
				{
					int num2 = 1;
					if (buffDef.canStack)
					{
						num2 = (int)reader.ReadPackedUInt32();
					}
					if (num2 > 0 && !NetworkServer.active)
					{
						this.<ReadBuffs>g__ZeroBuffIndexRange|17_0(buffIndex + 1, buffIndex2, ref CS$<>8__locals1);
						this.SetBuffCount(buffIndex2, num2);
					}
					buffIndex = buffIndex2;
				}
				else
				{
					Debug.LogErrorFormat("No BuffDef for index {0}. body={1}, netID={2}", new object[]
					{
						buffIndex2,
						base.gameObject,
						base.netId
					});
				}
			}
			if (!NetworkServer.active)
			{
				this.<ReadBuffs>g__ZeroBuffIndexRange|17_0(buffIndex + 1, (BuffIndex)BuffCatalog.buffCount, ref CS$<>8__locals1);
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0007ABB3 File Offset: 0x00078DB3
		[Server]
		public void AddBuff(BuffIndex buffType)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddBuff(RoR2.BuffIndex)' called on client");
				return;
			}
			if (buffType == BuffIndex.None)
			{
				return;
			}
			this.SetBuffCount(buffType, this.buffs[(int)buffType] + 1);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0007ABE0 File Offset: 0x00078DE0
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddBuff(BuffDef buffDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddBuff(RoR2.BuffDef)' called on client");
				return;
			}
			this.AddBuff((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0007AC0C File Offset: 0x00078E0C
		[Server]
		public void RemoveBuff(BuffIndex buffType)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::RemoveBuff(RoR2.BuffIndex)' called on client");
				return;
			}
			if (buffType == BuffIndex.None)
			{
				return;
			}
			this.SetBuffCount(buffType, this.buffs[(int)buffType] - 1);
			if (buffType == RoR2Content.Buffs.MedkitHeal.buffIndex)
			{
				if (this.GetBuffCount(RoR2Content.Buffs.MedkitHeal.buffIndex) == 0)
				{
					int itemCount = this.inventory.GetItemCount(RoR2Content.Items.Medkit);
					float num = 20f;
					float num2 = this.maxHealth * 0.05f * (float)itemCount;
					this.healthComponent.Heal(num + num2, default(ProcChainMask), true);
					EffectData effectData = new EffectData
					{
						origin = this.transform.position
					};
					effectData.SetNetworkedObjectReference(base.gameObject);
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MedkitHealEffect"), effectData, true);
					return;
				}
			}
			else if (buffType == RoR2Content.Buffs.TonicBuff.buffIndex && this.inventory && this.GetBuffCount(RoR2Content.Buffs.TonicBuff) == 0 && this.pendingTonicAfflictionCount > 0)
			{
				this.inventory.GiveItem(RoR2Content.Items.TonicAffliction, this.pendingTonicAfflictionCount);
				PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(RoR2Content.Items.TonicAffliction.itemIndex);
				GenericPickupController.SendPickupMessage(this.master, pickupIndex);
				this.pendingTonicAfflictionCount = 0;
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0007AD4A File Offset: 0x00078F4A
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveBuff(BuffDef buffDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::RemoveBuff(RoR2.BuffDef)' called on client");
				return;
			}
			this.RemoveBuff((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0007AD74 File Offset: 0x00078F74
		private void SetBuffCount(BuffIndex buffType, int newCount)
		{
			ref int ptr = ref this.buffs[(int)buffType];
			if (newCount == ptr)
			{
				return;
			}
			int num = ptr;
			ptr = newCount;
			BuffDef buffDef = BuffCatalog.GetBuffDef(buffType);
			bool flag = true;
			if (!buffDef.canStack)
			{
				flag = (num == 0 != (newCount == 0));
			}
			if (flag)
			{
				if (newCount == 0)
				{
					ArrayUtils.ArrayRemoveAt<BuffIndex>(this.activeBuffsList, ref this.activeBuffsListCount, Array.IndexOf<BuffIndex>(this.activeBuffsList, buffType), 1);
					this.OnBuffFinalStackLost(buffDef);
				}
				else if (num == 0)
				{
					int num2 = 0;
					while (num2 < this.activeBuffsListCount && buffType >= this.activeBuffsList[num2])
					{
						num2++;
					}
					ArrayUtils.ArrayInsert<BuffIndex>(ref this.activeBuffsList, ref this.activeBuffsListCount, num2, buffType);
					this.OnBuffFirstStackGained(buffDef);
				}
				if (NetworkServer.active)
				{
					base.SetDirtyBit(2U);
				}
			}
			this.statsDirty = true;
			if (NetworkClient.active)
			{
				this.OnClientBuffsChanged();
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0007AE48 File Offset: 0x00079048
		private void OnBuffFirstStackGained(BuffDef buffDef)
		{
			if (buffDef.isElite)
			{
				this.eliteBuffCount++;
			}
			if (buffDef == RoR2Content.Buffs.Intangible)
			{
				this.UpdateHurtBoxesEnabled();
				return;
			}
			if (buffDef == RoR2Content.Buffs.WarCryBuff)
			{
				if (this.HasBuff(RoR2Content.Buffs.TeamWarCry))
				{
					this.ClearTimedBuffs(RoR2Content.Buffs.TeamWarCry);
					return;
				}
			}
			else if (buffDef == RoR2Content.Buffs.TeamWarCry)
			{
				if (this.HasBuff(RoR2Content.Buffs.WarCryBuff))
				{
					this.ClearTimedBuffs(RoR2Content.Buffs.WarCryBuff);
					return;
				}
			}
			else if (buffDef == RoR2Content.Buffs.AffixEcho && NetworkServer.active)
			{
				this.AddItemBehavior<CharacterBody.AffixEchoBehavior>(1);
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0007AEE8 File Offset: 0x000790E8
		private void OnBuffFinalStackLost(BuffDef buffDef)
		{
			if (buffDef.isElite)
			{
				this.eliteBuffCount--;
			}
			if (buffDef.buffIndex == RoR2Content.Buffs.Intangible.buffIndex)
			{
				this.UpdateHurtBoxesEnabled();
				return;
			}
			if (buffDef == RoR2Content.Buffs.AffixEcho && NetworkServer.active)
			{
				this.AddItemBehavior<CharacterBody.AffixEchoBehavior>(0);
			}
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0007AF40 File Offset: 0x00079140
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetBuffCount(BuffIndex buffType)
		{
			return ArrayUtils.GetSafe<int>(this.buffs, (int)buffType);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0007AF4E File Offset: 0x0007914E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetBuffCount(BuffDef buffDef)
		{
			return this.GetBuffCount((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0007AF62 File Offset: 0x00079162
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasBuff(BuffIndex buffType)
		{
			return this.GetBuffCount(buffType) > 0;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0007AF6E File Offset: 0x0007916E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasBuff(BuffDef buffDef)
		{
			return this.HasBuff((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0007AF82 File Offset: 0x00079182
		public void AddTimedBuffAuthority(BuffIndex buffType, float duration)
		{
			if (NetworkServer.active)
			{
				this.AddTimedBuff(buffType, duration);
				return;
			}
			this.CallCmdAddTimedBuff(buffType, duration);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0007AF9C File Offset: 0x0007919C
		[Command]
		public void CmdAddTimedBuff(BuffIndex buffType, float duration)
		{
			this.AddTimedBuff(buffType, duration);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0007AFA8 File Offset: 0x000791A8
		[Server]
		public void AddTimedBuff(BuffDef buffDef, float duration, int maxStacks)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddTimedBuff(RoR2.BuffDef,System.Single,System.Int32)' called on client");
				return;
			}
			if (ImmuneToDebuffBehavior.OverrideDebuff(buffDef, this))
			{
				return;
			}
			if (this.GetBuffCount(buffDef) < maxStacks)
			{
				this.AddTimedBuff(buffDef, duration);
				return;
			}
			int num = -1;
			float num2 = duration;
			for (int i = 0; i < this.timedBuffs.Count; i++)
			{
				if (this.timedBuffs[i].buffIndex == buffDef.buffIndex && this.timedBuffs[i].timer < num2)
				{
					num = i;
					num2 = this.timedBuffs[i].timer;
				}
			}
			if (num >= 0)
			{
				this.timedBuffs[num].timer = duration;
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0007B058 File Offset: 0x00079258
		[Server]
		public void AddTimedBuff(BuffDef buffDef, float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddTimedBuff(RoR2.BuffDef,System.Single)' called on client");
				return;
			}
			CharacterBody.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.buffDef = buffDef;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.duration = duration;
			if (CS$<>8__locals1.buffDef == null || ImmuneToDebuffBehavior.OverrideDebuff(CS$<>8__locals1.buffDef, this))
			{
				return;
			}
			CS$<>8__locals1.buffType = CS$<>8__locals1.buffDef.buffIndex;
			if (CS$<>8__locals1.buffType == BuffIndex.None)
			{
				return;
			}
			if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.AttackSpeedOnCrit)
			{
				int num = this.inventory ? this.inventory.GetItemCount(RoR2Content.Items.AttackSpeedOnCrit) : 0;
				int num2 = 1 + num * 2;
				int num3 = 0;
				int num4 = -1;
				float num5 = 999f;
				for (int i = 0; i < this.timedBuffs.Count; i++)
				{
					if (this.timedBuffs[i].buffIndex == CS$<>8__locals1.buffType)
					{
						num3++;
						if (this.timedBuffs[i].timer < num5)
						{
							num4 = i;
							num5 = this.timedBuffs[i].timer;
						}
					}
				}
				if (num3 < num2)
				{
					this.timedBuffs.Add(new CharacterBody.TimedBuff
					{
						buffIndex = CS$<>8__locals1.buffType,
						timer = CS$<>8__locals1.duration
					});
					this.AddBuff(CS$<>8__locals1.buffType);
					ChildLocator component = this.modelLocator.modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						UnityEngine.Object exists = component.FindChild("HandL");
						Transform exists2 = component.FindChild("HandR");
						GameObject effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/WolfProcEffect");
						if (exists)
						{
							EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, "HandL", true);
						}
						if (exists2)
						{
							EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, "HandR", true);
						}
					}
				}
				else if (num4 > -1)
				{
					this.timedBuffs[num4].timer = CS$<>8__locals1.duration;
				}
				EntitySoundManager.EmitSoundServer(CharacterBody.CommonAssets.procCritAttackSpeedSounds[Mathf.Min(CharacterBody.CommonAssets.procCritAttackSpeedSounds.Length - 1, num3)].index, this.networkIdentity);
				return;
			}
			if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.BeetleJuice)
			{
				if (this.<AddTimedBuff>g__RefreshStacks|32_1(ref CS$<>8__locals1) < 10)
				{
					this.timedBuffs.Add(new CharacterBody.TimedBuff
					{
						buffIndex = CS$<>8__locals1.buffType,
						timer = CS$<>8__locals1.duration
					});
					this.AddBuff(CS$<>8__locals1.buffType);
					return;
				}
			}
			else if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.NullifyStack)
			{
				if (!this.HasBuff(RoR2Content.Buffs.Nullified))
				{
					int num6 = 0;
					for (int j = 0; j < this.timedBuffs.Count; j++)
					{
						if (this.timedBuffs[j].buffIndex == CS$<>8__locals1.buffType)
						{
							num6++;
							if (this.timedBuffs[j].timer < CS$<>8__locals1.duration)
							{
								this.timedBuffs[j].timer = CS$<>8__locals1.duration;
							}
						}
					}
					if (num6 < 2)
					{
						this.timedBuffs.Add(new CharacterBody.TimedBuff
						{
							buffIndex = CS$<>8__locals1.buffType,
							timer = CS$<>8__locals1.duration
						});
						this.AddBuff(CS$<>8__locals1.buffType);
						return;
					}
					this.ClearTimedBuffs(RoR2Content.Buffs.NullifyStack.buffIndex);
					this.AddTimedBuff(RoR2Content.Buffs.Nullified.buffIndex, 3f);
					return;
				}
			}
			else if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.AffixHauntedRecipient)
			{
				if (!this.HasBuff(RoR2Content.Buffs.AffixHaunted))
				{
					this.<AddTimedBuff>g__DefaultBehavior|32_0(ref CS$<>8__locals1);
					return;
				}
			}
			else
			{
				if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.LunarDetonationCharge)
				{
					this.<AddTimedBuff>g__RefreshStacks|32_1(ref CS$<>8__locals1);
					this.<AddTimedBuff>g__DefaultBehavior|32_0(ref CS$<>8__locals1);
					return;
				}
				if (CS$<>8__locals1.buffDef == RoR2Content.Buffs.Overheat)
				{
					this.<AddTimedBuff>g__RefreshStacks|32_1(ref CS$<>8__locals1);
					this.<AddTimedBuff>g__DefaultBehavior|32_0(ref CS$<>8__locals1);
					return;
				}
				this.<AddTimedBuff>g__DefaultBehavior|32_0(ref CS$<>8__locals1);
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0007B42C File Offset: 0x0007962C
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddTimedBuff(BuffIndex buffIndex, float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddTimedBuff(RoR2.BuffIndex,System.Single)' called on client");
				return;
			}
			this.AddTimedBuff(BuffCatalog.GetBuffDef(buffIndex), duration);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0007B450 File Offset: 0x00079650
		[Server]
		public void ClearTimedBuffs(BuffIndex buffType)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::ClearTimedBuffs(RoR2.BuffIndex)' called on client");
				return;
			}
			for (int i = this.timedBuffs.Count - 1; i >= 0; i--)
			{
				CharacterBody.TimedBuff timedBuff = this.timedBuffs[i];
				if (timedBuff.buffIndex == buffType)
				{
					this.timedBuffs.RemoveAt(i);
					this.RemoveBuff(timedBuff.buffIndex);
				}
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0007B4B8 File Offset: 0x000796B8
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClearTimedBuffs(BuffDef buffDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::ClearTimedBuffs(RoR2.BuffDef)' called on client");
				return;
			}
			this.ClearTimedBuffs((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0007B4E4 File Offset: 0x000796E4
		[Server]
		public void RemoveOldestTimedBuff(BuffIndex buffType)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::RemoveOldestTimedBuff(RoR2.BuffIndex)' called on client");
				return;
			}
			float num = float.NegativeInfinity;
			int num2 = -1;
			for (int i = this.timedBuffs.Count - 1; i >= 0; i--)
			{
				CharacterBody.TimedBuff timedBuff = this.timedBuffs[i];
				if (timedBuff.buffIndex == buffType && num < timedBuff.timer)
				{
					num = timedBuff.timer;
					num2 = i;
				}
			}
			if (num2 > 0)
			{
				this.timedBuffs.RemoveAt(num2);
				this.RemoveBuff(buffType);
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0007B565 File Offset: 0x00079765
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveOldestTimedBuff(BuffDef buffDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::RemoveOldestTimedBuff(RoR2.BuffDef)' called on client");
				return;
			}
			this.RemoveOldestTimedBuff((buffDef != null) ? buffDef.buffIndex : BuffIndex.None);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0007B590 File Offset: 0x00079790
		[Server]
		private void UpdateBuffs(float deltaTime)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::UpdateBuffs(System.Single)' called on client");
				return;
			}
			for (int i = this.timedBuffs.Count - 1; i >= 0; i--)
			{
				CharacterBody.TimedBuff timedBuff = this.timedBuffs[i];
				timedBuff.timer -= deltaTime;
				if (timedBuff.timer <= 0f)
				{
					this.timedBuffs.RemoveAt(i);
					this.RemoveBuff(timedBuff.buffIndex);
				}
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0007B60C File Offset: 0x0007980C
		[Client]
		private void OnClientBuffsChanged()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.CharacterBody::OnClientBuffsChanged()' called on server");
				return;
			}
			bool flag = this.HasBuff(RoR2Content.Buffs.WarCryBuff);
			if (!flag && this.warCryEffectInstance)
			{
				UnityEngine.Object.Destroy(this.warCryEffectInstance);
			}
			if (flag && !this.warCryEffectInstance)
			{
				Transform transform = this.mainHurtBox ? this.mainHurtBox.transform : this.transform;
				if (transform)
				{
					this.warCryEffectInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/WarCryEffect"), transform.position, Quaternion.identity, transform);
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0007B6AD File Offset: 0x000798AD
		public CharacterMaster master
		{
			get
			{
				if (!this.masterObject)
				{
					return null;
				}
				return this._master;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0007B6C4 File Offset: 0x000798C4
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0007B6CC File Offset: 0x000798CC
		public Inventory inventory { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0007B6D5 File Offset: 0x000798D5
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0007B6DD File Offset: 0x000798DD
		public bool isPlayerControlled { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0007B6E6 File Offset: 0x000798E6
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0007B6EE File Offset: 0x000798EE
		public float executeEliteHealthFraction { get; private set; }

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0007B6F8 File Offset: 0x000798F8
		private void UpdateHurtBoxesEnabled()
		{
			bool flag = (this.inventory && this.inventory.GetItemCount(RoR2Content.Items.Ghost) > 0) || this.HasBuff(RoR2Content.Buffs.Intangible);
			if (flag == this.disablingHurtBoxes)
			{
				return;
			}
			if (this.hurtBoxGroup)
			{
				if (flag)
				{
					HurtBoxGroup hurtBoxGroup = this.hurtBoxGroup;
					int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
					hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
				}
				else
				{
					HurtBoxGroup hurtBoxGroup2 = this.hurtBoxGroup;
					int hurtBoxesDeactivatorCounter = hurtBoxGroup2.hurtBoxesDeactivatorCounter - 1;
					hurtBoxGroup2.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
				}
			}
			this.disablingHurtBoxes = flag;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0007B784 File Offset: 0x00079984
		private void OnInventoryChanged()
		{
			EquipmentIndex currentEquipmentIndex = this.inventory.currentEquipmentIndex;
			if (currentEquipmentIndex != this.previousEquipmentIndex)
			{
				EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(this.previousEquipmentIndex);
				EquipmentDef equipmentDef2 = EquipmentCatalog.GetEquipmentDef(currentEquipmentIndex);
				if (equipmentDef != null)
				{
					this.OnEquipmentLost(equipmentDef);
				}
				if (equipmentDef2 != null)
				{
					this.OnEquipmentGained(equipmentDef2);
				}
				this.previousEquipmentIndex = currentEquipmentIndex;
			}
			this.statsDirty = true;
			this.UpdateHurtBoxesEnabled();
			this.AddItemBehavior<CharacterBody.AffixHauntedBehavior>(this.HasBuff(RoR2Content.Buffs.AffixHaunted) ? 1 : 0);
			this.AddItemBehavior<AffixEarthBehavior>(this.HasBuff(DLC1Content.Buffs.EliteEarth) ? 1 : 0);
			this.AddItemBehavior<AffixVoidBehavior>(this.HasBuff(DLC1Content.Buffs.EliteVoid) ? 1 : 0);
			if (NetworkServer.active)
			{
				this.AddItemBehavior<CharacterBody.QuestVolatileBatteryBehaviorServer>((this.inventory.GetEquipment((uint)this.inventory.activeEquipmentSlot).equipmentDef == RoR2Content.Equipment.QuestVolatileBattery) ? 1 : 0);
				this.AddItemBehavior<CharacterBody.ElementalRingsBehavior>(this.inventory.GetItemCount(RoR2Content.Items.IceRing) + this.inventory.GetItemCount(RoR2Content.Items.FireRing));
				this.AddItemBehavior<ElementalRingVoidBehavior>(this.inventory.GetItemCount(DLC1Content.Items.ElementalRingVoid));
				this.AddItemBehavior<OutOfCombatArmorBehavior>(this.inventory.GetItemCount(DLC1Content.Items.OutOfCombatArmor));
				this.AddItemBehavior<PrimarySkillShurikenBehavior>(this.inventory.GetItemCount(DLC1Content.Items.PrimarySkillShuriken));
				this.AddItemBehavior<MushroomVoidBehavior>(this.inventory.GetItemCount(DLC1Content.Items.MushroomVoid));
				this.AddItemBehavior<BearVoidBehavior>(this.inventory.GetItemCount(DLC1Content.Items.BearVoid));
				this.AddItemBehavior<LunarSunBehavior>(this.inventory.GetItemCount(DLC1Content.Items.LunarSun));
				this.AddItemBehavior<VoidMegaCrabItemBehavior>(this.inventory.GetItemCount(DLC1Content.Items.VoidMegaCrabItem));
				this.AddItemBehavior<DroneWeaponsBehavior>(this.inventory.GetItemCount(DLC1Content.Items.DroneWeapons));
				this.AddItemBehavior<DroneWeaponsBoostBehavior>(this.inventory.GetItemCount(DLC1Content.Items.DroneWeaponsBoost));
			}
			this.executeEliteHealthFraction = Util.ConvertAmplificationPercentageIntoReductionPercentage(13f * (float)this.inventory.GetItemCount(RoR2Content.Items.ExecuteLowHealthElite)) / 100f;
			if (this.skillLocator)
			{
				this.ReplaceSkillIfItemPresent(this.skillLocator.primary, RoR2Content.Items.LunarPrimaryReplacement.itemIndex, CharacterBody.CommonAssets.lunarPrimaryReplacementSkillDef);
				this.ReplaceSkillIfItemPresent(this.skillLocator.secondary, RoR2Content.Items.LunarSecondaryReplacement.itemIndex, CharacterBody.CommonAssets.lunarSecondaryReplacementSkillDef);
				this.ReplaceSkillIfItemPresent(this.skillLocator.special, RoR2Content.Items.LunarSpecialReplacement.itemIndex, CharacterBody.CommonAssets.lunarSpecialReplacementSkillDef);
				this.ReplaceSkillIfItemPresent(this.skillLocator.utility, RoR2Content.Items.LunarUtilityReplacement.itemIndex, CharacterBody.CommonAssets.lunarUtilityReplacementSkillDef);
			}
			Action action = this.onInventoryChanged;
			if (action != null)
			{
				action();
			}
			Action<CharacterBody> action2 = CharacterBody.onBodyInventoryChangedGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0007BA41 File Offset: 0x00079C41
		private void ReplaceSkillIfItemPresent(GenericSkill skill, ItemIndex itemIndex, SkillDef skillDef)
		{
			if (skill)
			{
				if (this.inventory.GetItemCount(itemIndex) > 0 && skillDef)
				{
					skill.SetSkillOverride(this, skillDef, GenericSkill.SkillOverridePriority.Replacement);
					return;
				}
				skill.UnsetSkillOverride(this, skillDef, GenericSkill.SkillOverridePriority.Replacement);
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0007BA75 File Offset: 0x00079C75
		private void OnEquipmentLost(EquipmentDef equipmentDef)
		{
			if (NetworkServer.active && equipmentDef.passiveBuffDef != null)
			{
				this.RemoveBuff(equipmentDef.passiveBuffDef);
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0007BA92 File Offset: 0x00079C92
		private void OnEquipmentGained(EquipmentDef equipmentDef)
		{
			if (NetworkServer.active && equipmentDef.passiveBuffDef != null)
			{
				this.AddBuff(equipmentDef.passiveBuffDef);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001CF9 RID: 7417 RVA: 0x0007BAB0 File Offset: 0x00079CB0
		// (remove) Token: 0x06001CFA RID: 7418 RVA: 0x0007BAE8 File Offset: 0x00079CE8
		public event Action onInventoryChanged;

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0007BB20 File Offset: 0x00079D20
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x0007BC1F File Offset: 0x00079E1F
		public GameObject masterObject
		{
			get
			{
				if (!this._masterObject)
				{
					if (NetworkServer.active)
					{
						this._masterObject = NetworkServer.FindLocalObject(this.masterObjectId);
					}
					else if (NetworkClient.active)
					{
						this._masterObject = ClientScene.FindLocalObject(this.masterObjectId);
					}
					this._master = (this._masterObject ? this._masterObject.GetComponent<CharacterMaster>() : null);
					if (this._master)
					{
						this.isPlayerControlled = this._masterObject.GetComponent<PlayerCharacterMasterController>();
						if (this.inventory)
						{
							this.inventory.onInventoryChanged -= this.OnInventoryChanged;
						}
						this.inventory = this._master.inventory;
						if (this.inventory)
						{
							this.inventory.onInventoryChanged += this.OnInventoryChanged;
							this.OnInventoryChanged();
						}
						this.statsDirty = true;
					}
				}
				return this._masterObject;
			}
			set
			{
				this.masterObjectId = value.GetComponent<NetworkIdentity>().netId;
				this.statsDirty = true;
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0007BC3C File Offset: 0x00079E3C
		private void UpdateMasterLink()
		{
			if (this.bodyFlags.HasFlag(CharacterBody.BodyFlags.Masterless))
			{
				return;
			}
			if (!this.linkedToMaster && this.master && this.master)
			{
				this.master.OnBodyStart(this);
				this.linkedToMaster = true;
				this.skinIndex = this.master.loadout.bodyLoadoutManager.GetSkinIndex(this.bodyIndex);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0007BCB8 File Offset: 0x00079EB8
		// (set) Token: 0x06001CFF RID: 7423 RVA: 0x0007BCC0 File Offset: 0x00079EC0
		public Rigidbody rigidbody { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0007BCC9 File Offset: 0x00079EC9
		// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0007BCD1 File Offset: 0x00079ED1
		public NetworkIdentity networkIdentity { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0007BCDA File Offset: 0x00079EDA
		// (set) Token: 0x06001D03 RID: 7427 RVA: 0x0007BCE2 File Offset: 0x00079EE2
		public CharacterMotor characterMotor { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0007BCEB File Offset: 0x00079EEB
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x0007BCF3 File Offset: 0x00079EF3
		public CharacterDirection characterDirection { get; private set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0007BCFC File Offset: 0x00079EFC
		// (set) Token: 0x06001D07 RID: 7431 RVA: 0x0007BD04 File Offset: 0x00079F04
		public TeamComponent teamComponent { get; private set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0007BD0D File Offset: 0x00079F0D
		// (set) Token: 0x06001D09 RID: 7433 RVA: 0x0007BD15 File Offset: 0x00079F15
		public HealthComponent healthComponent { get; private set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0007BD1E File Offset: 0x00079F1E
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0007BD26 File Offset: 0x00079F26
		public EquipmentSlot equipmentSlot { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0007BD2F File Offset: 0x00079F2F
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0007BD37 File Offset: 0x00079F37
		public InputBankTest inputBank { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0007BD40 File Offset: 0x00079F40
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0007BD48 File Offset: 0x00079F48
		public SkillLocator skillLocator { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0007BD51 File Offset: 0x00079F51
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0007BD59 File Offset: 0x00079F59
		public ModelLocator modelLocator { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0007BD62 File Offset: 0x00079F62
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x0007BD6A File Offset: 0x00079F6A
		public HurtBoxGroup hurtBoxGroup { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0007BD73 File Offset: 0x00079F73
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0007BD7B File Offset: 0x00079F7B
		public HurtBox mainHurtBox { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0007BD84 File Offset: 0x00079F84
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0007BD8C File Offset: 0x00079F8C
		public Transform coreTransform { get; private set; }

		// Token: 0x06001D18 RID: 7448 RVA: 0x0007BD95 File Offset: 0x00079F95
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			CharacterBody.AssetReferences.Resolve();
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0007BD9C File Offset: 0x00079F9C
		private void Awake()
		{
			this.transform = base.transform;
			this.rigidbody = base.GetComponent<Rigidbody>();
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			this.teamComponent = base.GetComponent<TeamComponent>();
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.equipmentSlot = base.GetComponent<EquipmentSlot>();
			this.skillLocator = base.GetComponent<SkillLocator>();
			this.modelLocator = base.GetComponent<ModelLocator>();
			this.characterMotor = base.GetComponent<CharacterMotor>();
			this.characterDirection = base.GetComponent<CharacterDirection>();
			this.inputBank = base.GetComponent<InputBankTest>();
			this.sfxLocator = base.GetComponent<SfxLocator>();
			this.activeBuffsList = BuffCatalog.GetPerBuffBuffer<BuffIndex>();
			this.buffs = BuffCatalog.GetPerBuffBuffer<int>();
			if (this.modelLocator)
			{
				this.modelLocator.onModelChanged += this.OnModelChanged;
				this.OnModelChanged(this.modelLocator.modelTransform);
			}
			this.radius = 1f;
			CapsuleCollider component = base.GetComponent<CapsuleCollider>();
			if (component)
			{
				this.radius = component.radius;
			}
			else
			{
				SphereCollider component2 = base.GetComponent<SphereCollider>();
				if (component2)
				{
					this.radius = component2.radius;
				}
			}
			try
			{
				Action<CharacterBody> action = CharacterBody.onBodyAwakeGlobal;
				if (action != null)
				{
					action(this);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0007BEF4 File Offset: 0x0007A0F4
		private void OnModelChanged(Transform modelTransform)
		{
			this.hurtBoxGroup = null;
			this.mainHurtBox = null;
			this.coreTransform = this.transform;
			if (modelTransform)
			{
				this.hurtBoxGroup = modelTransform.GetComponent<HurtBoxGroup>();
				if (this.hurtBoxGroup)
				{
					this.mainHurtBox = this.hurtBoxGroup.mainHurtBox;
					if (this.mainHurtBox)
					{
						this.coreTransform = this.mainHurtBox.transform;
					}
				}
			}
			if (this.overrideCoreTransform)
			{
				this.coreTransform = this.overrideCoreTransform;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0007BF84 File Offset: 0x0007A184
		private void Start()
		{
			this.UpdateAuthority();
			this.localStartTime = Run.FixedTimeStamp.now;
			bool flag = (this.bodyFlags & CharacterBody.BodyFlags.Masterless) > CharacterBody.BodyFlags.None;
			this.outOfCombatStopwatch = float.PositiveInfinity;
			this.outOfDangerStopwatch = float.PositiveInfinity;
			this.notMovingStopwatch = 0f;
			if (NetworkServer.active)
			{
				this.outOfCombat = true;
				this.outOfDanger = true;
			}
			this.RecalculateStats();
			this.UpdateMasterLink();
			if (flag)
			{
				this.healthComponent.Networkhealth = this.maxHealth;
			}
			if (this.sfxLocator && this.healthComponent.alive)
			{
				Util.PlaySound(this.sfxLocator.aliveLoopStart, base.gameObject);
			}
			Action<CharacterBody> action = CharacterBody.onBodyStartGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0007C043 File Offset: 0x0007A243
		public void Update()
		{
			this.UpdateSpreadBloom(Time.deltaTime);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0007C050 File Offset: 0x0007A250
		public void FixedUpdate()
		{
			this.outOfCombatStopwatch += Time.fixedDeltaTime;
			this.outOfDangerStopwatch += Time.fixedDeltaTime;
			this.aimTimer = Mathf.Max(this.aimTimer - Time.fixedDeltaTime, 0f);
			if (NetworkServer.active)
			{
				this.UpdateMultiKill(Time.fixedDeltaTime);
			}
			this.UpdateMasterLink();
			bool outOfCombat = this.outOfCombat;
			bool flag = outOfCombat;
			if (NetworkServer.active || this.hasEffectiveAuthority)
			{
				flag = (this.outOfCombatStopwatch >= 5f);
				if (this.outOfCombat != flag)
				{
					if (NetworkServer.active)
					{
						base.SetDirtyBit(4U);
					}
					this.outOfCombat = flag;
					this.statsDirty = true;
				}
			}
			if (NetworkServer.active)
			{
				this.UpdateBuffs(Time.fixedDeltaTime);
				bool flag2 = this.outOfDangerStopwatch >= 7f;
				bool outOfDanger = this.outOfDanger;
				bool flag3 = outOfCombat && outOfDanger;
				bool flag4 = flag && flag2;
				if (this.outOfDanger != flag2)
				{
					base.SetDirtyBit(8U);
					this.outOfDanger = flag2;
					this.statsDirty = true;
				}
				if (flag4 && !flag3)
				{
					this.OnOutOfCombatAndDangerServer();
				}
				Vector3 position = this.transform.position;
				float num = 0.1f * Time.fixedDeltaTime;
				if ((position - this.previousPosition).sqrMagnitude <= num * num)
				{
					this.notMovingStopwatch += Time.fixedDeltaTime;
				}
				else
				{
					this.notMovingStopwatch = 0f;
				}
				this.previousPosition = position;
				this.UpdateHelfire();
				this.UpdateAffixPoison(Time.fixedDeltaTime);
				this.UpdateAffixLunar(Time.fixedDeltaTime);
			}
			if (this.statsDirty)
			{
				this.RecalculateStats();
			}
			this.UpdateFireTrail();
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0007C1F0 File Offset: 0x0007A3F0
		public void OnDeathStart()
		{
			base.enabled = false;
			if (this.sfxLocator)
			{
				Util.PlaySound(this.sfxLocator.aliveLoopStop, base.gameObject);
			}
			if (NetworkServer.active && this.currentVehicle)
			{
				this.currentVehicle.EjectPassenger(base.gameObject);
			}
			if (this.master)
			{
				this.master.OnBodyDeath(this);
			}
			ModelLocator component = base.GetComponent<ModelLocator>();
			if (component)
			{
				Transform modelTransform = component.modelTransform;
				if (modelTransform)
				{
					CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
					if (component2)
					{
						component2.OnDeath();
					}
				}
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0007C299 File Offset: 0x0007A499
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			if (damageReport.damageDealt > 0f)
			{
				this.outOfDangerStopwatch = 0f;
			}
			if (this.master)
			{
				this.master.OnBodyDamaged(damageReport);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001D20 RID: 7456 RVA: 0x0007C2CC File Offset: 0x0007A4CC
		// (remove) Token: 0x06001D21 RID: 7457 RVA: 0x0007C304 File Offset: 0x0007A504
		public event Action<GenericSkill> onSkillActivatedServer;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001D22 RID: 7458 RVA: 0x0007C33C File Offset: 0x0007A53C
		// (remove) Token: 0x06001D23 RID: 7459 RVA: 0x0007C374 File Offset: 0x0007A574
		public event Action<GenericSkill> onSkillActivatedAuthority;

		// Token: 0x06001D24 RID: 7460 RVA: 0x0007C3AC File Offset: 0x0007A5AC
		public void OnSkillActivated(GenericSkill skill)
		{
			if (skill.isCombatSkill)
			{
				this.outOfCombatStopwatch = 0f;
			}
			if (this.hasEffectiveAuthority)
			{
				Action<GenericSkill> action = this.onSkillActivatedAuthority;
				if (action != null)
				{
					action(skill);
				}
			}
			if (!NetworkServer.active)
			{
				this.CallCmdOnSkillActivated((sbyte)this.skillLocator.FindSkillSlot(skill));
				return;
			}
			Action<GenericSkill> action2 = this.onSkillActivatedServer;
			if (action2 == null)
			{
				return;
			}
			action2(skill);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0007C414 File Offset: 0x0007A614
		public void OnDestroy()
		{
			try
			{
				Action<CharacterBody> action = CharacterBody.onBodyDestroyGlobal;
				if (action != null)
				{
					action(this);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			if (this.sfxLocator)
			{
				Util.PlaySound(this.sfxLocator.aliveLoopStop, base.gameObject);
			}
			if (this.modelLocator != null)
			{
				this.modelLocator.onModelChanged -= this.OnModelChanged;
			}
			if (this.inventory)
			{
				this.inventory.onInventoryChanged -= this.OnInventoryChanged;
			}
			if (this.master)
			{
				this.master.OnBodyDestroyed(this);
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0007C4CC File Offset: 0x0007A6CC
		public float GetNormalizedThreatValue()
		{
			if (Run.instance)
			{
				return (this.master ? this.master.money : 0f) / Mathf.Pow(Run.instance.compensatedDifficultyCoefficient, 2f);
			}
			return 0f;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x0007C522 File Offset: 0x0007A722
		private void OnEnable()
		{
			CharacterBody.instancesList.Add(this);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0007C52F File Offset: 0x0007A72F
		private void OnDisable()
		{
			CharacterBody.instancesList.Remove(this);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x0007C53D File Offset: 0x0007A73D
		private void OnValidate()
		{
			if (this.autoCalculateLevelStats)
			{
				this.PerformAutoCalculateLevelStats();
			}
			if (!Application.isPlaying && this.bodyIndex != BodyIndex.None)
			{
				this.bodyIndex = BodyIndex.None;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001D2A RID: 7466 RVA: 0x0007C564 File Offset: 0x0007A764
		// (remove) Token: 0x06001D2B RID: 7467 RVA: 0x0007C598 File Offset: 0x0007A798
		public static event Action<CharacterBody> onBodyAwakeGlobal;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001D2C RID: 7468 RVA: 0x0007C5CC File Offset: 0x0007A7CC
		// (remove) Token: 0x06001D2D RID: 7469 RVA: 0x0007C600 File Offset: 0x0007A800
		public static event Action<CharacterBody> onBodyDestroyGlobal;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001D2E RID: 7470 RVA: 0x0007C634 File Offset: 0x0007A834
		// (remove) Token: 0x06001D2F RID: 7471 RVA: 0x0007C668 File Offset: 0x0007A868
		public static event Action<CharacterBody> onBodyStartGlobal;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001D30 RID: 7472 RVA: 0x0007C69C File Offset: 0x0007A89C
		// (remove) Token: 0x06001D31 RID: 7473 RVA: 0x0007C6D0 File Offset: 0x0007A8D0
		public static event Action<CharacterBody> onBodyInventoryChangedGlobal;

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0007C703 File Offset: 0x0007A903
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0007C70B File Offset: 0x0007A90B
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x06001D34 RID: 7476 RVA: 0x0007C714 File Offset: 0x0007A914
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0007C727 File Offset: 0x0007A927
		public override void OnStartAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0007C727 File Offset: 0x0007A927
		public override void OnStopAuthority()
		{
			this.UpdateAuthority();
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0007C72F File Offset: 0x0007A92F
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0007C738 File Offset: 0x0007A938
		public bool isSprinting
		{
			get
			{
				return this._isSprinting;
			}
			set
			{
				if (this._isSprinting != value)
				{
					this._isSprinting = value;
					this.RecalculateStats();
					if (value)
					{
						this.OnSprintStart();
					}
					else
					{
						this.OnSprintStop();
					}
					if (NetworkServer.active)
					{
						base.SetDirtyBit(16U);
						return;
					}
					if (this.hasEffectiveAuthority)
					{
						this.CallCmdUpdateSprint(value);
					}
				}
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0007C78B File Offset: 0x0007A98B
		private void OnSprintStart()
		{
			if (this.sfxLocator)
			{
				Util.PlaySound(this.sfxLocator.sprintLoopStart, base.gameObject);
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0007C7B1 File Offset: 0x0007A9B1
		private void OnSprintStop()
		{
			if (this.sfxLocator)
			{
				Util.PlaySound(this.sfxLocator.sprintLoopStop, base.gameObject);
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0007C7D7 File Offset: 0x0007A9D7
		[Command]
		private void CmdUpdateSprint(bool newIsSprinting)
		{
			this.isSprinting = newIsSprinting;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0007C7E0 File Offset: 0x0007A9E0
		[Command]
		private void CmdOnSkillActivated(sbyte skillIndex)
		{
			this.OnSkillActivated(this.skillLocator.GetSkill((SkillSlot)skillIndex));
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0007C7F4 File Offset: 0x0007A9F4
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0007C7FC File Offset: 0x0007A9FC
		public bool outOfCombat { get; private set; } = true;

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0007C805 File Offset: 0x0007AA05
		// (set) Token: 0x06001D40 RID: 7488 RVA: 0x0007C80D File Offset: 0x0007AA0D
		public bool outOfDanger
		{
			get
			{
				return this._outOfDanger;
			}
			private set
			{
				if (this._outOfDanger == value)
				{
					return;
				}
				this._outOfDanger = value;
				this.OnOutOfDangerChanged();
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0007C826 File Offset: 0x0007AA26
		private void OnOutOfDangerChanged()
		{
			if (this.outOfDanger && this.healthComponent.shield != this.healthComponent.fullShield)
			{
				Util.PlaySound("Play_item_proc_personal_shield_recharge", base.gameObject);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0007C859 File Offset: 0x0007AA59
		[Server]
		private void OnOutOfCombatAndDangerServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::OnOutOfCombatAndDangerServer()' called on client");
				return;
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0007C870 File Offset: 0x0007AA70
		[Server]
		public bool GetNotMoving()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.CharacterBody::GetNotMoving()' called on client");
				return false;
			}
			return this.notMovingStopwatch >= 1f;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0007C898 File Offset: 0x0007AA98
		public void PerformAutoCalculateLevelStats()
		{
			this.levelMaxHealth = Mathf.Round(this.baseMaxHealth * 0.3f);
			this.levelMaxShield = Mathf.Round(this.baseMaxShield * 0.3f);
			this.levelRegen = this.baseRegen * 0.2f;
			this.levelMoveSpeed = 0f;
			this.levelJumpPower = 0f;
			this.levelDamage = this.baseDamage * 0.2f;
			this.levelAttackSpeed = 0f;
			this.levelCrit = 0f;
			this.levelArmor = 0f;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0007C92E File Offset: 0x0007AB2E
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x0007C936 File Offset: 0x0007AB36
		public float experience { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0007C93F File Offset: 0x0007AB3F
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0007C947 File Offset: 0x0007AB47
		public float level { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x0007C950 File Offset: 0x0007AB50
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x0007C958 File Offset: 0x0007AB58
		public float maxHealth { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0007C961 File Offset: 0x0007AB61
		// (set) Token: 0x06001D4C RID: 7500 RVA: 0x0007C969 File Offset: 0x0007AB69
		public float maxBarrier { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0007C972 File Offset: 0x0007AB72
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x0007C97A File Offset: 0x0007AB7A
		public float barrierDecayRate { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0007C983 File Offset: 0x0007AB83
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0007C98B File Offset: 0x0007AB8B
		public float regen { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0007C994 File Offset: 0x0007AB94
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0007C99C File Offset: 0x0007AB9C
		public float maxShield { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0007C9A5 File Offset: 0x0007ABA5
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0007C9AD File Offset: 0x0007ABAD
		public float moveSpeed { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0007C9B6 File Offset: 0x0007ABB6
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0007C9BE File Offset: 0x0007ABBE
		public float acceleration { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x0007C9C7 File Offset: 0x0007ABC7
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0007C9CF File Offset: 0x0007ABCF
		public float jumpPower { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0007C9D8 File Offset: 0x0007ABD8
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0007C9E0 File Offset: 0x0007ABE0
		public int maxJumpCount { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0007C9E9 File Offset: 0x0007ABE9
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0007C9F1 File Offset: 0x0007ABF1
		public float maxJumpHeight { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x0007C9FA File Offset: 0x0007ABFA
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x0007CA02 File Offset: 0x0007AC02
		public float damage { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x0007CA0B File Offset: 0x0007AC0B
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x0007CA13 File Offset: 0x0007AC13
		public float attackSpeed { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0007CA1C File Offset: 0x0007AC1C
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0007CA24 File Offset: 0x0007AC24
		public float crit { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0007CA2D File Offset: 0x0007AC2D
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0007CA35 File Offset: 0x0007AC35
		public float critMultiplier { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0007CA3E File Offset: 0x0007AC3E
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x0007CA46 File Offset: 0x0007AC46
		public float bleedChance { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0007CA4F File Offset: 0x0007AC4F
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0007CA57 File Offset: 0x0007AC57
		public float armor { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0007CA60 File Offset: 0x0007AC60
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0007CA68 File Offset: 0x0007AC68
		public float visionDistance { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0007CA71 File Offset: 0x0007AC71
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0007CA79 File Offset: 0x0007AC79
		public float critHeal { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0007CA82 File Offset: 0x0007AC82
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0007CA8A File Offset: 0x0007AC8A
		public float cursePenalty { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0007CA93 File Offset: 0x0007AC93
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x0007CA9B File Offset: 0x0007AC9B
		public bool hasOneShotProtection { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0007CAA4 File Offset: 0x0007ACA4
		// (set) Token: 0x06001D72 RID: 7538 RVA: 0x0007CAAC File Offset: 0x0007ACAC
		public bool isGlass { get; private set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x0007CAB5 File Offset: 0x0007ACB5
		// (set) Token: 0x06001D74 RID: 7540 RVA: 0x0007CABD File Offset: 0x0007ACBD
		public float oneShotProtectionFraction { get; private set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0007CAC6 File Offset: 0x0007ACC6
		// (set) Token: 0x06001D76 RID: 7542 RVA: 0x0007CACE File Offset: 0x0007ACCE
		public bool canPerformBackstab { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x0007CAD7 File Offset: 0x0007ACD7
		// (set) Token: 0x06001D78 RID: 7544 RVA: 0x0007CADF File Offset: 0x0007ACDF
		public bool canReceiveBackstab { get; private set; }

		// Token: 0x06001D79 RID: 7545 RVA: 0x0007CAE8 File Offset: 0x0007ACE8
		public void MarkAllStatsDirty()
		{
			this.statsDirty = true;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0007CAF4 File Offset: 0x0007ACF4
		public void RecalculateStats()
		{
			float level = this.level;
			TeamManager.instance.GetTeamExperience(this.teamComponent.teamIndex);
			float level2 = TeamManager.instance.GetTeamLevel(this.teamComponent.teamIndex);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			int num13 = 0;
			int num14 = 0;
			int num15 = 0;
			int num16 = 0;
			int num17 = 0;
			int num18 = 0;
			int bonusStockFromBody = 0;
			int num19 = 0;
			int num20 = 0;
			int num21 = 0;
			int num22 = 0;
			int num23 = 0;
			int num24 = 0;
			int num25 = 0;
			int num26 = 0;
			int num27 = 0;
			int num28 = 0;
			int num29 = 0;
			int num30 = 0;
			int num31 = 0;
			int num32 = 0;
			int num33 = 0;
			int num34 = 0;
			int num35 = 0;
			int num36 = 0;
			int num37 = 0;
			int num38 = 0;
			int num39 = 0;
			int num40 = 0;
			int num41 = 0;
			int num42 = 0;
			int num43 = 0;
			int num44 = 0;
			int num45 = 0;
			int num46 = 0;
			int num47 = 0;
			EquipmentIndex equipmentIndex = EquipmentIndex.None;
			uint num48 = 0U;
			if (this.inventory)
			{
				num = this.inventory.GetItemCount(RoR2Content.Items.LevelBonus);
				num2 = this.inventory.GetItemCount(RoR2Content.Items.Infusion);
				num3 = this.inventory.GetItemCount(RoR2Content.Items.HealWhileSafe);
				num4 = this.inventory.GetItemCount(RoR2Content.Items.PersonalShield);
				num5 = this.inventory.GetItemCount(RoR2Content.Items.Hoof);
				num6 = this.inventory.GetItemCount(RoR2Content.Items.SprintOutOfCombat);
				num7 = this.inventory.GetItemCount(RoR2Content.Items.Feather);
				num8 = this.inventory.GetItemCount(RoR2Content.Items.Syringe);
				num9 = this.inventory.GetItemCount(RoR2Content.Items.CritGlasses);
				num10 = this.inventory.GetItemCount(RoR2Content.Items.AttackSpeedOnCrit);
				num11 = this.inventory.GetItemCount(JunkContent.Items.CooldownOnCrit);
				num12 = this.inventory.GetItemCount(RoR2Content.Items.HealOnCrit);
				num13 = this.inventory.GetItemCount(RoR2Content.Items.ShieldOnly);
				num14 = this.inventory.GetItemCount(RoR2Content.Items.AlienHead);
				num15 = this.inventory.GetItemCount(RoR2Content.Items.Knurl);
				num16 = this.inventory.GetItemCount(RoR2Content.Items.BoostHp);
				num17 = this.inventory.GetItemCount(JunkContent.Items.CritHeal);
				num18 = this.inventory.GetItemCount(RoR2Content.Items.SprintBonus);
				bonusStockFromBody = this.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine);
				num20 = this.inventory.GetItemCount(RoR2Content.Items.SprintArmor);
				num21 = this.inventory.GetItemCount(RoR2Content.Items.UtilitySkillMagazine);
				num22 = this.inventory.GetItemCount(RoR2Content.Items.HealthDecay);
				num24 = this.inventory.GetItemCount(RoR2Content.Items.TonicAffliction);
				num25 = this.inventory.GetItemCount(RoR2Content.Items.LunarDagger);
				num23 = this.inventory.GetItemCount(RoR2Content.Items.DrizzlePlayerHelper);
				num26 = this.inventory.GetItemCount(RoR2Content.Items.MonsoonPlayerHelper);
				num27 = this.inventory.GetItemCount(RoR2Content.Items.Pearl);
				num28 = this.inventory.GetItemCount(RoR2Content.Items.ShinyPearl);
				num29 = this.inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger);
				num30 = this.inventory.GetItemCount(RoR2Content.Items.CutHp);
				num31 = this.inventory.GetItemCount(RoR2Content.Items.BoostAttackSpeed);
				num32 = this.inventory.GetItemCount(RoR2Content.Items.BleedOnHitAndExplode);
				num33 = this.inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);
				num34 = this.inventory.GetItemCount(RoR2Content.Items.FlatHealth);
				num35 = this.inventory.GetItemCount(RoR2Content.Items.TeamSizeDamageBonus);
				num36 = this.inventory.GetItemCount(RoR2Content.Items.SummonedEcho);
				num37 = this.inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel);
				num19 = this.inventory.GetItemCount(DLC1Content.Items.EquipmentMagazineVoid);
				num41 = this.inventory.GetItemCount(DLC1Content.Items.HalfAttackSpeedHalfCooldowns);
				num42 = this.inventory.GetItemCount(DLC1Content.Items.HalfSpeedDoubleHealth);
				num38 = this.inventory.GetItemCount(RoR2Content.Items.BleedOnHit);
				num39 = this.inventory.GetItemCount(DLC1Content.Items.AttackSpeedAndMoveSpeed);
				num40 = this.inventory.GetItemCount(DLC1Content.Items.CritDamage);
				num43 = this.inventory.GetItemCount(DLC1Content.Items.ConvertCritChanceToCritDamage);
				num44 = this.inventory.GetItemCount(DLC1Content.Items.DroneWeaponsBoost);
				num45 = this.inventory.GetItemCount(DLC1Content.Items.MissileVoid);
				equipmentIndex = this.inventory.currentEquipmentIndex;
				num48 = this.inventory.infusionBonus;
				num46 = ((equipmentIndex == DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex) ? 1 : 0);
				num47 = this.inventory.GetItemCount(DLC1Content.Items.OutOfCombatArmor);
				this.inventory.GetItemCount(DLC1Content.Items.VoidmanPassiveItem);
			}
			this.level = level2;
			if (num37 > 0)
			{
				this.level = Math.Max(this.level, (float)Run.instance.ambientLevelFloor);
			}
			this.level += (float)num;
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
			float num49 = this.level - 1f;
			this.isElite = (this.eliteBuffCount > 0);
			bool flag = this.HasBuff(RoR2Content.Buffs.TonicBuff);
			bool flag2 = this.HasBuff(RoR2Content.Buffs.Entangle);
			bool flag3 = this.HasBuff(RoR2Content.Buffs.Nullified);
			bool flag4 = this.HasBuff(RoR2Content.Buffs.LunarSecondaryRoot);
			bool flag5 = this.teamComponent.teamIndex == TeamIndex.Player && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.glassArtifactDef);
			object obj = num13 > 0 || this.HasBuff(RoR2Content.Buffs.AffixLunar);
			bool flag6 = equipmentDef != null && equipmentDef == JunkContent.Equipment.EliteYellowEquipment;
			this.hasOneShotProtection = this.isPlayerControlled;
			int buffCount = this.GetBuffCount(RoR2Content.Buffs.BeetleJuice);
			this.isGlass = (flag5 || num25 > 0);
			this.canPerformBackstab = ((this.bodyFlags & CharacterBody.BodyFlags.HasBackstabPassive) == CharacterBody.BodyFlags.HasBackstabPassive);
			this.canReceiveBackstab = ((this.bodyFlags & CharacterBody.BodyFlags.HasBackstabImmunity) != CharacterBody.BodyFlags.HasBackstabImmunity);
			float maxHealth = this.maxHealth;
			float maxShield = this.maxShield;
			float num50 = this.baseMaxHealth + this.levelMaxHealth * num49;
			float num51 = 1f;
			num51 += (float)num16 * 0.1f;
			num51 += (float)(num27 + num28) * 0.1f;
			num51 += (float)num46 * 0.5f;
			num51 += (float)num42 * 1f;
			if (num2 > 0)
			{
				num50 += num48;
			}
			num50 += (float)num34 * 25f;
			num50 += (float)num15 * 40f;
			num50 *= num51;
			num50 /= (float)(num30 + 1);
			if (num29 > 0)
			{
				num50 *= 10f;
			}
			if (num36 > 0)
			{
				num50 *= 0.1f;
			}
			this.maxHealth = num50;
			float num52 = this.baseMaxShield + this.levelMaxShield * num49;
			num52 += (float)num4 * 0.08f * this.maxHealth;
			if (this.HasBuff(RoR2Content.Buffs.EngiShield))
			{
				num52 += this.maxHealth * 1f;
			}
			if (this.HasBuff(JunkContent.Buffs.EngiTeamShield))
			{
				num52 += this.maxHealth * 0.5f;
			}
			if (num45 > 0)
			{
				num52 += this.maxHealth * 0.1f;
			}
			object obj2 = obj;
			if (obj2 != null)
			{
				num52 += this.maxHealth * (1.5f + (float)(num13 - 1) * 0.25f);
				this.maxHealth = 1f;
			}
			if (this.HasBuff(RoR2Content.Buffs.AffixBlue))
			{
				float num53 = this.maxHealth * 0.5f;
				this.maxHealth -= num53;
				num52 += this.maxHealth;
			}
			this.maxShield = num52;
			float num54 = this.baseRegen + this.levelRegen * num49;
			float num55 = 1f + num49 * 0.2f;
			float num56 = (float)num15 * 1.6f * num55;
			float num57 = ((this.outOfDanger && num3 > 0) ? (3f * (float)num3) : 0f) * num55;
			float num58 = (this.HasBuff(JunkContent.Buffs.MeatRegenBoost) ? 2f : 0f) * num55;
			float num59 = (float)this.GetBuffCount(RoR2Content.Buffs.CrocoRegen) * this.maxHealth * 0.1f;
			float num60 = (float)num28 * 0.1f * num55;
			float num61 = 1f;
			if (num23 > 0)
			{
				num61 += 0.5f;
			}
			if (num26 > 0)
			{
				num61 -= 0.4f;
			}
			float num62 = (num54 + num56 + num57 + num58 + num60) * num61;
			if (this.HasBuff(RoR2Content.Buffs.OnFire) || this.HasBuff(DLC1Content.Buffs.StrongerBurn))
			{
				num62 = Mathf.Min(0f, num62);
			}
			num62 += num59;
			if (obj2 != null)
			{
				num62 = Mathf.Max(num62, 0f);
			}
			if (num22 > 0)
			{
				num62 = Mathf.Min(num62, 0f) - this.maxHealth / this.cursePenalty / (float)num22;
			}
			this.regen = num62;
			float num63 = this.baseMoveSpeed + this.levelMoveSpeed * num49;
			float num64 = 1f;
			if (flag6)
			{
				num63 += 2f;
			}
			if (this.isSprinting)
			{
				num63 *= this.sprintingSpeedMultiplier;
			}
			num64 += (float)num5 * 0.14f;
			num64 += (float)num39 * 0.07f;
			num64 += (float)num28 * 0.1f;
			num64 += 0.25f * (float)this.GetBuffCount(DLC1Content.Buffs.KillMoveSpeed);
			if (this.teamComponent.teamIndex == TeamIndex.Monster && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse4)
			{
				num64 += 0.4f;
			}
			if (this.isSprinting && num18 > 0)
			{
				num64 += 0.25f * (float)num18 / this.sprintingSpeedMultiplier;
			}
			if (num6 > 0 && this.HasBuff(RoR2Content.Buffs.WhipBoost))
			{
				num64 += (float)num6 * 0.3f;
			}
			if (num36 > 0)
			{
				num64 += 0.66f;
			}
			if (this.HasBuff(RoR2Content.Buffs.BugWings))
			{
				num64 += 0.2f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Warbanner))
			{
				num64 += 0.3f;
			}
			if (this.HasBuff(JunkContent.Buffs.EnrageAncientWisp))
			{
				num64 += 0.4f;
			}
			if (this.HasBuff(RoR2Content.Buffs.CloakSpeed))
			{
				num64 += 0.4f;
			}
			if (this.HasBuff(RoR2Content.Buffs.WarCryBuff) || this.HasBuff(RoR2Content.Buffs.TeamWarCry))
			{
				num64 += 0.5f;
			}
			if (this.HasBuff(JunkContent.Buffs.EngiTeamShield))
			{
				num64 += 0.3f;
			}
			if (this.HasBuff(RoR2Content.Buffs.AffixLunar))
			{
				num64 += 0.3f;
			}
			float num65 = 1f;
			if (this.HasBuff(RoR2Content.Buffs.Slow50))
			{
				num65 += 0.5f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Slow60))
			{
				num65 += 0.6f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Slow80))
			{
				num65 += 0.8f;
			}
			if (this.HasBuff(RoR2Content.Buffs.ClayGoo))
			{
				num65 += 0.5f;
			}
			if (this.HasBuff(JunkContent.Buffs.Slow30))
			{
				num65 += 0.3f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Cripple))
			{
				num65 += 1f;
			}
			if (this.HasBuff(DLC1Content.Buffs.JailerSlow))
			{
				num65 += 1f;
			}
			num65 += (float)num42 * 1f;
			num63 *= num64 / num65;
			if (buffCount > 0)
			{
				num63 *= 1f - 0.05f * (float)buffCount;
			}
			this.moveSpeed = num63;
			this.acceleration = this.moveSpeed / this.baseMoveSpeed * this.baseAcceleration;
			if (flag2 || flag3 || flag4)
			{
				this.moveSpeed = 0f;
				this.acceleration = 80f;
			}
			float jumpPower = this.baseJumpPower + this.levelJumpPower * num49;
			this.jumpPower = jumpPower;
			this.maxJumpHeight = Trajectory.CalculateApex(this.jumpPower);
			this.maxJumpCount = this.baseJumpCount + num7;
			this.oneShotProtectionFraction = 0.1f;
			float num66 = this.baseDamage + this.levelDamage * num49;
			float num67 = 1f;
			int num68 = this.inventory ? this.inventory.GetItemCount(RoR2Content.Items.BoostDamage) : 0;
			if (num68 > 0)
			{
				num67 += (float)num68 * 0.1f;
			}
			if (num35 > 0)
			{
				int num69 = Math.Max(TeamComponent.GetTeamMembers(this.teamComponent.teamIndex).Count - 1, 0);
				num67 += (float)(num69 * num35) * 1f;
			}
			if (buffCount > 0)
			{
				num67 -= 0.05f * (float)buffCount;
			}
			if (this.HasBuff(JunkContent.Buffs.GoldEmpowered))
			{
				num67 += 1f;
			}
			if (this.HasBuff(RoR2Content.Buffs.PowerBuff))
			{
				num67 += 0.5f;
			}
			num67 += (float)num28 * 0.1f;
			num67 += Mathf.Pow(2f, (float)num25) - 1f;
			num67 -= (float)num46 * 0.3f;
			num66 *= num67;
			if (num29 > 0)
			{
				num66 *= 0.04f;
			}
			if (flag5)
			{
				num66 *= 5f;
			}
			this.damage = num66;
			float num70 = this.baseAttackSpeed + this.levelAttackSpeed * num49;
			float num71 = 1f;
			num71 += (float)num31 * 0.1f;
			num71 += (float)num8 * 0.15f;
			num71 += (float)num39 * 0.075f;
			num71 += (float)num44 * 0.5f;
			if (flag6)
			{
				num71 += 0.5f;
			}
			num71 += (float)this.GetBuffCount(RoR2Content.Buffs.AttackSpeedOnCrit) * 0.12f;
			if (this.HasBuff(RoR2Content.Buffs.Warbanner))
			{
				num71 += 0.3f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Energized))
			{
				num71 += 0.7f;
			}
			if (this.HasBuff(RoR2Content.Buffs.WarCryBuff) || this.HasBuff(RoR2Content.Buffs.TeamWarCry))
			{
				num71 += 1f;
			}
			num71 += (float)num28 * 0.1f;
			num71 /= (float)(num41 + 1);
			num71 = Mathf.Max(num71, 0.1f);
			num70 *= num71;
			if (buffCount > 0)
			{
				num70 *= 1f - 0.05f * (float)buffCount;
			}
			this.attackSpeed = num70;
			this.critMultiplier = 2f + 1f * (float)num40;
			float num72 = this.baseCrit + this.levelCrit * num49;
			num72 += (float)num9 * 10f;
			if (num10 > 0)
			{
				num72 += 5f;
			}
			if (num32 > 0)
			{
				num72 += 5f;
			}
			if (num11 > 0)
			{
				num72 += 5f;
			}
			if (num12 > 0)
			{
				num72 += 5f;
			}
			if (num17 > 0)
			{
				num72 += 5f;
			}
			if (this.HasBuff(RoR2Content.Buffs.FullCrit))
			{
				num72 += 100f;
			}
			num72 += (float)num28 * 10f;
			if (num43 == 0)
			{
				this.crit = num72;
			}
			else
			{
				this.critMultiplier += num72 * 0.01f;
				this.crit = 0f;
			}
			this.armor = this.baseArmor + this.levelArmor * num49;
			if (num28 > 0)
			{
				this.armor *= 1f + 0.1f * (float)num28;
			}
			this.armor += (float)num23 * 70f;
			this.armor += (this.HasBuff(RoR2Content.Buffs.ArmorBoost) ? 200f : 0f);
			this.armor += (this.HasBuff(RoR2Content.Buffs.SmallArmorBoost) ? 100f : 0f);
			this.armor += (this.HasBuff(DLC1Content.Buffs.OutOfCombatArmorBuff) ? (100f * (float)num47) : 0f);
			this.armor += (this.HasBuff(RoR2Content.Buffs.ElephantArmorBoost) ? 500f : 0f);
			this.armor += (this.HasBuff(DLC1Content.Buffs.VoidSurvivorCorruptMode) ? 100f : 0f);
			if (this.HasBuff(RoR2Content.Buffs.Cripple))
			{
				this.armor -= 20f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Pulverized))
			{
				this.armor -= 60f;
			}
			if (this.isSprinting && num20 > 0)
			{
				this.armor += (float)(num20 * 30);
			}
			int buffCount2 = this.GetBuffCount(DLC1Content.Buffs.PermanentDebuff);
			this.armor -= (float)buffCount2 * 2f;
			float num73 = 0f;
			if (num33 > 0)
			{
				num73 += 2f + 1f * (float)(num33 - 1);
			}
			float num74 = 1f;
			if (this.HasBuff(JunkContent.Buffs.GoldEmpowered))
			{
				num74 *= 0.25f;
			}
			for (int i = 0; i < num14; i++)
			{
				num74 *= 0.75f;
			}
			for (int j = 0; j < num41; j++)
			{
				num74 *= 0.5f;
			}
			for (int k = 0; k < num44; k++)
			{
				num74 *= 0.5f;
			}
			if (this.teamComponent.teamIndex == TeamIndex.Monster && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse7)
			{
				num74 *= 0.5f;
			}
			if (this.HasBuff(RoR2Content.Buffs.NoCooldowns))
			{
				num74 = 0f;
			}
			if (this.skillLocator.primary)
			{
				this.skillLocator.primary.cooldownScale = num74;
				this.skillLocator.primary.flatCooldownReduction = num73;
			}
			if (this.skillLocator.secondaryBonusStockSkill)
			{
				this.skillLocator.secondaryBonusStockSkill.cooldownScale = num74;
				this.skillLocator.secondaryBonusStockSkill.SetBonusStockFromBody(bonusStockFromBody);
				this.skillLocator.secondaryBonusStockSkill.flatCooldownReduction = num73;
			}
			if (this.skillLocator.utilityBonusStockSkill)
			{
				float num75 = num74;
				if (num21 > 0)
				{
					num75 *= 0.6666667f;
				}
				this.skillLocator.utilityBonusStockSkill.cooldownScale = num75;
				this.skillLocator.utilityBonusStockSkill.flatCooldownReduction = num73;
				this.skillLocator.utilityBonusStockSkill.SetBonusStockFromBody(num21 * 2);
			}
			if (this.skillLocator.specialBonusStockSkill)
			{
				this.skillLocator.specialBonusStockSkill.cooldownScale = num74;
				if (num19 > 0)
				{
					this.skillLocator.specialBonusStockSkill.cooldownScale *= 0.67f;
				}
				this.skillLocator.specialBonusStockSkill.flatCooldownReduction = num73;
				this.skillLocator.specialBonusStockSkill.SetBonusStockFromBody(num19);
			}
			this.critHeal = 0f;
			if (num17 > 0)
			{
				float crit = this.crit;
				this.crit /= (float)(num17 + 1);
				this.critHeal = crit - this.crit;
			}
			this.cursePenalty = 1f;
			if (num25 > 0)
			{
				this.cursePenalty = Mathf.Pow(2f, (float)num25);
			}
			if (flag5)
			{
				this.cursePenalty *= 10f;
			}
			int buffCount3 = this.GetBuffCount(RoR2Content.Buffs.PermanentCurse);
			if (buffCount3 > 0)
			{
				this.cursePenalty += (float)buffCount3 * 0.01f;
			}
			if (this.HasBuff(RoR2Content.Buffs.Weak))
			{
				this.armor -= 30f;
				this.damage *= 0.6f;
				this.moveSpeed *= 0.6f;
			}
			if (flag)
			{
				this.maxHealth *= 1.5f;
				this.maxShield *= 1.5f;
				this.attackSpeed *= 1.7f;
				this.moveSpeed *= 1.3f;
				this.armor += 20f;
				this.damage *= 2f;
				this.regen *= 4f;
			}
			else if (num24 > 0)
			{
				float num76 = Mathf.Pow(0.95f, (float)num24);
				this.attackSpeed *= num76;
				this.moveSpeed *= num76;
				this.damage *= num76;
				this.regen *= num76;
				this.cursePenalty += 0.1f * (float)num24;
			}
			this.maxHealth /= this.cursePenalty;
			this.maxShield /= this.cursePenalty;
			this.oneShotProtectionFraction = Mathf.Max(0f, this.oneShotProtectionFraction - (1f - 1f / this.cursePenalty));
			this.maxBarrier = this.maxHealth + this.maxShield;
			this.barrierDecayRate = this.maxBarrier / 30f;
			if (NetworkServer.active)
			{
				float num77 = this.maxHealth - maxHealth;
				float num78 = this.maxShield - maxShield;
				if (num77 > 0f)
				{
					this.healthComponent.Heal(num77, default(ProcChainMask), false);
				}
				else if (this.healthComponent.health > this.maxHealth)
				{
					this.healthComponent.Networkhealth = Mathf.Max(this.healthComponent.health + num77, this.maxHealth);
				}
				if (num78 > 0f)
				{
					this.healthComponent.RechargeShield(num78);
				}
				else if (this.healthComponent.shield > this.maxShield)
				{
					this.healthComponent.Networkshield = Mathf.Max(this.healthComponent.shield + num78, this.maxShield);
				}
			}
			this.bleedChance = 10f * (float)num38;
			this.visionDistance = this.baseVisionDistance;
			if (this.HasBuff(DLC1Content.Buffs.Blinded))
			{
				this.visionDistance = Mathf.Min(this.visionDistance, 15f);
			}
			if (this.level != level)
			{
				this.OnCalculatedLevelChanged(level, this.level);
			}
			this.UpdateAllTemporaryVisualEffects();
			this.statsDirty = false;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0007CAE8 File Offset: 0x0007ACE8
		public void OnTeamLevelChanged()
		{
			this.statsDirty = true;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0007E08C File Offset: 0x0007C28C
		private void OnCalculatedLevelChanged(float oldLevel, float newLevel)
		{
			if (newLevel > oldLevel)
			{
				int num = Mathf.FloorToInt(oldLevel);
				if (Mathf.FloorToInt(newLevel) > num && num != 0)
				{
					this.OnLevelUp();
				}
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0007E0B6 File Offset: 0x0007C2B6
		private void OnLevelUp()
		{
			GlobalEventManager.OnCharacterLevelUp(this);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0007E0BE File Offset: 0x0007C2BE
		public void SetAimTimer(float duration)
		{
			this.aimTimer = duration;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0007E0C7 File Offset: 0x0007C2C7
		public bool shouldAim
		{
			get
			{
				return this.aimTimer > 0f && !this.isSprinting;
			}
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			byte b = reader.ReadByte();
			if ((b & 1) != 0)
			{
				NetworkInstanceId c = reader.ReadNetworkId();
				if (c != this.masterObjectId)
				{
					this.masterObjectId = c;
					this.statsDirty = true;
				}
			}
			if ((b & 2) != 0)
			{
				this.ReadBuffs(reader);
			}
			if ((b & 4) != 0)
			{
				bool flag = reader.ReadBoolean();
				if (!this.hasEffectiveAuthority && flag != this.outOfCombat)
				{
					this.outOfCombat = flag;
					this.statsDirty = true;
				}
			}
			if ((b & 8) != 0)
			{
				bool flag2 = reader.ReadBoolean();
				if (flag2 != this.outOfDanger)
				{
					this.outOfDanger = flag2;
					this.statsDirty = true;
				}
			}
			if ((b & 16) != 0)
			{
				bool flag3 = reader.ReadBoolean();
				if (flag3 != this.isSprinting && !this.hasEffectiveAuthority)
				{
					this.statsDirty = true;
					this.isSprinting = flag3;
				}
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0007E1A4 File Offset: 0x0007C3A4
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 31U;
			}
			bool flag = (num & 1U) > 0U;
			bool flag2 = (num & 2U) > 0U;
			bool flag3 = (num & 4U) > 0U;
			bool flag4 = (num & 8U) > 0U;
			bool flag5 = (num & 16U) > 0U;
			writer.Write((byte)num);
			if (flag)
			{
				writer.Write(this.masterObjectId);
			}
			if (flag2)
			{
				this.WriteBuffs(writer);
			}
			if (flag3)
			{
				writer.Write(this.outOfCombat);
			}
			if (flag4)
			{
				writer.Write(this.outOfDanger);
			}
			if (flag5)
			{
				writer.Write(this.isSprinting);
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0007E23C File Offset: 0x0007C43C
		public T AddItemBehavior<T>(int stack) where T : CharacterBody.ItemBehavior
		{
			T t = base.GetComponent<T>();
			if (stack > 0)
			{
				if (!t)
				{
					t = base.gameObject.AddComponent<T>();
					t.body = this;
					t.enabled = true;
				}
				t.stack = stack;
				return t;
			}
			if (t)
			{
				UnityEngine.Object.Destroy(t);
			}
			return default(T);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0007E2B4 File Offset: 0x0007C4B4
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0007E2BC File Offset: 0x0007C4BC
		public int killCountServer { get; private set; }

		// Token: 0x06001D85 RID: 7557 RVA: 0x0007E2C8 File Offset: 0x0007C4C8
		public void HandleOnKillEffectsServer(DamageReport damageReport)
		{
			int killCountServer = this.killCountServer + 1;
			this.killCountServer = killCountServer;
			this.AddMultiKill(1);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnKilledOtherServer(DamageReport damageReport)
		{
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0007E2EC File Offset: 0x0007C4EC
		public void AddHelfireDuration(float duration)
		{
			this.helfireLifetime = duration;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0007E2F8 File Offset: 0x0007C4F8
		[Server]
		private void UpdateHelfire()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::UpdateHelfire()' called on client");
				return;
			}
			this.helfireLifetime -= Time.fixedDeltaTime;
			bool flag = false;
			if (this.inventory)
			{
				flag = (this.inventory.GetItemCount(JunkContent.Items.BurnNearby) > 0 || this.helfireLifetime > 0f);
			}
			if (this.helfireController != flag)
			{
				if (flag)
				{
					this.helfireController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HelfireController")).GetComponent<HelfireController>();
					this.helfireController.networkedBodyAttachment.AttachToGameObjectAndSpawn(base.gameObject, null);
					return;
				}
				UnityEngine.Object.Destroy(this.helfireController.gameObject);
				this.helfireController = null;
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0007E3BC File Offset: 0x0007C5BC
		private void UpdateFireTrail()
		{
			bool flag = this.HasBuff(RoR2Content.Buffs.AffixRed);
			if (flag != this.fireTrail)
			{
				if (flag)
				{
					this.fireTrail = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/FireTrail"), this.transform).GetComponent<DamageTrail>();
					this.fireTrail.transform.position = this.footPosition;
					this.fireTrail.owner = base.gameObject;
					this.fireTrail.radius *= this.radius;
				}
				else
				{
					UnityEngine.Object.Destroy(this.fireTrail.gameObject);
					this.fireTrail = null;
				}
			}
			if (this.fireTrail)
			{
				this.fireTrail.damagePerSecond = this.damage * 1.5f;
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0007E484 File Offset: 0x0007C684
		private void UpdateAffixPoison(float deltaTime)
		{
			if (this.HasBuff(RoR2Content.Buffs.AffixPoison))
			{
				this.poisonballTimer += deltaTime;
				if (this.poisonballTimer >= 6f)
				{
					int num = 3 + (int)this.radius;
					this.poisonballTimer = 0f;
					Vector3 up = Vector3.up;
					float num2 = 360f / (float)num;
					Vector3 normalized = Vector3.ProjectOnPlane(this.transform.forward, up).normalized;
					Vector3 point = Vector3.RotateTowards(up, normalized, 0.43633232f, float.PositiveInfinity);
					for (int i = 0; i < num; i++)
					{
						Vector3 forward = Quaternion.AngleAxis(num2 * (float)i, up) * point;
						ProjectileManager.instance.FireProjectile(LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/PoisonOrbProjectile"), this.corePosition, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damage * 1f, 0f, Util.CheckRoll(this.crit, this.master), DamageColorIndex.Default, null, -1f);
					}
				}
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0007E588 File Offset: 0x0007C788
		private void UpdateAffixLunar(float deltaTime)
		{
			if (!this.outOfCombat && this.HasBuff(RoR2Content.Buffs.AffixLunar))
			{
				this.lunarMissileRechargeTimer += deltaTime;
				this.lunarMissileTimerBetweenShots += deltaTime;
				int num = 4;
				if (!this.lunarMissilePrefab)
				{
					this.lunarMissilePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/LunarMissileProjectile");
				}
				if (this.lunarMissileRechargeTimer >= 10f)
				{
					this.lunarMissileRechargeTimer = 0f;
					this.remainingMissilesToFire += num;
				}
				if (this.remainingMissilesToFire > 0 && this.lunarMissileTimerBetweenShots > 0.1f)
				{
					this.lunarMissileTimerBetweenShots = 0f;
					Vector3 vector = this.inputBank ? this.inputBank.aimDirection : this.transform.forward;
					float num2 = 180f / (float)num;
					float d = 3f + (float)((int)this.radius) * 1f;
					float damage = this.damage * 0.3f;
					Quaternion rotation = Util.QuaternionSafeLookRotation(vector);
					Vector3 b = Quaternion.AngleAxis((float)(this.remainingMissilesToFire - 1) * num2 - num2 * (float)(num - 1) / 2f, vector) * Vector3.up * d;
					Vector3 position = this.aimOrigin + b;
					FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
					{
						projectilePrefab = this.lunarMissilePrefab,
						position = position,
						rotation = rotation,
						owner = base.gameObject,
						damage = damage,
						crit = Util.CheckRoll(this.crit, this.master),
						force = 200f
					};
					ProjectileManager.instance.FireProjectile(fireProjectileInfo);
					this.remainingMissilesToFire--;
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0007E750 File Offset: 0x0007C950
		public float bestFitRadius
		{
			get
			{
				return Mathf.Max(this.radius, this.characterMotor ? this.characterMotor.capsuleHeight : 1f);
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0007E77C File Offset: 0x0007C97C
		private void UpdateAllTemporaryVisualEffects()
		{
			int buffCount = this.GetBuffCount(RoR2Content.Buffs.NullifyStack);
			this.UpdateSingleTemporaryVisualEffect(ref this.engiShieldTempEffectInstance, CharacterBody.AssetReferences.engiShieldTempEffectPrefab, this.bestFitRadius, this.healthComponent.shield > 0f && this.HasBuff(RoR2Content.Buffs.EngiShield), "");
			GameObject bucklerShieldTempEffectPrefab = CharacterBody.AssetReferences.bucklerShieldTempEffectPrefab;
			float radius = this.radius;
			bool active;
			if (this.isSprinting)
			{
				Inventory inventory = this.inventory;
				active = (inventory != null && inventory.GetItemCount(RoR2Content.Items.SprintArmor) > 0);
			}
			else
			{
				active = false;
			}
			this.UpdateSingleTemporaryVisualEffect(ref this.bucklerShieldTempEffectInstance, bucklerShieldTempEffectPrefab, radius, active, "");
			this.UpdateSingleTemporaryVisualEffect(ref this.slowDownTimeTempEffectInstance, CharacterBody.AssetReferences.slowDownTimeTempEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Slow60), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.crippleEffectInstance, CharacterBody.AssetReferences.crippleEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Cripple), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.tonicBuffEffectInstance, CharacterBody.AssetReferences.tonicBuffEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.TonicBuff), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.weakTempEffectInstance, CharacterBody.AssetReferences.weakTempEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Weak), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.energizedTempEffectInstance, CharacterBody.AssetReferences.energizedTempEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Energized), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.barrierTempEffectInstance, CharacterBody.AssetReferences.barrierTempEffectPrefab, this.bestFitRadius, this.healthComponent.barrier > 0f, "");
			this.UpdateSingleTemporaryVisualEffect(ref this.regenBoostEffectInstance, CharacterBody.AssetReferences.regenBoostEffectPrefab, this.bestFitRadius, this.HasBuff(JunkContent.Buffs.MeatRegenBoost), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.elephantDefenseEffectInstance, CharacterBody.AssetReferences.elephantDefenseEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.ElephantArmorBoost), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.healingDisabledEffectInstance, CharacterBody.AssetReferences.healingDisabledEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.HealingDisabled), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.noCooldownEffectInstance, CharacterBody.AssetReferences.noCooldownEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.NoCooldowns), "Head");
			GameObject doppelgangerEffectPrefab = CharacterBody.AssetReferences.doppelgangerEffectPrefab;
			float radius2 = this.radius;
			Inventory inventory2 = this.inventory;
			this.UpdateSingleTemporaryVisualEffect(ref this.doppelgangerEffectInstance, doppelgangerEffectPrefab, radius2, inventory2 != null && inventory2.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0, "Head");
			this.UpdateSingleTemporaryVisualEffect(ref this.nullifyStack1EffectInstance, CharacterBody.AssetReferences.nullifyStack1EffectPrefab, this.radius, buffCount == 1, "");
			this.UpdateSingleTemporaryVisualEffect(ref this.nullifyStack2EffectInstance, CharacterBody.AssetReferences.nullifyStack2EffectPrefab, this.radius, buffCount == 2, "");
			this.UpdateSingleTemporaryVisualEffect(ref this.nullifyStack3EffectInstance, CharacterBody.AssetReferences.nullifyStack3EffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Nullified), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.deathmarkEffectInstance, CharacterBody.AssetReferences.deathmarkEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.DeathMark), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.crocoRegenEffectInstance, CharacterBody.AssetReferences.crocoRegenEffectPrefab, this.bestFitRadius, this.HasBuff(RoR2Content.Buffs.CrocoRegen), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.mercExposeEffectInstance, CharacterBody.AssetReferences.mercExposeEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.MercExpose), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.lifestealOnHitEffectInstance, CharacterBody.AssetReferences.lifestealOnHitEffectPrefab, this.bestFitRadius, this.HasBuff(RoR2Content.Buffs.LifeSteal), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.teamWarCryEffectInstance, CharacterBody.AssetReferences.teamWarCryEffectPrefab, this.bestFitRadius, this.HasBuff(RoR2Content.Buffs.TeamWarCry), "HeadCenter");
			this.UpdateSingleTemporaryVisualEffect(ref this.lunarGolemShieldEffectInstance, CharacterBody.AssetReferences.lunarGolemShieldEffectPrefab, this.bestFitRadius, this.HasBuff(RoR2Content.Buffs.LunarShell), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.randomDamageEffectInstance, CharacterBody.AssetReferences.randomDamageEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.PowerBuff), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.warbannerEffectInstance, CharacterBody.AssetReferences.warbannerEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Warbanner), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.teslaFieldEffectInstance, CharacterBody.AssetReferences.teslaFieldEffectPrefab, this.bestFitRadius, this.HasBuff(RoR2Content.Buffs.TeslaField), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.lunarSecondaryRootEffectInstance, CharacterBody.AssetReferences.lunarSecondaryRootEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.LunarSecondaryRoot), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.lunarDetonatorEffectInstance, CharacterBody.AssetReferences.lunarDetonatorEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.LunarDetonationCharge), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.fruitingEffectInstance, CharacterBody.AssetReferences.fruitingEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.Fruiting), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.mushroomVoidTempEffectInstance, CharacterBody.AssetReferences.mushroomVoidTempEffectPrefab, this.radius, this.HasBuff(DLC1Content.Buffs.MushroomVoidActive), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.bearVoidTempEffectInstance, CharacterBody.AssetReferences.bearVoidTempEffectPrefab, this.radius, this.HasBuff(DLC1Content.Buffs.BearVoidReady), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.outOfCombatArmorEffectInstance, CharacterBody.AssetReferences.outOfCombatArmorEffectPrefab, this.radius, this.HasBuff(DLC1Content.Buffs.OutOfCombatArmorBuff), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.voidFogMildEffectInstance, CharacterBody.AssetReferences.voidFogMildEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.VoidFogMild), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.voidFogStrongEffectInstance, CharacterBody.AssetReferences.voidFogStrongEffectPrefab, this.radius, this.HasBuff(RoR2Content.Buffs.VoidFogStrong), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.voidRaidcrabWardWipeFogEffectInstance, CharacterBody.AssetReferences.voidRaidcrabWardWipeFogEffectPrefab, this.radius, this.HasBuff(DLC1Content.Buffs.VoidRaidCrabWardWipeFog), "");
			this.UpdateSingleTemporaryVisualEffect(ref this.voidJailerSlowEffectInstance, CharacterBody.AssetReferences.voidJailerSlowEffectPrefab, this.radius, this.HasBuff(DLC1Content.Buffs.JailerSlow), "");
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0007ED24 File Offset: 0x0007CF24
		private void UpdateSingleTemporaryVisualEffect(ref TemporaryVisualEffect tempEffect, string resourceString, float effectRadius, bool active, string childLocatorOverride = "")
		{
			bool flag = tempEffect != null;
			if (flag != active)
			{
				if (active)
				{
					if (!flag)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>(resourceString), this.corePosition, Quaternion.identity);
						tempEffect = gameObject.GetComponent<TemporaryVisualEffect>();
						tempEffect.parentTransform = this.coreTransform;
						tempEffect.visualState = TemporaryVisualEffect.VisualState.Enter;
						tempEffect.healthComponent = this.healthComponent;
						tempEffect.radius = effectRadius;
						LocalCameraEffect component = gameObject.GetComponent<LocalCameraEffect>();
						if (component)
						{
							component.targetCharacter = base.gameObject;
						}
						if (!string.IsNullOrEmpty(childLocatorOverride))
						{
							ModelLocator modelLocator = this.modelLocator;
							ChildLocator childLocator;
							if (modelLocator == null)
							{
								childLocator = null;
							}
							else
							{
								Transform modelTransform = modelLocator.modelTransform;
								childLocator = ((modelTransform != null) ? modelTransform.GetComponent<ChildLocator>() : null);
							}
							ChildLocator childLocator2 = childLocator;
							if (childLocator2)
							{
								Transform transform = childLocator2.FindChild(childLocatorOverride);
								if (transform)
								{
									tempEffect.parentTransform = transform;
									return;
								}
							}
						}
					}
				}
				else if (tempEffect)
				{
					tempEffect.visualState = TemporaryVisualEffect.VisualState.Exit;
				}
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0007EE14 File Offset: 0x0007D014
		private void UpdateSingleTemporaryVisualEffect(ref TemporaryVisualEffect tempEffect, GameObject tempEffectPrefab, float effectRadius, bool active, string childLocatorOverride = "")
		{
			bool flag = tempEffect != null;
			if (flag != active)
			{
				if (active)
				{
					if (!flag)
					{
						if (!tempEffectPrefab)
						{
							Debug.LogError("Can't instantiate null temporary visual effect");
							return;
						}
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(tempEffectPrefab, this.corePosition, Quaternion.identity);
						tempEffect = gameObject.GetComponent<TemporaryVisualEffect>();
						tempEffect.parentTransform = this.coreTransform;
						tempEffect.visualState = TemporaryVisualEffect.VisualState.Enter;
						tempEffect.healthComponent = this.healthComponent;
						tempEffect.radius = effectRadius;
						LocalCameraEffect component = gameObject.GetComponent<LocalCameraEffect>();
						if (component)
						{
							component.targetCharacter = base.gameObject;
						}
						if (!string.IsNullOrEmpty(childLocatorOverride))
						{
							ModelLocator modelLocator = this.modelLocator;
							ChildLocator childLocator;
							if (modelLocator == null)
							{
								childLocator = null;
							}
							else
							{
								Transform modelTransform = modelLocator.modelTransform;
								childLocator = ((modelTransform != null) ? modelTransform.GetComponent<ChildLocator>() : null);
							}
							ChildLocator childLocator2 = childLocator;
							if (childLocator2)
							{
								Transform transform = childLocator2.FindChild(childLocatorOverride);
								if (transform)
								{
									tempEffect.parentTransform = transform;
									return;
								}
							}
						}
					}
				}
				else if (tempEffect)
				{
					tempEffect.visualState = TemporaryVisualEffect.VisualState.Exit;
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0007EF12 File Offset: 0x0007D112
		public bool hasCloakBuff
		{
			get
			{
				return this.HasBuff(RoR2Content.Buffs.Cloak) || this.HasBuff(RoR2Content.Buffs.AffixHauntedRecipient);
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0007EF2E File Offset: 0x0007D12E
		public VisibilityLevel GetVisibilityLevel(CharacterBody observer)
		{
			return this.GetVisibilityLevel(observer ? observer.teamComponent.teamIndex : TeamIndex.None);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0007EF4C File Offset: 0x0007D14C
		public VisibilityLevel GetVisibilityLevel(TeamIndex observerTeam)
		{
			if (!this.hasCloakBuff)
			{
				return VisibilityLevel.Visible;
			}
			if (this.teamComponent.teamIndex != observerTeam)
			{
				return VisibilityLevel.Cloaked;
			}
			return VisibilityLevel.Revealed;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0007EF69 File Offset: 0x0007D169
		public void AddSpreadBloom(float value)
		{
			this.spreadBloomInternal = Mathf.Min(this.spreadBloomInternal + value, 1f);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0007EF83 File Offset: 0x0007D183
		public void SetSpreadBloom(float value, bool canOnlyIncreaseBloom = true)
		{
			if (canOnlyIncreaseBloom)
			{
				this.spreadBloomInternal = Mathf.Clamp(value, this.spreadBloomInternal, 1f);
				return;
			}
			this.spreadBloomInternal = Mathf.Min(value, 1f);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0007EFB4 File Offset: 0x0007D1B4
		private void UpdateSpreadBloom(float dt)
		{
			float num = 1f / this.spreadBloomDecayTime;
			this.spreadBloomInternal = Mathf.Max(this.spreadBloomInternal - num * dt, 0f);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0007EFE8 File Offset: 0x0007D1E8
		public float spreadBloomAngle
		{
			get
			{
				return this.spreadBloomCurve.Evaluate(this.spreadBloomInternal);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0007EFFB File Offset: 0x0007D1FB
		public GameObject defaultCrosshairPrefab
		{
			get
			{
				return this._defaultCrosshairPrefab;
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0007F004 File Offset: 0x0007D204
		[Client]
		public void SendConstructTurret(CharacterBody builder, Vector3 position, Quaternion rotation, MasterCatalog.MasterIndex masterIndex)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.CharacterBody::SendConstructTurret(RoR2.CharacterBody,UnityEngine.Vector3,UnityEngine.Quaternion,RoR2.MasterCatalog/MasterIndex)' called on server");
				return;
			}
			CharacterBody.ConstructTurretMessage msg = new CharacterBody.ConstructTurretMessage
			{
				builder = builder.gameObject,
				position = position,
				rotation = rotation,
				turretMasterIndex = masterIndex
			};
			ClientScene.readyConnection.Send(62, msg);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0007F064 File Offset: 0x0007D264
		[NetworkMessageHandler(msgType = 62, server = true)]
		private static void HandleConstructTurret(NetworkMessage netMsg)
		{
			CharacterBody.ConstructTurretMessage constructTurretMessage = netMsg.ReadMessage<CharacterBody.ConstructTurretMessage>();
			if (constructTurretMessage.builder)
			{
				CharacterBody component = constructTurretMessage.builder.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterMaster master = component.master;
					if (master)
					{
						CharacterMaster characterMaster = new MasterSummon
						{
							masterPrefab = MasterCatalog.GetMasterPrefab(constructTurretMessage.turretMasterIndex),
							position = constructTurretMessage.position,
							rotation = constructTurretMessage.rotation,
							summonerBodyObject = component.gameObject,
							ignoreTeamMemberLimit = true,
							inventoryToCopy = master.inventory
						}.Perform();
						Deployable deployable = characterMaster.gameObject.AddComponent<Deployable>();
						deployable.onUndeploy = new UnityEvent();
						deployable.onUndeploy.AddListener(new UnityAction(characterMaster.TrueKill));
						master.AddDeployable(deployable, DeployableSlot.EngiTurret);
					}
				}
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0007F143 File Offset: 0x0007D343
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x0007F14B File Offset: 0x0007D34B
		public int multiKillCount { get; private set; }

		// Token: 0x06001D9C RID: 7580 RVA: 0x0007F154 File Offset: 0x0007D354
		[Server]
		public void AddMultiKill(int kills)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::AddMultiKill(System.Int32)' called on client");
				return;
			}
			this.multiKillTimer = 1f;
			this.multiKillCount += kills;
			int num = this.inventory ? this.inventory.GetItemCount(RoR2Content.Items.WarCryOnMultiKill) : 0;
			if (num > 0 && this.multiKillCount >= 4)
			{
				this.AddTimedBuff(RoR2Content.Buffs.WarCryBuff, 2f + 4f * (float)num);
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0007F1D8 File Offset: 0x0007D3D8
		[Server]
		private void UpdateMultiKill(float deltaTime)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::UpdateMultiKill(System.Single)' called on client");
				return;
			}
			this.multiKillTimer -= deltaTime;
			if (this.multiKillTimer <= 0f)
			{
				this.multiKillTimer = 0f;
				this.multiKillCount = 0;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0007F227 File Offset: 0x0007D427
		public Vector3 corePosition
		{
			get
			{
				if (!this.coreTransform)
				{
					return this.transform.position;
				}
				return this.coreTransform.position;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0007F250 File Offset: 0x0007D450
		public Vector3 footPosition
		{
			get
			{
				Vector3 position = this.transform.position;
				if (this.characterMotor)
				{
					position.y -= this.characterMotor.capsuleHeight * 0.5f;
				}
				return position;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0007F293 File Offset: 0x0007D493
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0007F29B File Offset: 0x0007D49B
		public float radius { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0007F2A4 File Offset: 0x0007D4A4
		public Vector3 aimOrigin
		{
			get
			{
				if (!this.aimOriginTransform)
				{
					return this.corePosition;
				}
				return this.aimOriginTransform.position;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0007F2C5 File Offset: 0x0007D4C5
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0007F2CD File Offset: 0x0007D4CD
		public bool isElite { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0007F2D6 File Offset: 0x0007D4D6
		public bool isBoss
		{
			get
			{
				return this.master && this.master.isBoss;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0007F2F2 File Offset: 0x0007D4F2
		public bool isFlying
		{
			get
			{
				return !this.characterMotor || this.characterMotor.isFlying;
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0007F30E File Offset: 0x0007D50E
		[ClientRpc]
		public void RpcBark()
		{
			if (this.sfxLocator)
			{
				Util.PlaySound(this.sfxLocator.barkSound, base.gameObject);
			}
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0007F334 File Offset: 0x0007D534
		[Command]
		public void CmdRequestVehicleEjection()
		{
			if (this.currentVehicle)
			{
				this.currentVehicle.EjectPassenger(base.gameObject);
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0007F354 File Offset: 0x0007D554
		public bool RollCrit()
		{
			return this.master && Util.CheckRoll(this.crit, this.master);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0007F376 File Offset: 0x0007D576
		[ClientRpc]
		private void RpcUsePreferredInitialStateType()
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetBodyStateToPreferredInitialState();
			}
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0007F388 File Offset: 0x0007D588
		public void SetBodyStateToPreferredInitialState()
		{
			if (!this.hasEffectiveAuthority)
			{
				if (NetworkServer.active)
				{
					this.CallRpcUsePreferredInitialStateType();
				}
				return;
			}
			Type stateType = this.preferredInitialStateType.stateType;
			if (stateType == null || stateType == typeof(Uninitialized))
			{
				return;
			}
			EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Body");
			if (entityStateMachine == null)
			{
				return;
			}
			entityStateMachine.SetState(EntityStateCatalog.InstantiateState(stateType));
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0007F3F3 File Offset: 0x0007D5F3
		[Server]
		public void SetLoadoutServer(Loadout loadout)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.CharacterBody::SetLoadoutServer(RoR2.Loadout)' called on client");
				return;
			}
			this.skillLocator.ApplyLoadoutServer(loadout, this.bodyIndex);
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0007F41C File Offset: 0x0007D61C
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x0007F424 File Offset: 0x0007D624
		public Run.FixedTimeStamp localStartTime { get; private set; } = Run.FixedTimeStamp.positiveInfinity;

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0007F42D File Offset: 0x0007D62D
		public bool isEquipmentActivationAllowed
		{
			get
			{
				return !this.currentVehicle || this.currentVehicle.isEquipmentActivationAllowed;
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		static CharacterBody()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterBody), CharacterBody.kCmdCmdAddTimedBuff, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeCmdCmdAddTimedBuff));
			CharacterBody.kCmdCmdUpdateSprint = -1006016914;
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterBody), CharacterBody.kCmdCmdUpdateSprint, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeCmdCmdUpdateSprint));
			CharacterBody.kCmdCmdOnSkillActivated = 384138986;
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterBody), CharacterBody.kCmdCmdOnSkillActivated, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeCmdCmdOnSkillActivated));
			CharacterBody.kCmdCmdRequestVehicleEjection = 1803737791;
			NetworkBehaviour.RegisterCommandDelegate(typeof(CharacterBody), CharacterBody.kCmdCmdRequestVehicleEjection, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeCmdCmdRequestVehicleEjection));
			CharacterBody.kRpcRpcBark = -76716871;
			NetworkBehaviour.RegisterRpcDelegate(typeof(CharacterBody), CharacterBody.kRpcRpcBark, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeRpcRpcBark));
			CharacterBody.kRpcRpcUsePreferredInitialStateType = 638695010;
			NetworkBehaviour.RegisterRpcDelegate(typeof(CharacterBody), CharacterBody.kRpcRpcUsePreferredInitialStateType, new NetworkBehaviour.CmdDelegate(CharacterBody.InvokeRpcRpcUsePreferredInitialStateType));
			NetworkCRC.RegisterBehaviour("CharacterBody", 0);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0007F618 File Offset: 0x0007D818
		[CompilerGenerated]
		private void <ReadBuffs>g__ZeroBuffIndexRange|17_0(BuffIndex start, BuffIndex end, ref CharacterBody.<>c__DisplayClass17_0 A_3)
		{
			while (A_3.activeBuffsIndexToCheck < this.activeBuffsListCount)
			{
				BuffIndex buffIndex = this.activeBuffsList[A_3.activeBuffsIndexToCheck];
				if (end <= buffIndex)
				{
					return;
				}
				int activeBuffsIndexToCheck;
				if (start <= buffIndex)
				{
					this.SetBuffCount(buffIndex, 0);
					activeBuffsIndexToCheck = A_3.activeBuffsIndexToCheck - 1;
					A_3.activeBuffsIndexToCheck = activeBuffsIndexToCheck;
				}
				activeBuffsIndexToCheck = A_3.activeBuffsIndexToCheck + 1;
				A_3.activeBuffsIndexToCheck = activeBuffsIndexToCheck;
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0007F674 File Offset: 0x0007D874
		[CompilerGenerated]
		private void <AddTimedBuff>g__DefaultBehavior|32_0(ref CharacterBody.<>c__DisplayClass32_0 A_1)
		{
			bool flag = false;
			if (!A_1.buffDef.canStack)
			{
				for (int i = 0; i < this.timedBuffs.Count; i++)
				{
					if (this.timedBuffs[i].buffIndex == A_1.buffType)
					{
						flag = true;
						this.timedBuffs[i].timer = Mathf.Max(this.timedBuffs[i].timer, A_1.duration);
						break;
					}
				}
			}
			if (!flag)
			{
				this.timedBuffs.Add(new CharacterBody.TimedBuff
				{
					buffIndex = A_1.buffType,
					timer = A_1.duration
				});
				this.AddBuff(A_1.buffType);
			}
			if (A_1.buffDef.startSfx)
			{
				EntitySoundManager.EmitSoundServer(A_1.buffDef.startSfx.index, this.networkIdentity);
			}
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0007F754 File Offset: 0x0007D954
		[CompilerGenerated]
		private int <AddTimedBuff>g__RefreshStacks|32_1(ref CharacterBody.<>c__DisplayClass32_0 A_1)
		{
			int num = 0;
			for (int i = 0; i < this.timedBuffs.Count; i++)
			{
				CharacterBody.TimedBuff timedBuff = this.timedBuffs[i];
				if (timedBuff.buffIndex == A_1.buffType)
				{
					num++;
					if (timedBuff.timer < A_1.duration)
					{
						timedBuff.timer = A_1.duration;
					}
				}
			}
			return num;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0007F7B3 File Offset: 0x0007D9B3
		protected static void InvokeCmdCmdAddTimedBuff(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdAddTimedBuff called on client.");
				return;
			}
			((CharacterBody)obj).CmdAddTimedBuff((BuffIndex)reader.ReadInt32(), reader.ReadSingle());
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0007F7E3 File Offset: 0x0007D9E3
		protected static void InvokeCmdCmdUpdateSprint(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdUpdateSprint called on client.");
				return;
			}
			((CharacterBody)obj).CmdUpdateSprint(reader.ReadBoolean());
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0007F80C File Offset: 0x0007DA0C
		protected static void InvokeCmdCmdOnSkillActivated(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdOnSkillActivated called on client.");
				return;
			}
			((CharacterBody)obj).CmdOnSkillActivated((sbyte)reader.ReadPackedUInt32());
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0007F835 File Offset: 0x0007DA35
		protected static void InvokeCmdCmdRequestVehicleEjection(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdRequestVehicleEjection called on client.");
				return;
			}
			((CharacterBody)obj).CmdRequestVehicleEjection();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0007F858 File Offset: 0x0007DA58
		public void CallCmdAddTimedBuff(BuffIndex buffType, float duration)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdAddTimedBuff called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdAddTimedBuff(buffType, duration);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kCmdCmdAddTimedBuff);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write((int)buffType);
			networkWriter.Write(duration);
			base.SendCommandInternal(networkWriter, 0, "CmdAddTimedBuff");
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0007F8F0 File Offset: 0x0007DAF0
		public void CallCmdUpdateSprint(bool newIsSprinting)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdUpdateSprint called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdUpdateSprint(newIsSprinting);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kCmdCmdUpdateSprint);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(newIsSprinting);
			base.SendCommandInternal(networkWriter, 0, "CmdUpdateSprint");
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0007F97C File Offset: 0x0007DB7C
		public void CallCmdOnSkillActivated(sbyte skillIndex)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdOnSkillActivated called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdOnSkillActivated(skillIndex);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kCmdCmdOnSkillActivated);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)skillIndex);
			base.SendCommandInternal(networkWriter, 0, "CmdOnSkillActivated");
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0007FA08 File Offset: 0x0007DC08
		public void CallCmdRequestVehicleEjection()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdRequestVehicleEjection called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdRequestVehicleEjection();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kCmdCmdRequestVehicleEjection);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdRequestVehicleEjection");
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0007FA84 File Offset: 0x0007DC84
		protected static void InvokeRpcRpcBark(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcBark called on server.");
				return;
			}
			((CharacterBody)obj).RpcBark();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0007FAA7 File Offset: 0x0007DCA7
		protected static void InvokeRpcRpcUsePreferredInitialStateType(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcUsePreferredInitialStateType called on server.");
				return;
			}
			((CharacterBody)obj).RpcUsePreferredInitialStateType();
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0007FACC File Offset: 0x0007DCCC
		public void CallRpcBark()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcBark called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kRpcRpcBark);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcBark");
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0007FB38 File Offset: 0x0007DD38
		public void CallRpcUsePreferredInitialStateType()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcUsePreferredInitialStateType called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)CharacterBody.kRpcRpcUsePreferredInitialStateType);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcUsePreferredInitialStateType");
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040022A5 RID: 8869
		[HideInInspector]
		[Tooltip("This is assigned to the prefab automatically by BodyCatalog at runtime. Do not set this value manually.")]
		public BodyIndex bodyIndex = BodyIndex.None;

		// Token: 0x040022A6 RID: 8870
		[Tooltip("The language token to use as the base name of this character.")]
		public string baseNameToken;

		// Token: 0x040022A7 RID: 8871
		public string subtitleNameToken;

		// Token: 0x040022A8 RID: 8872
		private BuffIndex[] activeBuffsList;

		// Token: 0x040022A9 RID: 8873
		private int activeBuffsListCount;

		// Token: 0x040022AA RID: 8874
		private int[] buffs;

		// Token: 0x040022AB RID: 8875
		private int eliteBuffCount;

		// Token: 0x040022AC RID: 8876
		private List<CharacterBody.TimedBuff> timedBuffs = new List<CharacterBody.TimedBuff>();

		// Token: 0x040022AD RID: 8877
		[NonSerialized]
		public int pendingTonicAfflictionCount;

		// Token: 0x040022AE RID: 8878
		private GameObject warCryEffectInstance;

		// Token: 0x040022AF RID: 8879
		[EnumMask(typeof(CharacterBody.BodyFlags))]
		public CharacterBody.BodyFlags bodyFlags;

		// Token: 0x040022B0 RID: 8880
		private NetworkInstanceId masterObjectId;

		// Token: 0x040022B1 RID: 8881
		private GameObject _masterObject;

		// Token: 0x040022B2 RID: 8882
		private CharacterMaster _master;

		// Token: 0x040022B4 RID: 8884
		private bool linkedToMaster;

		// Token: 0x040022B6 RID: 8886
		private bool disablingHurtBoxes;

		// Token: 0x040022B7 RID: 8887
		private EquipmentIndex previousEquipmentIndex = EquipmentIndex.None;

		// Token: 0x040022BA RID: 8890
		private new Transform transform;

		// Token: 0x040022C4 RID: 8900
		private SfxLocator sfxLocator;

		// Token: 0x040022CB RID: 8907
		private static List<CharacterBody> instancesList = new List<CharacterBody>();

		// Token: 0x040022CC RID: 8908
		public static readonly ReadOnlyCollection<CharacterBody> readOnlyInstancesList = new ReadOnlyCollection<CharacterBody>(CharacterBody.instancesList);

		// Token: 0x040022D2 RID: 8914
		private bool _isSprinting;

		// Token: 0x040022D3 RID: 8915
		private const float outOfCombatDelay = 5f;

		// Token: 0x040022D4 RID: 8916
		private const float outOfDangerDelay = 7f;

		// Token: 0x040022D5 RID: 8917
		private float outOfCombatStopwatch;

		// Token: 0x040022D6 RID: 8918
		private float outOfDangerStopwatch;

		// Token: 0x040022D8 RID: 8920
		private bool _outOfDanger = true;

		// Token: 0x040022D9 RID: 8921
		private Vector3 previousPosition;

		// Token: 0x040022DA RID: 8922
		private const float notMovingWait = 1f;

		// Token: 0x040022DB RID: 8923
		private float notMovingStopwatch;

		// Token: 0x040022DC RID: 8924
		public bool rootMotionInMainState;

		// Token: 0x040022DD RID: 8925
		public float mainRootSpeed;

		// Token: 0x040022DE RID: 8926
		public float baseMaxHealth;

		// Token: 0x040022DF RID: 8927
		public float baseRegen;

		// Token: 0x040022E0 RID: 8928
		public float baseMaxShield;

		// Token: 0x040022E1 RID: 8929
		public float baseMoveSpeed;

		// Token: 0x040022E2 RID: 8930
		public float baseAcceleration;

		// Token: 0x040022E3 RID: 8931
		public float baseJumpPower;

		// Token: 0x040022E4 RID: 8932
		public float baseDamage;

		// Token: 0x040022E5 RID: 8933
		public float baseAttackSpeed;

		// Token: 0x040022E6 RID: 8934
		public float baseCrit;

		// Token: 0x040022E7 RID: 8935
		public float baseArmor;

		// Token: 0x040022E8 RID: 8936
		public float baseVisionDistance = float.PositiveInfinity;

		// Token: 0x040022E9 RID: 8937
		public int baseJumpCount = 1;

		// Token: 0x040022EA RID: 8938
		public float sprintingSpeedMultiplier = 1.45f;

		// Token: 0x040022EB RID: 8939
		public bool autoCalculateLevelStats;

		// Token: 0x040022EC RID: 8940
		public float levelMaxHealth;

		// Token: 0x040022ED RID: 8941
		public float levelRegen;

		// Token: 0x040022EE RID: 8942
		public float levelMaxShield;

		// Token: 0x040022EF RID: 8943
		public float levelMoveSpeed;

		// Token: 0x040022F0 RID: 8944
		public float levelJumpPower;

		// Token: 0x040022F1 RID: 8945
		public float levelDamage;

		// Token: 0x040022F2 RID: 8946
		public float levelAttackSpeed;

		// Token: 0x040022F3 RID: 8947
		public float levelCrit;

		// Token: 0x040022F4 RID: 8948
		public float levelArmor;

		// Token: 0x0400230F RID: 8975
		private bool statsDirty;

		// Token: 0x04002310 RID: 8976
		private float aimTimer;

		// Token: 0x04002311 RID: 8977
		private const uint masterDirtyBit = 1U;

		// Token: 0x04002312 RID: 8978
		private const uint buffsDirtyBit = 2U;

		// Token: 0x04002313 RID: 8979
		private const uint outOfCombatBit = 4U;

		// Token: 0x04002314 RID: 8980
		private const uint outOfDangerBit = 8U;

		// Token: 0x04002315 RID: 8981
		private const uint sprintingBit = 16U;

		// Token: 0x04002316 RID: 8982
		private const uint allDirtyBits = 31U;

		// Token: 0x04002318 RID: 8984
		private HelfireController helfireController;

		// Token: 0x04002319 RID: 8985
		private float helfireLifetime;

		// Token: 0x0400231A RID: 8986
		private DamageTrail fireTrail;

		// Token: 0x0400231B RID: 8987
		public bool wasLucky;

		// Token: 0x0400231C RID: 8988
		private const float poisonballAngle = 25f;

		// Token: 0x0400231D RID: 8989
		private const float poisonballDamageCoefficient = 1f;

		// Token: 0x0400231E RID: 8990
		private const float poisonballRefreshTime = 6f;

		// Token: 0x0400231F RID: 8991
		private float poisonballTimer;

		// Token: 0x04002320 RID: 8992
		private const float lunarMissileDamageCoefficient = 0.3f;

		// Token: 0x04002321 RID: 8993
		private const float lunarMissileRefreshTime = 10f;

		// Token: 0x04002322 RID: 8994
		private const float lunarMissileDelayBetweenShots = 0.1f;

		// Token: 0x04002323 RID: 8995
		private float lunarMissileRechargeTimer = 10f;

		// Token: 0x04002324 RID: 8996
		private float lunarMissileTimerBetweenShots;

		// Token: 0x04002325 RID: 8997
		private int remainingMissilesToFire;

		// Token: 0x04002326 RID: 8998
		private GameObject lunarMissilePrefab;

		// Token: 0x04002327 RID: 8999
		private GameObject timeBubbleWardInstance;

		// Token: 0x04002328 RID: 9000
		private TemporaryVisualEffect engiShieldTempEffectInstance;

		// Token: 0x04002329 RID: 9001
		private TemporaryVisualEffect bucklerShieldTempEffectInstance;

		// Token: 0x0400232A RID: 9002
		private TemporaryVisualEffect slowDownTimeTempEffectInstance;

		// Token: 0x0400232B RID: 9003
		private TemporaryVisualEffect crippleEffectInstance;

		// Token: 0x0400232C RID: 9004
		private TemporaryVisualEffect tonicBuffEffectInstance;

		// Token: 0x0400232D RID: 9005
		private TemporaryVisualEffect weakTempEffectInstance;

		// Token: 0x0400232E RID: 9006
		private TemporaryVisualEffect energizedTempEffectInstance;

		// Token: 0x0400232F RID: 9007
		private TemporaryVisualEffect barrierTempEffectInstance;

		// Token: 0x04002330 RID: 9008
		private TemporaryVisualEffect nullifyStack1EffectInstance;

		// Token: 0x04002331 RID: 9009
		private TemporaryVisualEffect nullifyStack2EffectInstance;

		// Token: 0x04002332 RID: 9010
		private TemporaryVisualEffect nullifyStack3EffectInstance;

		// Token: 0x04002333 RID: 9011
		private TemporaryVisualEffect regenBoostEffectInstance;

		// Token: 0x04002334 RID: 9012
		private TemporaryVisualEffect elephantDefenseEffectInstance;

		// Token: 0x04002335 RID: 9013
		private TemporaryVisualEffect healingDisabledEffectInstance;

		// Token: 0x04002336 RID: 9014
		private TemporaryVisualEffect noCooldownEffectInstance;

		// Token: 0x04002337 RID: 9015
		private TemporaryVisualEffect doppelgangerEffectInstance;

		// Token: 0x04002338 RID: 9016
		private TemporaryVisualEffect deathmarkEffectInstance;

		// Token: 0x04002339 RID: 9017
		private TemporaryVisualEffect crocoRegenEffectInstance;

		// Token: 0x0400233A RID: 9018
		private TemporaryVisualEffect mercExposeEffectInstance;

		// Token: 0x0400233B RID: 9019
		private TemporaryVisualEffect lifestealOnHitEffectInstance;

		// Token: 0x0400233C RID: 9020
		private TemporaryVisualEffect teamWarCryEffectInstance;

		// Token: 0x0400233D RID: 9021
		private TemporaryVisualEffect randomDamageEffectInstance;

		// Token: 0x0400233E RID: 9022
		private TemporaryVisualEffect lunarGolemShieldEffectInstance;

		// Token: 0x0400233F RID: 9023
		private TemporaryVisualEffect warbannerEffectInstance;

		// Token: 0x04002340 RID: 9024
		private TemporaryVisualEffect teslaFieldEffectInstance;

		// Token: 0x04002341 RID: 9025
		private TemporaryVisualEffect lunarSecondaryRootEffectInstance;

		// Token: 0x04002342 RID: 9026
		private TemporaryVisualEffect lunarDetonatorEffectInstance;

		// Token: 0x04002343 RID: 9027
		private TemporaryVisualEffect fruitingEffectInstance;

		// Token: 0x04002344 RID: 9028
		private TemporaryVisualEffect mushroomVoidTempEffectInstance;

		// Token: 0x04002345 RID: 9029
		private TemporaryVisualEffect bearVoidTempEffectInstance;

		// Token: 0x04002346 RID: 9030
		private TemporaryVisualEffect outOfCombatArmorEffectInstance;

		// Token: 0x04002347 RID: 9031
		private TemporaryVisualEffect voidFogMildEffectInstance;

		// Token: 0x04002348 RID: 9032
		private TemporaryVisualEffect voidFogStrongEffectInstance;

		// Token: 0x04002349 RID: 9033
		private TemporaryVisualEffect voidJailerSlowEffectInstance;

		// Token: 0x0400234A RID: 9034
		private TemporaryVisualEffect voidRaidcrabWardWipeFogEffectInstance;

		// Token: 0x0400234B RID: 9035
		[Tooltip("How long it takes for spread bloom to reset from full.")]
		public float spreadBloomDecayTime = 0.45f;

		// Token: 0x0400234C RID: 9036
		[Tooltip("The spread bloom interpretation curve.")]
		public AnimationCurve spreadBloomCurve;

		// Token: 0x0400234D RID: 9037
		private float spreadBloomInternal;

		// Token: 0x0400234E RID: 9038
		[Tooltip("The crosshair prefab used for this body.")]
		[SerializeField]
		[FormerlySerializedAs("crosshairPrefab")]
		private GameObject _defaultCrosshairPrefab;

		// Token: 0x0400234F RID: 9039
		[HideInInspector]
		public bool hideCrosshair;

		// Token: 0x04002350 RID: 9040
		private const float multiKillMaxInterval = 1f;

		// Token: 0x04002351 RID: 9041
		private float multiKillTimer;

		// Token: 0x04002353 RID: 9043
		private const int multiKillThresholdForWarcry = 4;

		// Token: 0x04002355 RID: 9045
		[Tooltip("The child transform to be used as the aiming origin.")]
		public Transform aimOriginTransform;

		// Token: 0x04002356 RID: 9046
		[Tooltip("The hull size to use when pathfinding for this object.")]
		public HullClassification hullClassification;

		// Token: 0x04002357 RID: 9047
		[Tooltip("The icon displayed for ally healthbars")]
		public Texture portraitIcon;

		// Token: 0x04002358 RID: 9048
		[Tooltip("The main color of the body. Currently only used in the logbook.")]
		public Color bodyColor = Color.clear;

		// Token: 0x04002359 RID: 9049
		[FormerlySerializedAs("isBoss")]
		[Tooltip("Whether or not this is a boss for dropping items on death.")]
		public bool isChampion;

		// Token: 0x0400235B RID: 9051
		public VehicleSeat currentVehicle;

		// Token: 0x0400235C RID: 9052
		[Tooltip("The pod prefab to use for handling this character's first-time spawn animation.")]
		public GameObject preferredPodPrefab;

		// Token: 0x0400235D RID: 9053
		[Tooltip("The preferred state to use for handling the character's first-time spawn animation. Only used with no preferred pod prefab.")]
		public SerializableEntityStateType preferredInitialStateType = new SerializableEntityStateType(typeof(Uninitialized));

		// Token: 0x0400235E RID: 9054
		public uint skinIndex;

		// Token: 0x04002360 RID: 9056
		public string customKillTotalStatName;

		// Token: 0x04002361 RID: 9057
		public Transform overrideCoreTransform;

		// Token: 0x04002362 RID: 9058
		private static int kCmdCmdAddTimedBuff = -160178508;

		// Token: 0x04002363 RID: 9059
		private static int kCmdCmdUpdateSprint;

		// Token: 0x04002364 RID: 9060
		private static int kCmdCmdOnSkillActivated;

		// Token: 0x04002365 RID: 9061
		private static int kRpcRpcBark;

		// Token: 0x04002366 RID: 9062
		private static int kCmdCmdRequestVehicleEjection;

		// Token: 0x04002367 RID: 9063
		private static int kRpcRpcUsePreferredInitialStateType;

		// Token: 0x0200061C RID: 1564
		private static class CommonAssets
		{
			// Token: 0x06001DC3 RID: 7619 RVA: 0x0007FBA4 File Offset: 0x0007DDA4
			public static void Load()
			{
				CharacterBody.CommonAssets.nullifiedBuffAppliedSound = LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseNullifiedBuffApplied");
				CharacterBody.CommonAssets.pulverizeBuildupBuffAppliedSound = LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nsePulverizeBuildupBuffApplied");
				CharacterBody.CommonAssets.procCritAttackSpeedSounds = new NetworkSoundEventDef[]
				{
					LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseProcCritAttackSpeed1"),
					LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseProcCritAttackSpeed2"),
					LegacyResourcesAPI.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseProcCritAttackSpeed3")
				};
				SkillCatalog.skillsDefined.CallWhenAvailable(delegate
				{
					CharacterBody.CommonAssets.lunarUtilityReplacementSkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("LunarUtilityReplacement"));
					CharacterBody.CommonAssets.lunarPrimaryReplacementSkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("LunarPrimaryReplacement"));
					CharacterBody.CommonAssets.lunarSecondaryReplacementSkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("LunarSecondaryReplacement"));
					CharacterBody.CommonAssets.lunarSpecialReplacementSkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("LunarDetonatorSpecialReplacement"));
				});
			}

			// Token: 0x04002368 RID: 9064
			public static SkillDef lunarUtilityReplacementSkillDef;

			// Token: 0x04002369 RID: 9065
			public static SkillDef lunarPrimaryReplacementSkillDef;

			// Token: 0x0400236A RID: 9066
			public static SkillDef lunarSecondaryReplacementSkillDef;

			// Token: 0x0400236B RID: 9067
			public static SkillDef lunarSpecialReplacementSkillDef;

			// Token: 0x0400236C RID: 9068
			public static NetworkSoundEventDef nullifiedBuffAppliedSound;

			// Token: 0x0400236D RID: 9069
			public static NetworkSoundEventDef pulverizeBuildupBuffAppliedSound;

			// Token: 0x0400236E RID: 9070
			public static NetworkSoundEventDef[] procCritAttackSpeedSounds;
		}

		// Token: 0x0200061E RID: 1566
		private class TimedBuff
		{
			// Token: 0x04002371 RID: 9073
			public BuffIndex buffIndex;

			// Token: 0x04002372 RID: 9074
			public float timer;
		}

		// Token: 0x0200061F RID: 1567
		[Flags]
		public enum BodyFlags : uint
		{
			// Token: 0x04002374 RID: 9076
			None = 0U,
			// Token: 0x04002375 RID: 9077
			IgnoreFallDamage = 1U,
			// Token: 0x04002376 RID: 9078
			Mechanical = 2U,
			// Token: 0x04002377 RID: 9079
			Masterless = 4U,
			// Token: 0x04002378 RID: 9080
			ImmuneToGoo = 8U,
			// Token: 0x04002379 RID: 9081
			ImmuneToExecutes = 16U,
			// Token: 0x0400237A RID: 9082
			SprintAnyDirection = 32U,
			// Token: 0x0400237B RID: 9083
			ResistantToAOE = 64U,
			// Token: 0x0400237C RID: 9084
			HasBackstabPassive = 128U,
			// Token: 0x0400237D RID: 9085
			HasBackstabImmunity = 256U,
			// Token: 0x0400237E RID: 9086
			OverheatImmune = 512U,
			// Token: 0x0400237F RID: 9087
			Void = 1024U,
			// Token: 0x04002380 RID: 9088
			ImmuneToVoidDeath = 2048U
		}

		// Token: 0x02000620 RID: 1568
		public class ItemBehavior : MonoBehaviour
		{
			// Token: 0x04002381 RID: 9089
			public CharacterBody body;

			// Token: 0x04002382 RID: 9090
			public int stack;
		}

		// Token: 0x02000621 RID: 1569
		public class AffixHauntedBehavior : CharacterBody.ItemBehavior
		{
			// Token: 0x06001DC9 RID: 7625 RVA: 0x0007FC98 File Offset: 0x0007DE98
			private void FixedUpdate()
			{
				if (!NetworkServer.active)
				{
					return;
				}
				bool flag = this.stack > 0;
				if (this.affixHauntedWard != flag)
				{
					if (flag)
					{
						this.affixHauntedWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/AffixHauntedWard"));
						this.affixHauntedWard.GetComponent<TeamFilter>().teamIndex = this.body.teamComponent.teamIndex;
						this.affixHauntedWard.GetComponent<BuffWard>().Networkradius = 30f + this.body.radius;
						this.affixHauntedWard.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(this.body.gameObject, null);
						return;
					}
					UnityEngine.Object.Destroy(this.affixHauntedWard);
					this.affixHauntedWard = null;
				}
			}

			// Token: 0x06001DCA RID: 7626 RVA: 0x0007FD50 File Offset: 0x0007DF50
			private void OnDisable()
			{
				if (this.affixHauntedWard)
				{
					UnityEngine.Object.Destroy(this.affixHauntedWard);
				}
			}

			// Token: 0x04002383 RID: 9091
			private GameObject affixHauntedWard;
		}

		// Token: 0x02000622 RID: 1570
		public class QuestVolatileBatteryBehaviorServer : CharacterBody.ItemBehavior
		{
			// Token: 0x06001DCC RID: 7628 RVA: 0x0007FD72 File Offset: 0x0007DF72
			private void Start()
			{
				this.attachment = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/QuestVolatileBatteryAttachment")).GetComponent<NetworkedBodyAttachment>();
				this.attachment.AttachToGameObjectAndSpawn(this.body.gameObject, null);
			}

			// Token: 0x06001DCD RID: 7629 RVA: 0x0007FDA5 File Offset: 0x0007DFA5
			private void OnDestroy()
			{
				if (this.attachment)
				{
					UnityEngine.Object.Destroy(this.attachment.gameObject);
					this.attachment = null;
				}
			}

			// Token: 0x04002384 RID: 9092
			private NetworkedBodyAttachment attachment;
		}

		// Token: 0x02000623 RID: 1571
		public class TimeBubbleItemBehaviorServer : CharacterBody.ItemBehavior
		{
			// Token: 0x06001DCF RID: 7631 RVA: 0x0007FDCB File Offset: 0x0007DFCB
			private void OnDestroy()
			{
				if (this.body.timeBubbleWardInstance)
				{
					UnityEngine.Object.Destroy(this.body.timeBubbleWardInstance);
				}
			}
		}

		// Token: 0x02000624 RID: 1572
		public class ElementalRingsBehavior : CharacterBody.ItemBehavior
		{
			// Token: 0x06001DD1 RID: 7633 RVA: 0x0007FDF0 File Offset: 0x0007DFF0
			private void OnDisable()
			{
				if (this.body)
				{
					if (this.body.HasBuff(RoR2Content.Buffs.ElementalRingsReady))
					{
						this.body.RemoveBuff(RoR2Content.Buffs.ElementalRingsReady);
					}
					if (this.body.HasBuff(RoR2Content.Buffs.ElementalRingsCooldown))
					{
						this.body.RemoveBuff(RoR2Content.Buffs.ElementalRingsCooldown);
					}
				}
			}

			// Token: 0x06001DD2 RID: 7634 RVA: 0x0007FE50 File Offset: 0x0007E050
			private void FixedUpdate()
			{
				bool flag = this.body.HasBuff(RoR2Content.Buffs.ElementalRingsCooldown);
				bool flag2 = this.body.HasBuff(RoR2Content.Buffs.ElementalRingsReady);
				if (!flag && !flag2)
				{
					this.body.AddBuff(RoR2Content.Buffs.ElementalRingsReady);
				}
				if (flag2 && flag)
				{
					this.body.RemoveBuff(RoR2Content.Buffs.ElementalRingsReady);
				}
			}
		}

		// Token: 0x02000625 RID: 1573
		public class AffixEchoBehavior : CharacterBody.ItemBehavior
		{
			// Token: 0x06001DD4 RID: 7636 RVA: 0x0007FEAA File Offset: 0x0007E0AA
			private void FixedUpdate()
			{
				this.spawnCard.nodeGraphType = (this.body.isFlying ? MapNodeGroup.GraphType.Air : MapNodeGroup.GraphType.Ground);
			}

			// Token: 0x06001DD5 RID: 7637 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
			private void Awake()
			{
				base.enabled = false;
			}

			// Token: 0x06001DD6 RID: 7638 RVA: 0x0007FED4 File Offset: 0x0007E0D4
			private void OnEnable()
			{
				MasterCatalog.MasterIndex masterIndex = MasterCatalog.FindAiMasterIndexForBody(this.body.bodyIndex);
				this.spawnCard = ScriptableObject.CreateInstance<CharacterSpawnCard>();
				this.spawnCard.prefab = MasterCatalog.GetMasterPrefab(masterIndex);
				this.spawnCard.inventoryToCopy = this.body.inventory;
				this.spawnCard.equipmentToGrant = new EquipmentDef[1];
				this.spawnCard.itemsToGrant = new ItemCountPair[]
				{
					new ItemCountPair
					{
						itemDef = RoR2Content.Items.SummonedEcho,
						count = 1
					}
				};
				this.CreateSpawners();
			}

			// Token: 0x06001DD7 RID: 7639 RVA: 0x0007FF70 File Offset: 0x0007E170
			private void OnDisable()
			{
				UnityEngine.Object.Destroy(this.spawnCard);
				this.spawnCard = null;
				for (int i = this.spawnedEchoes.Count - 1; i >= 0; i--)
				{
					if (this.spawnedEchoes[i])
					{
						this.spawnedEchoes[i].TrueKill();
					}
				}
				this.DestroySpawners();
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x0007FFD4 File Offset: 0x0007E1D4
			private void CreateSpawners()
			{
				CharacterBody.AffixEchoBehavior.<>c__DisplayClass7_0 CS$<>8__locals1;
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.rng = new Xoroshiro128Plus(Run.instance.seed ^ (ulong)((long)base.GetInstanceID()));
				this.<CreateSpawners>g__CreateSpawner|7_0(ref this.echoSpawner1, DeployableSlot.RoboBallRedBuddy, this.spawnCard, ref CS$<>8__locals1);
				this.<CreateSpawners>g__CreateSpawner|7_0(ref this.echoSpawner2, DeployableSlot.RoboBallGreenBuddy, this.spawnCard, ref CS$<>8__locals1);
			}

			// Token: 0x06001DD9 RID: 7641 RVA: 0x00080033 File Offset: 0x0007E233
			private void DestroySpawners()
			{
				DeployableMinionSpawner deployableMinionSpawner = this.echoSpawner1;
				if (deployableMinionSpawner != null)
				{
					deployableMinionSpawner.Dispose();
				}
				this.echoSpawner1 = null;
				DeployableMinionSpawner deployableMinionSpawner2 = this.echoSpawner2;
				if (deployableMinionSpawner2 != null)
				{
					deployableMinionSpawner2.Dispose();
				}
				this.echoSpawner2 = null;
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x00080068 File Offset: 0x0007E268
			private void OnMinionSpawnedServer(SpawnCard.SpawnResult spawnResult)
			{
				GameObject spawnedInstance = spawnResult.spawnedInstance;
				if (spawnedInstance)
				{
					CharacterMaster spawnedMaster = spawnedInstance.GetComponent<CharacterMaster>();
					if (spawnedMaster)
					{
						this.spawnedEchoes.Add(spawnedMaster);
						OnDestroyCallback.AddCallback(spawnedMaster.gameObject, delegate(OnDestroyCallback _)
						{
							this.spawnedEchoes.Remove(spawnedMaster);
						});
					}
				}
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x000800EC File Offset: 0x0007E2EC
			[CompilerGenerated]
			private void <CreateSpawners>g__CreateSpawner|7_0(ref DeployableMinionSpawner buddySpawner, DeployableSlot deployableSlot, SpawnCard spawnCard, ref CharacterBody.AffixEchoBehavior.<>c__DisplayClass7_0 A_4)
			{
				buddySpawner = new DeployableMinionSpawner(this.body.master, deployableSlot, A_4.rng)
				{
					respawnInterval = 30f,
					spawnCard = spawnCard
				};
				buddySpawner.onMinionSpawnedServer += this.OnMinionSpawnedServer;
			}

			// Token: 0x04002385 RID: 9093
			private DeployableMinionSpawner echoSpawner1;

			// Token: 0x04002386 RID: 9094
			private DeployableMinionSpawner echoSpawner2;

			// Token: 0x04002387 RID: 9095
			private CharacterSpawnCard spawnCard;

			// Token: 0x04002388 RID: 9096
			private List<CharacterMaster> spawnedEchoes = new List<CharacterMaster>();
		}

		// Token: 0x02000628 RID: 1576
		private static class AssetReferences
		{
			// Token: 0x06001DDF RID: 7647 RVA: 0x00080154 File Offset: 0x0007E354
			public static void Resolve()
			{
				CharacterBody.AssetReferences.engiShieldTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/EngiShield");
				CharacterBody.AssetReferences.bucklerShieldTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/BucklerDefense");
				CharacterBody.AssetReferences.slowDownTimeTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/SlowDownTime");
				CharacterBody.AssetReferences.crippleEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/CrippleEffect");
				CharacterBody.AssetReferences.tonicBuffEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/TonicBuffEffect");
				CharacterBody.AssetReferences.weakTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/WeakEffect");
				CharacterBody.AssetReferences.energizedTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/EnergizedEffect");
				CharacterBody.AssetReferences.barrierTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/BarrierEffect");
				CharacterBody.AssetReferences.regenBoostEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/RegenBoostEffect");
				CharacterBody.AssetReferences.elephantDefenseEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/ElephantDefense");
				CharacterBody.AssetReferences.healingDisabledEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/HealingDisabledEffect");
				CharacterBody.AssetReferences.noCooldownEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/NoCooldownEffect");
				CharacterBody.AssetReferences.doppelgangerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/DoppelgangerEffect");
				CharacterBody.AssetReferences.nullifyStack1EffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/NullifyStack1Effect");
				CharacterBody.AssetReferences.nullifyStack2EffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/NullifyStack2Effect");
				CharacterBody.AssetReferences.nullifyStack3EffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/NullifyStack3Effect");
				CharacterBody.AssetReferences.deathmarkEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/DeathMarkEffect");
				CharacterBody.AssetReferences.crocoRegenEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/CrocoRegenEffect");
				CharacterBody.AssetReferences.mercExposeEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/MercExposeEffect");
				CharacterBody.AssetReferences.lifestealOnHitEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/LifeStealOnHitAura");
				CharacterBody.AssetReferences.teamWarCryEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/TeamWarCryAura");
				CharacterBody.AssetReferences.lunarGolemShieldEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/LunarDefense");
				CharacterBody.AssetReferences.randomDamageEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/RandomDamageBuffEffect");
				CharacterBody.AssetReferences.warbannerEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/WarbannerBuffEffect");
				CharacterBody.AssetReferences.teslaFieldEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/TeslaFieldBuffEffect");
				CharacterBody.AssetReferences.lunarSecondaryRootEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/LunarSecondaryRootEffect");
				CharacterBody.AssetReferences.lunarDetonatorEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/LunarDetonatorEffect");
				CharacterBody.AssetReferences.fruitingEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/FruitingEffect");
				CharacterBody.AssetReferences.mushroomVoidTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/MushroomVoidEffect");
				CharacterBody.AssetReferences.bearVoidTempEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/BearVoidEffect");
				CharacterBody.AssetReferences.outOfCombatArmorEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/OutOfCombatArmor/OutOfCombatArmorEffect.prefab").WaitForCompletion();
				CharacterBody.AssetReferences.voidFogMildEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/VoidFogMildEffect");
				CharacterBody.AssetReferences.voidFogStrongEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/VoidFogStrongEffect");
				CharacterBody.AssetReferences.voidRaidcrabWardWipeFogEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidRaidCrab/VoidRaidCrabWardWipeFogEffect.prefab").WaitForCompletion();
				CharacterBody.AssetReferences.voidJailerSlowEffectPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidJailer/VoidJailerTetherDebuff.prefab").WaitForCompletion();
			}

			// Token: 0x0400238D RID: 9101
			public static GameObject engiShieldTempEffectPrefab;

			// Token: 0x0400238E RID: 9102
			public static GameObject bucklerShieldTempEffectPrefab;

			// Token: 0x0400238F RID: 9103
			public static GameObject slowDownTimeTempEffectPrefab;

			// Token: 0x04002390 RID: 9104
			public static GameObject crippleEffectPrefab;

			// Token: 0x04002391 RID: 9105
			public static GameObject tonicBuffEffectPrefab;

			// Token: 0x04002392 RID: 9106
			public static GameObject weakTempEffectPrefab;

			// Token: 0x04002393 RID: 9107
			public static GameObject energizedTempEffectPrefab;

			// Token: 0x04002394 RID: 9108
			public static GameObject barrierTempEffectPrefab;

			// Token: 0x04002395 RID: 9109
			public static GameObject nullifyStack1EffectPrefab;

			// Token: 0x04002396 RID: 9110
			public static GameObject nullifyStack2EffectPrefab;

			// Token: 0x04002397 RID: 9111
			public static GameObject nullifyStack3EffectPrefab;

			// Token: 0x04002398 RID: 9112
			public static GameObject regenBoostEffectPrefab;

			// Token: 0x04002399 RID: 9113
			public static GameObject elephantDefenseEffectPrefab;

			// Token: 0x0400239A RID: 9114
			public static GameObject healingDisabledEffectPrefab;

			// Token: 0x0400239B RID: 9115
			public static GameObject noCooldownEffectPrefab;

			// Token: 0x0400239C RID: 9116
			public static GameObject doppelgangerEffectPrefab;

			// Token: 0x0400239D RID: 9117
			public static GameObject deathmarkEffectPrefab;

			// Token: 0x0400239E RID: 9118
			public static GameObject crocoRegenEffectPrefab;

			// Token: 0x0400239F RID: 9119
			public static GameObject mercExposeEffectPrefab;

			// Token: 0x040023A0 RID: 9120
			public static GameObject lifestealOnHitEffectPrefab;

			// Token: 0x040023A1 RID: 9121
			public static GameObject teamWarCryEffectPrefab;

			// Token: 0x040023A2 RID: 9122
			public static GameObject randomDamageEffectPrefab;

			// Token: 0x040023A3 RID: 9123
			public static GameObject lunarGolemShieldEffectPrefab;

			// Token: 0x040023A4 RID: 9124
			public static GameObject warbannerEffectPrefab;

			// Token: 0x040023A5 RID: 9125
			public static GameObject teslaFieldEffectPrefab;

			// Token: 0x040023A6 RID: 9126
			public static GameObject lunarSecondaryRootEffectPrefab;

			// Token: 0x040023A7 RID: 9127
			public static GameObject lunarDetonatorEffectPrefab;

			// Token: 0x040023A8 RID: 9128
			public static GameObject fruitingEffectPrefab;

			// Token: 0x040023A9 RID: 9129
			public static GameObject mushroomVoidTempEffectPrefab;

			// Token: 0x040023AA RID: 9130
			public static GameObject bearVoidTempEffectPrefab;

			// Token: 0x040023AB RID: 9131
			public static GameObject outOfCombatArmorEffectPrefab;

			// Token: 0x040023AC RID: 9132
			public static GameObject voidFogMildEffectPrefab;

			// Token: 0x040023AD RID: 9133
			public static GameObject voidFogStrongEffectPrefab;

			// Token: 0x040023AE RID: 9134
			public static GameObject voidJailerSlowEffectPrefab;

			// Token: 0x040023AF RID: 9135
			public static GameObject voidRaidcrabWardWipeFogEffectPrefab;

			// Token: 0x040023B0 RID: 9136
			public static GameObject permanentDebuffEffectPrefab;
		}

		// Token: 0x02000629 RID: 1577
		private class ConstructTurretMessage : MessageBase
		{
			// Token: 0x06001DE1 RID: 7649 RVA: 0x00080386 File Offset: 0x0007E586
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.builder);
				writer.Write(this.position);
				writer.Write(this.rotation);
				GeneratedNetworkCode._WriteNetworkMasterIndex_MasterCatalog(writer, this.turretMasterIndex);
			}

			// Token: 0x06001DE2 RID: 7650 RVA: 0x000803B8 File Offset: 0x0007E5B8
			public override void Deserialize(NetworkReader reader)
			{
				this.builder = reader.ReadGameObject();
				this.position = reader.ReadVector3();
				this.rotation = reader.ReadQuaternion();
				this.turretMasterIndex = GeneratedNetworkCode._ReadNetworkMasterIndex_MasterCatalog(reader);
			}

			// Token: 0x040023B1 RID: 9137
			public GameObject builder;

			// Token: 0x040023B2 RID: 9138
			public Vector3 position;

			// Token: 0x040023B3 RID: 9139
			public Quaternion rotation;

			// Token: 0x040023B4 RID: 9140
			public MasterCatalog.NetworkMasterIndex turretMasterIndex;
		}

		// Token: 0x0200062A RID: 1578
		[Serializable]
		public class CharacterBodyUnityEvent : UnityEvent<CharacterBody>
		{
		}
	}
}
