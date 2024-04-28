using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001EF RID: 495
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetSettingOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700038A RID: 906
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0000E392 File Offset: 0x0000C592
		public string SettingName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SettingName, value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
		public string SettingValue
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SettingValue, value);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public void Set(SetSettingOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SettingName = other.SettingName;
				this.SettingValue = other.SettingValue;
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		public void Set(object other)
		{
			this.Set(other as SetSettingOptions);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SettingName);
			Helper.TryMarshalDispose(ref this.m_SettingValue);
		}

		// Token: 0x04000637 RID: 1591
		private int m_ApiVersion;

		// Token: 0x04000638 RID: 1592
		private IntPtr m_SettingName;

		// Token: 0x04000639 RID: 1593
		private IntPtr m_SettingValue;
	}
}
