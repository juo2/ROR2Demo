using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine.Networking;

namespace RoR2.Stats
{
	// Token: 0x02000ABC RID: 2748
	public struct StatField : IComparable<StatField>
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x001046F4 File Offset: 0x001028F4
		public string name
		{
			get
			{
				return this.statDef.name;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x00104701 File Offset: 0x00102901
		public StatRecordType recordType
		{
			get
			{
				return this.statDef.recordType;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x0010470E File Offset: 0x0010290E
		public StatDataType dataType
		{
			get
			{
				return this.statDef.dataType;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x0010471B File Offset: 0x0010291B
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x00104728 File Offset: 0x00102928
		private ulong ulongValue
		{
			get
			{
				return this.value.ulongValue;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x00104736 File Offset: 0x00102936
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x00104743 File Offset: 0x00102943
		private double doubleValue
		{
			get
			{
				return this.value.doubleValue;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x00104754 File Offset: 0x00102954
		public override string ToString()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				return TextSerialization.ToStringInvariant(this.value.ulongValue);
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			return TextSerialization.ToStringInvariant(this.value.doubleValue);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00104798 File Offset: 0x00102998
		public string ToLocalNumeric()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				return TextSerialization.ToStringNumeric(this.value.ulongValue);
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			return TextSerialization.ToStringNumeric(this.value.doubleValue);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00062756 File Offset: 0x00060956
		public ulong CalculatePointValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x001047DC File Offset: 0x001029DC
		[Pure]
		public static StatField GetDelta(ref StatField newerValue, ref StatField olderValue)
		{
			StatField result = new StatField
			{
				statDef = newerValue.statDef
			};
			StatDataType dataType = newerValue.dataType;
			if (dataType != StatDataType.ULong)
			{
				if (dataType == StatDataType.Double)
				{
					switch (newerValue.recordType)
					{
					case StatRecordType.Sum:
						result.doubleValue = newerValue.doubleValue - olderValue.doubleValue;
						break;
					case StatRecordType.Max:
						result.doubleValue = Math.Max(newerValue.doubleValue, olderValue.doubleValue);
						break;
					case StatRecordType.Newest:
						result.doubleValue = newerValue.doubleValue;
						break;
					}
				}
			}
			else
			{
				switch (newerValue.recordType)
				{
				case StatRecordType.Sum:
					result.ulongValue = newerValue.ulongValue - olderValue.ulongValue;
					break;
				case StatRecordType.Max:
					result.ulongValue = Math.Max(newerValue.ulongValue, olderValue.ulongValue);
					break;
				case StatRecordType.Newest:
					result.ulongValue = newerValue.ulongValue;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x001048CC File Offset: 0x00102ACC
		public void PushDelta(ref StatField deltaField)
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				this.PushStatValue(deltaField.ulongValue);
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.PushStatValue(deltaField.doubleValue);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00104908 File Offset: 0x00102B08
		public void Write(NetworkWriter writer)
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				writer.WritePackedUInt64(this.ulongValue);
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			writer.Write(this.doubleValue);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00104944 File Offset: 0x00102B44
		public void Read(NetworkReader reader)
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				this.ulongValue = reader.ReadPackedUInt64();
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.doubleValue = reader.ReadDouble();
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x00104980 File Offset: 0x00102B80
		private void EnforceDataType(StatDataType otherDataType)
		{
			if (this.dataType != otherDataType)
			{
				throw new InvalidOperationException(string.Format("Expected data type {0}, got data type {1}.", this.dataType, otherDataType));
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x001049AC File Offset: 0x00102BAC
		public void PushStatValue(ulong incomingValue)
		{
			this.EnforceDataType(StatDataType.ULong);
			switch (this.recordType)
			{
			case StatRecordType.Sum:
				this.ulongValue += incomingValue;
				return;
			case StatRecordType.Max:
				this.ulongValue = Math.Max(incomingValue, this.ulongValue);
				return;
			case StatRecordType.Newest:
				this.ulongValue = incomingValue;
				return;
			default:
				return;
			}
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00104A04 File Offset: 0x00102C04
		public void PushStatValue(double incomingValue)
		{
			this.EnforceDataType(StatDataType.Double);
			switch (this.recordType)
			{
			case StatRecordType.Sum:
				this.doubleValue += incomingValue;
				return;
			case StatRecordType.Max:
				this.doubleValue = Math.Max(incomingValue, this.doubleValue);
				return;
			case StatRecordType.Newest:
				this.doubleValue = incomingValue;
				return;
			default:
				return;
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00104A5C File Offset: 0x00102C5C
		public void SetFromString(string valueString)
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				ulong ulongValue;
				TextSerialization.TryParseInvariant(valueString, out ulongValue);
				this.value = ulongValue;
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			double doubleValue;
			TextSerialization.TryParseInvariant(valueString, out doubleValue);
			this.value = doubleValue;
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00104AAA File Offset: 0x00102CAA
		public ulong GetULongValue()
		{
			this.EnforceDataType(StatDataType.ULong);
			return this.ulongValue;
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00104AB9 File Offset: 0x00102CB9
		public double GetDoubleValue()
		{
			this.EnforceDataType(StatDataType.Double);
			return this.doubleValue;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00104AC8 File Offset: 0x00102CC8
		public double GetValueAsDouble()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				return this.ulongValue;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.doubleValue;
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00104AFC File Offset: 0x00102CFC
		[Obsolete]
		public decimal GetDecimalValue()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				return this.ulongValue;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (decimal)this.doubleValue;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00104B38 File Offset: 0x00102D38
		public bool IsDefault()
		{
			StatDataType dataType = this.dataType;
			if (dataType != StatDataType.ULong)
			{
				return dataType != StatDataType.Double || this.doubleValue == 0.0;
			}
			return this.ulongValue == 0UL;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00104B74 File Offset: 0x00102D74
		public void SetDefault()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				this.ulongValue = 0UL;
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new NotImplementedException();
			}
			this.doubleValue = 0.0;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x00104BB0 File Offset: 0x00102DB0
		public ulong GetPointValue(double pointValue)
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				return (ulong)(this.ulongValue * pointValue);
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			return (ulong)(this.doubleValue * pointValue);
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x00104BE9 File Offset: 0x00102DE9
		public int CompareTo(StatField other)
		{
			return this.CompareTo(other);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00104BF4 File Offset: 0x00102DF4
		public int CompareTo(in StatField other)
		{
			StatField statField = other;
			this.EnforceDataType(statField.dataType);
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				ulong ulongValue = this.ulongValue;
				statField = other;
				return ulongValue.CompareTo(statField.ulongValue);
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			double doubleValue = this.doubleValue;
			statField = other;
			return doubleValue.CompareTo(statField.doubleValue);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x00104C64 File Offset: 0x00102E64
		public void SetToMaxValue()
		{
			StatDataType dataType = this.dataType;
			if (dataType == StatDataType.ULong)
			{
				this.ulongValue = ulong.MaxValue;
				return;
			}
			if (dataType != StatDataType.Double)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.doubleValue = double.MaxValue;
		}

		// Token: 0x04003DAE RID: 15790
		public StatDef statDef;

		// Token: 0x04003DAF RID: 15791
		private StatField.ValueUnion value;

		// Token: 0x02000ABD RID: 2749
		[StructLayout(LayoutKind.Explicit)]
		private struct ValueUnion
		{
			// Token: 0x06003F36 RID: 16182 RVA: 0x00104C9F File Offset: 0x00102E9F
			public static explicit operator ulong(StatField.ValueUnion v)
			{
				return v.ulongValue;
			}

			// Token: 0x06003F37 RID: 16183 RVA: 0x00104CA7 File Offset: 0x00102EA7
			public static explicit operator double(StatField.ValueUnion v)
			{
				return v.doubleValue;
			}

			// Token: 0x06003F38 RID: 16184 RVA: 0x00104CAF File Offset: 0x00102EAF
			public static implicit operator StatField.ValueUnion(ulong ulongValue)
			{
				return new StatField.ValueUnion(ulongValue);
			}

			// Token: 0x06003F39 RID: 16185 RVA: 0x00104CB7 File Offset: 0x00102EB7
			public static implicit operator StatField.ValueUnion(double doubleValue)
			{
				return new StatField.ValueUnion(doubleValue);
			}

			// Token: 0x06003F3A RID: 16186 RVA: 0x00104CBF File Offset: 0x00102EBF
			private ValueUnion(ulong ulongValue)
			{
				this = default(StatField.ValueUnion);
				this.ulongValue = ulongValue;
			}

			// Token: 0x06003F3B RID: 16187 RVA: 0x00104CCF File Offset: 0x00102ECF
			private ValueUnion(double doubleValue)
			{
				this = default(StatField.ValueUnion);
				this.doubleValue = doubleValue;
			}

			// Token: 0x04003DB0 RID: 15792
			[FieldOffset(0)]
			public readonly ulong ulongValue;

			// Token: 0x04003DB1 RID: 15793
			[FieldOffset(0)]
			public readonly double doubleValue;
		}
	}
}
