using System;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C3B RID: 3131
	public class EOSNetworkClient : NetworkClient
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x00124DB2 File Offset: 0x00122FB2
		public EOSNetworkConnection eosConnection
		{
			get
			{
				return (EOSNetworkConnection)base.connection;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x00124DBF File Offset: 0x00122FBF
		public string status
		{
			get
			{
				return this.m_AsyncConnect.ToString();
			}
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00124DD2 File Offset: 0x00122FD2
		public void Connect()
		{
			base.Connect("localhost", 0);
			this.m_AsyncConnect = NetworkClient.ConnectState.Connected;
			base.connection.ForceInitialize(base.hostTopology);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00124DF8 File Offset: 0x00122FF8
		public EOSNetworkClient(NetworkConnection conn) : base(conn)
		{
			base.SetNetworkConnectionClass<EOSNetworkConnection>();
		}
	}
}
