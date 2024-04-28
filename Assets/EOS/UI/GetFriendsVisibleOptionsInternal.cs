using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000045 RID: 69
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsVisibleOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700006D RID: 109
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00004AED File Offset: 0x00002CED
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00004AFC File Offset: 0x00002CFC
		public void Set(GetFriendsVisibleOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00004B14 File Offset: 0x00002D14
		public void Set(object other)
		{
			this.Set(other as GetFriendsVisibleOptions);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00004B22 File Offset: 0x00002D22
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000191 RID: 401
		private int m_ApiVersion;

		// Token: 0x04000192 RID: 402
		private IntPtr m_LocalUserId;
	}
}
