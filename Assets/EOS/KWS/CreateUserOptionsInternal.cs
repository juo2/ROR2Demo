using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E4 RID: 996
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000721 RID: 1825
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00019733 File Offset: 0x00017933
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000722 RID: 1826
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x00019742 File Offset: 0x00017942
		public string DateOfBirth
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DateOfBirth, value);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x00019751 File Offset: 0x00017951
		public string ParentEmail
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParentEmail, value);
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00019760 File Offset: 0x00017960
		public void Set(CreateUserOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.DateOfBirth = other.DateOfBirth;
				this.ParentEmail = other.ParentEmail;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00019790 File Offset: 0x00017990
		public void Set(object other)
		{
			this.Set(other as CreateUserOptions);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0001979E File Offset: 0x0001799E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_DateOfBirth);
			Helper.TryMarshalDispose(ref this.m_ParentEmail);
		}

		// Token: 0x04000B4B RID: 2891
		private int m_ApiVersion;

		// Token: 0x04000B4C RID: 2892
		private IntPtr m_LocalUserId;

		// Token: 0x04000B4D RID: 2893
		private IntPtr m_DateOfBirth;

		// Token: 0x04000B4E RID: 2894
		private IntPtr m_ParentEmail;
	}
}
