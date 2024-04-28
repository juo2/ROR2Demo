using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020004BA RID: 1210
	public static class NetworkExtensions
	{
		// Token: 0x060015B4 RID: 5556 RVA: 0x00060B9C File Offset: 0x0005ED9C
		public static void WriteAchievementIndex(this NetworkWriter writer, AchievementIndex value)
		{
			writer.WritePackedUInt32((uint)value.intValue);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00060BAC File Offset: 0x0005EDAC
		public static AchievementIndex ReadAchievementIndex(this NetworkReader reader)
		{
			return new AchievementIndex
			{
				intValue = (int)reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00060BCF File Offset: 0x0005EDCF
		public static void WriteBodyIndex(this NetworkWriter writer, BodyIndex bodyIndex)
		{
			writer.WritePackedIndex32((int)bodyIndex);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00060BD8 File Offset: 0x0005EDD8
		public static BodyIndex ReadBodyIndex(this NetworkReader reader)
		{
			return (BodyIndex)reader.ReadPackedIndex32();
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00060BCF File Offset: 0x0005EDCF
		public static void WriteBuffIndex(this NetworkWriter writer, BuffIndex buffIndex)
		{
			writer.WritePackedIndex32((int)buffIndex);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00060BD8 File Offset: 0x0005EDD8
		public static BuffIndex ReadBuffIndex(this NetworkReader reader)
		{
			return (BuffIndex)reader.ReadPackedIndex32();
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00060BE0 File Offset: 0x0005EDE0
		public static DamageType ReadDamageType(this NetworkReader reader)
		{
			return (DamageType)reader.ReadPackedUInt32();
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00060BE8 File Offset: 0x0005EDE8
		public static void Write(this NetworkWriter writer, DamageType damageType)
		{
			writer.WritePackedUInt32((uint)damageType);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00060BF1 File Offset: 0x0005EDF1
		public static DamageColorIndex ReadDamageColorIndex(this NetworkReader reader)
		{
			return (DamageColorIndex)reader.ReadByte();
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00060BF9 File Offset: 0x0005EDF9
		public static void Write(this NetworkWriter writer, DamageColorIndex damageColorIndex)
		{
			writer.Write((byte)damageColorIndex);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00060C04 File Offset: 0x0005EE04
		public static void Write(this NetworkWriter writer, DamageInfo damageInfo)
		{
			writer.Write(damageInfo.damage);
			writer.Write(damageInfo.crit);
			writer.Write(damageInfo.attacker);
			writer.Write(damageInfo.inflictor);
			writer.Write(damageInfo.position);
			writer.Write(damageInfo.force);
			writer.Write(damageInfo.procChainMask);
			writer.Write(damageInfo.procCoefficient);
			writer.WritePackedUInt32((uint)damageInfo.damageType);
			writer.Write((byte)damageInfo.damageColorIndex);
			writer.Write((byte)(damageInfo.dotIndex + 1));
			writer.Write(damageInfo.canRejectForce);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00060CA4 File Offset: 0x0005EEA4
		public static DamageInfo ReadDamageInfo(this NetworkReader reader)
		{
			return new DamageInfo
			{
				damage = reader.ReadSingle(),
				crit = reader.ReadBoolean(),
				attacker = reader.ReadGameObject(),
				inflictor = reader.ReadGameObject(),
				position = reader.ReadVector3(),
				force = reader.ReadVector3(),
				procChainMask = reader.ReadProcChainMask(),
				procCoefficient = reader.ReadSingle(),
				damageType = (DamageType)reader.ReadPackedUInt32(),
				damageColorIndex = (DamageColorIndex)reader.ReadByte(),
				dotIndex = (DotController.DotIndex)(reader.ReadByte() - 1),
				canRejectForce = reader.ReadBoolean()
			};
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00060D48 File Offset: 0x0005EF48
		public static void WriteEffectIndex(this NetworkWriter writer, EffectIndex effectIndex)
		{
			writer.WritePackedUInt32((uint)(effectIndex + 1));
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00060D53 File Offset: 0x0005EF53
		public static EffectIndex ReadEffectIndex(this NetworkReader reader)
		{
			return (EffectIndex)(reader.ReadPackedUInt32() - 1U);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00060D5D File Offset: 0x0005EF5D
		public static void Write(this NetworkWriter writer, EffectData effectData)
		{
			effectData.Serialize(writer);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00060D66 File Offset: 0x0005EF66
		public static EffectData ReadEffectData(this NetworkReader reader)
		{
			EffectData effectData = new EffectData();
			effectData.Deserialize(reader);
			return effectData;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00060D74 File Offset: 0x0005EF74
		public static void ReadEffectData(this NetworkReader reader, EffectData effectData)
		{
			effectData.Deserialize(reader);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00060BCF File Offset: 0x0005EDCF
		public static void Write(this NetworkWriter writer, EntityStateIndex entityStateIndex)
		{
			writer.WritePackedIndex32((int)entityStateIndex);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00060BD8 File Offset: 0x0005EDD8
		public static EntityStateIndex ReadEntityStateIndex(this NetworkReader reader)
		{
			return (EntityStateIndex)reader.ReadPackedIndex32();
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00060D48 File Offset: 0x0005EF48
		public static void Write(this NetworkWriter writer, EquipmentIndex equipmentIndex)
		{
			writer.WritePackedUInt32((uint)(equipmentIndex + 1));
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00060D53 File Offset: 0x0005EF53
		public static EquipmentIndex ReadEquipmentIndex(this NetworkReader reader)
		{
			return (EquipmentIndex)(reader.ReadPackedUInt32() - 1U);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00060D7D File Offset: 0x0005EF7D
		public static void Write(this NetworkWriter writer, Run.TimeStamp timeStamp)
		{
			Run.TimeStamp.Serialize(writer, timeStamp);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00060D86 File Offset: 0x0005EF86
		public static Run.TimeStamp ReadTimeStamp(this NetworkReader reader)
		{
			return Run.TimeStamp.Deserialize(reader);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00060D8E File Offset: 0x0005EF8E
		public static void Write(this NetworkWriter writer, Run.FixedTimeStamp timeStamp)
		{
			Run.FixedTimeStamp.Serialize(writer, timeStamp);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00060D97 File Offset: 0x0005EF97
		public static Run.FixedTimeStamp ReadFixedTimeStamp(this NetworkReader reader)
		{
			return Run.FixedTimeStamp.Deserialize(reader);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00060D9F File Offset: 0x0005EF9F
		public static void Write(this NetworkWriter writer, HurtBoxReference hurtBoxReference)
		{
			hurtBoxReference.Write(writer);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00060DAC File Offset: 0x0005EFAC
		public static HurtBoxReference ReadHurtBoxReference(this NetworkReader reader)
		{
			HurtBoxReference result = default(HurtBoxReference);
			result.Read(reader);
			return result;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00060D48 File Offset: 0x0005EF48
		public static void Write(this NetworkWriter writer, ItemIndex itemIndex)
		{
			writer.WritePackedUInt32((uint)(itemIndex + 1));
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00060D53 File Offset: 0x0005EF53
		public static ItemIndex ReadItemIndex(this NetworkReader reader)
		{
			return (ItemIndex)(reader.ReadPackedUInt32() - 1U);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00060DCA File Offset: 0x0005EFCA
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			NetworkExtensions.itemMaskBitCount = ItemCatalog.itemCount;
			NetworkExtensions.itemMaskByteCount = NetworkExtensions.itemMaskBitCount + 7 >> 3;
			NetworkExtensions.itemMaskByteBuffer = new byte[NetworkExtensions.itemMaskByteCount];
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00060DF4 File Offset: 0x0005EFF4
		public static void WriteItemStacks(this NetworkWriter writer, int[] srcItemStacks)
		{
			int num = 0;
			for (int i = 0; i < NetworkExtensions.itemMaskByteCount; i++)
			{
				byte b = 0;
				int num2 = 0;
				while (num2 < 8 && num < NetworkExtensions.itemMaskBitCount)
				{
					if (srcItemStacks[num] > 0)
					{
						b |= (byte)(1 << num2);
					}
					num2++;
					num++;
				}
				NetworkExtensions.itemMaskByteBuffer[i] = b;
			}
			for (int j = 0; j < NetworkExtensions.itemMaskByteCount; j++)
			{
				writer.Write(NetworkExtensions.itemMaskByteBuffer[j]);
			}
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				int num3 = srcItemStacks[(int)itemIndex];
				if (num3 > 0)
				{
					writer.WritePackedUInt32((uint)num3);
				}
				itemIndex++;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00060E94 File Offset: 0x0005F094
		public static void ReadItemStacks(this NetworkReader reader, int[] destItemStacks)
		{
			for (int i = 0; i < NetworkExtensions.itemMaskByteCount; i++)
			{
				NetworkExtensions.itemMaskByteBuffer[i] = reader.ReadByte();
			}
			int num = 0;
			for (int j = 0; j < NetworkExtensions.itemMaskByteCount; j++)
			{
				byte b = NetworkExtensions.itemMaskByteBuffer[j];
				int num2 = 0;
				while (num2 < 8 && num < NetworkExtensions.itemMaskBitCount)
				{
					destItemStacks[num] = (int)(((b & (byte)(1 << num2)) != 0) ? reader.ReadPackedUInt32() : 0U);
					num2++;
					num++;
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00060F0C File Offset: 0x0005F10C
		public static void WriteBitArray(this NetworkWriter writer, [NotNull] bool[] values)
		{
			writer.WriteBitArray(values, values.Length);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00060F18 File Offset: 0x0005F118
		public static void WriteBitArray(this NetworkWriter writer, [NotNull] bool[] values, int bufferLength)
		{
			int num = bufferLength + 7 >> 3;
			int num2 = num - 1;
			int num3 = bufferLength - (num2 << 3);
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				byte b = 0;
				int num5 = (i < num2) ? 8 : num3;
				int j = 0;
				while (j < num5)
				{
					if (values[num4])
					{
						b |= (byte)(1 << j);
					}
					j++;
					num4++;
				}
				writer.Write(b);
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00060F85 File Offset: 0x0005F185
		public static void ReadBitArray(this NetworkReader reader, [NotNull] bool[] values)
		{
			reader.ReadBitArray(values, values.Length);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00060F94 File Offset: 0x0005F194
		public static void ReadBitArray(this NetworkReader reader, [NotNull] bool[] values, int bufferLength)
		{
			int num = bufferLength + 7 >> 3;
			int num2 = num - 1;
			int num3 = bufferLength - (num2 << 3);
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				int num5 = (i < num2) ? 8 : num3;
				byte b = reader.ReadByte();
				int j = 0;
				while (j < num5)
				{
					values[num4] = ((b & (byte)(1 << j)) > 0);
					j++;
					num4++;
				}
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00060D48 File Offset: 0x0005EF48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WritePackedIndex32(this NetworkWriter writer, int index)
		{
			writer.WritePackedUInt32((uint)(index + 1));
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00060D53 File Offset: 0x0005EF53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadPackedIndex32(this NetworkReader reader)
		{
			return (int)(reader.ReadPackedUInt32() - 1U);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00060FFC File Offset: 0x0005F1FC
		public static void Write(this NetworkWriter writer, NetworkPlayerName networkPlayerName)
		{
			networkPlayerName.Serialize(writer);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00061008 File Offset: 0x0005F208
		public static NetworkPlayerName ReadNetworkPlayerName(this NetworkReader reader)
		{
			NetworkPlayerName result = default(NetworkPlayerName);
			result.Deserialize(reader);
			return result;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00061026 File Offset: 0x0005F226
		public static void Write(this NetworkWriter writer, PackedUnitVector3 value)
		{
			writer.Write(value.value);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00061034 File Offset: 0x0005F234
		public static PackedUnitVector3 ReadPackedUnitVector3(this NetworkReader reader)
		{
			return new PackedUnitVector3(reader.ReadUInt16());
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00061041 File Offset: 0x0005F241
		public static void Write(this NetworkWriter writer, PickupIndex value)
		{
			PickupIndex.WriteToNetworkWriter(writer, value);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0006104A File Offset: 0x0005F24A
		public static PickupIndex ReadPickupIndex(this NetworkReader reader)
		{
			return PickupIndex.ReadFromNetworkReader(reader);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00061052 File Offset: 0x0005F252
		public static void Write(this NetworkWriter writer, PitchYawPair pitchYawPair)
		{
			writer.Write(pitchYawPair.pitch);
			writer.Write(pitchYawPair.yaw);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00061070 File Offset: 0x0005F270
		public static PitchYawPair ReadPitchYawPair(this NetworkReader reader)
		{
			float pitch = reader.ReadSingle();
			float yaw = reader.ReadSingle();
			return new PitchYawPair(pitch, yaw);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00061090 File Offset: 0x0005F290
		public static void Write(this NetworkWriter writer, ProcChainMask procChainMask)
		{
			writer.WritePackedUInt32(procChainMask.mask);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000610A0 File Offset: 0x0005F2A0
		public static ProcChainMask ReadProcChainMask(this NetworkReader reader)
		{
			return new ProcChainMask
			{
				mask = reader.ReadPackedUInt32()
			};
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000610C3 File Offset: 0x0005F2C3
		public static void Write(this NetworkWriter writer, RuleBook src)
		{
			src.Serialize(writer);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000610CC File Offset: 0x0005F2CC
		public static void ReadRuleBook(this NetworkReader reader, RuleBook dest)
		{
			dest.Deserialize(reader);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000610D5 File Offset: 0x0005F2D5
		public static void Write(this NetworkWriter writer, RuleMask src)
		{
			src.Serialize(writer);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000610DE File Offset: 0x0005F2DE
		public static void ReadRuleMask(this NetworkReader reader, RuleMask dest)
		{
			dest.Deserialize(reader);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000610E7 File Offset: 0x0005F2E7
		public static void Write(this NetworkWriter writer, RuleChoiceMask src)
		{
			src.Serialize(writer);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000610F0 File Offset: 0x0005F2F0
		public static void ReadRuleChoiceMask(this NetworkReader reader, RuleChoiceMask dest)
		{
			dest.Deserialize(reader);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000610FC File Offset: 0x0005F2FC
		public static void Write(this NetworkWriter writer, TeamIndex teamIndex)
		{
			byte value = (byte)(teamIndex + 1);
			writer.Write(value);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00061115 File Offset: 0x0005F315
		public static TeamIndex ReadTeamIndex(this NetworkReader reader)
		{
			return (TeamIndex)(reader.ReadByte() - 1);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00060BCF File Offset: 0x0005EDCF
		public static void Write(this NetworkWriter writer, UnlockableIndex index)
		{
			writer.WritePackedIndex32((int)index);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00060BD8 File Offset: 0x0005EDD8
		public static UnlockableIndex ReadUnlockableIndex(this NetworkReader reader)
		{
			return (UnlockableIndex)reader.ReadPackedIndex32();
		}

		// Token: 0x04001BAD RID: 7085
		private static int itemMaskBitCount;

		// Token: 0x04001BAE RID: 7086
		private static int itemMaskByteCount;

		// Token: 0x04001BAF RID: 7087
		private static byte[] itemMaskByteBuffer;
	}
}
