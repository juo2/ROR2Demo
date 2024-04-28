using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000506 RID: 1286
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserLoginInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00020934 File Offset: 0x0001EB34
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x00020950 File Offset: 0x0001EB50
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0002095F File Offset: 0x0001EB5F
		public void Set(UserLoginInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DisplayName = other.DisplayName;
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00020977 File Offset: 0x0001EB77
		public void Set(object other)
		{
			this.Set(other as UserLoginInfo);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00020985 File Offset: 0x0001EB85
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_DisplayName);
		}

		// Token: 0x04000E4D RID: 3661
		private int m_ApiVersion;

		// Token: 0x04000E4E RID: 3662
		private IntPtr m_DisplayName;
	}
}
