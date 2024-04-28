using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004CE RID: 1230
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserExternalAccountCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000914 RID: 2324
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0001FC16 File Offset: 0x0001DE16
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0001FC25 File Offset: 0x0001DE25
		public void Set(GetProductUserExternalAccountCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0001FC3D File Offset: 0x0001DE3D
		public void Set(object other)
		{
			this.Set(other as GetProductUserExternalAccountCountOptions);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0001FC4B File Offset: 0x0001DE4B
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000DEC RID: 3564
		private int m_ApiVersion;

		// Token: 0x04000DED RID: 3565
		private IntPtr m_TargetUserId;
	}
}
