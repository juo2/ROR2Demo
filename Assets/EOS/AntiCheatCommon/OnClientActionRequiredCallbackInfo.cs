using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059B RID: 1435
	public class OnClientActionRequiredCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00024CDC File Offset: 0x00022EDC
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x00024CE4 File Offset: 0x00022EE4
		public object ClientData { get; private set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x00024CED File Offset: 0x00022EED
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x00024CF5 File Offset: 0x00022EF5
		public IntPtr ClientHandle { get; private set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00024CFE File Offset: 0x00022EFE
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x00024D06 File Offset: 0x00022F06
		public AntiCheatCommonClientAction ClientAction { get; private set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x00024D0F File Offset: 0x00022F0F
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x00024D17 File Offset: 0x00022F17
		public AntiCheatCommonClientActionReason ActionReasonCode { get; private set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x00024D20 File Offset: 0x00022F20
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x00024D28 File Offset: 0x00022F28
		public string ActionReasonDetailsString { get; private set; }

		// Token: 0x060022D7 RID: 8919 RVA: 0x00024D34 File Offset: 0x00022F34
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00024D4C File Offset: 0x00022F4C
		internal void Set(OnClientActionRequiredCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientAction = other.Value.ClientAction;
				this.ActionReasonCode = other.Value.ActionReasonCode;
				this.ActionReasonDetailsString = other.Value.ActionReasonDetailsString;
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00024DCB File Offset: 0x00022FCB
		public void Set(object other)
		{
			this.Set(other as OnClientActionRequiredCallbackInfoInternal?);
		}
	}
}
