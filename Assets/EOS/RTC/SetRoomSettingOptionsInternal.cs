using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001ED RID: 493
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetRoomSettingOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000384 RID: 900
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000385 RID: 901
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x17000386 RID: 902
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		public string SettingName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SettingName, value);
			}
		}

		// Token: 0x17000387 RID: 903
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0000E2E5 File Offset: 0x0000C4E5
		public string SettingValue
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SettingValue, value);
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		public void Set(SetRoomSettingOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.SettingName = other.SettingName;
				this.SettingValue = other.SettingValue;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0000E330 File Offset: 0x0000C530
		public void Set(object other)
		{
			this.Set(other as SetRoomSettingOptions);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0000E33E File Offset: 0x0000C53E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_SettingName);
			Helper.TryMarshalDispose(ref this.m_SettingValue);
		}

		// Token: 0x04000630 RID: 1584
		private int m_ApiVersion;

		// Token: 0x04000631 RID: 1585
		private IntPtr m_LocalUserId;

		// Token: 0x04000632 RID: 1586
		private IntPtr m_RoomName;

		// Token: 0x04000633 RID: 1587
		private IntPtr m_SettingName;

		// Token: 0x04000634 RID: 1588
		private IntPtr m_SettingValue;
	}
}
