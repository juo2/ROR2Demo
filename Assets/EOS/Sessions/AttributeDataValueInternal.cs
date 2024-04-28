using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : ISettable, IDisposable
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000073B8 File Offset: 0x000055B8
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x000073DB File Offset: 0x000055DB
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

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x000073FC File Offset: 0x000055FC
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000741F File Offset: 0x0000561F
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

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00007440 File Offset: 0x00005640
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00007463 File Offset: 0x00005663
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

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00007484 File Offset: 0x00005684
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x000074A7 File Offset: 0x000056A7
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

		// Token: 0x06000633 RID: 1587 RVA: 0x000074C8 File Offset: 0x000056C8
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

		// Token: 0x06000634 RID: 1588 RVA: 0x000074FD File Offset: 0x000056FD
		public void Set(object other)
		{
			this.Set(other as AttributeDataValue);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000750B File Offset: 0x0000570B
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x04000313 RID: 787
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x04000314 RID: 788
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x04000315 RID: 789
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x04000316 RID: 790
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x04000317 RID: 791
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
