using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031F RID: 799
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005E0 RID: 1504
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00015284 File Offset: 0x00013484
		public LobbyDetails LobbyDetailsHandle
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyDetailsHandle, value);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x00015293 File Offset: 0x00013493
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x000152A2 File Offset: 0x000134A2
		public bool PresenceEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x000152B1 File Offset: 0x000134B1
		public LocalRTCOptions LocalRTCOptions
		{
			set
			{
				Helper.TryMarshalSet<LocalRTCOptionsInternal, LocalRTCOptions>(ref this.m_LocalRTCOptions, value);
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000152C0 File Offset: 0x000134C0
		public void Set(JoinLobbyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.LobbyDetailsHandle = other.LobbyDetailsHandle;
				this.LocalUserId = other.LocalUserId;
				this.PresenceEnabled = other.PresenceEnabled;
				this.LocalRTCOptions = other.LocalRTCOptions;
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000152FC File Offset: 0x000134FC
		public void Set(object other)
		{
			this.Set(other as JoinLobbyOptions);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0001530A File Offset: 0x0001350A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyDetailsHandle);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_LocalRTCOptions);
		}

		// Token: 0x0400097C RID: 2428
		private int m_ApiVersion;

		// Token: 0x0400097D RID: 2429
		private IntPtr m_LobbyDetailsHandle;

		// Token: 0x0400097E RID: 2430
		private IntPtr m_LocalUserId;

		// Token: 0x0400097F RID: 2431
		private int m_PresenceEnabled;

		// Token: 0x04000980 RID: 2432
		private IntPtr m_LocalRTCOptions;
	}
}
