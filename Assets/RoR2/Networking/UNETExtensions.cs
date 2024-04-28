using System;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C57 RID: 3159
	public static class UNETExtensions
	{
		// Token: 0x06004786 RID: 18310 RVA: 0x001273D4 File Offset: 0x001255D4
		public static void ForceInitialize(this NetworkConnection conn, HostTopology hostTopology)
		{
			int num = 0;
			conn.Initialize("localhost", num, num, hostTopology);
		}
	}
}
