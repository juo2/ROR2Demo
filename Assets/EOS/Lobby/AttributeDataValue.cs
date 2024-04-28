using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000300 RID: 768
	public class AttributeDataValue : ISettable
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0001456C File Offset: 0x0001276C
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0001458F File Offset: 0x0001278F
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

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x000145A8 File Offset: 0x000127A8
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x000145CB File Offset: 0x000127CB
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

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x000145E4 File Offset: 0x000127E4
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x00014607 File Offset: 0x00012807
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

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00014620 File Offset: 0x00012820
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00014643 File Offset: 0x00012843
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

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0001465A File Offset: 0x0001285A
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00014662 File Offset: 0x00012862
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

		// Token: 0x06001328 RID: 4904 RVA: 0x0001466B File Offset: 0x0001286B
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0001467E File Offset: 0x0001287E
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00014691 File Offset: 0x00012891
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000146A4 File Offset: 0x000128A4
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000146B4 File Offset: 0x000128B4
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

		// Token: 0x0600132D RID: 4909 RVA: 0x0001471E File Offset: 0x0001291E
		public void Set(object other)
		{
			this.Set(other as AttributeDataValueInternal?);
		}

		// Token: 0x0400091C RID: 2332
		private long? m_AsInt64;

		// Token: 0x0400091D RID: 2333
		private double? m_AsDouble;

		// Token: 0x0400091E RID: 2334
		private bool? m_AsBool;

		// Token: 0x0400091F RID: 2335
		private string m_AsUtf8;

		// Token: 0x04000920 RID: 2336
		private AttributeType m_ValueType;
	}
}
