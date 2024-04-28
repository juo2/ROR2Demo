using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000301 RID: 769
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : ISettable, IDisposable
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00014734 File Offset: 0x00012934
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x00014757 File Offset: 0x00012957
		public long? AsInt64
		{
			get
			{
				long? result;
				Helper.TryMarshalGet<long, AttributeType>(this.m_AsInt64, out result, this.m_ValueType, AttributeType.Int64);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<long, AttributeType>(ref this.m_AsInt64, value, ref this.m_ValueType, AttributeType.Int64, this);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00014778 File Offset: 0x00012978
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x0001479B File Offset: 0x0001299B
		public double? AsDouble
		{
			get
			{
				double? result;
				Helper.TryMarshalGet<double, AttributeType>(this.m_AsDouble, out result, this.m_ValueType, AttributeType.Double);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<double, AttributeType>(ref this.m_AsDouble, value, ref this.m_ValueType, AttributeType.Double, this);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000147BC File Offset: 0x000129BC
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x000147DF File Offset: 0x000129DF
		public bool? AsBool
		{
			get
			{
				bool? result;
				Helper.TryMarshalGet<AttributeType>(this.m_AsBool, out result, this.m_ValueType, AttributeType.Boolean);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AttributeType>(ref this.m_AsBool, value, ref this.m_ValueType, AttributeType.Boolean, this);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00014800 File Offset: 0x00012A00
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x00014823 File Offset: 0x00012A23
		public string AsUtf8
		{
			get
			{
				string result;
				Helper.TryMarshalGet<AttributeType>(this.m_AsUtf8, out result, this.m_ValueType, AttributeType.String);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AttributeType>(ref this.m_AsUtf8, value, ref this.m_ValueType, AttributeType.String, this);
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00014844 File Offset: 0x00012A44
		public void Set(AttributeDataValue other)
		{
			if (other != null)
			{
				this.AsInt64 = other.AsInt64;
				this.AsDouble = other.AsDouble;
				this.AsBool = other.AsBool;
				this.AsUtf8 = other.AsUtf8;
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00014879 File Offset: 0x00012A79
		public void Set(object other)
		{
			this.Set(other as AttributeDataValue);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00014887 File Offset: 0x00012A87
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x04000921 RID: 2337
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x04000922 RID: 2338
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x04000923 RID: 2339
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x04000924 RID: 2340
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x04000925 RID: 2341
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
