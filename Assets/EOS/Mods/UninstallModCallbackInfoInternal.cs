using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D5 RID: 725
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UninstallModCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000138BD File Offset: 0x00011ABD
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x000138C8 File Offset: 0x00011AC8
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000138E4 File Offset: 0x00011AE4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00013900 File Offset: 0x00011B00
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00013908 File Offset: 0x00011B08
		public ModIdentifier Mod
		{
			get
			{
				ModIdentifier result;
				Helper.TryMarshalGet<ModIdentifierInternal, ModIdentifier>(this.m_Mod, out result);
				return result;
			}
		}

		// Token: 0x0400089B RID: 2203
		private Result m_ResultCode;

		// Token: 0x0400089C RID: 2204
		private IntPtr m_LocalUserId;

		// Token: 0x0400089D RID: 2205
		private IntPtr m_ClientData;

		// Token: 0x0400089E RID: 2206
		private IntPtr m_Mod;
	}
}
