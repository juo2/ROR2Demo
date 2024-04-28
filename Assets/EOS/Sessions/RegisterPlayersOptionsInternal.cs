using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FD RID: 253
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlayersOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000196 RID: 406
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x00008124 File Offset: 0x00006324
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00008133 File Offset: 0x00006333
		public ProductUserId[] PlayersToRegister
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_PlayersToRegister, value, out this.m_PlayersToRegisterCount);
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00008148 File Offset: 0x00006348
		public void Set(RegisterPlayersOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.SessionName;
				this.PlayersToRegister = other.PlayersToRegister;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000816C File Offset: 0x0000636C
		public void Set(object other)
		{
			this.Set(other as RegisterPlayersOptions);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000817A File Offset: 0x0000637A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_PlayersToRegister);
		}

		// Token: 0x04000389 RID: 905
		private int m_ApiVersion;

		// Token: 0x0400038A RID: 906
		private IntPtr m_SessionName;

		// Token: 0x0400038B RID: 907
		private IntPtr m_PlayersToRegister;

		// Token: 0x0400038C RID: 908
		private uint m_PlayersToRegisterCount;
	}
}
