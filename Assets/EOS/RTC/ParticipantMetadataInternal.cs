using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E7 RID: 487
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantMetadataInternal : ISettable, IDisposable
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x0000DD14 File Offset: 0x0000BF14
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

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0000DD24 File Offset: 0x0000BF24
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public string Value
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Value, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Value, value);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0000DD4F File Offset: 0x0000BF4F
		public void Set(ParticipantMetadata other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.Value = other.Value;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public void Set(object other)
		{
			this.Set(other as ParticipantMetadata);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0000DD81 File Offset: 0x0000BF81
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
			Helper.TryMarshalDispose(ref this.m_Value);
		}

		// Token: 0x0400060F RID: 1551
		private int m_ApiVersion;

		// Token: 0x04000610 RID: 1552
		private IntPtr m_Key;

		// Token: 0x04000611 RID: 1553
		private IntPtr m_Value;
	}
}
