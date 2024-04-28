using System;

namespace Facepunch.Steamworks
{
	// Token: 0x02000174 RID: 372
	public class Utils : IDisposable
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x00038264 File Offset: 0x00036464
		internal Utils(Client c)
		{
			this.client = c;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00038273 File Offset: 0x00036473
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003827C File Offset: 0x0003647C
		public uint GetServerRealTime()
		{
			return this.client.native.utils.GetServerRealTime();
		}

		// Token: 0x04000843 RID: 2115
		internal Client client;
	}
}
