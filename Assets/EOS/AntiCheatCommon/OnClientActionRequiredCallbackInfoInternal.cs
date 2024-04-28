using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059C RID: 1436
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientActionRequiredCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x00024DFC File Offset: 0x00022FFC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00024E04 File Offset: 0x00023004
		public IntPtr ClientHandle
		{
			get
			{
				return this.m_ClientHandle;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x00024E0C File Offset: 0x0002300C
		public AntiCheatCommonClientAction ClientAction
		{
			get
			{
				return this.m_ClientAction;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00024E14 File Offset: 0x00023014
		public AntiCheatCommonClientActionReason ActionReasonCode
		{
			get
			{
				return this.m_ActionReasonCode;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00024E1C File Offset: 0x0002301C
		public string ActionReasonDetailsString
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ActionReasonDetailsString, out result);
				return result;
			}
		}

		// Token: 0x04001072 RID: 4210
		private IntPtr m_ClientData;

		// Token: 0x04001073 RID: 4211
		private IntPtr m_ClientHandle;

		// Token: 0x04001074 RID: 4212
		private AntiCheatCommonClientAction m_ClientAction;

		// Token: 0x04001075 RID: 4213
		private AntiCheatCommonClientActionReason m_ActionReasonCode;

		// Token: 0x04001076 RID: 4214
		private IntPtr m_ActionReasonDetailsString;
	}
}
