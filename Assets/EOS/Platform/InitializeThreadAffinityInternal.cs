using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E3 RID: 1507
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InitializeThreadAffinityInternal : ISettable, IDisposable
	{
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x00026D66 File Offset: 0x00024F66
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x00026D6E File Offset: 0x00024F6E
		public ulong NetworkWork
		{
			get
			{
				return this.m_NetworkWork;
			}
			set
			{
				this.m_NetworkWork = value;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00026D77 File Offset: 0x00024F77
		// (set) Token: 0x060024A0 RID: 9376 RVA: 0x00026D7F File Offset: 0x00024F7F
		public ulong StorageIo
		{
			get
			{
				return this.m_StorageIo;
			}
			set
			{
				this.m_StorageIo = value;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00026D88 File Offset: 0x00024F88
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00026D90 File Offset: 0x00024F90
		public ulong WebSocketIo
		{
			get
			{
				return this.m_WebSocketIo;
			}
			set
			{
				this.m_WebSocketIo = value;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x00026D99 File Offset: 0x00024F99
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x00026DA1 File Offset: 0x00024FA1
		public ulong P2PIo
		{
			get
			{
				return this.m_P2PIo;
			}
			set
			{
				this.m_P2PIo = value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x00026DAA File Offset: 0x00024FAA
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x00026DB2 File Offset: 0x00024FB2
		public ulong HttpRequestIo
		{
			get
			{
				return this.m_HttpRequestIo;
			}
			set
			{
				this.m_HttpRequestIo = value;
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00026DBC File Offset: 0x00024FBC
		public void Set(InitializeThreadAffinity other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.NetworkWork = other.NetworkWork;
				this.StorageIo = other.StorageIo;
				this.WebSocketIo = other.WebSocketIo;
				this.P2PIo = other.P2PIo;
				this.HttpRequestIo = other.HttpRequestIo;
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00026E0F File Offset: 0x0002500F
		public void Set(object other)
		{
			this.Set(other as InitializeThreadAffinity);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04001146 RID: 4422
		private int m_ApiVersion;

		// Token: 0x04001147 RID: 4423
		private ulong m_NetworkWork;

		// Token: 0x04001148 RID: 4424
		private ulong m_StorageIo;

		// Token: 0x04001149 RID: 4425
		private ulong m_WebSocketIo;

		// Token: 0x0400114A RID: 4426
		private ulong m_P2PIo;

		// Token: 0x0400114B RID: 4427
		private ulong m_HttpRequestIo;
	}
}
