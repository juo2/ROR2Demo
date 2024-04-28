using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004BE RID: 1214
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008F0 RID: 2288
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x0001F5DD File Offset: 0x0001D7DD
		public string DeviceModel
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DeviceModel, value);
			}
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		public void Set(CreateDeviceIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DeviceModel = other.DeviceModel;
			}
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0001F604 File Offset: 0x0001D804
		public void Set(object other)
		{
			this.Set(other as CreateDeviceIdOptions);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0001F612 File Offset: 0x0001D812
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_DeviceModel);
		}

		// Token: 0x04000DC4 RID: 3524
		private int m_ApiVersion;

		// Token: 0x04000DC5 RID: 3525
		private IntPtr m_DeviceModel;
	}
}
