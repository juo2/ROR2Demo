using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000177 RID: 375
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioDevicesChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0000B3D4 File Offset: 0x000095D4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x040004E4 RID: 1252
		private IntPtr m_ClientData;
	}
}
