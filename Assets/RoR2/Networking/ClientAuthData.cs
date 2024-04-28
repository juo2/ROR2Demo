using System;
using Unity;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C4A RID: 3146
	public class ClientAuthData : MessageBase
	{
		// Token: 0x06004738 RID: 18232 RVA: 0x00126014 File Offset: 0x00124214
		public override void Serialize(NetworkWriter writer)
		{
			GeneratedNetworkCode._WriteCSteamID_None(writer, this.steamId);
			writer.WriteBytesFull(this.authTicket);
			GeneratedNetworkCode._WriteArrayString_None(writer, this.entitlements);
			writer.Write(this.password);
			writer.Write(this.version);
			writer.Write(this.modHash);
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0012606C File Offset: 0x0012426C
		public override void Deserialize(NetworkReader reader)
		{
			this.steamId = GeneratedNetworkCode._ReadCSteamID_None(reader);
			this.authTicket = reader.ReadBytesAndSize();
			this.entitlements = GeneratedNetworkCode._ReadArrayString_None(reader);
			this.password = reader.ReadString();
			this.version = reader.ReadString();
			this.modHash = reader.ReadString();
		}

		// Token: 0x040044D3 RID: 17619
		public CSteamID steamId;

		// Token: 0x040044D4 RID: 17620
		public byte[] authTicket;

		// Token: 0x040044D5 RID: 17621
		public string[] entitlements;

		// Token: 0x040044D6 RID: 17622
		public string password;

		// Token: 0x040044D7 RID: 17623
		public string version;

		// Token: 0x040044D8 RID: 17624
		public string modHash;
	}
}
