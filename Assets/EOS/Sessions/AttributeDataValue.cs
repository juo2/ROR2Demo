using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B6 RID: 182
	public class AttributeDataValue : ISettable
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000071F0 File Offset: 0x000053F0
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00007213 File Offset: 0x00005413
		public long? AsInt64
		{
			get
			{
				long? result;
				Helper.TryMarshalGet<long?, AttributeType>(this.m_AsInt64, out result, this.m_ValueType, AttributeType.Int64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<long?, AttributeType>(ref this.m_AsInt64, value, ref this.m_ValueType, AttributeType.Int64, null);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000722C File Offset: 0x0000542C
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0000724F File Offset: 0x0000544F
		public double? AsDouble
		{
			get
			{
				double? result;
				Helper.TryMarshalGet<double?, AttributeType>(this.m_AsDouble, out result, this.m_ValueType, AttributeType.Double);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<double?, AttributeType>(ref this.m_AsDouble, value, ref this.m_ValueType, AttributeType.Double, null);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00007268 File Offset: 0x00005468
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000728B File Offset: 0x0000548B
		public bool? AsBool
		{
			get
			{
				bool? result;
				Helper.TryMarshalGet<bool?, AttributeType>(this.m_AsBool, out result, this.m_ValueType, AttributeType.Boolean);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<bool?, AttributeType>(ref this.m_AsBool, value, ref this.m_ValueType, AttributeType.Boolean, null);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000072A4 File Offset: 0x000054A4
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x000072C7 File Offset: 0x000054C7
		public string AsUtf8
		{
			get
			{
				string result;
				Helper.TryMarshalGet<string, AttributeType>(this.m_AsUtf8, out result, this.m_ValueType, AttributeType.String);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<string, AttributeType>(ref this.m_AsUtf8, value, ref this.m_ValueType, AttributeType.String, null);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000072DE File Offset: 0x000054DE
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x000072E6 File Offset: 0x000054E6
		public AttributeType ValueType
		{
			get
			{
				return this.m_ValueType;
			}
			private set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000072EF File Offset: 0x000054EF
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00007302 File Offset: 0x00005502
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00007315 File Offset: 0x00005515
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00007328 File Offset: 0x00005528
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00007338 File Offset: 0x00005538
		internal void Set(AttributeDataValueInternal? other)
		{
			if (other != null)
			{
				this.AsInt64 = other.Value.AsInt64;
				this.AsDouble = other.Value.AsDouble;
				this.AsBool = other.Value.AsBool;
				this.AsUtf8 = other.Value.AsUtf8;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000073A2 File Offset: 0x000055A2
		public void Set(object other)
		{
			this.Set(other as AttributeDataValueInternal?);
		}

		// Token: 0x0400030E RID: 782
		private long? m_AsInt64;

		// Token: 0x0400030F RID: 783
		private double? m_AsDouble;

		// Token: 0x04000310 RID: 784
		private bool? m_AsBool;

		// Token: 0x04000311 RID: 785
		private string m_AsUtf8;

		// Token: 0x04000312 RID: 786
		private AttributeType m_ValueType;
	}
}
