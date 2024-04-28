using System;
using System.Net;

namespace Facepunch.Steamworks
{
	// Token: 0x0200017D RID: 381
	public class ServerInit
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0003A030 File Offset: 0x00038230
		public ServerInit(string modDir, string gameDesc)
		{
			this.ModDir = modDir;
			this.GameDescription = gameDesc;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003A09A File Offset: 0x0003829A
		public ServerInit RandomSteamPort()
		{
			this.SteamPort = (ushort)new Random().Next(10000, 60000);
			return this;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0003A0B8 File Offset: 0x000382B8
		public ServerInit QueryShareGamePort()
		{
			this.QueryPort = ushort.MaxValue;
			return this;
		}

		// Token: 0x04000881 RID: 2177
		public IPAddress IpAddress;

		// Token: 0x04000882 RID: 2178
		public ushort SteamPort;

		// Token: 0x04000883 RID: 2179
		public ushort GamePort = 27015;

		// Token: 0x04000884 RID: 2180
		public ushort QueryPort = 27016;

		// Token: 0x04000885 RID: 2181
		public bool Secure = true;

		// Token: 0x04000886 RID: 2182
		public string VersionString = "2.0.0.0";

		// Token: 0x04000887 RID: 2183
		public string ModDir = "unset";

		// Token: 0x04000888 RID: 2184
		public string GameDescription = "unset";

		// Token: 0x04000889 RID: 2185
		public string GameData = "";
	}
}
