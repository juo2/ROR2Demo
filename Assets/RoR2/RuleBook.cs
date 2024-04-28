using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using HG;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A13 RID: 2579
	public class RuleBook : IEquatable<RuleBook>
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000F5E5B File Offset: 0x000F405B
		public uint startingMoney
		{
			get
			{
				return (uint)this.GetRuleChoice(RuleBook.startingMoneyRule).extraData;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000F5E72 File Offset: 0x000F4072
		public StageOrder stageOrder
		{
			get
			{
				return (StageOrder)this.GetRuleChoice(RuleBook.stageOrderRule).extraData;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000F5E89 File Offset: 0x000F4089
		public bool keepMoneyBetweenStages
		{
			get
			{
				return (bool)this.GetRuleChoice(RuleBook.keepMoneyBetweenStagesRule).extraData;
			}
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000F5EA0 File Offset: 0x000F40A0
		[SystemInitializer(new Type[]
		{
			typeof(RuleCatalog)
		})]
		private static void Init()
		{
			RuleBook.defaultValues = new byte[RuleCatalog.ruleCount];
			for (int i = 0; i < RuleCatalog.ruleCount; i++)
			{
				RuleBook.defaultValues[i] = (byte)RuleCatalog.GetRuleDef(i).defaultChoiceIndex;
			}
			HGXml.Register<RuleBook>(new HGXml.Serializer<RuleBook>(RuleBook.ToXml), new HGXml.Deserializer<RuleBook>(RuleBook.FromXml));
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000F5EFC File Offset: 0x000F40FC
		public RuleBook()
		{
			this.SetToDefaults();
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000F5F1A File Offset: 0x000F411A
		public void SetToDefaults()
		{
			Array.Copy(RuleBook.defaultValues, 0, this.ruleValues, 0, this.ruleValues.Length);
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000F5F36 File Offset: 0x000F4136
		public void ApplyChoice(RuleChoiceDef choiceDef)
		{
			this.ruleValues[choiceDef.ruleDef.globalIndex] = (byte)choiceDef.localIndex;
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000F5F51 File Offset: 0x000F4151
		public bool IsChoiceActive(RuleChoiceDef choiceDef)
		{
			return (int)this.ruleValues[choiceDef.ruleDef.globalIndex] == choiceDef.localIndex;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000F5F6D File Offset: 0x000F416D
		public int GetRuleChoiceIndex(int ruleIndex)
		{
			return (int)this.ruleValues[ruleIndex];
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000F5F77 File Offset: 0x000F4177
		public int GetRuleChoiceIndex(RuleDef ruleDef)
		{
			return (int)this.ruleValues[ruleDef.globalIndex];
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000F5F86 File Offset: 0x000F4186
		public RuleChoiceDef GetRuleChoice(int ruleIndex)
		{
			return RuleCatalog.GetRuleDef(ruleIndex).choices[(int)this.ruleValues[ruleIndex]];
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000F5FA0 File Offset: 0x000F41A0
		public RuleChoiceDef GetRuleChoice(RuleDef ruleDef)
		{
			return ruleDef.choices[(int)this.ruleValues[ruleDef.globalIndex]];
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000F5FBC File Offset: 0x000F41BC
		public void Serialize(NetworkWriter writer)
		{
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				writer.Write(this.ruleValues[i]);
			}
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000F5FEC File Offset: 0x000F41EC
		public void Deserialize(NetworkReader reader)
		{
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				this.ruleValues[i] = reader.ReadByte();
			}
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000F601A File Offset: 0x000F421A
		public bool Equals(RuleBook other)
		{
			return other != null && ArrayUtils.SequenceEquals<byte>(this.ruleValues, other.ruleValues);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000F6032 File Offset: 0x000F4232
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RuleBook);
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000F6040 File Offset: 0x000F4240
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				num += (int)this.ruleValues[i];
			}
			return num;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000F606E File Offset: 0x000F426E
		public void Copy([NotNull] RuleBook src)
		{
			if (src == null)
			{
				throw new ArgumentException("Argument cannot be null.", "src");
			}
			Array.Copy(src.ruleValues, this.ruleValues, this.ruleValues.Length);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000F609C File Offset: 0x000F429C
		public DifficultyIndex FindDifficulty()
		{
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				RuleChoiceDef ruleChoiceDef = RuleCatalog.GetRuleDef(i).choices[(int)this.ruleValues[i]];
				if (ruleChoiceDef.difficultyIndex != DifficultyIndex.Invalid)
				{
					return ruleChoiceDef.difficultyIndex;
				}
			}
			return DifficultyIndex.Invalid;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000F60E8 File Offset: 0x000F42E8
		public ArtifactMask GenerateArtifactMask()
		{
			ArtifactMask result = default(ArtifactMask);
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				RuleChoiceDef ruleChoiceDef = RuleCatalog.GetRuleDef(i).choices[(int)this.ruleValues[i]];
				if (ruleChoiceDef.artifactIndex != ArtifactIndex.None)
				{
					result.AddArtifact(ruleChoiceDef.artifactIndex);
				}
			}
			return result;
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000F6140 File Offset: 0x000F4340
		public void GenerateItemMask([NotNull] ItemMask dest)
		{
			dest.Clear();
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				RuleChoiceDef ruleChoiceDef = RuleCatalog.GetRuleDef(i).choices[(int)this.ruleValues[i]];
				if (ruleChoiceDef.itemIndex != ItemIndex.None)
				{
					dest.Add(ruleChoiceDef.itemIndex);
				}
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000F6194 File Offset: 0x000F4394
		public void GenerateEquipmentMask([NotNull] EquipmentMask dest)
		{
			dest.Clear();
			for (int i = 0; i < this.ruleValues.Length; i++)
			{
				RuleChoiceDef ruleChoiceDef = RuleCatalog.GetRuleDef(i).choices[(int)this.ruleValues[i]];
				if (ruleChoiceDef.equipmentIndex != EquipmentIndex.None)
				{
					dest.Add(ruleChoiceDef.equipmentIndex);
				}
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000F61E8 File Offset: 0x000F43E8
		public static void ToXml(XElement element, RuleBook src)
		{
			byte[] array = src.ruleValues;
			RuleBook.<>c__DisplayClass30_0 CS$<>8__locals1;
			CS$<>8__locals1.choiceNamesBuffer = new string[array.Length];
			CS$<>8__locals1.choiceNamesCount = 0;
			for (int i = 0; i < array.Length; i++)
			{
				RuleDef ruleDef = RuleCatalog.GetRuleDef(i);
				byte b = array[i];
				if ((ulong)b < (ulong)((long)ruleDef.choices.Count))
				{
					RuleBook.<ToXml>g__AddChoice|30_0(ruleDef.choices[(int)b].globalName, ref CS$<>8__locals1);
				}
			}
			element.Value = string.Join(" ", CS$<>8__locals1.choiceNamesBuffer, 0, CS$<>8__locals1.choiceNamesCount);
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000F6274 File Offset: 0x000F4474
		public static bool FromXml(XElement element, ref RuleBook dest)
		{
			dest.SetToDefaults();
			string[] array = element.Value.Split(new char[]
			{
				' '
			});
			for (int i = 0; i < array.Length; i++)
			{
				RuleChoiceDef ruleChoiceDef = RuleCatalog.FindChoiceDef(array[i]);
				if (ruleChoiceDef != null)
				{
					dest.ApplyChoice(ruleChoiceDef);
				}
			}
			return true;
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000F62C4 File Offset: 0x000F44C4
		public static void WriteBase64ToStringBuilder(RuleBook src, StringBuilder dest)
		{
			RuleBook.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.dest = dest;
			byte[] array = src.ruleValues;
			CS$<>8__locals1.inputWriteIndex = 0;
			CS$<>8__locals1.inputBytes = new byte[3];
			CS$<>8__locals1.outputChars = new char[4];
			CS$<>8__locals1.bytesPerRuleChoiceGlobalIndex = 1;
			if (RuleCatalog.choiceCount > 256)
			{
				CS$<>8__locals1.bytesPerRuleChoiceGlobalIndex = 2;
				if (RuleCatalog.choiceCount > 65536)
				{
					CS$<>8__locals1.bytesPerRuleChoiceGlobalIndex = 3;
					if (RuleCatalog.choiceCount > 16777216)
					{
						CS$<>8__locals1.bytesPerRuleChoiceGlobalIndex = 4;
					}
				}
			}
			RuleBook.<WriteBase64ToStringBuilder>g__PushByte|32_1((byte)CS$<>8__locals1.bytesPerRuleChoiceGlobalIndex, ref CS$<>8__locals1);
			for (int i = 0; i < array.Length; i++)
			{
				byte index = array[i];
				RuleChoiceDef ruleChoiceDef = RuleCatalog.GetRuleDef(i).choices[(int)index];
				if (!ruleChoiceDef.isDefaultChoice)
				{
					RuleBook.<WriteBase64ToStringBuilder>g__PushRuleChoiceGlobalIndex|32_0((int)((ushort)ruleChoiceDef.globalIndex), ref CS$<>8__locals1);
				}
			}
			RuleBook.<WriteBase64ToStringBuilder>g__PushCurrentInputToDest|32_2(ref CS$<>8__locals1);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000F6398 File Offset: 0x000F4598
		public static void ReadBase64(string src, RuleBook dest)
		{
			dest.SetToDefaults();
			byte[] array = System.Convert.FromBase64String(src);
			if (array.Length == 0)
			{
				return;
			}
			int num = (int)array[0];
			if (num > 4)
			{
				return;
			}
			for (int i = 1; i < array.Length; i += num)
			{
				int num2 = 0;
				for (int j = 0; j < num; j++)
				{
					num2 <<= 8;
					num2 |= (int)array[i + j];
				}
				RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(num2);
				if (choiceDef != null)
				{
					dest.ApplyChoice(choiceDef);
				}
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000F6401 File Offset: 0x000F4601
		public RuleBook.ChoicesEnumerable choices
		{
			get
			{
				return new RuleBook.ChoicesEnumerable(this);
			}
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000F6438 File Offset: 0x000F4638
		[CompilerGenerated]
		internal static void <ToXml>g__AddChoice|30_0(string globalChoiceName, ref RuleBook.<>c__DisplayClass30_0 A_1)
		{
			string[] choiceNamesBuffer = A_1.choiceNamesBuffer;
			int choiceNamesCount = A_1.choiceNamesCount;
			A_1.choiceNamesCount = choiceNamesCount + 1;
			choiceNamesBuffer[choiceNamesCount] = globalChoiceName;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000F6460 File Offset: 0x000F4660
		[CompilerGenerated]
		internal static void <WriteBase64ToStringBuilder>g__PushRuleChoiceGlobalIndex|32_0(int id, ref RuleBook.<>c__DisplayClass32_0 A_1)
		{
			for (int i = 0; i < A_1.bytesPerRuleChoiceGlobalIndex; i++)
			{
				int num = (A_1.bytesPerRuleChoiceGlobalIndex - 1 - i) * 8;
				RuleBook.<WriteBase64ToStringBuilder>g__PushByte|32_1((byte)(id >> num), ref A_1);
			}
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000F6498 File Offset: 0x000F4698
		[CompilerGenerated]
		internal static void <WriteBase64ToStringBuilder>g__PushByte|32_1(byte value, ref RuleBook.<>c__DisplayClass32_0 A_1)
		{
			byte[] inputBytes = A_1.inputBytes;
			int inputWriteIndex = A_1.inputWriteIndex;
			A_1.inputWriteIndex = inputWriteIndex + 1;
			inputBytes[inputWriteIndex] = value;
			if (A_1.inputWriteIndex == 3)
			{
				RuleBook.<WriteBase64ToStringBuilder>g__PushCurrentInputToDest|32_2(ref A_1);
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000F64CD File Offset: 0x000F46CD
		[CompilerGenerated]
		internal static void <WriteBase64ToStringBuilder>g__PushCurrentInputToDest|32_2(ref RuleBook.<>c__DisplayClass32_0 A_0)
		{
			if (A_0.inputWriteIndex == 0)
			{
				return;
			}
			System.Convert.ToBase64CharArray(A_0.inputBytes, 0, A_0.inputWriteIndex, A_0.outputChars, 0);
			A_0.dest.Append(A_0.outputChars);
			A_0.inputWriteIndex = 0;
		}

		// Token: 0x04003B23 RID: 15139
		private readonly byte[] ruleValues = new byte[RuleCatalog.ruleCount];

		// Token: 0x04003B24 RID: 15140
		protected static readonly RuleDef startingMoneyRule = RuleCatalog.FindRuleDef("Misc.StartingMoney");

		// Token: 0x04003B25 RID: 15141
		protected static readonly RuleDef stageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");

		// Token: 0x04003B26 RID: 15142
		protected static readonly RuleDef keepMoneyBetweenStagesRule = RuleCatalog.FindRuleDef("Misc.KeepMoneyBetweenStages");

		// Token: 0x04003B27 RID: 15143
		private static byte[] defaultValues;

		// Token: 0x02000A14 RID: 2580
		public struct ChoicesEnumerable : IEnumerable<RuleChoiceDef>, IEnumerable
		{
			// Token: 0x06003B8E RID: 15246 RVA: 0x000F650B File Offset: 0x000F470B
			public ChoicesEnumerable(RuleBook target)
			{
				this.target = target;
			}

			// Token: 0x06003B8F RID: 15247 RVA: 0x000F6514 File Offset: 0x000F4714
			public RuleBook.ChoicesEnumerable.Enumerator GetEnumerator()
			{
				return new RuleBook.ChoicesEnumerable.Enumerator(this.target.ruleValues);
			}

			// Token: 0x06003B90 RID: 15248 RVA: 0x000F6526 File Offset: 0x000F4726
			IEnumerator<RuleChoiceDef> IEnumerable<RuleChoiceDef>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06003B91 RID: 15249 RVA: 0x000F6526 File Offset: 0x000F4726
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003B28 RID: 15144
			public readonly RuleBook target;

			// Token: 0x02000A15 RID: 2581
			public struct Enumerator : IEnumerator<RuleChoiceDef>, IEnumerator, IDisposable
			{
				// Token: 0x06003B92 RID: 15250 RVA: 0x000F6533 File Offset: 0x000F4733
				public Enumerator(byte[] localChoiceIndicesArray)
				{
					this.ruleIndex = -1;
					this.localChoiceIndicesArray = localChoiceIndicesArray;
				}

				// Token: 0x06003B93 RID: 15251 RVA: 0x000F6543 File Offset: 0x000F4743
				public bool MoveNext()
				{
					this.ruleIndex++;
					return this.ruleIndex < this.localChoiceIndicesArray.Length;
				}

				// Token: 0x06003B94 RID: 15252 RVA: 0x000F6563 File Offset: 0x000F4763
				public void Reset()
				{
					this.ruleIndex = -1;
				}

				// Token: 0x170005A3 RID: 1443
				// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000F656C File Offset: 0x000F476C
				public RuleChoiceDef Current
				{
					get
					{
						RuleDef ruleDef = RuleCatalog.GetRuleDef(this.ruleIndex);
						if (ruleDef == null)
						{
							return null;
						}
						return ruleDef.choices[(int)this.localChoiceIndicesArray[this.ruleIndex]];
					}
				}

				// Token: 0x170005A4 RID: 1444
				// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000F6596 File Offset: 0x000F4796
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06003B97 RID: 15255 RVA: 0x000026ED File Offset: 0x000008ED
				public void Dispose()
				{
				}

				// Token: 0x04003B29 RID: 15145
				private int ruleIndex;

				// Token: 0x04003B2A RID: 15146
				private readonly byte[] localChoiceIndicesArray;
			}
		}
	}
}
