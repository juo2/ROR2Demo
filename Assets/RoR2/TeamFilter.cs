using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B6 RID: 2230
	public class TeamFilter : NetworkBehaviour
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x000D2679 File Offset: 0x000D0879
		public void Awake()
		{
			if (NetworkServer.active)
			{
				this.teamIndex = this.defaultTeam;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000D268E File Offset: 0x000D088E
		// (set) Token: 0x06003199 RID: 12697 RVA: 0x000D2697 File Offset: 0x000D0897
		public TeamIndex teamIndex
		{
			get
			{
				return (TeamIndex)this.teamIndexInternal;
			}
			set
			{
				this.NetworkteamIndexInternal = (int)value;
				this.defaultTeam = value;
			}
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000D26A8 File Offset: 0x000D08A8
		[Server]
		public void SetTeamServer(string teamName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeamFilter::SetTeamServer(System.String)' called on client");
				return;
			}
			TeamIndex teamIndex;
			if (Enum.TryParse<TeamIndex>(teamName, out teamIndex))
			{
				this.teamIndex = teamIndex;
			}
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000D26EC File Offset: 0x000D08EC
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000D26FF File Offset: 0x000D08FF
		public int NetworkteamIndexInternal
		{
			get
			{
				return this.teamIndexInternal;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.teamIndexInternal, 1U);
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000D2714 File Offset: 0x000D0914
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.teamIndexInternal);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.teamIndexInternal);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000D2780 File Offset: 0x000D0980
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.teamIndexInternal = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.teamIndexInternal = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003318 RID: 13080
		[SyncVar]
		private int teamIndexInternal;

		// Token: 0x04003319 RID: 13081
		[SerializeField]
		private TeamIndex defaultTeam = TeamIndex.None;
	}
}
