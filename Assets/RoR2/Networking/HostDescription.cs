using System;
using System.Text;
using Epic.OnlineServices;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C6C RID: 3180
	public struct HostDescription : IEquatable<HostDescription>
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x0012AA8C File Offset: 0x00128C8C
		public bool isRemote
		{
			get
			{
				return this.hostType != HostDescription.HostType.None && this.hostType != HostDescription.HostType.Self;
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0012AAA4 File Offset: 0x00128CA4
		public HostDescription(UserID id, HostDescription.HostType type)
		{
			this = default(HostDescription);
			this.hostType = type;
			this.userID = id;
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0012AABB File Offset: 0x00128CBB
		public HostDescription(AddressPortPair addressPortPair)
		{
			this = default(HostDescription);
			this.hostType = HostDescription.HostType.IPv4;
			this.addressPortPair = addressPortPair;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x0012AAD2 File Offset: 0x00128CD2
		public HostDescription(HostDescription.HostingParameters hostingParameters)
		{
			this = default(HostDescription);
			this.hostType = HostDescription.HostType.Self;
			this.hostingParameters = hostingParameters;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0012AAEC File Offset: 0x00128CEC
		public bool DescribesCurrentHost()
		{
			switch (this.hostType)
			{
			case HostDescription.HostType.None:
				return !NetworkManagerSystem.singleton.isNetworkActive;
			case HostDescription.HostType.Self:
				return NetworkServer.active && this.hostingParameters.listen != NetworkServer.dontListen;
			case HostDescription.HostType.Steam:
			{
				NetworkClient client = NetworkManagerSystem.singleton.client;
				SteamNetworkConnection steamNetworkConnection;
				return (steamNetworkConnection = (((client != null) ? client.connection : null) as SteamNetworkConnection)) != null && steamNetworkConnection.steamId == this.userID.CID;
			}
			case HostDescription.HostType.IPv4:
			{
				NetworkClient client2 = NetworkManagerSystem.singleton.client;
				NetworkConnection networkConnection;
				return (networkConnection = ((client2 != null) ? client2.connection : null)) != null && networkConnection.address == this.addressPortPair.address;
			}
			case HostDescription.HostType.EOS:
			{
				NetworkClient client3 = NetworkManagerSystem.singleton.client;
				EOSNetworkConnection eosnetworkConnection;
				if ((eosnetworkConnection = (((client3 != null) ? client3.connection : null) as EOSNetworkConnection)) != null)
				{
					Handle remoteUserID = eosnetworkConnection.RemoteUserID;
					CSteamID cid = this.userID.CID;
					if (remoteUserID == cid.egsValue)
					{
						return true;
					}
				}
				return false;
			}
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0012AC0D File Offset: 0x00128E0D
		private HostDescription(object o)
		{
			this = default(HostDescription);
			this.hostType = HostDescription.HostType.None;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x0012AC20 File Offset: 0x00128E20
		public bool Equals(HostDescription other)
		{
			return this.hostType == other.hostType && this.userID.Equals(other.userID) && this.addressPortPair.Equals(other.addressPortPair) && this.hostingParameters.Equals(other.hostingParameters);
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x0012AC88 File Offset: 0x00128E88
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is HostDescription)
			{
				HostDescription other = (HostDescription)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0012ACB4 File Offset: 0x00128EB4
		public override int GetHashCode()
		{
			int num = (int)(this.hostType * (HostDescription.HostType)397);
			CSteamID cid = this.userID.CID;
			return ((num ^ cid.GetHashCode()) * 397 ^ this.addressPortPair.GetHashCode()) * 397 ^ this.hostingParameters.GetHashCode();
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0012AD20 File Offset: 0x00128F20
		public override string ToString()
		{
			HostDescription.sharedStringBuilder.Clear();
			HostDescription.sharedStringBuilder.Append("{ hostType=").Append(this.hostType);
			switch (this.hostType)
			{
			case HostDescription.HostType.None:
				goto IL_138;
			case HostDescription.HostType.Self:
				HostDescription.sharedStringBuilder.Append(" listen=").Append(this.hostingParameters.listen);
				HostDescription.sharedStringBuilder.Append(" maxPlayers=").Append(this.hostingParameters.maxPlayers);
				goto IL_138;
			case HostDescription.HostType.Steam:
				HostDescription.sharedStringBuilder.Append(" steamId=").Append(this.userID.CID);
				goto IL_138;
			case HostDescription.HostType.IPv4:
				HostDescription.sharedStringBuilder.Append(" address=").Append(this.addressPortPair.address);
				HostDescription.sharedStringBuilder.Append(" port=").Append(this.addressPortPair.port);
				goto IL_138;
			case HostDescription.HostType.EOS:
				HostDescription.sharedStringBuilder.Append(" steamId=").Append(this.userID.ToString());
				goto IL_138;
			}
			throw new ArgumentOutOfRangeException();
			IL_138:
			HostDescription.sharedStringBuilder.Append(" }");
			return HostDescription.sharedStringBuilder.ToString();
		}

		// Token: 0x04004573 RID: 17779
		public readonly HostDescription.HostType hostType;

		// Token: 0x04004574 RID: 17780
		public readonly UserID userID;

		// Token: 0x04004575 RID: 17781
		public readonly AddressPortPair addressPortPair;

		// Token: 0x04004576 RID: 17782
		public readonly HostDescription.HostingParameters hostingParameters;

		// Token: 0x04004577 RID: 17783
		public static readonly HostDescription none = new HostDescription(null);

		// Token: 0x04004578 RID: 17784
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x02000C6D RID: 3181
		public enum HostType
		{
			// Token: 0x0400457A RID: 17786
			None,
			// Token: 0x0400457B RID: 17787
			Self,
			// Token: 0x0400457C RID: 17788
			Steam,
			// Token: 0x0400457D RID: 17789
			IPv4,
			// Token: 0x0400457E RID: 17790
			Pia,
			// Token: 0x0400457F RID: 17791
			PS4,
			// Token: 0x04004580 RID: 17792
			EOS
		}

		// Token: 0x02000C6E RID: 3182
		public struct HostingParameters : IEquatable<HostDescription.HostingParameters>
		{
			// Token: 0x060048A3 RID: 18595 RVA: 0x0012AE96 File Offset: 0x00129096
			public bool Equals(HostDescription.HostingParameters other)
			{
				return this.listen == other.listen && this.maxPlayers == other.maxPlayers;
			}

			// Token: 0x060048A4 RID: 18596 RVA: 0x0012AEB8 File Offset: 0x001290B8
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is HostDescription.HostingParameters)
				{
					HostDescription.HostingParameters other = (HostDescription.HostingParameters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x060048A5 RID: 18597 RVA: 0x0012AEE4 File Offset: 0x001290E4
			public override int GetHashCode()
			{
				return this.listen.GetHashCode() * 397 ^ this.maxPlayers;
			}

			// Token: 0x04004581 RID: 17793
			public bool listen;

			// Token: 0x04004582 RID: 17794
			public int maxPlayers;
		}
	}
}
