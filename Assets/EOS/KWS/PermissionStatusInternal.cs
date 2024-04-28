using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F8 RID: 1016
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PermissionStatusInternal : ISettable, IDisposable
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00019CA8 File Offset: 0x00017EA8
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x00019CC4 File Offset: 0x00017EC4
		public string Name
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Name, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Name, value);
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00019CD3 File Offset: 0x00017ED3
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x00019CDB File Offset: 0x00017EDB
		public KWSPermissionStatus Status
		{
			get
			{
				return this.m_Status;
			}
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00019CE4 File Offset: 0x00017EE4
		public void Set(PermissionStatus other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Name;
				this.Status = other.Status;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00019D08 File Offset: 0x00017F08
		public void Set(object other)
		{
			this.Set(other as PermissionStatus);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00019D16 File Offset: 0x00017F16
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Name);
		}

		// Token: 0x04000B69 RID: 2921
		private int m_ApiVersion;

		// Token: 0x04000B6A RID: 2922
		private IntPtr m_Name;

		// Token: 0x04000B6B RID: 2923
		private KWSPermissionStatus m_Status;
	}
}
