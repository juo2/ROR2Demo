using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C3 RID: 707
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InstallModCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0001314D File Offset: 0x0001134D
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00013158 File Offset: 0x00011358
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00013174 File Offset: 0x00011374
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00013190 File Offset: 0x00011390
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00013198 File Offset: 0x00011398
		public ModIdentifier Mod
		{
			get
			{
				ModIdentifier result;
				Helper.TryMarshalGet<ModIdentifierInternal, ModIdentifier>(this.m_Mod, out result);
				return result;
			}
		}

		// Token: 0x04000871 RID: 2161
		private Result m_ResultCode;

		// Token: 0x04000872 RID: 2162
		private IntPtr m_LocalUserId;

		// Token: 0x04000873 RID: 2163
		private IntPtr m_ClientData;

		// Token: 0x04000874 RID: 2164
		private IntPtr m_Mod;
	}
}
