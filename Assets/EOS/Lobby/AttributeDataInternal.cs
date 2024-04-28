using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FF RID: 767
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : ISettable, IDisposable
	{
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x000144C8 File Offset: 0x000126C8
		// (set) Token: 0x06001318 RID: 4888 RVA: 0x000144E4 File Offset: 0x000126E4
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

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x000144F4 File Offset: 0x000126F4
		// (set) Token: 0x0600131A RID: 4890 RVA: 0x00014510 File Offset: 0x00012710
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

		// Token: 0x0600131B RID: 4891 RVA: 0x0001451F File Offset: 0x0001271F
		public void Set(AttributeData other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.Value = other.Value;
			}
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00014543 File Offset: 0x00012743
		public void Set(object other)
		{
			this.Set(other as AttributeData);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00014551 File Offset: 0x00012751
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
			Helper.TryMarshalDispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x04000919 RID: 2329
		private int m_ApiVersion;

		// Token: 0x0400091A RID: 2330
		private IntPtr m_Key;

		// Token: 0x0400091B RID: 2331
		private AttributeDataValueInternal m_Value;
	}
}
