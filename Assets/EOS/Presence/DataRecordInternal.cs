using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020C RID: 524
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DataRecordInternal : ISettable, IDisposable
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0000EACC File Offset: 0x0000CCCC
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0000EADC File Offset: 0x0000CCDC
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
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

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0000EB07 File Offset: 0x0000CD07
		public void Set(DataRecord other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.Value = other.Value;
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0000EB2B File Offset: 0x0000CD2B
		public void Set(object other)
		{
			this.Set(other as DataRecord);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0000EB39 File Offset: 0x0000CD39
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
			Helper.TryMarshalDispose(ref this.m_Value);
		}

		// Token: 0x0400066B RID: 1643
		private int m_ApiVersion;

		// Token: 0x0400066C RID: 1644
		private IntPtr m_Key;

		// Token: 0x0400066D RID: 1645
		private IntPtr m_Value;
	}
}
