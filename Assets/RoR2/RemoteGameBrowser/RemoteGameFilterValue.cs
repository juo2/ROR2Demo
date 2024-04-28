using System;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AE1 RID: 2785
	public struct RemoteGameFilterValue
	{
		// Token: 0x06004001 RID: 16385 RVA: 0x00108D88 File Offset: 0x00106F88
		public RemoteGameFilterValue(bool boolValue)
		{
			this.valueType = RemoteGameFilterValue.ValueType.Bool;
			this.intValue = (boolValue ? 1 : 0);
			this.stringValue = null;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x00108DA5 File Offset: 0x00106FA5
		public RemoteGameFilterValue(int intValue)
		{
			this.valueType = RemoteGameFilterValue.ValueType.Int;
			this.intValue = intValue;
			this.stringValue = null;
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x00108DBC File Offset: 0x00106FBC
		public RemoteGameFilterValue(string stringValue)
		{
			this.valueType = RemoteGameFilterValue.ValueType.String;
			this.intValue = 0;
			this.stringValue = stringValue;
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x00108DD3 File Offset: 0x00106FD3
		public static implicit operator RemoteGameFilterValue(bool boolValue)
		{
			return new RemoteGameFilterValue(boolValue);
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x00108DDB File Offset: 0x00106FDB
		public static implicit operator RemoteGameFilterValue(int intValue)
		{
			return new RemoteGameFilterValue(intValue);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x00108DE3 File Offset: 0x00106FE3
		public static implicit operator RemoteGameFilterValue(string stringValue)
		{
			return new RemoteGameFilterValue(stringValue);
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x00108DEB File Offset: 0x00106FEB
		public bool boolValue
		{
			get
			{
				return this.intValue != 0;
			}
		}

		// Token: 0x04003E57 RID: 15959
		public readonly RemoteGameFilterValue.ValueType valueType;

		// Token: 0x04003E58 RID: 15960
		public readonly int intValue;

		// Token: 0x04003E59 RID: 15961
		public readonly string stringValue;

		// Token: 0x02000AE2 RID: 2786
		public enum ValueType
		{
			// Token: 0x04003E5B RID: 15963
			Bool,
			// Token: 0x04003E5C RID: 15964
			Int,
			// Token: 0x04003E5D RID: 15965
			String
		}
	}
}
