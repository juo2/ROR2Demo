using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021C RID: 540
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0000F23C File Offset: 0x0000D43C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0000F258 File Offset: 0x0000D458
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0000F260 File Offset: 0x0000D460
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0000F27C File Offset: 0x0000D47C
		public EpicAccountId PresenceUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_PresenceUserId, out result);
				return result;
			}
		}

		// Token: 0x04000697 RID: 1687
		private IntPtr m_ClientData;

		// Token: 0x04000698 RID: 1688
		private IntPtr m_LocalUserId;

		// Token: 0x04000699 RID: 1689
		private IntPtr m_PresenceUserId;
	}
}
