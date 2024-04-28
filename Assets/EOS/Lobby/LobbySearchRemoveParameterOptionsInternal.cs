using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000368 RID: 872
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchRemoveParameterOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000661 RID: 1633
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x000176CC File Offset: 0x000158CC
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x000176DB File Offset: 0x000158DB
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x000176E4 File Offset: 0x000158E4
		public void Set(LobbySearchRemoveParameterOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.ComparisonOp = other.ComparisonOp;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00017708 File Offset: 0x00015908
		public void Set(object other)
		{
			this.Set(other as LobbySearchRemoveParameterOptions);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00017716 File Offset: 0x00015916
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x04000A60 RID: 2656
		private int m_ApiVersion;

		// Token: 0x04000A61 RID: 2657
		private IntPtr m_Key;

		// Token: 0x04000A62 RID: 2658
		private ComparisonOp m_ComparisonOp;
	}
}
