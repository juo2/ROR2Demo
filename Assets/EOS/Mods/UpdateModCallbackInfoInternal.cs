using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D9 RID: 729
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateModCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00013A81 File Offset: 0x00011C81
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00013A8C File Offset: 0x00011C8C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00013AC4 File Offset: 0x00011CC4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00013ACC File Offset: 0x00011CCC
		public ModIdentifier Mod
		{
			get
			{
				ModIdentifier result;
				Helper.TryMarshalGet<ModIdentifierInternal, ModIdentifier>(this.m_Mod, out result);
				return result;
			}
		}

		// Token: 0x040008A8 RID: 2216
		private Result m_ResultCode;

		// Token: 0x040008A9 RID: 2217
		private IntPtr m_LocalUserId;

		// Token: 0x040008AA RID: 2218
		private IntPtr m_ClientData;

		// Token: 0x040008AB RID: 2219
		private IntPtr m_Mod;
	}
}
