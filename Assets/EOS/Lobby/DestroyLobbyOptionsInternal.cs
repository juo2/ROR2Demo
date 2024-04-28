using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000311 RID: 785
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005BE RID: 1470
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x00014E02 File Offset: 0x00013002
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00014E11 File Offset: 0x00013011
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00014E20 File Offset: 0x00013020
		public void Set(DestroyLobbyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.LobbyId = other.LobbyId;
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00014E44 File Offset: 0x00013044
		public void Set(object other)
		{
			this.Set(other as DestroyLobbyOptions);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00014E52 File Offset: 0x00013052
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_LobbyId);
		}

		// Token: 0x04000957 RID: 2391
		private int m_ApiVersion;

		// Token: 0x04000958 RID: 2392
		private IntPtr m_LocalUserId;

		// Token: 0x04000959 RID: 2393
		private IntPtr m_LobbyId;
	}
}
