using System;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C44 RID: 3140
	public static class NetworkExtensions
	{
		// Token: 0x06004709 RID: 18185 RVA: 0x0012586D File Offset: 0x00123A6D
		public static void Write(this NetworkWriter writer, in NetworkDateTime networkDateTime)
		{
			NetworkDateTime.Serialize(networkDateTime, writer);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00125878 File Offset: 0x00123A78
		public static NetworkDateTime ReadNetworkDateTime(this NetworkReader reader)
		{
			NetworkDateTime result;
			NetworkDateTime.Deserialize(out result, reader);
			return result;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00125890 File Offset: 0x00123A90
		public static void WriteNetworkGuid(this NetworkWriter networkWriter, in NetworkGuid guid)
		{
			NetworkGuid networkGuid = guid;
			networkGuid.Serialize(networkWriter);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x001258AC File Offset: 0x00123AAC
		public static void WriteGuid(this NetworkWriter networkWriter, in Guid guid)
		{
			NetworkGuid networkGuid = (NetworkGuid)guid;
			networkWriter.WriteNetworkGuid(networkGuid);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x001258D0 File Offset: 0x00123AD0
		public static NetworkGuid ReadNetworkGuid(this NetworkReader networkReader)
		{
			NetworkGuid result = default(NetworkGuid);
			result.Deserialize(networkReader);
			return result;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x001258EE File Offset: 0x00123AEE
		public static Guid ReadGuid(this NetworkReader networkReader)
		{
			return (Guid)networkReader.ReadNetworkGuid();
		}
	}
}
