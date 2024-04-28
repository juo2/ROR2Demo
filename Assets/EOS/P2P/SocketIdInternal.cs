using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002BB RID: 699
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SocketIdInternal : ISettable, IDisposable
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00012E14 File Offset: 0x00011014
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00012E30 File Offset: 0x00011030
		public string SocketName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SocketName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SocketName, value, 33);
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00012E41 File Offset: 0x00011041
		public void Set(SocketId other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SocketName = other.SocketName;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00012E59 File Offset: 0x00011059
		public void Set(object other)
		{
			this.Set(other as SocketId);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000859 RID: 2137
		private int m_ApiVersion;

		// Token: 0x0400085A RID: 2138
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
		private byte[] m_SocketName;
	}
}
