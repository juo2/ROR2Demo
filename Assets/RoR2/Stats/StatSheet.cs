using System;
using System.Linq;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Stats
{
	// Token: 0x02000AC7 RID: 2759
	public class StatSheet
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x00105B61 File Offset: 0x00103D61
		public void SetStatValueFromString([CanBeNull] StatDef statDef, string value)
		{
			if (statDef == null)
			{
				return;
			}
			this.fields[statDef.index].SetFromString(value);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00105B7E File Offset: 0x00103D7E
		public void PushStatValue([CanBeNull] StatDef statDef, ulong statValue)
		{
			if (statDef == null)
			{
				return;
			}
			this.fields[statDef.index].PushStatValue(statValue);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x00105B9B File Offset: 0x00103D9B
		public void PushStatValue([CanBeNull] StatDef statDef, double statValue)
		{
			if (statDef == null)
			{
				return;
			}
			this.fields[statDef.index].PushStatValue(statValue);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00105BB8 File Offset: 0x00103DB8
		public void PushStatValue([NotNull] PerBodyStatDef perBodyStatDef, BodyIndex bodyIndex, ulong statValue)
		{
			this.PushStatValue(perBodyStatDef.FindStatDef(bodyIndex), statValue);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x00105BC8 File Offset: 0x00103DC8
		public void PushStatValue([NotNull] PerBodyStatDef perBodyStatDef, BodyIndex bodyIndex, double statValue)
		{
			this.PushStatValue(perBodyStatDef.FindStatDef(bodyIndex), statValue);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00105BD8 File Offset: 0x00103DD8
		public ulong GetStatValueULong([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return 0UL;
			}
			return this.fields[statDef.index].GetULongValue();
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00105BF6 File Offset: 0x00103DF6
		public double GetStatValueDouble([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return 0.0;
			}
			return this.fields[statDef.index].GetDoubleValue();
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x00105C1B File Offset: 0x00103E1B
		public double GetStatValueAsDouble([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return 0.0;
			}
			return this.fields[statDef.index].GetValueAsDouble();
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x00105C40 File Offset: 0x00103E40
		[Obsolete]
		public decimal GetStatValueDecimal([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return 0m;
			}
			return this.fields[statDef.index].GetDecimalValue();
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x00105C61 File Offset: 0x00103E61
		[NotNull]
		public string GetStatValueString([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return "INVALID_STAT";
			}
			return this.fields[statDef.index].ToString();
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x00105C88 File Offset: 0x00103E88
		[NotNull]
		public string GetStatDisplayValue([CanBeNull] StatDef statDef)
		{
			if (statDef == null)
			{
				return "INVALID_STAT";
			}
			return statDef.displayValueFormatter(ref this.fields[statDef.index]);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x00105CAF File Offset: 0x00103EAF
		public ulong GetStatPointValue([NotNull] StatDef statDef)
		{
			return this.fields[statDef.index].GetPointValue(statDef.pointValue);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x00105CCD File Offset: 0x00103ECD
		public ulong GetStatValueULong([NotNull] PerBodyStatDef perBodyStatDef, [NotNull] string bodyName)
		{
			return this.GetStatValueULong(perBodyStatDef.FindStatDef(bodyName));
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x00105CDC File Offset: 0x00103EDC
		public double GetStatValueDouble([NotNull] PerBodyStatDef perBodyStatDef, [NotNull] string bodyName)
		{
			return this.GetStatValueDouble(perBodyStatDef.FindStatDef(bodyName));
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00105CEB File Offset: 0x00103EEB
		[NotNull]
		public string GetStatValueString([NotNull] PerBodyStatDef perBodyStatDef, [NotNull] string bodyName)
		{
			return this.GetStatValueString(perBodyStatDef.FindStatDef(bodyName));
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x00105CFC File Offset: 0x00103EFC
		public BodyIndex FindBodyWithHighestStat([NotNull] PerBodyStatDef perBodyStatDef)
		{
			StatField statField = this.fields[perBodyStatDef.FindStatDef((BodyIndex)0).index];
			BodyIndex result = (BodyIndex)0;
			for (BodyIndex bodyIndex = (BodyIndex)1; bodyIndex < (BodyIndex)BodyCatalog.bodyCount; bodyIndex++)
			{
				ref StatField ptr = ref this.fields[perBodyStatDef.FindStatDef(bodyIndex).index];
				if (statField.CompareTo(ptr) < 0)
				{
					statField = ptr;
					result = bodyIndex;
				}
			}
			if (statField.IsDefault())
			{
				return BodyIndex.None;
			}
			return result;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x00105D6C File Offset: 0x00103F6C
		public EquipmentIndex FindEquipmentWithHighestStat([NotNull] PerEquipmentStatDef perEquipmentStatDef)
		{
			StatField statField = this.fields[perEquipmentStatDef.FindStatDef((EquipmentIndex)0).index];
			EquipmentIndex result = (EquipmentIndex)0;
			for (int i = 1; i < EquipmentCatalog.equipmentCount; i++)
			{
				ref StatField ptr = ref this.fields[perEquipmentStatDef.FindStatDef((EquipmentIndex)i).index];
				if (statField.CompareTo(ptr) < 0)
				{
					statField = ptr;
					result = (EquipmentIndex)i;
				}
			}
			if (statField.IsDefault())
			{
				return EquipmentIndex.None;
			}
			return result;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00105DDB File Offset: 0x00103FDB
		[SystemInitializer(new Type[]
		{
			typeof(StatDef)
		})]
		private static void Init()
		{
			Debug.Log("init stat sheet");
			StatSheet.OnFieldsFinalized();
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x00105DEC File Offset: 0x00103FEC
		static StatSheet()
		{
			HGXml.Register<StatSheet>(new HGXml.Serializer<StatSheet>(StatSheet.ToXml), new HGXml.Deserializer<StatSheet>(StatSheet.FromXml));
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x00105E0C File Offset: 0x0010400C
		public static void ToXml(XElement element, StatSheet statSheet)
		{
			element.RemoveAll();
			XElement xelement = new XElement("fields");
			element.Add(xelement);
			StatField[] array = statSheet.fields;
			for (int i = 0; i < array.Length; i++)
			{
				ref StatField ptr = ref array[i];
				if (!ptr.IsDefault())
				{
					xelement.Add(new XElement(ptr.name, ptr.ToString()));
				}
			}
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x00105E80 File Offset: 0x00104080
		public static bool FromXml(XElement element, ref StatSheet statSheet)
		{
			XElement xelement = element.Element("fields");
			if (xelement == null)
			{
				return false;
			}
			StatField[] array = statSheet.fields;
			for (int i = 0; i < array.Length; i++)
			{
				ref StatField ptr = ref array[i];
				XElement xelement2 = xelement.Element(ptr.name);
				if (xelement2 != null)
				{
					ptr.SetFromString(xelement2.Value);
				}
			}
			return true;
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x00105EE8 File Offset: 0x001040E8
		private static void OnFieldsFinalized()
		{
			StatSheet.fieldsTemplate = (from v in StatDef.allStatDefs
			select new StatField
			{
				statDef = v
			}).ToArray<StatField>();
			StatSheet.nonDefaultFieldsBuffer = new bool[StatSheet.fieldsTemplate.Length];
			PlatformSystems.saveSystem.isXmlReady = true;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x00105F44 File Offset: 0x00104144
		private StatSheet([NotNull] StatField[] fields)
		{
			this.fields = fields;
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x00105F60 File Offset: 0x00104160
		public static StatSheet New()
		{
			StatField[] array = new StatField[StatSheet.fieldsTemplate.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StatSheet.fieldsTemplate[i];
			}
			return new StatSheet(array);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x00105FA0 File Offset: 0x001041A0
		public int GetUnlockableCount()
		{
			return this.unlockables.Length;
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x00105FAA File Offset: 0x001041AA
		public UnlockableIndex GetUnlockableIndex(int index)
		{
			return this.unlockables[index];
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x00105FB4 File Offset: 0x001041B4
		public UnlockableDef GetUnlockable(int index)
		{
			return UnlockableCatalog.GetUnlockableDef(this.unlockables[index]);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x00105FC4 File Offset: 0x001041C4
		public bool HasUnlockable([CanBeNull] UnlockableDef unlockableDef)
		{
			if (!unlockableDef || unlockableDef.index == UnlockableIndex.None)
			{
				return true;
			}
			for (int i = 0; i < this.unlockables.Length; i++)
			{
				if (this.unlockables[i] == unlockableDef.index)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0010600A File Offset: 0x0010420A
		private void AllocateUnlockables(int desiredCount)
		{
			Array.Resize<UnlockableIndex>(ref this.unlockables, desiredCount);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x00106018 File Offset: 0x00104218
		public void AddUnlockable([NotNull] UnlockableDef unlockableDef)
		{
			this.AddUnlockable(unlockableDef.index);
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x00106026 File Offset: 0x00104226
		public void AddUnlockable(UnlockableIndex unlockIndex)
		{
			if (Array.IndexOf<UnlockableIndex>(this.unlockables, unlockIndex) != -1)
			{
				return;
			}
			Array.Resize<UnlockableIndex>(ref this.unlockables, this.unlockables.Length + 1);
			this.unlockables[this.unlockables.Length - 1] = unlockIndex;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00106060 File Offset: 0x00104260
		public void RemoveUnlockable(UnlockableIndex unlockIndex)
		{
			int num = Array.IndexOf<UnlockableIndex>(this.unlockables, unlockIndex);
			if (num == -1)
			{
				return;
			}
			int newSize = this.unlockables.Length;
			ArrayUtils.ArrayRemoveAt<UnlockableIndex>(this.unlockables, ref newSize, num, 1);
			Array.Resize<UnlockableIndex>(ref this.unlockables, newSize);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x001060A4 File Offset: 0x001042A4
		public void Write(NetworkWriter writer)
		{
			for (int i = 0; i < this.fields.Length; i++)
			{
				StatSheet.nonDefaultFieldsBuffer[i] = !this.fields[i].IsDefault();
			}
			writer.WriteBitArray(StatSheet.nonDefaultFieldsBuffer);
			for (int j = 0; j < this.fields.Length; j++)
			{
				if (StatSheet.nonDefaultFieldsBuffer[j])
				{
					this.fields[j].Write(writer);
				}
			}
			writer.Write((byte)this.unlockables.Length);
			for (int k = 0; k < this.unlockables.Length; k++)
			{
				writer.Write(this.unlockables[k]);
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x00106148 File Offset: 0x00104348
		public void Read(NetworkReader reader)
		{
			reader.ReadBitArray(StatSheet.nonDefaultFieldsBuffer);
			for (int i = 0; i < this.fields.Length; i++)
			{
				if (StatSheet.nonDefaultFieldsBuffer[i])
				{
					this.fields[i].Read(reader);
				}
				else
				{
					this.fields[i].SetDefault();
				}
			}
			int num = (int)reader.ReadByte();
			this.AllocateUnlockables(num);
			for (int j = 0; j < num; j++)
			{
				this.unlockables[j] = reader.ReadUnlockableIndex();
			}
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x001061CC File Offset: 0x001043CC
		public static void GetDelta(StatSheet result, StatSheet newerStats, StatSheet olderStats)
		{
			StatField[] array = result.fields;
			StatField[] array2 = newerStats.fields;
			StatField[] array3 = olderStats.fields;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StatField.GetDelta(ref array2[i], ref array3[i]);
			}
			UnlockableIndex[] array4 = newerStats.unlockables;
			UnlockableIndex[] array5 = olderStats.unlockables;
			int num = 0;
			foreach (UnlockableIndex unlockableIndex in array4)
			{
				bool flag = false;
				for (int k = 0; k < array5.Length; k++)
				{
					if (array5[k] == unlockableIndex)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num++;
				}
			}
			result.AllocateUnlockables(num);
			UnlockableIndex[] array6 = result.unlockables;
			int num2 = 0;
			foreach (UnlockableIndex unlockableIndex2 in array4)
			{
				bool flag2 = false;
				for (int m = 0; m < array5.Length; m++)
				{
					if (array5[m] == unlockableIndex2)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					array6[num2++] = unlockableIndex2;
				}
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x001062D4 File Offset: 0x001044D4
		public void ApplyDelta(StatSheet deltaSheet)
		{
			StatField[] array = deltaSheet.fields;
			for (int i = 0; i < this.fields.Length; i++)
			{
				this.fields[i].PushDelta(ref array[i]);
			}
			for (int j = 0; j < deltaSheet.unlockables.Length; j++)
			{
				this.AddUnlockable(deltaSheet.unlockables[j]);
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x00106334 File Offset: 0x00104534
		public void SetAllFieldsToMaxValue()
		{
			for (int i = 0; i < this.fields.Length; i++)
			{
				this.fields[i].SetToMaxValue();
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00106368 File Offset: 0x00104568
		public static void Copy([NotNull] StatSheet src, [NotNull] StatSheet dest)
		{
			Array.Copy(src.fields, dest.fields, src.fields.Length);
			dest.AllocateUnlockables(src.unlockables.Length);
			Array.Copy(src.unlockables, dest.unlockables, src.unlockables.Length);
		}

		// Token: 0x04003DD6 RID: 15830
		private static StatField[] fieldsTemplate;

		// Token: 0x04003DD7 RID: 15831
		private static bool[] nonDefaultFieldsBuffer;

		// Token: 0x04003DD8 RID: 15832
		public readonly StatField[] fields;

		// Token: 0x04003DD9 RID: 15833
		private UnlockableIndex[] unlockables = Array.Empty<UnlockableIndex>();
	}
}
