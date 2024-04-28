using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001F1 RID: 497
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddProgressionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700038F RID: 911
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x0000E42F File Offset: 0x0000C62F
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0000E438 File Offset: 0x0000C638
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x17000391 RID: 913
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0000E447 File Offset: 0x0000C647
		public string Value
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Value, value);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0000E456 File Offset: 0x0000C656
		public void Set(AddProgressionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.SnapshotId;
				this.Key = other.Key;
				this.Value = other.Value;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0000E486 File Offset: 0x0000C686
		public void Set(object other)
		{
			this.Set(other as AddProgressionOptions);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0000E494 File Offset: 0x0000C694
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
			Helper.TryMarshalDispose(ref this.m_Value);
		}

		// Token: 0x0400063D RID: 1597
		private int m_ApiVersion;

		// Token: 0x0400063E RID: 1598
		private uint m_SnapshotId;

		// Token: 0x0400063F RID: 1599
		private IntPtr m_Key;

		// Token: 0x04000640 RID: 1600
		private IntPtr m_Value;
	}
}
