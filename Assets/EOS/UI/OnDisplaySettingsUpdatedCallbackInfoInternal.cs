using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000051 RID: 81
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnDisplaySettingsUpdatedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00004D44 File Offset: 0x00002F44
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00004D60 File Offset: 0x00002F60
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00004D68 File Offset: 0x00002F68
		public bool IsVisible
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsVisible, out result);
				return result;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00004D84 File Offset: 0x00002F84
		public bool IsExclusiveInput
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsExclusiveInput, out result);
				return result;
			}
		}

		// Token: 0x04000215 RID: 533
		private IntPtr m_ClientData;

		// Token: 0x04000216 RID: 534
		private int m_IsVisible;

		// Token: 0x04000217 RID: 535
		private int m_IsExclusiveInput;
	}
}
