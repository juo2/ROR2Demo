using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000179 RID: 377
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioInputDeviceInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0000B478 File Offset: 0x00009678
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0000B494 File Offset: 0x00009694
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0000B4A4 File Offset: 0x000096A4
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0000B4C0 File Offset: 0x000096C0
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0000B4D0 File Offset: 0x000096D0
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0000B4EC File Offset: 0x000096EC
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

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000B4FB File Offset: 0x000096FB
		public void Set(AudioInputDeviceInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DefaultDevice = other.DefaultDevice;
				this.DeviceId = other.DeviceId;
				this.DeviceName = other.DeviceName;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000B52B File Offset: 0x0000972B
		public void Set(object other)
		{
			this.Set(other as AudioInputDeviceInfo);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000B539 File Offset: 0x00009739
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_DeviceId);
			Helper.TryMarshalDispose(ref this.m_DeviceName);
		}

		// Token: 0x040004E8 RID: 1256
		private int m_ApiVersion;

		// Token: 0x040004E9 RID: 1257
		private int m_DefaultDevice;

		// Token: 0x040004EA RID: 1258
		private IntPtr m_DeviceId;

		// Token: 0x040004EB RID: 1259
		private IntPtr m_DeviceName;
	}
}
