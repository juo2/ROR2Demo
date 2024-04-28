using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : ISettable, IDisposable
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000714C File Offset: 0x0000534C
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00007168 File Offset: 0x00005368
		public string Key
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00007178 File Offset: 0x00005378
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00007194 File Offset: 0x00005394
		public AttributeDataValue Value
		{
			get
			{
				AttributeDataValue result;
				Helper.TryMarshalGet<AttributeDataValueInternal, AttributeDataValue>(this.m_Value, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataValueInternal>(ref this.m_Value, value);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000071A3 File Offset: 0x000053A3
		public void Set(AttributeData other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.Value = other.Value;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000071C7 File Offset: 0x000053C7
		public void Set(object other)
		{
			this.Set(other as AttributeData);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000071D5 File Offset: 0x000053D5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
			Helper.TryMarshalDispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x0400030B RID: 779
		private int m_ApiVersion;

		// Token: 0x0400030C RID: 780
		private IntPtr m_Key;

		// Token: 0x0400030D RID: 781
		private AttributeDataValueInternal m_Value;
	}
}
