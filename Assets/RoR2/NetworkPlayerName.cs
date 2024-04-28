using System;
using Facepunch.Steamworks;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000976 RID: 2422
	public struct NetworkPlayerName
	{
		// Token: 0x060036FD RID: 14077 RVA: 0x000E7BD3 File Offset: 0x000E5DD3
		public void Deserialize(NetworkReader reader)
		{
			if (reader.ReadBoolean())
			{
				this.steamId = CSteamID.nil;
				this.nameOverride = reader.ReadString();
				return;
			}
			this.steamId = new CSteamID(reader.ReadUInt64());
			this.nameOverride = null;
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000E7C10 File Offset: 0x000E5E10
		public void Serialize(NetworkWriter writer)
		{
			bool flag = this.nameOverride != null;
			writer.Write(flag);
			if (flag)
			{
				writer.Write(this.nameOverride);
				return;
			}
			writer.Write((ulong)this.steamId.value);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000E7C54 File Offset: 0x000E5E54
		public string GetResolvedName()
		{
			if (!string.IsNullOrEmpty(this.nameOverride))
			{
				return (PlatformSystems.lobbyManager as EOSLobbyManager).GetUserDisplayNameFromProductIdString(this.nameOverride);
			}
			if (PlatformSystems.ShouldUseEpicOnlineSystems)
			{
				LobbyManager lobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
				UserID user = new UserID(this.steamId);
				return lobbyManager.GetUserDisplayName(user);
			}
			Client instance = Client.Instance;
			if (instance != null)
			{
				return instance.Friends.GetName(this.steamId.steamValue);
			}
			return "???";
		}

		// Token: 0x0400374E RID: 14158
		public CSteamID steamId;

		// Token: 0x0400374F RID: 14159
		public string nameOverride;
	}
}
