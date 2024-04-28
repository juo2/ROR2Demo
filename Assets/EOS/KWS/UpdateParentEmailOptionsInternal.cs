using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200040A RID: 1034
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParentEmailOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700075F RID: 1887
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0001A3D6 File Offset: 0x000185D6
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0001A3E5 File Offset: 0x000185E5
		public string ParentEmail
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParentEmail, value);
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public void Set(UpdateParentEmailOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ParentEmail = other.ParentEmail;
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0001A418 File Offset: 0x00018618
		public void Set(object other)
		{
			this.Set(other as UpdateParentEmailOptions);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0001A426 File Offset: 0x00018626
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ParentEmail);
		}

		// Token: 0x04000B9C RID: 2972
		private int m_ApiVersion;

		// Token: 0x04000B9D RID: 2973
		private IntPtr m_LocalUserId;

		// Token: 0x04000B9E RID: 2974
		private IntPtr m_ParentEmail;
	}
}
