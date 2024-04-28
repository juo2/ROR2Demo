using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005AE RID: 1454
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddExternalIntegrityCatalogOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B14 RID: 2836
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x0002553B File Offset: 0x0002373B
		public string PathToBinFile
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PathToBinFile, value);
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0002554A File Offset: 0x0002374A
		public void Set(AddExternalIntegrityCatalogOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PathToBinFile = other.PathToBinFile;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00025562 File Offset: 0x00023762
		public void Set(object other)
		{
			this.Set(other as AddExternalIntegrityCatalogOptions);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00025570 File Offset: 0x00023770
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PathToBinFile);
		}

		// Token: 0x040010AB RID: 4267
		private int m_ApiVersion;

		// Token: 0x040010AC RID: 4268
		private IntPtr m_PathToBinFile;
	}
}
