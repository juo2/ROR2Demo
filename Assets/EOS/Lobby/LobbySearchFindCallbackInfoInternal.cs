using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000360 RID: 864
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0001760F File Offset: 0x0001580F
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00017618 File Offset: 0x00015818
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00017634 File Offset: 0x00015834
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000A58 RID: 2648
		private Result m_ResultCode;

		// Token: 0x04000A59 RID: 2649
		private IntPtr m_ClientData;
	}
}
