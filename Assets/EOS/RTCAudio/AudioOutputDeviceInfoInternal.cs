using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017D RID: 381
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioOutputDeviceInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000B730 File Offset: 0x00009930
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0000B74C File Offset: 0x0000994C
		public bool DefaultDevice
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_DefaultDevice, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DefaultDevice, value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0000B75C File Offset: 0x0000995C
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0000B778 File Offset: 0x00009978
		public string DeviceId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeviceId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DeviceId, value);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0000B788 File Offset: 0x00009988
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0000B7A4 File Offset: 0x000099A4
		public string DeviceName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeviceName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DeviceName, value);
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000B7B3 File Offset: 0x000099B3
		public void Set(AudioOutputDeviceInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DefaultDevice = other.DefaultDevice;
				this.DeviceId = other.DeviceId;
				this.DeviceName = other.DeviceName;
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000B7E3 File Offset: 0x000099E3
		public void Set(object other)
		{
			this.Set(other as AudioOutputDeviceInfo);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000B7F1 File Offset: 0x000099F1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_DeviceId);
			Helper.TryMarshalDispose(ref this.m_DeviceName);
		}

		// Token: 0x040004F7 RID: 1271
		private int m_ApiVersion;

		// Token: 0x040004F8 RID: 1272
		private int m_DefaultDevice;

		// Token: 0x040004F9 RID: 1273
		private IntPtr m_DeviceId;

		// Token: 0x040004FA RID: 1274
		private IntPtr m_DeviceName;
	}
}
