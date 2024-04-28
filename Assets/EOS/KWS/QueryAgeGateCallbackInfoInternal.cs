using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003FC RID: 1020
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryAgeGateCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00019EC5 File Offset: 0x000180C5
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00019ED0 File Offset: 0x000180D0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00019EEC File Offset: 0x000180EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00019EF4 File Offset: 0x000180F4
		public string CountryCode
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CountryCode, out result);
				return result;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00019F10 File Offset: 0x00018110
		public uint AgeOfConsent
		{
			get
			{
				return this.m_AgeOfConsent;
			}
		}

		// Token: 0x04000B74 RID: 2932
		private Result m_ResultCode;

		// Token: 0x04000B75 RID: 2933
		private IntPtr m_ClientData;

		// Token: 0x04000B76 RID: 2934
		private IntPtr m_CountryCode;

		// Token: 0x04000B77 RID: 2935
		private uint m_AgeOfConsent;
	}
}
