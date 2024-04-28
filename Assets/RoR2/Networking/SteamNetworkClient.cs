using System;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C55 RID: 3157
	public class SteamNetworkClient : NetworkClient
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x0012709A File Offset: 0x0012529A
		public SteamNetworkConnection steamConnection
		{
			get
			{
				return (SteamNetworkConnection)base.connection;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x00124DBF File Offset: 0x00122FBF
		public string status
		{
			get
			{
				return this.m_AsyncConnect.ToString();
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00124DD2 File Offset: 0x00122FD2
		public void Connect()
		{
			base.Connect("localhost", 0);
			this.m_AsyncConnect = NetworkClient.ConnectState.Connected;
			base.connection.ForceInitialize(base.hostTopology);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x001270A7 File Offset: 0x001252A7
		public SteamNetworkClient(NetworkConnection conn) : base(conn)
		{
			base.SetNetworkConnectionClass<SteamNetworkConnection>();
		}
	}
}
